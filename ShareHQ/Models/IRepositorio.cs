using System.Collections.Generic;

namespace ShareHQ.Models
{
    public interface IRepositorio
    {
        Categoria GetCategoriaById(int Id);

        List<Categoria> GetCategorias();

        Item GetItemById(int Id);

        List<Item> GetItens();

        void Add(Item item);

        void Update(Item item);

        void Remove(Item item);
    }
}
