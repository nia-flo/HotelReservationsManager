using System;
using System.Collections.Generic;
using System.Text;
using HotelReservationsManager.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationsManager.Data
{
    public class DbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<ClientReservation> ClientReservation { get; set; }

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ClientReservation>()
        //        .HasKey(cr => new { cr.ClientId, cr.ReservationId });
        //    modelBuilder.Entity<ClientReservation>()
        //        .HasOne(cr => cr.Client)
        //        .WithMany(c => c.ClientReservations)
        //        .HasForeignKey(cr => cr.ClientId);
        //    modelBuilder.Entity<ClientReservation>()
        //        .HasOne(cr => cr.Reservation)
        //        .WithMany(c => c.ClientReservations)
        //        .HasForeignKey(cr => cr.ReservationId);
        //}

    }
}
