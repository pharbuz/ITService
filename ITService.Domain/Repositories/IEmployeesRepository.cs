using System;
using System.Threading.Tasks;
using ITService.Domain.Entities;
using ITService.Domain.Enums;
using ITService.Domain.Query.Dto.Pagination.PageResults;

namespace ITService.Domain.Repositories
{
    public interface IEmployeesRepository : IRepository<Employee>
    {
        Task<EmployeePageResult<Employee>> SearchAsync(Guid userId, string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
    }
}
