using System;

namespace Unica.Models
{
    public class Veiculo
    {

        public int? Id {get; set;}
        public string Placa { get; set; }

        public string Descricao { get; set; }

        public double ValorDiaria { get; set; }

        public int Lugares { get; set; }

        public float? Carga { get; set; }

        public CategoriaVeiculo Categoria { get; set; }

        public TipoVeiculo Tipo { get; set; }

        public StatusVeiculo Status;

        public Veiculo()
        {
        }
    }
}


