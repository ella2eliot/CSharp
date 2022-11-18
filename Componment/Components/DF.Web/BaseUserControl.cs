using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.Web
{
    public class BaseUserControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// Get the login user account id.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        protected string GetLoginAccount()
        {
            return Session[WebConstants.Session.AccountId].ToString();
        }

    }
}
