namespace DbCore.WebAutoFear.Klasat
{
    public class Warehouse
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public Quantity[] Quantities { get; set; }
        public string[] Memo { get; set; }
        public bool Default { get; set; }

        public Warehouse() {

        }

        public Warehouse(string Description, Quantity Quantity, bool Default)
        {
            Quantities = new Quantity[1];
            this.Description = Description;
            this.Quantities[0] = Quantity;
            this.Default = Default;
        }
    }
}
