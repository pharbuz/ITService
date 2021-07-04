using ITService.Domain.Enums;
using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;

namespace ITService.Domain.Query.User
{
    public sealed class SearchUsersQuery : IQuery<UserPageResult<UserDto>>
    {
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
