using SYS.BLL.Base;
using SYS.BLL.Constants;
using SYS.BLL.Domain;
using SYS.BLL.Domain.GAIA;
using SYS.BLL.Domain.Line;
using SYS.BLL.Entities;
using SYS.BLL.Entities.WebServices;
using SYS.Model.SQL.ChatBot;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace DFWebService.WebServices
{
    /// <summary>
    /// Summary description for ChatBotWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ChatBotWebService : System.Web.Services.WebService
    {
        private readonly IBusinessLogicFactory _factory;
        public AuthHeader AuthHeader;

        public ChatBotWebService() : this(new BusinessLogicFactory())
        {
        }
        public ChatBotWebService(IBusinessLogicFactory factory)
        {
            this._factory = factory;
        }

        [WebMethod]
        public bool IsRegisted(string lineId)
        {
            var result = _factory.GetLogic<ILineBotLogic>().IsRegisted(lineId);
            return result;
        }
        
        [SoapHeader("AuthHeader")]
        [WebMethod]
        public string ValidateEmailandEmpNo(string lineId, string email)
        {
            var result = "";
            if (CheckUser(AuthHeader))
            {
                result = _factory.GetLogic<ILineBotLogic>().ValidateEmailandEmpNo(lineId, email);
            }
            else
            {
                result = "Invalid Login";
            }

            return result;
        }
        [SoapHeader("AuthHeader")]
        [WebMethod]
        public string SendRegistCode(string lineId, string empNo, string empMail)
        {
            var result = "";
            if (CheckUser(AuthHeader))
            {
                result = _factory.GetLogic<IMailLogic>().SendRegistCode(lineId, empNo, empMail);

            }
            else
            {
                result = "Invalid Login";
            }

            return result;
        }
        [SoapHeader("AuthHeader")]
        [WebMethod]
        public string ValidateRegistCode(string lineId, string code)
        {
            var result = "";
            if (CheckUser(AuthHeader))
            {
                result = _factory.GetLogic<ILineBotLogic>().ValidateRegistCode(lineId, code);
            }
            return result;
        }

        [SoapHeader("AuthHeader")]
        [WebMethod]        
        public EmpEntity GAIAGetEmpByEmpNo(string input)
        {
            var result = new EmpEntity();
            if (CheckUser(AuthHeader))
            {
                result = _factory.GetLogic<IGAIALogic>().GetEmpByEmpNo(input);
            }
            return result;
        }

        [SoapHeader("AuthHeader")]
        [WebMethod]
        public List<EmpEntity> GAIAGetEmpByName(string lan, string empName)
        {
            var result = new List<EmpEntity>();
            if (CheckUser(AuthHeader))
            {
                result = _factory.GetLogic<IGAIALogic>().GetEmpByName(lan, empName);
            }
            return result;
        }

        [SoapHeader("AuthHeader")]
        [WebMethod]
        public AccountRegist LineGetEmpByLineId(string input)
        {
            var result = new AccountRegist();
            if (CheckUser(AuthHeader))
            {
                result = _factory.GetLogic<IAccountRegistLogic>().GetAccountByLineId(input);
            }
            return result;
        }

        [SoapHeader("AuthHeader")]
        [WebMethod]
        public AccountRegist LineGetEmpByEmpNo(string input)
        {
            var result = new AccountRegist();
            if (CheckUser(AuthHeader))
            {
                result = _factory.GetLogic<IAccountRegistLogic>().GetAccountByEmpNo(input);
            }
            return result;
        }       

        [SoapHeader("AuthHeader")]
        [WebMethod]
        public string GetGAIASSOredirectURL(string apiKey, string sourceSystem, string targetSystem, string userLineId, string targetPage)
        {
            var result = "";
            if (CheckUser(AuthHeader))
            {
                var sid = _factory.GetLogic<IAccountRegistLogic>().GetAccountByLineId(userLineId).Emp_Id;
                result = _factory.GetLogic<IGAIALogic>().GetGAIASSOredirectURL(apiKey, GAIASystemsConstants.Line, GAIASystemsConstants.GAIA, sid, targetPage);
            }
            return result;
        }
        
        [SoapHeader("AuthHeader")]
        [WebMethod]
        public void CreateAcc(AccountRegist input)
        {
            if (CheckUser(AuthHeader))
            {
                _factory.GetLogic<IAccountRegistLogic>().CreateLineAcc(input);
            }
        }

        [SoapHeader("AuthHeader")]
        [WebMethod]
        public void UpdateAcc(AccountRegist input)
        {
            if (CheckUser(AuthHeader))
            {
                _factory.GetLogic<IAccountRegistLogic>().UpdateAcc(input);
            }
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
                if ((user == "user") && (password == "user"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
