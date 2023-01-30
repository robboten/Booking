using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Booking.Web.Filters
{
    public class ModelRestrictions : ActionFilterAttribute
    {
        private string _parameterName;
        public ModelRestrictions(string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            _parameterName= parameterName;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.RouteData.Values[_parameterName] == null)
            {
                context.Result = new NotFoundResult();
            }
           
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if(context.Result is ViewResult viewResult)
            {
                if(viewResult.Model is null)
                {
                    context.Result = new NotFoundResult();
                }
            }
        }
    }
}
