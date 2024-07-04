namespace WarshipsOnline
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
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
            //TODO: select list games or create game
        }

        private void SettingsButton_Clicked(object sender, EventArgs e)
        {
            GlobalManager.LoadingOverlay(LoadingOverlay, Navigation, new Settings());
        }

        private void HowPlayButton_Clicked(object sender, EventArgs e)
        {
            //TODO: Create "how play?" page
        }
    }

}
