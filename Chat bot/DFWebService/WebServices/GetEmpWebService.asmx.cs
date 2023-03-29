using SYS.BLL.Base;
using System.Collections.Generic;
using System.Web.Services;
using SYS.BLL.Entities;
using SYS.BLL.Entities.WebServices;
using System.Web.Services.Protocols;
using SYS.BLL.Domain.GAIA;

namespace DFWebService.WebServices
{
    /// <summary>
    /// Summary description for GetEmpWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GetEmpWebService : System.Web.Services.WebService
    {
        private readonly IBusinessLogicFactory _factory;
        public AuthHeader AuthHeader;

        public GetEmpWebService() : this(new BusinessLogicFactory())
        {
        }
        public GetEmpWebService(IBusinessLogicFactory factory)
        {
            this._factory = factory;
        }

        [SoapHeader("AuthHeader")]
        [WebMethod]
        public List<EmpEntity> GetEmpByName(string lang, string name)
        {
            var result = new List<EmpEntity>();
            if (CheckUser(AuthHeader))
            {
                result = _factory.GetLogic<IGAIALogic>().GetEmpByName(lang, name);
            }
            else
            {
                result.Add(new EmpEntity { remark = "Invalidate auth" });
            }
            return result;            
        }

        [SoapHeader("AuthHeader")]
        [WebMethod]
        public EmpEntity GetEmpByEmpNo(string empNo)
        {
            EmpEntity result = new EmpEntity();
            if (CheckUser(AuthHeader))
            {            
                result = _factory.GetLogic<IGAIALogic>().GetEmpByEmpNo(empNo);
            }
            else
            {
                result = new EmpEntity { remark = "Invalidate auth" };
            }
            return result;
        }

        private bool CheckUser(AuthHeader authHeader)
        {
            if (authHeader == null)
            {
                return false;
            }
            else
            {
                var user = authHeader.UserName;
                var password = authHeader.Password;
                if ((user == "dfITS") && (password == "ITS@dmin01"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
