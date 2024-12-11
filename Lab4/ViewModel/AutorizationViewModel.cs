using Lab4.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lab4.View;
using System.Windows.Controls;
namespace Lab4.ViewModel
{
    internal class AutorizationViewModel: INotifyPropertyChanged
    {
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        private string password;
        public string LoginPassword
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("LoginPassword");
            }
        }

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                  (loginCommand = new RelayCommand(async obj =>
                  {
                      PasswordBox? password = obj as PasswordBox;
                      HttpClient client = new HttpClient();
                      User user = new User { Login = Login, Password = password!.Password };
                      JsonContent content = JsonContent.Create(user);
                      using var response = await client.PostAsync("http://localhost:5000/login", content);
                      string responseText = await response.Content.ReadAsStringAsync();
                      Response? resp = JsonSerializer.Deserialize<Response>(responseText);
                      if (resp != null)
                      {
                          AppWindow window = new AppWindow();
                          window.Show();
                      }
                  }));
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
