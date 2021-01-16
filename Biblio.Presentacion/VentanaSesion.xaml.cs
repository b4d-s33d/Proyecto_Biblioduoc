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
using System.Windows.Shapes;
using Biblio.Negocios;
using MaterialDesignThemes.Wpf;


namespace Biblio.Presentacion
{
    /// <summary>
    /// Lógica de interacción para VentanaSesion.xaml
    /// </summary>
    public partial class VentanaSesion : Window
    {
        public VentanaSesion()
        {
            InitializeComponent();
            txtNomUs.Text = string.Empty;
            passUs.Password = string.Empty;
            txtNomUs.Focus();
        }

        private void BtnSal_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            //o
            //System.Environment.Exit(0);
        }

        private void BtnIniSes_Click(object sender, RoutedEventArgs e)
        {
            Usuario unaSesion = new Usuario();
            try
            {
                unaSesion.NombreUs = txtNomUs.Text;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtNomUs.Text = string.Empty;
                txtNomUs.Focus();
                return;
            }

            try
            {
                unaSesion.Contrasenia = passUs.Password;
            }
            catch (ArgumentException zz)
            {
                MessageBox.Show(zz.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passUs.Password = string.Empty;
                passUs.Focus();
                return;
            }

            if (unaSesion.Read2())
            {
                if (unaSesion.TipoUs.ToString() == "Administrador")
                {

                    MessageBox.Show("Bienvenido " + unaSesion.Nombre + " " + unaSesion.Apellido, "Biblio Duoc",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Hide();
                    MainWindow admi = new MainWindow(unaSesion.Nombre);
                    admi.Show();
                }
                if (unaSesion.TipoUs.ToString() == "Bibliotecario")
                {

                    MessageBox.Show("Bienvenido " + unaSesion.Nombre + " " + unaSesion.Apellido, "Biblio Duoc",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Hide();
                    MainWindow2 bib = new MainWindow2(unaSesion.Nombre);
                    bib.Show();
                }
            }

            else
            {
                //DialogHost.Show("");
                MessageBox.Show("Nombre de usuario o contraseña incorrecta. Intente nuevamente.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                txtNomUs.Text = string.Empty;
                passUs.Password = string.Empty;
                txtNomUs.Focus();                
            
            }

            
        }
    }
}
