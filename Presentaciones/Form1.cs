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

namespace RecetasULTIMO00
{
    public partial class Recetas : Form
    {
        private IServicio Servicio;
        private FabricaServicio fabrica;
        Receta receta = new Receta();

        public Recetas()
        {
            InitializeComponent();
            fabrica = new FabricaServicioIMP();
            Servicio = fabrica.ObtenerServicio();
            Receta receta = new Receta();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarCombo();
            limpiar();
            Habilitar(false);
        }

        public void CargarCombo()
        {
            cboIngredientes.DataSource = Servicio.ConsultarDB();
            cboIngredientes.DisplayMember = "n_ingrediente";
            cboIngredientes.ValueMember = "id_ingrediente";
            cboIngredientes.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void limpiar()
        {
            txtNombre.Text = string.Empty;
            txtCheff.Text = string.Empty;
            cboIngredientes.SelectedIndex = 0;
            nudCantidad.Value = 1;
        }

        public void Habilitar(bool x)
        {
            txtNombre.Enabled = x;
            txtCheff.Enabled = x;
            cboIngredientes.Enabled = x;
            cboTipo.Enabled = x;
            nudCantidad.Enabled = x;
            btnAceptar.Enabled = x;
            btnNuevo.Enabled = !x;
            btnAgregar.Enabled = x;
            btnConsultar.Enabled = x;
            btnModificar.Enabled = x;
        }

        private bool validar()
        {
            bool valido = true;
           
            if(string.IsNullOrEmpty(txtCheff.Text))
            {
                valido = false;
                MessageBox.Show("Debe ingresar un cheff", "CONTROL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
            if(string.IsNullOrEmpty(txtNombre.Text))
            {
                valido = false;
                MessageBox.Show("Debe ingresar un nombre", "CONTROL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
            if(string.IsNullOrEmpty(cboIngredientes.Text))
            {
                valido = false;
                MessageBox.Show("Debe seleccionar un ingrediente", "CONTROL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            foreach (DataGridViewRow fila in dgvDetalles.Rows)
            {
                if (fila.Cells["Ingrediente"].Value.ToString().Equals(cboIngredientes.Text))
                {
                    MessageBox.Show("La comida ya tiene este ingrediente", "CONTROL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    valido = false;
                }
            }
            return valido;          

        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if (validar())
            {
                DataRowView item = (DataRowView)cboIngredientes.SelectedItem;
                int id = Convert.ToInt32(item.Row.ItemArray[0]);
                string nomb = Convert.ToString(item.Row.ItemArray[1]);
                Ingrediente ingrediente = new Ingrediente(id, nomb);

                int cantidad = Convert.ToInt32(nudCantidad.Value);
                DetalleReceta comida = new DetalleReceta(ingrediente, cantidad);

                receta.agregarComida(comida);
                dgvDetalles.Rows.Add(new object[] { id, ingrediente, cantidad }); // las columnas que tiene la grid

                ContarIngredientes();
            }
        }

        private void ContarIngredientes()
        {
            lblTotalIngredientes.Text = "Total de ingredientes: " + dgvDetalles.RowCount.ToString();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            receta.nombre = txtNombre.Text;
            receta.cheff = txtCheff.Text;
            receta.tipoReceta = cboTipo.SelectedIndex + 1;
            if(Servicio.EjecutarInsert(receta))
            {
                MessageBox.Show("Se inserto con exito", "CONTROL", MessageBoxButtons.OK);
                limpiar();
            }
            else
            {
                MessageBox.Show("ERROR, NO se pudo insertar", "CONTROL", MessageBoxButtons.OK);
            }
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvDetalles.CurrentCell.ColumnIndex == 3)
            {
                receta.quitarDetalle(dgvDetalles.CurrentCell.RowIndex);
                dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentCell.RowIndex);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            receta.nombre = txtNombre.Text;
            receta.cheff = txtCheff.Text;
            receta.tipoReceta = cboTipo.SelectedIndex + 1;
            if (Servicio.EjecutarUpdate(receta))
            {
                MessageBox.Show("Se modifico con exito", "CONTROL", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("ERROR, NO se pudo modificar", "CONTROL", MessageBoxButtons.OK);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("¿Está seguro que desea abandonar la carga?", "Salir", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            FormConsultas FC =  new FormConsultas();
            FC.Show();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
            Habilitar(true);
            txtNombre.Focus();
        }
    }
}
