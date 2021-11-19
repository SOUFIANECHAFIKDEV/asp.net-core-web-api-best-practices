using Api.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Servises
{
    public interface IPostServeic
    {
        public Task<List<Post>> GetAllAsync(PaginationFilter paginationFilter = null);
        public Task<bool> Create(Post postToCreate);
        public Task<Post> GetPostByIdAsync(Guid postId);
        public Task<bool> UpdateAsync(Post postToUpdate);
        public Task<bool> DeleteAsync(Guid postId);
        public Task<bool> UserOwnsPostAsync(Guid postId, string userId);
    }
}