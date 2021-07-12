using ITService.Domain.Query.Dto;
using System;

namespace ITService.Domain.Query.ShoppingCart
{
    public sealed class GetShoppingCartQuery : IQuery<ShoppingCartDto>
    {
        public GetShoppingCartQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
