using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WFTracer
{
    public class UpdateManager
    {
        public async Task Update()
        {
            while (true)
            {
                try
                {
                    HttpResponseMessage response = await AppManager.client.GetAsync("https://api.warframestat.us/pc/");
                    string jsonString = await response.Content.ReadAsStringAsync();
                    AppManager.JsonData = JsonConvert.DeserializeObject(jsonString);
                    AppManager.mainWindow.LoadingBorder.Visibility = System.Windows.Visibility.Hidden;
                    AppManager.bIsConnected = true;
                    AppManager.arbitration.Update(AppManager.JsonData);
                    AppManager.fissure.Update(AppManager.JsonData);
                    AppManager.news.Update(AppManager.JsonData);
                    AppManager.voidTrader.Update(AppManager.JsonData);
                    break;
                }
                catch (Exception)
                {
                    AppManager.mainWindow.LoadingBorder.Visibility = System.Windows.Visibility.Visible;
                    AppManager.bIsConnected = false;
                    await Task.Delay(1000);
                }

            }



        }

        public async Task UpdateMarket()
        {
            do
            {
                await Task.Delay(1000);

            } while (!AppManager.bIsConnected);

            AppManager.mainWindow.Market_DataGrid.Items.Clear();
            await AppManager.marketManager.Update();
        }
    }
}
