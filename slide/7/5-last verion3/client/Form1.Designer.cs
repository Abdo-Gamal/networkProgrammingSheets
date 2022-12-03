namespace client
{
    partial class Form1
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
            this.textMessage = new System.Windows.Forms.TextBox();
            this.MessageList = new System.Windows.Forms.ListBox();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.avlib_server = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.connect = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(224, 225);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textMessage
            // 
            this.textMessage.Location = new System.Drawing.Point(12, 227);
            this.textMessage.Name = "textMessage";
            this.textMessage.Size = new System.Drawing.Size(206, 20);
            this.textMessage.TabIndex = 12;
            // 
            // MessageList
            // 
            this.MessageList.FormattingEnabled = true;
            this.MessageList.Location = new System.Drawing.Point(12, 41);
            this.MessageList.Name = "MessageList";
            this.MessageList.Size = new System.Drawing.Size(287, 173);
            this.MessageList.TabIndex = 11;
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(12, 12);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(153, 20);
            this.txtIpAddress.TabIndex = 9;
            // 
            // avlib_server
            // 
            this.avlib_server.Location = new System.Drawing.Point(305, 12);
            this.avlib_server.Name = "avlib_server";
            this.avlib_server.Size = new System.Drawing.Size(131, 23);
            this.avlib_server.TabIndex = 8;
            this.avlib_server.Text = "avlib_server";
            this.avlib_server.UseVisualStyleBackColor = true;
            this.avlib_server.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(171, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(128, 20);
            this.txtPort.TabIndex = 10;
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(442, 12);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(122, 23);
            this.connect.TabIndex = 15;
            this.connect.Text = "connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(305, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(247, 173);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(325, 227);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(227, 21);
            this.button3.TabIndex = 17;
            this.button3.Text = "choose and send an image";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(614, 25);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(151, 225);
            this.listBox1.TabIndex = 18;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(621, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = " client connect to server";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 275);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textMessage);
            this.Controls.Add(this.MessageList);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.avlib_server);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textMessage;
        private System.Windows.Forms.ListBox MessageList;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Button avlib_server;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
    }
}

