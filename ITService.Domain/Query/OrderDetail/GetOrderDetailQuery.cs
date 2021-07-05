using ITService.Domain.Query.Dto;
using System;

namespace ITService.Domain.Query.OrderDetail
{
    public class GetOrderDetailQuery : IQuery<OrderDetailDto>
    {
        public GetOrderDetailQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
