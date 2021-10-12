using Api.Contracts.V1;
using Api.Contracts.V1.Requests;
using Api.Contracts.V1.Responses;
using Api.Domain;
using Api.Extensions;
using Api.Servises;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Poster")]
    public class PostsController : Controller
    {
        private readonly IPostServeic _postServeic;
        private readonly IMapper _mapper;

        public PostsController(IPostServeic postServeic, IMapper mapper)
        {
            _postServeic = postServeic;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postServeic.GetAllAsync();
            var postsResponses = _mapper.Map<List<PostResponse>>(posts);
            return Ok(postsResponses);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var userOwnsPost = await _postServeic.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "you do not own this post" });
            }

            var post = await _postServeic.GetPostByIdAsync(postId);
            post.Name = request.Name;

            var updated = await _postServeic.UpdateAsync(post);

            if (updated)
                return Ok(post);

            return NotFound();
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            return Ok(await _postServeic.GetPostByIdAsync(postId));
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var tags = postRequest.Tags.Select(tag =>
            {
                return new Tags
                {
                    TagName = tag
                };
            }).ToList();

            var post = new Post
            {
                Name = postRequest.Name,
                UserId = HttpContext.GetUserId(),
                Tags = tags
            };

            if (post.Id != Guid.Empty)
                post.Id = Guid.NewGuid();

            await _postServeic.Create(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = _mapper.Map<PostResponse>(post);
            return Created(locationUrl, response);
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        [Authorize(Roles = "Admin", Policy = "MustWorkForMicrosoft")]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var deleted = await _postServeic.DeleteAsync(postId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
