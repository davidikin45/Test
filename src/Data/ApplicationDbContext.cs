using Microsoft.EntityFrameworkCore;
using Test.Dto;

namespace Test.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<RoomProgress> RoomProgressStats { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().ToTable("RX_Job");
            modelBuilder.Entity<RoomType>().ToTable("RX_RoomType");
            modelBuilder.Entity<RoomProgress>().HasNoKey();
        }
    }
}
