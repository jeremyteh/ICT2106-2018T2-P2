using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartHome.DAL;
using SmartHome.Models;

namespace SmartHome.Controllers
{
    public class ScheduleController : Controller
    {
        internal DataGateway<Schedule> dataGateway;
        

        public ScheduleController(SmartHomeDbContext context)
        {
            dataGateway = new ScheduleGateway(context);
        }

        // GET: Scheduler
        public ActionResult Index()
        {
            return View(dataGateway.SelectAll());
        }

        // GET: Scheduler
        public ActionResult Index1()
        {
            return View(dataGateway.SelectAll());
        }

        // GET: Scheduler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Schedule schedule = dataGateway.SelectById(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Tours/Confirm
        public ActionResult Confirm(Schedule schedule)
        {
            Console.WriteLine("HELLO" + schedule.device.DeviceName);
            return View("Confirm", schedule);
        }

        [HttpGet]
        public ActionResult SelectDay(string day)
        {
            TempData["selectedDay"] = day;
            return RedirectToAction("Index", "Device");
        }

        // GET: Scheduler/Create
        /*[HttpGet]
        public ActionResult Create(int id, string deviceName)
        {

            @ViewBag.dID = id;
            @ViewBag.dName = deviceName;
            ViewBag.dDay = TempData["selectedDay"];

            return View();
        }*/
        
        public ActionResult Create(Device device)
        {
            ViewBag.dDay = TempData["selectedDay"];

            ViewData["Device"] = device;

            return View();
        }



        // POST: Scheduler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("startTime,endTime,applyToEveryWeek,dayOfWeek,device")] Schedule schedule)
        {
            Console.WriteLine("ByeBye" + schedule.endTime);
            if (ModelState.IsValid)
            {
                
                dataGateway.Insert(schedule);
                return RedirectToAction(nameof(Confirm), schedule);
            }
            return View(schedule);
        }

        // GET: Scheduler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Schedule schedule = dataGateway.SelectById(id);
            if (schedule == null)
            {
                return NotFound();
            }
            //@ViewBag.dID = schedule.deviceId;
            //@ViewBag.dName = schedule.deviceName;
            return View(schedule);
        }

        // POST: Scheduler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("scheduleId,startTime,endTime,applyToEveryWeek,dayOfWeek")] Schedule schedule)
        {
            if (id != schedule.scheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dataGateway.Update(schedule);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.scheduleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: Scheduler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Schedule schedule = dataGateway.SelectById(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        // POST: Scheduler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dataGateway.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            Schedule schedule = new Schedule();
            if ((schedule = dataGateway.SelectById(id)) != null)
                return true;
            return false;
        }
    }
}
