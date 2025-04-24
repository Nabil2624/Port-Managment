using Microsoft.EntityFrameworkCore;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Database
{
    public class ProgramContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public ProgramContext(DbContextOptions<ProgramContext> options) : base(options)
        {

        }


        public DbSet<Ship> Ships { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
