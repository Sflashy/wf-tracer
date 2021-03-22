using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WFTracer
{

    public static class StyleManager
    {
        private static ResourceDictionary theme = new ResourceDictionary();

        /*
         * BG: Background
         * BB: BorderBrush
         */

        /*---------- Default Settings ----------*/
        public static SolidColorBrush activeBtnBGColor = new SolidColorBrush(Color.FromArgb(100, 50, 50, 50));
        public static SolidColorBrush btnHoverBGColor = new SolidColorBrush(Color.FromArgb(100, 50, 50, 50));
        public static SolidColorBrush activeBtnBBColor = new SolidColorBrush(Color.FromArgb(200, 200, 200, 200));
        public static SolidColorBrush rowHoverBGColor = new SolidColorBrush(Color.FromArgb(200, 38, 38, 38));
        public static double opacity = 0.9;

        /*---------- Dark Theme ----------*/
        public static SolidColorBrush dForeground = new SolidColorBrush(Color.FromArgb(221, 189, 189, 189));
        public static SolidColorBrush dBackground = new SolidColorBrush(Color.FromRgb(20, 20, 20));
        public static SolidColorBrush dBtnHoverBGColor = new SolidColorBrush(Color.FromArgb(100, 50, 50, 50));
        public static SolidColorBrush dHeaderBGColor = new SolidColorBrush(Color.FromRgb(18, 18, 18));
        public static SolidColorBrush dActiveBtnBGColor = new SolidColorBrush(Color.FromArgb(100, 50, 50, 50));
        public static SolidColorBrush dActiveBtnBBColor = new SolidColorBrush(Color.FromArgb(200, 200, 200, 200));
        public static SolidColorBrush dGridColor = new SolidColorBrush(Color.FromArgb(25, 200, 200, 200));
        public static SolidColorBrush dRowHoverBGColor = new SolidColorBrush(Color.FromArgb(200, 38, 38, 38));

        /*---------- White Theme ----------*/
        public static SolidColorBrush wBackground = new SolidColorBrush(Color.FromRgb(210, 210, 210));
        public static SolidColorBrush wForeground = new SolidColorBrush(Color.FromRgb(70, 70, 70));
        public static SolidColorBrush wHeaderBGColor = new SolidColorBrush(Color.FromRgb(230, 230, 230));
        public static SolidColorBrush wActiveBtnBGColor = new SolidColorBrush(Color.FromArgb(50, 189, 189, 189));
        public static SolidColorBrush wActiveBtnBBColor = new SolidColorBrush(Color.FromArgb(200, 40, 40, 40));
        public static SolidColorBrush wBtnHoverBGColor = new SolidColorBrush(Color.FromArgb(20, 189, 189, 189));
        public static SolidColorBrush wGridColor = new SolidColorBrush(Color.FromRgb(150, 150, 150));
        public static SolidColorBrush wRowHoverBGColor = new SolidColorBrush(Color.FromArgb(200, 185, 185, 185));

        public static void DarkTheme()
        {
            theme.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml", UriKind.RelativeOrAbsolute);
            AppManager.mainWindow.Resources.MergedDictionaries.Clear();
            AppManager.priceWindow.Resources.MergedDictionaries.Clear();

            AppManager.priceWindow.Resources.MergedDictionaries.Add(theme);
            AppManager.mainWindow.Resources.MergedDictionaries.Add(theme);

            activeBtnBBColor = dActiveBtnBBColor;
            btnHoverBGColor = dBtnHoverBGColor;
            activeBtnBGColor = dActiveBtnBGColor;
            rowHoverBGColor = dRowHoverBGColor;

            #region MainWindow

            AppManager.mainWindow.Opacity = opacity;
            AppManager.mainWindow.Background = dBackground;
            AppManager.mainWindow.LoadingBorder.Background = dBackground;
            AppManager.mainWindow.LoadingBar.Foreground = dForeground;

            foreach (Button button in AppManager.FindControls<Button>(AppManager.mainWindow))
            {
                if (button.Uid == "1")
                {
                    button.BorderBrush = dActiveBtnBBColor;
                    button.Background = dActiveBtnBGColor;
                }

                button.Foreground = dForeground;
            }

            foreach (DataGrid datagrid in AppManager.FindControls<DataGrid>(AppManager.mainWindow))
            {
                datagrid.Foreground = dForeground;
                datagrid.HorizontalGridLinesBrush = dGridColor;
                foreach (DataGridTextColumn column in datagrid.Columns)
                {
                    column.Foreground = dForeground;
                }
            }

            foreach (Grid grid in AppManager.FindControls<Grid>(AppManager.mainWindow))
            {
                if (grid.Name == "HeaderBackground" | grid.Name == "Header2Background")
                    grid.Background = dHeaderBGColor;
            }

            foreach (Label label in AppManager.FindControls<Label>(AppManager.mainWindow))
            {
                label.Foreground = dForeground;
            }

            #endregion

            #region PriceListWindow
            AppManager.priceWindow.Opacity = opacity;
            AppManager.priceWindow.Background = dBackground;

            /*PriceListWindows*/

            foreach (DataGrid datagrid in AppManager.FindControls<DataGrid>(AppManager.priceWindow))
            {
                datagrid.Foreground = dForeground;
                datagrid.HorizontalGridLinesBrush = dGridColor;
                foreach (DataGridTextColumn column in datagrid.Columns)
                {
                    column.Foreground = dForeground;
                }
            }

            foreach (Grid grid in AppManager.FindControls<Grid>(AppManager.mainWindow))
            {
                if (grid.Name == "Header")
                    grid.Background = dHeaderBGColor;
            }

            foreach (Label label in AppManager.FindControls<Label>(AppManager.priceWindow))
            {
                label.Foreground = dForeground;
            }
            #endregion
        }

        public static void WhiteTheme()
        {
            theme.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml", UriKind.RelativeOrAbsolute);
            AppManager.mainWindow.Resources.MergedDictionaries.Clear();
            AppManager.priceWindow.Resources.MergedDictionaries.Clear();

            AppManager.priceWindow.Resources.MergedDictionaries.Add(theme);
            AppManager.mainWindow.Resources.MergedDictionaries.Add(theme);

            activeBtnBBColor = wActiveBtnBBColor;
            btnHoverBGColor = wBtnHoverBGColor;
            activeBtnBGColor = wActiveBtnBGColor;
            rowHoverBGColor = wRowHoverBGColor;

            AppManager.mainWindow.Opacity = opacity;
            AppManager.mainWindow.Background = wBackground;
            AppManager.mainWindow.LoadingBorder.Background = wBackground;
            AppManager.mainWindow.LoadingBar.Foreground = wForeground;

            foreach (Button button in AppManager.FindControls<Button>(AppManager.mainWindow))
            {
                if (button.Uid == "1")
                {
                    button.BorderBrush = wActiveBtnBBColor;
                    button.Background = wActiveBtnBGColor;
                }

                button.Foreground = wForeground;
            }

            foreach (DataGrid datagrid in AppManager.FindControls<DataGrid>(AppManager.mainWindow))
            {
                datagrid.Foreground = wForeground;
                datagrid.HorizontalGridLinesBrush = wGridColor;
                foreach (DataGridTextColumn column in datagrid.Columns)
                {
                    column.Foreground = wForeground;
                }
            }

            foreach (Grid grid in AppManager.FindControls<Grid>(AppManager.mainWindow))
            {
                if (grid.Name == "HeaderBackground" | grid.Name == "Header2Background")
                    grid.Background = wHeaderBGColor;
            }

            foreach (Label label in AppManager.FindControls<Label>(AppManager.mainWindow))
            {
                label.Foreground = wForeground;
            }


            #region PriceListWindow
            AppManager.priceWindow.Opacity = opacity;
            AppManager.priceWindow.Background = wBackground;

            /*PriceListWindows*/

            foreach (DataGrid datagrid in AppManager.FindControls<DataGrid>(AppManager.priceWindow))
            {
                datagrid.Foreground = wForeground;
                datagrid.HorizontalGridLinesBrush = wGridColor;
                foreach (DataGridTextColumn column in datagrid.Columns)
                {
                    column.Foreground = wForeground;
                }
            }

            foreach (Grid grid in AppManager.FindControls<Grid>(AppManager.priceWindow))
            {
                if (grid.Name == "Header")
                    grid.Background = wHeaderBGColor;
            }

            foreach (Label label in AppManager.FindControls<Label>(AppManager.priceWindow))
            {
                label.Foreground = wForeground;
            }
            #endregion
        }
    }
}
