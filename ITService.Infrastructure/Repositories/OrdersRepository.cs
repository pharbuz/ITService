﻿using ITService.Domain.Entities;
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
            var order = await _context.Orders.Include(x => x.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public async Task<OrderPageResult<Order>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Orders
                .Where(o => searchPhrase == null
                            || o.OrderDate.ToString().Contains(searchPhrase.ToLower())
                            || o.ShippingDate.ToString().Contains(searchPhrase.ToLower())
                            || o.OrderTotal.ToString().Contains(searchPhrase.ToLower())
                            || o.TrackingNumber.ToLower().Contains(searchPhrase.ToLower())
                            || o.Carrier.ToLower().Contains(searchPhrase.ToLower())
                            || o.OrderStatus.ToLower().Contains(searchPhrase.ToLower())
                            || o.PaymentStatus.ToLower().Contains(searchPhrase.ToLower())
                            || o.PaymentDate.ToString().Contains(searchPhrase.ToLower())
                            || o.PaymentDueDate.ToString().Contains(searchPhrase.ToLower())
                            || o.TransactionId.ToString().Contains(searchPhrase.ToLower())
                            || o.Street.ToLower().Contains(searchPhrase.ToLower())
                            || o.City.ToLower().Contains(searchPhrase.ToLower())
                            || o.PostalCode.ToLower().Contains(searchPhrase.ToLower())
                            || o.PhoneNumber.ToLower().Contains(searchPhrase.ToLower())
                            );
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Order, object>>>()
                {
                    {nameof(Order.OrderDate), o => o.OrderDate},
                    {nameof(Order.ShippingDate), o => o.ShippingDate},
                    {nameof(Order.OrderTotal), o => o.OrderTotal},
                    {nameof(Order.TrackingNumber), o => o.TrackingNumber},
                    {nameof(Order.Carrier), o => o.Carrier},
                    {nameof(Order.OrderStatus), o => o.OrderStatus},
                    {nameof(Order.PaymentStatus), o => o.PaymentStatus},
                    {nameof(Order.PaymentDate), o => o.PaymentDate},
                    {nameof(Order.PaymentDueDate), o => o.PaymentDueDate},
                    {nameof(Order.TransactionId), o => o.TransactionId},
                    {nameof(Order.Street), o => o.Street},
                    {nameof(Order.City), o => o.City},
                    {nameof(Order.PostalCode), o => o.PostalCode},
                    {nameof(Order.PhoneNumber), o => o.PhoneNumber},
                };

                Expression<Func<Order, object>> selectedColumn;

                if (columnSelectors.Keys.Contains(orderBy))
                {
                    selectedColumn = columnSelectors[orderBy];
                }
                else
                {
                    selectedColumn = columnSelectors["OrderDate"];
                }

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
