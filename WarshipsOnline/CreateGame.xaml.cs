using System.Collections.ObjectModel;

namespace WarshipsOnline;

public partial class CreateGame : ContentPage
{
    protected override bool OnBackButtonPressed()
    {
        HandleBackButtonPressed();
        return true;
    }
    private async void HandleBackButtonPressed()
    {
        bool anserw = await DisplayAlert(AppResources.Attention,AppResources.CreateGameExitAlert, AppResources.Yes, AppResources.No);
        if (anserw)
            GlobalManager.LoadingOverlay(LoadingOverlay, Navigation);
    }

    public ObservableCollection<string> Gamemodes { get; set; }
    private int SizeOfBoard { get; set; }
    private int CountOfShip { get; set; }

    public CreateGame()
    {
        GlobalManager.SetCulture(GlobalManager.CultureCode);
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        Gamemodes = new ObservableCollection<string>(new List<string>() { AppResources.WithPlayer, AppResources.WithBot});
        BindingContext = this;

        InitializeStartValues();
        InitializeLanguage();
    }

    private void InitializeStartValues()
    {
        SelectGamemode.SelectedIndex = 0;
        SizeOfBoard = CountOfShip = 3;

    }

    private void InitializeLanguage()
    {
        SelectGamemode.Title = AppResources.SelectGamemode;
        Header.Text = AppResources.CreateGameHeader;
        ExitButton.Text = AppResources.Exit;
        BoardSize.Text = AppResources.BoardSize + SizeOfBoard + "x" + SizeOfBoard;
        ShipCount.Text = AppResources.ShipCount + CountOfShip;
    }

    private void BoardSize_DragCompleted(object sender, EventArgs e)
    {
        Slider slider = (Slider)sender;
        SizeOfBoard = (int)slider.Value;
        slider.Value = SizeOfBoard;

        CountOfShip = (int)slider.Value;
        ShipCountSlider.Minimum = (int)slider.Value;
        ShipCountSlider.Maximum = CountOfShip*CountOfShip - 1;
        ShipCountSlider.Value = (int)slider.Value;
        InitializeLanguage();
    }

    private void ShipCount_DragCompleted(object sender, EventArgs e)
    {
        CountOfShip = (int)ShipCountSlider.Value;
        ShipCountSlider.Value = CountOfShip;
        ShipCount.Text = AppResources.ShipCount + CountOfShip;
    }

    private void ExitButton_Clicked(object sender, EventArgs e)
    {
        HandleBackButtonPressed();
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

}