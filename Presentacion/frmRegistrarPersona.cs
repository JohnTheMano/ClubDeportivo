using ClubDeportivo.Entidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClubDeportivo.Datos;
using MySql.Data.MySqlClient;

namespace ClubDeportivo.Presentacion
{
    public partial class frmRegistrarPersona : Form
    {
        private List<Persona> listaPersonas = new List<Persona>();
        private List<Socio> listaSocios = new List<Socio>();
        private List<NoSocio> listaNoSocios = new List<NoSocio>();

        private Form formAnterior;

        public frmRegistrarPersona()
        {
            InitializeComponent();
        }

        public frmRegistrarPersona(Form frmAnterior)
        {
            InitializeComponent();
            this.formAnterior = frmAnterior;
        }

        private void btnVolverMenu_Click(object sender, EventArgs e)
        {
            if (formAnterior != null)
            {
                formAnterior.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No hay una ventana anterior a la que volver.");
                this.Close();
            }
        }

        private void btnVolverDNI_Click(object sender, EventArgs e)
        {
            panelDatosPersonales.Visible = false;
            panelDNI.Visible = true;
        }

        private void btnContinuarDNI_Click(object sender, EventArgs e)
        {
            string dni = txtDNI.Text.Trim();
            if (string.IsNullOrWhiteSpace(dni))
            {
                MessageBox.Show("❌ Por favor ingrese un DNI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conexion = new Conexion().ObtenerConexion())
                {
                    conexion.Open();
                    string query = "SELECT COUNT(*) FROM persona WHERE dni = @dni";
                    using (var cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@dni", dni);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("⚠ El cliente ya está registrado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDNI.Clear();
                            return;
                        }
                    }
                }

                panelDNI.Visible = false;
                panelDatosPersonales.Visible = true;
                panelDatosPersonales.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al consultar la base de datos: " + ex.Message);
            }
        }

