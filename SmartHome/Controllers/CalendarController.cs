using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DAL;
using SmartHome.Models;

namespace SmartHome.Controllers
{
    public class CalendarController : Controller
    {
        internal DataGateway<Scheduler> dataGateway;

        public CalendarController(SmartHomeDbContext context)
        {
            dataGateway = new SchedulerGateway(context);
        }

        public ActionResult calendar()
        {
            IEnumerable<Scheduler> abc = dataGateway.SelectAll();
            @ViewBag.def = abc;
            return View(dataGateway.SelectAll());
        }            
    }
}