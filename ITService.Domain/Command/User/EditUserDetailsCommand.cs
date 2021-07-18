using System;

namespace ITService.Domain.Command.User
{
    public sealed class EditUserDetailsCommand : ICommand
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }
}
