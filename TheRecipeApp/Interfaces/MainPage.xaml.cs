using Microsoft.Maui.Controls;

namespace TheRecipeApp.Interfaces
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GetstartedButton_Clicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync("///Intro1");
        }


        private async void OnSignUpLabelTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///SignUp");
        }


    }
}
