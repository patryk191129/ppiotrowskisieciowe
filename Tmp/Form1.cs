using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                CodeBase64 codeBase64 = new CodeBase64(_openFileDialog.FileName, null);
                codeBase64.EncodeBase64();
            }

        }
    }
}
