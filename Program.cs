using ClubDeportivo.Presentacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubDeportivo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 👇 Ahora inicia directamente en el formulario principal
            Application.Run(new frmPrincipal());

        }
    }
}
