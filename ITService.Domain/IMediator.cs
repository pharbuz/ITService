using System.Threading.Tasks;
using ITService.Domain.Command;
using ITService.Domain.Query;

namespace ITService.Domain
{
    public interface IMediator
    {
        Task<Result> CommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query);
        Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
    }
}
