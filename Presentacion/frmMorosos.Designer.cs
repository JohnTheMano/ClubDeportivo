namespace ClubDeportivo.Presentacion
{
    partial class frmMorosos
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
            this.btnCargarMorosos = new System.Windows.Forms.Button();
            this.dgvMorosos = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMorosos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCargarMorosos
            // 
            this.btnCargarMorosos.Location = new System.Drawing.Point(307, 384);
            this.btnCargarMorosos.Name = "btnCargarMorosos";
            this.btnCargarMorosos.Size = new System.Drawing.Size(161, 68);
            this.btnCargarMorosos.TabIndex = 0;
            this.btnCargarMorosos.Text = "Cargar Morosos";
            this.btnCargarMorosos.UseVisualStyleBackColor = true;
            this.btnCargarMorosos.Click += new System.EventHandler(this.btnCargarMorosos_Click_1);
            // 
            // dgvMorosos
            // 
            this.dgvMorosos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMorosos.Location = new System.Drawing.Point(36, 12);
            this.dgvMorosos.Name = "dgvMorosos";
            this.dgvMorosos.RowHeadersWidth = 51;
            this.dgvMorosos.RowTemplate.Height = 24;
            this.dgvMorosos.Size = new System.Drawing.Size(916, 366);
            this.dgvMorosos.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(474, 384);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 68);
            this.button1.TabIndex = 2;
            this.button1.Text = "Volver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // frmMorosos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 553);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvMorosos);
            this.Controls.Add(this.btnCargarMorosos);
            this.Name = "frmMorosos";
            this.Text = "frmMorosos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMorosos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCargarMorosos;
        private System.Windows.Forms.DataGridView dgvMorosos;
        private System.Windows.Forms.Button button1;
    }
}