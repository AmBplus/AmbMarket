using _02_Framework.Application.DbContext;
using ambMarket.Domain.userAggregate;
using Microsoft.EntityFrameworkCore;

namespace ambMarket.Application.Interfaces;

public interface IIdentityDbContext : IBaseDbContext
{
    DbSet<User> Users { get; set; }
}