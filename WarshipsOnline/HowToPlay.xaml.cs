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
        InitializeButtonColors();
    }
    private void InitializeLanguage()
    {
        Header.Text = AppResources.HowPlay;
    }

    private void InitializeButtonColors()
    {
        AnalyzePreference("FieldColor", FieldButton, Colors.Gray);
        AnalyzePreference("ShipColor", ShipButton, Colors.Yellow);
        AnalyzePreference("AttackedfieldColor", AttackedFieldButton, Colors.Red);
        AnalyzePreference("EmptyFieldColor", EmptyFieldButton, Colors.DarkRed);
        AnalyzePreference("SelectedAttackFieldColor", SelectedAttackFieldButton, Colors.Orange);
    }

    private void AnalyzePreference(string keyPreference, Button button, Color defaultColor)
    {
        if (Preferences.Default.ContainsKey(keyPreference))
        {
            string color = Preferences.Default.Get(keyPreference, defaultColor.ToHex());
            button.BackgroundColor = Color.FromArgb(color);
        }
        else
            Preferences.Default.Set(keyPreference, defaultColor.ToHex());
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