using System.Windows;

namespace WFTracer
{
    public class VoidTrader
    {
        public bool bIsArrived;

        public void SetActive()
        {
            AppManager.UpdateControls(this.GetType().Name);
        }
        public void Update(dynamic data)
        {
            AppManager.mainWindow.VoidTrader_DataGrid.Items.Clear();
            AppManager.mainWindow.VoidTrader_Inventory.Items.Clear();
            bIsArrived = data.voidTrader.active;
            if (bIsArrived)
            {
                foreach (var inventory in data.voidTrader.inventory)
                {
                    VT_Inventory vt_inventory = new VT_Inventory(inventory.item, inventory.ducats, inventory.credits);
                    AppManager.mainWindow.VoidTrader_Inventory.Items.Add(vt_inventory);
                }
            }
            else
            {
                string location = data.voidTrader.location;
                string arrivetime = data.voidTrader.startString;

                Trader trader = new Trader(location, arrivetime);
                AppManager.mainWindow.VoidTrader_DataGrid.Items.Add(trader);
            }


        }

        public class VT_Inventory
        {
            public string Item { get; set; }
            public int Ducats { get; set; }
            public int Credits { get; set; }

            public VT_Inventory(string item, int ducats, int credits)
            {
                this.Item = item;
                this.Ducats = ducats;
                this.Credits = credits;
            }
        }

        public class Trader
        {
            public string Location { get; set; }
            public string ArriveTime { get; set; }

            public Trader(string location, string arrivetime)
            {
                this.Location = location;
                this.ArriveTime = arrivetime;
            }
        }
    }
}
