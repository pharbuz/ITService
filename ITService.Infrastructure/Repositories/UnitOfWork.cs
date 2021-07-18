using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using ITService.Domain.Entities;
using ITService.Domain.Query.Dto.Auth;
using ITService.Domain.Repositories;

namespace ITService.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ITServiceDBContext _context;

        public UnitOfWork(ITServiceDBContext context, IPasswordHasher<User> hasher, JwtOptions jwtOptions, IHttpContextAccessor contextAccessor, IDistributedCache distributedCache)
        {
            _context = context;
            CategoriesRepository = new CategoriesRepository(context);
            FacilitiesRepository = new FacilitiesRepository(context);
            ManufacturersRepository = new ManufacturersRepository(context);
            OrdersRepository = new OrdersRepository(context);
            OrderDetailsRepository = new OrderDetailsRepository(context);
            ProductsRepository = new ProductsRepository(context);
            RolesRepository = new RolesRepository(context);
            ServicesRepository = new ServicesRepository(context);
            ShoppingCartsRepository = new ShoppingCartsRepository(context);
            TokenRepository = new TokenRepository(contextAccessor, jwtOptions, distributedCache);
            UsersRepository = new UsersRepository(context, hasher, jwtOptions, contextAccessor, TokenRepository);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ICategoriesRepository CategoriesRepository { get; }
        public IFacilitiesRepository FacilitiesRepository { get; }
        public IManufacturersRepository ManufacturersRepository { get; }
        public IOrdersRepository OrdersRepository { get; }
        public IOrderDetailsRepository OrderDetailsRepository { get; }
        public IProductsRepository ProductsRepository { get; }
        public IRolesRepository RolesRepository { get; }
        public IServicesRepository ServicesRepository { get; }
        public IShoppingCartsRepository ShoppingCartsRepository { get; }
        public IUsersRepository UsersRepository { get; }
        public ITokenRepository TokenRepository { get; }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
