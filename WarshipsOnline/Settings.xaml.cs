using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Maui.Graphics;

namespace WarshipsOnline;

public partial class Settings : ContentPage
{
    public ObservableCollection<string> Languages { get; set; }
    public ObservableCollection<string> ColorsBinding { get; set; }
    protected override bool OnBackButtonPressed()
    {
        HandleBackButtonPressed();
        return true;
    }
    private async void HandleBackButtonPressed()
    {
        bool answer = await DisplayAlert(AppResources.ExitAlert, AppResources.ExitSettingsAlert, AppResources.SettingsAccept, AppResources.SettingsReject);
        if (answer)
            SaveChanges();
        else
            GlobalManager.LoadingOverlay(LoadingOverlay, Navigation);
    }

    public Settings()
    {
        GlobalManager.SetCulture(GlobalManager.CultureCode);
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        ColorsBinding = new ObservableCollection<string>(GlobalManager.colorsList.Keys);
        Languages = new ObservableCollection<string>(GlobalManager.languageMap.Keys);
        BindingContext = this;
        
        InitializeValues();
    }

    private void InitializeValues()
    {
        var culture = CultureInfo.CurrentCulture;
        if (culture != null)
            for (int i = 0; i < Languages.Count; i++)
                if (culture.Name == GlobalManager.languageMap[Languages[i]])
                    SelectedLanguage.SelectedItem = Languages[i];

        if (SelectedLanguage.SelectedItem == null)
            SelectedLanguage.SelectedItem = Languages[0];

        InitializeLanguage();
        InitializeStartColorSettings();
        MainGrid_SizeChanged(this, new EventArgs());
    }
    private void InitializeLanguage()
    {
        Header.Text = AppResources.Settings;
        SelectedLanguage.Title = AppResources.LangSelect;
        FieldColor.Title = AppResources.FieldSelectColor;
        ShipColor.Title = AppResources.SelectShipColor;
        AttackedFieldColor.Title = AppResources.AttackedFieldColor;
        EmptyFieldColor.Title = AppResources.EmptyFieldColor;
        SelectedAttackFieldColor.Title= AppResources.SelectedAttackFieldColor;
        AcceptSettingsBtn.Text = AppResources.SettingsAccept;
        RejectSettingsBtn.Text = AppResources.SettingsReject;
    }

    private void InitializeStartColorSettings()
    {
        AnalyzePreference("FieldColor", FieldColor, FieldButton, Colors.Gray);
        AnalyzePreference("ShipColor",ShipColor,ShipButton, Colors.Yellow);
        AnalyzePreference("AttackedfieldColor",AttackedFieldColor,AttackedFieldButton, Colors.Red);
        AnalyzePreference("EmptyFieldColor", EmptyFieldColor,EmptyFieldButton,Colors.DarkRed);
        AnalyzePreference("SelectedAttackFieldColor", SelectedAttackFieldColor,SelectedAttackFieldButton, Colors.Orange);
    }

    private void AnalyzePreference(string keyPreference, Picker picker, Button button, Color defaultColor)
    {
        if (Preferences.Default.ContainsKey(keyPreference))
        {
            string color = Preferences.Default.Get(keyPreference, defaultColor.ToHex());
            button.BackgroundColor = Color.FromArgb(color);
            string colorName = "";
            for (int i = 0; i < ColorsBinding.Count; i++)
            {
                if (color == GlobalManager.colorsList[ColorsBinding[i]].ToHex())
                    colorName = ColorsBinding[i];
            }
            picker.SelectedItem = colorName;
        }
        else
        {
            Preferences.Default.Set(keyPreference, defaultColor.ToHex());
            string colorName = "";
            for (int i = 0; i < ColorsBinding.Count; i++)
            {
                if (defaultColor == GlobalManager.colorsList[ColorsBinding[i]])
                    colorName = ColorsBinding[i];
            }
            picker.SelectedItem = colorName;
        }
    }

