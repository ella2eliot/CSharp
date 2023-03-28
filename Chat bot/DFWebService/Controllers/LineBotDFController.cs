using SYS.BLL.Base;
using SYS.BLL.Constants;
using SYS.BLL.Domain.Line;
using System;
using System.Web.Http;

namespace DFWebService.Controllers
{
    public class LineBotDFController : ApiController
    {
        private readonly IBusinessLogicFactory _factory;
        private ILineBotLogic _LineBotLogic;
        public LineBotDFController() : this(new BusinessLogicFactory())
        {

        }
        public LineBotDFController(IBusinessLogicFactory factory)
        {
            this._factory = factory;
            _LineBotLogic = factory.GetLogic<ILineBotLogic>();
        }
        [HttpPost]
        public IHttpActionResult Post()
        {            
            try
            {
                string postData = Request.Content.ReadAsStringAsync().Result;
                var Message = _LineBotLogic.EventAZHandel(postData);
                
                return Ok();
            }
            catch (Exception ex)
            {
                _LineBotLogic._TransactionLogRepository.Create(TransactionLogConstants.LineAPI, "Error: API input", $"LineBotDFController API ERROR", ex.ToString(), SpecialEditorConstants.LineAPI);
                return Ok();
            }


        }
    }
}
