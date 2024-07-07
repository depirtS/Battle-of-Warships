namespace WarshipsOnline
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            GlobalManager.SetCulture(GlobalManager.CultureCode);
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeLanguage();
        }

        private void InitializeLanguage()
        {
            NameApplication.Text = AppResources.NameApplication;
            PlayButton.Text = AppResources.FirstMenuButton;
            SettingsButton.Text = AppResources.Settings;
            HowPlayButton.Text = AppResources.HowPlay;
        }

        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            GlobalManager.LoadingOverlay(LoadingOverlay, Navigation, new SelectGamemode());
        }

        private void SettingsButton_Clicked(object sender, EventArgs e)
        {
            GlobalManager.LoadingOverlay(LoadingOverlay, Navigation, new Settings());
        }

        private void HowPlayButton_Clicked(object sender, EventArgs e)
        {
            GlobalManager.LoadingOverlay(LoadingOverlay, Navigation, new HowToPlay());
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

}
