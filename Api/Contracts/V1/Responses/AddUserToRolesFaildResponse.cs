using System.Collections.Generic;

namespace Api.Contracts.V1.Responses
{
    public class AddUserToRolesFaildResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}