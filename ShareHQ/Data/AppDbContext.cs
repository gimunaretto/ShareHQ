using Microsoft.EntityFrameworkCore;

namespace ShareHQ.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Item> Itens { get; set; }

        public DbSet<ItemEmprestado> ItensEmprestados { get; set; }
    }
}