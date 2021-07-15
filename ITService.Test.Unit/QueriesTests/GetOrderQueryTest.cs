using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Query.Order;
using ITService.Domain.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ITService.Test.Unit.QueriesTests
{
    public class GetOrderQueryTest
    {
        [Fact]
        public void GetOrder_WhenItsExist()
        {
            using (var sut = new SystemUnderTest())
            {
                var order = sut.CreateOrder(Guid.NewGuid(),Guid.NewGuid(),"status zamówienia",DateTime.Now,"sposób dostawy","Rzeszów",123,DateTime.Now
                    ,DateTime.Now,"status opłaty","321654987","12-345",DateTime.Now,"Sucharskiego","132490876","nie wiem czemu tu jest string");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.OrdersRepository.GetAsync(order.Id).Returns(order);
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var query = new GetOrderQuery(order.Id);
                var queryHandler = new GetOrderQueryHandler(unitOfWorkSubstitute, mapperSubsitute);
                var orderQuery = queryHandler.HandleAsync(query);
                orderQuery.Result.Id.Should().Be(order.Id);
            }
        }
    }
}
