using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Lab3.Models
{
    public class Product: INotifyPropertyChanged
    {
        private long productId;
        public long ProductId
        {
            get { return productId; }
            set
            {
                productId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }
        private string? name;
        public string Name
        {
            get { return name!; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        private long categoryId;
        public long CategoryId
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }
        private long supplierId;
        public long SupplierId
        {
            get { return supplierId; }
            set
            {
                supplierId = value;
                OnPropertyChanged(nameof(SupplierId));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
