using SimpleTimer.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SimpleTimer.ViewModels
{
    public class SettingsExampleViewModel : ISettingsExampleViewModel
    {
        public SettingsExampleViewModel(IUserDataProvider userDataProvider)
        {
            this.UserDataProvider = userDataProvider;
            Initialize();
        }

        private void Initialize()
        {
            this.UserAlias = this.UserDataProvider.GetUserAlias();
            this.UserEmail = this.UserDataProvider.GetUserEmail();
            this.LastLogin = this.UserDataProvider.GetLastUserLoginTimeStamp();
        }

        private void UpdateUserAlias() => this.UserDataProvider.SetUserAlias(this.UserAlias);
        private void UpdateUserEmail() => this.UserDataProvider.SetUserEmail(this.UserEmail);
        private void UpdateUserLastLoginTimeStamp() => this.UserDataProvider.SetLastUserLoginTimeStamp(this.LastLogin);

        public ICommand LoginCommand => new RelayCommand(param => this.LastLogin = DateTime.Now);

        private IUserDataProvider UserDataProvider { get; }

        private string userAlias;
        public string UserAlias
        {
            get => this.userAlias;
            set
            {
                this.userAlias = value;
                OnPropertyChanged();
                UpdateUserAlias();
            }
        }

        private DateTime lastLogin;
        public DateTime LastLogin
        {
            get => this.lastLogin;
            set
            {
                this.lastLogin = value;
                OnPropertyChanged();
                UpdateUserLastLoginTimeStamp();

                this.LastLoginDisplay = this.LastLogin == default
                  ? string.Empty
                  : this.LastLogin.ToString("dddd, d MMMM yyyy hh:mm");
            }
        }

        private string lastLoginDisplay;
        public string LastLoginDisplay
        {
            get => this.lastLoginDisplay;
            set
            {
                this.lastLoginDisplay = value;
                OnPropertyChanged();
            }
        }

        private string userEmail;
        public string UserEmail
        {
            get => this.userEmail;
            set
            {
                this.userEmail = value;
                OnPropertyChanged();
                UpdateUserEmail();
            }
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Implementation of INotifyPropertyChanged
    }
}
