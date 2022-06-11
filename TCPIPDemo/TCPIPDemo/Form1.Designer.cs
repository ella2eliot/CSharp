namespace TCPIPDemo
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
            this.label1 = new System.Windows.Forms.Label();
            this.txb_Host = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_Port = new System.Windows.Forms.TextBox();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.txb_Status = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host:";
            // 
            // txb_Host
            // 
            this.txb_Host.Location = new System.Drawing.Point(52, 23);
            this.txb_Host.Name = "txb_Host";
            this.txb_Host.Size = new System.Drawing.Size(100, 22);
            this.txb_Host.TabIndex = 1;
            this.txb_Host.Text = "127.0.0.1";
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(306, 23);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 2;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Port:";
            // 
            // txb_Port
            // 
            this.txb_Port.Location = new System.Drawing.Point(194, 23);
            this.txb_Port.Name = "txb_Port";
            this.txb_Port.Size = new System.Drawing.Size(100, 22);
            this.txb_Port.TabIndex = 1;
            this.txb_Port.Text = "8910";
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(387, 23);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(75, 23);
            this.btn_Stop.TabIndex = 2;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // txb_Status
            // 
            this.txb_Status.Location = new System.Drawing.Point(52, 65);
            this.txb_Status.Multiline = true;
            this.txb_Status.Name = "txb_Status";
            this.txb_Status.Size = new System.Drawing.Size(410, 165);
            this.txb_Status.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 274);
            this.Controls.Add(this.txb_Status);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.txb_Port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txb_Host);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_Host;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_Port;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.TextBox txb_Status;
    }
}

