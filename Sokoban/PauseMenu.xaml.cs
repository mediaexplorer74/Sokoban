using Sokoban_Game_WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sokoban_Game_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PauseMenu : Page
    {
        Controller controller;
        MainPage mainPage;
        public PauseMenu()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Parameters)e.Parameter;

            controller = parameters.getController();
            mainPage = parameters.getPage();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            controller.SaveGame();
            mainPage.SetCanMove(true);
            controller.setLoaded(true);
            CoreWindow.GetForCurrentThread().Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            controller.LoadGame();
            mainPage.ResetLevel();
            mainPage.SetCanMove(true);
            controller.setLoaded(true);
            CoreWindow.GetForCurrentThread().Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mainPage.SetCanMove(true);
            controller.setLoaded(true);
            CoreWindow.GetForCurrentThread().Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }
    }
}
