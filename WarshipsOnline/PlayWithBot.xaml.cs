namespace WarshipsOnline;

public partial class PlayWithBot : ContentPage
{
    private int SizeOfBoard { get; set; }
    private int CountOfShip { get; set; }
    private int TimeOfRound { get; set; }
    public PlayWithBot(int sizeOfBoard, int countOfShip, int timeOfRound)
    {
        SizeOfBoard = sizeOfBoard;
        CountOfShip = countOfShip;
        TimeOfRound = timeOfRound;
        GlobalManager.SetCulture(GlobalManager.CultureCode);
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }
    private void MainGrid_SizeChanged(object sender, EventArgs e)
    {
        if (this.Width <= this.Height)
        {
            MainGrid.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);
            MainGrid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
        }
        else
        {
            MainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
            MainGrid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
        }
    }

}