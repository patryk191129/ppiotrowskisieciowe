using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Lab1;

namespace Lab2_3
{
    public class MailClient
    {
        Thread _pop3Thread;

        string _serverAddress;
        int _serverPort;

        string _login;
        string _password;

        float _updateTime;

        Socket _socket;
        MainWindow _form;

        public bool started;


        public struct MailInfo
        {
            public int mailID;
            public string title;
        }

        List<MailInfo> _mailInfo;
        
        

        public MailClient(MainWindow form)
        {
            _mailInfo = new List<MailInfo>();

            started = true;
            _form = form;
            _serverAddress = MailInformation.mailAddress;
            _serverPort = MailInformation.pop3Port;
            _login = MailInformation.userName;
            _password = MailInformation.password;
            _updateTime = MailInformation.updateTime;


            if(_pop3Thread == null)
            {
                _pop3Thread = new Thread(this.POP3Thread);
                _pop3Thread.Start();
            }

        }


    
        private void EstablishConnection()
        {

            if(_socket != null)
            {
                _socket.Close();
            }

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(_serverAddress, _serverPort);
            _socket.ReceiveTimeout = 5000;

            _socket.Send(Encoding.UTF8.GetBytes("user " + _login + "\r\n"));
            _socket.Send(Encoding.UTF8.GetBytes("pass " + _password + "\r\n"));

            string outputString = GetCurrentSocketInfo(_socket);

        }



        public bool SendMessage(SMTPMessage smtpMessage)
        {
            CodeBase64 codeBase64 = new CodeBase64();

            string b64Login = codeBase64.EncodeBase64(smtpMessage.Username);
            string b64Password = codeBase64.EncodeBase64(smtpMessage.Password);


            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(_serverAddress, 587);
            socket.ReceiveTimeout = 1000;

            string outputString;


            socket.Send(Encoding.UTF8.GetBytes("helo\r\n"));
            socket.Send(Encoding.UTF8.GetBytes("auth login\r\n"));
            outputString = GetCurrentSocketInfo(socket);
            socket.Send(Encoding.UTF8.GetBytes(b64Login+"\r\n"));
            outputString = GetCurrentSocketInfo(socket);
            socket.Send(Encoding.UTF8.GetBytes(b64Password+"\r\n"));
            outputString = GetCurrentSocketInfo(socket);

            socket.Send(Encoding.UTF8.GetBytes("mail from:<"+smtpMessage.Username+"@o2.pl>\n"));
            outputString = GetCurrentSocketInfo(socket);




            if (outputString.Contains("250") || outputString.Contains("235"))
            {
                socket.Send(Encoding.UTF8.GetBytes("rcpt to:<" + smtpMessage.TargetAddress + ">\n"));
                outputString = GetCurrentSocketInfo(socket);
                socket.Send(Encoding.UTF8.GetBytes("DATA\r\n"));
                outputString = GetCurrentSocketInfo(socket);
                socket.Send(Encoding.UTF8.GetBytes("From:<" + smtpMessage.Username + ">\n" + "To:<" + smtpMessage.TargetAddress + ">\nSubject:" + smtpMessage.Subject + "\nContent:<" + smtpMessage.Message + "\r\n.\n"));


                outputString = GetCurrentSocketInfo(socket);

                if (outputString.Contains("250"))
                    return true;


            }
            return false;

        }


        private void AddNewItem(int id)
        {
            _socket.Send(Encoding.UTF8.GetBytes("retr "+id+"\r\n"));
            string outputString = GetCurrentSocketInfo(_socket);


            string[] words = outputString.Split('\n');

            int i = 0;

            do
                i++;
            while (!words[i].Contains("Subject"));

            string[] values = words[i].Split(':');

            MailInfo mailInfo = new MailInfo();
            mailInfo.mailID = id;
            mailInfo.title = values[1];

            _mailInfo.Add(mailInfo);

            _form.UpdateLog("You received a new message: '"+ mailInfo.title + "'");
            _form.AddMessageToCounter();


        }


        private string GetCurrentSocketInfo(Socket socket)
        {
            StringBuilder builder = new StringBuilder();


            int read = 0;


            string currentBytesString = "";

            byte[] currentBytes = new byte[socket.ReceiveBufferSize];
            var canRead = true;

            do
            {

                canRead = socket.Poll(3000000, SelectMode.SelectRead);
                
                if (canRead)
                {
                    try
                    {
                        read = socket.Receive(currentBytes);
                        currentBytesString += new string(Encoding.UTF8.GetChars(currentBytes)).Replace("\0", string.Empty);
                    }
                    catch
                    {
                        _form.UpdateLog("Error: lost connection with mail server. Attempting to reconnect...");
                    }
 

                }

            } while (read>0 && canRead);

            return currentBytesString;
         
        }


        public void GetMailList()
        {
            EstablishConnection();
            _socket.Send(Encoding.UTF8.GetBytes("uidl\r\n"));


            string outputBuff = GetCurrentSocketInfo(_socket);
            _form.UpdateOutputLog(outputBuff);

            string[] words = outputBuff.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
                return;

            if(words[0] == "+OK")
            {

                for(int i=1;i<words.Length;i=i+2)
                {
                    try
                    {

                        if (words[i][0] == 0 || words[i][0] == '.')
                            return;

                        int mailID = Convert.ToInt32(words[i]);

                        int idx = _mailInfo.FindIndex(x => x.mailID == mailID);


                        if(idx == -1)
                        {
                            AddNewItem(mailID);                           
                        }
                    }
                    catch
                    {
                        _form.UpdateLog("Error: invalid format");
                    }

                }
            }
            else
            {
                _form.UpdateLog("Error loading mails");
            }


        }



        private void POP3Thread()
        {

            _form.UpdateLog("Client started");

            while(started)
            {
                GetMailList();
                Thread.Sleep((int)_updateTime * 1000);
            }
        }


        public void StopApp()
        {
            if(_pop3Thread != null)
            {
            
                _pop3Thread.Abort();

                if (_socket != null)
                    _socket.Close();
            }


        }



    }
}
