using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ValidNumPic
{
    public class ValidNumPic
    {
        private int number;
        private int width;
        private int height;
        private Bitmap bmp;
        private string validAnswer;
        private Random rr ;

        public Bitmap ValidPic {
            get { return this.bmp; }
        }

        public string ValidAnswer {
            get { return this.validAnswer; }
        }

        public ValidNumPic(int number=6)
        {
            this.rr = new Random();
            this.number = number;
            this.width = 30 + number * 30 + 30;
            this.height = 70;
            this.bmp = new Bitmap(this.width, this.height);

            //初始動作
            Init(); //初始動作
            RenderImage();  //產生圖
        }

        public void Init()
        {            
            List<char> ll = new List<char>();
            for (int i=50;i<=57;i++)
                ll.Add((char)i);
            for (int i = 65; i <= 90; i++)
            {
                if (i == 79) continue;
                ll.Add((char)i);
            }
            for (int i = 65; i <= 90; i++)
            {
                if (i == 79) continue;
                ll.Add((char)i);
            }
            for (int i = 97; i <= 122; i++)
            {
                if (i == 108) continue;
                ll.Add((char)i);
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= number; i++)
            {
                sb.Append(ll[rr.Next(ll.Count)]);
            }
            this.validAnswer = sb.ToString(); 
        }
        //產生驗證圖的部分
        private void RenderImage()
        {
            Color bgColor = Color.FromArgb(rr.Next(256), rr.Next(256), rr.Next(256));
            Brush bb;
            Graphics gg = Graphics.FromImage(this.bmp);

            bb = new SolidBrush(bgColor);
            gg.FillRectangle(bb, 0, 0, this.width, this.height);

            Color fontColor = Color.FromArgb(bgColor.R ^ 255, bgColor.G ^ 255, bgColor.B ^ 255);
            Font ff;
            bb = new SolidBrush(fontColor);

            for (int i = 0; i < this.validAnswer.Length; i++)
            {
                int xx = 20 + i * 30;
                int fontSize = rr.Next(15) + 16;
                int yy = this.height - fontSize * 2 - 10;
                ff = new Font("Arial Black", fontSize, FontStyle.Bold);
                gg.DrawString(this.validAnswer.Substring(i, 1), ff, bb, new Point(xx, yy));
            }

            //加入雜點
            for (int i = 1; i <= this.number * 100; i++)
            {
                bb = new SolidBrush(Color.White);
                gg.FillRectangle(bb, rr.Next(this.width), rr.Next(this.height), 2, 2);
            }

        }
    }
}
