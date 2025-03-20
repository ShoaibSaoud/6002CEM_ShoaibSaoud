using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace TheRecipeApp.Interfaces
{
    public partial class ShoppingListPage : ContentPage
    {
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; }

        public ShoppingListPage()
        {
            InitializeComponent();

            // Initialize shopping list
            ShoppingItems = new ObservableCollection<ShoppingItem>();

            // Check if the user is logged in
            if (IsUserLoggedIn())
            {
                LoadUserShoppingList();
            }
            else
            {
                ShoppingItems.Clear();
            }

            BindingContext = this;
        }

        private bool IsUserLoggedIn()
        {
            return Preferences.Get("IsLoggedIn", false); 
        }

        private void LoadUserShoppingList()
        {
            string savedItems = Preferences.Get("ShoppingList", string.Empty);
            if (!string.IsNullOrEmpty(savedItems))
            {
                var items = savedItems.Split(',').ToList();
                foreach (var item in items)
                {
                    ShoppingItems.Add(new ShoppingItem { Name = item });
                }
            }
        }

        private void SaveUserShoppingList()
        {

            var items = ShoppingItems.Select(i => i.Name).ToArray();
            Preferences.Set("ShoppingList", string.Join(",", items));
        }

        private void OnAddItemClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewItemEntry.Text))
            {
                var newItem = new ShoppingItem { Name = NewItemEntry.Text };
                ShoppingItems.Add(newItem);
                SaveUserShoppingList();
                NewItemEntry.Text = string.Empty;
            }
        }
    }

    public class ShoppingItem
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
