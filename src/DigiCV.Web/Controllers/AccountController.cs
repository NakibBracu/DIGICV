using Autofac;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Persistence.Features.Membership;
using DigiCV.Web.Models;
using DigiCV.Web.Models.Letter;
using DigiCV.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DigiCV.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILifetimeScope _scope;

        public AccountController(ILogger<AccountController> logger, 
            UserManager<ApplicationUser> userManager,
            ILifetimeScope scope)
        {
            _logger = logger;
            _userManager = userManager;
            _scope = scope;

        }
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user != null && code != null)
            {
                var decodedTokenByte = WebEncoders.Base64UrlDecode(code);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenByte);
                IdentityResult result = await _userManager.ConfirmEmailAsync(user, decodedToken);
                if(result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Login", "Auth");
                }
                return RedirectToAction(nameof(EmailConfirmationFailed));
            }

            return RedirectToAction(nameof(EmailConfirmationFailed));
        }
        [AllowAnonymous]
        public IActionResult EmailConfirmationFailed(string code=null)
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> RegisterConfirmation(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var isverified = await _userManager.IsEmailConfirmedAsync(user);
                if (!isverified)
                {
                    return View();
                }
            }

            return RedirectToAction("Login", "Auth");
        }
        public async Task<IActionResult> Profile()
        {
            var model = _scope.Resolve<UserProfileModel>();
            await model.LoadData(User);

            return View(model);
        }
        public async Task<JsonResult> UpdateUserProfileImage(IFormFile blob)
        {
            var model = _scope.Resolve<UserProfileModel>();
            try
            {
                await model.UpdateProfileImage(User, blob);
            }
            catch (Exception ex)
            {
                return Json(new {Message = "ERROR" });
                }
            return Json(new { Message = "OK" });

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateUserProfile(UserProfileModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    
                    await model.UpdateProfile(User);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Type = ResponseTypes.Success,
                        Message = "Profile Updated Successfully"
                    });
                    return RedirectToAction("Profile");
                }
                catch(Exception ex) 
                {
                    _logger.LogError(ex, ex.Message);
                }
            }
            model.LoadResumeIdandTitleList(Guid.Parse(_userManager.GetUserId(User)));
            model.LoadCoverLetterIdandTitleList(Guid.Parse(_userManager.GetUserId(User)));

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Type = ResponseTypes.Danger,
                Message = "Update Failed"
            }) ;
            return View("Profile", model);
        }

        public async Task<IActionResult> Cv()
        {
           var user = await _userManager.GetUserAsync(User);

            var model = new ResumeListModel();
            ViewBag.PDFTemplateName = "Modern";
            if(user != null && user.Resumes != null)
            {
                model.Resumes = (IList<Domain.Entities.Resume>)user.Resumes;
            }
            return View(model);
        }
        public async Task<IActionResult> CoverLetter()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new LetterViewModel();
            if (user != null && user.Letters != null)
            {
                model.Letters = (IList<Domain.Entities.CoverLetter>)user.Letters;
            }

            return View(model);
        }
    }
}
