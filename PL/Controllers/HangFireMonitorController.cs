using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class HangFireMonitorController : Controller
    {
        // GET: HangFireMonitor
        public ActionResult HangFire()
        {
            return View(BL.HangFireMonitor.GetJobs());
        }
    }
}