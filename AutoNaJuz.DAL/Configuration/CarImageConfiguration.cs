using AutoNaJuz.Model.CarImage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoNaJuz.DAL.Configuration;

public class CarImageConfiguration : IEntityTypeConfiguration<CarImage>
{
    public void Configure(EntityTypeBuilder<CarImage> builder)
    {
        builder.ToTable("Car_Images");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.MimeType).IsRequired();
        
        builder.Property(x => x.Description)
            .HasMaxLength(4000);
        
        builder.HasMany(x => x.Cars)
            .WithMany(x => x.Images);
    }
}