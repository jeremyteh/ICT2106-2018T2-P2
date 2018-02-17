using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.DAL
{
    public class SchedulerGateway : DataGateway<Scheduler>
    {
        public SchedulerGateway(SmartHomeDbContext context) : base(context)
        {

        }
    }
}
