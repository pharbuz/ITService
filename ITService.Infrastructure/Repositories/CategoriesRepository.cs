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
    public sealed class CategoriesRepository : RepositoryBase, ICategoriesRepository
    {
        public CategoriesRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<Category> GetAsync(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Category entity)
        {
            _context.Categories.Remove(entity);
        }

        public async Task AddAsync(Category entity)
        {
            await _context.Categories.AddAsync(entity);
        }

        public async Task UpdateAsync(Category entity)
        {
            _context.Categories.Update(entity);
        }

        public async Task<CategoryPageResult<Category>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Categories
                .Where(o => searchPhrase == null
                            || o.Name.ToLower().Contains(searchPhrase.ToLower()));
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Category, object>>>()
                {
                    { nameof(Category.Name), x => x.Name }
                };

                Expression<Func<Category, object>> selectedColumn;

                if (columnSelectors.Keys.Contains(orderBy))
                {
                    selectedColumn = columnSelectors[orderBy];
                }
                else
                {
                    selectedColumn = columnSelectors["Name"];
                }

                baseQuery = sortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }
            var orders = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new CategoryPageResult<Category>(orders, baseQuery.Count(), pageSize, pageNumber);
        }
    }
}
