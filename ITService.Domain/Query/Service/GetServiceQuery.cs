using ITService.Domain.Query.Dto;
using System;

namespace ITService.Domain.Query.Service
{
    public class GetServiceQuery : IQuery<ServiceDto>
    {
        public GetServiceQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
