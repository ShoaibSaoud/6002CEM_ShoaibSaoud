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
            _database.CreateTableAsync<User>().Wait();
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
    }
}
