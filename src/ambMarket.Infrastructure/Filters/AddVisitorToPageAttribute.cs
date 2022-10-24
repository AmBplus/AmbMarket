using ambMarket.Infrastructure.Utilities.Map;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ambMarket.Infrastructure.Filters;

public class AddVisitorToPageAttribute : Attribute,  IPageFilter
{
    private IMediator Mediator { get; }
    public AddVisitorToPageAttribute(IMediator mediator)
    {
        Mediator = mediator;
    }
    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
      
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
    
    }

    public async void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        if(context.HandlerMethod.HttpMethod.ToLower() != "get")return;
        var requestSaveVisitor = MapUtilityInfrastructure.MapHttpContextToRequestSaveVisitorDto(context.HttpContext);
        await Mediator.Send(requestSaveVisitor);
    }
}