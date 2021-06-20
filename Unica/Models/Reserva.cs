using System;

namespace Unica.Models
{
    public class Reserva
    {
        public int? IdContrato { get; set; }
        public int? IdVeiculo { get; set; }
        public int Status { get; set; }
        public DateTime DataFinalContratada { get; set; }
        public DateTime DataRetirada { get; set; }
        public decimal Valor { get; set; }
        public Reserva()
        {
        }
    }
}


