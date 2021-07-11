using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITService.Domain.Entities;
using ITService.Domain.Enums;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using ITService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITService.Infrastructure.Repositories
{
    public sealed class EmployeesRepository : RepositoryBase, IEmployeesRepository
    {
        public EmployeesRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<Employee> GetAsync(Guid id)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Employee entity)
        {
            _context.Employees.Remove(entity);
        }

        public async Task AddAsync(Employee entity)
        {
            _context.Employees.AddAsync(entity);
        }

        public async Task UpdateAsync(Employee entity)
        {
            _context.Employees.Update(entity);
        }

        public async Task<EmployeePageResult<Employee>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Employees
                .Where(o => searchPhrase == null
                            || o.Login.ToLower().Contains(searchPhrase.ToLower())
                            || o.Email.ToLower().Contains(searchPhrase.ToLower())
                            || o.Salary.ToString().Contains(searchPhrase.ToLower())
                            );
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Employee, object>>>()
                {
                    { nameof(Employee.Email), x => x.Email },
                    { nameof(Employee.Login), x => x.Login },
                    { nameof(Employee.Salary), x => x.Salary }
                };

                Expression<Func<Employee, object>> selectedColumn;

                if (columnSelectors.Keys.Contains(orderBy))
                {
                    selectedColumn = columnSelectors[orderBy];
                }
                else
                {
                    selectedColumn = columnSelectors["Login"];
                }

                baseQuery = sortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }
            var orders = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new EmployeePageResult<Employee>(orders, baseQuery.Count(), pageSize, pageNumber);
        }
    }
}
