using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Command.Order;
using ITService.Domain.Repositories;
using ITService.Test.Unit.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ITService.Test.Unit
{
    public class AddOrderCommandTest
    {
        [Fact]
        public void AddOrder_ShouldSucces()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddOrderCommand
                {
                    Carrier = "sposób dostawy",
                    ShippingDate = DateTime.Now,
                    Street = "Sucharskiego",
                    OrderStatus = "Status zamówienia",
                    PaymentStatus = "Status opłaty zamówienia",
                    City = "Rzeszów",
                    OrderDate = DateTime.Now,
                    OrderTotal = 200,
                    PaymentDate = DateTime.Now,
                    PaymentDueDate = DateTime.Now,
                    PhoneNumber = "321654987",
                    PostalCode = "54-321",
                    TrackingNumber ="213456",
                    TransactionId = "czemu tu jest string?",
                    UserId = Guid.NewGuid() 
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddOrderCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }
       
    }
}
