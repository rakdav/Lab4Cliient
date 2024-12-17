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

namespace Lab4.View
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
            
        }

        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            AppWindow mainForm = AppWindow.Instance;
            mainForm.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.Instance!.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();            
        }
    }
}
