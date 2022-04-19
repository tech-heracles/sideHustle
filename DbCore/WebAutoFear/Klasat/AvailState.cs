namespace DbCore.WebAutoFear.Klasat
{
    public class AvailableState
    {
        public int AvailState { get; set; }
        public string AvailDescription { get; set; }
        public string AvailIconUrl { get; set; }
        public AvailableState()
        {
        }

        public AvailableState(int AvailState, string AvailDescription)
        {
            this.AvailState = AvailState;
            this.AvailDescription = AvailDescription;
        }

    }
}