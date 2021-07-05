using System;
using ITService.Domain.Entities;
using ITService.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITService.Domain.Query.Dto.Pagination.PageResults;

namespace ITService.Domain.Repositories
{
    public interface IRolesRepository : IRepository<Role>
    {
        Task<RolePageResult<Role>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
    }
}
