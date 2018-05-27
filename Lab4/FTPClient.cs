using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lab4
{
    class FTPClient
    {

        string _login;
        string _password;
        string _hosting;
        int _port;

        string _passiveServerAddress;
        int _passiveServerPort;


        Socket _mainSocket;
        Socket _passiveSocket;

        public FTPClient()
        {
            _login = "patrykpiotrowski19.ugu.pl";
            _password = "TajneHaslo123";
            _hosting = "patrykpiotrowski19.ugu.pl";
            _port = 21;

            EstablishConnection();

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

                canRead = socket.Poll(500000, SelectMode.SelectRead);

                if (canRead)
                {
                    try
                    {
                        read = socket.Receive(currentBytes);
                        currentBytesString += new string(Encoding.UTF8.GetChars(currentBytes)).Replace("\0", string.Empty);
                    }
                    catch
                    {
                        Console.WriteLine("Error loading socket information");
                    }


                }

            } while (read > 0 && canRead);

            return currentBytesString;

        }




        private string GetPassiveSocketInformation()
        {
 
            _passiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _passiveSocket.Connect(_passiveServerAddress, _passiveServerPort);
            _passiveSocket.ReceiveTimeout = 2000;

            string outputString = GetCurrentSocketInfo(_passiveSocket);

            _passiveSocket.Close();

            return outputString;

        }


        private void EstablishConnection()
        {
            if (_mainSocket != null)
            {
                _mainSocket.Close();
            }

            string outputString;

            Console.WriteLine("Attempting to connect to FTP server...");

            _mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _mainSocket.Connect(_hosting, _port);
            _mainSocket.ReceiveTimeout = 5000;

            _mainSocket.Send(Encoding.UTF8.GetBytes("user " + _login + "\r\n"));
            outputString = GetCurrentSocketInfo(_mainSocket);


            _mainSocket.Send(Encoding.UTF8.GetBytes("pass " + _password + "\r\n"));
            outputString = GetCurrentSocketInfo(_mainSocket);

            if (outputString.Contains("230"))
            {
                Console.WriteLine("Successfully connected to FTP server");
                GenerateNewPassiveSocket();
                Console.WriteLine("Connected successfully to FTP Server. Press any key to continue...");
                Console.ReadKey();
                FTPMenu();

            }

            else
            {
                Console.WriteLine("Invalid username/password");
                return;
            }     

        }



        private void GenerateNewPassiveSocket()
        {

            _mainSocket.Send(Encoding.UTF8.GetBytes("pasv\r\n"));

            string outputString = GetCurrentSocketInfo(_mainSocket);

            String[] numbers = Regex.Split(outputString, @"\D+");
            int idx = 0;

            for (int i = 0; i < numbers.Length; i++)
                if (numbers[i] == "227")
                {
                    idx = i;
                    break;
                }

            _passiveServerAddress = numbers[idx + 1] + "." + numbers[idx + 2] + "." + numbers[idx + 3] + "." + numbers[idx + 4];
            _passiveServerPort = Int32.Parse(numbers[idx + 5]) * 256 + Int32.Parse(numbers[idx + 6]);


        }


        private void FTPMenu()
        {
            int option = 0;

            do
            {

                Console.Clear();
                GenerateNewPassiveSocket();
                ShowCurrentDirectory();
                ShowMenu();

                try
                {
                    option = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid option");
                }

                switch(option)
                {
                    case 1: GotoDirectory(); break;
                    case 2: GotoParentDirectory(); break;
                    case 3: ShowDirectoryTree(); break;
                    default: Console.WriteLine("Invalid option"); break;
                }



            } while (option != 4);


            _mainSocket.Close();

            if (_passiveSocket != null)
                _passiveSocket.Close();



        }



        private List<string> GetDirectoryList(string output)
        {
            List<string> directoryList = new List<string>();

            string[] currentDirElements = output.Split("\r\n");


            foreach (string currentDir in currentDirElements)
            {

                string[] tmp = currentDir.Split(" ");

                if (tmp[0].Contains("d") && !directoryList.Contains(tmp[tmp.Length - 1]))
                    directoryList.Add(tmp[tmp.Length - 1]);
            }

            return directoryList;
        }

        private List<string> GetFileList(string output)
        {
            List<string> fileList = new List<string>();

            string[] currentDirElements = output.Split("\r\n");


            foreach (string currentDir in currentDirElements)
            {

                string[] tmp = currentDir.Split(" ");

                if (!tmp[0].Contains("d") && !fileList.Contains(tmp[tmp.Length - 1]))
                    fileList.Add(tmp[tmp.Length - 1]);
            }


            return fileList;
        }



        private void ShowCurrentDirectory()
        {

            _mainSocket.Send(Encoding.UTF8.GetBytes("pwd\r\n"));
            string dirname = GetCurrentSocketInfo(_mainSocket);


            _mainSocket.Send(Encoding.UTF8.GetBytes("list\r\n"));
            string output = GetPassiveSocketInformation();

            List<string> directoryList = GetDirectoryList(output);
            List<string> fileList = GetFileList(output);


            Console.WriteLine("[Directory] "+dirname.Substring(4, dirname.Length-4));

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n[Directories]");
            Console.ResetColor();
            foreach (string directoryName in directoryList)
                Console.WriteLine(directoryName);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n\n[Files]");
            Console.ResetColor();
            foreach (string fileName in fileList)
                Console.WriteLine(fileName);

            Console.WriteLine();
        }


        private void ShowMenu()
        {

            Console.Write("\n\n[Menu]\n[1] Go to directory\n[2] Go to parent directory\n[3] Show directory tree\n[4] Close application\n\nChoose option: ");
        }


        private void GotoParentDirectory()
        {
            _mainSocket.Send(Encoding.UTF8.GetBytes("cdup\r\n"));
        }

        private void GotoDirectory()
        {
            Console.Write("Type directory: ");
            string dirname = Console.ReadLine();

            _mainSocket.Send(Encoding.UTF8.GetBytes("cwd "+dirname+"\r\n"));
            if(!GetCurrentSocketInfo(_mainSocket).Contains("250"))
            {
                Console.WriteLine("Invalid directory. Press any key to continue...");
                Console.ReadKey();

            }

        }


        private void ShowDirectoryTree()
        {
            GenerateNewPassiveSocket();
            _mainSocket.Send(Encoding.UTF8.GetBytes("cwd /\r\n"));
            GetCurrentSocketInfo(_mainSocket);

            _mainSocket.Send(Encoding.UTF8.GetBytes("list\r\n"));
            string output = GetPassiveSocketInformation();


            DisplayDirectory(GetDirectoryList(output), GetFileList(output), 0);

            Console.ReadKey();

        }


        private void DisplayDirectory(List<string> directoryList, List<string> fileList, int node)
        {

            foreach(string currentFile in fileList)
            {

                for (int i = 0; i < node; i++)
                    Console.Write("-");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(currentFile);
                Console.ResetColor();

            }




            foreach(string currentDir in directoryList)
            {
                for (int i = 0; i < node; i++)
                    Console.Write("-");
                Console.WriteLine(currentDir);

                GenerateNewPassiveSocket();
                _mainSocket.Send(Encoding.UTF8.GetBytes("cwd " + currentDir + "\r\n"));
                Thread.Sleep(500);
                _mainSocket.Send(Encoding.UTF8.GetBytes("list\r\n"));
                string output = GetPassiveSocketInformation();

                DisplayDirectory(GetDirectoryList(output), GetFileList(output), node + 1);
            }


        }



    }
}
