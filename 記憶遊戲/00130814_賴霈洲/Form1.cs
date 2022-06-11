using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;
using System.Threading;

namespace _00130814_賴霈洲
{
    public partial class Form1 : Form
    {
        int[] card;     // 牌陣列
        int num = 0;    //卡片編號
        Random rd = new Random();    // 亂數
        Bitmap[] b = new Bitmap[32]; //圖片陣列
        Panel Pm;   //主容器

        int First_Sceond = 1;   //翻牌順序
        int FirstCardNo;        //翻牌編號
        Panel FirstCardPanel;

        int Good = 0, Bad = 0; // 猜對 猜錯 次數

        Stopwatch sw = new Stopwatch(); // 計時碼表
        TimeSpan ts; // 經過的時間

        public Form1()
        {
            InitializeComponent();
            b[0] = Properties.Resources.f1; b[1] = Properties.Resources.f2; b[2] = Properties.Resources.f3; b[3] = Properties.Resources.f4;
            b[4] = Properties.Resources.f5; b[5] = Properties.Resources.f6; b[6] = Properties.Resources.f7; b[7] = Properties.Resources.f8;
            b[8] = Properties.Resources.f9; b[9] = Properties.Resources.f10; b[10] = Properties.Resources.f11;b[11] = Properties.Resources.f12;
            b[12] = Properties.Resources.f13; b[13] = Properties.Resources.f14; b[14] = Properties.Resources.f15; b[15] = Properties.Resources.f16;
            b[16] = Properties.Resources.f17; b[17] = Properties.Resources.f18; b[18] = Properties.Resources.f19; b[19] = Properties.Resources.f20;
            b[20] = Properties.Resources.f21; b[21] = Properties.Resources.f22; b[22] = Properties.Resources.f23; b[23] = Properties.Resources.f24;
            b[24] = Properties.Resources.f25; b[25] = Properties.Resources.f26; b[26] = Properties.Resources.f27; b[27] = Properties.Resources.f28;
            b[28] = Properties.Resources.f29; b[29] = Properties.Resources.f30; b[30] = Properties.Resources.f31; b[31] = Properties.Resources.f32;
           
        }

        private void easy_Click(object sender, EventArgs e)
        {

            Pm = new Panel();
            Pm.Location = new Point(0, 0);
            Pm.Size = new Size(600, 600);
            this.Controls.Add(Pm);

            card = new int[16];
            easy.Enabled = false;medium.Enabled = false;hard.Enabled = false;
            int temp, t1, t2;
            int x, y=20 ;//卡片座標            
            
            for (int i = 0; i < 4; i++)
            {
                x = 20;
                for (int j = 0; j < 4; j++)
                {
                    Panel p = new Panel();
                    p.Location = new Point(x, y);
                    p.Size = new Size(100, 100);
                    Pm.Controls.Add(p);
                    p.Click += Peasy_Click;
                    p.Tag = num ;
                    if (i == 3 && j == 3) continue;
                    else num += 1;
                    x += 120;
                }
                y += 120;
            }
            for (int i = 0; i < 16; i++)
                card[i] = i % 8;

            for (int i = 0; i < 100; i++)// 兩兩打亂
            {
                t1 = rd.Next(16);
                t2 = rd.Next(16);
                temp = card[t1];
                card[t1] = card[t2];
                card[t2] = temp;
            }
            foreach (Control ctrl in Pm.Controls)   // 先看
            {
                if (ctrl is Panel)
                {
                    int CardNo = Convert.ToInt32(ctrl.Tag);
                    Bitmap bmp = new Bitmap(b[card[CardNo]], 100, 100);
                    ((Panel)ctrl).BackgroundImage =bmp;
                    ((Panel)ctrl).Enabled = false;
                }
            }
            Thread.Sleep(1000);
            foreach (Control ctrl in Pm.Controls)   // 16 張牌的封面 
            {
                if (ctrl is Panel)
                {
                    int CardNo = Convert.ToInt32(ctrl.Tag);
                    Bitmap bmp = new Bitmap(Properties.Resources.ff, 100, 100);
                    ((Panel)ctrl).BackgroundImage = bmp;
                    ((Panel)ctrl).Enabled = true;
                }
            } 
            Good = 0; correct.Text = "答對："+Convert.ToString(Good);
            Bad = 0; error.Text = "答錯：" + Convert.ToString(Bad);
            sw.Reset(); // 碼表歸零
            sw.Start(); // 開始計時
            timer1.Enabled = true;
        }

