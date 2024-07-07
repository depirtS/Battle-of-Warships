namespace WarshipsOnline;

public partial class HowToPlay : ContentPage
{
	public HowToPlay()
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