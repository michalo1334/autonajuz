using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutoNaJuz.DAL;
using AutoNaJuz.Model;

namespace AutoNaJuz.DAL.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .HasMaxLength(256);

            builder.Property(e => e.Transmission);
            builder.Property(e => e.ProductionYear);
            builder.Property(e => e.FuelType);
            builder.Property(e => e.SeatCount);
            builder.Property(e => e.DoorCount);
            builder.Property(e => e.BodyType);
        }
    }
}