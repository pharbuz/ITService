using System;
using System.Threading.Tasks;
using ITService.Domain.Entities;
using ITService.Domain.Enums;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using ITService.Domain.Repositories;

namespace ITService.Infrastructure.Repositories
{
    public sealed class ProductsRepository : RepositoryBase, IProductsRepository
    {
        public ProductsRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<Product> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductPageResult<Product>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
