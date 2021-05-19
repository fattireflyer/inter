using System;
using System.ComponentModel.DataAnnotations;

namespace Unica.Models
{
    
    public class Contrato
    {
        public int? Id { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public StatusContrato Status;

        public Contrato()
        {

           
           
        }
    }
}
