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
using Biblio.Presentacion.Paginas;

namespace Biblio.Presentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string nom)
        {
            InitializeComponent();
            txtBUs.Text = "Bienvenido " + nom;
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            VentanaSesion vs = new VentanaSesion();
            this.Hide();
            vs.Show();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            LogoBiblioD.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            LogoBiblioD.Visibility = Visibility.Collapsed;
        }

        private void ButtonInicio_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new Inicio());
        }

        private void ButtonCPrestamo_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new ControlPrestamo());
        }

        private void ButtonMInterno_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new ManejoInterno());
        }

        private void ButtonCUsuarios_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new AgregarUsuario());
        }
    }
}
