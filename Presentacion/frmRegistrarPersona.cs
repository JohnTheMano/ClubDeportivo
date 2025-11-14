    using ClubDeportivo.Entidades;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using ClubDeportivo.Datos;  // para usar la clase Conexion
    using MySql.Data.MySqlClient; // para usar MySqlCommand



    namespace ClubDeportivo.Presentacion
    {
        public partial class frmRegistrarPersona : Form
        {
            // Listas en memoria
            private List<Persona> listaPersonas = new List<Persona>();
            private List<Socio> listaSocios = new List<Socio>();
            private List<NoSocio> listaNoSocios = new List<NoSocio>();


            public frmRegistrarPersona()
            {
                InitializeComponent();
            }

            private void button1_Click(object sender, EventArgs e)
            {
                // Validación de campos
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtDNI.Text) ||
                    string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    MessageBox.Show("❌ Por favor complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validación del tipo de persona (Socio o NoSocio)
                if (!rdSocio.Checked && !rbNoSocio.Checked)
                {
                    MessageBox.Show("❌ Seleccione si es Socio o NoSocio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Aquí luego pondremos la lógica de base de datos y creación de objetos
                // 🔹 Obtener el DNI ingresado
                string dni = txtDNI.Text.Trim();

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
                                MessageBox.Show("❌ El DNI ya está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error al consultar la base de datos: " + ex.Message);
                    return;
                }

                // 🔹 Obtener los datos ingresados
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string tipo = rdSocio.Checked ? "Socio" : "NoSocio";

                int idPersona = 0; // guardaremos el ID generado por la base

                try
                {
                    using(var conexion = new Conexion().ObtenerConexion())
                    {
                        conexion.Open();

                        string queryInsertPersona = @"INSERT INTO persona (nombre, apellido, dni, direccion, telefono, tipo)
                                         VALUES (@nombre, @apellido, @dni, @direccion, @telefono, @tipo);
                                         SELECT LAST_INSERT_ID();";

                        using (var cmd = new MySqlCommand(queryInsertPersona, conexion))
                        {
                            cmd.Parameters.AddWithValue("@nombre", nombre);
                            cmd.Parameters.AddWithValue("@apellido", apellido);
                            cmd.Parameters.AddWithValue("@dni", dni);
                            cmd.Parameters.AddWithValue("@direccion", direccion);
                            cmd.Parameters.AddWithValue("@telefono", telefono);
                            cmd.Parameters.AddWithValue("@tipo", tipo);

                            // Ejecuta el INSERT y devuelve el ID generado
                            idPersona = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error al insertar persona: " + ex.Message);
                    return;
                }


                if (!rdSocio.Checked) // Es NoSocio
                {
                    try
                    {
                        // Crear objeto NoSocio en memoria
                        NoSocio nuevoNoSocio = new NoSocio(0, idPersona, nombre, apellido, dni, direccion, telefono);

                        // Insertar en la tabla nosocio
                        using (var conexion = new Conexion().ObtenerConexion())
                        {
                            conexion.Open();

                            string queryInsertNoSocio = @"INSERT INTO nosocio (idPersona) VALUES (@idPersona)";

                            using (var cmd = new MySqlCommand(queryInsertNoSocio, conexion))
                            {
                                cmd.Parameters.AddWithValue("@idPersona", idPersona);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        listaPersonas.Add(nuevoNoSocio);
                        listaNoSocios.Add(nuevoNoSocio);

                        MessageBox.Show("✅ NoSocio registrado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Error al insertar NoSocio: " + ex.Message);
                        return;
                    }
                }

                if (rdSocio.Checked) // Es Socio
                {
                    try
                    {
                        // Crear objeto Socio en memoria
                        Socio nuevoSocio = new Socio(0, idPersona, nombre, apellido, dni, direccion, telefono, DateTime.Now, false, true);


                        // Insertar en la tabla socio
                        using (var conexion = new Conexion().ObtenerConexion())
                        {
                            conexion.Open();

                            string queryInsertSocio = @"INSERT INTO socio (idPersona, fechaAlta, tieneCarnet, estado) 
                                            VALUES (@idPersona, @fechaAlta, @tieneCarnet, @estado);
                                            SELECT LAST_INSERT_ID();";

                            int idSocio = 0;
                            using (var cmd = new MySqlCommand(queryInsertSocio, conexion))
                            {
                                cmd.Parameters.AddWithValue("@idPersona", idPersona);
                                cmd.Parameters.AddWithValue("@fechaAlta", DateTime.Now.Date); // Fecha de alta en la DB
                                cmd.Parameters.AddWithValue("@tieneCarnet", false);            // Carnet por defecto
                                cmd.Parameters.AddWithValue("@estado", true);                  // Estado activo

                                // Ejecuta el INSERT y devuelve el ID generado
                                idSocio = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // Generar la primera cuota
                            string queryInsertCuota = @"INSERT INTO cuota (idSocio, monto, fechaVencimiento, medioPago) 
                                            VALUES (@idSocio, @monto, @fechaVencimiento, @medioPago)";

                            using (var cmd = new MySqlCommand(queryInsertCuota, conexion))
                            {
                                cmd.Parameters.AddWithValue("@idSocio", idSocio);
                                cmd.Parameters.AddWithValue("@monto", 1000); // Ejemplo de monto inicial
                                cmd.Parameters.AddWithValue("@fechaVencimiento", DateTime.Now.AddMonths(1).Date);
                                cmd.Parameters.AddWithValue("@medioPago", "Efectivo"); // Por defecto

                                cmd.ExecuteNonQuery();
                            }
                        }

                        listaPersonas.Add(nuevoSocio);
                        listaSocios.Add(nuevoSocio);

                        MessageBox.Show("✅ Socio registrado correctamente, primera cuota generada.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Error al insertar Socio: " + ex.Message);
                        return;
                    }


                }

                




            }

            private void radioButton1_CheckedChanged(object sender, EventArgs e)
            {

            }

            private void btnCobrarCuota_Click(object sender, EventArgs e)
            {
                try
                {
                    using (var conexion = new Conexion().ObtenerConexion())
                    {
                        conexion.Open();

                        // Supongamos que el socio se identifica por DNI
                        string dni = txtDNI.Text.Trim();

                        // Buscamos al socio
                        string queryBuscar = @"
                    SELECT s.idSocio 
                    FROM socio s
                    INNER JOIN persona p ON s.idPersona = p.idPersona
                    WHERE p.dni = @dni;
                ";

                        MySqlCommand cmdBuscar = new MySqlCommand(queryBuscar, conexion);
                        cmdBuscar.Parameters.AddWithValue("@dni", dni);
                        object resultado = cmdBuscar.ExecuteScalar();

                        if (resultado == null)
                        {
                            MessageBox.Show("⚠ No se encontró ningún socio con ese DNI.");
                            return;
                        }

                        int idSocio = Convert.ToInt32(resultado);

                        // Actualizamos la fecha de vencimiento según el tipo de cuota
                        DateTime nuevaFecha;
                        if (rdSocio.Checked)
                            nuevaFecha = DateTime.Now.AddMonths(1);  // cuota mensual
                        else
                            nuevaFecha = DateTime.Now.AddDays(1);    // cuota diaria (para no socios, opcional)

                        string queryActualizar = @"
                    UPDATE cuota
                    SET fechaVencimiento = @nuevaFecha
                    WHERE idSocio = @idSocio;
                ";

                        MySqlCommand cmdActualizar = new MySqlCommand(queryActualizar, conexion);
                        cmdActualizar.Parameters.AddWithValue("@nuevaFecha", nuevaFecha);
                        cmdActualizar.Parameters.AddWithValue("@idSocio", idSocio);
                        cmdActualizar.ExecuteNonQuery();

                        MessageBox.Show($"✅ Cuota cobrada correctamente.\n📅 Próximo vencimiento: {nuevaFecha:dd/MM/yyyy}\n🎟 Carnet entregado.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error al cobrar cuota: " + ex.Message);
                }
            }

            private void button2_Click(object sender, EventArgs e)
            {
            
                this.Hide(); // Oculta el formulario actual
                frmPrincipal principal = new frmPrincipal();
                principal.Show(); // Vuelve al menú principal
            }

            private void label1_Click(object sender, EventArgs e)
            {

            }

            private void rdSocio_CheckedChanged(object sender, EventArgs e)
            {

            }

            private void frmRegistrarPersona_Click(object sender, EventArgs e)
            {
                // Oculta esta ventana
                this.Hide();

                // Abrir la ventana de morosos
                frmMorosos ventanaMorosos = new frmMorosos();
                ventanaMorosos.Show();
            }

            private Form formAnterior;

            public frmRegistrarPersona(Form frmAnterior)
            {
                InitializeComponent();
                formAnterior = frmAnterior;
            }
            private void btnVolver_Click(object sender, EventArgs e)
            {
                this.Close();
                formAnterior.Show();
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

                    // Si llegamos acá, el DNI no existe: mostrar el panel de datos personales
                    panelDNI.Visible = false;  // Oculta panel de DNI
                    panelDatosPersonales.Visible = true;  // Mostramos panel de datos personales
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error al consultar la base de datos: " + ex.Message);
                }
            }

            private void panelDNI_Paint(object sender, PaintEventArgs e)
            {

            }

            private void btnRegistrarPersonaPanel_Click(object sender, EventArgs e)
            {
                // 🔹 Validación de campos
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtDNI.Text) ||
                    string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    MessageBox.Show("❌ Por favor complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🔹 Validación del tipo de persona (Socio o NoSocio)
                if (!rdSocio.Checked && !rbNoSocio.Checked)
                {
                    MessageBox.Show("❌ Seleccione si es Socio o NoSocio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string dni = txtDNI.Text.Trim();

                try
                {
                    using (var conexion = new Conexion().ObtenerConexion())
                    {
                        conexion.Open();

                        // Verificar si ya existe el DNI
                        string query = "SELECT COUNT(*) FROM persona WHERE dni = @dni";
                        using (var cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@dni", dni);
                            int count = Convert.ToInt32(cmd.ExecuteScalar());

                            if (count > 0)
                            {
                                MessageBox.Show("⚠ El cliente ya está registrado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        // 🔹 Insertar Persona
                        string nombre = txtNombre.Text.Trim();
                        string apellido = txtApellido.Text.Trim();
                        string direccion = txtDireccion.Text.Trim();
                        string telefono = txtTelefono.Text.Trim();
                        string tipo = rdSocio.Checked ? "Socio" : "NoSocio";

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

                        // 🔹 Insertar según tipo
                        if (!rdSocio.Checked) // NoSocio
                        {
                            string queryInsertNoSocio = @"INSERT INTO nosocio (idPersona) VALUES (@idPersona)";
                            using (var cmd = new MySqlCommand(queryInsertNoSocio, conexion))
                            {
                                cmd.Parameters.AddWithValue("@idPersona", idPersona);
                                cmd.ExecuteNonQuery();
                            }
                            MessageBox.Show("✅ NoSocio registrado correctamente.");
                        }
                        else // Socio
                        {
                            string queryInsertSocio = @"INSERT INTO socio (idPersona, fechaAlta, tieneCarnet, estado)
                                                VALUES (@idPersona, @fechaAlta, @tieneCarnet, @estado);
                                                SELECT LAST_INSERT_ID();";

                            int idSocio = 0;
                            using (var cmd = new MySqlCommand(queryInsertSocio, conexion))
                            {
                                cmd.Parameters.AddWithValue("@idPersona", idPersona);
                                cmd.Parameters.AddWithValue("@fechaAlta", DateTime.Now.Date);
                                cmd.Parameters.AddWithValue("@tieneCarnet", false);
                                cmd.Parameters.AddWithValue("@estado", true);

                                idSocio = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // 🔹 Generar primera cuota
                            string queryInsertCuota = @"INSERT INTO cuota (idSocio, monto, fechaVencimiento, medioPago) 
                                                VALUES (@idSocio, @monto, @fechaVencimiento, @medioPago)";

                            using (var cmd = new MySqlCommand(queryInsertCuota, conexion))
                            {
                                cmd.Parameters.AddWithValue("@idSocio", idSocio);
                                cmd.Parameters.AddWithValue("@monto", 1000); // ejemplo de monto
                                cmd.Parameters.AddWithValue("@fechaVencimiento", DateTime.Now.AddMonths(1).Date);
                                cmd.Parameters.AddWithValue("@medioPago", "Efectivo");

                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("✅ Socio registrado correctamente, primera cuota generada.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error al registrar persona: " + ex.Message);
                }

                // 🔹 Limpiar campos y volver al panel inicial
                txtNombre.Clear();
                txtApellido.Clear();
                txtDNI.Clear();
                txtDireccion.Clear();
                txtTelefono.Clear();
                panelDatosPersonales.Visible = false;
                panelDNI.Visible = true;
            }

            private void btnVolverDNI_Click(object sender, EventArgs e)
            {
                panelDatosPersonales.Visible = false; // Oculta el panel de registro
                panelDNI.Visible = true;               // Muestra de nuevo el panel de DNI
            }

            private void btnVolverMenu_Click(object sender, EventArgs e)
            {
                if (formAnterior != null)
                {
                    formAnterior.Show(); // Muestra frmPrincipal
                    this.Close();        // Cierra frmRegistrarPersona
                }
                else
                {
                    MessageBox.Show("No hay una ventana anterior a la que volver.");
                    this.Close();
                }
            }
        }
    }

