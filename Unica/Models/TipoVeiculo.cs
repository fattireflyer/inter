﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Unica.Models
{
    public class TipoVeiculo
    {
        public int? Id { get; set; }

        [Display(Name="Tipo")]
        [Required(ErrorMessage = "Campo Tipo obrigatório")]
        public string Descricao { get; set; }
        public TipoVeiculo()
        {
        }
    }
}
