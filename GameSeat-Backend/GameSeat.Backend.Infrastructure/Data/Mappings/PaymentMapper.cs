using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Data.Mappings
{
    public class PaymentMapper : IEntityTypeConfiguration<PaymentModel>
    {
        public void Configure(EntityTypeBuilder<PaymentModel> builder)
        {
            builder.ToTable("Payment");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Amount);
            builder.Property(e => e.PaymentMethod).HasMaxLength(255);
            builder.Property(e => e.CreatedAt);
            builder.HasOne(e => e.Reservation)
                   .WithMany(r => r.Payments) // Asegúrate de que ReservationModel tiene una colección de PaymentModel
                   .HasForeignKey(e => e.ReservationId);
        }
    }
}
