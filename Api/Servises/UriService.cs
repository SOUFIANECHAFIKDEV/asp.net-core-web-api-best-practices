using Api.Contracts.V1;
using Api.Contracts.V1.Requests.Queries;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace Api.Servises
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetAllPostsUri(PaginationQuery pagination = null)
        {
            var uri = new Uri(_baseUri);

            if (pagination == null)
            {
                return uri;
            }

            var modifierdUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pagination.PageNumber.ToString());
            modifierdUri = QueryHelpers.AddQueryString(modifierdUri, "pageSize", pagination.PageSize.ToString());
            return new Uri(modifierdUri);
        }

        public Uri GetPostUri(string postId)
        {
            return new Uri(_baseUri + ApiRoutes.Posts.Get.Replace("{postId}", postId));
        }
    }
}
