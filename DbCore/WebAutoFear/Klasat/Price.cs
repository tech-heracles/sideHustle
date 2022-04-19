namespace DbCore.WebAutoFear.Klasat
{
    public class Price
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public decimal VAT { get; set; }
        public bool TaxIncluded { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Rebate { get; set; }
        public Quantity BatchSize { get; set; }
        public int PriceCode { get; set; }
        public string[] Memo { get; set; }
        public decimal PriceUnit { get; set; }
        public int DateFrom { get; set; }
        public int DateTo { get; set; }

        public Price()
        {

        }

        public Price(string Description, decimal Value, decimal VAT, bool TaxIncluded, int PriceCode)
        {
            this.Description = Description;
            this.Value = Value;
            this.VAT = VAT;
            this.TaxIncluded = TaxIncluded;
            this.PriceCode = PriceCode;
        }

        public Price(string Description, decimal Value, decimal VAT, bool TaxIncluded, int PriceCode, int DateFrom, int DateTo)
        {
            this.Description = Description;
            this.Value = Value;
            this.VAT = VAT;
            this.TaxIncluded = TaxIncluded;
            this.PriceCode = PriceCode;
            this.DateFrom = DateFrom;
            this.DateTo = DateTo;
        }
    }
}