        private void medium_Click(object sender, EventArgs e)
        {
            Pm = new Panel();
            Pm.Location = new Point(0, 0);
            Pm.Size = new Size(600, 600);
            this.Controls.Add(Pm);

            card = new int[36];
            easy.Enabled = false;medium.Enabled = false;hard.Enabled = false;
            int temp, t1, t2;
            int x, y = 20;//卡片座標

            for (int i = 0; i < 6; i++) // 36 張牌的封面
            {
                x = 20;
                for (int j = 0; j < 6; j++)
                {
                    Panel p = new Panel();
                    p.Location = new Point(x, y);
                    p.Size = new Size(70, 70);
                    Pm.Controls.Add(p);
                    p.Click += PMedium_Click;
                    p.Tag = num;
                    if (i == 5 && j == 5) continue;
                    else num += 1;
                    x += 90;
                }
                y += 90;
            }
            for (int i = 0; i < 36; i++)
                card[i] = i % 18;

            for (int i = 0; i < 1000; i++)// 兩兩打亂
            {
                t1 = rd.Next(36);
                t2 = rd.Next(36);
                temp = card[t1];
                card[t1] = card[t2];
                card[t2] = temp;
            }            
            foreach (Control ctrl in Pm.Controls)   // 先看
            {
                if (ctrl is Panel)
                {
                    int CardNo = Convert.ToInt32(ctrl.Tag);
                    Bitmap bmp = new Bitmap(b[card[CardNo]], 70, 70);
                    ((Panel)ctrl).BackgroundImage = bmp;
                    ((Panel)ctrl).Enabled = false;
                }
            }
            Thread.Sleep(1500);
            foreach (Control ctrl in Pm.Controls)
            {
                if (ctrl is Panel)
                {
                    Bitmap bmp=new Bitmap( Properties.Resources.ff,70,70);
                    ((Panel)ctrl).BackgroundImage =bmp;
                    ((Panel)ctrl).Enabled = true;
                }
            }
            Good = 0; correct.Text = "答對：" + Convert.ToString(Good);
            Bad = 0; error.Text = "答錯：" + Convert.ToString(Bad);
            sw.Reset(); // 碼表歸零
            sw.Start(); // 開始計時
            timer1.Enabled = true;
        }

        private void hard_Click(object sender, EventArgs e)
        {
            Pm = new Panel();
            Pm.Location = new Point(0, 0);
            Pm.Size = new Size(600, 600);
            this.Controls.Add(Pm);

            card = new int[64];
            easy.Enabled = false;medium.Enabled = false;hard.Enabled = false;
            int temp, t1, t2;
            int x, y = 20;//卡片座標

            for (int i = 0; i < 8; i++)// 64 張牌的封面
            {
                x = 20;
                for (int j = 0; j < 8; j++)
                {
                    Panel p = new Panel();
                    p.Location = new Point(x, y);
                    p.Size = new Size(50, 50);
                    Pm.Controls.Add(p);
                    p.Click += PHard_Click;
                    p.Tag = num;
                    if (i == 7 && j == 7) continue;
                    else num += 1;                    
                    x += 65;
                }
                y += 65;
            }
            for (int i = 0; i < 64; i++)
                card[i] = i % 32;

            for (int i = 0; i < 10000; i++)// 兩兩打亂
            {
                t1 = rd.Next(64);
                t2 = rd.Next(64);
                temp = card[t1];
                card[t1] = card[t2];
                card[t2] = temp;
            }
            foreach (Control ctrl in Pm.Controls)   // 先看
            {
                if (ctrl is Panel)
                {
                    int CardNo = Convert.ToInt32(ctrl.Tag);
                    Bitmap bmp = new Bitmap(b[card[CardNo]], 50, 50);
                    ((Panel)ctrl).BackgroundImage = bmp;
                    ((Panel)ctrl).Enabled = false;
                }
            }
            Thread.Sleep(2000);
            foreach (Control ctrl in Pm.Controls)
            {
                if (ctrl is Panel)
                {
                    Bitmap bmp = new Bitmap(Properties.Resources.ff, 50, 50);
                    ((Panel)ctrl).BackgroundImage = bmp;
                    ((Panel)ctrl).Enabled = true;
                }
            }
            Good = 0; correct.Text = "答對：" + Convert.ToString(Good);
            Bad = 0; error.Text = "答錯：" + Convert.ToString(Bad);
            sw.Reset(); // 碼表歸零
            sw.Start(); // 開始計時
            timer1.Enabled = true;
        }

