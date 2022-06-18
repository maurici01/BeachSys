using System;
using System.ComponentModel.DataAnnotations;

namespace BeachSys.Models
{
    public class Cadastro
    {
        [Key]
        public int CadastroId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Email { get; set; }

        
    }
} 