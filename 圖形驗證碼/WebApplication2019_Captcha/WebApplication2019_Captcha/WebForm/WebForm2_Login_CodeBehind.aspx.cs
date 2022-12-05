using Newtonsoft.Json.Linq;   // JSON
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2019_Captcha.WebForm
{
    public partial class WebForm2_Login_CodeBehind : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = null;
            string token = Request.Form["g-recaptcha-CodeBehind"];  // 最後會多出一個 , 符號導致錯誤，所以要刪除。
                                                                  // 如果您在畫面上有一個 hidden欄位就出現這種錯誤  
            token = token.Substring(0, token.Length - 1);  // 刪除最後一個字（ ,符號）
            Label1.Text += token;


            // 確認是否有Token回傳
            if (token == "" || token.Length == 0)   // 最後會多出一個 , 符號
            {   // https://dotblogs.com.tw/mickey/2018/05/19/155411
                Label1.Text = "請確認是否為機器人？（未通過 reCAPTCHA檢驗。無token）";
            }
            else
            {    // 建立一個HttpWebRequest網址指向Google的驗證API
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://www.google.com/recaptcha/api/siteverify");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";

                // Post的資料（如下）
                //    (1)   secret: 自己的secret_key  (Server端)  ****
                //    (2)   response: 回傳的Token
                //    (3)   remoteip: 自己設定的Domain Name （如localhost）  ****

                //****************** 自己的secret_key ***************************************
                string Secret_key = "6LfM_aYZAAAAADoxTzavLBFrVBL_N0SbPmb9Vn8V";   //************************
                //****************** 自己的secret_key ****************************************

                string postStr = string.Format("secret={0}&response={1}&remoteip={2}", Secret_key, token, Request.Url.Host);
                Label1.Text = "https://www.google.com/recaptcha/api/siteverify?" + postStr;

                byte[] byteStr = Encoding.UTF8.GetBytes(postStr);
                // 把Post的資料寫進HttpWebRequest
                using (Stream streamArr = req.GetRequestStream())
                {
                    streamArr.Write(byteStr, 0, byteStr.Length);
                }
                // 取得回傳 (CallBack)資料
                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader googleReply = new StreamReader(res.GetResponseStream()))
                    {
                        string json = googleReply.ReadToEnd();
                        Label1.Text += "<br><br>傳回的數值 --   " + json;

                        string returnResult = "\"success\": true,";
                        if (json.IndexOf(returnResult) == -1)   // 傳回的JSON文字中，有一段名為"success": true,  代表成功！
                            Label1.Text += "<br><br>成功";
                        else
                            Label1.Text += "<br><br>失敗";
                    }
                }
            }   // end of if


        }


    }
}