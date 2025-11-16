namespace ClubDeportivo.Presentacion
{
    partial class frmRegistrarPersona
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
            this.btnRegistrarPersonaPanel = new System.Windows.Forms.Button();
            this.panelDNI = new System.Windows.Forms.Panel();
            this.panelDatosPersonales = new System.Windows.Forms.Panel();
            this.btnVolverDNI = new System.Windows.Forms.Button();
            this.rbNoSocio = new System.Windows.Forms.RadioButton();
            this.rdSocio = new System.Windows.Forms.RadioButton();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnVolverMenu = new System.Windows.Forms.Button();
            this.btnContinuarDNI = new System.Windows.Forms.Button();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panelDNI.SuspendLayout();
            this.panelDatosPersonales.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRegistrarPersonaPanel
            // 
            this.btnRegistrarPersonaPanel.Location = new System.Drawing.Point(253, 285);
            this.btnRegistrarPersonaPanel.Name = "btnRegistrarPersonaPanel";
            this.btnRegistrarPersonaPanel.Size = new System.Drawing.Size(98, 48);
            this.btnRegistrarPersonaPanel.TabIndex = 10;
            this.btnRegistrarPersonaPanel.Text = "Registrar";
            this.btnRegistrarPersonaPanel.UseVisualStyleBackColor = true;
            this.btnRegistrarPersonaPanel.Click += new System.EventHandler(this.btnRegistrarPersonaPanel_Click);
            // 
            // panelDNI
            // 
            this.panelDNI.Controls.Add(this.btnVolverMenu);
            this.panelDNI.Controls.Add(this.btnContinuarDNI);
            this.panelDNI.Controls.Add(this.txtDNI);
            this.panelDNI.Controls.Add(this.label6);
            this.panelDNI.Location = new System.Drawing.Point(110, 12);
            this.panelDNI.Name = "panelDNI";
            this.panelDNI.Size = new System.Drawing.Size(615, 417);
            this.panelDNI.TabIndex = 15;
            this.panelDNI.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDNI_Paint);
            // 
            // panelDatosPersonales
            // 
            this.panelDatosPersonales.Controls.Add(this.btnVolverDNI);
            this.panelDatosPersonales.Controls.Add(this.rbNoSocio);
            this.panelDatosPersonales.Controls.Add(this.rdSocio);
            this.panelDatosPersonales.Controls.Add(this.txtTelefono);
            this.panelDatosPersonales.Controls.Add(this.label10);
            this.panelDatosPersonales.Controls.Add(this.btnRegistrarPersonaPanel);
            this.panelDatosPersonales.Controls.Add(this.txtDireccion);
            this.panelDatosPersonales.Controls.Add(this.label9);
            this.panelDatosPersonales.Controls.Add(this.txtApellido);
            this.panelDatosPersonales.Controls.Add(this.label8);
            this.panelDatosPersonales.Controls.Add(this.txtNombre);
            this.panelDatosPersonales.Controls.Add(this.label7);
            this.panelDatosPersonales.Location = new System.Drawing.Point(110, 12);
            this.panelDatosPersonales.Name = "panelDatosPersonales";
            this.panelDatosPersonales.Size = new System.Drawing.Size(615, 417);
            this.panelDatosPersonales.TabIndex = 16;
            this.panelDatosPersonales.Visible = false;
            // 
            // btnVolverDNI
            // 
            this.btnVolverDNI.Location = new System.Drawing.Point(369, 310);
            this.btnVolverDNI.Name = "btnVolverDNI";
            this.btnVolverDNI.Size = new System.Drawing.Size(75, 23);
            this.btnVolverDNI.TabIndex = 17;
            this.btnVolverDNI.Text = "Volver";
            this.btnVolverDNI.UseVisualStyleBackColor = true;
            this.btnVolverDNI.Click += new System.EventHandler(this.btnVolverDNI_Click);
            // 
            // rbNoSocio
            // 
            this.rbNoSocio.AutoSize = true;
            this.rbNoSocio.Location = new System.Drawing.Point(156, 313);
            this.rbNoSocio.Name = "rbNoSocio";
            this.rbNoSocio.Size = new System.Drawing.Size(84, 20);
            this.rbNoSocio.TabIndex = 9;
            this.rbNoSocio.TabStop = true;
            this.rbNoSocio.Text = "No Socio";
            this.rbNoSocio.UseVisualStyleBackColor = true;
            // 
            // rdSocio
            // 
            this.rdSocio.AutoSize = true;
            this.rdSocio.Location = new System.Drawing.Point(156, 282);
            this.rdSocio.Name = "rdSocio";
            this.rdSocio.Size = new System.Drawing.Size(63, 20);
            this.rdSocio.TabIndex = 8;
            this.rdSocio.TabStop = true;
            this.rdSocio.Text = "Socio";
            this.rdSocio.UseVisualStyleBackColor = true;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(253, 216);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(191, 22);
            this.txtTelefono.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(153, 219);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 16);
            this.label10.TabIndex = 6;
            this.label10.Text = "Teléfono";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(253, 175);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(191, 22);
            this.txtDireccion.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(153, 182);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "Dirección";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(253, 137);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(191, 22);
            this.txtApellido.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(153, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Apellido";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(253, 101);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(191, 22);
            this.txtNombre.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(153, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Nombre";
            // 
            // btnVolverMenu
            // 
            this.btnVolverMenu.Location = new System.Drawing.Point(277, 235);
            this.btnVolverMenu.Name = "btnVolverMenu";
            this.btnVolverMenu.Size = new System.Drawing.Size(75, 23);
            this.btnVolverMenu.TabIndex = 3;
            this.btnVolverMenu.Text = "Volver";
            this.btnVolverMenu.UseVisualStyleBackColor = true;
            this.btnVolverMenu.Click += new System.EventHandler(this.btnVolverMenu_Click);
            // 
            // btnContinuarDNI
            // 
            this.btnContinuarDNI.Location = new System.Drawing.Point(242, 165);
            this.btnContinuarDNI.Name = "btnContinuarDNI";
            this.btnContinuarDNI.Size = new System.Drawing.Size(134, 30);
            this.btnContinuarDNI.TabIndex = 2;
            this.btnContinuarDNI.Text = "Continuar";
            this.btnContinuarDNI.UseVisualStyleBackColor = true;
            this.btnContinuarDNI.Click += new System.EventHandler(this.btnContinuarDNI_Click);
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(232, 126);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(156, 22);
            this.txtDNI.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(274, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Ingrese DNI:";
            // 
            // frmRegistrarPersona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelDNI);
            this.Controls.Add(this.panelDatosPersonales);
            this.Name = "frmRegistrarPersona";
            this.Text = "frmRegistrarPersona";
            this.Click += new System.EventHandler(this.frmRegistrarPersona_Click);
            this.panelDNI.ResumeLayout(false);
            this.panelDNI.PerformLayout();
            this.panelDatosPersonales.ResumeLayout(false);
            this.panelDatosPersonales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRegistrarPersonaPanel;
        private System.Windows.Forms.Panel panelDNI;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Button btnContinuarDNI;
        private System.Windows.Forms.Panel panelDatosPersonales;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbNoSocio;
        private System.Windows.Forms.RadioButton rdSocio;
        private System.Windows.Forms.Button btnVolverDNI;
        private System.Windows.Forms.Button btnVolverMenu;
    }
}