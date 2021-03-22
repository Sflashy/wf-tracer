namespace WFTracer
{
    public class Arbitration
    {
        public void SetActive()
        {
            AppManager.UpdateControls(this.GetType().Name);
        }

        public void Update(dynamic data)
        {
            AppManager.mainWindow.Arbitration_DataGrid.Items.Clear();
            string node = data.arbitration.node;
            string enemy = data.arbitration.enemy;
            string mission = data.arbitration.type;
            ArbitrationData arbitration = new ArbitrationData(node, enemy, mission);
            AppManager.mainWindow.Arbitration_DataGrid.Items.Add(arbitration);
        }

        public class ArbitrationData
        {
            public string Node { get; set; }
            public string Enemy { get; set; }
            public string Mission { get; set; }
            public ArbitrationData(string node, string enemy, string mission)
            {
                this.Node = node;
                this.Enemy = enemy;
                this.Mission = mission;
            }
        }
    }
}
