using System;
using System.Collections.Generic;
using System.Text;

namespace SGVotaco.models
{
    class Presidentes
    {
        private int Id;
        private int IdFoto;
        private string Nome;
        private string Genero;
        private string DataNascimento;
        private string Bi;
        private Imagem Imagem;

        public int id { get => Id; set => Id = value; }
        public int idFoto { get => IdFoto; set => IdFoto = value; }
        public string nome { get => Nome; set => Nome = value; }
        public string genero { get => Genero; set => Genero = value; }
        public string dataNascimento { get => DataNascimento; set => DataNascimento = value; }
        public string bi { get => Bi; set => Bi = value; }
        public Imagem imagem { get => Imagem; set => Imagem = value; }

    }
}
