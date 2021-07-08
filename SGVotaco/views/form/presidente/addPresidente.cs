using SGVotaco.controllers;
using System;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGVotaco.models;
using System.Collections.Generic;

namespace SGVotaco.views.form.presidente
{
    public partial class addPresidente : Form
    {
        public addPresidente()
        {
            InitializeComponent();
            _presidentes = new Presidentes();
            _controllerImagem = new controllerImagem();
        }
        private Presidentes _presidentes;
        private controllerImagem _controllerImagem;

        private void btnImagem_Click(object sender, EventArgs e)
        {
            _controllerImagem.GetPicture(pictureBoxFoto);
        }
        private void Zerar()
        {
            txtNome.Text = txtBi.Text = comboBox.Text = string.Empty;
            pictureBoxFoto.ImageLocation = _controllerImagem.getImagemDefault();
            dateTimePicker1.Value = DateTime.Now;

        }
        private void ShowHide(bool logic)
        {
            pictureBoxCheckmark.Visible = lblSucesso.Visible = logic;
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNome.Text)) lblMensagem2.Text = "Preencha o nome é obrigatorio!";
                else if (string.IsNullOrEmpty(txtBi.Text)) lblMensagem2.Text = "Preencha o Bi é obrigatorio!";
                else if (string.IsNullOrEmpty(comboBox.Text)) lblMensagem2.Text = "Selecione o genero é obrigatorio!";
                else if (string.IsNullOrEmpty(pictureBoxFoto.ImageLocation)) lblMensagem2.Text = "Selecione a imagem";
                else if (!string.IsNullOrEmpty(txtNome.Text) && !string.IsNullOrEmpty(txtBi.Text) && !string.IsNullOrEmpty(comboBox.Text) && !string.IsNullOrEmpty(pictureBoxFoto.ImageLocation))
                {
                    lblMensagem2.Text = string.Empty;

                    _presidentes = new Presidentes
                    {
                        nome = txtNome.Text.Trim(),
                        bi = txtBi.Text.Trim(),
                        genero = comboBox.Text.Trim(),
                        dataNascimento = dateTimePicker1.Value.ToString("yyy:MM:dd")
                    };

                    var dataContext = new DataContext();

                    var obj = new Dictionary<string, string>
                    {
                        {"bi", $"'{_presidentes.bi}'" }
                    };


                    if (dataContext.GetData("Presidentes", obj) != _presidentes.bi)
                    {
                        var Data1 = new Dictionary<string, string>
                    {
                        {"caminho", $"'{_controllerImagem.GetCaminhoDestinoImg()}'" }
                    };

                        var Data2 = new Dictionary<string, string>
                    {
                        {"idFoto", "'1'" },
                        {"nome", $"'{_presidentes.nome}'" },
                        {"genero", $"'{_presidentes.genero}'" },
                        {"dataNascimento", $"'{_presidentes.dataNascimento}'" },
                        {"bi", $"'{_presidentes.bi}'" },

                    };

                        string[] tabelas = { "Imagem", "Presidentes" };
                        

                        for (int i = 0; i < tabelas.Length; i++)
                        {
                            if (i == 0)
                            {
                                dataContext.add(tabelas[i], Data1);

                                _presidentes.imagem = new Imagem { id = dataContext.getLastId(tabelas[i]) };

                                Data2["idFoto"] = $"'{_presidentes.imagem.id}'";

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
                    else lblMensagem2.Text = "Número do Bi já existente!";
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

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
