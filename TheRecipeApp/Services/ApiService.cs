using TheRecipeApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheRecipeApp.Models;

namespace TheRecipeApp.Services
{
    public class ApiService
    {
        private const string ApiKey = "b67bbd0e928c4b5bb87a41232b6aa6f8";
        private const string ApiBaseUrl = "https://api.spoonacular.com/recipes";

        private readonly HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient();
        }

        // Fetch random recipes with optional tag and query
        public async Task<List<Recipe>> GetRandomRecipesAsync(string tag = "", string query = "")
        {
            string apiUrl = $"{ApiBaseUrl}/random?apiKey={ApiKey}&number=10";

            if (!string.IsNullOrEmpty(tag))
                apiUrl += $"&tags={tag}";

            if (!string.IsNullOrEmpty(query))
                apiUrl += $"&query={query}";  

            var response = await _client.GetStringAsync(apiUrl);
            var recipes = JsonConvert.DeserializeObject<RandomRecipeResponse>(response);

            return recipes?.Recipes ?? new List<Recipe>();
        }

        // Fetch full details of a recipe
        public async Task<Recipe> GetRecipeDetailAsync(int recipeId)
        {
            string apiUrl = $"{ApiBaseUrl}/{recipeId}/information?apiKey={ApiKey}";

            var response = await _client.GetStringAsync(apiUrl);
            var recipeDetail = JsonConvert.DeserializeObject<Recipe>(response);

            return recipeDetail;
        }

        // API response structure for random recipes
        public class RandomRecipeResponse
        {
            public List<Recipe> Recipes { get; set; }
        }

    }
}
