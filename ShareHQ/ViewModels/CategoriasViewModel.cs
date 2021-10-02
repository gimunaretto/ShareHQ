using ShareHQ.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace ShareHQ.ViewModels
{
    public class CategoriasViewModel
    {
        [DisplayName("Digite a Categoria para Busca")]
        public string Search { get; set; }

        public IEnumerable<Categoria> Categorias { get; set; }
    }
}