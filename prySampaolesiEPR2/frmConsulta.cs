using System;
using System.Windows.Forms;
using prySampaolesiClaseBD;
using System.IO;

namespace prySampaolesiEPR2
{
    public partial class frmConsulta : Form
    {
        private clsConexion conexion;

        public frmConsulta()
        {
            InitializeComponent();
        }

        private void frmConsulta_Load(object sender, EventArgs e)
        {
            string rutaBD = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BD", "bdGoodHard.accdb");
            conexion = new clsConexion(rutaBD);
            conexion.Conectar();
        }

        private void cmbTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTablas.SelectedIndex >= 0)
            {
                string tabla = cmbTablas.SelectedItem.ToString();
                dgvTablas.DataSource = conexion.ObtenerDatos(tabla);
            }
        }
    }
}
