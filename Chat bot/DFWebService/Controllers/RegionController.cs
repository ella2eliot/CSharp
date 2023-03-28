using SYS.BLL;
using SYS.BLL.Base;
using System;
using System.Web.Http;

namespace DFWebService.Controllers
{
    public class RegionController : ApiController
    {
        private readonly IBusinessLogicFactory _factory;

        public RegionController() : this(new BusinessLogicFactory())
        {
        }

        public RegionController(IBusinessLogicFactory factory)
        {
            this._factory = factory;
        }
        // GET: Region
        [HttpPost]
        public IHttpActionResult Post()
        {
            try
            {
                return Ok(_factory.GetLogic<IRegionLogic>().ReadRegion());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}