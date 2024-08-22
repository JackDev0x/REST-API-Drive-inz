using Microsoft.EntityFrameworkCore;
using AutomovieApi.Services;

namespace AutomovieApi.Entities
{
    public class PlatformDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public PlatformDbContext(DbContextOptions<PlatformDbContext> options, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FavoriteAnnouncements> FavoriteAnnouncements { get; set; }
        public DbSet<AnnouncementImages> AnnouncementImages { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<BodyType> BodyTypes { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Multimedia> Multimedia { get; set; }
        public DbSet<MultimediaDataset> MultimediaDataset { get; set; }
        public DbSet<DriverAssistanceSystems> DriverAssistanceSystems { get; set; }
        public DbSet<DriverAssistanceSystemsDataset> DriverAssistanceSystemsDataset { get; set; }
        public DbSet<Safety> Safety { get; set; }
        public DbSet<SafetyDataset> SafetyDataset { get; set; }
        public DbSet<Performance> Performance { get; set; }
        public DbSet<PerformanceDataset> PerformanceDataset { get; set; }
        public DbSet<Other> Other { get; set; }
        public DbSet<OtherDataset> OtherDataset { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model>()
                .HasOne(m => m.Brand)
                .WithMany(b => b.Models)
                .HasForeignKey(m => m.BrandId);

            modelBuilder.Entity<Announcement>()
                .HasIndex(a => a.Slug)
                .IsUnique();

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(a => a.User)
                    .WithMany(u => u.Announcements)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(a => a.Comments)
                    .WithOne(c => c.Announcement)
                    .HasForeignKey(c => c.AnId);

                entity.HasMany(a => a.Images)
                    .WithOne(ai => ai.Announcement)
                    .HasForeignKey(ai => ai.AnId);
            });

            modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Announcement)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AnId);

            modelBuilder.Entity<AnnouncementImages>()
                .HasOne(ai => ai.Announcement)
                .WithMany(a => a.Images)
                .HasForeignKey(ai => ai.AnId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Multimedia>()
            .HasOne(m => m.Announcement)
            .WithMany(a => a.Multimedia)
            .HasForeignKey(m => m.AnId);

            modelBuilder.Entity<Multimedia>()
                .HasOne(m => m.MultimediaDataset)
                .WithMany()
                .HasForeignKey(f => f.featureId);


            modelBuilder.Entity<DriverAssistanceSystems>()
                .HasOne(a => a.Announcement)
                .WithMany(o => o.DriverAssistanceSystems)
                .HasForeignKey(m => m.AnId);

            modelBuilder.Entity<DriverAssistanceSystems>()
                .HasOne(m => m.DriverAssistanceSystemsDataset)
                .WithMany()
                .HasForeignKey(f => f.featureId);

            modelBuilder.Entity<Safety>()
            .HasOne(s => s.Announcement)
            .WithMany(a => a.Safety)
            .HasForeignKey(s => s.AnId);

            modelBuilder.Entity<Safety>()
                .HasOne(s => s.SafetyDataset)
                .WithMany()
                .HasForeignKey(s => s.featureId);

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Announcement)
                .WithMany(a => a.Performance)
                .HasForeignKey(p => p.AnId);

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.PerformanceDataset)
                .WithMany()
                .HasForeignKey(p => p.featureId);

            modelBuilder.Entity<Other>()
                .HasOne(o => o.Announcement)
                .WithMany(a => a.Other)
                .HasForeignKey(o => o.AnId);

            modelBuilder.Entity<Other>()
                .HasOne(o => o.OtherDataset)
                .WithMany()
                .HasForeignKey(o => o.featureId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
            }
        }
    }
}
