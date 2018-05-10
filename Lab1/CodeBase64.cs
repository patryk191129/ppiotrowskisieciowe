using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class CodeBase64
    {
        private string _inputFileDirectory;
        private string _inputFilename;
        private string _outputFilename;

        private List<Byte> _outputString;


        public CodeBase64(string inputFileDirectory, string outputFileName)
        {

            _inputFileDirectory = inputFileDirectory;
            _inputFilename = Path.GetFileName(inputFileDirectory);

            _outputFilename = outputFileName;

            _outputString = new List<Byte>();



        }
        public CodeBase64()
        {

        }

        public void EncodeBase64()
        {
            LoadFileToEncode();
            SaveToFile();
        }

        public void DecodeBase64()
        {
            LoadFileToDecode();
            SaveToFile();

        }


        public string EncodeBase64(string value)
        {

            _outputString = new List<Byte>();
            byte[] bytes = Encoding.ASCII.GetBytes(value);


            int i = 0;

            for (i = 0; i < bytes.Length - 3; i = i + 3)
            {
                List<byte> tmpBytes = new List<byte>();

                for (int j = 0; j < 3; j++)
                    tmpBytes.Add(bytes[i + j]);

                CodeString(tmpBytes);
            }

            List<byte> lastBytes = new List<byte>();
            for (int j = i; j < value.Length; j++)
            {
                lastBytes.Add(bytes[j]);
            }
            CodeString(lastBytes);


            return System.Text.Encoding.UTF8.GetString(_outputString.ToArray());
        }



        private void LoadFileToEncode()
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(_inputFileDirectory);
                int i = 0;

                for (i = 0; i < fileBytes.Length - 3; i = i + 3)
                {
                    List<byte> tmpBytes = new List<byte>();

                    for (int j = 0; j < 3; j++)
                        tmpBytes.Add(fileBytes[i + j]);

                    CodeString(tmpBytes);
                }

                List<byte> lastBytes = new List<byte>();
                for (int j = i; j < fileBytes.Length; j++)
                {
                    lastBytes.Add(fileBytes[j]);
                }
                    CodeString(lastBytes);
  

            }
            catch (IOException e)
            {
                Console.WriteLine("Unable to load file %s", e);
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


        private void CodeString(List<byte> input)
        {
            bool[] mask = new bool[2];

            if (input.Count == 0)
                return;
            
            if (input.Count <= 2)
            {
                if (input.Count <= 1)
                {
                    input.Add(0);
                    mask[0] = true;
                }
                input.Add(0);
                mask[1] = true;
            }
            


            int groupValues = (input[0] << 16) | (input[1] << 8) | (input[2]);
            byte[] enc = new byte[4];

            enc[0] = Convert.ToByte((groupValues >> 18) & 63);
            enc[1] = Convert.ToByte((groupValues >> 12) & 63);
            enc[2] = Convert.ToByte((groupValues >> 6) & 63);
            enc[3] = Convert.ToByte((groupValues >> 0) & 63);


            for (int i = 0; i < enc.Length; i++)
            {
                //Duze znaki            
                if (enc[i] <= 25)
                {
                    enc[i] += 65;
                    continue;
                }

                //Male znaki
                if (enc[i] <= 51)
                {
                    enc[i] += 71;
                    continue;
                }

                //0-9
                if (enc[i] <= 61)
                {
                    enc[i] -= 4;
                    continue;
                }

                //Znak +
                if (enc[i] == 62)
                {
                    enc[i] = 43;
                    continue;
                }

                //Znak /
                if (enc[i] == 63)
                {
                    enc[i] = 47;
                    continue;
                }
            }

            if (mask[0])
                enc[2] = 61;
            if (mask[1])
                enc[3] = 61;

            for (int i = 0; i < enc.Length; i++)
            {
                _outputString.Add(enc[i]);
            }
        }

        private void DecodeString(string input)
        {
            byte[] values = new byte[input.Length];

            for(int i=0;i<input.Length;i++)
            {
                values[i] = Convert.ToByte(input[i]);

                if (values[i] == 61)
                    values[i] = 255;

            }


            for(int i=0;i<input.Length;i++)
            {

                //Duże znaki
                if(values[i] >= 65 && values[i] <= 90)
                {
                    values[i] -= 65;
                    continue;
                }
                
                //Małe znaki
                if(values[i] >= 97 && values[i] <= 122)
                {
                    values[i] -= 71;
                    continue;
                }


                //0-9
                if(values[i] >= 48 && values[i] <= 57)
                {
                    values[i] += 4;
                    continue;
                }
                //+
                if (values[i] == 43)
                {
                    values[i] = 62;
                    continue;
                }

                // dla slasha
                if (values[i] == 47)
                {
                    values[i] = 63;
                    continue;
                }
            }


            int groupValues = (values[0] << 18) | (values[1] << 12) | (values[2] << 6) | values[3];

            byte[] enc = new byte[3];

            enc[0] = Convert.ToByte((groupValues >> 16) & 255);
            enc[1] = Convert.ToByte((groupValues >> 8) & 255);
            enc[2] = Convert.ToByte(groupValues & 255);

            for(int i=0;i<enc.Length;i++)
            {
                if (input[i + 1] != '=')
                    _outputString.Add(enc[i]);
                else
                    return;
            }

        }


        private void SaveToFile()
        {
            string textString = string.Join("", _outputString.ToArray());

            File.ReadAllBytes(_inputFileDirectory);
            File.WriteAllBytes(Directory.GetParent(_inputFileDirectory) + "//" + _outputFilename, _outputString.ToArray());


        }

    }
}
