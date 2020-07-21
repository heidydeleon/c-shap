using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_agenda
{
    public partial class DgvAgenda : Form
    {
        private int id;
        agenda age = new agenda();
        DataTable dt;

        public DgvAgenda()
        {
            InitializeComponent();
            restablecerControles();
            consultar();
            dataGridView1.Columns["id"].Visible = false;

        }
        private void consultar()
        {
            dt = age.consultar();
            dataGridView1.DataSource = dt;
        }
        private void obtenerId()
        {
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
        }
        private void obtenerDatos()
        {
            obtenerId();
            TxtNombre.Text = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
            TxtTelefono.Text = dataGridView1.CurrentRow.Cells["telefono"].Value.ToString();
        }
        private void restablecerControles()
        {
            this.TxtNombre.Clear();
            this.TxtTelefono.Clear();
            this.TxtFiltrar.Clear();
            this.BtnEliminar.Enabled = false;
            this.BtnModificar.Enabled = false;
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtNombre.Text) || string.IsNullOrWhiteSpace(TxtNombre.Text))
            {
                return;
            }
            bool rs = age.insertar(TxtNombre.Text, TxtTelefono.Text);
            if (rs)
            {
                MessageBox.Show("Registro realizado con exicto");
                restablecerControles();
                consultar();
            }
            else 
            {
                MessageBox.Show("No se pudo realizar el registro");
            }
            
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtNombre.Text) || string.IsNullOrWhiteSpace(TxtNombre.Text))
            {
                return;
            }

            bool rs = age.actualizar(id, TxtNombre.Text, TxtTelefono.Text);
            if (rs)
            {
                MessageBox.Show("Su registro sea actualizado con exicto");
                restablecerControles();
                consultar();
            }
            else
            {
                MessageBox.Show("Su registro no se puedo actualizar");
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult m = 
                MessageBox.Show("Eliminar", 
                "Esta seguro que quiere eliminar este registro?", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (m == DialogResult.OK)
            {
                bool rs = age.eliminar(id);
                if (rs)
                {
                    MessageBox.Show("Registro eliminado con exicto");
                    restablecerControles();
                    consultar();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar correctamente");
                }
                
            }
        }

        private void TxtFiltrar_TextChanged(object sender, EventArgs e)
        {
            if (TxtFiltrar.Text.Length == 0)
                consultar();
            if (TxtFiltrar.Text.Length < 3)
                return;
            dataGridView1.DataSource = age.filtrar(TxtFiltrar.Text);

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            restablecerControles();
            obtenerId();
            this.BtnEliminar.Enabled = true;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            obtenerDatos();
            this.BtnEliminar.Enabled = false;
            this.BtnModificar.Enabled = true;
        }
    }
}
