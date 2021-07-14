using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Command.User;
using ITService.Domain.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ITService.Test.Unit
{
    public class DeleteUserCommandTest
    {/*[Fact]
         * nie znam id userów więc nie mam jak sprawdzić
        public void DeletetUser_ShouldSucces()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditUserCommand(Guid.NewGuid());

                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var handler = new DeleteUserCommandHandler(unitOfWorkSubstitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }*/
        [Fact]
        public void DeleteUser_ShouldFailUserDontExist()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteUserCommand(Guid.NewGuid());
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var handler = new DeleteUserCommandHandler(unitOfWorkSubstitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
    }
}
