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
    public sealed class ShoppingCartsRepository : RepositoryBase, IShoppingCartsRepository
    {
        public ShoppingCartsRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<ShoppingCart> GetAsync(Guid id)
        {
            return await _context.ShoppingCarts
                .Include(x => x.User)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(ShoppingCart entity)
        {
            _context.ShoppingCarts.Remove(entity);
        }

        public async Task AddAsync(ShoppingCart entity)
        {
            await _context.ShoppingCarts.AddAsync(entity);
        }

        public async Task UpdateAsync(ShoppingCart entity)
        {
            _context.ShoppingCarts.Update(entity);
        }

        public async Task<ShoppingCartPageResult<ShoppingCart>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection, Guid userId)
        {
            var baseQuery = _context.ShoppingCarts
                .Include(x => x.User)
                .Include(x => x.Product)
                .Where(o => (searchPhrase == null
                            || o.Count.ToString().Contains(searchPhrase.ToLower()))
                && o.UserId == userId
                );
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<ShoppingCart, object>>>()
                {
                    { nameof(ShoppingCart.Count), x => x.Count }
                };

                Expression<Func<ShoppingCart, object>> selectedColumn;

                if (columnSelectors.Keys.Contains(orderBy))
                {
                    selectedColumn = columnSelectors[orderBy];
                }
                else
                {
                    selectedColumn = columnSelectors["Count"];
                }

                baseQuery = sortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }
            var orders = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return new ShoppingCartPageResult<ShoppingCart>(orders, baseQuery.Count(), pageSize, pageNumber);
        }
    }
}
