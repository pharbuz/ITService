using ITService.Domain.Query.Dto;
using System;

namespace ITService.Domain.Query.Todo
{
    public sealed class GetTodoQuery : IQuery<TodoDto>
    {
        public GetTodoQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
