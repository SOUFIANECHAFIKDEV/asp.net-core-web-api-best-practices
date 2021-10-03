using Api.Contracts.V1;
using Api.Servises;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagsController : Controller
    {
        private readonly ITagsService _tagsService;

        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpGet(ApiRoutes.Tags.GetAll)]
        [Authorize(Policy ="TagViewer")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tagsService.GetAllAsync());
        }
    }
}