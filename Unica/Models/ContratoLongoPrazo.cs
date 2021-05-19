using System;
namespace Unica.Models
{
    public class ContratoLongoPrazo : Contrato
    {
        public int TempoContrato { get; set; }

        public double Mensalidade { get; set; }

        public double Desconto { get; set; }

        public ContratoLongoPrazo()
        {
            
            
        }
    }
}
