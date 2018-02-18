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
    public class SchedulerController : Controller
    {
        internal DataGateway<Scheduler> dataGateway;
        

        public SchedulerController(SmartHomeDbContext context)
        {
            dataGateway = new SchedulerGateway(context);
        }

        // GET: Scheduler
        public ActionResult Index()
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

            Scheduler scheduler = dataGateway.SelectById(id);
            if (scheduler == null)
            {
                return NotFound();
            }

            return View(scheduler);
        }

        // GET: Tours/Confirm
        public ActionResult Confirm(Scheduler scheduler)
        {
            return View("Confirm", scheduler);
        }

        [HttpGet]
        public ActionResult SelectDay(string day)
        {
            TempData["selectedDay"] = day;
            return RedirectToAction("Index", "Device");
        }

        // GET: Scheduler/Create
        [HttpGet]
        public ActionResult Create(int id, string deviceName)
        {

            @ViewBag.dID = id;
            @ViewBag.dName = deviceName;
            ViewBag.dDay = TempData["selectedDay"];

            return View();
        }



        // POST: Scheduler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("deviceId,deviceName,startTime,endTime,applyToEveryWeek,dayOfWeek")] Scheduler scheduler)
        {
            if (ModelState.IsValid)
            {
                dataGateway.Insert(scheduler);
                return RedirectToAction(nameof(Confirm), scheduler);
            }
            return View(scheduler);
        }

        // GET: Scheduler/Edit/5
        public ActionResult Edit(int? id, string deviceName)
        {
            if (id == null)
            {
                return NotFound();
            }

            Scheduler scheduler = dataGateway.SelectById(id);
            if (scheduler == null)
            {
                return NotFound();
            }
            @ViewBag.dName = deviceName;
            return View(scheduler);
        }

        // POST: Scheduler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,startTime,endTime,applyToEveryWeek,dayOfWeek")] Scheduler scheduler)
        {
            if (id != scheduler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dataGateway.Update(scheduler);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchedulerExists(scheduler.Id))
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
            return View(scheduler);
        }

        // GET: Scheduler/Delete/5
        public ActionResult Delete(int? id, string deviceName)
        {
            if (id == null)
            {
                return NotFound();
            }

            Scheduler scheduler= dataGateway.SelectById(id);
            if (scheduler == null)
            {
                return NotFound();
            }
            @ViewBag.dName = deviceName;
            return View(scheduler);
        }

        // POST: Scheduler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dataGateway.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SchedulerExists(int id)
        {
            Scheduler scheduler = new Scheduler();
            if ((scheduler = dataGateway.SelectById(id)) != null)
                return true;
            return false;
        }
    }
}
