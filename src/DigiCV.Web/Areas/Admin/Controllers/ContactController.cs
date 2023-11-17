using Autofac;
using DigiCV.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigiCV.Web.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class ContactController : Controller
    {
        ILifetimeScope _scope;
        ILogger<ContactController> _logger;

        public ContactController(ILifetimeScope scope, ILogger<ContactController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [Authorize(Policy = "AdminManager")]
        public IActionResult Index()
        {
            var model = _scope.Resolve<ContactListModel>();

            return View(model);
        }
    }
}
