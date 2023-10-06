using RecetasULTIMO00.Dominio;
using RecetasULTIMO00.Presentaciones;
using RecetasULTIMO00.Servicios;
using RecetasULTIMO00.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecetasULTIMO00.Presentaciones
{
    public partial class FormConsultas : Form
    {
        private IServicio Servicio;
        private FabricaServicio fabrica;

        public FormConsultas()
        {
            InitializeComponent();
            fabrica = new FabricaServicioIMP();
            Servicio = fabrica.ObtenerServicio();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            List<Receta> lst = Servicio.ObtenerRecetaPorFiltro(2, txtNombre.Text);
            foreach (Receta receta in lst)
            {
                dataGridView1.Rows.Add(new object[]
                {
                 receta.nombre,
                 receta.tipoReceta,
                 receta.cheff,
                });
            }
        }

        private void FormConsultas_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Desea volver a la carga?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
