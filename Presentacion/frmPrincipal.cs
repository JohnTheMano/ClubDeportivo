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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }


            private void btnRegistrar_Click(object sender, EventArgs e)
            {
                this.Hide();
                frmRegistrarPersona ventana = new frmRegistrarPersona(this);
                ventana.Show();
            }
        private void btnMorosos_Click(object sender, EventArgs e)
        {
            this.Hide();
            var ventana = new frmMorosos(this);
            ventana.Show();
        }

        private void btnEntregarCarnet_Click(object sender, EventArgs e)
        {
            frmEntregarCarnet frm = new frmEntregarCarnet();
            frm.ShowDialog(); // Esto abre el formulario como ventana modal

        }

        private void btnPagarCuota_Click(object sender, EventArgs e)
        {
            frmPagarCuota frmPago = new frmPagarCuota();
            frmPago.ShowDialog(); // Esto abre el formulario como ventana modal

        }
    }
}
