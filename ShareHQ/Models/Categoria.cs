using System.ComponentModel.DataAnnotations;

namespace ShareHQ.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Categoria obrigatória!")]
        public string Nome { get; set; }
    }
}
