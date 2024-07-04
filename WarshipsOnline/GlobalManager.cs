using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipsOnline
{
    public static class GlobalManager
    {

        public static readonly Dictionary<string, string> languageMap = new Dictionary<string, string>()
        {
            {AppResources.Polish, "pl"},
            {AppResources.English, "en-GB"},
        };

        public static readonly Dictionary<string, Color> colorsList = new Dictionary<string, Color>()
            {
                {AppResources.Gray, Colors.Gray},
                {AppResources.Dark_gray, Colors.DarkGray},
                {AppResources.Orange, Colors.Orange},
                {AppResources.Dark_orange, Colors.DarkOrange},
                {AppResources.Red, Colors.Red},
                {AppResources.Dark_red, Colors.DarkRed},
                {AppResources.Yellow, Colors.Yellow},
                {AppResources.Blue, Colors.Blue},
                {AppResources.Dark_blue, Colors.DarkBlue},
                {AppResources.Sky_blue, Colors.SkyBlue},
                {AppResources.Pink, Colors.Pink},
                {AppResources.Purple,Colors.Purple},
                {AppResources.Violet,Colors.Violet},
                {AppResources.Dark_violet, Colors.DarkViolet},
                {AppResources.Green, Colors.Green},
                {AppResources.Dark_green,Colors.DarkGreen},
                {AppResources.Sea_green, Colors.SeaGreen},
                {AppResources.Cyan, Colors.Cyan},
                {AppResources.Dark_cyan, Colors.DarkCyan},
                {AppResources.Brown, Colors.Brown}
            };
        public async static void LoadingOverlay(Grid LoadingOverlay, INavigation Navigation)
        {
            LoadingOverlay.IsVisible = true;
            await Task.Delay(500);
            await Navigation.PopAsync();
            await Task.Delay(1500);
            LoadingOverlay.IsVisible = false;
        }
        public async static void LoadingOverlay(Grid LoadingOverlay, INavigation Navigation, Page PageToPush)
        {
            LoadingOverlay.IsVisible = true;
            await Task.Delay(500);
            await Navigation.PushAsync(PageToPush);
            await Task.Delay(1500);
            LoadingOverlay.IsVisible = false;
        }
    }
}
