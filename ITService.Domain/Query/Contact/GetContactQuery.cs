using ITService.Domain.Query.Dto;
using System;

namespace ITService.Domain.Query.Contact
{
    public sealed class GetContactQuery : IQuery<ContactDto>
    {
        public GetContactQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
