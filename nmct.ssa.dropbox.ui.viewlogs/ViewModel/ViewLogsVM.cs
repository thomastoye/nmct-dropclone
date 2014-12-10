using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nmct.ssa.dropbox.common;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Net.Http;
using Newtonsoft.Json;
using nmct.ssa.dropbox.ui.viewlogs;
using Thinktecture.IdentityModel.Client;

namespace nmct.ssa.dropbox.ui.viewlogs.ViewModel
{
    class ViewLogsVM : ObservableObject, IPage
    {
        private ObservableCollection<FileLog> _logs = new ObservableCollection<FileLog>();

        public ObservableCollection<FileLog> Logs
        {
            get { return _logs; }
            set { _logs = value; OnPropertyChanged("Logs"); }
        }

        private bool _tokenOk = false;

        public bool TokenOk
        {
            get { return _tokenOk; }
            set { _tokenOk = value; OnPropertyChanged("TokenOk"); }
        }


        private string _user = "Gebruikersnaam";

        public string UserName
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged("UserName"); }
        }

        private string _pass = "Paswoord";

        public string Password
        {
            get { return _pass; }
            set { _pass = value; OnPropertyChanged("Password"); }
        }


        public TokenResponse Token { get; set; }

        public string Name
        {
            get { return "Logs"; }
        }

        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        public ICommand ReloadCommand
        {
            get { return new RelayCommand(ReloadLogs); }
        }

        public async void Login()
        {
            Token = Webaccess.GetToken(UserName, Password);
            if (Token.IsError)
                TokenOk = false;
            else
                TokenOk = true;

        }

        public async void ReloadLogs()
        {
            Logs = await Webaccess.GetLogs("token.AccessToken");
        }
    }
}
