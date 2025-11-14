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
            // Limpiamos los labels y mensaje al empezar
            lblNombre.Text = "";
            lblEstado.Text = "";
            lblCuotas.Text = "";
            lblMensaje.Text = "";
            btnEntregar.Enabled = false;

            // Leer el DNI ingresado
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

                    // Buscar datos del socio por DNI
                    string consulta = @"
                SELECT s.idSocio, s.estado, p.nombre, p.apellido
                FROM socio s
                INNER JOIN persona p ON s.idPersona = p.id
                WHERE p.dni = @dni";

                    using (var comando = new MySql.Data.MySqlClient.MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@dni", dni);

                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                // Obtenemos el idSocio
                                int idSocio = Convert.ToInt32(lector["idSocio"]);

                                // Mostramos info del socio
                                lblNombre.Text = lector["nombre"].ToString() + " " + lector["apellido"].ToString();
                                bool estado = Convert.ToBoolean(lector["estado"]);
                                lblEstado.Text = estado ? "Activo" : "Inactivo";

                                lector.Close(); // cerramos antes de la segunda consulta

                                // Verificar cuotas vencidas
                                string consultaCuotas = @"
                            SELECT COUNT(*) 
                            FROM cuota 
                            WHERE idSocio = @idSocio 
                              AND fechaVencimiento < CURDATE()";

                                using (var comandoCuotas = new MySql.Data.MySqlClient.MySqlCommand(consultaCuotas, conexion))
                                {
                                    comandoCuotas.Parameters.AddWithValue("@idSocio", idSocio);
                                    int cuotasVencidas = Convert.ToInt32(comandoCuotas.ExecuteScalar());

                                    if (cuotasVencidas > 0)
                                    {
                                        lblCuotas.Text = $"Tiene {cuotasVencidas} cuota(s) vencida(s).";
                                        btnEntregar.Enabled = false; // no puede entregar carnet
                                    }
                                    else
                                    {
                                        lblCuotas.Text = "Cuotas al día.";
                                        // Solo habilitamos botón si está activo
                                        btnEntregar.Enabled = estado;
                                    }
                                }
                            }
                            else
                            {
                                lblMensaje.Text = "No se encontró el socio con ese DNI.";
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
