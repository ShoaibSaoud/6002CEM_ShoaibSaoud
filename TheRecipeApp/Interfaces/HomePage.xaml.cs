using Microsoft.Maui.Storage;
using System;
using TheRecipeApp.ViewModels;
using TheRecipeApp.Models;
using TheRecipeApp.Services;

namespace TheRecipeApp.Interfaces
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            UpdateUserStatus();
         
        }

        private void UpdateUserStatus()
        {
            var loggedInUser = Preferences.Get("LoggedInUsername", "");
            userStatusLabel.Text = string.IsNullOrEmpty(loggedInUser) ? "User Not Logged In" : $"Welcome: {loggedInUser}!";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateUserStatus(); 
        }
    }
}
