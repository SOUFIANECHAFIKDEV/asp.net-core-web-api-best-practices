using Api.Domain;
using System.Threading.Tasks;

namespace Api.Servises
{
    public interface IIdentityService
    {
        public Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}