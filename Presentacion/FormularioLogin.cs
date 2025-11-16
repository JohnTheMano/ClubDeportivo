using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ClubDeportivo.Datos; // Importamos la clase de conexión

namespace ClubDeportivo.Presentacion
{
    public partial class FormularioLogin : Form
    {
        public FormularioLogin()
        {
            InitializeComponent();
        }

        

        private bool ValidarLogin(string usuario, string contrasena)
        {
            // Creamos la conexión con la base de datos
            using (var conexion = new Conexion().ObtenerConexion())
            {
                conexion.Open();

                // Preparamos la consulta SQL para validar las credenciales
                string query = "SELECT COUNT(*) FROM usuario WHERE usuario = @usuario AND contrasena = MD5(@contrasena)";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    // Ejecutamos la consulta y obtenemos el resultado
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    // Si encontramos una coincidencia, el login es válido
                    return count > 0;
                }
            }
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contrasena = txtContrasena.Text;

            if (ValidarLogin(usuario, contrasena))
            {
                // Si el login es correcto, cerramos el formulario de login y continuamos con la aplicación
                this.DialogResult = DialogResult.OK; // Indicamos que el login fue exitoso
                this.Close(); // Cerramos el formulario de login
            }
            else
            {
                // Si los datos son incorrectos, mostramos un mensaje de error
                MessageBox.Show("Usuario o contraseña incorrectos", "Error de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
