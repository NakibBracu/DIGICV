using Microsoft.AspNetCore.Mvc;

namespace DigiCV.Web.Models.PDF
{
    public interface IViewRenderer
    {
        Task<string> RenderViewToStringAsync(Controller controller, string viewName, object model);
    }
}