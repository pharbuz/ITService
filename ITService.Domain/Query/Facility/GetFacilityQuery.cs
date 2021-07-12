using ITService.Domain.Query.Dto;
using System;

namespace ITService.Domain.Query.Facility
{
    public sealed class GetFacilityQuery : IQuery<FacilityDto>
    {
        public GetFacilityQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
