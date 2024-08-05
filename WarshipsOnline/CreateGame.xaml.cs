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
    private int TimeOfRound { get; set; }
    private int AmountUseMarineRadar { get; set; }

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
        AmountUseMarineRadar = 0;
        SelectGamemode.SelectedIndex = 0;
        SizeOfBoard = CountOfShip = 3;
        TimeOfRound = 120;

    }

    private void InitializeLanguage()
    {
        Header.Text = AppResources.CreateGameHeader;
        ExitButton.Text = AppResources.Exit;
        SelectGamemode.Title = AppResources.SelectGamemode;
        BoardSize.Text = AppResources.BoardSize + SizeOfBoard + "x" + SizeOfBoard;
        ShipCount.Text = AppResources.ShipCount + CountOfShip;
        RoundTime.Text = AppResources.RoundTime + TimeOfRound +"s";
        HowManyUseMarineRadar.Text = AppResources.HowManyUseMarineRadar + AmountUseMarineRadar;
        CreateGameBtn.Text = AppResources.CreateGame;
    }

    private void BoardSize_DragCompleted(object sender, EventArgs e)
    {
        Slider slider = (Slider)sender;
        SizeOfBoard = (int)slider.Value;

        CountOfShip = (int)slider.Value;
        ShipCountSlider.Minimum = (int)slider.Value;
        ShipCountSlider.Maximum = CountOfShip*CountOfShip - 1;
        ShipCountSlider.Value = (int)slider.Value;
        InitializeLanguage();
    }

    private void ShipCount_DragCompleted(object sender, EventArgs e)
    {
        CountOfShip = (int)ShipCountSlider.Value;
        ShipCount.Text = AppResources.ShipCount + CountOfShip;
    }

    private void RoundTime_DragCompleted(object sender, EventArgs e)
    {
        Slider slider = (Slider)sender;
        TimeOfRound = (int)slider.Value;
        RoundTime.Text = AppResources.RoundTime + TimeOfRound + "s";
    }

    private void HowManyUseMarineRadar_DragCompleted(object sender, EventArgs e)
    {
        Slider slider = (Slider)sender;
        AmountUseMarineRadar = (int)slider.Value;
        HowManyUseMarineRadar.Text = AppResources.HowManyUseMarineRadar + AmountUseMarineRadar;
    }

    private void CreateGameBtn_Clicked(object sender, EventArgs e)
    {
        int gamemodeID = SelectGamemode.SelectedIndex;
        switch(gamemodeID)
        {
            case 0:
                GlobalManager.LoadingOverlay(LoadingOverlay, Navigation, new PlayWithPlayer(SizeOfBoard,CountOfShip,TimeOfRound, AmountUseMarineRadar));
                break;
            case 1:
                GlobalManager.LoadingOverlay(LoadingOverlay, Navigation, new PlayWithBot(SizeOfBoard, CountOfShip, TimeOfRound, AmountUseMarineRadar));
                break;
            default:
                DisplayAlert("Error", "Select gamemode not found", "OK"); //TODO: You want play with bot? if yes = start round with bot else close alert
                break;
        }
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

            int fontSize = 18;
            SelectGamemode.FontSize = fontSize;
            BoardSize.FontSize = fontSize;
            ShipCount.FontSize = fontSize;
            RoundTime.FontSize = fontSize;
        }
        else
        {
            MainGrid.ColumnDefinitions[0].Width = new GridLength(0.4, GridUnitType.Star);
            MainGrid.ColumnDefinitions[2].Width = new GridLength(0.4, GridUnitType.Star);

            int fontSize = 16;
            SelectGamemode.FontSize = fontSize;
            BoardSize.FontSize = fontSize;
            ShipCount.FontSize = fontSize;
            RoundTime.FontSize = fontSize;
        }
    }
}