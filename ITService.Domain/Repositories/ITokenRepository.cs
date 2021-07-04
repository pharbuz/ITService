using System.Threading.Tasks;

namespace ITService.Domain.Repositories
{
    public interface ITokenRepository
    {
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string token);
    }
}
