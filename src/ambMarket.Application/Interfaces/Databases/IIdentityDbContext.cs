using _02_Framework.Application.Interfaces.DatabaseContext;
using ambMarket.Domain.userAggregate;
using Microsoft.EntityFrameworkCore;

namespace ambMarket.Application.Interfaces.Databases;

public interface IIdentityDbContext : IBaseDbContext
{
    DbSet<User> Users { get; set; }
}