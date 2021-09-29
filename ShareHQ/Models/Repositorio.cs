using System.Collections.Generic;
using System.Linq;

namespace ShareHQ.Models
{
    public class Repositorio : IRepositorio
    {
        private readonly AppDbContext _context;
        
        public Repositorio(AppDbContext context)
        {
            _context = context;
        }
        
        public Categoria GetCategoriaById(int Id)
        {
            return _context.Categorias.FirstOrDefault(x => x.Id == Id);             
        }

        public List<Categoria> GetCategorias()
        {
            return _context.Categorias.ToList();
        }
    }
}
