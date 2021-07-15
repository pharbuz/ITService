using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Entities;
using ITService.Domain.Query.Category;
using ITService.Domain.Repositories;
using ITService.Test.Unit.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ITService.Test.Unit.QueriesTests
{
    public class GetCategoryQueryTest
    {
        [Fact]
        public void GetCategory_WhenItsExist()
        {
            using (var sut = new SystemUnderTest())
            {
                var category = sut.CreteCategory(Guid.NewGuid(),"Nazwa kategorii");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.CategoriesRepository.GetAsync(category.Id).Returns(category);
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var query = new GetCategoryQuery(category.Id);
                var queryHandler = new GetCategoryQueryHandler(unitOfWorkSubstitute,mapperSubsitute);
                var categoryQuery = queryHandler.HandleAsync(query);
                categoryQuery.Result.Name.Should().Be("Nazwa kategorii");
            }
        }
    }
}