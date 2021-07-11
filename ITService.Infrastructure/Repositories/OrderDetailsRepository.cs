using System;
using System.Threading.Tasks;
using ITService.Domain.Entities;
using ITService.Domain.Enums;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using ITService.Domain.Repositories;

namespace ITService.Infrastructure.Repositories
{
    public sealed class OrderDetailsRepository : RepositoryBase, IOrderDetailsRepository
    {
        public OrderDetailsRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<OrderDetail> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDetailPageResult<OrderDetail>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
