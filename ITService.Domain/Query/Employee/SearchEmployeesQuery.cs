using System;
using ITService.Domain.Enums;
using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;

namespace ITService.Domain.Query.Employee
{
    public sealed class SearchEmployeesQuery : IQuery<EmployeePageResult<EmployeeDto>>
    {
        public Guid ContactId { get; set; }
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string EmployeeBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
