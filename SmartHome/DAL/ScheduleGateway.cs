using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.DAL
{
    public class ScheduleGateway : DataGateway<Schedule>
    {
        public ScheduleGateway(SmartHomeDbContext context) : base(context)
        {

        }
    }
}
