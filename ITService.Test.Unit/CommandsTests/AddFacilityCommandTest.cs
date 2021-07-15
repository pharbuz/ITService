using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Command.Facility;
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
    public class AddFacilityCommandTest
    {
        [Fact]
        public void AddFacility_ShouldSucces()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddFacilityCommand
                {
                    City = "Rzeszów",
                    OpenedWeek = "pn-nd",
                    StreetAdress ="Sucharskiego",
                    OpenedSaturday ="tak",
                    MapUrl = "www.nazwastrony.pl",
                    Name = "Nazwa placówki",
                    PhoneNumber = "987654321",
                    PostalCode = "12-234"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddFacilityCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }
        [Fact]
        public void AddFacility_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {

                var command = new AddFacilityCommand
                {
                    City = "Rzeszów",
                    OpenedWeek = "pn-nd",
                    StreetAdress = "Sucharskiego",
                    OpenedSaturday = null,
                    MapUrl = "www.nazwastrony.pl",
                    Name = "Nazwa placówki",
                    PhoneNumber = "987654321",
                    PostalCode = "12-234"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddFacilityCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
    }
}
