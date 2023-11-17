using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace DigiCV.Web.Models.PDF
{
    public class ViewRenderer : IViewRenderer
    {
        public async Task<string> RenderViewToStringAsync(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using var writer = new StringWriter();
            var viewEngine = controller.HttpContext.RequestServices.GetService(typeof(IRazorViewEngine)) as IRazorViewEngine;
            var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

            if (viewResult.View == null)
            {
                throw new ArgumentNullException($"{viewName} does not match any available view");
            }

            var viewContext = new ViewContext(
                controller.ControllerContext,
                viewResult.View,
                controller.ViewData,
                controller.TempData,
                writer,
                new HtmlHelperOptions()
            );

            // Create a URL helper to resolve ~ in image paths
            var urlHelper = new UrlHelper(controller.ControllerContext);
            viewContext.ViewData["UrlHelper"] = urlHelper;

            // Render the view
            await viewResult.View.RenderAsync(viewContext);

            return writer.GetStringBuilder().ToString();
        }

    }
}
