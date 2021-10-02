using System;
using System.ComponentModel.DataAnnotations;

namespace ShareHQ.Models
{
    public class ItemEmprestado
    {
        public int Id { get; set; }

        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "Usuário obrigatório!")]
        public int UsuarioId { get; set; }

        public Item Item { get; set; }

        [Required(ErrorMessage = "Publicação obrigatória!")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Data obrigatória!")]
        public DateTime DataEmprestimo { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public int PrazoDeDevolucao { get; set; }

        public int StatusDevolucao { get; set; }

        public int StatusEmprestimo { get; set; }

        public enum DevolucaoStatus
        {
            [EnumStringValue("No Prazo")]
            NoPrazo = 1,

            [EnumStringValue("Atrasado")]
            Atrasado = 2,
        }

        public enum EmprestimoStatus
        {
            [EnumStringValue("Em Empréstimo")]
            EmEmprestimo = 1,

            [EnumStringValue("Disponível para empréstimo")]
            Disponivel = 2,

            [EnumStringValue("Devolvido")]
            Devolvido = 3,
        }
    }
}