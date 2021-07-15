using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Command.Category;
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
    public class AddCategoryCommandTest
    {
        [Fact]
        public void AddCategory_ShouldSucces()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddCategoryCommand
                {
                    Name = "Nowa kategoria"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddCategoryCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }
        [Fact]
        public void AddCategory_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {

                var command = new AddCategoryCommand
                {
                    Name = null
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddCategoryCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
    }
}
