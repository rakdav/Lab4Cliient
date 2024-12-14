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

        public AppWindowViewModel()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + RegisterUser.access_token);
            Load();
        }
        private void Load()
        {
            Task<ObservableCollection<Product>> task = Task.Run(() => GetProducts());
            Products = task.Result;
        }
        private async Task<ObservableCollection<Product>> GetProducts()
        {
            ObservableCollection<Product>? list= await client.
                GetFromJsonAsync<ObservableCollection<Product>>("http://localhost:5000/api/products");
            return list!;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
