using AutoMapper;
using FluentAssertions;
using ITService.Domain;
using ITService.Domain.Command.Product;
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
    public class AddProductCommandTest
    {
        [Fact]
        public void AddProduct_ShouldSucces()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddProductCommand
                {
                    Name = "Nowa produkt",
                    CategoryId = Guid.NewGuid(),
                    Description = "opis produktu",
                    Image = "url do obrazka",
                    ManufacturerId = Guid.NewGuid(),
                    Price = 120
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddProductCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsSuccess.Should().Be(true);
            }
        }
        [Fact]
        public void AddProduct_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = new ProductProxy
                {

                };
                var command = new AddProductCommand
                {
                    Name = "Nowa produkt",
                    CategoryId = Guid.NewGuid(),
                    Description = "opis produktu",
                    Image = null,
                    ManufacturerId = Guid.NewGuid(),
                    Price = 120
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapperSubsitute = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new EntityMappingProfile())));
                var handler = new AddProductCommandHandler(unitOfWorkSubstitute, mapperSubsitute);
                var result = handler.HandleAsync(command);
                result.Result.IsFailure.Should().Be(true);
            }
        }
    }
}
