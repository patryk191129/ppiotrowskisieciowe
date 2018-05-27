using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            Console.WriteLine("FTP CLIENT - Author Patryk Piotrowski");
            Console.Write("Insert your login: ");
            _login = Console.ReadLine();
            Console.Write("Insert your password: ");
            _password = Console.ReadLine();
            Console.Write("Insert ftp address: ");
            _hosting = Console.ReadLine();
            Console.Write("Insert FTP server port: ");
            _port = Int32.Parse(Console.ReadLine());

            

            //_login = "unaux_22147793";
            //_password = "tajnehaslo";
            //_hosting = "ftp.unaux.com";
            //_port = 21;

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
            try
            {
                _passiveSocket.Connect(_passiveServerAddress, _passiveServerPort);
                _passiveSocket.ReceiveTimeout = 2000;
            }
            catch
            {
                GenerateNewPassiveSocket();
                _passiveSocket.Connect(_passiveServerAddress, _passiveServerPort);
                _passiveSocket.ReceiveTimeout = 2000;
            }



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

            try
            {
                _passiveServerAddress = numbers[idx + 1] + "." + numbers[idx + 2] + "." + numbers[idx + 3] + "." + numbers[idx + 4];
                _passiveServerPort = Int32.Parse(numbers[idx + 5]) * 256 + Int32.Parse(numbers[idx + 6]);

            }
            catch
            {
                Console.WriteLine("Error while creating new passive server port.");
            }



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

                if (tmp[0].Contains("d") && !tmp[tmp.Length -1].Contains(".") && !directoryList.Contains(tmp[tmp.Length - 1]))
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

                if (!tmp[0].Contains("d") && tmp[tmp.Length-1] != "" && !fileList.Contains(tmp[tmp.Length - 1]))
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


            DisplayDirectory("/", 0);


            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

        }


        private void DisplayDirectory(string dirName, int node)
        {
            GenerateNewPassiveSocket();
            GetCurrentSocketInfo(_mainSocket);
            _mainSocket.Send(Encoding.UTF8.GetBytes("cwd " + dirName + "\r\n"));
            Thread.Sleep(400);
            _mainSocket.Send(Encoding.UTF8.GetBytes("list\r\n"));

            string output = GetPassiveSocketInformation();
            Thread.Sleep(400);

            List<string> fileList = GetFileList(output);
            List<string> directoryList = GetDirectoryList(output);

            for (int i = 0; i < node-1; i++)
                Console.Write("|\t");



            Console.WriteLine("|---" + Path.GetFileName(dirName));

            foreach(string currentFile in fileList)
            {
                for(int i=0;i<node;i++)
                    Console.Write("|\t");


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" " + currentFile);
                Console.ResetColor();
            }


            foreach(string currentDirectory in directoryList)
                DisplayDirectory(dirName + "/" + currentDirectory, node + 1);

           

        }



    }
}
