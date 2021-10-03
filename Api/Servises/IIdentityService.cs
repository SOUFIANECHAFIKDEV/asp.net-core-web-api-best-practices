using Api.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Servises
{
    public interface IIdentityService
    {
        public Task<AuthenticationResult> RegisterAsync(string email, string password);
        public Task<AuthenticationResult> LoginAsync(string email, string password);
        public Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        public Task<AddUserToRolesResult> AddUserToRolesAsync(string email, IEnumerable<string> roles);
    }
}