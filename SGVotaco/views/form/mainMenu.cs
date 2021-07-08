using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGVotaco.views.form.presidente;
using SGVotaco.views.form.partido;
using SGVotaco.views.form.eleitore;
using SGVotaco.views.form.votacao;

namespace SGVotaco.views.form
{
    public partial class mainMenu : Form
    {
        public mainMenu()
        {
            InitializeComponent();
        }

        private Form form;
        private void btnPresidente_Click(object sender, EventArgs e)
        {
            panelForm.Controls.Clear();
            form = new mainPresidente { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill};
            panelForm.Controls.Add(form);
            form.Show();
        }

        private void btnPartido_Click(object sender, EventArgs e)
        {
            panelForm.Controls.Clear();
            form = new mainPartido { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm.Controls.Add(form);
            form.Show();
        }

        private void btnEleitor_Click(object sender, EventArgs e)
        {
            panelForm.Controls.Clear();
            form = new mainEleitor { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm.Controls.Add(form);
            form.Show();
        }

        private void btnVotacao_Click(object sender, EventArgs e)
        {
            panelForm.Controls.Clear();
            form = new mainVotacao { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm.Controls.Add(form);
            form.Show();
        }
    }
}
