using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Query.User;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ITService.Test.Unit.QueriesTests
{
    public class GetUserQueryTest
    {
        [Fact]
        public void GetUser_WhenItsExist()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = sut.CreateUser(Guid.NewGuid(),"Nazwa użykownika","Hasło","email@email.com",Guid.NewGuid(),"Rzeszów","32-435","Sucharskiego","432543654",DateTime.Now);
                var unitOfWorkSubstitute = Substitute.For<Domain.Repositories.IUnitOfWork>();
                unitOfWorkSubstitute.UsersRepository.GetAsync(user.Id).Returns(user);
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var query = new GetUserQuery(user.Id);
                var queryHandler = new GetUserQueryHandler(unitOfWorkSubstitute, mapperSubsitute);
                var userQuery = queryHandler.HandleAsync(query);
                userQuery.Result.Id.Should().Be(user.Id);
            }
        }
    }
}
