using ITService.Domain.Entities;
using ITService.Domain.Enums;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using ITService.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace ITService.Infrastructure.Repositories
{
    public sealed class CategoriesRepository : RepositoryBase, ICategoriesRepository
    {
        public CategoriesRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<Category> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryPageResult<Category>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
