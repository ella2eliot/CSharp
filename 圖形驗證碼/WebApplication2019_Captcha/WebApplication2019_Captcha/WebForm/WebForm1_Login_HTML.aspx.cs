using Newtonsoft.Json.Linq;   // JSON
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2019_Captcha.WebForm
{
    public partial class WebForm1_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {

            }
            else
            {
                Label2.Text = null;
                // Google reCAPTCHA傳回的數值，以下面的 "g-recaptcha-response"名稱回應給我們
                string token = Request.Form["g-recaptcha-response"];  // 最後會多出一個 , 符號導致錯誤，所以要刪除。
                                                                                                                    // 如果您在畫面上有一個 hidden欄位就出現這種錯誤                

                // 確認是否有Token回傳
                if (string.IsNullOrEmpty(token) || token.Length == 0)
                {   // https://dotblogs.com.tw/mickey/2018/05/19/155411
                    Label2.Text = "請確認是否為機器人？（未通過 reCAPTCHA檢驗。無token）";
                }
                else
                {
                    var client = new System.Net.WebClient();
                    //****************** 自己的secret_key *************************************
                    string Secret_key = "6LfM_aYZAAAAADoxTzavLBFrVBL_N0SbPmb9Vn8V";   //************************
                    //****************** 自己的secret_key *************************************

                    var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}", Secret_key, token, Request.Url.Host));
                    // 官方網站的說明  https://developers.google.com/recaptcha/docs/verify
                    Label2.Text += "<br><br>google傳回值，JSON格式<br>" + googleReply;  // google傳回值，JSON格式。
                    
                    Label2.Text += "<br><br>網址<br>" + string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}", Secret_key, token, Request.Url.Host);
                    // 您可以把產生的網址，直接拿來測試。看看有沒有傳回值？成功或失敗？
                    client.Dispose();

                    dynamic Json = JObject.Parse(googleReply);
                    if(Json.success == "true")   // 傳回的JSON文字中，有一段 "success": true,  代表成功！屬性 "success"的值是什麼？
                        Label2.Text += "<br><br>成功";
                    else
                       Label2.Text += "<br><br>失敗";
                }
            }

        }




    }
}