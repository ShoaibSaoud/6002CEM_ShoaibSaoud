using Microsoft.Maui.Storage;
using TheRecipeApp.Services;

namespace TheRecipeApp.Interfaces
{
    public partial class Login : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public Login(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var username = usernameEntry.Text;
            var password = passwordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please enter both username and password", "OK");
                return;
            }

            var user = await _databaseService.LoginUser(username, password);

            if (user != null)
            {
                Preferences.Set("LoggedInUsername", user.Username);  // Ensure the username is saved
                Preferences.Set("IsLoggedIn", true); // Ensure that login state is saved

                AppShell.Instance.UpdateUserStatus();  // Assuming this updates UI elements based on login state

                await DisplayAlert("Success", $"Login Successful!\nWelcome {user.Username}", "OK");
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                await DisplayAlert("Error", "Invalid Username or Password", "OK");
            }
        }

        private async void ToSignUpPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//SignUp");
        }
    }
}
