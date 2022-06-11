namespace _00130814_賴霈洲
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.correct = new System.Windows.Forms.Label();
            this.error = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.Label();
            this.easy = new System.Windows.Forms.Button();
            this.medium = new System.Windows.Forms.Button();
            this.hard = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // correct
            // 
            this.correct.AutoSize = true;
            this.correct.Location = new System.Drawing.Point(29, 600);
            this.correct.Name = "correct";
            this.correct.Size = new System.Drawing.Size(41, 12);
            this.correct.TabIndex = 0;
            this.correct.Text = "答對：";
            // 
            // error
            // 
            this.error.AutoSize = true;
            this.error.Location = new System.Drawing.Point(29, 632);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(41, 12);
            this.error.TabIndex = 1;
            this.error.Text = "答錯：";
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Location = new System.Drawing.Point(133, 600);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(65, 12);
            this.time.TabIndex = 2;
            this.time.Text = "經過時間：";
            // 
            // easy
            // 
            this.easy.Location = new System.Drawing.Point(135, 627);
            this.easy.Name = "easy";
            this.easy.Size = new System.Drawing.Size(75, 23);
            this.easy.TabIndex = 3;
            this.easy.Text = "簡易";
            this.easy.UseVisualStyleBackColor = true;
            this.easy.Click += new System.EventHandler(this.easy_Click);
            // 
            // medium
            // 
            this.medium.Location = new System.Drawing.Point(216, 627);
            this.medium.Name = "medium";
            this.medium.Size = new System.Drawing.Size(75, 23);
            this.medium.TabIndex = 4;
            this.medium.Text = "中等";
            this.medium.UseVisualStyleBackColor = true;
            this.medium.Click += new System.EventHandler(this.medium_Click);
            // 
            // hard
            // 
            this.hard.Location = new System.Drawing.Point(297, 627);
            this.hard.Name = "hard";
            this.hard.Size = new System.Drawing.Size(75, 23);
            this.hard.TabIndex = 5;
            this.hard.Text = "困難";
            this.hard.UseVisualStyleBackColor = true;
            this.hard.Click += new System.EventHandler(this.hard_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(459, 627);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(75, 23);
            this.exit.TabIndex = 6;
            this.exit.Text = "結束遊戲";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(378, 627);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(75, 23);
            this.reset.TabIndex = 7;
            this.reset.Text = "重來";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 662);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.hard);
            this.Controls.Add(this.medium);
            this.Controls.Add(this.easy);
            this.Controls.Add(this.time);
            this.Controls.Add(this.error);
            this.Controls.Add(this.correct);
            this.Name = "Form1";
            this.Text = "對對碰";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label correct;
        private System.Windows.Forms.Label error;
        private System.Windows.Forms.Label time;
        private System.Windows.Forms.Button easy;
        private System.Windows.Forms.Button medium;
        private System.Windows.Forms.Button hard;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button reset;
    }
}

