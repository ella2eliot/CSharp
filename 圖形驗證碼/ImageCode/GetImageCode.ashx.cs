using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.SessionState;


namespace Darfon.HRMS.Web
{
    public partial class GetImageCode : IHttpHandler, IRequiresSessionState
    {
        private const double PI = 3.14159265358979;

        private const double DOUBLE_PI = 6.28318530717959;

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public GetImageCode()
        {
        }

        //http://localhost:50599/GetImageCode.ashx?r=123
        public void ProcessRequest(HttpContext ctx)
        {
            string str = "";
            byte[] numArray = this.GenerateVerifyImage(6, ref str, true, false);
            SessionManager.ImageCode = str;
            ctx.Response.OutputStream.Write(numArray, 0, (int)numArray.Length);
        }


        private byte[] GenerateVerifyImage(int keyLength, ref string imageCodeKey, bool pureBlackBackGround, bool drawDisturbLine)
        {
            int num;
            int num1;
            int num2;
            Pen pen;
            int num3 = 26 * keyLength + 8;
            int num4 = 20;
            Bitmap bitmap = new Bitmap(num3, num4);
            Random random = new Random((int)DateTime.Now.Ticks);
            if (pureBlackBackGround)
            {
                num = 255;
                num1 = 255;
                num2 = 255;
            }
            else
            {
                num = random.Next(255) % 128 + 128;
                num1 = random.Next(255) % 128 + 128;
                num2 = random.Next(255) % 128 + 128;
            }
            Graphics graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(new SolidBrush(Color.FromArgb(num, num1, num2)), 0, 0, num3, num4);
            if (drawDisturbLine)
            {
                int num5 = 3;
                pen = (pureBlackBackGround ? new Pen(Color.FromArgb(12, 12, 12), 1f) : new Pen(Color.FromArgb(num - 17, num1 - 17, num2 - 17), 1f));
                for (int i = 0; i < num5; i++)
                {
                    int num6 = random.Next() % num3;
                    int num7 = random.Next() % num4;
                    int num8 = random.Next() % num3;
                    int num9 = random.Next() % num4;
                    graphic.DrawLine(pen, num6, num7, num8, num9);
                }
            }
            string str = "ABCDEFGHJKLMNPRSTUVWXYZ23456789";
            string str1 = "";
            for (int j = 0; j < keyLength; j++)
            {
                int num10 = j * 26 + random.Next(3);
                int num11 = random.Next(2) + 1;
                Font font = new Font("Courier New", (float)(13 + random.Next() % 4), FontStyle.Bold);
                char chr = str[random.Next(str.Length)];
                str1 = string.Concat(str1, chr.ToString());
                if (pureBlackBackGround)
                {
                    graphic.DrawString(chr.ToString(), font, new SolidBrush(Color.FromArgb(0, 0, 0)), (float)num10, (float)num11);
                }
                else
                {
                    graphic.DrawString(chr.ToString(), font, new SolidBrush(Color.FromArgb(num - 60 + num11 * 3, num1 - 60 + num11 * 3, num2 - 40 + num11 * 3)), (float)num10, (float)num11);
                }
            }
            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Jpeg);
            bitmap.Dispose();
            graphic.Dispose();
            imageCodeKey = str1;
            byte[] array = memoryStream.ToArray();
            memoryStream.Close();
            return array;
        }


        public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            Bitmap bitmap = new Bitmap(srcBmp.Width, srcBmp.Height);
            Graphics graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);
            graphic.Dispose();
            double num = (bXDir ? (double)bitmap.Height : (double)bitmap.Width);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    double num1 = Math.Sin((bXDir ? 6.28318530717959 * (double)j / num : 6.28318530717959 * (double)i / num) + dPhase);
                    int num2 = 0;
                    int num3 = 0;
                    num2 = (bXDir ? i + (int)(num1 * dMultValue) : i);
                    num3 = (bXDir ? j : j + (int)(num1 * dMultValue));
                    Color pixel = srcBmp.GetPixel(i, j);
                    if (num2 >= 0 && num2 < bitmap.Width && num3 >= 0 && num3 < bitmap.Height)
                    {
                        bitmap.SetPixel(num2, num3, pixel);
                    }
                }
            }
            return bitmap;
        }
    }



}
