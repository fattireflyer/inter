using System;
namespace Unica.Models
{
    public class Veiculo
    {

        public string Placa { get; set; }
        public string Descricao { get; set; }
        public double ValorDiaria { get; set; }
        public int Lugares { get; set; }
        public int? Carga { get; set; }
        public Categoria categoria { get; set; }
        public Tipo tipo { get; set; }

        public Veiculo()
        {
        }
    }
}
