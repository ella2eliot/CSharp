using SYS.BLL.Base;
using SYS.BLL.Domain.Line;
using isRock.LineBot;
using isRock.LineBot.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DFWebService.Controllers
{
    public class LineBotAZController : ApiController
    {
        private readonly IBusinessLogicFactory _factory;
        private ILineBotLogic _LineBotLogic;        
        private string _accessToken;
        public LineBotAZController() : this(new BusinessLogicFactory())
        {
            this._accessToken = "RCGMQrgQmpEKboLZu0TREWK/7klJ1gsWGaq2vM9D6/1Q3/Xs3J33vnavCSxATwo4Ikj4dBgc0QM0IA3/tdNE1qa+/bMJGws8/d2e7W6fKDUYDiozqFDQ/Ks7ICZlpMEjDp2AmD47JOklHCTrB3ogKwdB04t89/1O/w1cDnyilFU=";

        }
        public LineBotAZController(IBusinessLogicFactory factory)
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
                string postData = Request.Content.ReadAsStringAsync().Result;
                var ReceivedMessage = Utility.Parsing(postData);
                if (ReceivedMessage.events != null)
                {
                    var userSays = ReceivedMessage.events[0].message.text;
                    var replyToken = ReceivedMessage.events[0].replyToken;
                    _factory.GetLogic<ILineBotReplyLogic>().ReplyMessage(replyToken, $"LineBot 發生了些錯誤，請聯繫 ITS \n usersays: {userSays} \n exceptions: {ex.Message}", _accessToken);
                    // _factory.GetLogic<ILineBotReplyLogic>().ReplyMessage(replyToken, $"LineBot 發生了些錯誤，請聯繫 ITS \n usersays: {userSays} \n exceptions: {ex.ToString()}", _accessToken);
                }
                return Ok();
            }


        }
    }
}
