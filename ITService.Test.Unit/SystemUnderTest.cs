using ITService.Test.Unit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITService.Test.Unit
{
    public class SystemUnderTest : IDisposable
    {
        public void Dispose() { }
        public UserProxy CreateUser(Guid id,string login,string password,string email,Guid? roleId,string city,string postalCode,string street,string phoneNumber,DateTime? lockoutEnd)
        {
            var user = new UserProxy
            {
                Id = id,
                City = city,
                Email = email,
                LockoutEnd = lockoutEnd,
                Login = login,
                Password = password,
                PostalCode = postalCode,
                PhoneNumber = phoneNumber,
                RoleId = roleId,
                Street = street
            };
           
            return user;
        }
        public CategoryProxy CreteCategory(Guid  id, String name)
        {
            var category = new CategoryProxy
            {
                Id = id,
                Name = name
            };

            return category;
        }
        public FacilityProxy CreateFacility(Guid id,string name,string streetAdress,string postalCode,string city,string phoneNumber,string openedSaturday,string openedWeek,string mapUrl)
        {
            var facility = new FacilityProxy
            {
                Id = id,
                City = city,
                OpenedWeek = openedWeek,
                StreetAdress = streetAdress,
                OpenedSaturday = openedSaturday,
                MapUrl = mapUrl,
                Name = name,
                PhoneNumber = phoneNumber,
                PostalCode = postalCode
            };
            return facility;
        }
        public ManufacturerProxy CreateManufacturer(Guid id, string name)
        {
            var manufacturer = new ManufacturerProxy
            {
                Id = id,
                Name = name
            };
            return manufacturer;
        }
        public OrderProxy CreateOrder(Guid id,Guid userId,string orderStatus,DateTime orderDate,string carrier,string city,double orderTotal,DateTime paymentDate,
            DateTime paymentDueDate,string paymentStatus,string phoneNumber,string postalCode,DateTime shippingDate,string street,string trackingNumber,string transactionId)
        {
            var order = new OrderProxy
            {
                Id = id,
                OrderStatus = orderStatus,
                OrderDate = orderDate,
                Carrier = carrier,
                City = city,
                OrderTotal = orderTotal,
                PaymentDate = paymentDate,
                PaymentDueDate = paymentDueDate,
                PaymentStatus = paymentStatus,
                PhoneNumber = phoneNumber,
                PostalCode = postalCode,
                ShippingDate = shippingDate,
                Street = street,
                TrackingNumber = trackingNumber,
                TransactionId = transactionId
            };
            return order;
        }
        public OrderDetailProxy CreateOrderDetail(Guid id,Guid orderId,Guid productId,decimal price,int quantity)
        {
            var orderdetail = new OrderDetailProxy
            {
                Id = id,
                OrderId = orderId,
                ProductId = productId,
                Price = price,
                Quantity = quantity
            };
            return orderdetail;
        }
        public ProductProxy CreateProduct(Guid id,string name,decimal price,string image,Guid categoryId,string description,Guid manufacturerId)
        {
            var product = new ProductProxy
            {
                Id = id,
                Name = name,
                Price = price,
                Image = image,
                CategoryId = categoryId,
                Description = description,
                ManufacturerId = manufacturerId
            };
            return product;
        }
        public RoleProxy CreateRole(Guid id, string name)
        {
            var role = new RoleProxy
            {
                Id = id,
                Name = name
            };
            return role;
        }
        public ServiceProxy CreateService(Guid id, string name,string image,string description,double estimatedServicePrice)
        {
            var service = new ServiceProxy
            {
                Id = id,
                Name = name,
                Image = image,
                Description = description,
                EstimatedServicePrice = estimatedServicePrice
            };
            return service;
        }
        public ShoppingCartProxy CreateShoppingCart(Guid id,Guid userId,Guid productId,int count)
        {
            var shoppingcard = new ShoppingCartProxy
            {
                Id = id,
                UserId = userId,
                ProductId = productId,
                Count = count
            };
            return shoppingcard;
        }
    }
}
