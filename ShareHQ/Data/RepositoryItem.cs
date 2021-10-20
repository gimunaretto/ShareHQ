using ShareHQ.Models;

namespace ShareHQ.Data
{
    public class RepositoryItem : Repository<Item>, IRepositoryItem
    {
        public RepositoryItem(AppDbContext context) : base(context)
        {
        }
    }
}
