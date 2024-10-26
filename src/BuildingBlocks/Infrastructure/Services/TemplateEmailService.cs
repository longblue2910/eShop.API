using Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Shared.Services.Email;

namespace Infrastructure.Services;

public class TemplateEmailService(IRazorViewEngine engine, IServiceProvider serviceProvider, ITempDataProvider tempDataProvider) : ITemplateEmailService
{
    private IRazorViewEngine _razorViewEngine = engine;
    private IServiceProvider _serviceProvider = serviceProvider;
    private ITempDataProvider _tempDataProvider = tempDataProvider;

    public async Task<string> GetTemplateHtmlAsStringAsync(string viewPath, MailModel model)
    {
        var httpContext = new DefaultHttpContext() { RequestServices = _serviceProvider };
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

        using StringWriter sw = new();
        var viewResult = _razorViewEngine.FindView(actionContext, viewPath, false);

        if (viewResult.View == null)
        {
            return string.Empty;
        }

        var metadataProvider = new EmptyModelMetadataProvider();
        var msDictionary = new ModelStateDictionary();
        var viewDataDictionary = new ViewDataDictionary(metadataProvider, msDictionary)
        {
            Model = model
        };

        var tempDictionary = new TempDataDictionary(actionContext.HttpContext, _tempDataProvider);
        var viewContext = new ViewContext(
            actionContext,
            viewResult.View,
            viewDataDictionary,
            tempDictionary,
            sw,
            new HtmlHelperOptions()
        );

        await viewResult.View.RenderAsync(viewContext);
        return sw.ToString();
    }

}
