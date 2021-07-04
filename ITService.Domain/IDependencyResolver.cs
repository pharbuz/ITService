using System.Threading.Tasks;

namespace ITService.Domain
{
    public interface IDependencyResolver
    {
        T ResolveOrDefault<T>() where T : class;
    }
}
