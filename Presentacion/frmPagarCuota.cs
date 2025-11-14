using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ClubDeportivo.Presentacion
{
    public partial class frmPagarCuota : Form
    {
        private int idSocio = 0;
        private int idNoSocio = 0;
        private bool esSocio = false;

        public frmPagarCuota()
        {
            InitializeComponent();

            // Deshabilitamos controles al inicio
            nudMonto.Enabled = false;
            rbEfectivo.Enabled = false;
            rbTarjeta.Enabled = false;
            rbTransferencia.Enabled = false;
            dtpFechaPago.Enabled = false;
            btnRegistrarPago.Enabled = false;
            btnImprimirComprobante.Enabled = false;
            cmbActividad.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Limpiar mensajes y nombre
            lblNombre.Text = "";
            lblMensaje.Text = "";
            cmbActividad.Items.Clear();
            cmbActividad.Enabled = false;

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

                    // Buscar socio
                    string consultaSocio = @"SELECT s.idSocio, p.nombre, p.apellido 
                                             FROM socio s
                                             INNER JOIN persona p ON s.idPersona = p.id
                                             WHERE p.dni = @dni";
                    using (var comando = new MySqlCommand(consultaSocio, conexion))
                    {
                        comando.Parameters.AddWithValue("@dni", dni);
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                idSocio = Convert.ToInt32(lector["idSocio"]);
                                esSocio = true;
                                lblNombre.Text = lector["nombre"].ToString() + " " + lector["apellido"].ToString();
                                lblMensaje.Text = "Es socio. ID: " + idSocio;
                                lector.Close();

                                HabilitarCampos();
                                return;
                            }
                        }
                    }

                    // Buscar no socio
                    string consultaNoSocio = @"SELECT n.idNoSocio, p.nombre, p.apellido 
                                               FROM nosocio n
                                               INNER JOIN persona p ON n.idPersona = p.id
                                               WHERE p.dni = @dni";
                    using (var comando = new MySqlCommand(consultaNoSocio, conexion))
                    {
                        comando.Parameters.AddWithValue("@dni", dni);
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                idNoSocio = Convert.ToInt32(lector["idNoSocio"]);
                                esSocio = false;
                                lblNombre.Text = lector["nombre"].ToString() + " " + lector["apellido"].ToString();
                                lblMensaje.Text = "Es no socio. ID: " + idNoSocio;
                                lector.Close();

                                HabilitarCampos();

                                // Cargar actividades para no socios
                                cmbActividad.Enabled = true;
                                CargarActividades();
                                return;
                            }
                            else
                            {
                                lblMensaje.Text = "No se encontró la persona.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al buscar: " + ex.Message;
            }
        }

        private void HabilitarCampos()
        {
            nudMonto.Enabled = true;
            rbEfectivo.Enabled = true;
            rbTarjeta.Enabled = true;
            rbTransferencia.Enabled = true;
            dtpFechaPago.Enabled = true;
            btnRegistrarPago.Enabled = true;
            btnImprimirComprobante.Enabled = true;
        }

        private void CargarActividades()
        {
            try
            {
                using (var conexion = new ClubDeportivo.Datos.Conexion().ObtenerConexion())
                {
                    conexion.Open();
                    string consulta = "SELECT idActividad, nombre FROM actividad";
                    using (var comando = new MySqlCommand(consulta, conexion))
                    {
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                cmbActividad.Items.Add(new ComboboxItem
                                {
                                    Text = lector["nombre"].ToString(),
                                    Value = Convert.ToInt32(lector["idActividad"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar actividades: " + ex.Message;
            }
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            if (nudMonto.Value <= 0)
            {
                lblMensaje.Text = "Ingrese un monto válido.";
                return;
            }

            string medioPago = rbEfectivo.Checked ? "Efectivo" :
                               rbTarjeta.Checked ? "Tarjeta" :
                               rbTransferencia.Checked ? "Transferencia" : "";

            if (string.IsNullOrEmpty(medioPago))
            {
                lblMensaje.Text = "Seleccione un medio de pago.";
                return;
            }

            try
            {
                using (var conexion = new ClubDeportivo.Datos.Conexion().ObtenerConexion())
                {
                    conexion.Open();

                    if (esSocio)
                    {
                        string insertarCuota = @"INSERT INTO cuota (idSocio, monto, fechaVencimiento, medioPago)
                                                 VALUES (@idSocio, @monto, @fecha, @medio)";
                        using (var comando = new MySqlCommand(insertarCuota, conexion))
                        {
                            comando.Parameters.AddWithValue("@idSocio", idSocio);
                            comando.Parameters.AddWithValue("@monto", nudMonto.Value);
                            comando.Parameters.AddWithValue("@fecha", dtpFechaPago.Value.Date);
                            comando.Parameters.AddWithValue("@medio", medioPago);
                            comando.ExecuteNonQuery();
                        }
                        lblMensaje.Text = "Pago de cuota registrado correctamente.";
                    }
                    else
                    {
                        if (cmbActividad.SelectedItem == null)
                        {
                            lblMensaje.Text = "Seleccione una actividad.";
                            return;
                        }

                        int idActividad = ((ComboboxItem)cmbActividad.SelectedItem).Value;
                        string insertarPago = @"INSERT INTO pago_eventual (idNoSocio, idActividad, monto, medioPago, fechaPago)
                                                VALUES (@idNoSocio, @idActividad, @monto, @medio, @fecha)";
                        using (var comando = new MySqlCommand(insertarPago, conexion))
                        {
                            comando.Parameters.AddWithValue("@idNoSocio", idNoSocio);
                            comando.Parameters.AddWithValue("@idActividad", idActividad);
                            comando.Parameters.AddWithValue("@monto", nudMonto.Value);
                            comando.Parameters.AddWithValue("@medio", medioPago);
                            comando.Parameters.AddWithValue("@fecha", dtpFechaPago.Value.Date);
                            comando.ExecuteNonQuery();
                        }
                        lblMensaje.Text = "Pago de actividad registrado correctamente.";
                    }

                    // Habilitar imprimir comprobante
                    btnImprimirComprobante.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al registrar pago: " + ex.Message;
            }
        }
    }

    // Clase auxiliar para manejar ComboBox con valor
    public class ComboboxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public override string ToString() => Text;
    }
}
