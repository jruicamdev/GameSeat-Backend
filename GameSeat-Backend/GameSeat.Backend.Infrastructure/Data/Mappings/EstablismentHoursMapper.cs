using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Data.Mappings
{
    public class EstablishmentHourMapper : IEntityTypeConfiguration<EstablishmentHourModel>
    {
        public void Configure(EntityTypeBuilder<EstablishmentHourModel> builder)
        {
            builder.ToTable("EstablishmentHours");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.DayOfWeek);
            builder.Property(e => e.OpeningTime);
            builder.Property(e => e.ClosingTime);
        }
    }
}
