using TheRecipeApp.Models;
using TheRecipeApp.Services;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TheRecipeApp.Interfaces
{
    public partial class RecipeDetailPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private Recipe _recipe;
        private bool _isFavorite;

        public RecipeDetailPage(Recipe selectedRecipe)
        {
            InitializeComponent();
            _databaseService = ServiceHelper.GetService<DatabaseService>();

            _recipe = selectedRecipe;
            BindingContext = _recipe;

            LoadRecipeDetails(_recipe.Id);
            CheckFavoriteStatus();
        }

        // Loads recipe details (asynchronously)
        private async void LoadRecipeDetails(int recipeId)
        {
            try
            {
                var apiService = new ApiService();
                var recipeDetail = await apiService.GetRecipeDetailAsync(recipeId);
                if (recipeDetail != null)
                {
                    _recipe = recipeDetail;

              
                    _recipe.Summary = RemoveHtmlTags(_recipe.Summary);
                    Console.WriteLine($"Cleaned Summary: {_recipe.Summary}");
                    BindingContext = _recipe;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load recipe details: {ex.Message}", "OK");
            }
        }

        // Removing HTML tags from the summary text
        private string RemoveHtmlTags(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        // Checks if the recipe is already favorited
        private async void CheckFavoriteStatus()
        {
            _isFavorite = await _databaseService.IsFavorite(_recipe.Id);
            UpdateFavoriteButton();
        }

        // Favorite toggle Button 
        private void UpdateFavoriteButton()
        {
            FavoriteButton.Text = _isFavorite ? "Unfavorite ❤️" : "Favorite 🤍";
        }

        // Handle Favorite Button Click
        private async void OnFavoriteClicked(object sender, EventArgs e)
        {
            string username = _databaseService.GetLoggedInUsername();
            if (string.IsNullOrEmpty(username) || username == "User Not Logged In")
            {
                await DisplayAlert("Login Required", "Please log in to add the recipe to favorites.", "OK");
                return;
            }

            if (_isFavorite)
            {
                await _databaseService.RemoveFavorite(_recipe.Id);
                _isFavorite = false;
            }
            else
            {
                await _databaseService.AddFavorite(_recipe.Id);
                _isFavorite = true;
            }
            UpdateFavoriteButton();
        }
    }
}
