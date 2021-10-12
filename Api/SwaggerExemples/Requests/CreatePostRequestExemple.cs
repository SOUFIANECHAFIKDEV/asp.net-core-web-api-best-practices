using Api.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace Api.SwaggerExemples.Requests
{
    public class CreatePostRequestExemple : IExamplesProvider<CreatePostRequest>
    {
        public CreatePostRequest GetExamples()
        {
            return new CreatePostRequest
            {
                Name = "new post"
            };
        }
    }
}
