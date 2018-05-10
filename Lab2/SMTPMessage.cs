using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2_3
{
    public struct SMTPMessage
    {
        public string Username { get; set; }
        public string MailAddress { get; set; }
        
        public string TargetAddress { get; set; }
        public string Password { get; set; }
        public string DestinationAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
