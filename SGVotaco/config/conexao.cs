using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace SGVotaco.config
{
    class conexao
    {
        private static string Host = "localhost;";
        private static string User = "root;";
        private static string DbName = "Gvoto";
        private static string Senha = "";
        private static MySqlConnection Conex;
        private MySqlCommand SqlCommand;
        private MySqlDataAdapter SqlDataAdapter;
        private MySqlDataReader SqlDataReader;

        public  MySqlConnection conex { get => conexao.Conex; set => conexao.Conex = value; }
        public MySqlCommand sqlCommand { get => SqlCommand; set => SqlCommand = value; }
        public MySqlDataAdapter sqlDataAdapter { get => SqlDataAdapter; set => SqlDataAdapter = value; }
        public MySqlDataReader sqlDataReader { get => SqlDataReader; set => SqlDataReader = value; }

        private  MySqlConnection DdConex()
        {
            try
            {
                if (conex != null) conex.Close();

                conex = new MySqlConnection($@"server={conexao.Host} user id={conexao.User} database={conexao.DbName}");

                if (conex != null) conex.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return conex;
        }

        public conexao()
        {
            DdConex();
        }
    }
}
