using System;
using ITService.Domain.Enums;
using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;

namespace ITService.Domain.Query.Category
{
    public sealed class SearchCategoriesQuery : IQuery<CategoryPageResult<CategoryDto>>
    {
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string CategoryBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
