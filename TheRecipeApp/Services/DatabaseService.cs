using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRecipeApp.Models;
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
        public Task<int> RegisterUser(User user)
        {
            return _database.InsertAsync(user);
        }

        // Check if the user exists (for login)
        public Task<User> LoginUser(string username, string password)
        {
            return _database.Table<User>().Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
        }
    }
}