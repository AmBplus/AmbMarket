using ambMarket.Application.Mapper;
using ambMarket.Application.Services.VisitorCqrs.Command;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ambMarket.Infrastructure.ConfigurationServices.Common;

public static class CommonServices
{
    public static void BootstrapCommonServices(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddMediatR(typeof(RequestSaveVisitorCommand));
        var mapperConfig = new AutoMapper.MapperConfiguration(
            cfg => cfg.AddProfile(new AutoMapperConfig()));
        services.AddSingleton(mapperConfig.CreateMapper());
        services.AddSignalR();
    }
}