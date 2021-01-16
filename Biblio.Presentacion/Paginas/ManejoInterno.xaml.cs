using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Biblio.Negocios;

namespace Biblio.Presentacion.Paginas
{
    /// <summary>
    /// Lógica de interacción para ManejoInterno.xaml
    /// </summary>
    public partial class ManejoInterno : Page
    {
        Manejadora mane = new Manejadora();

        public ManejoInterno()
        {
            InitializeComponent();
            cboGenero.ItemsSource = Enum.GetValues(typeof(Genero));
            Limpiar();
        }

        public void Limpiar()
        {
            txtCodigo.Text = string.Empty;
            txtTitulo.Text = string.Empty;
            txtAutor.Text = string.Empty;
            txtAnioEd.Text = string.Empty;
            cboGenero.SelectedIndex = -1;
            txtEditorial.Text = string.Empty;
            dtFechaIn.SelectedDate = DateTime.Now;
            txtCodigo.Focus();
        }

        private void BtnAgregarLibro_Click(object sender, RoutedEventArgs e)
        {
            Negocios.Libro Lib = new Negocios.Libro();

            try
            {
                Lib.Codigo = txtCodigo.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtCodigo.Text = string.Empty;
                txtCodigo.Focus();
                return;
            }

            try
            {
                Lib.Titulo = txtTitulo.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtTitulo.Text = string.Empty;
                txtTitulo.Focus();
                return;
            }

            try
            {
                Lib.Autor = txtAutor.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtAutor.Text = string.Empty;
                txtAutor.Focus();
                return;
            }

            try
            {
                Lib.Editorial = txtEditorial.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtEditorial.Text = string.Empty;
                txtEditorial.Focus();
                return;
            }

            try
            {
                int a;
                int.TryParse(txtAnioEd.Text, out a);
                Lib.AnioEd = a;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtAnioEd.Text = string.Empty;
                txtAnioEd.Focus();
                return;
            }

            try
            {
                Lib.Genero = (Genero)cboGenero.SelectedValue;
                Lib.FechaIn = dtFechaIn.SelectedDate.Value;

                if (Lib.Create())
                {
                    MessageBox.Show("¡Libro ingresado con éxito!", "Catálogo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Libro no pudo ser ingresado.", "Catálogo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception zz)
            {
                MessageBox.Show(zz.Message, "Error de ingreso.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Limpiar();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Biblio.Negocios.Libro lib = new Biblio.Negocios.Libro()
                {
                    Codigo = txtCodigo.Text
                };
                if (lib.Read())
                {
                    txtCodigo.Text = lib.Codigo;
                    txtTitulo.Text = lib.Titulo;
                    txtAutor.Text = lib.Autor;
                    txtAnioEd.Text = lib.AnioEd.ToString();
                    cboGenero.SelectedValue = lib.Genero;
                    txtEditorial.Text = lib.Editorial;
                    dtFechaIn.SelectedDate = lib.FechaIn;
                }
                else
                {
                    Limpiar();
                    MessageBox.Show("Libro no encontrado.", "Catálogo", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception zz)
            {

                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
            }
        }

        private void BtnVerCat_Click(object sender, RoutedEventArgs e)
        {
            dGridMostrar.ItemsSource = (
                from lib in mane.ListarLibro()
                select new
                {
                    Codigo = lib.Codigo,
                    Titulo = lib.Titulo,
                    Autor = lib.Autor,
                    AnioEd = lib.AnioEd,
                    Genero = lib.Genero,
                    Editorial = lib.Editorial,
                    FechaIn = lib.FechaIn

                }
                )
                .ToList();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Libro lib = new Libro()
                {
                    Codigo = txtCodigo.Text

                };
                if (lib.Delete())
                {
                    Limpiar();
                    MessageBox.Show("¡Libro eliminado con éxito!", "Catálogo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Limpiar();
                    MessageBox.Show("Libro no pudo ser eliminado.", "Catálogo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
            }
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Libro lib = new Libro()
                {
                    Codigo = txtCodigo.Text,
                    Titulo = txtTitulo.Text,
                    Autor = txtAutor.Text,
                    AnioEd = int.Parse(txtAnioEd.Text),
                    Genero = (Genero)(cboGenero.SelectedValue),
                    Editorial = txtEditorial.Text,
                    FechaIn = dtFechaIn.SelectedDate.Value

                };
                if (lib.Update())
                {
                    Limpiar();
                    MessageBox.Show("Libro Actualizado.", "Catálogo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Libro no pudo ser actualizado.", "Catálogo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
            }
        }
    }
}
