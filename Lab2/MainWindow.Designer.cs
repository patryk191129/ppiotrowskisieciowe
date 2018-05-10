namespace Lab2_3
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mailAddress = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.applicationLog = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.receivedMsg = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.outputLog = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SendMessageButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Connect to mail account";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SendMessageButton);
            this.groupBox1.Controls.Add(this.mailAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.applicationLog);
            this.groupBox1.Location = new System.Drawing.Point(8, 49);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(449, 439);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application Log";
            // 
            // mailAddress
            // 
            this.mailAddress.AutoSize = true;
            this.mailAddress.Location = new System.Drawing.Point(77, 28);
            this.mailAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mailAddress.Name = "mailAddress";
            this.mailAddress.Size = new System.Drawing.Size(44, 13);
            this.mailAddress.TabIndex = 6;
            this.mailAddress.Text = "address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mail address:";
            // 
            // applicationLog
            // 
            this.applicationLog.Location = new System.Drawing.Point(4, 84);
            this.applicationLog.Margin = new System.Windows.Forms.Padding(2);
            this.applicationLog.Name = "applicationLog";
            this.applicationLog.Size = new System.Drawing.Size(443, 351);
            this.applicationLog.TabIndex = 0;
            this.applicationLog.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(175, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Received messages:";
            // 
            // receivedMsg
            // 
            this.receivedMsg.AutoSize = true;
            this.receivedMsg.Location = new System.Drawing.Point(285, 20);
            this.receivedMsg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.receivedMsg.Name = "receivedMsg";
            this.receivedMsg.Size = new System.Drawing.Size(13, 13);
            this.receivedMsg.TabIndex = 3;
            this.receivedMsg.Text = "0";
            this.receivedMsg.Click += new System.EventHandler(this.receivedMsg_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.outputLog);
            this.groupBox2.Location = new System.Drawing.Point(473, 49);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(449, 439);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OutputLog";
            // 
            // outputLog
            // 
            this.outputLog.Location = new System.Drawing.Point(4, 16);
            this.outputLog.Margin = new System.Windows.Forms.Padding(2);
            this.outputLog.Name = "outputLog";
            this.outputLog.Size = new System.Drawing.Size(443, 420);
            this.outputLog.TabIndex = 0;
            this.outputLog.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(767, 8);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(152, 37);
            this.button2.TabIndex = 4;
            this.button2.Text = "Stop client";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Enabled = false;
            this.SendMessageButton.Location = new System.Drawing.Point(280, 28);
            this.SendMessageButton.Margin = new System.Windows.Forms.Padding(2);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(152, 37);
            this.SendMessageButton.TabIndex = 5;
            this.SendMessageButton.Text = "Send message";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 496);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.receivedMsg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox applicationLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label receivedMsg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox outputLog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label mailAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SendMessageButton;
    }
}

