using System;
using System.ComponentModel.DataAnnotations;

namespace Unica.Models
{
    public class Endereco
    {
        public int? id { get; set; }

        public int? pessoaCodigo { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "Campo CEP obrigatório")]
        public string Cep { get; set; }

        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "Campo Logradouro obrigatório")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Campo Número obrigatório")]
        public string Numero { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Campo Bairro obrigatório")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Campo Cidade obrigatório")]
        public string Cidade { get; set; }

        [Display(Name = "UF")]
        [Required(ErrorMessage = "Campo UF obrigatório")]
        public string Uf { get; set; }






        public Endereco()
        {
        }
    }
}
