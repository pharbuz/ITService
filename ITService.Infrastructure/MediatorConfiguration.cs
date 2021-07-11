using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ITService.Domain;
using ITService.Domain.Command;
using ITService.Domain.Command.User;
using ITService.Domain.Entities;
using ITService.Domain.Query;

namespace ITService.Infrastructure
{
    public static class MediatorConfiguration
    {
        public static void ConfigureMediator(this ContainerBuilder containerBuilder)
        {
            containerBuilder.Register(context => new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(typeof(EntityMappingProfile));
                })).AsSelf().SingleInstance();

            containerBuilder.Register(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    var config = context.Resolve<MapperConfiguration>();
                    return config.CreateMapper(context.Resolve);
                })
                .As<IMapper>()
                .InstancePerLifetimeScope();

            containerBuilder
                .RegisterType<PasswordHasher<User>>()
                .As<IPasswordHasher<User>>()
                .InstancePerLifetimeScope();

            containerBuilder
                .RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .InstancePerLifetimeScope();

            containerBuilder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            containerBuilder
                .Register(factory =>
                {
                    var lifetimeScope = factory.Resolve<ILifetimeScope>();
                    return new AutofacDependencyResolver(lifetimeScope);
                })
                .As<IDependencyResolver>()
                .InstancePerLifetimeScope();

            var handlersAssembly = typeof(AddUserCommandHandler).Assembly;

            containerBuilder
                .RegisterAssemblyTypes(handlersAssembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            containerBuilder
                .RegisterAssemblyTypes(handlersAssembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .InstancePerLifetimeScope();
        }
    }
}
