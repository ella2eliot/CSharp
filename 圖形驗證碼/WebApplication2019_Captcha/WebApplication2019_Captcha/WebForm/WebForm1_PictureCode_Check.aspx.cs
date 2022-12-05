using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2019_Captcha.WebForm
{
    public partial class WebForm1_PictureCode_Check : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox3.Text))
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "請輸入「圖形驗證碼」！";
                TextBox3.Focus();
            }


            if (TextBox3.Text == Session["ValidatePictureCode"].ToString())
            {
                Label1.ForeColor = System.Drawing.Color.Blue;
                Label1.Text = "圖形驗證  成功！";
            }
            else
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "圖形驗證  失敗！";
            }
        }


    }
}