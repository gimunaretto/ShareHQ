using ShareHQ.Models;

namespace ShareHQ.Data
{
    public class RepositoryUsuario : Repository<Usuario>, IRepositoryUsuario
    {
        public RepositoryUsuario(AppDbContext context) : base(context)
        {
        }
    }
}
