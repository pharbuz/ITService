using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Command.User;
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
    public class EditUserCommandTest
    {
        /*[Fact]
         * nie znam id userów więc nie mam jak sprawdzić
        public void EditUser_ShouldSucces()
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
                    PhoneNumber = "123456789",
                    PostalCode = "35-230",
                    RoleId = Guid.NewGuid()
                };
                var command = new EditUserCommand
                {
                    Id = Guid.NewGuid(),
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
                var handler = new EditUserCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }*/
        [Fact]
        public void EditUser_ShouldFailValidation()
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
                var command = new EditUserCommand
                {
                    Id = Guid.NewGuid(),
                    Login = "Jan",
                    Password = "Hasło",
                    Street = "Sucharskiego",
                    City = "Rzeszów",
                    Email = "email@gmail.com",
                    PhoneNumber = "123456789",
                    PostalCode = "35-230",
                    RoleId = new Guid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new EditUserCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
        [Fact]
        public void EditUser_ShouldFailUserDontExist()
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
                    PhoneNumber = "123456789",
                    PostalCode = "35-230",
                    RoleId = Guid.NewGuid()
                };
                var command = new EditUserCommand
                {
                    Id = Guid.NewGuid(),
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
                var handler = new EditUserCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
    }
}
