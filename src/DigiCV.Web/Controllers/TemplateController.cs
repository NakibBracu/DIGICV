using Autofac;
using DigiCV.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigiCV.Web.Controllers;

public class TemplateController : Controller
{
    ILifetimeScope _scope;
    ILogger<TemplateController> _logger;

    public TemplateController(ILifetimeScope scope, ILogger<TemplateController> logger)
    {
        _scope = scope;
        _logger = logger;
    }


    // GET
    public IActionResult Index()
    {
        var model = _scope.Resolve<TemplateListModel>();
        return View(model);
    }
    public IActionResult ChangeTemplate(Guid ResumeId)
    {
        var model = _scope.Resolve<TemplateListModel>();
        model.ResumeId = ResumeId;
        return View(model);
    }
}