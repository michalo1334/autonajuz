using AutoNaJuz.Model;
using AutoNaJuz.Model.Car;
using AutoNaJuz.Model.CarRental;
using AutoNaJuz.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AutoNaJuz.DAL.Data;

public class AppDbContext(DbContextOptions options)
    : IdentityDbContext<User>(options)
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarRental> CarRentals { get; set; }
    public DbSet<RenterInfo> RenterInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}