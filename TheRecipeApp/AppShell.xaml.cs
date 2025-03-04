using Microsoft.Maui.Storage;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TheRecipeApp
{
    public partial class AppShell : Shell, INotifyPropertyChanged
    {
        private string _userStatus;

        public string UserStatus
        {
            get => _userStatus;
            set
            {
                if (_userStatus != value)
                {
                    _userStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public static AppShell Instance { get; private set; }

        public AppShell()
        {
            InitializeComponent();
            Instance = this; 
            UpdateUserStatus();
        }

        public void UpdateUserStatus()
        {
            var username = Preferences.Get("LoggedInUsername", "");
            UserStatus = string.IsNullOrEmpty(username) ? "User Not Logged In" : $"Logged in as: {username}";
        }

        // Logout Method
        public async Task Logout()
        {
            Preferences.Remove("LoggedInUsername"); 
            UpdateUserStatus();
            await Shell.Current.GoToAsync("//Login");
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await Logout();
        }
    }
}
