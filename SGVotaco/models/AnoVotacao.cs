using System;
using System.Collections.Generic;
using System.Text;

namespace SGVotaco.models
{
    class AnoVotacao
    {
        private int Id;
        private string Ano;
        public enum Estado { On, OFF }
        
        public int id { get => Id; set => Id = value; }
        public string ano { get => Ano; set => Ano = value; }


      
    }
}
