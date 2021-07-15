using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Command.ShoppingCart;
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
    public class AddShoppingCartCommandTest
    {
        [Fact]
        public void AddShoppingCart_ShouldSucces()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddShoppingCartCommand
                {
                    Count = 3,
                    ProductId = Guid.NewGuid(),
                    UserId = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddShoppingCartCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }
        [Fact]
        public void AddShoppingCart_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {

                var command = new AddShoppingCartCommand
                {
                    Count = -3,
                    ProductId = Guid.NewGuid(),
                    UserId = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddShoppingCartCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
    }
}
