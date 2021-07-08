using System;
using System.Collections.Generic;
using System.Text;

namespace SGVotaco.models
{
    class eleitores
    {
        private int Id;
        private string Nome;
        private string CodVoto;


        public int id { get => Id; set => Id = value; }
        public string nome { get => Nome; set => Nome = value; }
        public string codVoto { get => CodVoto; set => CodVoto = value; }

    }
}
