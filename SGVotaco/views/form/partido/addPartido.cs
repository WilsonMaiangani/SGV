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
    public partial class addPartido : Form
    {
        public addPartido()
        {
            InitializeComponent();
            _presidentes = new Presidentes();
            _controllerImagem = new controllerImagem();
            dataContext = new DataContext();
            _partidos = new partidos();
        }
        private Presidentes _presidentes;
        private partidos _partidos;
        private controllerImagem _controllerImagem;
        private DataContext dataContext;


        private void Zerar()
        {
            txtNome.Text = comboBox.Text = string.Empty;
            lblNome.Text = lblBi.Text = lblGenero.Text = ":";
            pictureBoxFotoPR.ImageLocation = _controllerImagem.getImagemDefault();
            pictureBoxFoto.ImageLocation = _controllerImagem.getImagemDefault("default_");


        }
        private void ShowHide(bool logic)
        {
            pictureBoxCheckmark.Visible = lblSucesso.Visible = logic;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtNome.Text)) lblMensagem2.Text = "Preencha o nome do partido é obrigatorio!";
                else if (string.IsNullOrEmpty(comboBox.Text)) lblMensagem2.Text = "Selecione o nome do presidente é obrigatorio!";
                else if (string.IsNullOrEmpty(pictureBoxFoto.ImageLocation)) lblMensagem2.Text = "Selecione a imagem do partido";
                else if (!string.IsNullOrEmpty(txtNome.Text) && !string.IsNullOrEmpty(comboBox.Text) && !string.IsNullOrEmpty(pictureBoxFoto.ImageLocation))
                {
                    lblMensagem2.Text = string.Empty;

                    _partidos = new partidos
                    {
                        idPresidente = _presidentes.id,
                        nome = txtNome.Text.Trim(),
                    };

                    var obj = new Dictionary<string, string>
                    {
                        {"nome", $"'{_partidos.nome}'" }
                    };


                    if (dataContext.GetData("Partidos", obj) == "Not")
                    {
                        obj = new Dictionary<string, string>
                            {
                                {"idPresidente", $"'{_presidentes.id}'" }
                            };

                        if (dataContext.GetData("Partidos", obj) == "Not")
                        {


                            var Data1 = new Dictionary<string, string>
                            {
                                {"caminho", $"'{_controllerImagem.GetCaminhoDestinoImg()}'" }
                            };

                            var Data2 = new Dictionary<string, string>
                            {
                                {"idPresidente", $"'{_presidentes.id}'" },
                                {"idBandeira", $"'0'" },
                                {"nome", $"'{_partidos.nome}'" }

                            };

                            string[] tabelas = { "Imagem", "Partidos" };

                            for (int i = 0; i < tabelas.Length; i++)
                            {
                                if (i == 0)
                                {
                                    dataContext.add(tabelas[i], Data1);

                                    _partidos.imagem = new Imagem { id = dataContext.getLastId(tabelas[i]) };

                                    Data2["idBandeira"] = $"'{_partidos.imagem.id}'";

                                }
                                else
                                {
                                    if (dataContext.add(tabelas[i], Data2) == "Ok")
                                    {
                                        _controllerImagem.Upload(_controllerImagem.GetCaminhoDestinoImg());
                                        Zerar();
                                        timer1.Enabled = true;
                                        ShowHide(true);
                                    }

                                }
                            }



                        }
                        else lblMensagem2.Text = "Este presidente já se encontra associado a um partido.";
                    }
                    else lblMensagem2.Text = "Partido já existente";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnImagem_Click(object sender, EventArgs e)
        {
            _controllerImagem.GetPicture(pictureBoxFoto);
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var obj = new Dictionary<string, string>
                {
                    {"nome",$"'{comboBox.Text}'" },
                    {"idFoto",$"" },
                    {"bi",$"" },
                    {"genero",$"" },
                    {"id",$"" }
                };

                var dataList = new List<string>() { };

                dataList = dataContext.GetDatas("Presidentes", obj);

                if (dataList.Count > 0)
                {
                    _presidentes.nome = lblNome.Text = comboBox.Text.Trim();
                    lblBi.Text = dataList[1];
                    lblGenero.Text = dataList[2];
                    _presidentes.id = int.Parse(dataList[3]);
                    _presidentes.imagem = new Imagem { id = int.Parse(dataList[0]) };

                    obj = new Dictionary<string, string>
                    {
                        {"id", $"'{_presidentes.imagem.id  }'" },
                        {"caminho", $"" }
                    };
                    pictureBoxFotoPR.ImageLocation = _presidentes.imagem.caminho = dataContext.GetData("imagem", obj);

                }




            }
            catch (Exception)
            {

                throw;
            }
        }

        private void addPartido_Load(object sender, EventArgs e)
        {
            var obj = new Dictionary<string, string>
            {
                {"nome","*" }
            };


            var dataList = new List<string>() { };


            dataList = dataContext.GetDatas("Presidentes", obj);
            if (dataList.Count > 0) comboBox.Items.AddRange(dataList.ToArray());

            //comboBox.Items.AddRange();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval++;
            if (timer1.Interval == 1002)
            {
                timer1.Stop();
                timer1.Enabled = false;
                ShowHide(false);

            }
        }
    }
}
