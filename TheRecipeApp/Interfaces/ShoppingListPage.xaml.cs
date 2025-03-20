using Microsoft.Maui.Controls;
using TheRecipeApp.ViewModels;

namespace TheRecipeApp.Interfaces
{
    public partial class ShoppingListPage : ContentPage
    {
        public ShoppingListPage()
        {
            InitializeComponent();
            BindingContext = new ShoppingListViewModel(this);
            
        }
    }
}
