using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Query.Manufacturer;
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
    public class GetManufacturerQueryTest
    {
        [Fact]
        public void GetManufacturer_WhenItsExist()
        {
            using (var sut = new SystemUnderTest())
            {
                var manufacturer = sut.CreateManufacturer(Guid.NewGuid(), "Nazwa placówki");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.ManufacturersRepository.GetAsync(manufacturer.Id).Returns(manufacturer);
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var query = new GetManufacturerQuery(manufacturer.Id);
                var queryHandler = new GetManufacturerQueryHandler(unitOfWorkSubstitute, mapperSubsitute);
                var manufacturerQuery = queryHandler.HandleAsync(query);
                manufacturerQuery.Result.Name.Should().Be("Nazwa placówki");
            }
        }
    }
}
