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

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

    }
}
