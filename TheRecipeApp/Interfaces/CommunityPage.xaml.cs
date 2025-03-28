using TheRecipeApp.ViewModels;
using TheRecipeApp.Models;

namespace TheRecipeApp.Interfaces;

public partial class CommunityPage : ContentPage
{

	private readonly CommunityViewModel _communityViewModel;
	public CommunityPage()
	{
		InitializeComponent();
		_communityViewModel = new CommunityViewModel();
        this.BindingContext = _communityViewModel;
        _communityViewModel.LoadRandomRecipesAsync();
    }
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
    private async void OnItemTappedCommand(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Recipe selectedRecipe)
        {
            await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe));
        }
    }
    private async Task NavigateToDetailPage(Recipe selectedRecipe)
    {
        await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe));
    }
}