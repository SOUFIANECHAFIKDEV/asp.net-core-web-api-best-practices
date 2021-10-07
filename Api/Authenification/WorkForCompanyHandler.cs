using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Authenification
{
    public class WorkForCompanyHandler : AuthorizationHandler<WorkForCompanyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WorkForCompanyRequirement requirement)
        {
            var userEmailAdress = context.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            if (userEmailAdress.EndsWith(requirement.DomaineName))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
