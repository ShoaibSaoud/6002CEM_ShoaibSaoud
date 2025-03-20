using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace TheRecipeApp.ViewModels
{
    public class ShoppingListViewModel : BindableObject
    {
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; }
        public ICommand AddItemCommand { get; }
        public ICommand RemoveItemCommand { get; }

        public string NewItemText { get; set; }
        private string UserId;
        private readonly ContentPage _page;

        public ShoppingListViewModel(ContentPage page)
        {
            ShoppingItems = new ObservableCollection<ShoppingItem>();
            AddItemCommand = new Command(OnAddItem);
            RemoveItemCommand = new Command<ShoppingItem>(OnRemoveItem);

            _page = page;

            UserId = Preferences.Get("LoggedInUsername", "");
            LoadUserShoppingList();
        }

        private void LoadUserShoppingList()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                ShoppingItems.Clear(); 
                return;
            }

            string savedItems = Preferences.Get($"ShoppingList_{UserId}", string.Empty);
            ShoppingItems.Clear();

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
            if (string.IsNullOrEmpty(UserId)) return;

            var items = ShoppingItems.Select(i => i.Name).ToArray();
            Preferences.Set($"ShoppingList_{UserId}", string.Join(",", items));
        }

        private async void OnAddItem()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                // Show an error message if the user is not logged in
                await _page.DisplayAlert("Error", "You must be logged in to add items to the shopping list.", "OK");
                return;
            }

            var newItem = new ShoppingItem { Name = NewItemText }; 
            ShoppingItems.Add(newItem);
            SaveUserShoppingList();
            NewItemText = string.Empty; 
        }

        private void OnRemoveItem(ShoppingItem item)
        {
            if (string.IsNullOrEmpty(UserId)) return;

            ShoppingItems.Remove(item);
            SaveUserShoppingList();
        }
    }

    public class ShoppingItem
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
