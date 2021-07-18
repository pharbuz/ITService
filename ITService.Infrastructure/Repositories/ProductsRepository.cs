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
    public sealed class ProductsRepository : RepositoryBase, IProductsRepository
    {
        public ProductsRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _context.Products
                .Include(x => x.Manufacturer)
                .Include(x => x.OrderDetails)
                .Include(x => x.ShoppingCarts)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Product entity)
        {
            _context.Products.Remove(entity);
        }

        public async Task AddAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
        }

        public async Task UpdateAsync(Product entity)
        {
            _context.Products.Update(entity);
        }

        public async Task<ProductPageResult<Product>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Products
                .Include(x => x.Manufacturer)
                .Include(x => x.OrderDetails)
                .Include(x => x.ShoppingCarts)
                .Include(x => x.Category)
                .Where(o => searchPhrase == null
                            || o.Name.ToLower().Contains(searchPhrase.ToLower())
                            || o.Price.ToString().Contains(searchPhrase.ToLower())
                            || o.Image.Contains(searchPhrase)
                            || o.Description.ToLower().Contains(searchPhrase.ToLower())
                            );
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Product, object>>>()
                {
                    { nameof(Product.Name), x => x.Name },
                    { nameof(Product.Price), x => x.Price },
                    { nameof(Product.Image), x => x.Image },
                    { nameof(Product.Description), x => x.Description }
                };

                Expression<Func<Product, object>> selectedColumn;

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
            return new ProductPageResult<Product>(orders, baseQuery.Count(), pageSize, pageNumber);
        }
    }
}
