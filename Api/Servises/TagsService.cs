using Api.Data;
using Api.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Servises
{
    public class TagsService : ITagsService
    {
        private readonly DataContext _dataContext;

        public TagsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Tags>> GetAllAsync()
        {
            return await _dataContext.Tags.ToListAsync();
        }
    }
}
