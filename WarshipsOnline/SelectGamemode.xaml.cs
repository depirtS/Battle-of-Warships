namespace WarshipsOnline;

public partial class SelectGamemode : ContentPage
{
    protected override bool OnBackButtonPressed()
    {
        GlobalManager.LoadingOverlay(LoadingOverlay, Navigation);
        return true;
    }
	public SelectGamemode()
	{
        GlobalManager.SetCulture(GlobalManager.CultureCode);
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeLanguage();
    }

    private void InitializeLanguage()
    {
        NameWindow.Text = AppResources.SelectGamemode;
        JoinGame.Text = AppResources.JoinGame;
        CreateGame.Text = AppResources.CreateGame;
        ExitButton.Text = AppResources.ExitSelectGamemode;
    }

    private void MainGrid_SizeChanged(object sender, EventArgs e)
    {
        if (this.Width <= this.Height)
        {
            MainGrid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
            MainGrid.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Star);
        }
        else
        {
            MainGrid.ColumnDefinitions[0].Width = new GridLength(0.4, GridUnitType.Star);
            MainGrid.ColumnDefinitions[2].Width = new GridLength(0.4, GridUnitType.Star);
        }
    }
    private async void JoinGame_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert(AppResources.Attention, AppResources.BetaVersion,"OK");
    }

    private void CreateGame_Clicked(object sender, EventArgs e)
    {
        GlobalManager.LoadingOverlay(LoadingOverlay, Navigation, new CreateGame());
    }

    private void ExitButton_Clicked(object sender, EventArgs e)
    {
        GlobalManager.LoadingOverlay(LoadingOverlay, Navigation);
    }
}