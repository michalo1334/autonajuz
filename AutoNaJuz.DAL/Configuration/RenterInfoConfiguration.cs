using AutoNaJuz.Model;
using AutoNaJuz.Model.RenterInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoNaJuz.DAL.Configuration;

public class RenterInfoConfiguration : IEntityTypeConfiguration<RenterInfo>
{
    public void Configure(EntityTypeBuilder<RenterInfo> builder)
    {
        builder.ToTable("Renter_Infos");

        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.Email);
        
        builder.Property(e => e.FullName)
            .IsRequired()
            .HasMaxLength(100);
    }
}