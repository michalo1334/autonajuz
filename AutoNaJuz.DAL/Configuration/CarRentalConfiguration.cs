using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutoNaJuz.Model.CarRental;

namespace AutoNaJuz.DAL.Configuration;

public class CarRentalConfiguration : IEntityTypeConfiguration<CarRental>
{
    public void Configure(EntityTypeBuilder<CarRental> builder)
    {
        builder.ToTable("Car_Rentals");

        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.From);
        builder.Property(e => e.To);
        builder.Property(e => e.Notes)
            .HasMaxLength(3000);

        builder.HasOne(e => e.Car)
            .WithMany(e => e.Rentals)
            .HasForeignKey(e => e.CarId);

        builder.HasOne(e => e.Renter)
            .WithMany()
            .HasForeignKey(e => e.RenterId);
    }
}