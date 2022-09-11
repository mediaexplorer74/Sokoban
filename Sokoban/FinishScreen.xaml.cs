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
    public sealed partial class FinishScreen : Page
    {
        Controller controller;
        MainPage mainPage;
        TextBlock moveCount;

        public FinishScreen()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Parameters)e.Parameter;

            controller = parameters.getController();
            mainPage = parameters.getPage();

            moveCount = (TextBlock)FindName("MoveCount");
            moveCount.Text = controller.GetMoveCount().ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            controller.Restart();
            mainPage.ResetLevel();
            mainPage.SetCanMove(true);
            CoreWindow.GetForCurrentThread().Close();
        }
    }
}
