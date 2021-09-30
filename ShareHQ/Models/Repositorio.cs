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
    }
}
