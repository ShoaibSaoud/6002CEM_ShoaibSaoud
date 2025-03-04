using Microsoft.Maui.Controls;
namespace TheRecipeApp.Interfaces;

public partial class Intro1 : ContentPage
{
    public Intro1()
    {
        InitializeComponent();
    }

    private async void TapGestureRecognizers_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(Intro2)}");
    }
}
