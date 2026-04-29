using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using prySampaolesiClaseBD;

namespace prySampaolesiEPR2
{
    public partial class frmPrincipal : Form
    {
        private string[] archivosSeleccionados = new string[2];

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de texto (*.txt)|*.txt";
            ofd.Multiselect = true;
            ofd.Title = "Seleccionar 2 archivos .txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileNames.Length >= 2)
                {
                    archivosSeleccionados[0] = ofd.FileNames[0];
                    archivosSeleccionados[1] = ofd.FileNames[1];
                    txtLog.AppendText($"Archivo 1: {Path.GetFileName(archivosSeleccionados[0])}" + Environment.NewLine);
                    txtLog.AppendText($"Archivo 2: {Path.GetFileName(archivosSeleccionados[1])}" + Environment.NewLine);
                }
                else
                {
                    txtLog.AppendText("Error: Debes seleccionar exactamente 2 archivos." + Environment.NewLine);
                }
            }
        }

        private void btnMigrar_Click(object sender, EventArgs e)
        {
            if (archivosSeleccionados[0] == null || archivosSeleccionados[1] == null)
            {
                txtLog.AppendText("Error: Primero debes seleccionar 2 archivos." + Environment.NewLine);
                return;
            }

            string rutaBD = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BD", "bdGoodHard.accdb");
            clsConexion conexion = new clsConexion(rutaBD);

            if (!conexion.Conectar())
            {
                txtLog.AppendText("Error: No se pudo conectar a la base de datos." + Environment.NewLine);
                return;
            }

            txtLog.AppendText("Iniciando migración..." + Environment.NewLine);

            try
            {
                string archivoCategorias = null;
                string archivoArticulos = null;

                // Identificar archivos
                foreach (string archivo in archivosSeleccionados)
                {
                    string nombreArchivo = Path.GetFileNameWithoutExtension(archivo).ToLower();

                    if (nombreArchivo.Contains("categoria"))
                        archivoCategorias = archivo;
                    else if (nombreArchivo.Contains("articulo"))
                        archivoArticulos = archivo;
                }

                // Procesar Categorias primero (por integridad referencial)
                if (archivoCategorias != null)
                    ProcesarArchivo(conexion, archivoCategorias, "categorias");

                // Luego procesar Articulos
                if (archivoArticulos != null)
                    ProcesarArchivo(conexion, archivoArticulos, "Articulos");

                txtLog.AppendText("Migración completada exitosamente." + Environment.NewLine);
            }
            catch (Exception ex)
            {
                txtLog.AppendText($"Error durante la migración: {ex.Message}" + Environment.NewLine);
            }
            finally
            {
                conexion.Desconectar();
            }
        }

        private void ProcesarArchivo(clsConexion conexion, string ruta, string tabla)
        {
            string[] lineas = File.ReadAllLines(ruta);
            int insertados = 0;
            int errores = 0;

            foreach (string linea in lineas)
            {
                if (!string.IsNullOrWhiteSpace(linea))
                {
                    string[] datos = linea.Split(new[] { '|', '\t', ',' }, StringSplitOptions.None);

                    // Validar cantidad de campos
                    int camposEsperados = tabla == "categorias" ? 2 : 4;
                    if (datos.Length != camposEsperados)
                    {
                        txtLog.AppendText($"Advertencia: Línea con {datos.Length} campos, se esperaban {camposEsperados}: {linea}" + Environment.NewLine);
                        errores++;
                        continue;
                    }

                    // Limpiar espacios en blanco
                    for (int i = 0; i < datos.Length; i++)
                    {
                        datos[i] = datos[i].Trim();
                    }

                    string comando = GenerarComandoInsert(datos, tabla);
                    if (!string.IsNullOrEmpty(comando))
                    {
                        if (conexion.EjecutarComando(comando))
                        {
                            insertados++;
                        }
                        else
                        {
                            txtLog.AppendText($"Error al insertar en {tabla}: {linea}" + Environment.NewLine);
                            errores++;
                        }
                    }
                }
            }

            txtLog.AppendText($"{Path.GetFileName(ruta)} ({tabla}): {insertados} registros insertados, {errores} errores." + Environment.NewLine);
        }

        private string GenerarComandoInsert(string[] datos, string tabla)
        {
            try
            {
                if (tabla == "categorias" && datos.Length == 2)
                {
                    return $"INSERT INTO categorias (IdCategoria, Nombre) VALUES ({datos[0]}, '{datos[1].Replace("'", "''")}')";
                }
                else if (tabla == "Articulos" && datos.Length == 4)
                {
                    return $"INSERT INTO Articulos (IdArticulos, Nombre, IdCategoria, Precio) VALUES ({datos[0]}, '{datos[1].Replace("'", "''")}', {datos[2]}, {datos[3]})";
                }
            }
            catch (Exception ex)
            {
                txtLog.AppendText($"Error al generar comando: {ex.Message}" + Environment.NewLine);
            }
            return "";
        }

        private void btnConsultaBd_Click(object sender, EventArgs e)
        {
            frmConsulta frm = new frmConsulta();
            frm.ShowDialog();
        }
    }
}
