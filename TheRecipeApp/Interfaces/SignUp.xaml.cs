namespace TheRecipeApp.Interfaces;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
	}


    private async void ToLoginPage(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("///Login");
    }
}