        private void reset_Click(object sender, EventArgs e)
        {
            easy.Enabled = medium.Enabled = hard.Enabled = true;

            foreach (Control ctrl in this.Controls) if (ctrl is Panel) this.Controls.Remove(ctrl);                 

            Good = 0; correct.Text = "答對：" + Convert.ToString(Good);
            Bad = 0; error.Text = "答錯：" + Convert.ToString(Bad);
            time.Text = "經過時間：";
            sw.Reset();
            First_Sceond = 1;
            num = 0;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你確定要離開嗎?", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }

        private void Peasy_Click(object sender, EventArgs e)
        {
            Panel PanelX = (Panel)sender;
            int CardNo = Convert.ToInt32(PanelX.Tag); // 第幾張牌 0~ 15
            Bitmap bmp=new Bitmap( b[card[CardNo]],100,100);
            PanelX.BackgroundImage =bmp; // 第幾張牌的圖形
            PanelX.Enabled = false;
            PanelX.Refresh(); // 重繪 (翻牌)

            if (First_Sceond == 1) // 是按第一次
            {
                FirstCardNo = card[CardNo]; // 紀錄第一次翻牌的圖形編號
                FirstCardPanel = PanelX;
                First_Sceond = 2;
            }
            else // 是按第二次
            {
                // 第二次翻牌的圖形編號 和 第一次翻牌的圖形編號 是一致的
                if (FirstCardNo == card[CardNo])
                {
                    Thread.Sleep(200); // 停  0.3 秒
                    Pm.Controls.Remove(PanelX);
                    Pm.Controls.Remove(FirstCardPanel); 
                    Good++; // 猜對次數 加一
                    correct.Text = "答對："+Convert.ToString(Good);
                    if (Good == 8) // 如果已經 猜對8次
                    {
                        sw.Stop();
                        timer1.Enabled = false; // 就結束了
                        
                    }
                }
                else // 第二次翻牌的圖形編號 和 第一次翻牌的圖形編號 是不一致的
                {
                    //SystemSounds.Beep.Play(); // 發聲 警告
                    Thread.Sleep(500); // 停  0.5 秒

                    // 回復 第一、二次 Panel 的封面
                    FirstCardPanel.BackgroundImage = bmp = new Bitmap(Properties.Resources.ff, 100, 100);
                    PanelX.BackgroundImage = bmp = new Bitmap(Properties.Resources.ff, 100, 100);

                    FirstCardPanel.Enabled = true;
                    PanelX.Enabled = true;

                    Bad++; // 猜錯次數 加一
                    error.Text = "答錯：" + Convert.ToString(Bad);
                }
                First_Sceond = 1;
            }
        }

        private void PMedium_Click(object sender, EventArgs e) 
        {
            Panel PanelX = (Panel)sender;
            int CardNo = Convert.ToInt32(PanelX.Tag); // 第幾張牌 0~ 35
            Bitmap bmp = new Bitmap(b[card[CardNo]], 70, 70);
            PanelX.BackgroundImage = bmp; // 第幾張牌的圖形
            PanelX.Enabled = false;
            PanelX.Refresh(); // 重繪 (翻牌)

            if (First_Sceond == 1) // 是按第一次
            {
                FirstCardNo = card[CardNo]; // 紀錄第一次翻牌的圖形編號
                FirstCardPanel = PanelX;
                First_Sceond = 2;
            }
            else // 是按第二次
            {
                // 第二次翻牌的圖形編號 和 第一次翻牌的圖形編號 是一致的
                if (FirstCardNo == card[CardNo])
                {
                    Thread.Sleep(200); // 停  0.3 秒
                    Pm.Controls.Remove(PanelX);
                    Pm.Controls.Remove(FirstCardPanel); 
                    Good++; // 猜對次數 加一
                    correct.Text ="答對："+ Convert.ToString(Good);
                    if (Good == 18) // 如果已經 猜對18次
                    {
                        sw.Stop();
                        timer1.Enabled = false; // 就結束了
                        
                    }
                }
                else // 第二次翻牌的圖形編號 和 第一次翻牌的圖形編號 是不一致的
                {
                    //SystemSounds.Beep.Play(); // 發聲 警告
                    Thread.Sleep(500); // 停  0.5 秒

                    // 回復 第一、二次 Panel 的封面
                    FirstCardPanel.BackgroundImage = bmp = new Bitmap(Properties.Resources.ff, 70, 70);
                    PanelX.BackgroundImage = bmp = new Bitmap(Properties.Resources.ff, 70, 70);

                    FirstCardPanel.Enabled = true;
                    PanelX.Enabled = true;

                    Bad++; // 猜錯次數 加一
                    error.Text = "答錯：" + Convert.ToString(Bad);
                }
                First_Sceond = 1;
            }
        }

        private void PHard_Click(object sender, EventArgs e) 
        {
            Panel PanelX = (Panel)sender;
            int CardNo = Convert.ToInt32(PanelX.Tag); // 第幾張牌 0~ 63
            Bitmap bmp = new Bitmap(b[card[CardNo]], 50, 50);
            PanelX.BackgroundImage = bmp; // 第幾張牌的圖形
            PanelX.Enabled = false;
            PanelX.Refresh(); // 重繪 (翻牌)

            if (First_Sceond == 1) // 是按第一次
            {
                FirstCardNo = card[CardNo]; // 紀錄第一次翻牌的圖形編號
                FirstCardPanel = PanelX;
                First_Sceond = 2;
            }
            else // 是按第二次
            {
                // 第二次翻牌的圖形編號 和 第一次翻牌的圖形編號 是一致的
                if (FirstCardNo == card[CardNo])
                {
                    Thread.Sleep(200); // 停  0.3 秒
                    Pm.Controls.Remove(PanelX);
                    Pm.Controls.Remove(FirstCardPanel); 
                    //PanelX.Enabled = false; // 將 第二次翻牌的 Panel 設為不能再翻牌
                    //FirstCardPanel.Enabled = false; // 將 第一次翻牌的 Panel 設為不能再翻牌
                    Good++; // 猜對次數 加一
                    correct.Text = "答對："+Convert.ToString(Good);
                    if (Good == 32) // 如果已經 猜對32次
                    {
                        sw.Stop();
                        timer1.Enabled = false; // 就結束了
                    }
                }
                else // 第二次翻牌的圖形編號 和 第一次翻牌的圖形編號 是不一致的
                {
                   // SystemSounds.Beep.Play(); // 發聲 警告
                    Thread.Sleep(500); // 停  0.5 秒

                    // 回復 第一、二次 Panel 的封面
                    FirstCardPanel.BackgroundImage = bmp = new Bitmap(Properties.Resources.ff, 50, 50);
                    PanelX.BackgroundImage = bmp = new Bitmap(Properties.Resources.ff, 50, 50);

                    FirstCardPanel.Enabled = true;
                    PanelX.Enabled = true;

                    Bad++; // 猜錯次數 加一
                    error.Text = "答錯：" + Convert.ToString(Bad);
                }
                First_Sceond = 1;
            }
        } 

        private void timer1_Tick(object sender, EventArgs e)
        {
            ts = TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds);
            time.Text = "經過時間：" + Convert.ToString(ts.Minutes) + "分 " +
                          Convert.ToString(ts.Seconds) + "秒." +
                          Convert.ToString(ts.Milliseconds);
        }

