namespace DbCore.WebAutoFear.Klasat
{
    public class Address
    {
        public string AddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Contact { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string CustomerId { get; set; }

        public Address()
        {

        }

        public Address(string AddressId, string FirstName, string LastName, string CompanyName, string Contact, string Street, string ZipCode, string City, string Country, string Phone, string Fax, string EMail, string CustomerId)
        {
            this.AddressId = AddressId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.CompanyName = CompanyName;
            this.Contact = Contact;
            this.Street = Street;
            this.ZipCode = ZipCode;
            this.City = City;
            this.Country = Country;
            this.Phone = Phone;
            this.Fax = Fax;
            this.EMail = EMail;
            this.CustomerId = CustomerId;
        }
    }
}
