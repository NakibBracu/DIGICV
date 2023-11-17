using Autofac;
using DigiCV.Infrastructure;
using DigiCV.Web.Areas.Admin.Models;
using DigiCV.Web.Models;
using DigiCV.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DigiCV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        ILifetimeScope _scope;
        ILogger<UserController> _logger;

        public UserController(ILifetimeScope scope, ILogger<UserController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [Authorize(Policy = "Administrator")]
        public IActionResult Index()
        {
            var model = _scope.Resolve<UserListModel>();

            return View(model);
        }

        public async Task<JsonResult> GetUsers()
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<UserListModel>();

            object data = await model.GetPagedUsersAsync(dataTablesModel);
            return Json(data);
        }

        public IActionResult Update(Guid id)
        {
            var model = _scope.Resolve<UserUpdateModel>();
            model.Load(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(UserUpdateModel model)
        {
            model.ResolveDependency(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    model.Update();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server Error");
                }
            }
            return View(model);
        }
    }
}
