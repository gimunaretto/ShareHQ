using ShareHQ.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace ShareHQ.ViewModels
{
    public class UsuariosViewModel
    {
        [DisplayName("Digite o Nome para Busca")]
        public string Search { get; set; }

        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}