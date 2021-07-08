using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SGVotaco.views.form.votacao
{
    public partial class mainVotacao : Form
    {
        public mainVotacao()
        {
            InitializeComponent();
        }
        Form form;
        private void btnBack_Click(object sender, EventArgs e)
        {
            panelForm.Controls.Clear();
            panelForm.Controls.Clear();
            form = new mainMenu { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm.Controls.Add(form);
            form.Show();
        }

        private void btnAddVotacao_Click(object sender, EventArgs e)
        {
            panelForm2.Controls.Clear();

            form = new addVotacao { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm2.Controls.Add(form);
            form.Show();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            panelForm2.Controls.Clear();

            form = new configVotacao { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm2.Controls.Add(form);
            form.Show();
        }

        private void btnListEleitor_Click(object sender, EventArgs e)
        {
            panelForm2.Controls.Clear();

            form = new listVotacao { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm2.Controls.Add(form);
            form.Show();
        }
    }
}
