namespace DbCore.classes
{
	public class BigQueryLogMessage
	{
		public string view { get; set; }
		public string user { get; set; }
		public string organization { get; set; }
		public string enterprise { get; set; }

		public BigQueryLogMessage(string view, string user, string organization, string enterprise)
		{
			this.view = view;
			this.user = user;
			this.organization = organization;
			this.enterprise = enterprise;
		}
	}
}
