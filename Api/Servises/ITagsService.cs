using Api.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Servises
{
    public interface ITagsService
    {
        public Task<List<Tags>> GetAllAsync();
    }
}
