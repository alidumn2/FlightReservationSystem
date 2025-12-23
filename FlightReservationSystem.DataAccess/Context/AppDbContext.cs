using FlightReservation.Core;
using FlightReservation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        // Veritabanı tablolarımız
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<BusinessSeat> BusinessSeats { get; set; }

        // Veritabanı yapılandırması
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = System.IO.Path.Combine(projectPath, "flightreservation.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        // Model oluşturma ve ilişkiler
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
