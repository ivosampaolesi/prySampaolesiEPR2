using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace prySampaolesiClaseBD
{
    public class clsConexion
    {
        private OleDbConnection conexion;
        private string rutaBD;
        private string[] proveedores = { "Microsoft.ACE.OLEDB.12.0", "Microsoft.ACE.OLEDB.16.0", "Microsoft.Jet.OLEDB.4.0" };

        public clsConexion(string rutaBD)
        {
            this.rutaBD = rutaBD;
        }

        public bool Conectar()
        {
            foreach (string proveedor in proveedores)
            {
                try
                {
                    string cadenaConexion = $"Provider={proveedor};Data Source={rutaBD};";
                    conexion = new OleDbConnection(cadenaConexion);
                    conexion.Open();
                    return true;
                }
                catch { }
            }
            MessageBox.Show("No se pudo conectar a la base de datos con ningún proveedor OLEDB disponible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public void Desconectar()
        {
            if (conexion != null && conexion.State == ConnectionState.Open)
            {
                conexion.Close();
                conexion.Dispose();
            }
        }

        public DataTable ObtenerDatos(string tabla)
        {
            try
            {
                DataTable dt = new DataTable();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter($"SELECT * FROM {tabla}", conexion))
                {
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public DataTable EjecutarConsulta(string consulta)
        {
            try
            {
                DataTable dt = new DataTable();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(consulta, conexion))
                {
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en consulta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public bool EjecutarComando(string comando)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(comando, conexion))
                {
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar comando: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
