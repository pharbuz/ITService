using ITService.Domain.Query.Dto;
using System;

namespace ITService.Domain.Query.Employee
{
    public sealed class GetEmployeeQuery : IQuery<EmployeeDto>
    {
        public GetEmployeeQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
