using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        private readonly ApiClient client;
        private bool EditingMode = false;
        private bool AddingMode = false;

        public Form1()
        {
            InitializeComponent();
            client = new ApiClient();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            List<Articulo> list = client.GetAllArticulos().Result.ToList();
            this.mainList.DataSource = list;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void mainList_SelectionChanged(object sender, EventArgs e)
        {
            if(mainList.SelectedRows.Count > 0)
            {
                btnEditar.Visible = true;
                btnBorrar.Visible = true;

                LoadDataSelected();
            }
            else
            {
                btnEditar.Visible = false;
                btnBorrar.Visible = false;
            }
        }

        private void LoadDataSelected()
        {
            Articulo selected = (Articulo)mainList.SelectedRows[0].DataBoundItem;

            txtID.Text = selected.Id.ToString();
            txtDescripcion.Text = selected.Descripcion;
            txtPrecio.Text = selected.Precio.ToString();
            txtStock.Text = selected.Stock.ToString();
            chkbxActivo.Checked = selected.Activo;
        }

        private void ToggleEditMode()
        {
            EditingMode = !EditingMode;
            EnableDisableControls();
            btnGuardar.Visible = !btnGuardar.Visible;
            if (EditingMode)
                btnEditar.Text = "Cancelar";
            else
            {
                btnEditar.Text = "Editar";
                LoadDataSelected();
            }

            btnAgregar.Enabled = !btnAgregar.Enabled;
        }

        private void ToggleAddingMode()
        {
            AddingMode = !AddingMode;
            EnableDisableControls();
            btnGuardar.Visible = !btnGuardar.Visible;
            btnEditar.Visible = !btnEditar.Visible;
            btnBorrar.Visible = !btnBorrar.Visible;

            if (AddingMode)
                btnAgregar.Text = "Cancelar";
            else
            {
                btnAgregar.Text = "Agregar";
                CleanInputs();
            }

        }

        private void EnableDisableControls()
        {
            txtDescripcion.ReadOnly = !txtDescripcion.ReadOnly;
            txtPrecio.ReadOnly = !txtPrecio.ReadOnly;
            txtStock.ReadOnly = !txtStock.ReadOnly;
            chkbxActivo.Enabled = !chkbxActivo.Enabled;
        }

        private void CleanInputs()
        {
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            chkbxActivo.Checked = false;
        }

        private void inputBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ToggleEditMode();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Articulo current = new Articulo();
            if (mainList.SelectedRows.Count > 0)
            {
                if (EditingMode)
                {
                    current = (Articulo)mainList.SelectedRows[0].DataBoundItem;
                    current.Descripcion = txtDescripcion.Text;
                    current.Precio = Double.Parse(txtPrecio.Text);
                    current.Stock = int.Parse(txtStock.Text);
                    current.Activo = chkbxActivo.Checked;
                    Articulo result = this.client.Update(current).Result;
                    if (result.Id != 0)
                    {
                        LoadGrid();
                        ToggleEditMode();
                        btnGuardar.Visible = false;
                    }
                }
            }

            if (AddingMode)
            {
                current.Descripcion = txtDescripcion.Text;
                current.Precio = Double.Parse(txtPrecio.Text);
                current.Stock = int.Parse(txtStock.Text);
                current.Activo = chkbxActivo.Checked;
                Articulo result = this.client.Add(current).Result;

                if (result.Id != 0)
                {
                    LoadGrid();
                    ToggleAddingMode();
                    btnGuardar.Visible = false;
                }
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var ingreso = inputBuscar.Text;
            Articulo result =  this.client.GetById(ingreso).Result;
            List<Articulo> listaResult = new List<Articulo>();
            if(result.Id != 0) //Cuando no se encuentra Articulo se retorna desde el Cliente un new Articulo()
            {
                listaResult.Add(result);

            }
            else
            {
                MessageBox.Show("No se encontraron resultados");
                CleanInputs();
            }
            mainList.DataSource = listaResult;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ToggleAddingMode();              
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            Articulo deleted = (Articulo)mainList.SelectedRows[0].DataBoundItem;
            bool result = client.Delete(deleted.Id).Result;

            if(result)
                MessageBox.Show("Borrado!");
            else
                MessageBox.Show("Ocurrió un error y no pudo ser borrado");

            LoadGrid();
        }
    }
}
