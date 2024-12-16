using Lab3.Models;
using Lab4.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        public ObservableCollection<Supplier> Suppliers { get; set; } = new();
        public ObservableCollection<Category> Categories { get; set; } = new();
        public AddEditWindow()
        {
            InitializeComponent();
        }
    }
}
