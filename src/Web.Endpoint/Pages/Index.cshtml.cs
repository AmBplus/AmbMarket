using _02_Framework.Application.Interfaces.Repositories;
using ambMarket.Domain.Visitors;
using ambMarket.Infrastructure.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Endpoint.Pages;
[ServiceFilter(typeof(AddVisitorToPageAttribute))]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        
    }
    public void OnGet()
    {
        
    }
}

