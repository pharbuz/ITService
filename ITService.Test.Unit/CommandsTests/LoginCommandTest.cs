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
    public class LoginCommandTest
    {
        /*
        [Fact]
        //nie znam id userów więc nie mam jak sprawdzić
        public void EditUser_ShouldSucces()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new LoginCommand
                {
                    Login = "Jan",
                    Password = "Hasło",
                    RememberMe = true
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var handler = new LoginCommandHandler(unitOfWorkSubstitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }
        */
        [Xunit.Fact]
        public void LoginUser_ShouldFailValidation()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new LoginCommand
                {
                    Login = "Jan",
                    Password = "Hasło",
                    RememberMe = false
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var handler = new LoginCommandHandler(unitOfWorkSubstitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
    }
}
