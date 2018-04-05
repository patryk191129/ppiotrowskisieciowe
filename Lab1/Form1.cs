using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog _openFileDialog = new OpenFileDialog();

            if (_openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string outputFileName = Path.GetFileNameWithoutExtension(_openFileDialog.FileName) + ".b64";
                CodeBase64 codeBase64 = new CodeBase64(_openFileDialog.FileName, outputFileName);
                codeBase64.EncodeBase64();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog();

            if (_openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string outputFileName = "out_" + Path.GetFileNameWithoutExtension(_openFileDialog.FileName) + "." + ExtensionValue.Text;

                CodeBase64 codeBase64 = new CodeBase64(_openFileDialog.FileName, outputFileName);
                codeBase64.DecodeBase64();
            }
        }
    }
}
