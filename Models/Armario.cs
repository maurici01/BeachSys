using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeachSys.Models
{
    public class Armario
    {
        [Key]
        public int ArmarioId { get; set; }
        
        public string Regiao { get; set; }

        public string PontoX { get; set; }

        public string PontoY { get; set; }

        public virtual ICollection<Compartimento> Compartimento { get; set; }
    }
}