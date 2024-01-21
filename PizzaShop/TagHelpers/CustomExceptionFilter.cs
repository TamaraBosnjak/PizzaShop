using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PizzaShop.TagHelpers
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private IModelMetadataProvider _metadataProvider;
        public CustomExceptionFilter(IModelMetadataProvider modelMetadataProvider)
        {
            _metadataProvider = modelMetadataProvider;
        }
        public void OnException(ExceptionContext context)
        {
            var result = new ViewResult { ViewName = "ErrorPage" };
            result.ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(_metadataProvider, context.ModelState);

            result.ViewData.Add("Exception obj", context.Exception);

            context.ExceptionHandled = true;
            context.Result = result;
        }
    }
}
