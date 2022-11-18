using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using log4net;

namespace DF.Web
{
    public class BasePage : System.Web.UI.Page
    {
        private static readonly ILog _logger = LogManager.GetLogger(WebConstants.Logging.PageExceptionLog);

        /// <summary>
        /// 註解：檢查登入的權限
        /// </summary>
        public void defense()
        {
            if (HttpContext.Current.Session["Login"].ToString() == "OK")
            {
                HttpContext.Current.Response.Write("<h3>登入成功！Success！</h3>");
            }
            else
            {
                HttpContext.Current.Response.Write("<h3>*** 失敗 Fail ***</h3>");
            }
        }

        protected string GetLoginAccount()
        {
            return Session[WebConstants.Session.AccountId].ToString();
        }

        protected RequestInformations GetRequestInformations()
        {
            var informations = new RequestInformations
            {
                RemoteHost = Request.ServerVariables["REMOTE_HOST"],
                RemoteIP = Request.ServerVariables["REMOTE_ADDR"],
            };

            return informations;
        }

        /// <summary>
		/// Alert error message to client by alert message box.
		/// </summary>
		/// <param name="messageControl">Control to store message. This is used for multi-line message.</param>
		/// <param name="ex">Error exception.</param>
		/// <remarks></remarks>
		protected virtual void ShowClientMessage(HiddenField messageControl, Exception ex)
        {
            var message = "";

            while (ex != null)
            {
                message += Environment.NewLine + ex.Message;
                ex = ex.InnerException;
            }

            messageControl.Value = message;

            var script = string.Format("alert(document.getElementById('{0}').value);", messageControl.ClientID);
            this.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlertScript", script, true);
        }

        /// <summary>
        /// Alert error message to client by label.
        /// </summary>
        /// <param name="messageControl">Control to store message. This is used for multi-line message.</param>
        /// <param name="ex">Error exception.</param>
        /// <remarks></remarks>
        protected virtual void ShowClientMessage(Label messageControl, Exception ex)
        {
            var message = "";

            while (ex != null)
            {
                message += "<br/>" + ex.Message;
                ex = ex.InnerException;
            }

            messageControl.Text = message;
        }

        protected virtual void AddAlertMessage(string message)
        {
            var script = string.Format("alert('{0}');", message);
            this.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlertScript", script, true);
        }
    }
}
