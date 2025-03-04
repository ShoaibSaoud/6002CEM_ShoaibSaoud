namespace TheRecipeApp.Interfaces;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}


    private async void ToSignUpPage(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("///SignUp");
    }
}