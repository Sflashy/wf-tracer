using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WFTracer
{
    public static class AppManager
    {
        public static readonly HttpClient client = new HttpClient();
        public static bool bIsConnected;
        public static Main mainWindow = (WFTracer.Main)App.Current.MainWindow;
        public static UpdateManager updateManager = new UpdateManager();
        public static Fissure fissure = new Fissure();
        public static News news = new News();
        public static VoidTrader voidTrader = new VoidTrader();
        public static Arbitration arbitration = new Arbitration();
        public static MarketManager marketManager = new MarketManager();
        public static PriceWindow priceWindow = new PriceWindow();
        public static Market market = new Market();
        public static dynamic JsonData { get; set; }

        public static void UpdateControls(string _object)
        {
            /*Button Manager*/
            foreach (Button button in FindControls<Button>(mainWindow))
            {
                if (button.Name.Contains(_object))
                {
                    button.Uid = "1";
                    button.Background = StyleManager.activeBtnBGColor;
                    button.BorderThickness = new Thickness(0, 0, 0, 2);
                    button.BorderBrush = StyleManager.activeBtnBBColor;
                }
                else
                {
                    button.Uid = "0";
                    button.Background = Brushes.Transparent;
                    button.BorderThickness = new Thickness(0);
                }
            }

            /*DataGrid Manager*/

            foreach (DataGrid datagrid in FindControls<DataGrid>(mainWindow))
            {
                if (datagrid.Name.Contains(_object))
                {
                    if (datagrid.Name.Contains("Market"))
                    {
                        if (!market.bIsMarketUpated)
                        {
                            mainWindow.MarketStatus_DataGrid.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            datagrid.Visibility = Visibility.Visible;
                        }

                        continue;
                    }
                    if(datagrid.Name.Contains("VoidTrader"))
                    {
                        if (voidTrader.bIsArrived)
                            mainWindow.VoidTrader_Inventory.Visibility = Visibility.Visible;
                        else
                            mainWindow.VoidTrader_DataGrid.Visibility = Visibility.Visible;
                        continue;

                    }
                    datagrid.Visibility = Visibility.Visible;

                }
                else
                {
                    mainWindow.MarketStatus_DataGrid.Visibility = Visibility.Hidden;
                    datagrid.Visibility = Visibility.Hidden;
                }
            }
        }

        public static IEnumerable<T> FindControls<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                int depObjCount = VisualTreeHelper.GetChildrenCount(depObj);
                for (int i = 0; i < depObjCount; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    if (child is GroupBox)
                    {
                        GroupBox gb = child as GroupBox;
                        Object gpchild = gb.Content;
                        if (gpchild is T)
                        {
                            yield return (T)child;
                            child = gpchild as T;
                        }
                    }

                    foreach (T childOfChild in FindControls<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
