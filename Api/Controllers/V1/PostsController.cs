using Api.Cache;
using Api.Contracts.V1;
using Api.Contracts.V1.Requests;
using Api.Contracts.V1.Requests.Queries;
using Api.Contracts.V1.Responses;
using Api.Domain;
using Api.Extensions;
using Api.Helpers;
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
    [Produces("application/json")]
    public class PostsController : Controller
    {
        private readonly IPostServeic _postServeic;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PostsController(IPostServeic postServeic, IMapper mapper, IUriService uriService)
        {
            _postServeic = postServeic;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Return all the tags in the system
        /// </summary>
        /// <response code="200">Returns all the posts in the system</response>
        [HttpGet(ApiRoutes.Posts.GetAll)]
        [Cached(600)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var posts = await _postServeic.GetAllAsync(pagination);
            var postsResponse = _mapper.Map<List<PostResponse>>(posts);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PageResponse<PostResponse>(postsResponse));
            }

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, pagination, postsResponse);

            return Ok(paginationResponse);
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
                return Ok(new Response<PostResponse>(_mapper.Map<PostResponse>(post)));


            return NotFound();
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        [Cached(600)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postServeic.GetPostByIdAsync(postId);

            if (post == null)
                return NotFound();

            return Ok(new Response<PostResponse>(_mapper.Map<PostResponse>(post)));
        }

        /// <summary>
        /// Creates a post in the system
        /// </summary>
        /// <remarks>
        ///     Sample **Request**
        ///     
        ///         Post /Api/V1/Tags
        ///         {
        ///             "Name" : "Some Name"
        ///         }
        /// </remarks>
        ///<response code="201">Creates a post in the system</response>
        /// <response code="400">Unable to create the tag to validation error</response>
        [HttpPost(ApiRoutes.Posts.Create)]
        [ProducesResponseType(typeof(PostResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
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

            var created = await _postServeic.Create(post);

            if (!created)
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to create tag" } } });
            }

            //var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            //var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var postLocation = _uriService.GetPostUri(post.Id.ToString());
            return Created(postLocation, new Response<PostResponse>(_mapper.Map<PostResponse>(post)));
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
