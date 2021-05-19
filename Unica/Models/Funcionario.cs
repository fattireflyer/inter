using System;
using System.ComponentModel.DataAnnotations;
namespace Unica.Models

{
    public class Funcionario : Pessoa
    {

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Campo CPF obrigatório")]
        public string Cpf { get; set; }


        public double Salario { get; set;}


        public Cargo cargo { get; set; }


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
