using Microsoft.AspNetCore.Authorization;

namespace DigiCV.Infrastructure.Securities.Permissions
{
    public class AdminManagerRequirementHandler : AuthorizationHandler<AdminManagerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminManagerRequirement requirement)
        {
            if (context.User.HasClaim(c =>
                (c.Type == "Administrator" && c.Value == "Administrator") ||
                (c.Type == "Manager" && c.Value == "Manager")))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
