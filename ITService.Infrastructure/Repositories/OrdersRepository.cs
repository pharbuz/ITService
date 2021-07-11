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
    public sealed class OrdersRepository : RepositoryBase, IOrdersRepository
    {
        public OrdersRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task<Order> GetAsync(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public async Task<OrderPageResult<Order>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Orders
                .Where(o => searchPhrase == null
                            || o.Amount.ToString().Contains(searchPhrase.ToLower())
                            || o.OrderStatus.ToLower().Contains(searchPhrase.ToLower()));
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Order, object>>>()
                {
                    { nameof(Order.Amount), o => o.Amount },
                    { nameof(Order.OrderStatus), o => o.OrderStatus },
                    { nameof(Order.OrderDate), o => o.OrderDate }
                };

                var selectedColumn = columnSelectors[orderBy];

                baseQuery = sortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }
            var orders = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new OrderPageResult<Order>(orders, baseQuery.Count(), pageSize, pageNumber);
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}
