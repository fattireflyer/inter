using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Unica.Models
{
    public class ContratoViewModel
    {
        public int? Id { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }

        public int Status { get; set; }

        public string NomeCliente { get; set; }

        public ContratoViewModel()
        {
        }
    }
}
