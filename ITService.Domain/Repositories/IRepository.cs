using System;
using System.Threading.Tasks;

namespace ITService.Domain.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(Guid id);
        Task DeleteAsync(T entity);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
