using System;
using System.Threading.Tasks;

namespace ITService.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoriesRepository CategoriesRepository { get; }
        IFacilitiesRepository FacilitiesRepository { get; }
        IManufacturersRepository ManufacturersRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        IProductsRepository ProductsRepository { get; }
        IRolesRepository RolesRepository { get; }
        IServicesRepository ServicesRepository { get; }
        IShoppingCartsRepository ShoppingCartsRepository { get; }
        IUsersRepository UsersRepository { get; }
        ITokenRepository TokenRepository { get; }
        Task CommitAsync();
    }
}
