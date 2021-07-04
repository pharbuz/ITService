using System;
using ITService.Domain.Entities;
using ITService.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITService.Domain.Query.Dto.Pagination.PageResults;

namespace ITService.Domain.Repositories
{
    public interface IRolesRepository
    {
        Task<Role> GetAsync(Guid id);
        Task DeleteAsync(Role role);
        Task<RolePageResult<Role>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
    }
}
