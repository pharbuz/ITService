using ITService.Domain.Query.Dto;
using System;

namespace ITService.Domain.Query.Role
{
    public class GetRoleQuery : IQuery<RoleDto>
    {
        public GetRoleQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
