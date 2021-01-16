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
    /// Lógica de interacción para ControlPrestamo.xaml
    /// </summary>
    public partial class ControlPrestamo : Page
    {
        Manejadora mane = new Manejadora();

        public ControlPrestamo()
        {
            InitializeComponent();
            cboTipo.ItemsSource = Enum.GetValues(typeof(TipoSolicitante));
            Limpiar();
        }

        public void Limpiar()
        {
            txtCodigoL.Text = string.Empty;
            txtRut.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            cboTipo.SelectedIndex = -1;
            dtFechaPr.SelectedDate = DateTime.Now;
            txtCodigoL.Focus();
        }

        private void BtnRealizarPr_Click(object sender, RoutedEventArgs e)
        {
            Negocios.Solicitante sol = new Negocios.Solicitante();

            try
            {
                sol.CodigoLibro = txtCodigoL.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtCodigoL.Text = string.Empty;
                txtCodigoL.Focus();
                return;
            }

            try
            {
                sol.Rut = txtRut.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtRut.Text = string.Empty;
                txtRut.Focus();
                return;
            }

            try
            {
                sol.Nombre = txtNombre.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtNombre.Text = string.Empty;
                txtNombre.Focus();
                return;
            }

            try
            {
                sol.Apellido = txtApellido.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtApellido.Text = string.Empty;
                txtApellido.Focus();
                return;
            }

            try
            {
                sol.Tipo = (TipoSolicitante)cboTipo.SelectedValue;
                sol.FechaPr = dtFechaPr.SelectedDate.Value;

                if (sol.Create())
                {
                    MessageBox.Show("¡Préstamo ingresado con éxito!", "Préstamos", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Préstamo no pudo ser ingresado.", "Préstamos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception zz)
            {
                MessageBox.Show(zz.Message, "Error de ingreso.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Limpiar();
        }

        private void BtnVerPre_Click(object sender, RoutedEventArgs e)
        {
            dGridMostrar.ItemsSource = (
                from sol in mane.ListarSolicitante()
                select new
                {
                    CodigoLibro = sol.CodigoLibro,
                    Rut = sol.Rut,
                    Nombre = sol.Nombre,                  
                    Apellido = sol.Apellido,
                    Tipo = sol.Tipo,
                    FechaPr = sol.FechaPr
                }
                )
                .ToList();
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

        private void BtnModificarPr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Solicitante sol = new Solicitante()
                {

                    CodigoLibro = txtCodigoL.Text,
                    Rut = txtRut.Text,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Tipo = (TipoSolicitante)(cboTipo.SelectedValue),
                    FechaPr = dtFechaPr.SelectedDate.Value

                };
                if (sol.Update())
                {
                    Limpiar();
                    MessageBox.Show("Préstamo actualizado.", "Préstamos", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Préstamo no pudo ser actualizado.", "Préstamos", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
            }
        }

        private void BtnBuscarPr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Biblio.Negocios.Solicitante sol = new Biblio.Negocios.Solicitante()
                {
                    CodigoLibro = txtCodigoL.Text
                };
                if (sol.Read())
                {
                    txtCodigoL.Text = sol.CodigoLibro;
                    txtRut.Text = sol.Rut;
                    txtNombre.Text = sol.Nombre;
                    txtApellido.Text = sol.Apellido;
                    cboTipo.SelectedValue = sol.Tipo;
                    dtFechaPr.SelectedDate = sol.FechaPr;
                }
                else
                {
                    Limpiar();
                    MessageBox.Show("Préstamo no encontrado.", "Préstamos", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception zz)
            {

                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
            }
        }

        private void BtnRealizarDev_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Solicitante sol = new Solicitante()
                {
                    CodigoLibro = txtCodigoL.Text

                };
                if (sol.Delete())
                {
                    Limpiar();
                    MessageBox.Show("¡Devolución realizada con éxito!", "Préstamos", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Limpiar();
                    MessageBox.Show("Devolución no pudo realizarse.", "Préstamos", MessageBoxButton.OK, MessageBoxImage.Error);
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
