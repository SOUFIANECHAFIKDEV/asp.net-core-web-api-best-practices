using Api.Contracts.V1.Requests.Queries;
using System;

namespace Api.Servises
{
    public interface IUriService
    {
        Uri GetPostUri(string postId);
        Uri GetAllPostsUri(PaginationQuery Pagination = null);
    }
}
