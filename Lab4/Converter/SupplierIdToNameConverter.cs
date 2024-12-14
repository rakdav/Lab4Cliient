using Lab3.Models;
using Lab4.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Lab4.Converter
{
    public class SupplierIdToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long id = (long)value;
            Task<Supplier> task = Task.Run(() => GetSupplier(id));
            return task.Result.Name!;
        }
        private async Task<Supplier> GetSupplier(long id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + RegisterUser.access_token);
            Supplier? supplier = await client.
                GetFromJsonAsync<Supplier>("http://localhost:5000/api/Suppliers/"+id);
            return supplier!;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
