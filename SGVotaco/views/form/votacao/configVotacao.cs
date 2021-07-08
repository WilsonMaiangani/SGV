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
    public partial class configVotacao : Form
    {
        public configVotacao()
        {
            InitializeComponent();
        }

        private void ShowHide(bool logic)
        {
            pictureBoxCheckmark.Visible = lblSucesso.Visible = logic;
        }

        private void txtAno_KeyPress(object sender, KeyPressEventArgs e)
        {
            string value = "0123456789";
            if (!value.Contains(e.KeyChar) && (int)e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {


            if (Convert.ToDateTime(dateTimePickerData.Value.ToShortDateString()) >= Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                if (Convert.ToDateTime(dateTimePickerHoraInic.Value.ToShortTimeString()) == Convert.ToDateTime(dateTimePickerHoraTermn.Value.ToShortTimeString()))
                    lblMensagem2.Text = "Hora de começo não pode ser igual a hora de termino";
                else
                {
                    if (Convert.ToDateTime(dateTimePickerHoraTermn.Value.ToShortTimeString()) > Convert.ToDateTime(dateTimePickerHoraInic.Value.ToShortTimeString()))
                    {
                        lblMensagem2.Text = string.Empty;

                        var dataContext = new DataContext();
                        var _ConfigVotacao = new ConfigVotacao
                        {
                            dataComeco = dateTimePickerData.Value.ToString("yyy:MM:dd"),
                            horaInicio = dateTimePickerHoraInic.Value.ToString("T"),
                            horaTermino = dateTimePickerHoraTermn.Value.ToString("T")
                        };

                        var obj = new Dictionary<string, string>
                        {
                            {"dataComeco", $"'{_ConfigVotacao.dataComeco}'" }
                        };


                        if (dataContext.GetData("ConfigVotacao", obj) == "Not")
                        {
                            obj = new Dictionary<string, string>
                            {
                                {"dataComeco", $"'{_ConfigVotacao.dataComeco}'" },
                                {"horaInicio", $"'{_ConfigVotacao.horaInicio}'" },
                                {"horaTermino", $"'{_ConfigVotacao.horaTermino}'" }
                            };
                            if (dataContext.add("ConfigVotacao", obj) == "Ok")
                            {
                                _ConfigVotacao.id = dataContext.getLastId("ConfigVotacao");
                                obj = new Dictionary<string, string>
                                {
                                    {"id", $"" },
                                    {"op", $"!=" },
                                    {"value", $"'{_ConfigVotacao.id}'" },
                                    {"estado", $"'{ConfigVotacao.Estado.OFF}'" }
                                };
                                dataContext.Update("ConfigVotacao", obj);

                                GetDGV();
                                dateTimePickerData.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                dateTimePickerHoraTermn.Value = dateTimePickerHoraInic.Value = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
                                timer1.Enabled = true;
                                ShowHide(true);

                            }
                        }
                        else lblMensagem2.Text = "Data de começo já existente.";
                    }
                    else lblMensagem2.Text = "Hora de termino não pode ser menor que a hora de começo.";
                }
            }
            else lblMensagem2.Text = "Data de começo não pode ser menor que a data atual.";

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

        private void GetDGV()
        {
            var dataContext = new DataContext();

            dataContext.GridView(dataGridView1, "GetConfigVotacao");
            dataGridView1.Columns["datacomeco"].Width = 150;
            dataGridView1.Columns["horaInicio"].Width = 150;
            dataGridView1.Columns["horaTermino"].Width = 150;
            dataGridView1.Columns["estado"].Width = 150;
        }
        private void configVotacao_Load(object sender, EventArgs e)
        {
            GetDGV();
        }
    }


    //private void GetAno()
    //{

    //    var obj = new Dictionary<string, string>
    //    {
    //        {"ano","*" }
    //    };

    //    var dataContext = new DataContext();
    //    var dataList = new List<string>() { };


    //    dataList = dataContext.GetDatas("AnoVotacao", obj);
    //    if (dataList.Count > 0) comboBox.Items.AddRange(dataList.ToArray());
    //}
    //bool verf;
    //private void btnVerAno_Click(object sender, EventArgs e)
    //{
    //    if (!verf)
    //    {
    //        verf = true;
    //        panelVerAno.Visible = verf;
    //        GetAno();
    //        lblMensagem3.Text = string.Empty;
    //    }
    //    else if (verf)
    //    {
    //        verf = false;
    //        panelVerAno.Visible = verf;
    //    }

    //}

    //private void btnStart_Click(object sender, EventArgs e)
    //{
    //    if (string.IsNullOrEmpty(comboBox.Text)) lblMensagem3.Text = "Selecione o ano obrigatorio!";
    //    {

    //    }
    //    //verf = false;
    //    //panelVerAno.Visible = verf;
    //}

}
