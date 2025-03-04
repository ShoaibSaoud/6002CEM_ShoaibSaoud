using TheRecipeApp.Models;
using TheRecipeApp.Services;

namespace TheRecipeApp.Interfaces
{
    public partial class SignUp : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public SignUp(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        // Event handler for Sign Up Button click
        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            var username = usernameEntry.Text;
            var password = passwordEntry.Text;
            var confirmPassword = confirmPasswordEntry.Text;

            // Basic validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            // Create a new user model
            var newUser = new User
            {
                Username = username,
                Password = password  // Hash the password for production apps!
            };

            // Register the user in the database
            var result = await _databaseService.RegisterUser(newUser);

            if (result > 0)
            {
                // Success: Navigate to Login page
                await DisplayAlert("Success", "Account created!", "OK");
                await Shell.Current.GoToAsync("//Login");
            }
            else
            {
                // Failure: Handle error (e.g., user already exists)
                await DisplayAlert("Error", "Registration failed. Try again.", "OK");
            }
        }

        // Navigate to Login Page
        private async void ToLoginPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///Login");
        }
    }
}
