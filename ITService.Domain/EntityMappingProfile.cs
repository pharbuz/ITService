﻿using AutoMapper;
using ITService.Domain.Command.Contact;
using ITService.Domain.Command.Order;
using ITService.Domain.Command.Role;
using ITService.Domain.Command.Todo;
using ITService.Domain.Command.User;
using ITService.Domain.Entities;
using ITService.Domain.Query.Dto;

namespace ITService.Domain
{
    public class EntityMappingProfile : Profile
    {
        public void CreateMapForContact()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();

            CreateMap<Contact, AddContactCommand>().ReverseMap();
            CreateMap<ContactDto, AddContactCommand>().ReverseMap();

            CreateMap<Contact, EditContactCommand>().ReverseMap();
            CreateMap<ContactDto, EditContactCommand>().ReverseMap();
        }

        public void CreateMapForOrder()
        {
            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<Order, AddOrderCommand>().ReverseMap();
            CreateMap<OrderDto, AddOrderCommand>().ReverseMap();

            CreateMap<Order, EditOrderCommand>().ReverseMap();
            CreateMap<OrderDto, EditOrderCommand>().ReverseMap();
        }

        public void CreateMapForRole()
        {
            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<Role, AddRoleCommand>().ReverseMap();
            CreateMap<RoleDto, AddRoleCommand>().ReverseMap();

            CreateMap<Role, EditRoleCommand>().ReverseMap();
            CreateMap<RoleDto, EditRoleCommand>().ReverseMap();
        }

        public void CreateMapForTodo()
        {
            CreateMap<Todo, TodoDto>().ReverseMap();

            CreateMap<Todo, AddTodoCommand>().ReverseMap();
            CreateMap<TodoDto, AddTodoCommand>().ReverseMap();

            CreateMap<Todo, EditTodoCommand>().ReverseMap();
            CreateMap<TodoDto, EditTodoCommand>().ReverseMap();
        }

        public void CreateMapForUser()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, AddUserCommand>().ReverseMap();
            CreateMap<UserDto, AddUserCommand>().ReverseMap();

            CreateMap<User, EditUserCommand>().ReverseMap();
            CreateMap<UserDto, EditUserCommand>().ReverseMap();
        }

        public EntityMappingProfile()
        {
            CreateMapForContact();
            CreateMapForOrder();
            CreateMapForRole();
            CreateMapForTodo();
            CreateMapForUser();
        }
    }
}
