namespace WFTracer
{
    public class News
    {
        public void SetActive()
        {
            AppManager.UpdateControls(this.GetType().Name);
        }

        public void Update(dynamic data)
        {
            AppManager.mainWindow.News_DataGrid.Items.Clear();
            foreach (var _news in data.news)
            {
                string message = _news.message;
                string link = _news.link;
                NewsData newsData = new NewsData(message, link);
                AppManager.mainWindow.News_DataGrid.Items.Insert(0, newsData);
            }
        }

        public class NewsData
        {
            public string Message { get; set; }
            public string Link { get; set; }
            public NewsData(string message, string link)
            {
                this.Message = message;
                this.Link = link;
            }

        }
    }
}
