using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Query.Role;
using ITService.Domain.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ITService.Test.Unit.QueriesTests
{
    public class GetRoleQueryTest
    {
        [Fact]
        public void GetRole_WhenItsExist()
        {
            using (var sut = new SystemUnderTest())
            {
                var role = sut.CreateRole(Guid.NewGuid(),"Nazwa roli");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.RolesRepository.GetAsync(role.Id).Returns(role);
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var query = new GetRoleQuery(role.Id);
                var queryHandler = new GetRoleQueryHandler(unitOfWorkSubstitute, mapperSubsitute);
                var roleQuery = queryHandler.HandleAsync(query);
                roleQuery.Result.Id.Should().Be(role.Id);
            }
        }
    }
}
