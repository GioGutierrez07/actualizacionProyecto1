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
    public partial class FrmInventario : Form
    {
        public FrmInventario()
        {
            InitializeComponent();

            DAOAreas dao = new DAOAreas();
            cboArea.DataSource = dao.consultarTodas();
            cboArea.ValueMember = "idArea";
            cboArea.DisplayMember = "nombre";

            txtId.Enabled = true;

        }

        public FrmInventario(int id)
        {
            InitializeComponent();
            txtId.Enabled = false;
            //cboFecha.Enabled = false;
            DAOAreas dao = new DAOAreas();
            cboArea .DataSource = dao.consultarTodas();
            cboArea.ValueMember = "IdArea";
            cboArea .DisplayMember = "nombre";
            // Buscar la categoría que tenga ese id


            Inv cat = new DAOInv().solicitarProducto(id);
            //Cargar los datos de las cajas con los datos de la categoría
            if (cat != null)
            {
                txtId.Text = cat.IdInventario + "";
                
                txtNombre.Text = cat.NombreCorto;
                txtColor.Text = cat.Color;
                txtSerie.Text = cat.Serie;
                txtDes.Text = cat.Descripcion;
                cboFecha.Text = cat.Fecha;
                //monthCalendar1.Enabled = false;
                cboObcer.Text = cat.obserbaciones;
                cboTipo.Text = cat.TipoAdquisio;


               

            }
            else
            {
                MessageBox.Show("el producto no existe en invventario no existe");
                this.Close();
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Inv obj;
            DAOInv dao = new DAOInv();
            bool resultado;
            //int id = int.Parse(txtId.Text);
            

            if (txtId.Text.Length > 0)
            {


                
                //Editar
                obj = new Inv();
                int idcategoria = (int)cboArea.SelectedValue;
                string categ = cboArea.Text;
                obj.IdInventario = int.Parse(txtId.Text);
                obj.NombreCorto = txtNombre.Text;
                obj.obserbaciones = cboObcer.Text;
                obj.IdArea = idcategoria;
                obj.Fecha = cboFecha.Text;
                obj.Color = txtColor.Text;
                obj.Descripcion = txtDes.Text;
                obj.Serie = txtSerie.Text;
                obj.TipoAdquisio = cboTipo.Text;


                txtId.Text = "";

                resultado = dao.editarProducto(obj);
                this.Close();


            }
            else
            {
                obj = new Inv();
                txtId.Enabled = false; 
                int idcategoria = (int)cboArea.SelectedValue;
                string categ = cboArea.Text;
                
                obj.NombreCorto = txtNombre.Text;
                obj.obserbaciones = cboObcer.Text;
                obj.IdArea = idcategoria;
                obj.Fecha = cboFecha.Text;
                obj.Color = txtColor.Text;
                obj.Descripcion = txtDes.Text;
                obj.Serie = txtSerie.Text;
                obj.TipoAdquisio = cboTipo.Text;

                resultado = dao.incetarProducto(obj);
               
                this.Close();

            }
        }

        private void cboFecha_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
