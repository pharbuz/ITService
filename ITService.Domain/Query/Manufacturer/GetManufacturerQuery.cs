using ITService.Domain.Query.Dto;
using System;

namespace ITService.Domain.Query.Manufacturer
{
    public sealed class GetManufacturerQuery : IQuery<ManufacturerDto>
    {
        public GetManufacturerQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
