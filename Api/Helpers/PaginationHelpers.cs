using Api.Contracts.V1.Requests.Queries;
using Api.Contracts.V1.Responses;
using Api.Domain;
using Api.Servises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public class PaginationHelpers
    {
        public static object CreatePaginatedResponse<T>(IUriService uriService, PaginationFilter pagination , List<T> response)
        {
            var nextPage = pagination.PageNumber >= 1
                ? uriService.GetAllPostsUri(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize)).ToString()
                : null;

            var previousPage = pagination.PageNumber - 1 >= 1
                ? uriService.GetAllPostsUri(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize)).ToString()
                : null;


            return new PageResponse<T>
            {
                Data = response,
                PageNumber = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = previousPage
            };
        }
    }
}
