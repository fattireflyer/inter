using System;

namespace Unica.Models
{
    public class VeiculoReservaViewModel
    {
        public int? Id { get; set; }
        public string Marca { get; set; }
        public string Descricao { get; set; }
        public decimal ValorDiaria { get; set; }
        public string Categoria { get; set; }
        public int Status { get; set; }

        public VeiculoReservaViewModel()
        {
        }
    }
}


