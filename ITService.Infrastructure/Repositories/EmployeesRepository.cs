using System;
using System.Threading.Tasks;
using ITService.Domain.Entities;
using ITService.Domain.Enums;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using ITService.Domain.Repositories;

namespace ITService.Infrastructure.Repositories
{
    public sealed class EmployeesRepository : RepositoryBase, IEmployeesRepository
    {
        public EmployeesRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<Employee> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeePageResult<Employee>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
