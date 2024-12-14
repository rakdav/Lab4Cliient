using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab4.Model
{
    public class Supplier: INotifyPropertyChanged
    {
        private long supplierId { get; set; }
        public long SupplierId
        {
            get { return supplierId; }
            set
            {
                supplierId = value;
                OnPropertyChanged(nameof(SupplierId));
            }
        }
        private string? name { get; set; }
        public string Name
        {
            get { return name!; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string? city;
        public string City
        {
            get { return city!; }
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
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
