using System;
using System.ComponentModel.DataAnnotations;
namespace Unica.Models
{
    public class Pessoa
    {
        public int? codigo { get; set; }

        [Display(Name="Nome")]
        [Required(ErrorMessage = "Campo Nome obrigatório")]
        public string Nome { get; set;}

        public Endereco endereco { get; set; }

        [Display(Name ="Telefone")]
        [Required(ErrorMessage = "Campo Telefone obrigatório")]
        public string Telefone { get; set; }


        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo E-mail obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Campo Status obrigatório")]
        public string Status { get; set; }
        public Pessoa()
        {
            
        }
    }
}
