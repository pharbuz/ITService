using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Query.Facility;
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
    public class GetFacilityQueryTest
    {
        [Fact]
        public void GetFacility_WhenItsExist()
        {
            using (var sut = new SystemUnderTest())
            {
                var facility = sut.CreateFacility(Guid.NewGuid(),"Nazwa placówki","Sucharskiego","32-123","Rzeszów","098765123","8-14","8-18","Url do strony");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.FacilitiesRepository.GetAsync(facility.Id).Returns(facility);
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var query = new GetFacilityQuery(facility.Id);
                var queryHandler = new GetFacilityQueryHandler(unitOfWorkSubstitute, mapperSubsitute);
                var facilityQuery = queryHandler.HandleAsync(query);
                facilityQuery.Result.Name.Should().Be("Nazwa placówki");
            }
        }
    }
}
