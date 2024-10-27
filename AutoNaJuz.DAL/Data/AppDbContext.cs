using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoNaJuz.Model;
using AutoNaJuz.Model.Car;
using AutoNaJuz.Model.CarRental;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AutoNaJuz.DAL.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) 
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
                
        }
    }
}