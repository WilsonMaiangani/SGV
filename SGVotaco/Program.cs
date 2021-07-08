using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SGVotaco.views;
using SGVotaco.views.form;
using SGVotaco.views.form.presidente;
using SGVotaco.views.form.partido;
using SGVotaco.views.form.eleitore;
using SGVotaco.views.form.votacao;

namespace SGVotaco
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainVotacao());
        }
    }
}
