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

        public Item GetItemById(int Id)
        {
            return _context.Itens.FirstOrDefault(x => x.Id == Id);
        }

        public List<Item> GetItens()
        {
            return _context.Itens.ToList();
        }

        public void Add(Item item)
        {                       
            _context.Itens.Add(item);
            _context.SaveChanges();
        }

        public void Update(Item item)
        {
            _context.Itens.Update(item);
            _context.SaveChanges();
        }

        public void Remove(Item item)
        {
            _context.Itens.Remove(item);
            _context.SaveChanges();
        }
    }
}
