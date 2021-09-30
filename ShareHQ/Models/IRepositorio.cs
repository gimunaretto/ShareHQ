using System.Collections.Generic;
using System.Linq;

namespace ShareHQ.Models
{
    public interface IRepositorio
    {
        Categoria GetCategoriaById(int Id);

        List<Categoria> GetCategorias();

        #region [Usuário]
        IQueryable<Usuario> Usuarios { get; }
        void AdicionaUsuario(Usuario usuario);
        void UpdateUsuario(Usuario usuario);
        void RemoveUsuario(Usuario usuario);
        #endregion
    }

}