        private void btnRegistrarPersonaPanel_Click(object sender, EventArgs e)
        {
            // Verificar que todos los campos obligatorios estén completos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDNI.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("❌ Por favor complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si se ha seleccionado si es Socio o NoSocio
            if (!rdSocio.Checked && !rbNoSocio.Checked)
            {
                MessageBox.Show("❌ Seleccione si es Socio o NoSocio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener los valores de los campos
            string dni = txtDNI.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string tipo = rdSocio.Checked ? "Socio" : "NoSocio"; // Determina si es Socio o No Socio

            try
            {
                using (var conexion = new Conexion().ObtenerConexion())
                {
                    conexion.Open();

                    // Insertar la persona en la tabla persona
                    string queryInsertPersona = @"INSERT INTO persona (nombre, apellido, dni, direccion, telefono, tipo)
                                         VALUES (@nombre, @apellido, @dni, @direccion, @telefono, @tipo);
                                         SELECT LAST_INSERT_ID();";
                    int idPersona = 0;
                    using (var cmd = new MySqlCommand(queryInsertPersona, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellido", apellido);
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.Parameters.AddWithValue("@direccion", direccion);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@tipo", tipo);
                        idPersona = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Si es NoSocio
                    if (!rdSocio.Checked) // NoSocio
                    {
                        // Insertar No Socio en la tabla nosocio
                        string queryInsertNoSocio = @"INSERT INTO nosocio (idPersona) VALUES (@idPersona)";
                        using (var cmd = new MySqlCommand(queryInsertNoSocio, conexion))
                        {
                            cmd.Parameters.AddWithValue("@idPersona", idPersona);
                            cmd.ExecuteNonQuery();
                        }

                        NoSocio nuevoNoSocio = new NoSocio(0, idPersona, nombre, apellido, dni, direccion, telefono);
                        listaPersonas.Add(nuevoNoSocio);
                        listaNoSocios.Add(nuevoNoSocio);

                        MessageBox.Show("✅ NoSocio registrado correctamente.");
                    }
                    else // Socio
                    {
                        // Insertar Socio en la tabla socio
                        string queryInsertSocio = @"INSERT INTO socio (idPersona, fechaAlta, tieneCarnet, estado)
                                            VALUES (@idPersona, @fechaAlta, @tieneCarnet, @estado);
                                            SELECT LAST_INSERT_ID();";
                        int idSocio = 0;
                        using (var cmd = new MySqlCommand(queryInsertSocio, conexion))
                        {
                            cmd.Parameters.AddWithValue("@idPersona", idPersona);
                            cmd.Parameters.AddWithValue("@fechaAlta", DateTime.Now.Date);
                            cmd.Parameters.AddWithValue("@tieneCarnet", false); // Se crea sin carnet
                            cmd.Parameters.AddWithValue("@estado", true); // Socio activo
                            idSocio = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Registrar cuota con deuda pendiente
                        string queryInsertCuota = @"INSERT INTO cuota (idSocio, monto, fechaVencimiento, medioPago) 
                                            VALUES (@idSocio, @monto, @fechaVencimiento, @medioPago)";
                        using (var cmd = new MySqlCommand(queryInsertCuota, conexion))
                        {
                            cmd.Parameters.AddWithValue("@idSocio", idSocio);
                            cmd.Parameters.AddWithValue("@monto", 1000); // Deuda inicial
                            cmd.Parameters.AddWithValue("@fechaVencimiento", DateTime.Now.AddMonths(-1).Date); // Fecha de vencimiento pasada (moroso desde el inicio)
                            cmd.Parameters.AddWithValue("@medioPago", "Efectivo"); // Medio de pago (puedes cambiarlo si lo deseas)
                            cmd.ExecuteNonQuery();
                        }

                        // Crear objeto Socio en C#
                        Socio nuevoSocio = new Socio(0, idPersona, nombre, apellido, dni, direccion, telefono, DateTime.Now, false, true);
                        listaPersonas.Add(nuevoSocio);
                        listaSocios.Add(nuevoSocio);

                        MessageBox.Show("✅ Socio registrado correctamente, primera cuota generada con deuda pendiente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al registrar persona: " + ex.Message);
            }

            // Limpiar campos de texto
            txtNombre.Clear();
            txtApellido.Clear();
            txtDNI.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();

            // Volver al panel de DNI
            panelDatosPersonales.Visible = false;
            panelDNI.Visible = true;
        }


        private void btnCobrarCuota_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conexion = new Conexion().ObtenerConexion())
                {
                    conexion.Open();
                    string dni = txtDNI.Text.Trim();
                    string queryBuscar = @"SELECT s.idSocio 
                                           FROM socio s
                                           INNER JOIN persona p ON s.idPersona = p.idPersona
                                           WHERE p.dni = @dni;";
                    MySqlCommand cmdBuscar = new MySqlCommand(queryBuscar, conexion);
                    cmdBuscar.Parameters.AddWithValue("@dni", dni);
                    object resultado = cmdBuscar.ExecuteScalar();

                    if (resultado == null)
                    {
                        MessageBox.Show("⚠ No se encontró ningún socio con ese DNI.");
                        return;
                    }

                    int idSocio = Convert.ToInt32(resultado);

                    string queryMoroso = @"SELECT COUNT(*) FROM cuota WHERE idSocio = @idSocio AND fechaVencimiento < CURDATE()";
                    using (var cmdMoroso = new MySqlCommand(queryMoroso, conexion))
                    {
                        cmdMoroso.Parameters.AddWithValue("@idSocio", idSocio);
                        int moroso = Convert.ToInt32(cmdMoroso.ExecuteScalar());
                        if (moroso > 0)
                        {
                            MessageBox.Show("❌ No se puede entregar el carnet: el socio tiene cuotas vencidas.");
                            return;
                        }
                    }

                    DateTime nuevaFecha = rdSocio.Checked ? DateTime.Now.AddMonths(1) : DateTime.Now.AddDays(1);
                    string queryActualizar = @"UPDATE cuota SET fechaVencimiento = @nuevaFecha WHERE idSocio = @idSocio";
                    using (var cmdActualizar = new MySqlCommand(queryActualizar, conexion))
                    {
                        cmdActualizar.Parameters.AddWithValue("@nuevaFecha", nuevaFecha);
                        cmdActualizar.Parameters.AddWithValue("@idSocio", idSocio);
                        cmdActualizar.ExecuteNonQuery();
                    }

                    string queryActualizarCarnet = "UPDATE socio SET tieneCarnet = 1 WHERE idSocio = @idSocio";
                    using (var cmd = new MySqlCommand(queryActualizarCarnet, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idSocio", idSocio);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"✅ Cuota cobrada correctamente.\n📅 Próximo vencimiento: {nuevaFecha:dd/MM/yyyy}\n🎟 Carnet entregado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al cobrar cuota: " + ex.Message);
            }
        }

        // Métodos vacíos 
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void rdSocio_CheckedChanged(object sender, EventArgs e) { }
        private void panelDNI_Paint(object sender, PaintEventArgs e) { }
        private void frmRegistrarPersona_Click(object sender, EventArgs e) { }
    }
}
