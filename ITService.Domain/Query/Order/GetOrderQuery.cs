using ITService.Domain.Query.Dto;
using System;

namespace ITService.Domain.Query.Order
{
    public sealed class GetOrderQuery : IQuery<OrderDto>
    {
        public GetOrderQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
