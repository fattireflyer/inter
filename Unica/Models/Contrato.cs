using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Unica.Models
{

    public class Contrato
    {
        public int? Id { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }

        public int Status { get; set; }

        public int ClienteId { get; set; }

        public List<Reserva> ListaReservas { get; set; }

        public Contrato()
        {



        }
    }
}
