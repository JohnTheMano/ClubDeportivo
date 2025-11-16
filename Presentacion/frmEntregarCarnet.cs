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
    public partial class frmEntregarCarnet : Form
    {
        public frmEntregarCarnet()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lblNombre.Text = "";
            lblEstado.Text = "";
            lblCuotas.Text = "";
            lblMensaje.Text = "";
            btnEntregar.Enabled = false; // Deshabilitamos el botón al inicio

            string dni = txtDNI.Text.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                lblMensaje.Text = "Ingrese un DNI válido.";
                return;
            }

            try
            {
                using (var conexion = new ClubDeportivo.Datos.Conexion().ObtenerConexion())
                {
                    conexion.Open();

                    // Consulta para verificar las condiciones
                    string consulta = @"
                SELECT 
                    p.nombre, 
                    p.dni, 
                    s.tieneCarnet, 
                    s.estado, 
                    SUM(c.monto) AS deudaPendiente
                FROM persona p
                INNER JOIN socio s ON p.id = s.idPersona
                LEFT JOIN cuota c ON s.idSocio = c.idSocio 
                    AND c.monto > 0
                    AND c.fechaVencimiento < CURDATE()
                WHERE p.dni = @dni
                GROUP BY p.id, s.idSocio
                HAVING s.estado = 1
                    AND s.tieneCarnet = 0
                    AND (SUM(c.monto) = 0 OR SUM(c.monto) IS NULL)";

                    using (var comando = new MySql.Data.MySqlClient.MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@dni", dni);
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                lblNombre.Text = lector["nombre"].ToString();
                                lblEstado.Text = "Activo";
                                lblCuotas.Text = "Cuotas al día.";
                                btnEntregar.Enabled = true; // Si se cumplen todas las condiciones, habilitamos el botón
                            }
                            else
                            {
                                lblMensaje.Text = "El socio tiene deuda pendiente, o ya tiene el carnet.";
                                btnEntregar.Enabled = false; // Deshabilitamos el botón si no cumple las condiciones
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al buscar el socio: " + ex.Message;
            }
        }





        private void btnEntregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Primero obtenemos el DNI ingresado para buscar el idSocio
                string dni = txtDNI.Text.Trim();
                if (string.IsNullOrEmpty(dni))
                {
                    lblMensaje.Text = "Ingrese un DNI válido.";
                    return;
                }

                using (var conexion = new ClubDeportivo.Datos.Conexion().ObtenerConexion())
                {
                    conexion.Open();

                    // Buscamos el idSocio nuevamente
                    string consultaSocio = @"
                SELECT idSocio 
                FROM socio s
                INNER JOIN persona p ON s.idPersona = p.id
                WHERE p.dni = @dni";

                    int idSocio;
                    using (var comando = new MySql.Data.MySqlClient.MySqlCommand(consultaSocio, conexion))
                    {
                        comando.Parameters.AddWithValue("@dni", dni);
                        var resultado = comando.ExecuteScalar();
                        if (resultado == null)
                        {
                            lblMensaje.Text = "No se encontró el socio.";
                            return;
                        }
                        idSocio = Convert.ToInt32(resultado);
                    }

                    // Actualizamos para marcar que tiene el carnet
                    string actualizar = @"UPDATE socio SET tieneCarnet = TRUE WHERE idSocio = @idSocio";
                    using (var comandoActualizar = new MySql.Data.MySqlClient.MySqlCommand(actualizar, conexion))
                    {
                        comandoActualizar.Parameters.AddWithValue("@idSocio", idSocio);
                        comandoActualizar.ExecuteNonQuery();
                    }

                    lblMensaje.Text = "Carnet entregado correctamente.";
                    btnEntregar.Enabled = false; // desactivamos el botón
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al entregar el carnet: " + ex.Message;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
