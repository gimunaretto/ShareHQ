using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareHQ.Models
{
    public class Item
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }
        public string Titulo { get; set; }
        public string NrEdicao { get; set; }
        public string Descricao { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
        public bool? Disponivel { get; set; }

    }
}
