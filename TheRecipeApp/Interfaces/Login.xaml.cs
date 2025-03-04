using TheRecipeApp.Models;
using TheRecipeApp.Services;

namespace TheRecipeApp.Interfaces
{
    public partial class Login : ContentPage
    {
        private readonly DatabaseService _databaseService;

        // Constructor with dependency injection of DatabaseService
        public Login(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        // Event handler for Login Button click
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var username = usernameEntry.Text;
            var password = passwordEntry.Text;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please enter both username and password", "OK");
                return;
            }

            // Attempt to log in the user
            var user = await _databaseService.LoginUser(username, password);

            if (user != null)
            {
                //  Navigate to HomePage if successful
                await DisplayAlert("Success", "Login Successful!", "OK");
                await Shell.Current.GoToAsync("//HomePage"); 
            }
            else
            {
                // invalid credentials
                await DisplayAlert("Error", "Invalid Username or Password", "OK");
            }
        }

        // Navigate to SignUp Page
        private async void ToSignUpPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///SignUp"); 
        }
    }
}
