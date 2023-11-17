using DigiCV.Application.Features.Training.Services;
using DigiCV.Infrastructure.Features.Services;
using DigiCV.Persistence.Features.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiCV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminManager")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResumeService _resumeService;
        private readonly ITemplateService _templateService;
        private readonly ICoverLetterService _coverLetterService;
        public DashboardController(ILogger<DashboardController> logger, UserManager<ApplicationUser> userManager, IResumeService resumeService, ITemplateService templateService, ICoverLetterService coverLetterService)
        {
            _logger = logger;
            _userManager = userManager;
            _resumeService = resumeService;
            _templateService = templateService;
            _coverLetterService = coverLetterService;
        }

        public IActionResult Index()
        {
            var totalResumeCreated = _resumeService.GetTotalResumeCount();
            var total_Users = _userManager.Users.Count();
            var total_ResumeTemplates = _templateService.GetTotalResumeTemplateCount();
            var total_CoverLetterTemplates = _coverLetterService.GetTotalCoverLetterCount();

            ViewBag.TotalResumeCount = totalResumeCreated;
            ViewBag.TotalUsers = total_Users;
            ViewBag.TotalResumeTemplates = total_ResumeTemplates;
            ViewBag.TotalCoverLetterTemplates = total_CoverLetterTemplates;

            return View();
        }

        [HttpGet]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> UsersRoles()
        {
            var users = await _userManager.Users.ToListAsync();
            IList<(string, string, string)> info = new List<(string, string email, string claim)>();

            foreach (var user in users)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);

                var claims = "";
                foreach (var claim in userClaims)
                    claims += claim.Value + " ";

                info.Add((user.Id.ToString(), user.Email, claims));
            }

            return View(info);
        }
    }
}
