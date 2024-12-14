using Lab3.Models;
using Lab4.Model;
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
        public ObservableCollection<Product>? Products { get; set; }
        public ObservableCollection<Supplier> Suppliers { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
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
            Task<ObservableCollection<Product>> task1 = Task.Run(() => GetProducts());
            Products = task1.Result;
            Task<ObservableCollection<Category>> task2 = Task.Run(() => GetCategories());
            Categories = task2.Result;
            Task<ObservableCollection<Supplier>> task3 = Task.Run(() => GetSuppliers());
            Suppliers = task3.Result;
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
