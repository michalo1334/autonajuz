using AutoNaJuz.Model.Car;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoNaJuz.DAL.Configuration;

public class CarFeatureConfiguration : IEntityTypeConfiguration<CarFeature>
{
    public void Configure(EntityTypeBuilder<CarFeature> builder)
    {
        builder.ToTable("Car_Features");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .HasMaxLength(4000);
        
        builder.HasMany(c => c.Cars)
            .WithMany(c => c.Features);
    }
}