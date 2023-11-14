using demo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace demo.Data;

public class SampleContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Follow> Follows { get; set; } = null!;

    public SampleContext(DbContextOptions<SampleContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Followers)
            .WithOne(f => f.FollowsUser)
            .HasForeignKey(f => f.FollowsUserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Follows)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId);
    }
}