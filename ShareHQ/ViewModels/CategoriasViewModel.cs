using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareHQ.Models;
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
