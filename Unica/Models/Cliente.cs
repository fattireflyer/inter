using System;
using System.ComponentModel.DataAnnotations;
namespace Unica.Models
{
    public class Cliente : Pessoa
    {

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "Campo CEP obrigatório")]
        public string Cnpj { get; set; }


        [Display(Name = "Razão Social")]
        [Required(ErrorMessage = "Campo Razão Social obrigatório")]
        public string RazaoSocial { get; set; }

        public Cliente()
        {
        }
    }
}
