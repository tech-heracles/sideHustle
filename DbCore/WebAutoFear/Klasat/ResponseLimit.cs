namespace DbCore.WebAutoFear.Klasat
{
    public class ResponseLimit
    {
        public bool IsResponseBounded { get; set; }
        public int? ResponseMax { get; set; }
        public int NumberOfRequestedItems { get; set; }

        public ResponseLimit()
        {

        }

        public ResponseLimit(bool IsResponseBounded, int NumberOfRequestedItems)
        {
            this.IsResponseBounded = IsResponseBounded;
            this.NumberOfRequestedItems = NumberOfRequestedItems;
        }
    }
}