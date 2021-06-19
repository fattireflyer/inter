using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Unica.Models
{
    public class FuncionarioViewModel
    {


        //propriedades & atributos:
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Campo Usuário obrigatório")]
        public string Usuario { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo senha obrigatório")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres")]
        public string Senha { get; set; }


    }
}
