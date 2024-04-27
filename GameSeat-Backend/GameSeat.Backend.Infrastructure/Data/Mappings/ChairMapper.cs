using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Data.Mappings;
public class ChairMapper : IEntityTypeConfiguration<ChairModel>
{
    public void Configure(EntityTypeBuilder<ChairModel> builder)
    {
        builder.ToTable("Chair");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Description).HasMaxLength(255);
        builder.Property(e => e.Status).HasMaxLength(255);
        builder.Property(e => e.PricePerHour);
        builder.Property(e => e.CreatedAt);
        builder.HasMany(e => e.Reservations).WithOne(r => r.Chair).HasForeignKey(r => r.ChairId);
    }
}
