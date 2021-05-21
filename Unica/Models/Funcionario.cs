using System;
using System.ComponentModel.DataAnnotations;
namespace Unica.Models

{
    public class Funcionario : Pessoa
    {

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Campo CPF obrigatório")]
        public string Cpf { get; set; }

        [Display(Name = "Salário")]
        [Required(ErrorMessage = "Campo Salário obrigatório")]
        [DataType(DataType.Currency)]
        public double Salario { get; set;}

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "Campo Cargo obrigatório")]
        public Cargo cargo { get; set; }

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Campo Usuário obrigatório")]
        public string usuario { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo Senha obrigatório")]
        [MinLength(6)]
        public string senha { set; get; }
        public Funcionario()
        {
        }
    }

   
}
