using Autofac;
using DigiCV.Persistence.Features.Membership;
using DigiCV.Web.Models;
using DigiCV.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigiCV.Web.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<ResumeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public ResumeController(ILogger<ResumeController> logger,  ILifetimeScope scope, UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Share(string username, string resumeTitle)
        {
            var model = _scope.Resolve<ResumeViewModel>();

            try
            {
                var user = await _userManager.FindByNameAsync(username);

                if (user is not null)
                {
                    resumeTitle = resumeTitle.ToLower().Replace(' ', '-');
                    await model.GetResumeDataAsync(user.Id, resumeTitle);
                    if(model.ResumeProperty == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                    ViewBag.SpecificActionCompleted = true;
                    ViewBag.Username = username.ToLower();
                    ViewBag.ResumeTitle = resumeTitle.ToLower().Replace(' ', '-');

					var templateId = model.ResumeProperty.ResumeTemplateId ?? Guid.Empty;
					var TemplateName = await model.GetResumeViewName(templateId);

					return View($"{TemplateName}", model);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Server error");

                return NotFound();
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<ResumeListModel>();
            try
            {
                await model.DeleteResume(id, Guid.Parse(_userManager.GetUserId(User)));
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Type = ResponseTypes.Success,
                    Message = "Profile Updated Successfully"
                });
            }
            catch(Exception e)
            {
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Type = ResponseTypes.Danger,
                    Message = "Resume Delete Failed"
                });
            }
            return RedirectToAction("Profile", "Account");
        }
    }
}
