using Films.Core.Concrete.Models;
using Films.Core.Persistence.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Films.Core.Persistence
{
    public sealed class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Video> Videos { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<VideoGenre> VideoGenres { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VideoConfigurations).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}