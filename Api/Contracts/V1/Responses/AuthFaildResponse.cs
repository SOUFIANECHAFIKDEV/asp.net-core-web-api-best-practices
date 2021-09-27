using System.Collections.Generic;

namespace Api.Contracts.V1.Responses
{
    public class AuthFaildResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}