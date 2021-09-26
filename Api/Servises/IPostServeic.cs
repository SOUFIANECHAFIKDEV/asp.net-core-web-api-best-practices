using Api.Domain;
using System;
using System.Collections.Generic;

namespace Api.Servises
{
    public interface IPostServeic
    {
        public List<Post> GetAll();
        public Post GetPostById(Guid postId);
        public bool Update(Post postToUpdate);
        public bool Delete(Guid postId);
    }
}