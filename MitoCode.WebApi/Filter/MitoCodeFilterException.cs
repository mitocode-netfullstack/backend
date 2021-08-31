using Microsoft.AspNetCore.Mvc.Filters;

namespace MitoCode.WebApi.Filter
{
    public class MitoCodeFilterException : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is MitcodeException)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}