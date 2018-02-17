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
    public class DeviceController : Controller
    {
        internal DataGateway<Device> dataGateway;       
        public DeviceController(SmartHomeDbContext context)
        {
            dataGateway = new DeviceGateway(context);
        }
        // GET: Devices
        public ActionResult Index()
        {
            return View(dataGateway.SelectAll());
        }

        // GET: Devices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Device device = dataGateway.SelectById(id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Tours/Confirm
        public ActionResult Confirm(Device device)
        {
            return View("Confirm", device);
        }

        // GET: Devices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,HouseholdID,DeviceName,Brand,Model,Type,Location,State,UsageKwH,favourite,timestamp")] Device device)
        {
            if (ModelState.IsValid)
            {
                dataGateway.Insert(device);
                return RedirectToAction(nameof(Confirm), device);
            }
            return View(device);
        }

        // GET: Devices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Device device = dataGateway.SelectById(id);
            if (device == null)
            {
                return NotFound();
            }
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,HouseholdID,DeviceName,Brand,Model,Type,Location,State,UsageKwH,favourite,timestamp")] Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dataGateway.Update(device);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
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
            return View(device);
        }

        // GET: Devices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Device device = dataGateway.SelectById(id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dataGateway.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
            Device device = new Device();
            if ((device = dataGateway.SelectById(id)) != null)
                return true;
            return false;
        }
    }
}
