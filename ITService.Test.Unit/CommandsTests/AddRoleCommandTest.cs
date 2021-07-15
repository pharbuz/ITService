using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Command.Role;
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
    public class AddRoleCommandTest
    {
        [Fact]
        public void AddRole_ShouldSucces()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddRoleCommand
                {
                    Name = "Nowa rola"                    
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddRoleCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }
        [Fact]
        public void AddRole_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {

                var command = new AddRoleCommand
                {
                    Name = null
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddRoleCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
    }
}
