using TheRecipeApp.Models;
using TheRecipeApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TheRecipeApp.Services;

namespace TheRecipeApp.ViewModels
{
    public class HomePageViewModel
    {
        private readonly ApiService _apiService;

        // ObservableCollection to store all recipes
        public ObservableCollection<Recipe> AllRecipes { get; set; } = new ObservableCollection<Recipe>();

        // Observable collection to hold the filtered recipes
        public ObservableCollection<Recipe> FilteredRecipes { get; set; } = new ObservableCollection<Recipe>();

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    FilterRecipes(); // Filter recipes whenever the search text changes
                }
            }
        }

        public List<string> Tags { get; set; }

        public HomePageViewModel()
        {
            _apiService = new ApiService();

            Tags = new List<string>
            {
                "Clear", "main course", "side dish", "dessert", "appetizer", "salad",
                "bread", "breakfast", "soup", "beverage", "sauce",
                "marinade", "fingerfood", "snack", "drink"
            };
        }

        // Method to load random recipes from the API based on the selected tag and search query
        public async Task LoadRandomRecipesAsync(string tag = "", string query = "")
        {
            try
            {
                List<Recipe> recipes;

                // Fetch recipes from the API without requiring a tag or query
                recipes = await _apiService.GetRandomRecipesAsync(tag, query);

                // Ensure that recipes is not null
                if (recipes == null || recipes.Count == 0)
                {
                    return; // Prevent clearing existing data if no new data is available
                }

                // Clear previous data only if new data is available
                AllRecipes.Clear();
                FilteredRecipes.Clear();

                foreach (var recipe in recipes)
                {
                    AllRecipes.Add(recipe);
                    FilteredRecipes.Add(recipe);
                }
            }
            catch (Exception ex)
            {
                // Handle error (e.g., log it)
            }
        }

        // Method to filter recipes based on the search text
        public void FilterRecipes()
        {
            FilteredRecipes.Clear();

            var filtered = AllRecipes
                .Where(r => r.Title != null && r.Title.ToLower().Contains(SearchText.Trim().ToLower()))
                .ToList();

            foreach (var recipe in filtered)
            {
                FilteredRecipes.Add(recipe);
            }
        }
    }
}
