using ShareHQ.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace ShareHQ.ViewModels
{
    public class ItensViewModel
    {
        [DisplayName("Digite o Título para busca")]
        public string Search { get; set; }

        public IEnumerable<Item> Itens { get; set; }
    }
}
