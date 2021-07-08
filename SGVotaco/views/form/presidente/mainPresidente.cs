using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGVotaco.views.form.presidente;

namespace SGVotaco.views.form.presidente
{
    public partial class mainPresidente : Form
    {
        public mainPresidente()
        {
            InitializeComponent();
        }

        private Form form;
        private void btnBack_Click(object sender, EventArgs e)
        {
            panelForm.Controls.Clear();
            panel1.Controls.Clear();
            form = new mainMenu { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm.Controls.Add(form);
            form.Show();
        }

        private void btnAddPresidente_Click(object sender, EventArgs e)
        {
            panelForm2.Controls.Clear();
            
            form = new addPresidente { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm2.Controls.Add(form);
            form.Show();
        }

        private void btnListPresidente_Click(object sender, EventArgs e)
        {
            panelForm2.Controls.Clear();

            form = new listPresidente { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm2.Controls.Add(form);
            form.Show();
        }
    }
}
