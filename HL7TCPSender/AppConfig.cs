using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7TCPSender
{
    public class AppConfig
    {
        public string SendingHost { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 4040;
        public string FolderPath { get; set; } = "";
        public int DelayMs { get; set; } = 100;
        public int MaxRetries { get; set; } = 3;
    }
}
