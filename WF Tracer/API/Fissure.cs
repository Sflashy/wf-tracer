namespace WFTracer
{
    public class Fissure
    {
        public void SetActive()
        {
            AppManager.UpdateControls(this.GetType().Name);
        }
        public void Update(dynamic data)
        {
            AppManager.mainWindow.Fissure_DataGrid.Items.Clear();
            foreach (var fdata in data.fissures)
            {
                if (fdata.tier == "Requiem") continue;
                string missiontype = fdata.missionType;
                string tier = fdata.tier;
                string time = fdata.eta;
                string node = fdata.node;
                FissureData fissureData = new FissureData(missiontype, tier, time, node);
                AppManager.mainWindow.Fissure_DataGrid.Items.Add(fissureData);
            }

        }

        public class FissureData
        {
            public string MissionType { get; set; }
            public string Tier { get; set; }
            public string Time { get; set; }
            public string Node { get; set; }
            public FissureData(string missiontype, string tier, string time, string node)
            {
                this.MissionType = missiontype;
                this.Tier = tier;
                this.Time = time;
                this.Node = node;
            }
        }
    }
}
