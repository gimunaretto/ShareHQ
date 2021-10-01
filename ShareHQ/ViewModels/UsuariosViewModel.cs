using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareHQ.Models;
using System.ComponentModel;

namespace ShareHQ.ViewModels
{
    public class UsuariosViewModel
    {
        [DisplayName("Digite o Nome para Busca")]
        public string Search { get; set; }

        public IEnumerable <Usuario> Usuarios { get; set; }
    }
}
