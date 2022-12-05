using System;
//=== 自己宣告或是加入 =========================
using System.Drawing;
//********************************************
using System.Drawing.Imaging;        // .NET Standard 2.0沒有這個東西，請改裝 NuGet的System.Drawing.Common
using System.Drawing.Drawing2D;   // .NET Standard 2.0沒有這個東西，請改裝 NuGet的System.Drawing.Common
//********************************************



namespace ClassLibrary2_CAPTCHA_Standard
{
    public class Mis2000lab_CAPTCHA
    {

        #region 產生亂數 

        public string GetRandomNumberString(int int_NumberLength)
        {
            System.Text.StringBuilder str_resultNumber = new System.Text.StringBuilder();
            Random rand = new Random(Guid.NewGuid().GetHashCode());  // 亂數物件(Salt)
            // 或是寫成 Random rand = new Random((int)DateTime.Now.Ticks);

            //// === 方法一，簡易版（英文數字都有）。====================
            for (int i = 1; i <= int_NumberLength; i++)
            {
                string randChar = "";
                int randSeed = rand.Next(0, 36);   // 必須放在迴圈裡面，每次執行都會產生新的亂數！

                if (randSeed < 10)
                    randChar = randSeed.ToString();
                else
                    randChar = ((char)(65 + randSeed - 10)).ToString();

                str_resultNumber.Append(randChar);   // 產生0~9的亂數，組合成字串。
            }

            //// === 方法二，指定文字版。改用英文大小寫與數字。===================
            //// 資料來源 https://blog.kkbruce.net/2013/11/asp-net-mvc-captcha.html
            ////                 http://joeshua.pixnet.net/blog/post/36256920
            //// 產生英文（大小寫）與數字。  為了避免誤判，通常不使用英文 Oo 與 Ll、大寫的I，但保留數字的 "零"。
            //// 因為數字出現的機率較少，所以我加入兩次數字
            //string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,0,1,2,3,4,5,6,7,8,9";
            //string[] allCharArray = allChar.Split(',');  // 每一個字依照逗號（,）分別拆開，一個一個放進陣列
            //for (int i = 0; i < int_NumberLength; i++)
            //{   
            //    int t = rand.Next(allCharArray.Length);  // 必須放在迴圈裡面，每次執行都會產生新的亂數！
            //    str_resultNumber.Append(allCharArray[t]);   // 產生的亂數，組合成字串。
            //}

            //== 共用，輸出結果（亂數文字）================
            return str_resultNumber.ToString();
        }
        #endregion



        #region 產生圖片
        //*************************************************************
        // 重點！必須用 NuGet 安裝 System.Drawing.Common才行！
        // 不然的話， .NET Standard 2.0 沒有 System.Drawing.Image 與 .Drawing2D
        //*************************************************************
        public System.Drawing.Image CreateCheckCodeImage(string checkCode)
        {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap((checkCode.Length * 32), 38);
            // 產生圖片，寬32（自動依照文字長度，增加寬度），高38像素
            System.Drawing.Graphics g = Graphics.FromImage(image);

            // 隨機生成器
            Random random = new Random(Guid.NewGuid().GetHashCode());

            //int int_Red = 0;
            //int int_Green = 0;
            //int int_Blue = 0;
            int int_Red = random.Next(240, 256);  // 產生0~255    // 必須放在迴圈裡面，每次執行都會產生新的亂數！
            int int_Green = random.Next(240, 256);  // 產生0~255
            int int_Blue = (int_Red + int_Green > 400 ? 150 : 400 - int_Red - int_Green);
            //int_Blue = (int_Blue > 255 ? 255 : int_Blue);
            int int_bkack = random.Next(150, 200);

            // 清空圖片背景色
            g.Clear(Color.FromArgb(int_Red, int_Green, int_Blue));
            int blackbg = random.Next(0, 120);   // 必須放在迴圈裡面，每次執行都會產生新的亂數！
            int garykbg = random.Next(120, 160);

            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            //Brush brushBack = new LinearGradientBrush(rect, Color.FromArgb(random.Next(0, 100), random.Next(230, 256), 255),
            //Color.FromArgb(255, random.Next(200, 256), 255), random.Next(45));

            // 新增黑白漸層 
            Brush brushBack = new LinearGradientBrush(rect, Color.FromArgb(int_bkack, int_bkack, int_bkack),
            Color.FromArgb(255, 255, 255), 255);
            g.FillRectangle(brushBack, rect);

            //畫圖片的背景噪音線
            for (int i = 0; i <= 6; i++)
            {
                int x1 = random.Next(image.Width);   // 必須放在迴圈裡面，每次執行都會產生新的亂數！
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);
                int b1 = random.Next(70, 150);
                int b2 = random.Next(40, 80);
                //Color line1 = Color.FromArgb(random.Next(0, 100), random.Next(220, 255), random.Next(220, 255));
                //Color line2 = Color.FromArgb(random.Next(0, 100), 255, random.Next(200, 240));

                Color line1 = Color.FromArgb(b1, b1, b1);
                Color line2 = Color.FromArgb(b2, b2, b2);

                g.DrawLine(new Pen(line1), x1, y1, x2, y2);
                g.DrawEllipse(new Pen(line2), new System.Drawing.Rectangle(x1, y1, x2, y2));
            }

            for (int i = 0; i < checkCode.Length; i++)
            {
                int Cr = 1, Cg = 1, Cb = 1;
                while (Cr + Cg + Cb != 1)
                {
                    Cr = random.Next(0, 2);   // 必須放在迴圈裡面，每次執行都會產生新的亂數！
                    Cg = random.Next(0, 2);
                    Cb = random.Next(0, 2);
                }
                //// 增強對比
                //while (Cr == Cg && Cg == Cb)   {
                //    Cr = random.Next(0, 2);
                //    Cg = random.Next(0, 2);
                //    Cb = random.Next(0, 2); 
                //}                    
                //// 字體顏色
                Color wc = Color.FromArgb(Cr * 255, Cg * 130, Cb * 255);
                Color wc2 = Color.FromArgb(Cb * 255, Cr * 130, Cg * 255);

                int y = random.Next(0, 6);   // 必須放在迴圈裡面，每次執行都會產生新的亂數！
                // 字體 Size 如果設定太大可能出錯，無法產生圖片
                Font font = new System.Drawing.Font("Tahoma", 15 + y, System.Drawing.FontStyle.Italic);   // 字型、字體大小、粗細
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), wc, wc2, 1.2F, true);

                // 設置繪筆 
                g.DrawString(checkCode.Substring(i, 1), font, brush, 8 + i * 25, 2 + random.Next(0, 6 - y));
            }

            for (int i = 0; i <= 35; i++)
            {   // 圖片的 "前景" 噪音點
                int x = random.Next(image.Width);     // 必須放在迴圈裡面，每次執行都會產生新的亂數！
                int y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            // 圖片的邊框線
            g.DrawRectangle(new Pen(Color.DarkGray, 2), 2, 2, image.Width - 5, image.Height - 5);

            return image;
        }
        #endregion






    }
}
