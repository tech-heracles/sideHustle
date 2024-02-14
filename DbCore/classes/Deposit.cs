namespace DbCore.classes
{
    public class Deposit
    {
        public bool paid { get; set; }
        public string docNo { get; set; }
        public string docDate { get; set; }
        public AlphaMetadata alphaMetadata { get; set; }
    }
}