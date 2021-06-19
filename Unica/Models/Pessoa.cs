using System;
using System.ComponentModel.DataAnnotations;
namespace Unica.Models
{
    public class Pessoa
    {
        public int? Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Nome obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Campo Telefone obrigatório")]
        public string Telefone { get; set; }


        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo E-mail obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public StatusPessoa Status { get; set; }


        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "Campo Logradouro obrigatório")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Campo Número obrigatório")]
        public string Numero { get; set; }
        public string Complemento { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Campo Bairro obrigatório")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Campo Cidade obrigatório")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Campo Estado obrigatório")]
        public string Estado { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "Campo CEP obrigatório")]
        public string Cep { get; set; }

        public Pessoa()
        {

        }
    }
}
