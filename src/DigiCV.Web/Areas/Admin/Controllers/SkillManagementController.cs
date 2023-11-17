using Autofac;
using DigiCV.Infrastructure;
using DigiCV.Infrastructure.Features.Exceptions;
using DigiCV.Web.Areas.Admin.Models;
using DigiCV.Web.Models;
using DigiCV.Web.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DigiCV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillManagementController : Controller
    {
        ILifetimeScope _scope;
        ILogger<SkillManagementController> _logger;
        public SkillManagementController(ILifetimeScope scope, ILogger<SkillManagementController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = _scope.Resolve<SkillListModel>();
            return View(model);
        }
        public IActionResult Create()
        {
            var model = _scope.Resolve<SkillCreateModel>();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(SkillCreateModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Create();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully inserted new row.",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server Error");

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in inserting data!",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }
        public IActionResult Update(int id)
        {
            var model = _scope.Resolve<SkillUpdateModel>();
            model.Load(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(SkillUpdateModel model)
        {
            model.ResolveDependency(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    model.Update();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Data Updated",
                        Type = ResponseTypes.Warning
                    });
                    return RedirectToAction("Index");
                }
                catch (DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server Error");
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem to update data!",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var model = _scope.Resolve<SkillListModel>();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Delete(id);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Row deleted",
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server Error");
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<JsonResult> Get()
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<SkillListModel>();

            var data = await model.GetPagedSkillsAsync(dataTablesModel);
            return Json(data);
        }
    }
}
