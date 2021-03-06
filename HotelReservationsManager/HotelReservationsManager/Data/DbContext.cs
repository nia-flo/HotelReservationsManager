﻿using System;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }

    }
}
