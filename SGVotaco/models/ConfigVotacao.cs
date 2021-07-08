using System;
using System.Collections.Generic;
using System.Text;

namespace SGVotaco.models
{
    class ConfigVotacao
    {
        private int Id;
        private string Ano;
        public enum Estado { On, OFF }
        private string DataComeco;
        private string HoraInicio;
        private string HoraTermino;

        public int id { get => Id; set => Id = value; }
        public string dataComeco { get => DataComeco; set => DataComeco = value; }
        public string horaInicio { get => HoraInicio; set => HoraInicio = value; }
        public string horaTermino { get => HoraTermino; set => HoraTermino = value; }
        public string ano { get => Ano; set => Ano = value; }


    }
}
