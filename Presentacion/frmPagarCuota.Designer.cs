namespace ClubDeportivo.Presentacion
{
    partial class frmPagarCuota
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDNI = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudMonto = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.rbEfectivo = new System.Windows.Forms.RadioButton();
            this.rbTarjeta = new System.Windows.Forms.RadioButton();
            this.rbTransferencia = new System.Windows.Forms.RadioButton();
            this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRegistrarPago = new System.Windows.Forms.Button();
            this.btnImprimirComprobante = new System.Windows.Forms.Button();
            this.cmbActividad = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDeuda = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBoxAptoFisico = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonto)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Location = new System.Drawing.Point(302, 55);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(106, 16);
            this.lblDNI.TabIndex = 0;
            this.lblDNI.Text = "DNI/Nº de Socio";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(427, 52);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(100, 22);
            this.txtDNI.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(550, 52);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.Location = new System.Drawing.Point(302, 89);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(123, 40);
            this.lblNombre.TabIndex = 3;
            this.lblNombre.Text = "label1";
            this.lblNombre.Click += new System.EventHandler(this.lblNombre_Click);
            // 
            // lblMensaje
            // 
            this.lblMensaje.Location = new System.Drawing.Point(302, 149);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(151, 30);
            this.lblMensaje.TabIndex = 4;
            this.lblMensaje.Text = "label2";
            this.lblMensaje.Click += new System.EventHandler(this.lblMensaje_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(389, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Pado de Cuota o Actividad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(302, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Monto";
            // 
            // nudMonto
            // 
            this.nudMonto.DecimalPlaces = 2;
            this.nudMonto.Enabled = false;
            this.nudMonto.Location = new System.Drawing.Point(305, 229);
            this.nudMonto.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudMonto.Name = "nudMonto";
            this.nudMonto.Size = new System.Drawing.Size(120, 22);
            this.nudMonto.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(424, 303);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Medios de pago";
            // 
            // rbEfectivo
            // 
            this.rbEfectivo.AutoSize = true;
            this.rbEfectivo.Enabled = false;
            this.rbEfectivo.Location = new System.Drawing.Point(352, 336);
            this.rbEfectivo.Name = "rbEfectivo";
            this.rbEfectivo.Size = new System.Drawing.Size(76, 20);
            this.rbEfectivo.TabIndex = 10;
            this.rbEfectivo.TabStop = true;
            this.rbEfectivo.Text = "Efectivo";
            this.rbEfectivo.UseVisualStyleBackColor = true;
            // 
            // rbTarjeta
            // 
            this.rbTarjeta.AutoSize = true;
            this.rbTarjeta.Enabled = false;
            this.rbTarjeta.Location = new System.Drawing.Point(447, 336);
            this.rbTarjeta.Name = "rbTarjeta";
            this.rbTarjeta.Size = new System.Drawing.Size(71, 20);
            this.rbTarjeta.TabIndex = 11;
            this.rbTarjeta.TabStop = true;
            this.rbTarjeta.Text = "Tarjeta";
            this.rbTarjeta.UseVisualStyleBackColor = true;
            this.rbTarjeta.CheckedChanged += new System.EventHandler(this.rbTarjeta_CheckedChanged);
            // 
            // rbTransferencia
            // 
            this.rbTransferencia.AutoSize = true;
            this.rbTransferencia.Enabled = false;
            this.rbTransferencia.Location = new System.Drawing.Point(524, 336);
            this.rbTransferencia.Name = "rbTransferencia";
            this.rbTransferencia.Size = new System.Drawing.Size(111, 20);
            this.rbTransferencia.TabIndex = 12;
            this.rbTransferencia.TabStop = true;
            this.rbTransferencia.Text = "Transferencia";
            this.rbTransferencia.UseVisualStyleBackColor = true;
            // 
            // dtpFechaPago
            // 
            this.dtpFechaPago.Enabled = false;
            this.dtpFechaPago.Location = new System.Drawing.Point(354, 438);
            this.dtpFechaPago.Name = "dtpFechaPago";
            this.dtpFechaPago.Size = new System.Drawing.Size(271, 22);
            this.dtpFechaPago.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(288, 444);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Fecha:";
            // 
            // btnRegistrarPago
            // 
            this.btnRegistrarPago.Enabled = false;
            this.btnRegistrarPago.Location = new System.Drawing.Point(332, 484);
            this.btnRegistrarPago.Name = "btnRegistrarPago";
            this.btnRegistrarPago.Size = new System.Drawing.Size(121, 23);
            this.btnRegistrarPago.TabIndex = 15;
            this.btnRegistrarPago.Text = "Registrar pago";
            this.btnRegistrarPago.UseVisualStyleBackColor = true;
            this.btnRegistrarPago.Click += new System.EventHandler(this.btnRegistrarPago_Click);
            // 
            // btnImprimirComprobante
            // 
            this.btnImprimirComprobante.Enabled = false;
            this.btnImprimirComprobante.Location = new System.Drawing.Point(470, 484);
            this.btnImprimirComprobante.Name = "btnImprimirComprobante";
            this.btnImprimirComprobante.Size = new System.Drawing.Size(133, 23);
            this.btnImprimirComprobante.TabIndex = 16;
            this.btnImprimirComprobante.Text = "Imprimir comprobante";
            this.btnImprimirComprobante.UseVisualStyleBackColor = true;
            this.btnImprimirComprobante.Click += new System.EventHandler(this.btnImprimirComprobante_Click);
            // 
            // cmbActividad
            // 
            this.cmbActividad.FormattingEnabled = true;
            this.cmbActividad.Location = new System.Drawing.Point(536, 228);
            this.cmbActividad.Name = "cmbActividad";
            this.cmbActividad.Size = new System.Drawing.Size(121, 24);
            this.cmbActividad.TabIndex = 17;
            this.cmbActividad.SelectedIndexChanged += new System.EventHandler(this.cmbActividad_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(562, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Actividad";
            // 
            // lblDeuda
            // 
            this.lblDeuda.AutoSize = true;
            this.lblDeuda.Location = new System.Drawing.Point(494, 149);
            this.lblDeuda.Name = "lblDeuda";
            this.lblDeuda.Size = new System.Drawing.Size(131, 16);
            this.lblDeuda.TabIndex = 19;
            this.lblDeuda.Text = "Deuda pendiente: $0";
            this.lblDeuda.Click += new System.EventHandler(this.lblDeuda_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1 Pago",
            "3 Pagos",
            "6 Pagos"});
            this.comboBox1.Location = new System.Drawing.Point(427, 378);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 20;
            this.comboBox1.Visible = false;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // checkBoxAptoFisico
            // 
            this.checkBoxAptoFisico.AutoSize = true;
            this.checkBoxAptoFisico.Enabled = false;
            this.checkBoxAptoFisico.Location = new System.Drawing.Point(550, 269);
            this.checkBoxAptoFisico.Name = "checkBoxAptoFisico";
            this.checkBoxAptoFisico.Size = new System.Drawing.Size(96, 20);
            this.checkBoxAptoFisico.TabIndex = 21;
            this.checkBoxAptoFisico.Text = "Apto Físico";
            this.checkBoxAptoFisico.UseVisualStyleBackColor = true;
            this.checkBoxAptoFisico.CheckedChanged += new System.EventHandler(this.checkBoxAptoFisico_CheckedChanged);
            // 
            // frmPagarCuota
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 546);
            this.Controls.Add(this.checkBoxAptoFisico);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblDeuda);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbActividad);
            this.Controls.Add(this.btnImprimirComprobante);
            this.Controls.Add(this.btnRegistrarPago);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpFechaPago);
            this.Controls.Add(this.rbTransferencia);
            this.Controls.Add(this.rbTarjeta);
            this.Controls.Add(this.rbEfectivo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudMonto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtDNI);
            this.Controls.Add(this.lblDNI);
            this.Name = "frmPagarCuota";
            this.Text = "frmPagarCuota";
            this.Load += new System.EventHandler(this.frmPagarCuota_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMonto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudMonto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbEfectivo;
        private System.Windows.Forms.RadioButton rbTarjeta;
        private System.Windows.Forms.RadioButton rbTransferencia;
        private System.Windows.Forms.DateTimePicker dtpFechaPago;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRegistrarPago;
        private System.Windows.Forms.Button btnImprimirComprobante;
        private System.Windows.Forms.ComboBox cmbActividad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDeuda;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBoxAptoFisico;
    }
}