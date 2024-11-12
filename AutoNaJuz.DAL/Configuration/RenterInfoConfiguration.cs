using AutoNaJuz.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoNaJuz.DAL.Configuration;

public class RenterInfoConfiguration : IEntityTypeConfiguration<RenterInfo>
{
    public void Configure(EntityTypeBuilder<RenterInfo> builder)
    {
        builder.ToTable("Renter_Info");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.DriversLicenseIdent)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.Pesel)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(e => e.BirthDate)
            .IsRequired();

        builder.Property(e => e.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Street)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.BuildingNumber)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.ApartmentNumber)
            .HasMaxLength(10)
            .IsRequired(false);

        builder.Property(e => e.City)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.PostalCode)
            .HasMaxLength(10)
            .IsRequired();
    }
}