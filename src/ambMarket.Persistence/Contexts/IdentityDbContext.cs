using ambMarket.Application.Interfaces;
using ambMarket.Domain.userAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ambMarket.Persistence.Contexts;

public class IdentityDbContext : IdentityDbContext<User>, IIdentityDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) :base(options)
    {
        var user = new User();
    }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().Property(x => x.UpdateDate);
        builder.Entity<User>().Property(x => x.Removed);
        builder.Entity<User>().Property(x => x.RemovedTime);
        builder.Entity<User>().Property(x => x.CreateDate);
        base.OnModelCreating(builder);
    }
}