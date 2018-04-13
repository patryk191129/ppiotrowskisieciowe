using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class CodeBase64
    {
        private string _inputFileDirectory;
        private string _inputFilename;

        private string _outputFileDirectory;
        private string _outputFilename;

        private List<String> _outputString;


        public CodeBase64(string inputFileDirectory, string outputFileDirectory)
        {
            
            _inputFileDirectory = inputFileDirectory;
            _inputFilename = Path.GetFileName(inputFileDirectory);

            _outputFileDirectory = outputFileDirectory;
            _outputFilename = Path.GetFileName(outputFileDirectory);

            _outputString = new List<string>();
            
 

        }

        public void EncodeBase64()
        {
            LoadFileToEncode();
            SaveToFile();
        }

        public void DecodeBase64()
        {

        }


        private void LoadFileToEncode()
        {
            try
            {
                StreamReader sr = new StreamReader(_inputFileDirectory);
                string currentBuffer = sr.ReadToEnd();
                sr.Close();


                int i = 0;

                for (i = 0; i < currentBuffer.Length - 3; i = i + 3)
                    CodeString(currentBuffer.Substring(i, 3));

                CodeString(currentBuffer.Substring(i, currentBuffer.Length - i));


            }
            catch(IOException e)
            {
                Console.WriteLine("Unable to load file %s",e);
            }

        }
        
        private void LoadFileToDecode()
        {
            try
            {
                StreamReader sr = new StreamReader(_inputFileDirectory);
                string currentBuffer = sr.ReadToEnd();
                sr.Close();

                for (int i = 0; i < currentBuffer.Length; i = i + 4)
                    DecodeString(currentBuffer.Substring(i, 4));

            }
            catch (IOException e)
            {
                Console.WriteLine("Unable to load file %s", e);
            }

        }


        private void CodeString(string input)
        {
            bool[] mask = new bool[2];


            if(input.Length <= 2)
            {
                if (input.Length <= 1)
                {
                    input += '\0';
                    mask[0] = true;
                }    
                input += '\0';
                mask[1] = true;
            }



            int groupValues = (input[0] << 16) | (input[1] << 8) | (input[2]);
            byte[] enc = new byte[4];

            enc[0] = Convert.ToByte((groupValues >> 18) & 63);
            enc[1] = Convert.ToByte((groupValues >> 12) & 63);
            enc[2] = Convert.ToByte((groupValues >> 6) & 63);
            enc[3] = Convert.ToByte((groupValues >> 0) & 63);
            

            for(int i=0;i<enc.Length;i++)
            {
                //Duze znaki            
                if(enc[i] <= 25)
                {
                    enc[i] += 65;
                    continue;
                }
               
                //Male znaki
                if(enc[i] <= 51)
                {
                    enc[i] += 71;
                    continue;
                }

                //0-9
                if(enc[i] <= 61)
                {
                    enc[i] -= 4;
                    continue;
                }

                //Znak +
                if(enc[i] == 62)
                {
                    enc[i] = 43;
                    continue;
                }

                //Znak /
                if(enc[i] == 63)
                {
                    enc[i] = 47;
                    continue;
                }          
            }

            if (mask[0])
                enc[2] = 61;
            if (mask[1])
                enc[3] = 61;

            for(int i=0;i< enc.Length;i++)
            {
                _outputString.Add(Convert.ToString((char) enc[i]));
            }
        }

        private void DecodeString(string input)
        {

        }


        private void SaveToFile()
        {
            string textString = string.Join("",_outputString.ToArray());


            using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(Directory.GetParent(_inputFileDirectory) + "//kk.txt"))
            {
                outputFile.Write(textString);
                outputFile.Close();
            }

           

        }

    }
}
