using System.ComponentModel.DataAnnotations;

namespace ShareHQ.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "E-mail obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone obrigatória")]
        public string Telefone { get; set; }
    }
}
