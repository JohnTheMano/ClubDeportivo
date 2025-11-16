using ClubDeportivo.Datos;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

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

                                // Calcular la deuda pendiente
                                decimal deudaPendiente = CalcularDeuda(idSocio);
                                lblDeuda.Text = $"Deuda pendiente: ${deudaPendiente}";  // Mostramos la deuda

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

                    // Verificar deuda total de cuotas vencidas antes del pago
                    decimal totalDeuda = 0;
                    using (var cmdDeuda = new MySqlCommand(@"
                SELECT SUM(monto) 
                FROM cuota
                WHERE idSocio = @idSocio AND fechaVencimiento < CURDATE() AND monto > 0;
            ", conexion))
                    {
                        cmdDeuda.Parameters.AddWithValue("@idSocio", idSocio);
                        var result = cmdDeuda.ExecuteScalar();
                        totalDeuda = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    }

                    if (totalDeuda > 0)
                    {
                        // Registrar pago en la tabla cuota
                        string insertarPago = @"INSERT INTO cuota (idSocio, monto, fechaVencimiento, medioPago)
                                        VALUES (@idSocio, @monto, @fecha, @medio)";
                        using (var comando = new MySqlCommand(insertarPago, conexion))
                        {
                            comando.Parameters.AddWithValue("@idSocio", idSocio);
                            comando.Parameters.AddWithValue("@monto", nudMonto.Value);
                            var fechaVencimiento = dtpFechaPago.Value.Date.AddMonths(1); // Vencimiento para la nueva cuota
                            comando.Parameters.AddWithValue("@fecha", fechaVencimiento);
                            comando.Parameters.AddWithValue("@medio", medioPago);
                            comando.ExecuteNonQuery();
                        }

                        // Procesar la deuda y descontar el pago realizado
                        decimal montoRestante = nudMonto.Value;

                        // Descontar el monto de las cuotas vencidas
                        string actualizarCuotas = @"
                    UPDATE cuota 
                    SET monto = GREATEST(monto - @montoPago, 0) -- Resta el pago, no puede ser menor que 0
                    WHERE idSocio = @idSocio AND fechaVencimiento < CURDATE() AND monto > 0
                    LIMIT 1";
                        using (var comandoActualizarCuotas = new MySqlCommand(actualizarCuotas, conexion))
                        {
                            comandoActualizarCuotas.Parameters.AddWithValue("@idSocio", idSocio);
                            comandoActualizarCuotas.Parameters.AddWithValue("@montoPago", montoRestante);
                            comandoActualizarCuotas.ExecuteNonQuery();
                        }

                        // Recalcular la deuda restante después de descontar el pago
                        string consultaDeudaRestante = @"
                    SELECT SUM(monto) 
                    FROM cuota 
                    WHERE idSocio = @idSocio AND fechaVencimiento < CURDATE() AND monto > 0";
                        using (var cmdDeudaRestante = new MySqlCommand(consultaDeudaRestante, conexion))
                        {
                            cmdDeudaRestante.Parameters.AddWithValue("@idSocio", idSocio);
                            var deudaRestante = cmdDeudaRestante.ExecuteScalar();
                            decimal deudaTotalRestante = deudaRestante != DBNull.Value ? Convert.ToDecimal(deudaRestante) : 0;

                            // Mostrar el mensaje adecuado
                            if (deudaTotalRestante <= 0)
                            {
                                lblMensaje.Text = "Pago registrado correctamente. La deuda ha sido saldada.";
                            }
                            else
                            {
                                lblMensaje.Text = $"Pago registrado. Deuda restante: ${deudaTotalRestante}.";
                            }

                            // Actualizamos el lblDeuda para mostrar la deuda actualizada
                            lblDeuda.Text = $"Deuda pendiente: ${deudaTotalRestante}";
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "No hay deuda pendiente.";
                    }

                    // Habilitar la opción de imprimir comprobante solo si se realizó el pago
                    btnImprimirComprobante.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al registrar pago: " + ex.Message;
            }
        }


        private void lblMensaje_Click(object sender, EventArgs e)
        {

        }

        private void lblNombre_Click(object sender, EventArgs e)
        {

        }
        private decimal CalcularDeuda(int idSocio)
        {
            decimal deudaPendiente = 0;

            try
            {
                using (var conexion = new ClubDeportivo.Datos.Conexion().ObtenerConexion())
                {
                    conexion.Open();

                    string consultaDeuda = @"
                SELECT SUM(c.monto) 
                FROM cuota c 
                WHERE c.idSocio = @idSocio AND c.fechaVencimiento < CURDATE() AND c.monto > 0";
                    using (var comando = new MySqlCommand(consultaDeuda, conexion))
                    {
                        comando.Parameters.AddWithValue("@idSocio", idSocio);
                        var resultado = comando.ExecuteScalar();
                        deudaPendiente = resultado != DBNull.Value ? Convert.ToDecimal(resultado) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                lblDeuda.Text = "Error al calcular deuda: " + ex.Message;
            }

            return deudaPendiente;
        }

        private void lblDeuda_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí puedes agregar la lógica que quieres que ocurra cuando cambie la selección
            MessageBox.Show("Seleccionaste: " + comboBox1.SelectedItem.ToString());
        }

        private void rbTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            // Si el usuario selecciona "Tarjeta", mostramos el ComboBox
            if (rbTarjeta.Checked)
            {
                comboBox1.Visible = true;  // Hacemos visible el ComboBox
            }
            else
            {
                comboBox1.Visible = false;  // Lo ocultamos si no es Tarjeta
            }
        }

        private void cmbActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica si la actividad seleccionada es "Gimnasio"
            if (cmbActividad.SelectedItem.ToString() == "Gimnasio")
            {
                // Habilitar el CheckBox de Apto Físico
                checkBoxAptoFisico.Enabled = true;  // Habilitar
                checkBoxAptoFisico.Visible = true;  // Hacerlo visible

            }
            else
            {
                // Deshabilitar el CheckBox de Apto Físico
                checkBoxAptoFisico.Enabled = false;  // Deshabilitar
                checkBoxAptoFisico.Visible = false;  // Ocultarlo
            }
        }

        private void frmPagarCuota_Load(object sender, EventArgs e)
        {
            try
            {
                using (var conexion = new Conexion().ObtenerConexion())
                {
                    conexion.Open();

                    // Consulta para obtener las actividades
                    string query = "SELECT nombre FROM actividad";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conexion);
                    DataTable tabla = new DataTable();
                    da.Fill(tabla);

                    cmbActividad.Items.Clear(); // Limpiar el ComboBox antes de agregar las actividades

                    // Agregar las actividades al ComboBox
                    foreach (DataRow row in tabla.Rows)
                    {
                        cmbActividad.Items.Add(row["nombre"].ToString());
                    }

                    cmbActividad.SelectedItem = null; // Dejar el ComboBox vacío al principio
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al cargar actividades: " + ex.Message);
            }
        }

        private void checkBoxAptoFisico_CheckedChanged(object sender, EventArgs e)
        {
            // Cuando el CheckBox cambia de estado
            if (checkBoxAptoFisico.Checked)
            {
                MessageBox.Show("¡Apto físico habilitado!");
            }
            else
            {
                MessageBox.Show("¡Apto físico deshabilitado!");
            }
        }

        private void btnImprimirComprobante_Click(object sender, EventArgs e)
        {
            // Obtener los datos para el comprobante
            string nombreSocio = lblNombre.Text;  // El nombre del socio
            string actividadSeleccionada = cmbActividad.SelectedItem != null ? cmbActividad.SelectedItem.ToString() : "Ninguna actividad seleccionada";
            decimal montoPagado = nudMonto.Value;
            string medioPago = rbEfectivo.Checked ? "Efectivo" :
                               rbTarjeta.Checked ? "Tarjeta" :
                               rbTransferencia.Checked ? "Transferencia" : "No seleccionado";
            bool aptoFisicoSeleccionado = checkBoxAptoFisico.Checked;

            // Crear el texto para el comprobante
            string comprobante = "Comprobante de Pago\n\n";
            comprobante += $"Socio: {nombreSocio}\n";
            comprobante += $"Actividad: {actividadSeleccionada}\n";
            comprobante += $"Monto Pagado: ${montoPagado}\n";
            comprobante += $"Medio de Pago: {medioPago}\n";
            comprobante += $"Apto Físico: {(aptoFisicoSeleccionado ? "Sí" : "No")}\n";
            comprobante += $"\nFecha: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}\n";

            // Mostrar el comprobante en un MessageBox
            MessageBox.Show(comprobante, "Comprobante de Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
