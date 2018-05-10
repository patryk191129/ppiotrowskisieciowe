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
    public partial class SendMessageWindow : Form
    {
        public MailClient mailClient { get; set; }


        public SendMessageWindow()
        {
            InitializeComponent();
            mailFromBox.Text = MailInformation.mailAddress + "@o2.pl";
        }


        private void SendMessageWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SMTPMessage smtpMessage = new SMTPMessage();

            smtpMessage.Password = MailInformation.password;
            smtpMessage.MailAddress = MailInformation.mailAddress;
            smtpMessage.Username = MailInformation.userName;
            smtpMessage.TargetAddress = mailToBox.Text;
            smtpMessage.Subject = subjectBox.Text;
            smtpMessage.Message = messageBox.Text;


            bool value = mailClient.SendMessage(smtpMessage);

            string message;

            if (value)
                message = "Wiadomość została pomyślnie wysłana";
            else
                message = "Nie udało się wysłać wiadomości";

            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, "INFORMACJA", buttons);


        }
    }
}
