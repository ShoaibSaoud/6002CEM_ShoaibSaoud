namespace TheRecipeApp.Interfaces;

public partial class Intro2 : ContentPage
{
    public Intro2()
    {
        InitializeComponent();
    }
    private async void HomePageLabelTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///HomePage");
    }



    private async void SignUpButtton(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///SignUp");
    }
}
