using Sokoban_Game_WPF;
using SokobanGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Sokoban_Game_UWP namespace
namespace Sokoban_Game_UWP
{
   
    // MainPage class 
    public sealed partial class MainPage : Page
    {

        const int C1 = 30;

        static List<SquareWrapper> listOfSquaresVisual = new List<SquareWrapper>();
        Image thePlayer;
        Controller controller = new Controller();
        TextBlock moveCount;
        bool canMove = true;
        Grid childGrid;

        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            this.Loaded += delegate { this.Focus(FocusState.Programmatic); };
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!controller.getLoaded())
            {
                controller = new Controller();
                controller.Start();
                controller.CreateLevel();
            }
            
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            
            childGrid = new Grid();
            moveCount = (TextBlock)FindName("MoveCount");
            childGrid.HorizontalAlignment = HorizontalAlignment.Center;
            childGrid.VerticalAlignment = VerticalAlignment.Center;
            CreateLevelVisual();
            MainGrid.Children.Add(childGrid);

        }


        private async void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs e)
        {

            if (canMove)
            {

                switch (e.VirtualKey)
                {
                    case Windows.System.VirtualKey.Left:
                        controller.SaveLastMove();
                        controller.MovePlayer(Direction.Left);
                        break;
                    case Windows.System.VirtualKey.Up:
                        controller.SaveLastMove();
                        controller.MovePlayer(Direction.Up);
                        break;
                    case Windows.System.VirtualKey.Right:
                        controller.SaveLastMove();
                        controller.MovePlayer(Direction.Right);
                        break;
                    case Windows.System.VirtualKey.Down:
                        controller.SaveLastMove();
                        controller.MovePlayer(Direction.Down);
                        break;
                    case Windows.System.VirtualKey.Space:
                        controller.Restart();
                        break;
                    case Windows.System.VirtualKey.Escape:
                        ShowPauseMenu();
                        break;
                    case Windows.System.VirtualKey.Back:
                        controller.LoadLastMove();
                        break;
                }

                ResetLevel();


                if (controller.CheckWin() == true)
                {
                    canMove = false;
                    Parameters par = new Parameters(controller, this);
                    CoreApplicationView newView = CoreApplication.CreateNewView();
                    int newViewId = 0;

                    await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        Frame frame = new Frame();
                        frame.Navigate(typeof(FinishScreen), par);
                        Window.Current.Content = frame;
                        Window.Current.Activate();

                        newViewId = ApplicationView.GetForCurrentView().Id;
                    });
                    bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
                }
            }
        }

        public void CreateLevelVisual()
        {
            Game game = controller.GetGame();
            foreach (Square square in game.ReturnSquares())
            {
                switch (square.GetType().Name)
                {
                    case "Wall":
                        CreateWall(square.GetRow(), square.GetColumn());
                        break;
                    case "Block":
                        CreateBlock(square.GetRow(), square.GetColumn());
                        break;
                    case "Goal":
                        CreateGoal(square.GetRow(), square.GetColumn());
                        break;
                    case "Empty":
                        CreateEmpty(square.GetRow(), square.GetColumn());
                        break;
                }
            }

            CreatePlayer(game.GetPlayerRow(), game.GetPlayerColumn());

            moveCount.Text = controller.GetMoveCount().ToString();
        }

        public async void ResetLevel()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (SquareWrapper square in listOfSquaresVisual)
                {
                    Image picture = square.GetImage();
                    picture.Visibility = Visibility.Collapsed;
                }
                listOfSquaresVisual.Clear();
                thePlayer.Visibility = Visibility.Collapsed;

                CreateLevelVisual();
            });

        }

        public async void ShowPauseMenu()
        {
            //TODO
            /*
            canMove = false;
            Parameters par = new Parameters(controller, this);
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;

            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(PauseMenu), par);
                Window.Current.Content = frame;
                Window.Current.Activate();

                newViewId = ApplicationView.GetForCurrentView().Id;
            });

            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
            */
        }

        public void CreatePlayer(int row, int column)
        {
            TranslateTransform pos = new TranslateTransform();
            pos.X = ((childGrid.ActualWidth / 2) - (C1 * 5)) + (C1 * column);
            pos.Y = ((childGrid.ActualWidth / 2) - (C1 * 5)) + (C1 * row);
            Image player = new Image();
            player.Name = "Player";
            player.Width = C1;
            player.Height = C1;
            player.RenderTransform = pos;
            player.Source = new BitmapImage(new Uri("ms-appx:///Assets/Man.png"));
            Canvas.SetZIndex(player, 5);


            listOfSquaresVisual.Add(new SquareWrapper(row, column, player));
            childGrid.Children.Add(player);
            thePlayer = player;
        }

        public void CreateWall(int row, int column)
        {
            TranslateTransform pos = new TranslateTransform();
            pos.X = ((childGrid.ActualWidth / 2) - (C1 * 5)) + (C1 * column);
            pos.Y = ((childGrid.ActualWidth / 2) - (C1 * 5)) + (C1 * row);

            Image wall = new Image()
            {
                Name = "Wall",
                Width = C1,
                Height = C1,
                RenderTransform = pos,
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Wall.jpg", UriKind.Absolute)),
            };

            listOfSquaresVisual.Add(new SquareWrapper(row, column, wall));
            childGrid.Children.Add(wall);
        }

        public void CreateGoal(int row, int column)
        {
            TranslateTransform pos = new TranslateTransform();
            pos.X = ((childGrid.ActualWidth / 2) - (C1 * 5)) + (C1 * column);
            pos.Y = ((childGrid.ActualWidth / 2) - (C1 * 5)) + (C1 * row);

            Image goal = new Image()
            {
                Name = "Goal",
                Width = C1,
                Height = C1,
                RenderTransform = pos,
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Goal.jpg", UriKind.Absolute)),
            };

            listOfSquaresVisual.Add(new SquareWrapper(row, column, goal));
            childGrid.Children.Add(goal);
        }

        public void CreateBlock(int row, int column)
        {
            TranslateTransform pos = new TranslateTransform();
            pos.X = ((childGrid.ActualWidth / 2) - (C1 * 5)) + (C1 * column);
            pos.Y = ((childGrid.ActualWidth / 2) - (C1 * 5)) + (C1 * row);

            Image block = new Image()
            {
                Name = "Block",
                Width = C1,
                Height = C1,
                RenderTransform = pos,
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Block.jpg", 
                UriKind.Absolute)),
            };

            Canvas.SetZIndex(block, 5);

            listOfSquaresVisual.Add(new SquareWrapper(row, column, block));
            childGrid.Children.Add(block);
        }

        public void CreateEmpty(int row, int column)
        {
            TranslateTransform pos = new TranslateTransform();
            pos.X = ((childGrid.ActualWidth / 2) - (C1 * 5)) + (C1 * column);
            pos.Y = ((childGrid.ActualWidth / 2) - (C1 * 5)) + (C1 * row);

            Image empty = new Image()
            {
                Name = "Empty",
                Width = C1,
                Height = C1,
                RenderTransform = pos,
            };

            listOfSquaresVisual.Add(new SquareWrapper(row, column, empty));
            childGrid.Children.Add(empty);
        }

        // SetCanMove
        public void SetCanMove(bool setting)
        {
            canMove = setting;
        }

        private void Button_Left_Click(object sender, RoutedEventArgs e)
        {
            if (canMove)
            {
                controller.SaveLastMove();
                controller.MovePlayer(Direction.Left);
                AdditionDeal();
            }
        }

        private void Button_Up_Click(object sender, RoutedEventArgs e)
        {
            if (canMove)
            {
                controller.SaveLastMove();
                controller.MovePlayer(Direction.Up);
                AdditionDeal();
            }
        }

        private void Button_Down_Click(object sender, RoutedEventArgs e)
        {
            if (canMove)
            {
                controller.SaveLastMove();
                controller.MovePlayer(Direction.Down);
                AdditionDeal();
            }
        }

        private void Button_Right_Click(object sender, RoutedEventArgs e)
        {
            if (canMove)
            {
                controller.SaveLastMove();
                controller.MovePlayer(Direction.Right);
                AdditionDeal();
            }
        }


        private void Button_Undo_Click(object sender, RoutedEventArgs e)
        {
            if (canMove)
            {
                controller.LoadLastMove();
                AdditionDeal();
            }
        }

        private void Button_Pause_Click(object sender, RoutedEventArgs e)
        {
            if (1==1)//(canMove)
            {
                ShowPauseMenu();
                AdditionDeal();
            }
        }

        

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (1==1)//(canMove)
            {
                controller.Restart();
                AdditionDeal();
            }
        }

        async void AdditionDeal()
        {
            ResetLevel();


            if (controller.CheckWin() == true)
            {
                canMove = false;
                Parameters par = new Parameters(controller, this);
                CoreApplicationView newView = CoreApplication.CreateNewView();
                int newViewId = 0;

                await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Frame frame = new Frame();
                    frame.Navigate(typeof(FinishScreen), par);
                    Window.Current.Content = frame;
                    Window.Current.Activate();

                    newViewId = ApplicationView.GetForCurrentView().Id;
                });
                bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
            }
        }


    }//class end

}//namespace end
