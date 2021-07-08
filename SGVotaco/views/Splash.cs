using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGVotaco.views.form;


namespace SGVotaco.views
{
 public partial class Splash : Form
 {
  public Splash()
  {
   InitializeComponent();
  }

  private int cont = 0;
  private void timer1_Tick(object sender, EventArgs e)
  {
   try
   {
    cont++;

    labelCont.Text = cont.ToString() + "%";

    if(cont == 100)
    {
        timer1.Enabled = false;
        var login = new login();
        login.Show();
        this.Hide();
    }
   }
   catch (System.Exception ex)
   {
    // TODO
   }
  }
 }
}
