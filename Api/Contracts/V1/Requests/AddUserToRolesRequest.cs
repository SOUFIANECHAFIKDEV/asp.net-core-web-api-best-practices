using System.Collections.Generic;

namespace Api.Contracts.V1.Requests
{
    public class AddUserToRolesRequest
    {
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}