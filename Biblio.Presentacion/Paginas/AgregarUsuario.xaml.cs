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
    /// Lógica de interacción para AgregarUsuario.xaml
    /// </summary>
    public partial class AgregarUsuario : Page
    {
        Manejadora mane = new Manejadora();

        public AgregarUsuario()
        {
            InitializeComponent();
            cboTipo.ItemsSource = Enum.GetValues(typeof(TipoUsuario));
            Limpiar();
        }

        public void Limpiar()
        {
            txtRut1.Text = string.Empty;
            txtNombre1.Text = string.Empty;
            txtApellido1.Text = string.Empty;
            cboTipo.SelectedIndex = -1;
            txtNombreUs.Text = string.Empty;
            txtContrasenia.Text = string.Empty;
            txtRut1.Focus();
        }

        private void BtnAgregarUs_Click(object sender, RoutedEventArgs e)
        {
            Negocios.Usuario usu = new Negocios.Usuario();

            try
            {
                usu.Rut = txtRut1.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtRut1.Text = string.Empty;
                txtRut1.Focus();
                return;
            }

            try
            {
                usu.Nombre = txtNombre1.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtNombre1.Text = string.Empty;
                txtNombre1.Focus();
                return;
            }

            try
            {
                usu.Apellido = txtApellido1.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtApellido1.Text = string.Empty;
                txtApellido1.Focus();
                return;
            }

            try
            {
                usu.NombreUs = txtNombreUs.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtNombreUs.Text = string.Empty;
                txtNombreUs.Focus();
                return;
            }

            try
            {
                usu.Contrasenia = txtContrasenia.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtContrasenia.Text = string.Empty;
                txtContrasenia.Focus();
                return;
            }

            try
            {
                usu.TipoUs = (TipoUsuario)cboTipo.SelectedValue;

                if (usu.Create())
                {
                    MessageBox.Show("¡Usuario creado con éxito!", "Control usuarios", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Usuario no pudo ser creado.", "Control usuarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception zz)
            {
                MessageBox.Show(zz.Message, "Error de creación.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Limpiar();
        }

        private void BtnVerUs_Click(object sender, RoutedEventArgs e)
        {
            dGridMostrar.ItemsSource = (
                from usu in mane.ListarUsuario()
                select new
                {
                    Rut = usu.Rut,
                    Nombre = usu.Nombre,
                    Apellido = usu.Apellido,
                    TipoUs = usu.TipoUs,
                    NombreUs = usu.NombreUs,
                    Contrasenia = usu.Contrasenia
                }
                )
                .ToList();
        }

        private void BtnModificarUs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario usu = new Usuario()
                {

                    Rut = txtRut1.Text,
                    Nombre = txtNombre1.Text,
                    Apellido = txtApellido1.Text,
                    TipoUs = (TipoUsuario)(cboTipo.SelectedValue),
                    NombreUs = txtNombreUs.Text,
                    Contrasenia = txtContrasenia.Text,

                };
                if (usu.Update())
                {
                    Limpiar();
                    MessageBox.Show("Usuario actualizado.", "Control usuarios", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Usuario no pudo ser actualizado.", "Control usuarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
            }
        }

        private void BtnBuscarUs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Biblio.Negocios.Usuario usu = new Biblio.Negocios.Usuario()
                {
                    Rut = txtRut1.Text
                };
                if (usu.Read())
                {
                    txtRut1.Text = usu.Rut;
                    txtNombre1.Text = usu.Nombre;
                    txtApellido1.Text = usu.Apellido;
                    cboTipo.SelectedValue = usu.TipoUs;
                    txtNombreUs.Text = usu.NombreUs;
                    txtContrasenia.Text = usu.Contrasenia;
                }
                else
                {
                    Limpiar();
                    MessageBox.Show("Usuario no encontrado.", "Control usuarios", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception zz)
            {

                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
            }
        }

        private void BtnEliminarUs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario usu = new Usuario()
                {
                    Rut = txtRut1.Text

                };
                if (usu.Delete())
                {
                    Limpiar();
                    MessageBox.Show("¡Usuario eliminado con éxito!", "Control usuarios", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Limpiar();
                    MessageBox.Show("Usuario no pudo ser eliminado.", "Control usuarios", MessageBoxButton.OK, MessageBoxImage.Error);
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
