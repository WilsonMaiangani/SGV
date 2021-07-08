using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SGVotaco.views.form.partido
{
    public partial class mainPartido : Form
    {
        public mainPartido()
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

        private void btnAddPartido_Click(object sender, EventArgs e)
        {
            panelForm2.Controls.Clear();

            form = new addPartido { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm2.Controls.Add(form);
            form.Show();
        }

        private void btnListPartido_Click(object sender, EventArgs e)
        {
            panelForm2.Controls.Clear();

            form = new listPartido { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelForm2.Controls.Add(form);
            form.Show();
        }
    }
}
