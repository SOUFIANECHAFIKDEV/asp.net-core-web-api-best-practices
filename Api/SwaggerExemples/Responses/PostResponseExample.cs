using Api.Contracts.V1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace Api.SwaggerExemples.Responses
{
    public class PostResponseExample : IExamplesProvider<PostResponse>
    {
        public PostResponse GetExamples()
        {
            return new PostResponse
            {
                Name = "Post Name",
            };
        }
    }
}
