using SGVotaco.controllers;
using SGVotaco.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SGVotaco.views.form.presidente
{
    public partial class listPresidente : Form
    {
        public listPresidente()
        {
            InitializeComponent();
            _presidentes = new Presidentes();
            _controllerImagem = new controllerImagem();
        }
        private Presidentes _presidentes;
        private controllerImagem _controllerImagem;

        private void listPresidente_Load(object sender, EventArgs e)
        {
            var dataContext = new DataContext();

            dataContext.GridView(dataGridView1, "GetDataPresidentes");
            dataGridView1.Columns["nome"].Width = 200;
            dataGridView1.Columns["dataNascimento"].Width = 200;
            dataGridView1.Columns["bi"].Width = 200;
        }

        private void setValues()
        {

            lblNome.Text = _presidentes.nome;
            lblBi.Text = _presidentes.bi;
            lblGenero.Text = _presidentes.genero;
            lblDataNascimento.Text = _presidentes.dataNascimento;
            if (_presidentes.imagem == null)
                pictureBoxFoto.ImageLocation = _controllerImagem.getImagemDefault();
            else
                pictureBoxFoto.ImageLocation = _presidentes.imagem.caminho;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _presidentes.id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
                var dataContext = new DataContext();

                var obj = new Dictionary<string, string>
                    {
                        {"id", $"'{_presidentes.id}'" },
                        {"idFoto", "" }
                    };

                _presidentes = new Presidentes
                {
                    nome = dataGridView1.Rows[e.RowIndex].Cells["nome"].Value.ToString(),
                    bi = dataGridView1.Rows[e.RowIndex].Cells["bi"].Value.ToString(),
                    genero = dataGridView1.Rows[e.RowIndex].Cells["genero"].Value.ToString(),
                    dataNascimento = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["dataNascimento"].Value).ToShortDateString(),
                    imagem = new Imagem { id = Convert.ToInt32(dataContext.GetData("Presidentes", obj)) }
                };

                obj = new Dictionary<string, string>
                    {
                        {"id", $"'{_presidentes.imagem.id}'" },
                        {"caminho", $"" }
                    };
                _presidentes.imagem.caminho = dataContext.GetData("imagem", obj);

                setValues();

            }
            catch (Exception)
            {
                _presidentes = new Presidentes();
                setValues();
            }
        }
    }
}
