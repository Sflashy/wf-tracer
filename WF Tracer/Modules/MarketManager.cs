using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace WFTracer
{
    public class MarketManager
    {

        public string Name { get; set; }
        public int Price { get; set; }
        public string Tier { get; set; }

        readonly TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        readonly List<string> ItemNameList = new List<string>();
        readonly List<string> typelist = new List<string> { "Lith", "Neo", "Meso", "Axi" };
        readonly List<string> dumpList = new List<string>();

        string jsonString;
        HttpResponseMessage jsonResponse;

        public async Task Update()
        {
            ItemNameList.Clear();
            AppManager.mainWindow.Market_DataGrid.Items.Clear();
            if (AppManager.mainWindow.MarketBtn.Uid == "1")
                AppManager.mainWindow.MarketStatus_DataGrid.Visibility = System.Windows.Visibility.Visible;
            HttpResponseMessage response = await AppManager.client.GetAsync("https://drops.warframestat.us/data/relics.json");
            string jsonString = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(jsonString);

            foreach (var tierType in typelist)
            {
                foreach (var relic in data.relics)
                {
                    if (relic.tier == tierType && relic.state == "Intact")
                    {
                        foreach (var reward in relic.rewards)
                        {
                            if (reward.rarity == "Rare")
                            {
                                string itemName = reward.itemName;
                                if (!ItemNameList.Contains(itemName) && itemName != "Forma Blueprint")
                                {
                                    ItemNameList.Add(itemName);
                                }
                            }
                        }
                    }
                }

                await GetPrices(tierType);
            }

        }

        public async Task GetPrices(string tierType)
        {
            dumpList.Clear();
            int highestPrice = 0;
            MarketManager tempMarket = new MarketManager();
            foreach (var _itemName in ItemNameList)
            {
                string itemName = _itemName.ToLower().Replace(" ", "_");

                try
                {
                    if (itemName.Contains("&"))
                        itemName = itemName.Replace("&", "and");
                    if (itemName.Contains("chassis") | itemName.Contains("systems") | itemName.Contains("neuroptics") | itemName.Contains("wings") | itemName.Contains("harness"))
                        itemName = itemName.Replace("_blueprint", "");

                    do
                    {
                        jsonResponse = await AppManager.client.GetAsync($"https://api.warframe.market/v1/items/{itemName}/statistics");

                    } while (!jsonResponse.IsSuccessStatusCode);

                    jsonString = await jsonResponse.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(jsonString);
                    foreach (var statistics in data.payload.statistics_live["48hours"])
                    {
                        if (statistics.order_type == "sell")
                        {
                            itemName = itemName.Replace("_", " ");
                            itemName = textInfo.ToTitleCase(itemName);
                            int price = statistics.min_price;
                            UpdatePriceList(tierType, itemName, price);

                            if (statistics.min_price >= highestPrice)
                            {
                                highestPrice = statistics.min_price;
                                tempMarket.Name = itemName;
                                tempMarket.Price = highestPrice;
                                tempMarket.Tier = tierType;
                            }

                        }

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(itemName);
                    Console.WriteLine(jsonString);
                    Console.WriteLine(e.Message);
                }
            }
            AppManager.market.bIsMarketUpated = true;
            AppManager.mainWindow.MarketStatus_DataGrid.Visibility = System.Windows.Visibility.Hidden;
            AppManager.mainWindow.Market_DataGrid.Items.Add(tempMarket);
        }

        private void UpdatePriceList(string tierType, string itemName, int price)
        {
            if (tierType == "Lith")
            {
                Lith tempLith = new Lith();
                tempLith.Item = itemName;
                tempLith.Price = price;
                if (!dumpList.Contains(itemName))
                {
                    Console.WriteLine(AppManager.priceWindow.LithGridList.Items.Contains(itemName));
                    AppManager.priceWindow.LithGridList.Items.Add(tempLith);
                    dumpList.Add(itemName);
                }
            }

            if (tierType == "Meso")
            {
                Meso tempMeso = new Meso();
                tempMeso.Item = itemName;
                tempMeso.Price = price;
                if (!dumpList.Contains(itemName))
                {
                    AppManager.priceWindow.MesoGridList.Items.Add(tempMeso);
                    dumpList.Add(itemName);
                }
            }

            if (tierType == "Neo")
            {
                Neo tempNeo = new Neo();
                tempNeo.Item = itemName;
                tempNeo.Price = price;
                if (!dumpList.Contains(itemName))
                {
                    AppManager.priceWindow.NeoGridList.Items.Add(tempNeo);
                    dumpList.Add(itemName);
                }
            }

            if (tierType == "Axi")
            {
                Axi tempAxi = new Axi();
                tempAxi.Item = itemName;
                tempAxi.Price = price;
                if (!dumpList.Contains(itemName))
                {
                    AppManager.priceWindow.AxiGridList.Items.Add(tempAxi);
                    dumpList.Add(itemName);
                }
            }
        }

        public class Lith
        {
            public string Item { get; set; }
            public int Price { get; set; }
        }

        public class Meso
        {
            public string Item { get; set; }
            public int Price { get; set; }
        }

        public class Neo
        {
            public string Item { get; set; }
            public int Price { get; set; }
        }

        public class Axi
        {
            public string Item { get; set; }
            public int Price { get; set; }
        }
    }
}
