namespace WarshipsOnline;

public partial class PlayWithPlayer : ContentPage
{
    private int SizeOfBoard { get; set; }
    private int CountOfShip { get; set; }
    private int HandCountOfShip { get; set; }
    private int TimeOfRound { get; set; }
    private int HandTimeOfRound { get; set; }
    private int AmountUseMarineRadar { get; set; }
    private string FirstPlayerRadar { get; set; }
    private string SecondPlayerRadar { get; set; }
    private Player FirstPlayer { get; set; }
    private Player SecondPlayer { get; set; }
    private bool SelectShip { get; set; }
    private bool PlayerTurn { get; set; }
    private bool GamePaused { get; set; }
    private IDispatcherTimer Timer { get; set; }
    private Dictionary<string, Button> ButtonDictionary { get; set; }
    private Color FieldColor { get; set; }
    private Color ShipColor { get; set; }
    private Color AttackedFieldColor { get; set; }
    private Color EmptyFieldColor { get; set; }
    private Color SelectedAttackFieldColor { get; set; }
    private List<string> FieldNames { get; set; }
    private Button HandSelectAttackButton { get; set; }

    protected override bool OnBackButtonPressed()
    {
        GamePaused = true;
        HandleBackButtonPressed();
        return true;
    }
    private async void HandleBackButtonPressed()
    {
        bool anserw = await DisplayAlert(AppResources.Attention, AppResources.CreateGameExitAlert, AppResources.Yes, AppResources.No);
        if (anserw)
            GlobalManager.LoadingOverlay(LoadingOverlay, Navigation);
        else
            GamePaused = false;
    }

    public PlayWithPlayer(int sizeOfBoard, int countOfShip, int timeOfRound, int amountUseMarineRadar)
	{
        SizeOfBoard = sizeOfBoard;
        CountOfShip = countOfShip;
        TimeOfRound = timeOfRound;
        AmountUseMarineRadar = amountUseMarineRadar;
        ButtonDictionary = new Dictionary<string, Button>();
        GlobalManager.SetCulture(GlobalManager.CultureCode);
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeValues();
        InitializeBoard();
        InitializeTimer();
    }

    private void InitializeValues()
    {
        GamePaused = false;
        FieldNames = new List<string>();
        SelectShip = true;
        PlayerTurn = true;
        HandCountOfShip = CountOfShip;
        FirstPlayer = new Player(SizeOfBoard, AmountUseMarineRadar);
        SecondPlayer = new Player(SizeOfBoard, AmountUseMarineRadar);
        MarineRadarHeadline.Text = AppResources.MarineRadar;
        Confrim.Text = AppResources.Confrim;
        SeeBoard.Text = AppResources.SeeBoard;
        MarineRadar.Text = AppResources.UseMarineRadar + $": {AmountUseMarineRadar}";
        MarineRadarInfo.Text = AppResources.CountShip + CountOfShip;
        FieldColor = Color.FromArgb(Preferences.Get("FieldColor", Colors.Gray.ToHex()));
        ShipColor = Color.FromArgb(Preferences.Get("ShipColor", Colors.Yellow.ToHex()));
        AttackedFieldColor = Color.FromArgb(Preferences.Get("AttackedFieldColor", Colors.Red.ToHex()));
        EmptyFieldColor = Color.FromArgb(Preferences.Get("EmptyFieldColor", Colors.DarkRed.ToHex()));
        SelectedAttackFieldColor = Color.FromArgb(Preferences.Get("SelectedAttackFieldColor", Colors.Orange.ToHex()));
    }

