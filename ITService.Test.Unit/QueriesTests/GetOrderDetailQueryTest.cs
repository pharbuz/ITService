using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Query.OrderDetail;
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
    public class GetOrderDetailQueryTest
    {
        [Fact]
        public void GetOrderDetail_WhenItsExist()
        {
            using (var sut = new SystemUnderTest())
            {
                var orderDetail = sut.CreateOrderDetail(Guid.NewGuid(), Guid.NewGuid(),Guid.NewGuid(),353,2);
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.OrderDetailsRepository.GetAsync(orderDetail.Id).Returns(orderDetail);
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var query = new GetOrderDetailQuery(orderDetail.Id);
                var queryHandler = new GetOrderDetailQueryHandler(unitOfWorkSubstitute, mapperSubsitute);
                var orderDetailQuery = queryHandler.HandleAsync(query);
                orderDetailQuery.Result.Id.Should().Be(orderDetail.Id);
            }
        }
    }
}
