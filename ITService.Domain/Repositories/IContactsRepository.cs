using System;
using ITService.Domain.Entities;
using ITService.Domain.Enums;
using System.Threading.Tasks;
using ITService.Domain.Query.Dto.Pagination.PageResults;

namespace ITService.Domain.Repositories
{
    public interface IContactsRepository
    {
        Task<Contact> GetAsync(Guid id);
        Task DeleteAsync(Contact contact);
        Task<ContactPageResult<Contact>> SearchAsync(Guid userId, string searchPhrase, int pageNumber, int pageSize, string orderBy, SortDirection sortDirection);
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
    }
}
