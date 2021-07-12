using System;
using System.Threading.Tasks;

namespace ITService.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoriesRepository CategoriesRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        IProductsRepository ProductsRepository { get; }
        IRolesRepository RolesRepository { get; }
        IUsersRepository UsersRepository { get; }
        ITokenRepository TokenRepository { get; }
        Task CommitAsync();
    }
}
