namespace WarshipsOnline;

public partial class CreateGame : ContentPage
{
	public CreateGame()
    {
        GlobalManager.SetCulture(GlobalManager.CultureCode);
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeLanguage();
    }
    private void InitializeLanguage()
    {

    }
}