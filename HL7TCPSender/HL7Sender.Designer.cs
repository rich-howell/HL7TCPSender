namespace HL7TCPSender
{
    partial class HL7Sender
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_queueMessages = new Button();
            btn_sendSingle = new Button();
            btn_sendAll = new Button();
            label1 = new Label();
            txtAck = new TextBox();
            label3 = new Label();
            txttotalQueue = new TextBox();
            label4 = new Label();
            txtlastSequence = new TextBox();
            btn_clearLogs = new Button();
            btn_dirBrowse = new Button();
            folderBrowser = new FolderBrowserDialog();
            txtfolderPath = new TextBox();
            lblmessagePath = new Label();
            btn_clearPath = new Button();
            lstMessages = new ListBox();
            chkArchive = new CheckBox();
            btnRequeueFailed = new Button();
            progressBarSend = new ProgressBar();
            lblProgress = new Label();
            label7 = new Label();
            label8 = new Label();
            numDelayMs = new NumericUpDown();
            numMaxRetries = new NumericUpDown();
            txtSendingHost = new TextBox();
            label9 = new Label();
            numPort = new NumericUpDown();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            txtLogs = new RichTextBox();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            groupBox5 = new GroupBox();
            groupBox6 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)numDelayMs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxRetries).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            SuspendLayout();
            // 
            // btn_queueMessages
            // 
            btn_queueMessages.Location = new Point(6, 20);
            btn_queueMessages.Name = "btn_queueMessages";
            btn_queueMessages.Size = new Size(107, 23);
            btn_queueMessages.TabIndex = 0;
            btn_queueMessages.Text = "Queue Messages";
            btn_queueMessages.UseVisualStyleBackColor = true;
            btn_queueMessages.Click += btn_queueMessages_Click;
            // 
            // btn_sendSingle
            // 
            btn_sendSingle.Location = new Point(134, 20);
            btn_sendSingle.Name = "btn_sendSingle";
            btn_sendSingle.Size = new Size(135, 23);
            btn_sendSingle.TabIndex = 1;
            btn_sendSingle.Text = "Send Single Message";
            btn_sendSingle.UseVisualStyleBackColor = true;
            btn_sendSingle.Click += btn_sendSingle_Click;
            // 
            // btn_sendAll
            // 
            btn_sendAll.Location = new Point(288, 20);
            btn_sendAll.Name = "btn_sendAll";
            btn_sendAll.Size = new Size(163, 23);
            btn_sendAll.TabIndex = 2;
            btn_sendAll.Text = "Send All Messages";
            btn_sendAll.UseVisualStyleBackColor = true;
            btn_sendAll.Click += btn_sendAll_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(6, 22);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 4;
            label1.Text = "Port Number";
            // 
            // txtAck
            // 
            txtAck.Location = new Point(7, 22);
            txtAck.Multiline = true;
            txtAck.Name = "txtAck";
            txtAck.Size = new Size(474, 100);
            txtAck.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(25, 70);
            label3.Name = "label3";
            label3.Size = new Size(137, 15);
            label3.TabIndex = 8;
            label3.Text = "Total Messages Queued";
            // 
            // txttotalQueue
            // 
            txttotalQueue.Location = new Point(168, 67);
            txttotalQueue.Name = "txttotalQueue";
            txttotalQueue.PlaceholderText = "0";
            txttotalQueue.ReadOnly = true;
            txttotalQueue.Size = new Size(80, 23);
            txttotalQueue.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(254, 70);
            label4.Name = "label4";
            label4.Size = new Size(165, 15);
            label4.TabIndex = 9;
            label4.Text = "Last Sequence Number Sent";
            // 
            // txtlastSequence
            // 
            txtlastSequence.Location = new Point(425, 67);
            txtlastSequence.Name = "txtlastSequence";
            txtlastSequence.PlaceholderText = "0";
            txtlastSequence.ReadOnly = true;
            txtlastSequence.Size = new Size(80, 23);
            txtlastSequence.TabIndex = 10;
            // 
            // btn_clearLogs
            // 
            btn_clearLogs.Location = new Point(318, 124);
            btn_clearLogs.Name = "btn_clearLogs";
            btn_clearLogs.Size = new Size(163, 23);
            btn_clearLogs.TabIndex = 13;
            btn_clearLogs.Text = "Clear Logs";
            btn_clearLogs.UseVisualStyleBackColor = true;
            btn_clearLogs.Click += btn_clearLogs_Click;
            // 
            // btn_dirBrowse
            // 
            btn_dirBrowse.Location = new Point(325, 97);
            btn_dirBrowse.Name = "btn_dirBrowse";
            btn_dirBrowse.Size = new Size(67, 23);
            btn_dirBrowse.TabIndex = 14;
            btn_dirBrowse.Text = "Browse...";
            btn_dirBrowse.UseVisualStyleBackColor = true;
            btn_dirBrowse.Click += btn_dirBrowse_Click;
            // 
            // txtfolderPath
            // 
            txtfolderPath.Location = new Point(8, 97);
            txtfolderPath.Name = "txtfolderPath";
            txtfolderPath.PlaceholderText = "C:\\HL7Sender";
            txtfolderPath.ReadOnly = true;
            txtfolderPath.Size = new Size(311, 23);
            txtfolderPath.TabIndex = 15;
            // 
            // lblmessagePath
            // 
            lblmessagePath.AutoSize = true;
            lblmessagePath.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblmessagePath.Location = new Point(5, 79);
            lblmessagePath.Name = "lblmessagePath";
            lblmessagePath.Size = new Size(83, 15);
            lblmessagePath.TabIndex = 16;
            lblmessagePath.Text = "Message Path";
            // 
            // btn_clearPath
            // 
            btn_clearPath.Location = new Point(398, 97);
            btn_clearPath.Name = "btn_clearPath";
            btn_clearPath.Size = new Size(59, 23);
            btn_clearPath.TabIndex = 17;
            btn_clearPath.Text = "Clear";
            btn_clearPath.UseVisualStyleBackColor = true;
            btn_clearPath.Click += btn_clearPath_Click;
            // 
            // lstMessages
            // 
            lstMessages.FormattingEnabled = true;
            lstMessages.ItemHeight = 15;
            lstMessages.Location = new Point(6, 22);
            lstMessages.Name = "lstMessages";
            lstMessages.Size = new Size(525, 394);
            lstMessages.TabIndex = 18;
            // 
            // chkArchive
            // 
            chkArchive.AutoSize = true;
            chkArchive.Location = new Point(136, 48);
            chkArchive.Name = "chkArchive";
            chkArchive.Size = new Size(146, 19);
            chkArchive.TabIndex = 20;
            chkArchive.Text = "Archive Sent Messages";
            chkArchive.UseVisualStyleBackColor = true;
            // 
            // btnRequeueFailed
            // 
            btnRequeueFailed.Location = new Point(7, 70);
            btnRequeueFailed.Name = "btnRequeueFailed";
            btnRequeueFailed.Size = new Size(106, 23);
            btnRequeueFailed.TabIndex = 21;
            btnRequeueFailed.Text = "Move Failures";
            btnRequeueFailed.UseVisualStyleBackColor = true;
            btnRequeueFailed.Click += btnRequeueFailed_Click;
            // 
            // progressBarSend
            // 
            progressBarSend.Location = new Point(134, 70);
            progressBarSend.Name = "progressBarSend";
            progressBarSend.Size = new Size(315, 23);
            progressBarSend.TabIndex = 22;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(710, 476);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(0, 15);
            lblProgress.TabIndex = 23;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label7.Location = new Point(224, 24);
            label7.Name = "label7";
            label7.Size = new Size(74, 15);
            label7.TabIndex = 25;
            label7.Text = "Retry Count";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label8.Location = new Point(349, 24);
            label8.Name = "label8";
            label8.Size = new Size(129, 15);
            label8.TabIndex = 26;
            label8.Text = "Pause Between Sends";
            // 
            // numDelayMs
            // 
            numDelayMs.Location = new Point(352, 48);
            numDelayMs.Name = "numDelayMs";
            numDelayMs.Size = new Size(129, 23);
            numDelayMs.TabIndex = 28;
            // 
            // numMaxRetries
            // 
            numMaxRetries.Location = new Point(226, 48);
            numMaxRetries.Name = "numMaxRetries";
            numMaxRetries.Size = new Size(120, 23);
            numMaxRetries.TabIndex = 29;
            // 
            // txtSendingHost
            // 
            txtSendingHost.Location = new Point(98, 49);
            txtSendingHost.Name = "txtSendingHost";
            txtSendingHost.PlaceholderText = "127.0.0.1";
            txtSendingHost.Size = new Size(122, 23);
            txtSendingHost.TabIndex = 30;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label9.Location = new Point(98, 21);
            label9.Name = "label9";
            label9.Size = new Size(33, 15);
            label9.TabIndex = 31;
            label9.Text = "Host";
            // 
            // numPort
            // 
            numPort.Location = new Point(8, 49);
            numPort.Name = "numPort";
            numPort.Size = new Size(84, 23);
            numPort.TabIndex = 32;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(numPort);
            groupBox1.Controls.Add(btn_dirBrowse);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(txtfolderPath);
            groupBox1.Controls.Add(txtSendingHost);
            groupBox1.Controls.Add(lblmessagePath);
            groupBox1.Controls.Add(numMaxRetries);
            groupBox1.Controls.Add(btn_clearPath);
            groupBox1.Controls.Add(numDelayMs);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label8);
            groupBox1.Location = new Point(10, 311);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(489, 131);
            groupBox1.TabIndex = 33;
            groupBox1.TabStop = false;
            groupBox1.Text = "Configuration";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtLogs);
            groupBox2.Controls.Add(btn_clearLogs);
            groupBox2.Location = new Point(10, 146);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(490, 155);
            groupBox2.TabIndex = 34;
            groupBox2.TabStop = false;
            groupBox2.Text = "Logs";
            // 
            // txtLogs
            // 
            txtLogs.Location = new Point(8, 22);
            txtLogs.Name = "txtLogs";
            txtLogs.Size = new Size(476, 96);
            txtLogs.TabIndex = 14;
            txtLogs.Text = "";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btn_queueMessages);
            groupBox3.Controls.Add(btn_sendSingle);
            groupBox3.Controls.Add(btn_sendAll);
            groupBox3.Controls.Add(btnRequeueFailed);
            groupBox3.Controls.Add(progressBarSend);
            groupBox3.Controls.Add(chkArchive);
            groupBox3.Location = new Point(10, 448);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(490, 100);
            groupBox3.TabIndex = 35;
            groupBox3.TabStop = false;
            groupBox3.Text = "Controls";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(txtAck);
            groupBox4.Location = new Point(10, 12);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(490, 128);
            groupBox4.TabIndex = 36;
            groupBox4.TabStop = false;
            groupBox4.Text = "Received ACK";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(lstMessages);
            groupBox5.Location = new Point(514, 12);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(537, 430);
            groupBox5.TabIndex = 37;
            groupBox5.TabStop = false;
            groupBox5.Text = "Message Queue";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(label3);
            groupBox6.Controls.Add(txttotalQueue);
            groupBox6.Controls.Add(label4);
            groupBox6.Controls.Add(txtlastSequence);
            groupBox6.Location = new Point(514, 448);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(531, 100);
            groupBox6.TabIndex = 38;
            groupBox6.TabStop = false;
            groupBox6.Text = "Statistics";
            // 
            // HL7Sender
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1063, 556);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(lblProgress);
            Name = "HL7Sender";
            Text = "HL7 TCP Sender";
            FormClosing += HL7Sender_FormClosing;
            ((System.ComponentModel.ISupportInitialize)numDelayMs).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxRetries).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_queueMessages;
        private Button btn_sendSingle;
        private Button btn_sendAll;
        private Label label1;
        private TextBox txtAck;
        private Label label3;
        private TextBox txttotalQueue;
        private Label label4;
        private TextBox txtlastSequence;
        private Button btn_clearLogs;
        private Button btn_dirBrowse;
        private FolderBrowserDialog folderBrowser;
        private TextBox txtfolderPath;
        private Label lblmessagePath;
        private Button btn_clearPath;
        private ListBox lstMessages;
        private CheckBox chkArchive;
        private Button btnRequeueFailed;
        private ProgressBar progressBarSend;
        private Label lblProgress;
        private Label label7;
        private Label label8;
        private NumericUpDown numDelayMs;
        private NumericUpDown numMaxRetries;
        private TextBox txtSendingHost;
        private Label label9;
        private NumericUpDown numPort;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private RichTextBox txtLogs;
    }
}
