using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoNaJuz.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AutoNaJuz.DAL.Data
{
    public class AppDbContext : IdentityDbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) 
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = _configuration.GetConnectionString("MsSql");
            options.UseSqlServer(connectionString);
        }
    }
}