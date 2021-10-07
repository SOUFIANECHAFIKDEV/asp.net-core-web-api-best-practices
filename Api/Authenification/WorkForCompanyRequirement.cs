using Microsoft.AspNetCore.Authorization;

namespace Api.Authenification
{
    public class WorkForCompanyRequirement : IAuthorizationRequirement
    {
        public string DomaineName { get; set; }
    
        public WorkForCompanyRequirement(string domaineName)
        {
            DomaineName = domaineName;
        }
    }
}