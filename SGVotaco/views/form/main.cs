using SGVotaco.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SGVotaco.views.form
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        private Form form;
        private void main_Load(object sender, EventArgs e)
        {
            var usuario = new Usuario();
            lblNomeUsuario.Text = usuario.nome;

            panelForm.Controls.Clear();
            form = new mainMenu { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm.Controls.Add(form);
            form.Show();
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            var usuario = new Usuario { nome = string.Empty, id = 0 };
            var login = new  login();
            login.Show();
            this.Hide();
        }
    }
}
