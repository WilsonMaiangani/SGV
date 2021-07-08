using SGVotaco.controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SGVotaco.views.form.votacao
{
    public partial class listVotacao : Form
    {
        public listVotacao()
        {
            InitializeComponent();
        }

        private void listVotacao_Load(object sender, EventArgs e)
        {
            GetListData();
        }

        private void GetListData()
        {
            var dataContext = new DataContext();

            dataContext.GridView(dataGridView1, "GetVotaco");
            //dataGridView1.Columns["Eleitor"].Width = 200;
            //dataGridView1.Columns["CodVoto"].Width = 200;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            GetListData();
        }
    }
}
