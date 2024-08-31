namespace WarshipsOnline;

public partial class HowToPlay : ContentPage
{
    protected override bool OnBackButtonPressed()
    {
        GlobalManager.LoadingOverlay(LoadingOverlay, Navigation);
        return true;
    }
    public HowToPlay()
	{
        GlobalManager.SetCulture(GlobalManager.CultureCode);
        InitializeComponent();
        MainGrid_SizeChanged(this, new EventArgs());
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeLanguage();
    }
    private void InitializeLanguage()
    {
        Header.Text = AppResources.HowPlay;
    }

    private void MainGrid_SizeChanged(object sender, EventArgs e)
    {
        int width = (int)MainGrid.Width;
        int height = (int)MainGrid.Height;
        if (width < height)
        {
            GrayField.FontSize = YellowField.FontSize = RedField.FontSize = DarkRedField.FontSize = OrangeField.FontSize = TransparentField.FontSize = TransparentButton.FontSize = width / 30;
            Header.FontSize = width / 20;
        }
        else
        {
            GrayField.FontSize = YellowField.FontSize = RedField.FontSize = DarkRedField.FontSize = OrangeField.FontSize = TransparentField.FontSize = TransparentButton.FontSize = width / 30;
            Header.FontSize = width / 20;
        }
    }
}