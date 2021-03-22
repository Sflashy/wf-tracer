namespace WFTracer
{
    public class Market
    {
        public bool bIsMarketUpated;
        public void SetActive()
        {
            AppManager.UpdateControls(this.GetType().Name);
        }
    }

}
