using System;

namespace ITService.Domain.Command.User
{
    public sealed class AddUserCommand : ICommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid? RoleId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }
}