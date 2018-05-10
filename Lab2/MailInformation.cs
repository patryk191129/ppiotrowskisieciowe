using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace Lab2_3
{
    static class MailInformation
    {

        public static string userName { get; }
        public static string password { get; }
        public static string mailAddress { get; }
        public static int pop3Port { get; }
        public static float updateTime { get; }



        static MailInformation()
        {
            mailAddress = ConfigurationSettings.AppSettings["ServerAddress"];
            pop3Port = Int32.Parse(ConfigurationSettings.AppSettings["ServerPort"]);
            userName = ConfigurationSettings.AppSettings["Login"];
            password = ConfigurationSettings.AppSettings["Password"];
            updateTime = float.Parse(ConfigurationSettings.AppSettings["UpdateTime"]);

        }


    }
}
