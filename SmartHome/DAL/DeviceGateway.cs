using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.DAL
{
    public class DeviceGateway : DataGateway<Device>
    {
        public DeviceGateway(SmartHomeDbContext context) : base(context)
        {

        }
    }
}
