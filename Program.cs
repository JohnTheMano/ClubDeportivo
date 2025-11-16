using ClubDeportivo.Presentacion;
using System;
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

            // Creamos el formulario de login
            FormularioLogin loginForm = new FormularioLogin();

            // Mostramos el formulario de login, y si el login es correcto, se abre el formulario principal
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Si el login es correcto, mostramos el formulario principal
                Application.Run(new frmPrincipal());
            }
            else
            {
                // Si el login es incorrecto, cerramos la aplicación
                Application.Exit();
            }
        }
    }
}
