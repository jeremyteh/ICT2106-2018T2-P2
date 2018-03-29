using Microsoft.EntityFrameworkCore;
using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.DAL
{
    public class SmartHomeDbContext : DbContext
    {
        public SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> options)
            :base(options)
        {

        }

        public DbSet<Device> Device { get; set; }
        public DbSet<Schedule> Schedule { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().ToTable("Device");
            modelBuilder.Entity<Schedule>().ToTable("Schedule");
        }
    }
}
