using Autofac;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Domain.Services;
using DigiCV.Infrastructure.Securities;
using DigiCV.Persistence.Features.Membership;
using DigiCV.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace DigiCV.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<AuthController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailMessageService _emailService;
        private readonly ITokenService _tokenService;

        public AuthController(ILifetimeScope scope,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AuthController> logger,
            IEmailMessageService emailService,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _scope = scope;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> RegisterAsync(string returnUrl = null)
        {
            var model = _scope.Resolve<RegisterModel>();
            model.ReturnUrl = returnUrl;
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            model.ReturnUrl ??= Url.Content("~/Auth/Login");
            //model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var at = model.Email.IndexOf('@');
                var userName = model.Email.Substring(0, at);

                var user = new ApplicationUser
                {
                    UserName = userName,
                    Email = model.Email,
                    FullName = model.FullName,
                };

                user.JoiningDate = DateTime.Now;
                user.UserProfile = new UserProfile { IsActive = true };

                var result = await _userManager.CreateAsync(user, model.Password);

                // Adding default CLAIM to USER
                var claim = new Claim("User", "User");
                var claimResult = await _userManager.AddClaimAsync(user, claim);

                if (result.Succeeded && claimResult.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.ActionLink(
                        action:"ConfirmEmail", 
                        controller: "Account",
                        values: new { userId = user.Id, code = code, returnUrl = model.ReturnUrl },
                        protocol:Request.Scheme);

                    try
                    {
                        _emailService.CreateConfirmationEmail(model.Email, model.FullName, callbackUrl);
                    }catch (Exception ex)
                    {
                        await _userManager.DeleteAsync(user);
                        _logger.LogError("Email Send Failed", ex);
                    }

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToAction("RegisterConfirmation", "Account", new { email = model.Email, returnUrl = model.ReturnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(model.ReturnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            //if (!string.IsNullOrEmpty(ErrorMessage))
            //{
            //    ModelState.AddModelError(string.Empty, ErrorMessage);
            //}
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = _scope.Resolve<LoginModel>();
            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var at = model.Email.LastIndexOf('@');
                var userName = model.Email.Substring(0, at);
                var result = await _signInManager.PasswordSignInAsync(userName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if(result.IsNotAllowed)
                {
                    return RedirectToAction("RegisterConfirmation", "Account", new {email=model.Email});

                }
				
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email); 
                    var claims = (await _userManager.GetClaimsAsync(user)).ToArray();
                    var token = await _tokenService.GetJwtToken(claims);
                    HttpContext.Session.SetString("token", token);

                    var userClaim = await _userManager.GetClaimsAsync(user);

                    if (userClaim.Any(c => (c.Value == "Administrator" || c.Value == "Manager")) && model.ReturnUrl == Url.Content("~/"))
                        model.ReturnUrl = Url.Content("~/Admin");
                    else model.ReturnUrl ??= Url.Content("~/");
                    
                    return LocalRedirect(model.ReturnUrl);
                }

                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                //}
                //if (result.IsLockedOut)
                //{
                //    _logger.LogWarning("User account locked out.");
                //    return RedirectToPage("./Lockout");
                //}
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> LogoutAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
