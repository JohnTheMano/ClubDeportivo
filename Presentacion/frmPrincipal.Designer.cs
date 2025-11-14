namespace ClubDeportivo.Presentacion
{
    partial class frmPrincipal
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
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnMorosos = new System.Windows.Forms.Button();
            this.btnEntregarCarnet = new System.Windows.Forms.Button();
            this.btnPagarCuota = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(40, 81);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(164, 90);
            this.btnRegistrar.TabIndex = 0;
            this.btnRegistrar.Text = "Registrar Persona";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // btnMorosos
            // 
            this.btnMorosos.Location = new System.Drawing.Point(572, 81);
            this.btnMorosos.Name = "btnMorosos";
            this.btnMorosos.Size = new System.Drawing.Size(164, 90);
            this.btnMorosos.TabIndex = 1;
            this.btnMorosos.Text = "Ver Morosos";
            this.btnMorosos.UseVisualStyleBackColor = true;
            this.btnMorosos.Click += new System.EventHandler(this.btnMorosos_Click);
            // 
            // btnEntregarCarnet
            // 
            this.btnEntregarCarnet.Location = new System.Drawing.Point(210, 81);
            this.btnEntregarCarnet.Name = "btnEntregarCarnet";
            this.btnEntregarCarnet.Size = new System.Drawing.Size(182, 90);
            this.btnEntregarCarnet.TabIndex = 2;
            this.btnEntregarCarnet.Text = "Entregar carnet";
            this.btnEntregarCarnet.UseVisualStyleBackColor = true;
            this.btnEntregarCarnet.Click += new System.EventHandler(this.btnEntregarCarnet_Click);
            // 
            // btnPagarCuota
            // 
            this.btnPagarCuota.Location = new System.Drawing.Point(398, 81);
            this.btnPagarCuota.Name = "btnPagarCuota";
            this.btnPagarCuota.Size = new System.Drawing.Size(168, 90);
            this.btnPagarCuota.TabIndex = 3;
            this.btnPagarCuota.Text = "Pagar Cuota";
            this.btnPagarCuota.UseVisualStyleBackColor = true;
            this.btnPagarCuota.Click += new System.EventHandler(this.btnPagarCuota_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPagarCuota);
            this.Controls.Add(this.btnEntregarCarnet);
            this.Controls.Add(this.btnMorosos);
            this.Controls.Add(this.btnRegistrar);
            this.Name = "frmPrincipal";
            this.Text = "frmPrincipal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnMorosos;
        private System.Windows.Forms.Button btnEntregarCarnet;
        private System.Windows.Forms.Button btnPagarCuota;
    }
}