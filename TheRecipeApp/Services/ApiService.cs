using TheRecipeApp.Models;
using Newtonsoft.Json;


namespace TheRecipeApp.Services
{
    public class ApiService
    {
        private const string ApiKey = "026cbc15b82846e589307b5ef3981d75";
        private const string ApiBaseUrl = "https://api.spoonacular.com/recipes";

        private readonly HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient();
        }

        // Fetch random recipes with tag and query
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
