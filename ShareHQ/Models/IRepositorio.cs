using System.Collections.Generic;

namespace ShareHQ.Models
{
    public interface IRepositorio
    {
        Categoria GetCategoriaById(int Id);

        List<Categoria> GetCategorias();
    }
}
