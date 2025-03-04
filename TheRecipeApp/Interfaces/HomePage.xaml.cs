using Microsoft.Maui.Storage;

namespace TheRecipeApp.Interfaces
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            UpdateUserStatus();
        }

        private void UpdateUserStatus()
        {
            var loggedInUser = Preferences.Get("LoggedInUsername", "");
            userStatusLabel.Text = string.IsNullOrEmpty(loggedInUser) ? "User Not Logged In" : $"Logged in as: {loggedInUser}";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateUserStatus(); 
        }
    }
}
