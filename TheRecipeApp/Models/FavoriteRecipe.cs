using SQLite;

namespace TheRecipeApp.Models
{
    public class FavoriteRecipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
