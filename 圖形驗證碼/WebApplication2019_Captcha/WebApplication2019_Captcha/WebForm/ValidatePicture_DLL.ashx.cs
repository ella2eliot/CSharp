using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//============================
using System.Drawing;
using System.Web.SessionState;   // 使用 Session 所以會用到

//********************************************
using ClassLibrary2_CAPTCHA_Standard;
using System.Drawing.Imaging;        // .NET Standard 2.0沒有這個東西，請改裝 NuGet的System.Drawing.Common
using System.Drawing.Drawing2D;   // .NET Standard 2.0沒有這個東西，請改裝 NuGet的System.Drawing.Common
//********************************************

       
namespace WebApplication2019_Captcha.WebForm
{
    /// <summary>
    /// ValidatePicture_DLL 的摘要描述
    /// </summary>
    public class ValidatePicture_DLL : IHttpHandler, IRequiresSessionState
    {                                                                  //*************************使用 Session 所以會用到

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            int NumCount = 5;  // 預設產生5位亂數

            if (!string.IsNullOrEmpty(context.Request.QueryString["NumCount"]))
            {   // 您也可以透過網址傳遞數值，例如 ValidateCode.ashx?NumCount=5  指定產生幾位數
                // 字串轉數字，轉型成功則儲存到 NumCount。不成功的話，NumCount為0
                Int32.TryParse(context.Request.QueryString["NumCount"].Replace("'", "''"), out NumCount);
            }

            if (NumCount == 0)
                NumCount = 5;

            //*************************************************************************
            Mis2000lab_CAPTCHA mis2000Lab_CAPTCHA = new Mis2000lab_CAPTCHA();
            //*************************************************************************

            // 取得亂數 -- 自己寫的副程式 GetRandomNumberString
            string str_ValidatePictureCode = mis2000Lab_CAPTCHA.GetRandomNumberString(NumCount);

            //**** 用於驗證的Session ****
            context.Session["ValidatePictureCode"] = str_ValidatePictureCode;

            // 產生圖片 -- 自己寫的副程式
            System.Drawing.Image image = mis2000Lab_CAPTCHA.CreateCheckCodeImage(str_ValidatePictureCode);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            // 輸出圖片，呈現在網頁（.aspx檔）上的 <asp:Image>或 <Img>
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";
            context.Response.BinaryWrite(ms.ToArray());
            ms.Close();
        }




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}