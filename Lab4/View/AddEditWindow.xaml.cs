using Lab3.Models;
using Lab4.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
        public Product Product { get; private set; }
        private HttpClient client;
        private List<Category> Categories { get; set; } = new();
        private List<Supplier> Suppliers { get; set; } = new();
        public AddEditWindow(Product _product)
        {
            InitializeComponent();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + RegisterUser.access_token);
            Product = _product;
            DataContext = Product;
            Load();
            CategoryList.ItemsSource = Categories;
            SupplierList.ItemsSource = Suppliers;
        }
        private void Load()
        {
            try
            {
                Task<List<Category>> task1 = Task.Run(() => GetCategories());
                Categories = task1.Result;
                Task<List<Supplier>> task2 = Task.Run(() => GetSuppliers());
                Suppliers = task2.Result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private async Task<List<Category>> GetCategories()
        {
            List<Category>? list = await client.
                GetFromJsonAsync<List<Category>>("http://localhost:5000/api/categories");
            return new List<Category>(list!);
        }

        private async Task<List<Supplier>> GetSuppliers()
        {
            List<Supplier>? list = await client.
                GetFromJsonAsync<List<Supplier>>("http://localhost:5000/api/suppliers");
            return new List<Supplier>(list!);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
