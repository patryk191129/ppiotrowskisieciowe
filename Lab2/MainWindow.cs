using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Lab2_3
{
    public partial class MainWindow : Form
    {

        MailClient mailClient { get; set; }
        SendMessageWindow window;

        public MainWindow()
        {
            InitializeComponent();
            mailAddress.Text = MailInformation.userName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mailClient = new MailClient(this);
            SendMessageButton.Enabled = true;
            applicationLog.Text = "";
            outputLog.Text = "";
        }


        public void UpdateLog(string logInfo)
        {
            MethodInvoker inv = delegate
            {
                applicationLog.Text += "\n" + logInfo;
            };

            this.Invoke(inv);
        }

        public void UpdateOutputLog(string logInfo)
        {
            MethodInvoker inv = delegate
            {
                outputLog.Text = logInfo;
            };

            this.Invoke(inv);
        }

        public void AddMessageToCounter()
        {
            MethodInvoker inv = delegate
            {
                receivedMsg.Text = (Convert.ToInt32(receivedMsg.Text) + 1).ToString();
            };

            this.Invoke(inv);

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void receivedMsg_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (mailClient != null)
                mailClient.StopApp();


            SendMessageButton.Enabled = false;
            receivedMsg.Text = 0.ToString();


        }

        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            if (window != null)
                window.Hide();



            window = new SendMessageWindow();
            window.mailClient = mailClient;
            window.ShowDialog();
 
            
        }
    }
}
