using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace DiscosProject
{
    public partial class NuevoDisco : Form
    {
        private Discos disco = null;
        public NuevoDisco()
        {
            InitializeComponent();
        }

        public NuevoDisco(Discos disco)
        {
            InitializeComponent();
            this.disco = disco;
            Text = "Modificar Disco";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
           
            DiscosNegocio negocio = new DiscosNegocio();
            try
            {
                if (disco == null)
                    disco = new Discos();

                disco.Titulo = txtTitulo.Text;
                disco.FechaLanzamiento = dtpFecha.Value;
                disco.CantidadCanciones = int.Parse(txtCantidadCanciones.Text);
                disco.UrlImagen = txtUrlImagen.Text;
                disco.Tipo = (Estilos)cbxEstilos.SelectedItem;
                disco.Formato = (TiposEdicion)cbxTiposEdicion.SelectedItem;

                if(disco.Id != 0)
                {
                    negocio.modificar(disco);
                    MessageBox.Show("Disco modificado exitosamente");
                }else
                {
                    negocio.agregar(disco);
                    MessageBox.Show("Disco agregado exitosamente");
                }

                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void NuevoDisco_Load(object sender, EventArgs e)
        {
            EstilosNegocio estilosNegocio = new EstilosNegocio();
            FormatoNegocio tiposEdicion = new FormatoNegocio();
            try
            {
                cbxEstilos.DataSource = estilosNegocio.Listar();
                cbxEstilos.ValueMember = "Id";
                cbxEstilos.DisplayMember = "Descripcion";
                cbxTiposEdicion.DataSource = tiposEdicion.Listar();
                cbxTiposEdicion.ValueMember = "Id";
                cbxTiposEdicion.DisplayMember = "Formato";

                if(disco != null)
                {
                    txtTitulo.Text = disco.Titulo;
                    dtpFecha.Value = disco.FechaLanzamiento;
                    txtCantidadCanciones.Text = disco.CantidadCanciones.ToString();
                    txtUrlImagen.Text = disco.UrlImagen;
                    cargarImagen(disco.UrlImagen);
                    cbxEstilos.SelectedValue = disco.Tipo.Id;
                    cbxTiposEdicion.SelectedValue = disco.Formato.Id;

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxNuevoDisco.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxNuevoDisco.Load("https://www.pngkey.com/png/detail/233-2332677_image-500580-placeholder-transparent.png");
            }
        }
    }
}
