using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Query.Product;
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
    public class GetProductQueryTest
    {
        [Fact]
        public void GetProduct_WhenItsExist()
        {
            using (var sut = new SystemUnderTest())
            {
                var product = sut.CreateProduct(Guid.NewGuid(),"nazwa produktu",345,"url do obrazka",Guid.NewGuid(),"opis produktu",Guid.NewGuid());
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.ProductsRepository.GetAsync(product.Id).Returns(product);
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var query = new GetProductQuery(product.Id);
                var queryHandler = new GetProductQueryHandler(unitOfWorkSubstitute, mapperSubsitute);
                var productQuery = queryHandler.HandleAsync(query);
                productQuery.Result.Id.Should().Be(product.Id);
            }
        }
    }
}
