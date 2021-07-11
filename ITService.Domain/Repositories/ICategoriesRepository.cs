using System;
using ITService.Domain.Entities;
using ITService.Domain.Enums;
using System.Threading.Tasks;
using ITService.Domain.Query.Dto.Pagination.PageResults;

namespace ITService.Domain.Repositories
{
    public interface ICategoriesRepository : IRepository<Category>
    {
        Task<CategoryPageResult<Category>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
    }
}
