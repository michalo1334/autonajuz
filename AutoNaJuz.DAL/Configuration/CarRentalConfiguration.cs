using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutoNaJuz.DAL;
using AutoNaJuz.Model;

namespace AutoNaJuz.DAL.Configuration
{
    public class CarRentalConfiguration : IEntityTypeConfiguration<CarRental>
    {
        public void Configure(EntityTypeBuilder<CarRental> builder)
        {
            builder.ToTable("Car_Rentals");

            builder.HasKey(e => e.Id);
        }
    }
}