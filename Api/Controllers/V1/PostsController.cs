using Api.Contracts.V1;
using Api.Contracts.V1.Requests;
using Api.Contracts.V1.Responses;
using Api.Domain;
using Api.Servises;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers.V1
{
    public class PostsController : Controller
    {
        private readonly IPostServeic _postServeic;

        public PostsController(IPostServeic postServeic)
        {
            _postServeic = postServeic;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_postServeic.GetAll());
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };

            var updated = _postServeic.Update(post);

            if (updated)
                return Ok(post);

            return NotFound();
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute] Guid postId)
        {
            return Ok(_postServeic.GetPostById(postId));
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post { Id = postRequest.Id, Name = postRequest.Name };

            if (post.Id != Guid.Empty)
                post.Id = Guid.NewGuid();

            _postServeic.GetAll().Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id, Name = post.Name };
            return Created(locationUrl, response);
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public IActionResult Delete([FromRoute] Guid postId)
        {
            var deleted = _postServeic.Delete(postId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
