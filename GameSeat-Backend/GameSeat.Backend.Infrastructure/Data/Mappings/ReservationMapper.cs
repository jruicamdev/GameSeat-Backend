﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Data.Mappings
{
    public class ReservationMapper : IEntityTypeConfiguration<ReservationModel>
    {
        public void Configure(EntityTypeBuilder<ReservationModel> builder)
        {
            builder.ToTable("Reservation");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.StartTime);
            builder.Property(e => e.EndTime);
            builder.Property(e => e.Status).HasMaxLength(255);
            builder.Property(e => e.TotalAmount);
            builder.Property(e => e.CreatedAt);
            builder.HasOne(e => e.Chair).WithMany(c => c.Reservations).HasForeignKey(e => e.ChairId);
            builder.HasOne(e => e.User).WithMany(u => u.Reservations).HasForeignKey(e => e.UserId);
        }
    }
}
