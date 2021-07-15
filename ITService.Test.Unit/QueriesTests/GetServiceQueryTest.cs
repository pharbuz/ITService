using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Query.Service;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ITService.Test.Unit.QueriesTests
{
    public class GetServiceQueryTest
    {
        [Fact]
        public void GetService_WhenItsExist()
        {
            using (var sut = new SystemUnderTest())
            {
                var service = sut.CreateService(Guid.NewGuid(),"Nazwa usługi","Url do obrazka", "opis usługi", 123);
                var unitOfWorkSubstitute = Substitute.For<Domain.Repositories.IUnitOfWork>();
                unitOfWorkSubstitute.ServicesRepository.GetAsync(service.Id).Returns(service);
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var query = new GetServiceQuery(service.Id);
                var queryHandler = new GetServiceQueryHandler(unitOfWorkSubstitute, mapperSubsitute);
                var serviceQuery = queryHandler.HandleAsync(query);
                serviceQuery.Result.Id.Should().Be(service.Id);
            }
        }
    }
}
