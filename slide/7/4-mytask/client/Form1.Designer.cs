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
            this.avalibleClient = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textMessage = new System.Windows.Forms.TextBox();
            this.MessageList = new System.Windows.Forms.ListBox();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.avlib_server = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.connect = new System.Windows.Forms.Button();
            this.showClient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // avalibleClient
            // 
            this.avalibleClient.FormattingEnabled = true;
            this.avalibleClient.Location = new System.Drawing.Point(199, 41);
            this.avalibleClient.Name = "avalibleClient";
            this.avalibleClient.Size = new System.Drawing.Size(181, 173);
            this.avalibleClient.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(118, 227);
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
            this.textMessage.Size = new System.Drawing.Size(100, 20);
            this.textMessage.TabIndex = 12;
            // 
            // MessageList
            // 
            this.MessageList.FormattingEnabled = true;
            this.MessageList.Location = new System.Drawing.Point(12, 41);
            this.MessageList.Name = "MessageList";
            this.MessageList.Size = new System.Drawing.Size(181, 173);
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
            this.avlib_server.Size = new System.Drawing.Size(75, 23);
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
            this.connect.Location = new System.Drawing.Point(396, 12);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(75, 23);
            this.connect.TabIndex = 15;
            this.connect.Text = "connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // showClient
            // 
            this.showClient.Location = new System.Drawing.Point(477, 12);
            this.showClient.Name = "showClient";
            this.showClient.Size = new System.Drawing.Size(75, 23);
            this.showClient.TabIndex = 16;
            this.showClient.Text = "showClient";
            this.showClient.UseVisualStyleBackColor = true;
            this.showClient.Click += new System.EventHandler(this.showClient_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 275);
            this.Controls.Add(this.showClient);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.avalibleClient);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textMessage);
            this.Controls.Add(this.MessageList);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.avlib_server);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox avalibleClient;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textMessage;
        private System.Windows.Forms.ListBox MessageList;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Button avlib_server;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.Button showClient;
    }
}

