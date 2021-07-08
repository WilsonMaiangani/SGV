using System;
using System.Collections.Generic;
using System.Text;

namespace SGVotaco.models
{
    class partidos
    {
        private int Id;
        private int IdPresidente;
        private int IdBandeira;
        private string Nome;
        private Imagem Imagem;
        private Presidentes Presidentes;

        public int id { get => Id; set => Id = value; }
        public int idPresidente { get => IdPresidente; set => IdPresidente = value; }
        public int idBandeira { get => IdBandeira; set => IdBandeira = value; }
        public string nome { get => Nome; set => Nome = value; }

        public Imagem imagem { get => Imagem; set => Imagem = value; }
        public Presidentes presidentes { get => Presidentes; set => Presidentes = value; }
    }
}
