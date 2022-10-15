namespace Shared.AspCore.Filters.ModelState;

public class ValidateModelRazorPageAttribute : Attribute, IPageFilter
{
    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {

    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new PageResult();
        }
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {

    }
}