    private async void SaveChanges()
    {
        HashSet<string> colors = new HashSet<string>();
        colors.Add((string)FieldColor.SelectedItem);
        colors.Add((string)ShipColor.SelectedItem);
        colors.Add((string)AttackedFieldColor.SelectedItem);
        colors.Add((string)EmptyFieldColor.SelectedItem);
        colors.Add((string)SelectedAttackFieldColor.SelectedItem);
        if(colors.Count == 5)
        {
            Preferences.Set("FieldColor", FieldButton.BackgroundColor.ToHex());
            Preferences.Set("ShipColor", ShipButton.BackgroundColor.ToHex());
            Preferences.Set("AttackedfieldColor", AttackedFieldButton.BackgroundColor.ToHex());
            Preferences.Set("EmptyFieldColor", EmptyFieldButton.BackgroundColor.ToHex());
            Preferences.Set("SelectedAttackFieldColor", SelectedAttackFieldButton.BackgroundColor.ToHex());

            Preferences.Set("CultureCode", GlobalManager.languageMap[(string)SelectedLanguage.SelectedItem]);
            GlobalManager.CultureCode = Preferences.Default.Get("CultureCode", GlobalManager.languageMap[AppResources.English]);
            GlobalManager.SetCulture(GlobalManager.CultureCode);

            if(Application.Current != null)
                Application.Current.MainPage = new NavigationPage(new MainPage());
        }
        else
        {
            bool answer = await DisplayAlert(AppResources.ExitAlert, AppResources.ExitIncorrectSettingsAlert, AppResources.SettingsReject, AppResources.ChangeInncorectSettings);
            if (answer)
                GlobalManager.LoadingOverlay(LoadingOverlay, Navigation);
        }
    }

    private void FieldColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        Color color = GlobalManager.colorsList[(string)picker.SelectedItem];
        FieldButton.BackgroundColor = color;
    }

    private void SelectColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        Color color = GlobalManager.colorsList[(string)picker.SelectedItem];
        ShipButton.BackgroundColor = color;
    }

    private void AttackedFieldColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        Color color = GlobalManager.colorsList[(string)picker.SelectedItem];
        AttackedFieldButton.BackgroundColor = color;
    }

    private void EmptyFieldColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        Color color = GlobalManager.colorsList[(string)picker.SelectedItem];
        EmptyFieldButton.BackgroundColor = color;
    }

    private void SelectedAttackFieldColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        Color color = GlobalManager.colorsList[(string)picker.SelectedItem];
        SelectedAttackFieldButton.BackgroundColor = color;
    }

    private void AcceptSettingsBtn_Clicked(object sender, EventArgs e)
    {
        SaveChanges();
    }

    private void RejectSettingsBtn_Clicked(object sender, EventArgs e)
    {
        GlobalManager.LoadingOverlay(LoadingOverlay, Navigation);
    }

    private void MainGrid_SizeChanged(object sender, EventArgs e)
    {
        if(this.Width <= this.Height)
        {
            MainGrid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star); 
            MainGrid.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Star);

            SaveButtons.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
            SaveButtons.ColumnDefinitions[1].Width = new GridLength (0, GridUnitType.Star);
            SaveButtons.SetRow(RejectSettingsBtn, 1);
            SaveButtons.SetColumn(RejectSettingsBtn, 0);

            int fontSize = 18;
            SelectedLanguage.FontSize = fontSize;
            FieldColor.FontSize = fontSize;
            ShipColor.FontSize = fontSize;
            AttackedFieldColor.FontSize = fontSize;
            EmptyFieldColor.FontSize = fontSize;
            SelectedAttackFieldColor.FontSize = fontSize;
        }
        else
        {
            MainGrid.ColumnDefinitions[0].Width = new GridLength(0.4, GridUnitType.Star);
            MainGrid.ColumnDefinitions[2].Width = new GridLength(0.4, GridUnitType.Star);

            SaveButtons.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            SaveButtons.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
            SaveButtons.SetRow(RejectSettingsBtn, 0);
            SaveButtons.SetColumn(RejectSettingsBtn, 1);

            int fontSize = 16;
            SelectedLanguage.FontSize = fontSize;
            FieldColor.FontSize = fontSize;
            ShipColor.FontSize = fontSize;
            AttackedFieldColor.FontSize = fontSize;
            EmptyFieldColor.FontSize = fontSize;
            SelectedAttackFieldColor.FontSize = fontSize;
        }
    }
}