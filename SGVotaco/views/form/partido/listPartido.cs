using SGVotaco.controllers;
using SGVotaco.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SGVotaco.views.form.partido
{
    public partial class listPartido : Form
    {
        public listPartido()
        {
            InitializeComponent();
            _controllerImagem = new controllerImagem();
            _partidos = new partidos();
            dataContext = new DataContext();
        }
        private partidos _partidos;
        private controllerImagem _controllerImagem;
        private DataContext dataContext;
        private void setValues()
        {

            lblNome.Text = _partidos.presidentes.nome;
            lblGenero.Text = _partidos.presidentes.genero;
            lblPartido.Text = _partidos.nome;
            if (_partidos.imagem == null)
            {
                pictureBoxFotoPR.ImageLocation = _controllerImagem.getImagemDefault();
                pictureBoxFotoPartido.ImageLocation = _controllerImagem.getImagemDefault("default_");
            }

            else
            {
                pictureBoxFotoPR.ImageLocation = _partidos.presidentes.imagem.caminho;
                pictureBoxFotoPartido.ImageLocation = _partidos.imagem.caminho;
            }
        }
        private void listPartido_Load(object sender, EventArgs e)
        {
            var dataContext = new DataContext();

            dataContext.GridView(dataGridView1, "GetDataPartidos");
            dataGridView1.Columns["Partido"].Width = 200;
            dataGridView1.Columns["Presidente"].Width = 200;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _partidos.id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());

                var obj = new Dictionary<string, string>
                {
                    {"id",$"'{_partidos.id}'" },
                    {"idPresidente",$"" },
                    {"idBandeira",$"" }
                };

                var dataList = new List<string>() { };

                dataList = dataContext.GetDatas("Partidos", obj);

                obj = new Dictionary<string, string>
                    {
                        {"id", $"'{Convert.ToInt32(dataList[1])}'" },
                        {"caminho", $"" }
                    };

                var _obj = new Dictionary<string, string>
                    {
                        {"id", $"'{Convert.ToInt32(dataList[0])}'" },
                        {"idFoto", $"" }
                    };

                _partidos = new partidos
                {
                    nome = dataGridView1.Rows[e.RowIndex].Cells["Partido"].Value.ToString(),
                    idBandeira = Convert.ToInt32(dataList[1]),

                    imagem = new Imagem { caminho = dataContext.GetData("imagem", obj) },

                    presidentes = new Presidentes
                    {
                        id = Convert.ToInt32(dataList[0]),
                        nome = dataGridView1.Rows[e.RowIndex].Cells["Presidente"].Value.ToString(),
                        genero = dataGridView1.Rows[e.RowIndex].Cells["Genero"].Value.ToString(),
                        imagem = new Imagem
                        {
                            id = Convert.ToInt32(dataContext.GetData("Presidentes", _obj)),

                        }
                    }
                };

                obj["id"] = $"'{_partidos.presidentes.imagem.id}'";
                _partidos.presidentes.imagem.caminho = dataContext.GetData("imagem", obj);

                setValues();
            }
            catch (Exception)
            {
                _partidos = new partidos { presidentes = new Presidentes() };
                setValues();
            }
        }
    }
}
