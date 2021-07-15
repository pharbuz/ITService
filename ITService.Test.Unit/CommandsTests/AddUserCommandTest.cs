using ITService.Domain.Command.User;
using ITService.Domain.Repositories;
using ITService.Domain;
using ITService.Test.Unit.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using AutoMapper;
using ITService.Domain.Query.Dto;
using ITService.Domain.Entities;

namespace ITService.Test.Unit
{
    public class AddUserCommandTest
    {
        [Fact]
        public void AddUser_ShouldSucces()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddUserCommand
                {
                    Login = "Jan",
                    Password = "Hasło",
                    Street = "Sucharskiego",
                    City = "Rzeszów",
                    Email = "email@gmail.com",
                    PhoneNumber = "123456789",
                    PostalCode = "35-230",
                    RoleId = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddUserCommandHandler(unitOfWorkSubstitute,mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }
        [Fact]
        public void AddUser_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = new UserProxy
                {
                    Login = "Jan",
                    Password = "Hasło",
                    Street = "Sucharskiego",
                    City = "Rzeszów",
                    Email = "email@gmail.com",
                    PostalCode = "35-230",
                    RoleId = new Guid()
                };
                var command = new AddUserCommand
                {
                    Login = "Jan",
                    Password = "Hasło",
                    Street = "Sucharskiego",
                    City = "Rzeszów",
                    Email = "email@gmail.com",
                    PhoneNumber = "123456789",
                    PostalCode = "35-230",
                    RoleId =  new Guid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddUserCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
    }
}
