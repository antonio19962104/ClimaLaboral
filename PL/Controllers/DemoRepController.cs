using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PL.Controllers
{
    public class DemoRepController : ApiController
    {
        [HttpGet]
        [Route("api/GetData")]
        public IHttpActionResult getData()
        {
            var result = BL.Reporte.GetPreguntasLikertExceptDobleByEncuesta(2186);
            return Json(result.Objects);
        }
    }
}
