using NHapi.Base.Parser;
using NHapi.Base.Util;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Collections.Concurrent;

namespace HL7TCPSender
{
    public partial class HL7Sender : Form
    {
        private Queue<string> messageFiles = new Queue<string>();
        private UILogger _uiLogger;

        public HL7Sender()
        {
            InitializeComponent();

            _uiLogger = new UILogger();
            _uiLogger.OnLog += UILogger_OnLog;

            var config = LoadConfig();
            txtSendingHost.Text = config.SendingHost;

            int portMax = (int)numPort.Maximum;
            int portMin = (int)numPort.Minimum;

            if (config.Port < portMin || config.Port > portMax)
            {
                MessageBox.Show(
                    $"The port number in appsettings.json ({config.Port}) is outside the allowed range.\n" +
                    $"Please select a value between {portMin} and {portMax}.",
                    "Invalid Configuration Value",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                numPort.Value = portMin;
                _uiLogger.Info($"Invalid port in configuration ({config.Port}). Reverting to minimum allowed value {portMin}.");

            }
            else
            {
                numPort.Value = config.Port;
            }

            txtfolderPath.Text = config.FolderPath;
            numDelayMs.Value = config.DelayMs;
            numMaxRetries.Value = config.MaxRetries;
        }

        private void UILogger_OnLog(string message)
        {
            Color color = Color.Black;
            if (message.Contains("Error", StringComparison.OrdinalIgnoreCase)) color = Color.IndianRed;
            else if (message.Contains("Warning", StringComparison.OrdinalIgnoreCase)) color = Color.Gold;
            else if (message.Contains("Information", StringComparison.OrdinalIgnoreCase)) color = Color.DarkGreen;
            else if (message.Contains("Debug", StringComparison.OrdinalIgnoreCase)) color = Color.DodgerBlue;

            AppendColoredText(message + Environment.NewLine, color);
        }

        private void AppendColoredText(string text, Color color)
        {
            if (txtLogs.InvokeRequired)
            {
                txtLogs.BeginInvoke(new Action(() => AppendColoredText(text, color)));
                return;
            }

            txtLogs.SelectionStart = txtLogs.TextLength;
            txtLogs.SelectionColor = color;
            txtLogs.AppendText(text);
            txtLogs.SelectionColor = txtLogs.ForeColor;
            txtLogs.ScrollToCaret();
        }

        private void ResetProgress(int total)
        {
            progressBarSend.Value = 0;
            progressBarSend.Maximum = total;
            lblProgress.Text = $"0 / {total} sent";
        }

        private void IncrementProgress(int current, int total)
        {
            if (current <= total)
            {
                progressBarSend.Value = current;
                lblProgress.Text = $"{current} / {total} sent";
            }
        }

        private string GetSequenceFromFile(string filePath)
        {
            try
            {
                string text = File.ReadAllText(filePath);
                var parser = new PipeParser();
                var message = parser.Parse(text);

                // Generic, model-agnostic way
                var terser = new Terser(message);
                string controlId = terser.Get("/MSH-10");
                return controlId ?? "";

            }
            catch (Exception ex)
            {
                _uiLogger.Error($"Parsing {Path.GetFileName(filePath)}: {ex.Message} failed");
                return "";
            }
        }

        private void btn_dirBrowse_Click(object sender, EventArgs e)
        {
            string folderPath = "";

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowser.SelectedPath;
                txtfolderPath.Text = folderPath;
            }
        }

