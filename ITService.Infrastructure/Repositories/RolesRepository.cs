using ITService.Domain.Entities;
using ITService.Domain.Enums;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using ITService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ITService.Infrastructure.Repositories
{
    public sealed class RolesRepository : RepositoryBase, IRolesRepository
    {
        public RolesRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public async Task DeleteAsync(Role role)
        {
            _context.Roles.Remove(role);
        }

        public async Task<Role> GetAsync(Guid id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            return role;
        }

        public async Task<RolePageResult<Role>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Roles
                .Where(r => searchPhrase == null
                            || r.Name.ToLower().Contains(searchPhrase.ToLower()));
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Role, object>>>()
                {
                    { nameof(Role.Name), r => r.Name },
                };

                var selectedColumn = columnSelectors[orderBy];

                baseQuery = sortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }
            var orders = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new RolePageResult<Role>(orders, baseQuery.Count(), pageSize, pageNumber);
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
        }
    }
}
