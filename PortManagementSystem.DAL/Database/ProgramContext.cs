using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Database
{
    public class ProgramContext : DbContext
    {
        private readonly IConfiguration _config;

        public ProgramContext(IConfiguration config)
        {
            _config = config;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ship>().Property(i => i.terminalId).IsRequired(false);
            
            modelBuilder.Entity<TempTerminal>()
               .HasMany(v => v.tempShips)
               .WithOne(e => e.tempTerminal)
               .HasForeignKey(e => e.tempTerminalId)
               .OnDelete(DeleteBehavior.Cascade);

        }


        public DbSet<Ship> Ships { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ShipsWaiting> shipsWaitingTable {  get; set; }
        public DbSet<TempShip> TempShips { get; set; }
        public DbSet<TempShipsWaiting> TempShipsWaiting { get; set; }
        public DbSet<TempTerminal> TempTerminals { get; set; }

    }
}
