using System.Collections.Generic;

namespace Api.Contracts.V1.Responses
{
    public class PostResponse
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}