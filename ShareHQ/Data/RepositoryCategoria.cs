using ShareHQ.Models;

namespace ShareHQ.Data
{
    public class RepositoryCategoria : Repository<Categoria>, IRepositoryCategoria
    {
        public RepositoryCategoria(AppDbContext context) : base(context)
        {
        }
    }
}
