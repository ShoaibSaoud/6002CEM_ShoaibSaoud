using Microsoft.Maui.Storage;
using System;
using TheRecipeApp.ViewModels;
using TheRecipeApp.Models;
using TheRecipeApp.Services;

namespace TheRecipeApp.Interfaces
{
    public partial class HomePage : ContentPage
    {
        private readonly HomePageViewModel _viewModel;
        public HomePage()
        {
            InitializeComponent();
            UpdateUserStatus();

            _viewModel = new HomePageViewModel();
            this.BindingContext = _viewModel;
            tagPicker.SelectedItem = "Filter";
            _viewModel.LoadRandomRecipesAsync();

        }

        private void UpdateUserStatus()
        {
            var loggedInUser = Preferences.Get("LoggedInUsername", "");
            userStatusLabel.Text = string.IsNullOrEmpty(loggedInUser) ? "User Not Logged In" : $"Welcome {loggedInUser}!";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateUserStatus(); 
        }

        //activates when the search icon is tapped
        private async void OnSearchIconTapped(object sender, EventArgs e)
        {
            await _viewModel.LoadRandomRecipesAsync(tagPicker.SelectedItem.ToString(), _viewModel.SearchText);
        }

        //activates when the search text changes
        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SearchText = e.NewTextValue; 
        }

        //activates when the user selects a filter from the Picker
        private async void OnTagSelected(object sender, EventArgs e)
        {
            var selectedTag = tagPicker.SelectedItem as string;

            if (selectedTag != null && selectedTag != "Filter")
            {
                // Load recipes based on selected tag and search query
                await _viewModel.LoadRandomRecipesAsync(selectedTag, _viewModel.SearchText);
            }
            else
            {
                // Load recipes 
                await _viewModel.LoadRandomRecipesAsync("", _viewModel.SearchText);
            }
        }

        //handles when an item is tapped from the collection view
        private async void OnItemTapped(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var selectedRecipe = e.CurrentSelection[0] as Recipe;
            if (selectedRecipe != null)
            {
                await NavigateToDetailPage(selectedRecipe);
            }
            ((CollectionView)sender).SelectedItem = null;
        }

        //activates when clicked on the recipe
        private async void OnItemTappedCommand(object sender, EventArgs e)
        {
            if (sender is Frame frame && frame.BindingContext is Recipe selectedRecipe)
            {
                await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe));
            }
        }

        // Method to navigate to the recipe info page
        private async Task NavigateToDetailPage(Recipe selectedRecipe)
        {
            await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe));
        }

    }
}
