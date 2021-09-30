using Api.Data;
using Api.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Servises
{
    public class PostServeic : IPostServeic
    {
        private readonly DataContext _dataContext;

        public PostServeic(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _dataContext.Post.Include(x => x.Tags).ToListAsync();
        }

        public async Task<bool> Create(Post postToCreate)
        {
            await _dataContext.Post.AddAsync(postToCreate);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return await _dataContext.Post.SingleOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<bool> UpdateAsync(Post postToUpdate)
        {
            _dataContext.Post.Update(postToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(Guid postId)
        {
            var post = await GetPostByIdAsync(postId);

            if (post == null)
                return false;

            _dataContext.Post.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UserOwnsPostAsync(Guid postId, string userId)
        {
            var post = await _dataContext.Post.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);

            if (post == null)
            {
                return false;
            }

            if (post.UserId != userId)
            {
                return false;
            }

            return true;
        }
    }
}
