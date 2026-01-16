using Microsoft.EntityFrameworkCore;
namespace RepositoryLayer.EF
{
    public class RepoDbContext : DbContext
    {
        public RepoDbContext(DbContextOptions<RepoDbContext> options) : base(options) { }

        public DbSet<ForumPostEntity> ForumPosts { get; set; } = null!;
        public DbSet<ShotCallerEntity> ShotCaller { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ForumPostEntity>(b =>
            {
                b.ToTable("ForumPosts");
                b.HasKey(x => x.Id);
                b.Property(x => x.Title).HasMaxLength(250).IsRequired();
                b.Property(x => x.Body).IsRequired();
                b.Property(x => x.UserId).IsRequired();
            });

            modelBuilder.Entity<ShotCallerEntity>(b =>
            {
                b.ToTable("ShotCaller");
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).HasMaxLength(250).IsRequired();
                b.Property(x => x.Tequila).IsRequired();
                b.Property(x => x.Vodka).IsRequired();
                b.Property(x => x.Beers).IsRequired();
            });
        }
    }

  
}