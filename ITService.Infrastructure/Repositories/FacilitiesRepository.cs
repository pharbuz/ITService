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
    public sealed class FacilitiesRepository : RepositoryBase, IFacilitiesRepository
    {
        public FacilitiesRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<Facility> GetAsync(Guid id)
        {
            return await _context.Facilities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Facility entity)
        {
            _context.Facilities.Remove(entity);
        }

        public async Task AddAsync(Facility entity)
        {
            await _context.Facilities.AddAsync(entity);
        }

        public async Task UpdateAsync(Facility entity)
        {
            _context.Facilities.Update(entity);
        }

        public async Task<FacilityPageResult<Facility>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Facilities
                .Where(o => searchPhrase == null
                            || o.Name.ToLower().Contains(searchPhrase.ToLower())
                            || o.StreetAdress.ToLower().Contains(searchPhrase.ToLower())
                            || o.PostalCode.ToLower().Contains(searchPhrase.ToLower())
                            || o.City.ToLower().Contains(searchPhrase.ToLower())
                            || o.PhoneNumber.ToLower().Contains(searchPhrase.ToLower())
                            || o.OpenedSaturday.ToLower().Contains(searchPhrase.ToLower())
                            || o.OpenedWeek.ToLower().Contains(searchPhrase.ToLower())
                            || o.MapUrl.ToLower().Contains(searchPhrase.ToLower())
                            );
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Facility, object>>>()
                {
                    { nameof(Facility.Name), x => x.Name },
                    { nameof(Facility.StreetAdress), x => x.StreetAdress },
                    { nameof(Facility.PostalCode), x => x.PostalCode },
                    { nameof(Facility.City), x => x.City },
                    { nameof(Facility.PhoneNumber), x => x.PhoneNumber },
                    { nameof(Facility.OpenedSaturday), x => x.OpenedSaturday },
                    { nameof(Facility.OpenedWeek), x => x.OpenedWeek },
                    { nameof(Facility.MapUrl), x => x.MapUrl }
                };

                Expression<Func<Facility, object>> selectedColumn;

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
            return new FacilityPageResult<Facility>(facilities, baseQuery.Count(), pageSize, pageNumber);
        }
    }
}
