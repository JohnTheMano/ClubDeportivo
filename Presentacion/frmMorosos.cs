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
        public partial class frmMorosos : Form
        {
            public frmMorosos()
            {
                InitializeComponent();
            }

            private Form formAnterior;

            public frmMorosos(Form frmAnterior)
            {
                InitializeComponent();
                formAnterior = frmAnterior;
            }


        

            private void btnCargarMorosos_Click_1(object sender, EventArgs e)
            {
                try
                {
                using (var conexion = new Conexion().ObtenerConexion())
                    {
                        conexion.Open();

                        string query = @"
                SELECT s.idSocio, p.nombre, p.apellido, p.dni, c.monto, c.fechaVencimiento
                FROM socio s
                INNER JOIN persona p ON s.idPersona = p.id
                INNER JOIN cuota c ON s.idSocio = c.idSocio
                WHERE c.fechaVencimiento < CURDATE()
                ORDER BY c.fechaVencimiento ASC;";

                        MySqlDataAdapter da = new MySqlDataAdapter(query, conexion);
                        DataTable tabla = new DataTable();
                        da.Fill(tabla);

                        dgvMorosos.DataSource = tabla;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error al cargar morosos: " + ex.Message);
                }

            }

        

        



            private void btnVolver_Click(object sender, EventArgs e)
            {
                if (formAnterior != null)
                {
                    formAnterior.Show();
                    this.Close();
                }
                else
                {
                    // Si no hay formulario anterior (por ejemplo, si se abrió directo desde Program.cs)
                    MessageBox.Show("No hay una ventana anterior a la que volver.");
                    this.Close();
                }
            }

        }
    }