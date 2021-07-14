using System;

namespace ITService.Domain.Command.User
{
    public sealed class EditUserCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid? RoleId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
