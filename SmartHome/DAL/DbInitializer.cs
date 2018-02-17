using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartHome.Models;

namespace SmartHome.DAL
{
    public class DbInitializer
    {
        public static void Initialize(SmartHomeDbContext context)
        {
            context.Database.EnsureCreated();

            //Look for any device.
            if (context.Device.Any())
            {
                return;     //DB has been seeded
            }

            var device = new Device();
            context.Device.Add(device);

            context.SaveChanges();
        }
    }
}
