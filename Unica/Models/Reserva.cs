using System;

namespace Unica.Models
{
    public class Reserva
    {

        public int? IdContrato {get; set;}
        public string Placa { get; set; }

        public bool Status {get; set;}

        public DateTime DataFinalContratada {get; set;}

        public DateTime DataRetirada {get; set;}

        public DateTime DataDevolucao {get; set;} 


        public Reserva()
        {
        }
    }
}


