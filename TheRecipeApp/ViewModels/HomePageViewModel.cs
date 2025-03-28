using TheRecipeApp.Models;
using TheRecipeApp.Services;
using System.Collections.ObjectModel;


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
                    FilterRecipes(); 
                }
            }
        }

        public List<string> Tags { get; set; }

        public HomePageViewModel()
        {
            _apiService = new ApiService();
            //filtering recipes lists
            Tags = new List<string>
            {
                "Clear", "main course", "side dish", "dessert", "appetizer", "salad",
                "bread", "breakfast", "soup", "beverage", "sauce",
                "marinade", "fingerfood", "snack", "drink"
            };
        }

        // To load random recipes from the API 
        public async Task LoadRandomRecipesAsync(string tag = "", string query = "")
        {
            try
            {
                List<Recipe> recipes;

                recipes = await _apiService.GetRandomRecipesAsync(tag, query);

                if (recipes == null || recipes.Count == 0)
                {
                    return; 
                }

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
               
            }
        }

        //To filter recipes based on the search text
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
