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

        #region [Categoria]
        public Categoria GetCategoriaById(int Id)
        {
            return _context.Categorias.FirstOrDefault(x => x.Id == Id);             
        }

        public List<Categoria> GetCategorias()
        {
            return _context.Categorias.ToList();
        }

        public IQueryable<Categoria> Categorias { get => _context.Categorias; }
        public void AdicionaCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public void UpdateCategoria(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            _context.SaveChanges();
        }

        public void RemoveCategoria(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
        }
        #endregion

        #region [Usuário]
        public IQueryable<Usuario> Usuarios { get => _context.Usuarios; }

        public void AdicionaUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void UpdateUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void RemoveUsuario(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
        #endregion

        #region [Item]
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
        #endregion


        #region [ItemEmpresatdo]
        public ItemEmprestado GetEmprestadoById(int Id)
        {
            return _context.ItensEmprestados.FirstOrDefault(x => x.Id == Id);
        }

        public List<ItemEmprestado> GetEmprestado()
        {
            return _context.ItensEmprestados.ToList();
        }

        public void Add(ItemEmprestado itensEmprestado)
        {
            _context.ItensEmprestados.Add(itensEmprestado);
            _context.SaveChanges();
        }

        public void Update(ItemEmprestado itensEmprestado)
        {
            _context.ItensEmprestados.Update(itensEmprestado);
            _context.SaveChanges();
        }

        public void Remove(ItemEmprestado itensEmprestado)
        {
            _context.ItensEmprestados.Remove(itensEmprestado);
            _context.SaveChanges();
        }
        #endregion

    }
}
