using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Modelos;

namespace Inventario
{
    public partial class FrmAreas : Form
    {
        public FrmAreas()
        {
            InitializeComponent();

            actualizarTabla();

        }
        private void actualizarTabla()
        {
            DAOInv dao = new DAOInv();
            List<Inv> inv = dao.consultarTodos();
            dgvAreas.DataSource = inv;
            dgvAreas.Columns["IdArea"].Visible = false;
        }


        private void dgvEmpleado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAreas.SelectedRows.Count > 0)
            {
                //Obtener el id de la categoría que voy a editar
                int idproducto = int.Parse(dgvAreas.SelectedRows[0].Cells[0].Value.ToString());

                string producto = dgvAreas.SelectedRows[0].Cells[0].Value.ToString();

                //Solicitar confirmación
                DialogResult respuesta = MessageBox.Show(this, "Estás a punto de eliminar el producto " + producto +
                    ", ¿deseas continuar?", "Confirmación", MessageBoxButtons.YesNo);

                if (respuesta == DialogResult.Yes)
                {
                    //Eliminar la categoría
                    DAOInv dao = new DAOInv();
                    bool resultado = dao.eliminarProducto(idproducto);
                    if (resultado)
                    {
                        //informar la eliminación
                        MessageBox.Show("Se ha borrado el producto");
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido realizar la operación, " +
                            "verifica que si el producto existe ");
                    }
                    actualizarTabla();
                }

            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            new FrmInventario().ShowDialog();
            actualizarTabla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvAreas.SelectedRows.Count > 0)
            {
                //Obtener el id de la categoría que voy a editar
                int idProducto = int.Parse(dgvAreas.SelectedRows[0].Cells[0].Value.ToString());

                new FrmInventario(idProducto).ShowDialog();

                actualizarTabla();
            }
            else
            {
                MessageBox.Show("Selecciona un producto");


            }

        }
    }
}
