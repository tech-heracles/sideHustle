namespace DbCore.classes.ProcessedOrder
{
    public class Metadata
    {
        public string createdAt { get; set; }
        public string createdBy { get; set; }
        public string updatedAt { get; set; }
        public string updatedBy { get; set; }
        public string organization { get; set; }
        public string id { get; set; }

        public Metadata(string createdAt, string createdBy, string updatedAt, string updatedBy, string organization, string id)
        {
            this.createdAt = createdAt;
            this.createdBy = createdBy;
            this.updatedAt = updatedAt;
            this.updatedBy = updatedBy;
            this.organization = organization;
            this.id = id;
        }
        public object toObject()
        {
            return new
            {
                createdAt = this.createdAt,
                createdBy = this.createdBy,
                updatedAt = this.updatedAt,
                updatedBy = this.updatedBy,
                organization = this.organization,
                id = this.id
            };
        }


    }
}
