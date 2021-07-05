﻿using System;

namespace ITService.Domain.Command.Role
{
    public sealed class AddRoleCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}