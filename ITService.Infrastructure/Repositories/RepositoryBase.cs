namespace ITService.Infrastructure.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly ITServiceDBContext _context;

        protected RepositoryBase(ITServiceDBContext context)
        {
            _context = context;
        }
    }
}