       /* class Game {

            protected int[] card;
            protected int temp, t1, t2;
            protected int x, y = 20;
            protected int num = 0;
            
            int First_Sceond = 1;
            int FirstCardNo;
            Random rd = new Random();
            Panel FirstCardPanel;
            Label correct, error, time;
            
            int Good = 0, Bad = 0; // 猜對 猜錯 次數

            Stopwatch sw = new Stopwatch(); // 計時碼表
            TimeSpan ts; // 經過的時間
            Game(int n,Form f) {
                card = new int[n * n];
                for (int i = 0; i < n; i++)// 64 張牌的封面
                {
                    x = 20;
                    for (int j = 0; j < n; j++)
                    {
                        Panel p = new Panel();
                        p.Location = new Point(x, y);
                        p.Size = new Size(50, 50);
                        f.Controls.Add(p);
                        p.Click += this.Pclick;
                        p.Tag = num;
                        if (i == n-1 && j == n-1) continue;
                        else num += 1;
                        x += 65;
                    }
                    y += 65;
                }
                for (int i = 0; i < n*n; i++)
                    card[i] = i % (n*n/2);

                for (int i = 0; i < 100; i++)// 兩兩打亂
                {
                    t1 = rd.Next(n*n);
                    t2 = rd.Next(n*n);
                    temp = card[t1];
                    card[t1] = card[t2];
                    card[t2] = temp;
                }
                foreach (Control ctrl in f.Controls)   // 先看
                {
                    if (ctrl is Panel)
                    {
                        int CardNo = Convert.ToInt32(ctrl.Tag);
                        Bitmap bmp = new Bitmap(b[card[CardNo]], 100, 100);
                        ((Panel)ctrl).BackgroundImage = bmp;
                        ((Panel)ctrl).Enabled = false;
                    }
                }
                Thread.Sleep(1000);
                foreach (Control ctrl in f.Controls)   // 16 張牌的封面 
                {
                    if (ctrl is Panel)
                    {
                        ((Panel)ctrl).BackgroundImage = Properties.Resources.ff;
                        ((Panel)ctrl).Enabled = true;
                    }
                }
                Good = 0; correct.Text = "答對：" + Convert.ToString(Good);
                Bad = 0; error.Text = "答錯：" + Convert.ToString(Bad);
                sw.Reset(); // 碼表歸零
                sw.Start(); // 開始計時
                timer1.Enabled = true;


            }
            void Pclick(object sender, EventArgs e)
            {
                Panel PanelX = (Panel)sender;
                int CardNo = Convert.ToInt32(PanelX.Tag); // 第幾張牌 0~ 15
                Bitmap bmp = new Bitmap(b[card[CardNo]], 100, 100);
                PanelX.BackgroundImage = bmp; // 第幾張牌的圖形
                PanelX.Enabled = false;
                PanelX.Refresh();

                if (First_Sceond == 1) // 是按第一次
                {
                    FirstCardNo = card[CardNo]; // 紀錄第一次翻牌的圖形編號
                    FirstCardPanel = PanelX;
                    First_Sceond = 2;
                }
                else // 是按第二次
                {
                    // 第二次翻牌的圖形編號 和 第一次翻牌的圖形編號 是一致的
                    if (FirstCardNo == card[CardNo])
                    {
                        PanelX.Enabled = false; // 將 第二次翻牌的 Panel 設為不能再翻牌
                        FirstCardPanel.Enabled = false; // 將 第一次翻牌的 Panel 設為不能再翻牌
                        Good++; // 猜對次數 加一
                        correct.Text = "答對：" + Convert.ToString(Good);
                        if (Good == 8) // 如果已經 猜對8次
                        {
                            sw.Stop();
                            timer1.Enabled = false; // 就結束了

                        }
                    }
                    else // 第二次翻牌的圖形編號 和 第一次翻牌的圖形編號 是不一致的
                    {
                        SystemSounds.Beep.Play(); // 發聲 警告
                        Thread.Sleep(500); // 停  0.5 秒

                        // 回復 第一、二次 Panel 的封面
                        FirstCardPanel.BackgroundImage = Properties.Resources.ff;
                        PanelX.BackgroundImage = Properties.Resources.ff;

                        FirstCardPanel.Enabled = true;
                        PanelX.Enabled = true;

                        Bad++; // 猜錯次數 加一
                        error.Text = "答錯：" + Convert.ToString(Bad);
                    }
                    First_Sceond = 1;
                }
            }
            Game() { }
        }
        */
    }
}
