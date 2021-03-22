using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WFTracer
{
    public partial class Main : Window
    {
        private bool bTopMost;

        public Main()
        {
            InitializeComponent();
        }

        private void MenuLoaded(object sender, RoutedEventArgs e)
        {
            LoadTheme();

            Dispatcher.BeginInvoke(new Action(async () =>
            {
                while (true)
                {
                    await AppManager.updateManager.Update();
                    await Task.Delay(60 * 1000);

                }


            }), DispatcherPriority.Background);


            Dispatcher.BeginInvoke(new Action(async () =>
            {
                while (true)
                {
                    await AppManager.updateManager.UpdateMarket();
                    await Task.Delay(36 * 10000);
                }
            }), DispatcherPriority.Background);
        }

        #region Events

        private void OpenLink(object sender, RoutedEventArgs e)
        {
            var cellinfo = News_DataGrid.SelectedCells[1];
            var content = (cellinfo.Column.GetCellContent(cellinfo.Item) as TextBlock).Text;
            Process.Start(content);
        }
        private void BMouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            button.Background = StyleManager.btnHoverBGColor;
        }
        private void BMouseLeave(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button.Uid != "1")
                button.Background = Brushes.Transparent;
        }
        private void SetTopMost(object sender, RoutedEventArgs e)
        {
            bTopMost = !bTopMost;
            this.Topmost = bTopMost;
            AppManager.priceWindow.Topmost = bTopMost;

        }
        private void ChangeTheme(object sender, RoutedEventArgs e)
        {

            if (Properties.Settings.Default.bDarkTheme)
            {
                StyleManager.WhiteTheme();
                Properties.Settings.Default.bWhiteTheme = true;
                Properties.Settings.Default.bDarkTheme = false;
            }
            else if (Properties.Settings.Default.bWhiteTheme)
            {
                StyleManager.DarkTheme();
                Properties.Settings.Default.bDarkTheme = true;
                Properties.Settings.Default.bWhiteTheme = false;
            }
            Properties.Settings.Default.Save();
        }
        private void Row_Click(object sender, MouseButtonEventArgs e)
        {

            var cellInfo = Fissure_DataGrid.SelectedCells[1];

            var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;

            if (content == "Lith")
            {
                AppManager.priceWindow.Hide();

                AppManager.priceWindow.AxiGridList.Visibility = Visibility.Hidden;
                AppManager.priceWindow.NeoGridList.Visibility = Visibility.Hidden;
                AppManager.priceWindow.MesoGridList.Visibility = Visibility.Hidden;

                AppManager.priceWindow.WindowTitle.Content = "Lith Price List";
                AppManager.priceWindow.LithGridList.Visibility = Visibility.Visible;
                AppManager.priceWindow.Top = Top - 50;
                AppManager.priceWindow.Left = Left - 280;
                AppManager.priceWindow.Show();
            }

            if (content == "Meso")
            {
                AppManager.priceWindow.Hide();

                AppManager.priceWindow.AxiGridList.Visibility = Visibility.Hidden;
                AppManager.priceWindow.LithGridList.Visibility = Visibility.Hidden;
                AppManager.priceWindow.NeoGridList.Visibility = Visibility.Hidden;

                AppManager.priceWindow.WindowTitle.Content = "Meso Price List";
                AppManager.priceWindow.MesoGridList.Visibility = Visibility.Visible;
                AppManager.priceWindow.Show();
            }

            if (content == "Neo")
            {
                AppManager.priceWindow.Hide();

                AppManager.priceWindow.AxiGridList.Visibility = Visibility.Hidden;
                AppManager.priceWindow.LithGridList.Visibility = Visibility.Hidden;
                AppManager.priceWindow.MesoGridList.Visibility = Visibility.Hidden;

                AppManager.priceWindow.WindowTitle.Content = "Neo Price List";
                AppManager.priceWindow.NeoGridList.Visibility = Visibility.Visible;
                AppManager.priceWindow.Show();
            }

            if (content == "Axi")
            {
                AppManager.priceWindow.Hide();

                AppManager.priceWindow.NeoGridList.Visibility = Visibility.Hidden;
                AppManager.priceWindow.LithGridList.Visibility = Visibility.Hidden;
                AppManager.priceWindow.MesoGridList.Visibility = Visibility.Hidden;

                AppManager.priceWindow.WindowTitle.Content = "Axi Price List";
                AppManager.priceWindow.AxiGridList.Visibility = Visibility.Visible;
                AppManager.priceWindow.Show();
            }

        }
        private void NewsBtn_Click(object sender, RoutedEventArgs e) => AppManager.news.SetActive();
        private void FissuresBtn_Click(object sender, RoutedEventArgs e) => AppManager.fissure.SetActive();
        private void TraderBtn_Click(object sender, RoutedEventArgs e) => AppManager.voidTrader.SetActive();
        private void ArbitrationBtn_Click(object sender, RoutedEventArgs e) => AppManager.arbitration.SetActive();
        private void MarketBtn_Click(object sender, RoutedEventArgs e) => AppManager.market.SetActive();
        private void ExitAppMouseEnter(object sender, MouseEventArgs e) => ExitApp.Background = new SolidColorBrush(Color.FromRgb(200, 10, 10));
        private void ExitAppMouseLeave(object sender, MouseEventArgs e) => ExitApp.Background = Brushes.Transparent;
        private void MinimizeApplication(object sender, MouseButtonEventArgs e) => WindowState = WindowState.Minimized;
        private void ExitApplication(object sender, MouseButtonEventArgs e) => Application.Current.Shutdown();
        private void Drag(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception) { }
        }
        private void LoadTheme()
        {
            if (Properties.Settings.Default.bDarkTheme)
                StyleManager.DarkTheme();

            if (Properties.Settings.Default.bWhiteTheme)
                StyleManager.WhiteTheme();
        }
        private void GridMouseEnter(object sender, MouseEventArgs e)
        {
            DataGridRow column = sender as DataGridRow;
            column.Background = StyleManager.rowHoverBGColor;
            if (column.DataContext is News.NewsData newsData) column.ToolTip = newsData.Message;
        }
        private void GridMouseLeave(object sender, MouseEventArgs e)
        {
            DataGridRow column = sender as DataGridRow;
            column.Background = Brushes.Transparent;
        }
        #endregion
    }
}
