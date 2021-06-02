using System;
using System.ComponentModel.DataAnnotations;
namespace Unica.Models
{
    public class CategoriaVeiculo
    {
        
        public int? Id { get; set; }
        
        [Display(Name="Categoria")]
        [Required(ErrorMessage = "Campo Categoria obrigatório")]
        public string Descricao { get; set; }
        public CategoriaVeiculo()
        {
        }
    }
}
