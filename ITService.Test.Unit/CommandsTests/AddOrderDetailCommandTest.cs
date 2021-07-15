using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Command.OrderDetail;
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
    public class AddOrderDetailCommandTest
    {
        [Fact]
        public void AddOrderDetail_ShouldSucces()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddOrderDetailCommand
                {
                    Price = 21,
                    Quantity = 5,
                    OrderId = Guid.NewGuid(),
                    ProductId = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddOrderDetailCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }
        [Fact]
        public void AddOrderDetail_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = new OrderDetailProxy
                {

                };
                var command = new AddOrderDetailCommand
                {
                    Price = 21,
                    Quantity = -3,
                    OrderId = Guid.NewGuid(),
                    ProductId = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddOrderDetailCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
    }
}
