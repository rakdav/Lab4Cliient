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
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Lab4.ViewModel
{
    internal class AppWindowViewModel: INotifyPropertyChanged
    {
        private HttpClient client;
        private ObservableCollection<Product>? products;
        public ObservableCollection<Product>? Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
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
        public ICommand DoubleClickCommand { get; }
        public AppWindowViewModel()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + RegisterUser.access_token);
            Load();
            DoubleClickCommand = new RelayCommand(OnDoubleClick);
        }
        private async void OnDoubleClick(object parameter)
        {
            if (parameter is Product product)
            {
                AddEditWindow window = new AddEditWindow(product);
                if(window.ShowDialog()==true)
                {
                    product.Name = window.Product.Name;
                    product.Price = window.Product.Price;
                    product.SupplierId = window.Product.SupplierId;
                    product.CategoryId = window.Product.CategoryId;
                    await Edit(product);
                }
            }
        }
        private void Load()
        {
            try
            {
                Products = null;
                Task<ObservableCollection<Product>> task = Task.Run(() => GetProducts());
                Products = task.Result;
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
        
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(async obj =>
                  {
                      AddEditWindow window = new AddEditWindow(new Product());
                      if (window.ShowDialog() == true)
                      {
                          Product product = window.Product;
                          await Save(product);
                      }
                  }));
            }
        }
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(async obj =>
                  {
                      if (obj is Product product)
                      {
                          await Delete(product);
                      }
                  }));
            }
        }
        private async Task Save(Product product)
        {
            try
            {
                JsonContent content = JsonContent.Create(product);
                using var response = await client.PostAsync("http://localhost:5000/api/products", content);
                string responseText = await response.Content.ReadAsStringAsync();
                Load();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private async Task Edit(Product product)
        {
            try
            {
                JsonContent content = JsonContent.Create(product);
                using var response = await client.PutAsync("http://localhost:5000/api/products", content);
                string responseText = await response.Content.ReadAsStringAsync();
                Load();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private async Task Delete(Product product)
        {
            try
            {
                using var response = await client.DeleteAsync("http://localhost:5000/api/products/" + product?.ProductId);
                string responseText = await response.Content.ReadAsStringAsync();
                Load();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
