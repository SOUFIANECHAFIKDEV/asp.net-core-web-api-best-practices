using Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Servises
{
    public class PostServeic : IPostServeic
    {
        private List<Post> _posts;

        public PostServeic()
        {
            _posts = new List<Post>();

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    _posts.Add(new Post { Id = Guid.NewGuid(), Name = $"Post Name {i + 1}" });
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public List<Post> GetAll()
        {
            return _posts.ToList();
        }

        public Post GetPostById(Guid postId)
        {
            return _posts.SingleOrDefault(x => x.Id == postId);
        }

        public bool Update(Post postToUpdate)
        {
            var exists = GetPostById(postToUpdate.Id) != null;

            if (!exists)
                return false;

            var index = _posts.FindIndex(x => x.Id == postToUpdate.Id);
            _posts[index] = postToUpdate;
            return true;
        }
        
        public bool Delete(Guid postId)
        {
            var post = GetPostById(postId);

            if (post == null)
                return false;

            _posts.Remove(post);
            return true;
        }
    }
}
