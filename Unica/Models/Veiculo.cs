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

        public CategoriaVeiculo Categoria { get; set; }

        public TipoVeiculo Tipo { get; set; }

        public StatusVeiculo status;

        public Veiculo()
        {
        }
    }
}


