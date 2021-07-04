using System.Threading.Tasks;

namespace ITService.Domain.Command
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<Result> HandleAsync(TCommand command);
    }
}
