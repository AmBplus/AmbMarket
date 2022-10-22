using ambMarket.Application.Interfaces;
using ambMarket.Domain.userAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ambMarket.Persistence.Contexts;

public class IdentityDbContext : IdentityDbContext<User>, IIdentityDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) :base(options)
    {
    
    }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        var users = builder.Entity<User>();
        users.Property(x => x.UpdateDate);
        users.Property(x => x.Removed);
        users.Property(x => x.RemovedTime);
        users.Property(x => x.CreateDate);
        users.Property(x => x.Name);
        users.Property(x => x.LastName);
        base.OnModelCreating(builder);
    }
}