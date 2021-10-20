using ShareHQ.Models;

namespace ShareHQ.Data
{
    public class RepositoryItemEmprestado : Repository<ItemEmprestado>, IRepositoryItemEmprestado
    {
        public RepositoryItemEmprestado(AppDbContext context) : base(context)
        {
        }
    }
}
