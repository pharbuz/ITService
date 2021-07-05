using ITService.Domain.Query.Dto;
using System;

namespace ITService.Domain.Query.Product
{
    public class GetProductQuery : IQuery<ProductDto>
    {
        public GetProductQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
