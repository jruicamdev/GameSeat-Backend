using GameSeat.Backend.Infrastructure.Data.Mappings;
using GameSeat.Backend.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameSeat.Backend.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ApplyConfiguration(new ChairMapper());
            modelBuilder.ApplyConfiguration(new EstablishmentHourMapper());
            modelBuilder.ApplyConfiguration(new PaymentMapper());
            modelBuilder.ApplyConfiguration(new UserMapper());
        }
        public virtual DbSet<ChairModel> Chairs { get; set; }

        public virtual DbSet<EstablishmentHourModel> EstablishmentHours { get; set; }

        public virtual DbSet<PaymentModel> Payments { get; set; }

        public virtual DbSet<ReservationModel> Reservations { get; set; }

        public virtual DbSet<UserModel> Users { get; set; }
    }
}
