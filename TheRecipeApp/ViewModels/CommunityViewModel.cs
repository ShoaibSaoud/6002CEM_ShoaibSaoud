using TheRecipeApp.Models;
using TheRecipeApp.Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace TheRecipeApp.ViewModels
{
    public class CommunityViewModel
    {
        private readonly ApiService _apiService;
        public ObservableCollection<Recipe> AllRecipes { get; set; } = new ObservableCollection<Recipe>();

        public CommunityViewModel()
        {
            _apiService = new ApiService();
        }
        public async Task LoadRandomRecipesAsync(string tag = "", string query = "")
        {
            try
            {
                List<Recipe> recipes = await _apiService.GetRandomRecipesAsync(tag, query);
                if (recipes == null || recipes.Count == 0)
                {
                    return;
                }
                AllRecipes.Clear();
                foreach (var recipe in recipes)
                {
                    recipe.Summary = RemoveHtmlTags(recipe.Summary);
                    Console.WriteLine($"Cleaned Summary: {recipe.Summary}");
                    AllRecipes.Add(recipe);
                }
            }
            catch (Exception ex)
            {
            }
        }
        // removing HTML tags from the summary text
        private string RemoveHtmlTags(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}
