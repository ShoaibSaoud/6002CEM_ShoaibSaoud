using TheRecipeApp.Services;
using TheRecipeApp.Models;

namespace TheRecipeApp.Interfaces
{
    public partial class ProfilePage : ContentPage
    {
     
        private readonly ApiService _apiService;
        private readonly DatabaseService _databaseService;
        public ProfilePage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _apiService = new ApiService();
            LoadUserData();
            LoadFavoriteRecipes();
        }

        // Load user data (if not logged in it shows Not Logged In, NL )
        private void LoadUserData()
        {
            bool isLoggedIn = Preferences.Get("IsLoggedIn", false);
            string username = Preferences.Get("LoggedInUsername", string.Empty);

            if (!isLoggedIn || string.IsNullOrWhiteSpace(username))
            {
                UsernameLabel.Text = "Not Logged In";
                ProfileInitials.Text = "NL";
            }
            else
            {
                UsernameLabel.Text = username;
                ProfileInitials.Text = GetInitials(username);
            }
        }

        // Get initials from username (first and last aplhabet)
        private string GetInitials(string username)
        {
            string[] words = username.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return words.Length == 1 ? words[0][0].ToString().ToUpper(): (words[0][0].ToString() + words[1][0].ToString()).ToUpper();
        }

        // password update button click
        private async void OnUpdatePasswordClicked(object sender, EventArgs e)
        {
            string username = Preferences.Get("LoggedInUsername", string.Empty);
            if (string.IsNullOrWhiteSpace(username))
            {
                await DisplayAlert("Error", "You must be logged in to update your password.", "OK");
                return;
            }
            string currentPassword = CurrentPasswordEntry.Text;
            string newPassword = NewPasswordEntry.Text;
            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }
            bool isUpdated = await _databaseService.UpdatePassword(username, currentPassword, newPassword);
            if (isUpdated)
            {
                await DisplayAlert("Success", "Password updated successfully!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Incorrect current password.", "OK");
            }
        }


        // Load favorite recipes
        private async void LoadFavoriteRecipes()
        {
            var favoriteRecipeIds = await _databaseService.GetFavoriteRecipeIds();
            if (favoriteRecipeIds.Count > 0)
            {
                NoFavoritesMessage.IsVisible = false;
                FavoritesList.IsVisible = true;
                List<Recipe> favoriteRecipes = new List<Recipe>();
                foreach (int recipeId in favoriteRecipeIds)
                {
                    var recipe = await _apiService.GetRecipeDetailAsync(recipeId);
                    if (recipe != null)
                    {
                        favoriteRecipes.Add(recipe);
                    }
                }
                FavoritesList.ItemsSource = favoriteRecipes;
            }
            else
            {
                NoFavoritesMessage.IsVisible = true;
                FavoritesList.IsVisible = false;
            }
        }

        // Handle favorite item tap
        private async void OnFavoriteItemTapped(object sender, EventArgs e)
        {
            if (sender is Frame frame && frame.BindingContext is Recipe selectedRecipe)
            {
                await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe));
            }
        }
    }
}
