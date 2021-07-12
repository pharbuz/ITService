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
    public sealed class ManufacturersRepository : RepositoryBase, IManufacturersRepository
    {
        public ManufacturersRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<Manufacturer> GetAsync(Guid id)
        {
            return await _context.Manufacturers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Manufacturer entity)
        {
            _context.Manufacturers.Remove(entity);
        }

        public async Task AddAsync(Manufacturer entity)
        {
            await _context.Manufacturers.AddAsync(entity);
        }

        public async Task UpdateAsync(Manufacturer entity)
        {
            _context.Manufacturers.Update(entity);
        }

        public async Task<ManufacturerPageResult<Manufacturer>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Manufacturers
                .Where(o => searchPhrase == null
                            || o.Name.ToLower().Contains(searchPhrase.ToLower())
                            );
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Manufacturer, object>>>()
                {
                    { nameof(Manufacturer.Name), x => x.Name }
                };

                Expression<Func<Manufacturer, object>> selectedColumn;

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
            var facilities = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new ManufacturerPageResult<Manufacturer>(facilities, baseQuery.Count(), pageSize, pageNumber);
        }
    }
}
