using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.Web
{
    public class SecurePage : BasePage
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
                        
            var account = (string)Session[WebConstants.Session.AccountId];

            if (string.IsNullOrEmpty(account))
            {
                var rawUrl = Request.RawUrl.Replace("?", "!!").Replace("&", "||");
                var path = string.Format("~/SessionTimeOut.aspx?{0}={1}", WebConstants.Session.ReturnUrl, rawUrl);
                Server.Transfer(path);

                return;
            }

            var currentExecutionFilePath = Request.AppRelativeCurrentExecutionFilePath;

            if (string.IsNullOrEmpty(currentExecutionFilePath))
            {
                return;
            }

            var url = currentExecutionFilePath.Replace("~/", "");

            var allowed = false;

            if (IsValid)
            {
                // Validate the user password
                // var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                // var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            }

            if (!allowed)
            {
                Server.Transfer("~/Account/PermissionDenied.aspx");
            }
        }
    }
}
