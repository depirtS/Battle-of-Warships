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
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeLanguage();
        InitializeValues();
        InitializeStartColorSettings();

    }

    private void InitializeLanguage()
    {
        Header.Text = AppResources.Settings;
        SelectedLanguage.Title = AppResources.LangSelect;
        FieldColor.Title = AppResources.FieldSelectColor;
        SelectShipColor.Title = AppResources.SelectShipColor;
        AttackedFieldColor.Title = AppResources.AttackedFieldColor;
        EmptyFieldColor.Title = AppResources.EmptyFieldColor;
        SelectedAttackFieldColor.Title= AppResources.SelectedAttackFieldColor;
    }

    private void InitializeValues()
        {
            ColorsBinding = new ObservableCollection<string>(GlobalManager.colorsList.Keys);
            Languages = new ObservableCollection<string>(GlobalManager.languageMap.Keys);
            BindingContext = this;

            var culture = CultureInfo.CurrentCulture;
            if(culture != null)
                for(int i = 0; i < Languages.Count; i++)
                    if(culture.Name == GlobalManager.languageMap[Languages[i]])
                        SelectedLanguage.SelectedItem = Languages[i];

            if(SelectedLanguage.SelectedItem == null)
                SelectedLanguage.SelectedItem = Languages[0];
    }

    private void InitializeStartColorSettings()
    {
        AnalyzePreference("FieldColor", FieldColor, FieldButton, Colors.Gray);
        AnalyzePreference("SelectColor",SelectShipColor,ShipButton, Colors.Yellow);
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

    private void SaveChanges()
    {
        GlobalManager.LoadingOverlay(LoadingOverlay, Navigation);
        //TODO: saving settings in property
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
}