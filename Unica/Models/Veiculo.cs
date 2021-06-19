using System;

namespace Unica.Models
{
    public class Veiculo
    {

        public int? Id { get; set; }
        public string Placa { get; set; }

        public string Marca { get; set; }

        public string Descricao { get; set; }

        public double ValorDiaria { get; set; }

        public int Lugares { get; set; }

        public float? Carga { get; set; }

        public string Categoria { get; set; }

        public string Tipo { get; set; }

        public int Status { get; set; }


        public Veiculo()
        {
        }
    }
}


