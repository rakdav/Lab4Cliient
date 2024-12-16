using Lab3.Models;
using Lab4.Model;
using Lab4.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Lab4.ViewModel
{
    internal class AppWindowViewModel: INotifyPropertyChanged
    {
        private HttpClient client;
        public ObservableCollection<Product>? Products { get; set; } = new();
       
        private Product selectedProduct;
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }
        public AppWindowViewModel()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + RegisterUser.access_token);
            Load();
        }
        private void Load()
        {
            try
            {
                Task<ObservableCollection<Product>> task1 = Task.Run(() => GetProducts());
                Products = task1.Result;
                Task<ObservableCollection<Category>> task2 = Task.Run(() => GetCategories());
                Categories = task2.Result;
                Task<ObservableCollection<Supplier>> task3 = Task.Run(() => GetSuppliers());
                Suppliers = task3.Result;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async Task<ObservableCollection<Product>> GetProducts()
        {
            ObservableCollection<Product>? list= await client.
                GetFromJsonAsync<ObservableCollection<Product>>("http://localhost:5000/api/products");
            return new ObservableCollection<Product>(list!);
        }
        private async Task<ObservableCollection<Category>> GetCategories()
        {
            List<Category>? list = await client.
                GetFromJsonAsync<List<Category>>("http://localhost:5000/api/categories");
            return new ObservableCollection<Category>(list!);
        }

        private async Task<ObservableCollection<Supplier>> GetSuppliers()
        {
            List<Supplier>? list = await client.
                GetFromJsonAsync<List<Supplier>>("http://localhost:5000/api/suppliers");
            return new ObservableCollection<Supplier>(list!);
        }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(async obj =>
                  {
                      AddEditWindow window = new AddEditWindow();
                      if (window.ShowDialog() == true)
                      {

                      }
                  }));
            }
        }
        private async Task Save(Product product)
        {
            JsonContent content = JsonContent.Create(product);
            using var response = await client.PostAsync("http://localhost:5000/api/products", content);
            string responseText = await response.Content.ReadAsStringAsync();
            Load();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
