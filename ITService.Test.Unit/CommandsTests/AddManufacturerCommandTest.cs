using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Command.Manufacturer;
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
    public class AddManufacturerCommandTest
    {
        [Fact]
        public void AddManufacturer_ShouldSucces()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddManufacturerCommand
                {

                    Name = "Nazwa producenta",

                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddManufacturerCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }
        [Fact]
        public void AddManufacturer_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = new ManufacturerProxy
                {

                };
                var command = new AddManufacturerCommand
                {

                    Name = null,

                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddManufacturerCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
    }
}
