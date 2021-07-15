using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Command.Service;
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
    public class AddServiceCommandTest
    {
        [Fact]
        public void AddService_ShouldSucces()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddServiceCommand
                {
                    Name = "Nowa usługa",
                    EstimatedServicePrice =300,
                    Description = "opis usługi",
                    Image = "url od obrazka"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddServiceCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }
        [Fact]
        public void AddService_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {

                var command = new AddServiceCommand
                {
                    Name = "Nowa usługa",
                    EstimatedServicePrice = -300,
                    Description = "opis usługi",
                    Image = "url od obrazka"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddServiceCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
    }
}
