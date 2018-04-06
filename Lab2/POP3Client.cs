using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Sockets;

namespace Lab2
{
    class POP3Client
    {

        //Informacje o serwerze
        string _serverAddress;
        int _serverPort;

        //Informacje do logowania
        string _login;
        string _password;

        //Interwał aktualizacji poczty w sekundach
        float _updateTime;

        Socket _socket;


        private string SendMessage(string message)
        {
            byte []buff = new byte[100];

            _socket.Send(Encoding.UTF8.GetBytes(message));

            _socket.Receive(buff);

            return Encoding.ASCII.GetString(buff);
        }


        private string ReceiveMessage()
        {
            byte[] buff = new byte[1000];

            _socket.Receive(buff);

            return Encoding.ASCII.GetString(buff);
        }

        public POP3Client()
        {
            string buffer;

            _serverAddress = ConfigurationSettings.AppSettings["serverAddress"];
            _serverPort = Int32.Parse(ConfigurationSettings.AppSettings["port"]);

            _login = ConfigurationSettings.AppSettings["username"];
            _password = ConfigurationSettings.AppSettings["password"];

            _updateTime = float.Parse(ConfigurationSettings.AppSettings["updateTime"]);


            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _socket.Connect(_serverAddress, _serverPort);
            Console.WriteLine(SendMessage("user " + _login + "\r\n"));
            Console.WriteLine(SendMessage("pass " + _password + "\r\n"));
            Console.WriteLine(SendMessage("uidl\n"));




        }




    }
}
