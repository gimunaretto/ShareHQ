using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareHQ.Models
{
    public class ItemEmprestado
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public int PrazoDeDevolucao { get; set; }
        public int StatusDevolucao { get; set; }
        public int StatusEmprestimo { get; set; }


        public enum DevolucaoStatus
        {
            [EnumStringValue("No Prazo")]
            P = 1,
            [EnumStringValue("Atrasado")]
            A = 2,
        }

        public enum EmprestimoStatus
        {
            [EnumStringValue("Em Empréstimo")]
            E = 1,
            [EnumStringValue("Disponível para empréstimo")]
            DE = 2,
            [EnumStringValue("Devolvido")]
            D = 3,
        }

    }
}
