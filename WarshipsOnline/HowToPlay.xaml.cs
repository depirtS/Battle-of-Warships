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
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeLanguage();
        InitializeButtonColors();
    }
    private void InitializeLanguage()
    {
        Header.Text = AppResources.HowPlay;
        GrayField.Text = AppResources.BasicField;
        YellowField.Text = AppResources.ShipField;
        RedField.Text = AppResources.AttackField;
        DarkRedField.Text = AppResources.MissedAttackField;
        OrangeField.Text = AppResources.SetAttackField;
        RadarDescription.Text = AppResources.MarineRadarDescription;

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
}