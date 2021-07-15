using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Query.ShoppingCart;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ITService.Test.Unit.QueriesTests
{
    public class GetShoppingCartQueryTest
    {
        [Fact]
        public void GetShoppingCart_WhenItsExist()
        {
            using (var sut = new SystemUnderTest())
            {
                var shoppingCart = sut.CreateShoppingCart(Guid.NewGuid(),Guid.NewGuid(),Guid.NewGuid(),3);
                var unitOfWorkSubstitute = Substitute.For<Domain.Repositories.IUnitOfWork>();
                unitOfWorkSubstitute.ShoppingCartsRepository.GetAsync(shoppingCart.Id).Returns(shoppingCart);
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var query = new GetShoppingCartQuery(shoppingCart.Id);
                var queryHandler = new GetShoppingCartQueryHandler(unitOfWorkSubstitute, mapperSubsitute);
                var shoppingCartQuery = queryHandler.HandleAsync(query);
                shoppingCartQuery.Result.Id.Should().Be(shoppingCart.Id);
            }
        }
    }
}
