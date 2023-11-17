using Autofac;
using DigiCV.Domain.Entities;
using DigiCV.Web.Models;
using DigiCV.Web.Models.Builder;
using DigiCV.Web.Models.PDF;
using DigiCV.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DigiCV.Persistence.Features.Membership;
using Microsoft.AspNetCore.Identity;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using DigiCV.Web.Areas.Admin.Models;
using System.Security.Claims;

namespace DigiCV.Web.Controllers;

[Authorize]
public class BuilderController : Controller
{
    private readonly ILifetimeScope _scope;
    ILogger<BuilderController> _logger;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IPdfGenerationHelper _pdfGenerationHelper; 
     private readonly UserManager<ApplicationUser> _userManager;
    private readonly IViewRenderer _viewRenderer;
    private readonly IConverter _pdfConverter;
    private readonly BuilderCreateModel _builderCreateModel;
    public Guid IdFromCreateResume { get; set; }

    public BuilderController(ILifetimeScope scope, ILogger<BuilderController> logger,
        UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment, 
        IPdfGenerationHelper pdfGenerationHelper, IViewRenderer viewRenderer, IConverter pdfConverter, BuilderCreateModel builderCreateModel)
    {
        _userManager = userManager;
        _scope = scope;
        _logger = logger;
        _hostingEnvironment = hostingEnvironment;
        _pdfGenerationHelper = pdfGenerationHelper;
        _viewRenderer = viewRenderer;
        _pdfConverter = pdfConverter;
        _builderCreateModel = builderCreateModel;
    }

    public IActionResult Index(Guid Id)
    {
        var skillData = GetSkill();
        _builderCreateModel.SkillLitem = skillData.Select(skill => new SelectListItem
        {
            Value = skill.Id.ToString(),
            Text = skill.Name
        }).ToList();

        var model = _scope.Resolve<BuilderCreateModel>();
        model.TemplateId = Id;
        return View(model);
    }

    public async Task<IActionResult> GetResume(Guid Id)
    {

        var currentUser = await _userManager.GetUserAsync(User);
        var currentUserID = currentUser.Id;

        var model = _scope.Resolve<ResumeViewModel>();
        var user = await _userManager.GetUserAsync(User);
        ViewBag.Username = user.UserName;
        
        await model.GetResumeDataAsync(Id);


        if (model.ResumeProperty != null)
        {
            Guid? UserIdFromResume = model.ResumeProperty.UserId;
            if (UserIdFromResume != null && UserIdFromResume == currentUserID)
            {
                ViewBag.SpecificActionCompleted = true;
                ViewBag.IdForPDF = Id;
                ViewBag.ResumeTitle = model.ResumeProperty.Title.ToLower().Replace(' ', '-');
                ViewBag.IsCurrentUser = true;
                var templateId = model.ResumeProperty.ResumeTemplateId ?? Guid.Empty;
                var TemplateName = await model.GetResumeViewName(templateId);
                ViewBag.PDFTemplateName = TemplateName + "PDF";
                return View($"{TemplateName}", model);
            }
            else {
                return View("AccessDenied");
            }
        }

        // If no matching conditions were met, return the "Home/Index" view.
        return RedirectToAction("Index", "Home");

    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateResume(BuilderCreateModel model)
    {

        if (!ModelState.IsValid)
        {
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Some field are Invalid!",
                Type = ResponseTypes.Danger
            });
            var skillData = GetSkill();
            model.SkillLitem = skillData.Select(skill => new SelectListItem
            {
                Value = skill.Id.ToString(),
                Text = skill.Name
            }).ToList();
            return View("Index", model);
        }
        var user = await _userManager.GetUserAsync(User);
        model.UserId = user.Id;
        model.ResolveDependency(_scope);

        try
        {
            // Capture the generated Guid when calling InsertIntoDatabase
            Guid newResumeId = await model.InsertIntoDatabase();

            // Store the Id in the model for use in the view
            model.ResumeId = newResumeId;
            IdFromCreateResume = newResumeId;
            ViewBag.IdFromCreateResume = newResumeId; // Store IdFromCreateResume in ViewBag

            // Redirect to GetResume action with newResumeId as a query parameter
            return RedirectToAction("GetResume", new { Id = newResumeId });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Server Error");
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Something went wrong!",
                Type = ResponseTypes.Danger
            });
        }
        return View();
    }

    public async Task<IActionResult> UpdateResume(Guid resumeId)
    {
        var model = _scope.Resolve<BuilderUpdateModel>();
        await model.LoadData(resumeId);
        var skillData = GetSkill();
        model.SkillItems = skillData.Select(skill => new SelectListItem
        {
            Value = skill.Id.ToString(),
            Text = skill.Name
        }).ToList();
        return View("update", model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateResume(BuilderUpdateModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);
            model.UserId = Guid.Parse(_userManager.GetUserId(User));
            var result = await model.UpdateResume();
            if (result == true)
            {
                return RedirectToAction("ChangeTemplate","Template", new { resumeId = model.ResumeId });
            }
            else
            {
                _logger.LogError("Server Error");
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Something went wrong!",
                    Type = ResponseTypes.Danger
                });
            }           
        }
        TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
        {
            Message = "Something went wrong!",
            Type = ResponseTypes.Danger
        });
        var skillData = GetSkill();
        model.SkillItems = skillData.Select(skill => new SelectListItem
        {
            Value = skill.Id.ToString(),
            Text = skill.Name
        }).ToList();
        return View("update", model);
    }
    public async Task<IActionResult> UpdateResumeTemplate(Guid resumeId, Guid templateId)
    {
        var model = _scope.Resolve<BuilderUpdateModel>();
        model.ResumeId = resumeId;
        model.UserId = Guid.Parse(_userManager.GetUserId(User)!);
        model.TemplateId = templateId;
        bool result = await model.ChangeResumeTemplate();
        if (result)
        {
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Template Change Success fully!",
                Type = ResponseTypes.Success
            });
        }
        else
        {
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Something went wrong!",
                Type = ResponseTypes.Danger
            });
        }
        return RedirectToAction("Profile", "Account");
    }
    public async Task<IActionResult> GetPdf(Guid Id, string PDFTemplateName)
    {
        if (PDFTemplateName != null) {
            var currentUser = await _userManager.GetUserAsync(User);
            var currentUserID = currentUser.Id;
            var model = _scope.Resolve<ResumeViewModel>();
            await model.GetResumeDataAsync(Id);
            if (model.ResumeProperty != null)
            {
                Guid? UserIdFromResume = model.ResumeProperty.UserId;
                if (UserIdFromResume != null && UserIdFromResume == currentUserID)
                {
                    var createModel = new PdfCreateModel
                    {
                        ViewName = PDFTemplateName,
                        Model = model,
                        Controller = this
                    };

                    var pdfBytes = _pdfGenerationHelper.GeneratePdf(createModel);
                    var random = new Random();

                    // Remove the "PDF" word from the template name
                    string fileNameWithoutPDF = PDFTemplateName.Replace("PDF", "").Trim();

                    var uniqueFileName = $"{fileNameWithoutPDF}_{DateTime.Now.ToString("ddMMyyyy")}_{random.Next(1000, 9999)}.pdf";

                    return File(pdfBytes, "application/pdf", uniqueFileName);
                }
                else
                {
                    return View("AccessDenied");
                }

            }
        }

        // If no matching conditions were met, return the "Home/Index" view.
        return RedirectToAction("Index", "Home");

    }

    public IEnumerable<Skill> GetSkill()
    {
        var model = _scope.Resolve<SkillListModel>();
        return model.GetAllSkill();
    }
}