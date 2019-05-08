using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using MovieDotNet.Data;

namespace MovieDotNet.UI
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginAction);
            RegisterCommand = new RelayCommand(RegisterAction);
        }

        #region commands
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }

        private void LoginAction()
        {
            var userId = DataManager.Instance.AuthenticateUser(username, password);
            if (userId == 0)
            {
                MessageBox.Show("Incorrect username or password");
            }
            else
            {
                VMLocator.Instance.Main = new MovieListViewModel(userId);
            }
        }

        private void RegisterAction()
        {
            var userId = DataManager.Instance.RegisterUser(username, password);
            if (userId == 0)
            {
                MessageBox.Show("Unable to register your user - username might be taken");
            }
            else
            {
                VMLocator.Instance.Main = new MovieListViewModel(userId);
            }
        }
        #endregion

        #region properties
        private string username;
        private string password;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                RaisePropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }
        #endregion

    }
}
