using TheRecipeApp.Models;
using TheRecipeApp.Services;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace TheRecipeApp.Interfaces
{
    public partial class RecipeDetailPage : ContentPage
    {
        public RecipeDetailPage(Recipe selectedRecipe)
        {
            InitializeComponent();
            BindingContext = selectedRecipe; 
            LoadRecipeDetails(selectedRecipe.Id);
        }

        // BindingContext and loads the recipe with full details
        private async void LoadRecipeDetails(int recipeId)
        {
            try
            {
                var apiService = new ApiService();
                var recipeDetail = await apiService.GetRecipeDetailAsync(recipeId);

               
                BindingContext = recipeDetail;  
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load recipe details: {ex.Message}", "OK");
            }
        }
    }
}
