namespace Client
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
            this.btn_Connect = new System.Windows.Forms.Button();
            this.txb_Port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_Host = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txb_Message = new System.Windows.Forms.TextBox();
            this.btn_Send = new System.Windows.Forms.Button();
            this.txb_Status = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(315, 30);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_Connect.TabIndex = 7;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // txb_Port
            // 
            this.txb_Port.Location = new System.Drawing.Point(203, 30);
            this.txb_Port.Name = "txb_Port";
            this.txb_Port.Size = new System.Drawing.Size(100, 22);
            this.txb_Port.TabIndex = 5;
            this.txb_Port.Text = "8910";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port:";
            // 
            // txb_Host
            // 
            this.txb_Host.Location = new System.Drawing.Point(61, 30);
            this.txb_Host.Name = "txb_Host";
            this.txb_Host.Size = new System.Drawing.Size(100, 22);
            this.txb_Host.TabIndex = 6;
            this.txb_Host.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Host:";
            // 
            // txb_Message
            // 
            this.txb_Message.Location = new System.Drawing.Point(61, 58);
            this.txb_Message.Multiline = true;
            this.txb_Message.Name = "txb_Message";
            this.txb_Message.Size = new System.Drawing.Size(329, 74);
            this.txb_Message.TabIndex = 6;
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(315, 138);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 23);
            this.btn_Send.TabIndex = 7;
            this.btn_Send.Text = "Send";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // txb_Status
            // 
            this.txb_Status.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txb_Status.Location = new System.Drawing.Point(61, 167);
            this.txb_Status.Multiline = true;
            this.txb_Status.Name = "txb_Status";
            this.txb_Status.Size = new System.Drawing.Size(329, 136);
            this.txb_Status.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 316);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.txb_Port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txb_Status);
            this.Controls.Add(this.txb_Message);
            this.Controls.Add(this.txb_Host);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TextBox txb_Port;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_Host;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_Message;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox txb_Status;
    }
}

