using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Data.Mappings
{
    public class UserMapper : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Username).HasMaxLength(255);
            builder.Property(e => e.Email).HasMaxLength(255);
            builder.Property(e => e.Admin);
            builder.Property(e => e.Image);
            builder.Property(e => e.Enable);
        }
    }
}
