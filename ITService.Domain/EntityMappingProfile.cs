using AutoMapper;
using ITService.Domain.Command.Category;
using ITService.Domain.Command.Facility;
using ITService.Domain.Command.Manufacturer;
using ITService.Domain.Command.Order;
using ITService.Domain.Command.OrderDetail;
using ITService.Domain.Command.Product;
using ITService.Domain.Command.Role;
using ITService.Domain.Command.Service;
using ITService.Domain.Command.ShoppingCart;
using ITService.Domain.Command.User;
using ITService.Domain.Entities;
using ITService.Domain.Query.Dto;

namespace ITService.Domain
{
    public class EntityMappingProfile : Profile
    {
        public void CreateMapForCategory()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Category, AddCategoryCommand>().ReverseMap();
            CreateMap<CategoryDto, AddCategoryCommand>().ReverseMap();

            CreateMap<Category, EditCategoryCommand>().ReverseMap();
            CreateMap<CategoryDto, EditCategoryCommand>().ReverseMap();
        }

        public void CreateMapFacility()
        {
            CreateMap<Facility, FacilityDto>().ReverseMap();

            CreateMap<Facility, AddFacilityCommand>().ReverseMap();
            CreateMap<FacilityDto, AddFacilityCommand>().ReverseMap();

            CreateMap<Facility, EditFacilityCommand>().ReverseMap();
            CreateMap<FacilityDto, EditFacilityCommand>().ReverseMap();
        }

        public void CreateMapManufacturer()
        {
            CreateMap<Manufacturer, ManufacturerDto>().ReverseMap();

            CreateMap<Manufacturer, AddManufacturerCommand>().ReverseMap();
            CreateMap<ManufacturerDto, AddManufacturerCommand>().ReverseMap();

            CreateMap<Manufacturer, EditManufacturerCommand>().ReverseMap();
            CreateMap<ManufacturerDto, EditManufacturerCommand>().ReverseMap();
        }

        public void CreateMapForOrder()
        {
            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<Order, AddOrderCommand>().ReverseMap();
            CreateMap<OrderDto, AddOrderCommand>().ReverseMap();

            CreateMap<Order, EditOrderCommand>().ReverseMap();
            CreateMap<OrderDto, EditOrderCommand>().ReverseMap();
        }

        public void CreateMapForOrderDetail()
        {
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();

            CreateMap<OrderDetail, AddOrderDetailCommand>().ReverseMap();
            CreateMap<OrderDetailDto, AddOrderDetailCommand>().ReverseMap();

            CreateMap<OrderDetail, EditOrderDetailCommand>().ReverseMap();
            CreateMap<OrderDetailDto, EditOrderDetailCommand>().ReverseMap();
        }

        public void CreateMapForProduct()
        {
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Product, AddProductCommand>().ReverseMap();
            CreateMap<ProductDto, AddProductCommand>().ReverseMap();

            CreateMap<Product, EditProductCommand>().ReverseMap();
            CreateMap<ProductDto, EditProductCommand>().ReverseMap();
        }

        public void CreateMapForRole()
        {
            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<Role, AddRoleCommand>().ReverseMap();
            CreateMap<RoleDto, AddRoleCommand>().ReverseMap();

            CreateMap<Role, EditRoleCommand>().ReverseMap();
            CreateMap<RoleDto, EditRoleCommand>().ReverseMap();
        }

        public void CreateMapForService()
        {
            CreateMap<Service, ServiceDto>().ReverseMap();

            CreateMap<Service, AddServiceCommand>().ReverseMap();
            CreateMap<ServiceDto, AddServiceCommand>().ReverseMap();

            CreateMap<Service, EditServiceCommand>().ReverseMap();
            CreateMap<ServiceDto, EditServiceCommand>().ReverseMap();
        }

        public void CreateMapForShoppingCart()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();

            CreateMap<ShoppingCart, AddShoppingCartCommand>().ReverseMap();
            CreateMap<ShoppingCartDto, AddShoppingCartCommand>().ReverseMap();

            CreateMap<ShoppingCart, EditShoppingCartCommand>().ReverseMap();
            CreateMap<ShoppingCartDto, EditShoppingCartCommand>().ReverseMap();
        }

        public void CreateMapForUser()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, AddUserCommand>().ReverseMap();
            CreateMap<UserDto, AddUserCommand>().ReverseMap();

            CreateMap<User, EditUserDetailsCommand>().ReverseMap();
            CreateMap<UserDto, EditUserDetailsCommand>().ReverseMap();
        }

        public EntityMappingProfile()
        {
            CreateMapForCategory();
            CreateMapFacility();
            CreateMapManufacturer();
            CreateMapForOrder();
            CreateMapForOrderDetail();
            CreateMapForProduct();
            CreateMapForRole();
            CreateMapForService();
            CreateMapForShoppingCart();
            CreateMapForUser();
        }
    }
}
