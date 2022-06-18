using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeachSys.Models
{
    public class Compartimento
    {
        [Key]
        public int CompartimentoId { get; set; }

        public int Numero { get; set; }

        public string Tamanho { get; set; }

        public bool Disponivel { get; set; }

        public bool Trancado { get; set;}

        [ForeignKey("Cadastro")]
        public int? CadastroId { get; set; }

        public virtual Cadastro Cadastro { get; set; }

        [ForeignKey("Armario")]

        public int? ArmarioId { get; set; }

        public virtual Armario Armario { get; set; }
    }
}