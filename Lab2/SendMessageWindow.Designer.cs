namespace Lab2_3
{
    partial class SendMessageWindow
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
            this.mailToBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mailFromBox = new System.Windows.Forms.TextBox();
            this.subjectBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.messageBox = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mailToBox
            // 
            this.mailToBox.Location = new System.Drawing.Point(151, 80);
            this.mailToBox.Name = "mailToBox";
            this.mailToBox.Size = new System.Drawing.Size(168, 20);
            this.mailToBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mail to:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mail from:";
            // 
            // mailFromBox
            // 
            this.mailFromBox.Location = new System.Drawing.Point(151, 52);
            this.mailFromBox.Name = "mailFromBox";
            this.mailFromBox.ReadOnly = true;
            this.mailFromBox.Size = new System.Drawing.Size(168, 20);
            this.mailFromBox.TabIndex = 3;
            // 
            // subjectBox
            // 
            this.subjectBox.Location = new System.Drawing.Point(151, 119);
            this.subjectBox.Name = "subjectBox";
            this.subjectBox.Size = new System.Drawing.Size(562, 20);
            this.subjectBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(99, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Subject:";
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(151, 159);
            this.messageBox.Margin = new System.Windows.Forms.Padding(2);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(562, 335);
            this.messageBox.TabIndex = 6;
            this.messageBox.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Message:";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(151, 512);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(110, 31);
            this.sendMessageButton.TabIndex = 8;
            this.sendMessageButton.Text = "Send message";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // SendMessageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 575);
            this.Controls.Add(this.sendMessageButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.subjectBox);
            this.Controls.Add(this.mailFromBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mailToBox);
            this.Name = "SendMessageWindow";
            this.Text = "Send Message";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mailToBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mailFromBox;
        private System.Windows.Forms.TextBox subjectBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox messageBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button sendMessageButton;
    }
}