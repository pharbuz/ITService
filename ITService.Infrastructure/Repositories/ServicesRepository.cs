using System;
using System.Threading.Tasks;
using ITService.Domain.Entities;
using ITService.Domain.Enums;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using ITService.Domain.Repositories;

namespace ITService.Infrastructure.Repositories
{
    public sealed class ServicesRepository : RepositoryBase, IServicesRepository
    {
        public ServicesRepository(ITServiceDBContext context) : base(context)
        {
        }

        public async Task<Service> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Service entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Service entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Service entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ServicePageResult<Service>> SearchAsync(string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection)
        {
            throw new NotImplementedException();
        }
    }
}
