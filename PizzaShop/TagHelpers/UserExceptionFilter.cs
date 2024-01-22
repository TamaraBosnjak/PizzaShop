using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PizzaShop.TagHelpers
{
    public class UserExceptionFilter : IExceptionFilter
    {
        private IModelMetadataProvider _metadataProvider;

        public UserExceptionFilter(IModelMetadataProvider modelMetadataProvider)
        {
            _metadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            var result = new ViewResult { ViewName = "UserErrorPage" };
            result.ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(_metadataProvider, context.ModelState);

            result.ViewData.Add("Exception user", context.Exception);

            context.ExceptionHandled = true;
            context.Result = result;
        }
    }
}
