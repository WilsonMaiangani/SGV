using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGVotaco.config;
using SGVotaco.models;
using SGVotaco.controllers;

namespace SGVotaco.views.form
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();

        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = new Usuario { email = txtEmail.Text.Trim(), senha = txtSenha.Text.Trim() };

                if (string.IsNullOrEmpty(usuario.email) && string.IsNullOrEmpty(usuario.senha)) { txtEmail.Focus(); lblMensagem.Text = "Preencha os dois campos obrigatorio."; }

                else if (string.IsNullOrEmpty(usuario.email)) { txtEmail.Focus(); lblMensagem.Text = "Preencha o campo email obrigatorio."; }

                else if (string.IsNullOrEmpty(usuario.senha)) { txtSenha.Focus(); lblMensagem.Text = "Preencha o campo senha obrigatorio."; }

                else if (!string.IsNullOrEmpty(usuario.email) && !string.IsNullOrEmpty(usuario.senha))
                {
                    lblMensagem.Text = string.Empty;

                    var dataContext = new DataContext();
                    dataContext.mensagem = dataContext.login(usuario);

                    if (dataContext.mensagem == "Ok")
                    {
                        var _main = new main();
                        _main.Show();
                        this.Hide();
                    }
                    else lblMensagem2.Text = dataContext.mensagem;
                }

            }
            catch (System.Exception ex)
            {
                // TODO
            }
        }


    }
}
