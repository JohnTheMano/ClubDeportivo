using ClubDeportivo.Datos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubDeportivo.Presentacion
{
    public partial class frmPruebaConexion : Form
    {
        public frmPruebaConexion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conexion = null;

            try
            {
                // Crear instancia de tu clase Conexion
                Conexion conexionBD = new Conexion();
                conexion.Open();
                MessageBox.Show("✅ Conexión exitosa a la base de datos 'clubdeportivo'!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error de conexión: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                    conexion.Close();
            }

        }
    }
}
