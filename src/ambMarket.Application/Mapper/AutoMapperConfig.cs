using ambMarket.Application.Services.VisitorCqrs.Command;
using ambMarket.Domain.Visitors;
using AutoMapper;

namespace ambMarket.Application.Mapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Visitor, RequestSaveVisitorCommand>();
    }
}