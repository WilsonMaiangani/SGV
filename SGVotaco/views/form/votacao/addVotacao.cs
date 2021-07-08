using SGVotaco.controllers;
using SGVotaco.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SGVotaco.views.form.votacao
{
    public partial class addVotacao : Form
    {
        public addVotacao()
        {
            InitializeComponent();
            dataContext = new DataContext();
            _partido = new partidos();
            configVotacao = new ConfigVotacao();
        }
        private DataContext dataContext;
        private partidos _partido;
        private ConfigVotacao configVotacao;
        List<string> listDatas;
        private string ataAtual = "", horaAtual;
        private void addVotacao_Load(object sender, EventArgs e)
        {
            ataAtual = DateTime.Now.ToShortDateString();
            horaAtual = DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second;
            var obj = new Dictionary<string, string>
            {
                {"nome","*" }
            };


            var dataList = new List<string>() { };
            listDatas = new List<string>();

            dataList = dataContext.GetDatas("Partidos", obj);
            if (dataList.Count > 0) comboBox.Items.AddRange(dataList.ToArray());


            obj = new Dictionary<string, string>
            {
                {"estado",$"'{ConfigVotacao.Estado.On}'" },
                {"datacomeco","" },
                {"horaInicio","" },
                {"horaTermino","" },
                {"id","" }
            };

            listDatas = dataContext.GetDatas("ConfigVotacao", obj);

            if (listDatas.Count > 0)
            {
                configVotacao = new ConfigVotacao
                {
                    dataComeco = listDatas[0].ToString(),
                    horaInicio = listDatas[1].ToString(),
                    horaTermino = listDatas[2].ToString(),
                    id = Convert.ToInt32(listDatas[3])
                };
                configVotacao.dataComeco = configVotacao.dataComeco.Replace("00:00:00", string.Empty).Trim();
            }

        }

        private void ShowHide(bool logic)
        {
            pictureBoxCheckmark.Visible = lblSucesso.Visible = logic;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var obj = new Dictionary<string, string>
                {
                    {"nome",$"'{comboBox.Text}'" },
                    {"idPresidente",$"" },
                    {"idBandeira",$"" },
                    {"id",$"" }
                };

                var dataList = new List<string>() { };

                dataList = dataContext.GetDatas("Partidos", obj);

                if (dataList.Count > 0)
                {
                    obj = new Dictionary<string, string>
                    {
                        {"id", $"'{Convert.ToInt32(dataList[1])}'" },
                        {"caminho", $"" }
                    };

                    var _obj = new Dictionary<string, string>
                    {
                        {"id", $"'{Convert.ToInt32(dataList[0])}'" },
                        {"nome", $"" }
                    };

                    var _obj_ = new Dictionary<string, string>
                    {
                        {"id", $"'{Convert.ToInt32(dataList[0])}'" },
                        {"idFoto", $"" }
                    };


                    _partido = new partidos
                    {
                        nome = comboBox.Text,
                        idBandeira = Convert.ToInt32(dataList[1]),
                        id = Convert.ToInt32(dataList[2]),

                        imagem = new Imagem { caminho = dataContext.GetData("imagem", obj) },

                        presidentes = new Presidentes
                        {
                            id = Convert.ToInt32(dataList[0]),
                            nome = dataContext.GetData("presidentes", _obj),
                            imagem = new Imagem { id = Convert.ToInt32(dataContext.GetData("presidentes", _obj_)) },
                        }
                    };

                    obj["id"] = $"'{_partido.presidentes.imagem.id}'";
                    _partido.presidentes.imagem.caminho = dataContext.GetData("imagem", obj);

                    pictureBoxFotoPR.ImageLocation = _partido.presidentes.imagem.caminho;
                    pictureBoxFotoPartido.ImageLocation = _partido.imagem.caminho;

                    lblNome.Text = _partido.presidentes.nome;
                    lblPartido.Text = _partido.nome;
                }
            }
            catch (Exception)
            {

                throw;
            }
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

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNome.Text)) lblMensagem2.Text = "Preencha o código do eleitor é obrigatorio!";
            else if (string.IsNullOrEmpty(comboBox.Text)) lblMensagem2.Text = "Selecione o o partido de votação é obrigatorio!";
            else if (!string.IsNullOrEmpty(txtNome.Text) && !string.IsNullOrEmpty(comboBox.Text))
            {
                lblMensagem2.Text = string.Empty;

                if (listDatas.Count > 0)
                {


                    if (configVotacao.dataComeco.Equals(ataAtual))
                    {
                        if (Convert.ToDateTime(horaAtual) <= Convert.ToDateTime(configVotacao.horaTermino))
                        {
                            if (Convert.ToDateTime(horaAtual) >= Convert.ToDateTime(configVotacao.horaInicio))
                            {
                                var _eleitores = new eleitores
                                {
                                    codVoto = txtNome.Text.Trim()
                                };
                                var obj = new Dictionary<string, string>
                                {
                                    {"codVoto", $"'{_eleitores.codVoto}'" }
                                };

                                if (dataContext.GetData("Eleitores", obj) == _eleitores.codVoto)
                                {
                                    obj = new Dictionary<string, string>
                                {
                                    {"codVoto", $"'{_eleitores.codVoto}'" },
                                    {"id", $"" }
                                };
                                    _eleitores.id = Convert.ToInt32(dataContext.GetData("Eleitores", obj));

                                    if (_eleitores.id > 0)
                                    {
                                        obj = new Dictionary<string, string>
                                    {
                                        {"idEleitor",$"'{_eleitores.id}'" },
                                        {"idConfig","" },
                                        {"and",$"'{configVotacao.id}'" }
                                    };

                                        if (dataContext.GetData("Votacao", obj) == "Not")
                                        {
                                            obj = new Dictionary<string, string>
                                        {
                                            {"idPartido",$"'{_partido.id}'" },
                                            {"idEleitor",$"'{_eleitores.id}'" },
                                            {"idConfig",$"'{configVotacao.id}'" },
                                            {"dataCriacao",$"'{DateTime.Now.ToString("yyy:MM:dd")}'" }
                                        };
                                            if (dataContext.add("Votacao", obj) == "Ok")
                                            {
                                                txtNome.Text = comboBox.Text = string.Empty;
                                                timer1.Enabled = true;
                                                ShowHide(true);
                                            }
                                        }
                                        else lblMensagem2.Text = $"Caro eleitor para o ano de {DateTime.Now.Year} o seu voto já se encotra registrado.";
                                    }
                                }
                                else lblMensagem2.Text = $"Caro eleitor este código '{_eleitores.codVoto}' de votação é invalido.\n Por favor verifique o seu código e tente novamente. ";
                            }
                            else
                                lblMensagem2.Text = $"A emissão de votos será realizada neste horario: {configVotacao.horaInicio} ";
                        }
                        else
                            lblMensagem2.Text = $"Sessão de emissão de votos encontra-se encerrada \nporque a hora de termino desta sessão chegou ao fim\n Hora de Termino {configVotacao.horaTermino} ";
                    }
                    else lblMensagem2.Text = $"Não é possível começar a votação devido o intervalo das datas\n Data de começo: {configVotacao.dataComeco} and data atual: {ataAtual}";
                }
                else lblMensagem2.Text = $"Não é possível realizar a votação.\n Por favor va até a área de configuração e faça a configuraçao";

            }
        }


    }
}
