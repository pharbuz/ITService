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
    public sealed class OrderDetailsRepository : RepositoryBase, IOrderDetailsRepository
    {
        public OrderDetailsRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<OrderDetail> GetAsync(Guid id)
        {
            return await _context.OrderDetails
                .Include(o => o.Product)
                .FirstOrDefaultAsync(x => x.OrderId == id);
        }

        public async Task DeleteAsync(OrderDetail entity)
        {
            _context.OrderDetails.Remove(entity);
        }

        public async Task AddAsync(OrderDetail entity)
        {
            await _context.OrderDetails.AddAsync(entity);
        }

        public async Task UpdateAsync(OrderDetail entity)
        {
            _context.OrderDetails.Update(entity);
        }

        public async Task<OrderDetailPageResult<OrderDetail>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection, Guid orderId)
        {
            var baseQuery = _context.OrderDetails
                .Include(o => o.Product)
                .Where(o => (searchPhrase == null
                            || o.Price.ToString().Contains(searchPhrase.ToLower())
                            || o.Quantity.ToString().Contains(searchPhrase.ToLower())
                            ) && o.OrderId == orderId
                            );
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<OrderDetail, object>>>()
                {
                    { nameof(OrderDetail.Price), x => x.Price },
                    { nameof(OrderDetail.Quantity), x => x.Quantity }
                };

                Expression<Func<OrderDetail, object>> selectedColumn;

                if (columnSelectors.Keys.Contains(orderBy))
                {
                    selectedColumn = columnSelectors[orderBy];
                }
                else
                {
                    selectedColumn = columnSelectors["Price"];
                }

                baseQuery = sortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }
            var orders = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new OrderDetailPageResult<OrderDetail>(orders, baseQuery.Count(), pageSize, pageNumber);
        }
    }
}
