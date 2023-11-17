using Autofac;
using DigiCV.Infrastructure.Features.Exceptions;
using DigiCV.Persistence.Features.Membership;
using DigiCV.Web.Models;
using DigiCV.Web.Models.Builder;
using DigiCV.Web.Models.Letter;
using DigiCV.Web.Models.PDF;
using DigiCV.Web.Utilities;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigiCV.Web.Controllers
{
    [Authorize]
    public class LetterController : Controller
    {
        ILifetimeScope _scope;
        ILogger<LetterController> _logger;
        UserManager<ApplicationUser> _userManager;
        IPdfGenerationHelper _pdfGenerationHelper;
        public LetterController(ILifetimeScope scope, ILogger<LetterController> logger, 
            UserManager<ApplicationUser> userManager, IPdfGenerationHelper pdfGenerationHelper)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
            _pdfGenerationHelper = pdfGenerationHelper;
        }
        public IActionResult Create()
        {
            var model = _scope.Resolve<LetterCreateModel>();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LetterCreateModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            model.UserId = user.Id;
            model.ResolveDependency(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    Guid LetterId = model.CreateCoverLetter();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully Created Your Cover Letter.",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("GetCoverLetter", new { id = LetterId });
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
                        Message = "Cover Letter can't create.",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var model = _scope.Resolve<LetterUpdateModel>();

            model.Load(id);

            if (user.Id == model.UserId)
            {
                return View(model);
            }
            else
            {
                return View("AccessDenied");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(LetterUpdateModel model)
        {
            model.ResolveDependency(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    model.UpdateCoverLetter();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully Updated Your Cover Letter.",  
                        Type = ResponseTypes.Warning
                    });
                    return RedirectToAction("GetCoverLetter", new { id = model.Id });
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
                        Message = "Cover Letter can't update.",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(model);
        }

        public async Task<IActionResult> GetCoverLetter(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var model = _scope.Resolve<LetterViewModel>();

            model.GetCoverLetter(id);
            if(model.Property == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if(user.Id == model.Property.UserId)
            {
                return View(model);
            }
            else
            {
                return View("AccessDenied");
            }
            
        }

        public async Task<IActionResult> GetPdf(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var model = _scope.Resolve<LetterViewModel>();

            model.GetCoverLetter(id);

            if (model.Property == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if (user.Id == model.Property.UserId)
            {
                var createModel = new PdfCreateModel
                {
                    ViewName = "CoverLetterPDF",
                    Model = model,
                    Controller = this
                };

                var pdfBytes = _pdfGenerationHelper.GeneratePdf(createModel);
                var random = new Random();
                var uniqueFileName = $"{"Cover Letter"}_{DateTime.Now.ToString("dd MM yyyy")}_{random.Next(1000, 9999)}.pdf";

                return File(pdfBytes, "application/pdf", uniqueFileName);
            }
            else
            {
                return View("AccessDenied");
            }

        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<LetterViewModel>();
            var user = await _userManager.GetUserAsync(User);

            model.GetCoverLetter(id);

            if(model.Property == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (user.Id != model.Property.UserId)
            {
                return View("AccessDenied");
            }

            try
            {
                model.DeleteCoverLetter(id);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully deleted the item.",
                    Type = ResponseTypes.Success
                });
                return RedirectToAction("Profile", "Account");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Server Error");
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "This item can't delete.",
                    Type = ResponseTypes.Danger
                });
            }
            return View(model);
        }

        public IActionResult Example()
        {
            return View();
        }
    }
}
