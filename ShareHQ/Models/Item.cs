using System.ComponentModel.DataAnnotations;

namespace ShareHQ.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Titulo obrigatória!")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Edição obrigatória!")]
        public string Edicao { get; set; }

        [Required(ErrorMessage = "Descrição obrigatória!")]
        public string Descricao { get; set; }

        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Categoria obrigatória!")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Disponibilidade obrigatória!")]
        public bool Disponivel { get; set; }
    }
}