    private void InitializeBoard()
    {
        int _4x4 = 0, _5x5 = 0, _6x6 = 0, _7x7 = 0;
        switch (SizeOfBoard)
        {
            case 4:
                _4x4 = 1;
                break;
            case 5:
                _4x4 = _5x5 = 1;
                break;
            case 6:
                _4x4 = _5x5 = _6x6 = 1;
                break;
            case 7:
                _4x4 = _5x5 = _6x6 = _7x7 = 1;
                break;
        }

        GridBoard.RowDefinitions.Clear();
        GridBoard.ColumnDefinitions.Clear();

        GridBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
        GridBoard.RowDefinitions.Add(new RowDefinition());
        GridBoard.RowDefinitions.Add(new RowDefinition());
        GridBoard.RowDefinitions.Add(new RowDefinition());
        GridBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(_4x4, GridUnitType.Star) });
        GridBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(_5x5, GridUnitType.Star) });
        GridBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(_6x6, GridUnitType.Star) });
        GridBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(_7x7, GridUnitType.Star) });

        GridBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) });
        GridBoard.ColumnDefinitions.Add(new ColumnDefinition());
        GridBoard.ColumnDefinitions.Add(new ColumnDefinition());
        GridBoard.ColumnDefinitions.Add(new ColumnDefinition());
        GridBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(_4x4, GridUnitType.Star) });
        GridBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(_5x5, GridUnitType.Star) });
        GridBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(_6x6, GridUnitType.Star) });
        GridBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(_7x7, GridUnitType.Star) });

        string[] rows = { "A", "B", "C", "D", "E", "F", "G" };
        string[] cols = { "1", "2", "3", "4", "5", "6", "7" };
        for (int i = 0; i < SizeOfBoard; i++)
        {
            Label rowLabel = new Label
            {
                Text = rows[i],
                FontSize = 20,
                TextColor = Colors.Black,
                FontAttributes = FontAttributes.Bold
            };
            GridBoard.Children.Add(rowLabel);
            Grid.SetRow(rowLabel, i + 1);
            Grid.SetColumn(rowLabel, 0);

            Label colLabel = new Label
            {
                Text = cols[i],
                FontSize = 20,
                TextColor = Colors.Black,
                FontAttributes = FontAttributes.Bold
            };
            GridBoard.Children.Add(colLabel);
            Grid.SetRow(colLabel, 0);
            Grid.SetColumn(colLabel, i + 1);
        }

        for (int i = 0; i < SizeOfBoard; i++)
        {
            for (int j = 0; j < SizeOfBoard; j++)
            {
                string buttonName = $"{i}{j}";
                var button = new Button
                {
                    Text = buttonName,
                    StyleId = buttonName,
                    TextColor = Colors.Transparent,
                    BackgroundColor = FieldColor,
                    CornerRadius = 3,
                    MinimumHeightRequest = 0,
                    MinimumWidthRequest = 0,
                    BorderWidth = 2,
                    Margin = new Thickness(1),
                    Padding = new Thickness(1),
                    BorderColor = Colors.Black
                };

                ButtonDictionary.Add(button.Text, button);

                button.Clicked += Selected_Field;

                GridBoard.Children.Add(button);

                Grid.SetRow(button, i + 1);
                Grid.SetColumn(button, j + 1);
            }
        }

        var timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, e) =>
        {
            MainGrid_SizeChanged(this, e);
            timer.Stop();
        };
        timer.Start();
    }

    private void InitializeTimer()
    {
        HandTimeOfRound = TimeOfRound;
        Timer = Dispatcher.CreateTimer();
        Timer.Interval = TimeSpan.FromSeconds(1);
        Timer.Tick += (s, e) =>
        {
            if (HandTimeOfRound == 0)
            {
                HandTimeOfRound = TimeOfRound;
                if (SelectShip)
                {
                    if (PlayerTurn)
                    {
                        HandCountOfShip = CountOfShip;
                        PlayerTurn = false;
                        SelectRandomShipLocation(FirstPlayer);
                        NextPlayerAlert("2", SecondPlayer);
                        HandTimeOfRound = TimeOfRound;
                    }
                    else
                    {
                        PlayerTurn = true;
                        SelectShip = false;
                        SelectRandomShipLocation(SecondPlayer);
                        NextPlayerAlert("1", FirstPlayer);
                        HandTimeOfRound = TimeOfRound;
                        FirstPlayerRadar = "First player turn: " + SecondPlayer.MarineRadar();
                        MarineRadar.Text = AppResources.UseMarineRadar + $": {FirstPlayer.CountUseMarineRadar}";
                    }
                }
                else
                {
                    if (PlayerTurn)
                    {
                        Random random = new Random();
                        var rowID = random.Next(0, SizeOfBoard);
                        var columnID = random.Next(0, SizeOfBoard);
                        while (SecondPlayer.OwnFields[rowID, columnID] != 0 && SecondPlayer.OwnFields[rowID, columnID] != 1)
                        {
                            rowID = random.Next(0, SizeOfBoard);
                            columnID = random.Next(0, SizeOfBoard);
                        }
                        HandSelectAttackButton = ButtonDictionary[$"{rowID}{columnID}"];
                        SeePlayBoard(FirstPlayer);
                        AttackField(SecondPlayer);
                        PlayerTurn = false;
                        HandTimeOfRound = TimeOfRound;
                        UpdateTimer();
                        NextPlayerAlert("2", SecondPlayer);
                    }
                    else
                    {
                        Random random = new Random();
                        var rowID = random.Next(0, SizeOfBoard);
                        var columnID = random.Next(0, SizeOfBoard);
                        while (FirstPlayer.OwnFields[rowID, columnID] != 0 && FirstPlayer.OwnFields[rowID, columnID] != 1)
                        {
                            rowID = random.Next(0, SizeOfBoard);
                            columnID = random.Next(0, SizeOfBoard);
                        }
                        HandSelectAttackButton = ButtonDictionary[$"{rowID}{columnID}"];
                        SeePlayBoard(SecondPlayer);
                        AttackField(FirstPlayer);
                        PlayerTurn = true;
                        HandTimeOfRound = TimeOfRound;
                        UpdateTimer();
                        NextPlayerAlert("1", FirstPlayer);
                    }
                }
            }
            else
            {
                if (!GamePaused)
                {
                    UpdateTimer();
                    HandTimeOfRound--;
                }
            }
        };
        Timer.Start();
    }

    private void UpdateTimer()
    {
        int min = HandTimeOfRound / 60;
        int sec = HandTimeOfRound % 60;
        if (min < 10 && sec < 10)
            SeeTimer.Text = $"0{min}:0{sec}";
        else if (min < 10 && sec > 9)
            SeeTimer.Text = $"0{min}:{sec}";
        else if (min > 9 && sec > 9)
            SeeTimer.Text = $"{min}:{sec}";
    }
    private void Selected_Field(object sender, EventArgs e)
    {
        Button button = (Button)sender;

            if (SelectShip)
            {
                ShipStageSelectField(button);
            }
            else
            {
                AttackStageSelectField(button);
            }
      
    }
    private void ShipStageSelectField(Button button)
    {
        if (HandCountOfShip > 0 && button.BackgroundColor == FieldColor)
        {
            HandCountOfShip--;
            FieldNames.Add(button.Text);
            button.BackgroundColor = SelectedAttackFieldColor;
            MarineRadarInfo.Text = AppResources.CountShip + HandCountOfShip;
        }
        else if (button.BackgroundColor == SelectedAttackFieldColor)
        {
            HandCountOfShip++;
            FieldNames.Remove(button.Text);
            button.BackgroundColor = FieldColor;
            MarineRadarInfo.Text = AppResources.CountShip + HandCountOfShip;
        }
    }

    private void AttackStageSelectField(Button button)
    {
        if (button.BackgroundColor != AttackedFieldColor || button.BackgroundColor != SelectedAttackFieldColor || button.BackgroundColor != EmptyFieldColor)
        {
            if (SeeBoard.Text == AppResources.SeeBoard && button.BackgroundColor == FieldColor)
            {
                if (HandSelectAttackButton != null)
                {
                    HandSelectAttackButton.BackgroundColor = FieldColor;
                    HandSelectAttackButton = button;
                    HandSelectAttackButton.BackgroundColor = SelectedAttackFieldColor;
                }
                else
                {
                    HandSelectAttackButton = button;
                    HandSelectAttackButton.BackgroundColor = SelectedAttackFieldColor;
                }
            }
        }
    }
    private void Confrim_Clicked(object sender, EventArgs e)
    {
        if (PlayerTurn)
        {
            if (SelectShip)
            {
                if (HandCountOfShip == 0)
                {
                    PlayerSetSelectedShipLocation(FirstPlayer);
                }
            }
            else
            {
                if (HandSelectAttackButton != null)
                {
                    SeePlayBoard(FirstPlayer);
                    AttackField(SecondPlayer);
                    PlayerTurn = false;
                    HandTimeOfRound = TimeOfRound;
                    UpdateTimer();
                    NextPlayerAlert("2", SecondPlayer);
                }
            }
        }
        else
        {
            if (SelectShip)
            {
                if (HandCountOfShip == 0)
                {
                    PlayerSetSelectedShipLocation(SecondPlayer);
                }
            }
            else
            {
                if (HandSelectAttackButton != null)
                {
                    SeePlayBoard(SecondPlayer);
                    AttackField(FirstPlayer);
                    PlayerTurn = true;
                    HandTimeOfRound = TimeOfRound;
                    UpdateTimer();
                    NextPlayerAlert("1", FirstPlayer);
                }
            }
        }
    }
    private void PlayerSetSelectedShipLocation(Player player)
    {
        HandCountOfShip = CountOfShip;
        player.SetSelectedFileds(FieldNames);
        FieldNames = new List<string>();
        MarineRadarInfo.Text = AppResources.CountShip + CountOfShip;
        HandTimeOfRound = TimeOfRound;
        if (PlayerTurn)
        {
            SecondPlayerRadar = "Second player turn: " + FirstPlayer.MarineRadar();
            NextPlayerAlert("2", SecondPlayer);
            PlayerTurn = false;
        }
        else
        {
            FirstPlayerRadar = "First player turn: " + SecondPlayer.MarineRadar();
            NextPlayerAlert("1", FirstPlayer);
            MarineRadarInfo.Text = FirstPlayerRadar;
            PlayerTurn = true;
            SelectShip = false;
        }
    }

    private void SeeBoard_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (PlayerTurn && !SelectShip)
        {
            if (button.Text == AppResources.SeeBoard)
            {
                SeeOwnBoard(FirstPlayer);
                button.Text = AppResources.SeePlayBoard;
            }
            else
            {
                SeePlayBoard(SecondPlayer);
                button.Text = AppResources.SeeBoard;
            }
        }
        else if(!SelectShip)
        {
            if (button.Text == AppResources.SeeBoard)
            {
                SeeOwnBoard(SecondPlayer);
                button.Text = AppResources.SeePlayBoard;
            }
            else
            {
                SeePlayBoard(FirstPlayer);
                button.Text = AppResources.SeeBoard;
            }
        }
    }

    private void MarineRadar_Clicked(object sender, EventArgs e)
    {
        if (PlayerTurn && !SelectShip)
        {
            if (FirstPlayer.CountUseMarineRadar != 0)
            {
                FirstPlayer.CountUseMarineRadar--;
                MarineRadar.Text = AppResources.UseMarineRadar + $": {FirstPlayer.CountUseMarineRadar}";
                FirstPlayerRadar = "Player turn: " + SecondPlayer.MarineRadar();
                MarineRadarInfo.Text = FirstPlayerRadar;
            }
        }
        else if (!SelectShip)
        {
            if (SecondPlayer.CountUseMarineRadar != 0)
            {
                SecondPlayer.CountUseMarineRadar--;
                MarineRadar.Text = AppResources.UseMarineRadar + $": {SecondPlayer.CountUseMarineRadar}";
                SecondPlayerRadar = "Player turn: " + FirstPlayer.MarineRadar();
                MarineRadarInfo.Text = SecondPlayerRadar;
            }
        }
    }

    private void AttackField(Player player)
    {
        var rowIDStr = HandSelectAttackButton.Text.Substring(0, 1);
        var columnIDStr = HandSelectAttackButton.Text.Substring(1, 1);
        if (int.TryParse(rowIDStr, out var rowID) && int.TryParse(columnIDStr, out var columnID))
        {
            if (player.OwnFields[rowID, columnID] == 1)
                player.OwnFields[rowID, columnID] = 3;
            else if (player.OwnFields[rowID, columnID] == 0)
                player.OwnFields[rowID, columnID] = 2;
        }
        HandSelectAttackButton = null;
    }

    private void SeePlayBoard(Player player)
    {
        for (int i = 0; i < SizeOfBoard; i++)
            for (int j = 0; j < SizeOfBoard; j++)
            {
                if (player.OwnFields[i, j] == 0 || player.OwnFields[i, j] == 1)
                    ButtonDictionary[$"{i}{j}"].BackgroundColor = FieldColor;
                else if (player.OwnFields[i, j] == 2)
                    ButtonDictionary[$"{i}{j}"].BackgroundColor = EmptyFieldColor;
                else if (player.OwnFields[i, j] == 3)
                    ButtonDictionary[$"{i}{j}"].BackgroundColor = AttackedFieldColor;
            }
    }

    private void SeeOwnBoard(Player player)
    {
        for (int i = 0; i < SizeOfBoard; i++)
            for (int j = 0; j < SizeOfBoard; j++)
            {
                if (player.OwnFields[i, j] == 0)
                    ButtonDictionary[$"{i}{j}"].BackgroundColor = FieldColor;
                else if (player.OwnFields[i, j] == 1)
                    ButtonDictionary[$"{i}{j}"].BackgroundColor = ShipColor;
                else if (player.OwnFields[i, j] == 2)
                    ButtonDictionary[$"{i}{j}"].BackgroundColor = EmptyFieldColor;
                else if (player.OwnFields[i, j] == 3)
                    ButtonDictionary[$"{i}{j}"].BackgroundColor = AttackedFieldColor;
            }
    }

    private void SelectRandomShipLocation(Player player)
    {
        Random random = new Random();
        HashSet<string> list = new HashSet<string>();
        while (list.Count != CountOfShip)
        {
            var fieldName = $"{random.Next(0, SizeOfBoard)}{random.Next(0, SizeOfBoard)}";
            list.Add(fieldName);
        }
        player.SetSelectedFileds(list.ToList<string>());
    }

    private async void NextPlayerAlert(string playerNumber, Player player)
    {
        var gameNoEnd = true;
        GamePaused = true;
        if (!SelectShip)
        {
            if (TestEndGame(FirstPlayer))
            {
                gameNoEnd = false;
                await DisplayAlert(AppResources.Attention, AppResources.WinPlayerTwo, "OK");
                GlobalManager.LoadingOverlay(LoadingOverlay, Navigation);
            }
            if (TestEndGame(SecondPlayer))
            {
                gameNoEnd = false;
                await DisplayAlert(AppResources.Attention, AppResources.WinPlayerOne, "OK");
                GlobalManager.LoadingOverlay(LoadingOverlay, Navigation);
            }
        }

        if (gameNoEnd)
        {
            if (SelectShip)
            {
                foreach (var button in ButtonDictionary.Values)
                {
                    button.BackgroundColor = FieldColor;
                }
                await DisplayAlert(AppResources.Attention, $"{AppResources.Turn}{playerNumber}", "OK");
                GamePaused = false;
            }
            else
            {

                SeeBoard.Text = AppResources.SeeBoard;
                await DisplayAlert(AppResources.Attention, $"{AppResources.Turn}{playerNumber}", "OK");
                GamePaused = false;
                if (PlayerTurn)
                {
                    MarineRadarInfo.Text = FirstPlayerRadar;
                    MarineRadar.Text = AppResources.UseMarineRadar + $": {FirstPlayer.CountUseMarineRadar}";
                }
                else
                {
                    MarineRadarInfo.Text = SecondPlayerRadar;
                    MarineRadar.Text = AppResources.UseMarineRadar + $": {SecondPlayer.CountUseMarineRadar}";
                }
            }
        }
    }

    private bool TestEndGame(Player player)
    {
        for (int i = 0; i < SizeOfBoard; i++)
            for (int j = 0; j < SizeOfBoard; j++)
                if (player.OwnFields[i, j] == 1)
                    return false;
        return true;
    }

    private void MainGrid_SizeChanged(object sender, EventArgs e)
    {
        if (BoardAndControls.Width > BoardAndControls.Height)
        {
            BoardAndControls.Orientation = StackOrientation.Horizontal;
            GridBoard.HeightRequest = BoardAndControls.Width / 2.0 - 50;
            GridBoard.WidthRequest = BoardAndControls.Width / 2 - 50;
            GameControl.WidthRequest = BoardAndControls.Width / 2;
            if (SizeOfBoard == 7)
            {
                GridBoard.RowDefinitions[0] = new RowDefinition { Height = new GridLength(0.6, GridUnitType.Star) };
                GridBoard.ColumnDefinitions[0] = new ColumnDefinition { Width = new GridLength(0.6, GridUnitType.Star) };
            }
            else
            {
                GridBoard.RowDefinitions[0] = new RowDefinition { Height = new GridLength(0.4, GridUnitType.Star) };
                GridBoard.ColumnDefinitions[0] = new ColumnDefinition { Width = new GridLength(0.4, GridUnitType.Star) };
            }

        }
        else
        {
            BoardAndControls.Orientation = StackOrientation.Vertical;
            GameControl.WidthRequest = BoardAndControls.Width - 50;
            if (BoardAndControls.Height - BoardAndControls.Width > 600)
                GridBoard.WidthRequest = BoardAndControls.Height / 2.2 - 50;
            else
                GridBoard.WidthRequest = BoardAndControls.Height / 1.7 - 50;
            GridBoard.HeightRequest = BoardAndControls.Height / 2 - 50;
            if (SizeOfBoard == 7)
            {
                GridBoard.RowDefinitions[0] = new RowDefinition { Height = new GridLength(0.7, GridUnitType.Star) };
                GridBoard.ColumnDefinitions[0] = new ColumnDefinition { Width = new GridLength(0.7, GridUnitType.Star) };
            }
            else
            {
                GridBoard.RowDefinitions[0] = new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) };
                GridBoard.ColumnDefinitions[0] = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
            }

        }
    }
}