        private void btn_clearLogs_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtLogs.Text))
            {
                txtLogs.Clear();
            }
        }

        private void btn_clearPath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtfolderPath.Text.Trim()))
            {
                txtfolderPath.Clear();
            }
        }

        private async void btn_sendAll_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtfolderPath.Text.Trim()))
            {
                MessageBox.Show("Select a Message Path before queuing messages");
                return;
            }

            btn_sendAll.Enabled = false;
            btn_sendSingle.Enabled = false;
            int total = messageFiles.Count;

            try
            {
                if (total == 0)
                {
                    _uiLogger.Info($"No messages in queue");
                    return;
                }

                ResetProgress(total);

                int sentCount = 0;

                while (messageFiles.Count > 0)
                {
                    string nextFile = messageFiles.Peek(); // don’t dequeue yet

                    bool success = await SendMessageAsync(nextFile);
                    sentCount++;

                    IncrementProgress(sentCount, total);

                    if (success)
                    {
                        messageFiles.Dequeue();
                    }
                    else
                    {
                        _uiLogger.Info($"Stopping due to repeated failure on {Path.GetFileName(nextFile)}");
                        break;
                    }

                    await Task.Delay((int)numDelayMs.Value);
                }

                progressBarSend.Value = progressBarSend.Maximum;
                lblProgress.Text = $"{sentCount} / {total} sent";
                _uiLogger.Info("Sending Complete!");
            }
            finally
            {
                btn_sendAll.Enabled = true;
                btn_sendSingle.Enabled = true;
            }
        }

        private async Task<bool> SendMessageAsync(string filePath)
        {
            string host = txtSendingHost.Text.Trim();
            int port = (int)numPort.Value;
            int MaxRetries = (int)numMaxRetries.Value;
            string hl7 = await File.ReadAllTextAsync(filePath);
            string fileName = Path.GetFileName(filePath);

            byte SB = 0x0b, EB = 0x1c, CR = 0x0d;
            byte[] framed = BuildMllpMessage(hl7, SB, EB, CR);

            for (int attempt = 1; attempt <= MaxRetries; attempt++)
            {
                _uiLogger.Info($" Sending {fileName} (Attempt {attempt}/{MaxRetries})");

                try
                {
                    using (TcpClient client = new TcpClient())
                    {
                        await client.ConnectAsync(host, port);
                        using (NetworkStream stream = client.GetStream())
                        {
                            progressBarSend.Style = ProgressBarStyle.Marquee;
                            await stream.WriteAsync(framed, 0, framed.Length);
                            client.ReceiveTimeout = 5000;

                            byte[] buffer = new byte[4096];
                            int bytesRead;
                            List<byte> ackData = new List<byte>();

                            do
                            {
                                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                                if (bytesRead > 0)
                                {
                                    ackData.AddRange(buffer.Take(bytesRead));
                                    if (ackData.Count >= 2 &&
                                        ackData[^2] == EB && ackData[^1] == CR)
                                    {
                                        break;
                                    }
                                }
                            } while (bytesRead > 0);

                            if (ackData.Count > 0)
                            {
                                string ack = DecodeAck(ackData.ToArray());
                                _uiLogger.Info($"ACK received for {fileName}");
                                txtAck.Text = ack;
                                progressBarSend.Style = ProgressBarStyle.Continuous;

                                string seq = GetSequenceFromFile(filePath);
                                txtlastSequence.Text = seq;

                                await Task.Delay(200);

                                if (chkArchive.Checked)
                                {
                                    ArchiveMessage(filePath, "Sent");
                                }
                                else
                                {
                                    DeleteMessage(filePath);
                                }

                                Invoke(new Action(async () =>
                                {
                                    string name = Path.GetFileName(filePath);
                                    int index = lstMessages.Items.IndexOf(name);

                                    if (index >= 0)
                                    {
                                        lstMessages.SelectedIndex = index;
                                        lstMessages.TopIndex = index;

                                        // Optional: give a small delay so highlight is visible before removal
                                        await Task.Delay(200);
                                        lstMessages.Items.RemoveAt(index);
                                    }

                                    int messagesRemaining = int.Parse(txttotalQueue.Text) - 1;
                                    txttotalQueue.Text = messagesRemaining.ToString();
                                }));

                                await Task.Delay((int)numDelayMs.Value);
                                return true;
                            }
                            else
                            {
                                _uiLogger.Info("No ACK received, retrying...");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _uiLogger.Error($"{ex.Message}");
                }

                await Task.Delay(1000);
            }

            _uiLogger.Info($"Failed to send {fileName} after {MaxRetries} retries.");
            progressBarSend.Style = ProgressBarStyle.Continuous;
            ArchiveMessage(filePath, "Failed");
            return false;
        }

        private void ArchiveMessage(string filePath, string subfolder)
        {
            try
            {
                string baseFolder = Path.GetDirectoryName(filePath)!;
                string targetFolder = Path.Combine(baseFolder, subfolder);
                Directory.CreateDirectory(targetFolder);

                string fileName = Path.GetFileName(filePath);
                string destination = Path.Combine(targetFolder, fileName);

                // Overwrite if it already exists
                File.Move(filePath, destination, true);
                _uiLogger.Info($"Moved {fileName} ? {subfolder}");
            }
            catch (Exception ex)
            {
                _uiLogger.Error($"Moving file {filePath}: {ex.Message}");
            }
        }

        private void DeleteMessage(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    _uiLogger.Info($"Deleted message file: {Path.GetFileName(filePath)}");
                }
                else
                {
                    _uiLogger.Info($"File not found for deletion: {filePath}");
                }
            }
            catch (Exception ex)
            {
                _uiLogger.Error($"Deleting file {filePath}: {ex.Message}");
            }
        }

        private static byte[] BuildMllpMessage(string message, byte SB, byte EB, byte CR)
        {
            var msgBytes = Encoding.UTF8.GetBytes(message);
            var framed = new byte[msgBytes.Length + 3];
            framed[0] = SB;
            Buffer.BlockCopy(msgBytes, 0, framed, 1, msgBytes.Length);
            framed[^2] = EB;
            framed[^1] = CR;
            return framed;
        }

        private static string DecodeAck(byte[] ackData)
        {
            byte[] trimmed = ackData
                .Where(b => b != 0x0b && b != 0x1c && b != 0x0d)
                .ToArray();
            return Encoding.UTF8.GetString(trimmed);
        }

        private async void btn_queueMessages_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtfolderPath.Text.Trim()))
            {
                MessageBox.Show("Select a Message Path before queuing messages");
                return;
            }

            string folder = txtfolderPath.Text.Trim();

            if (!Directory.Exists(folder))
            {
                MessageBox.Show("Message folder not found!");
                return;
            }

            var files = Directory.GetFiles(folder, "*.hl7");
            int total = files.Length;

            if (total == 0)
            {
                _uiLogger.Info("No HL7 messages found in folder.");
                return;
            }

            progressBarSend.Value = 0;
            progressBarSend.Maximum = total;
            progressBarSend.Style = ProgressBarStyle.Continuous;
            lblProgress.Text = $"Queued 0 of {total}...";

            var parser = new NHapi.Base.Parser.PipeParser();
            var fileWithSeqConcurrent = new ConcurrentBag<(string FilePath, string Seq)>();
            int completed = 0;

            await Task.Run(() =>
            {
                Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = 4 }, file =>
                {
                    try
                    {

                        string messageText = File.ReadAllText(file);
                        var message = parser.Parse(messageText);
                        var terser = new NHapi.Base.Util.Terser(message);

                        string seq = terser.Get("/MSH-10") ?? "0";
                        fileWithSeqConcurrent.Add((file, seq));
                    }
                    catch
                    {
                        fileWithSeqConcurrent.Add((file, "0"));
                    }
                    finally
                    {
                        Interlocked.Increment(ref completed);
                        Invoke(new Action(() =>
                        {
                            progressBarSend.Value = completed;
                            lblProgress.Text = $"Queued {completed} of {total}...";
                        }));
                    }
                });
            });

            // Sort results and populate list
            var fileWithSeq = fileWithSeqConcurrent.ToList();
            var sortedFiles = fileWithSeq
                .OrderBy(f =>
                {
                    if (long.TryParse(new string(f.Seq.Where(char.IsDigit).ToArray()), out long num))
                        return num;
                    return long.MaxValue;
                })
                .ThenBy(f => f.Seq)
                .Select(f => f.FilePath)
                .ToList();

            txttotalQueue.Text = sortedFiles.Count.ToString();
            messageFiles = new Queue<string>(sortedFiles);

            lstMessages.Items.Clear();
            foreach (var file in sortedFiles)
                lstMessages.Items.Add(Path.GetFileName(file));

            lblProgress.Text = $"Queued {sortedFiles.Count} message(s)";
            _uiLogger.Info($"Queued {sortedFiles.Count} messages from {folder} (sorted by MSH-10).");
        }

        private async void btn_sendSingle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtfolderPath.Text.Trim()))
            {
                MessageBox.Show("Select a Message Path before queuing messages");
                return;
            }

            btn_sendAll.Enabled = false;
            btn_sendSingle.Enabled = false;
            try
            {
                if (messageFiles.Count == 0)
                {
                    _uiLogger.Info("No messages in queue.");
                    return;
                }

                int total = 1;
                ResetProgress(total);

                string nextFile = messageFiles.Dequeue();
                bool success = await SendMessageAsync(nextFile);

                if (success)
                {
                    IncrementProgress(1, 1);
                    lblProgress.Text = "1 / 1 sent successfully";
                    _uiLogger.Info("Single message sent successfully!");
                }
                else
                {
                    lblProgress.Text = "0 / 1 sent (failed)";
                }
            }
            finally
            {
                btn_sendAll.Enabled = true;
                btn_sendSingle.Enabled = true;
            }
        }

        private void btnRequeueFailed_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtfolderPath.Text.Trim()))
            {
                MessageBox.Show("Select a Message Path before attempting to move failed messages");
                return;
            }

            string baseFolder = txtfolderPath.Text.Trim();
            string failedFolder = Path.Combine(baseFolder, "Failed");
            string toSendFolder = baseFolder;

            if (!Directory.Exists(failedFolder))
            {
                _uiLogger.Info("No 'Failed' folder found.");
                return;
            }

            var failedFiles = Directory.GetFiles(failedFolder, "*.hl7");

            if (failedFiles.Length == 0)
            {
                _uiLogger.Info("No failed messages to re-queue.");
                return;
            }

            int movedCount = 0;

            foreach (var file in failedFiles)
            {
                try
                {
                    string dest = Path.Combine(toSendFolder, Path.GetFileName(file));
                    File.Move(file, dest, true);
                    movedCount++;
                }
                catch (Exception ex)
                {
                    _uiLogger.Error($"Failed to move {Path.GetFileName(file)}: {ex.Message}");
                }
            }

            _uiLogger.Info($"Re-queued {movedCount} failed message(s).");

            // Optionally reload the list of queued messages
            btn_queueMessages_Click(sender, e);
        }

        private string GetConfigPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        }

        private AppConfig LoadConfig()
        {
            try
            {
                string path = GetConfigPath();
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
                }
            }
            catch (Exception ex)
            {
                _uiLogger.Error($"Failed to load config: {ex.Message}");
            }
            return new AppConfig(); // defaults
        }

        private void SaveConfig(AppConfig config)
        {
            try
            {
                string path = GetConfigPath();
                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
                _uiLogger.Info("Configuration saved.");
            }
            catch (Exception ex)
            {
                _uiLogger.Error($"Failed to save config: {ex.Message}");
            }
        }

        private void HL7Sender_FormClosing(object sender, FormClosingEventArgs e)
        {
            var config = new AppConfig
            {
                SendingHost = txtSendingHost.Text,
                Port = (int)numPort.Value,
                FolderPath = txtfolderPath.Text,
                DelayMs = (int)numDelayMs.Value,
                MaxRetries = (int)numMaxRetries.Value
            };

            SaveConfig(config);
        }
    }
}
