using ITService.Domain.Query.Dto;
using System;

namespace ITService.Domain.Query.Category
{
    public sealed class GetCategoryQuery : IQuery<CategoryDto>
    {
        public GetCategoryQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
