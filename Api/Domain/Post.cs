using System.ComponentModel.DataAnnotations;

namespace Api.Domain
{
    public class Post
    {
        [Key]
        public System.Guid Id { get; set; }
        public string Name { get; set; }
    }
}