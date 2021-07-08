using SGVotaco.config;
using SGVotaco.models;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace SGVotaco.controllers
{
    class DataContext : conexao
    {
        conexao DbConexao;
        private string Mensagem;
        public string mensagem { get => Mensagem; set => Mensagem = value; }

        public DataContext()
        {
            DbConexao = new conexao();
        }

        private string Login(Usuario usuario)
        {

            try
            {
                DbConexao.sqlCommand = new MySqlCommand($"Select *from Usuario where email = '{usuario.email}' and senha = '{usuario.senha}'", DbConexao.conex);
                DbConexao.sqlDataReader = DbConexao.sqlCommand.ExecuteReader();

                if (DbConexao.sqlDataReader.HasRows)
                {
                    while (DbConexao.sqlDataReader.Read())
                    {
                        usuario = new Usuario
                        {
                            id = Convert.ToInt32(DbConexao.sqlDataReader["id"]),
                            nome = DbConexao.sqlDataReader["nome"].ToString(),
                        };

                    }

                    mensagem = "Ok";
                }
                else mensagem = "Email ou a senha incorreta.";

            }
            catch (Exception)
            {

                throw;
            }
            finally { DbConexao.sqlDataReader.Close(); }
            return mensagem;

        }

        private string Add(string tabela, Dictionary<string, string> Data)
        {
            try
            {
                string campos = "", values = "";
                mensagem = string.Empty;

                if (Data.Count > 0)
                {
                    foreach (KeyValuePair<string, string> item in Data)
                    {
                        campos += item.Key + ',';
                        values += item.Value + ',';
                    }
                    campos = campos.Substring(0, campos.Length - 1);
                    values = values.Substring(0, values.Length - 1);
                    if (campos == "caminho") values = values.Replace("\\", @"\\");

                    DbConexao.sqlCommand = new MySqlCommand($"Insert into {tabela} ({campos}) values ({values});", DbConexao.conex);

                    if (DbConexao.sqlCommand.ExecuteNonQuery() > 0)
                        mensagem = "Ok";

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            return mensagem;
        }

        private int GetLastId(string tabela)
        {
            int id = 0;
            try
            {

                DbConexao.sqlCommand = new MySqlCommand($"Select id from {tabela} order by(id)desc limit 1 ;", DbConexao.conex);
                DbConexao.sqlDataReader = DbConexao.sqlCommand.ExecuteReader();
                if (DbConexao.sqlDataReader.HasRows)
                {
                    DbConexao.sqlDataReader.Read();
                    id = Convert.ToInt32(DbConexao.sqlDataReader[0]);
                }

            }
            catch (Exception)
            {


            }
            finally { DbConexao.sqlDataReader.Close(); }
            return id;
        }

        public string GetData(string tabela, Dictionary<string, string> data)
        {
            string campo = "", value = "", campoS = "";
            try
            {
                foreach (KeyValuePair<string, string> item in data)
                {
                    if (!string.IsNullOrEmpty(item.Key) && string.IsNullOrEmpty(item.Value)) campoS = item.Key;
                    else
                    {
                        campo = item.Key;
                        value = item.Value;
                    }
                }
                if (string.IsNullOrEmpty(campoS))
                    DbConexao.sqlCommand = new MySqlCommand($"Select {campo} from {tabela} where {campo} = {value} ;", DbConexao.conex);
                else
                    DbConexao.sqlCommand = new MySqlCommand($"Select {campoS} from {tabela} where {campo} = {value} ;", DbConexao.conex);

                DbConexao.sqlDataReader = DbConexao.sqlCommand.ExecuteReader();

                if (DbConexao.sqlDataReader.HasRows)
                {
                    DbConexao.sqlDataReader.Read();
                    value = DbConexao.sqlDataReader[0].ToString();
                }
                else value = "Not";

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally { DbConexao.sqlDataReader.Close(); }

            return value;
        }

        public List<string> GetDatas(string tabela, Dictionary<string, string> data)
        {
            string campo = "", value = "", campoS = "";
            var dataList = new List<string>();
            try
            {
                foreach (KeyValuePair<string, string> item in data)
                {
                    if (!string.IsNullOrEmpty(item.Key) && string.IsNullOrEmpty(item.Value)) campoS += item.Key + ",";
                    else
                    {
                        campo = item.Key;
                        value = item.Value;
                    }
                }
                if (campoS.Contains(","))
                {
                    campoS = campoS.Substring(0, campoS.Length - 1);
                    DbConexao.sqlCommand = new MySqlCommand($"Select {campoS} from {tabela} where {campo} = {value} ;", DbConexao.conex);
                }
                //if (string.IsNullOrEmpty(campoS))
                //    DbConexao.sqlCommand = new MySqlCommand($"Select {campo} from {tabela} where {campo} = {value} ;", DbConexao.conex);
                //if (!string.IsNullOrEmpty(campoS))
                //    DbConexao.sqlCommand = new MySqlCommand($"Select {campoS} from {tabela} where {campo} = {value} ;", DbConexao.conex);
                if (!string.IsNullOrEmpty(campo) && value == "*")
                    DbConexao.sqlCommand = new MySqlCommand($"Select {campo} from {tabela};", DbConexao.conex);

                DbConexao.sqlDataReader = DbConexao.sqlCommand.ExecuteReader();

                if (DbConexao.sqlDataReader.HasRows)
                {
                    if (!string.IsNullOrEmpty(campo) && value == "*")
                    {
                        while (DbConexao.sqlDataReader.Read())
                        {
                            dataList.Add(DbConexao.sqlDataReader[campo].ToString());
                        }
                    }
                    else
                    {
                        string[] ArrayCampos = campoS.Split(',');
                        int qtd = 0;
                        while (DbConexao.sqlDataReader.Read())
                        {
                            for (int i = 0; i < ArrayCampos.Length; i++)
                            {
                                dataList.Add(DbConexao.sqlDataReader[ArrayCampos[i]].ToString());
                            }
                        }
                    }
                }
                else value = "Not";

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally { DbConexao.sqlDataReader.Close(); }

            return dataList;
        }

        public void GetDatas(string tabela, List<string> Data)
        {
            int qtd = 0;
            string campos = "";
            var values = new List<Dictionary<string, string>>();
            Dictionary<string, string> _Data = new Dictionary<string, string>();




            if (Data.Count == 0)
            {
                DbConexao.sqlCommand = new MySqlCommand($"Select * from {tabela};", DbConexao.conex);
                DbConexao.sqlDataReader = DbConexao.sqlCommand.ExecuteReader();
                //DbConexao.sqlDataReader.Read();
                for (int i = 0; i < DbConexao.sqlDataReader.FieldCount; i++)
                {
                    campos += Convert.ToString(DbConexao.sqlDataReader.GetName(i)) + ".";
                }
                campos = campos.Substring(0, campos.Length - 1);
                string[] ArrayCampos = campos.Split('.');

                while (DbConexao.sqlDataReader.Read())
                {
                    for (int i = 0; i < ArrayCampos.Length; i++)
                    {
                        _Data.Add(ArrayCampos[i], DbConexao.sqlDataReader[ArrayCampos[i]].ToString());

                        //_Data = new Dictionary<string, string>
                        //{
                        //    {ArrayCampos[i], DbConexao.sqlDataReader[ArrayCampos[i]].ToString() }
                        //};
                        if (i == ArrayCampos.Length - 1)
                        {

                            values.Add
                                (
                                _Data

                                );

                        }
                        //values.Add(DbConexao.sqlDataReader[ArrayCampos[i]].ToString());
                    }

                    qtd++;

                }

                foreach (Dictionary<string, string> item in values)
                {

                }

            }
        }

        public void ExecProcedure(string procedure)
        {
            DbConexao.sqlCommand = new MySqlCommand($"Call {procedure};", DbConexao.conex);
            DbConexao.sqlCommand.ExecuteNonQuery();
        }

        public DataGridView GridView(DataGridView dataGridView, string procedure)
        {
            DataTable table = new DataTable();
            DbConexao.sqlDataAdapter = new MySqlDataAdapter($"Call {procedure}();", DbConexao.conex);
            //DbConexao.sqlDataAdapter.SelectCommand.ExecuteReader();
            DbConexao.sqlDataAdapter.Fill(table);
            dataGridView.DataSource = table;

            return dataGridView;
        }

        public string Update(string tabela, Dictionary<string, string> data)
        {
            string campo = "", value = "", campoWhere = "", valueWhere = "", _campo = "", op = "";
            try
            {
                foreach (KeyValuePair<string, string> item in data)
                {
                    if (!string.IsNullOrEmpty(item.Key) && string.IsNullOrEmpty(item.Value)) campoWhere = item.Key;
                    else if (item.Key == "value" && !string.IsNullOrEmpty(item.Value)) valueWhere = item.Value;
                    else if (item.Key == "op" && !string.IsNullOrEmpty(item.Value)) op = item.Value;

                    else if (item.Key != "value" && !string.IsNullOrEmpty(item.Value))
                    {
                        campo = item.Key;
                        value = item.Value;

                        _campo += $"{campo} = {value},";
                    }
                }
                    _campo = _campo.Substring(0, _campo.Length - 1);
                    if (!string.IsNullOrEmpty(op))
                        DbConexao.sqlCommand = new MySqlCommand($"Update {tabela} set {_campo} where {campoWhere} {op} {valueWhere} ;", DbConexao.conex);
                    else
                        DbConexao.sqlCommand = new MySqlCommand($"Update {tabela} set {_campo} where {campoWhere} = {valueWhere} ;", DbConexao.conex);

                if (DbConexao.sqlCommand.ExecuteNonQuery() > 0)
                    mensagem = "Ok";
        
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally { DbConexao.sqlDataReader.Close(); }

            return mensagem;
        }

        public string login(Usuario usuario) => Login(usuario);
        public string add(string tabela, Dictionary<string, string> Data) => Add(tabela, Data);
        public int getLastId(string tabela) => GetLastId(tabela);

    }
}
