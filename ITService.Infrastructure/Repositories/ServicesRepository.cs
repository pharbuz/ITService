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
    public sealed class ServicesRepository : RepositoryBase, IServicesRepository
    {
        public ServicesRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<Service> GetAsync(Guid id)
        {
            return await _context.Services.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Service entity)
        {
            _context.Services.Remove(entity);
        }

        public async Task AddAsync(Service entity)
        {
            await _context.Services.AddAsync(entity);
        }

        public async Task UpdateAsync(Service entity)
        {
            _context.Services.Update(entity);
        }

        public async Task<ServicePageResult<Service>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Services
                .Where(o => searchPhrase == null
                            || o.Name.ToLower().Contains(searchPhrase.ToLower())
                            || o.Image.Contains(searchPhrase)
                            || o.EstimatedServicePrice.ToString().Contains(searchPhrase.ToLower())
                            || o.Description.ToLower().Contains(searchPhrase.ToLower())
                            );
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Service, object>>>()
                {
                    { nameof(Service.Name), x => x.Name },
                    { nameof(Service.Image), x => x.Image },
                    { nameof(Service.EstimatedServicePrice), x => x.EstimatedServicePrice },
                    { nameof(Service.Description), x => x.Description }
                };

                Expression<Func<Service, object>> selectedColumn;

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
            return new ServicePageResult<Service>(orders, baseQuery.Count(), pageSize, pageNumber);
        }
    }
}
