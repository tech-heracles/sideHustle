namespace DbCore.WebAutoFear.Klasat
{
    public class Quantity
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string QuantityUnit { get; set; }
        public string PackingUnit { get; set; }
        public decimal MinQuantity { get; set; }
        public decimal MaxQuantity { get; set; }
        public string ExpectedDeliveryTime { get; set; }
        public AvailableState AvailState { get; set; }
        public string[] Memo { get; set; }
        public int LotSize1 { get; set; }
        public int LotSize2 { get; set; }
        public decimal Division { get; set; }
        public decimal QuantityPackingUnit { get; set; }
        public Tour Tour { get; set; }

        public Quantity()
        {
        }

        public Quantity(decimal value, string QuantityUnit, string PackingUnit)
        {
            this.Value = value;
            this.QuantityUnit = QuantityUnit;
            this.PackingUnit = PackingUnit;

            if (Value > 0)
                AvailState = new AvailableState(1, "i disponueshem");
            else
                AvailState = new AvailableState(2, "Na telefononi");
        }
    }
}