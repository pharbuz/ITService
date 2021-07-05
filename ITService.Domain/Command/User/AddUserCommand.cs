﻿using System;

namespace ITService.Domain.Command.User
{
    public sealed class AddUserCommand : ICommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid? RoleId { get; set; }
    }
}