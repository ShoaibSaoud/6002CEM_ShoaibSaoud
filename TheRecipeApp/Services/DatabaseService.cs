using SQLite;
using System.Threading.Tasks;
using TheRecipeApp.Models;
using Microsoft.Maui.Storage;

namespace TheRecipeApp.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            await _database.CreateTableAsync<User>();
            await _database.CreateTableAsync<FavoriteRecipe>();
        }

        // Register new user
        public async Task<int> RegisterUser(User user)
        {
            var existingUser = await _database.Table<User>().Where(u => u.Username == user.Username).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return 0; // user already exists
            }

            return await _database.InsertAsync(user);
        }

        // Login User
        public async Task<User> LoginUser(string username, string password)
        {
            var user = await _database.Table<User>()
                                      .Where(u => u.Username == username && u.Password == password)
                                      .FirstOrDefaultAsync();

            if (user != null)
            {
                // Store user session
                Preferences.Set("LoggedInUsername", user.Username);
            }

            return user;
        }

       //logut
        public void LogoutUser()
        {
            Preferences.Remove("LoggedInUsername");
            AppShell.Instance.UpdateUserStatus();
        }

        // Get Logged-in User
        public string GetLoggedInUsername()
        {
            return Preferences.Get("LoggedInUsername", "User Not Logged In");
        }

        // update password (for logged in users only)
        public async Task<bool> UpdatePassword(string username, string currentPassword, string newPassword)
        {
            var user = await _database.Table<User>().Where(u => u.Username == username && u.Password == currentPassword).FirstOrDefaultAsync();
            if (user == null)
            {
                return false; 
            }
            user.Password = newPassword;
            await _database.UpdateAsync(user);
            return true;
        }

        // add to favorite
        public async Task<bool> AddFavorite(int recipeId)
        {
            string username = GetLoggedInUsername();
            if (string.IsNullOrEmpty(username))
                return false; // Not logged in

            var existingFavorite = await _database.Table<FavoriteRecipe>().Where(f => f.Username == username && f.RecipeId == recipeId).FirstOrDefaultAsync();
            if (existingFavorite == null)
            {
                await _database.InsertAsync(new FavoriteRecipe { Username = username, RecipeId = recipeId });
                return true;
            }
            return false;
        }

        // remove from favoritees
        public async Task<bool> RemoveFavorite(int recipeId)
        {
            string username = GetLoggedInUsername();
            if (string.IsNullOrEmpty(username))
                return false;
            var favorite = await _database.Table<FavoriteRecipe>().Where(f => f.Username == username && f.RecipeId == recipeId).FirstOrDefaultAsync();
            if (favorite != null)
            {
                await _database.DeleteAsync(favorite);
                return true;
            }
            return false;
        }

        // check for favorite
        public async Task<bool> IsFavorite(int recipeId)
        {
            string username = GetLoggedInUsername();
            if (string.IsNullOrEmpty(username))
                return false;
            var favorite = await _database.Table<FavoriteRecipe>().Where(f => f.Username == username && f.RecipeId == recipeId).FirstOrDefaultAsync();
            return favorite != null;
        }

        // get favorite recipes for user  (logged in)
        public async Task<List<int>> GetFavoriteRecipeIds()
        {
            string username = GetLoggedInUsername();
            if (string.IsNullOrEmpty(username))
                return new List<int>(); 
            var favorites = await _database.Table<FavoriteRecipe>().Where(f => f.Username == username).ToListAsync();
            return favorites.ConvertAll(f => f.RecipeId);
        }
    }
}
