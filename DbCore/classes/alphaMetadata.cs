using Newtonsoft.Json.Linq;

namespace DbCore.classes
{
	public class AlphaMetadata
	{
		public string organization;
		public string enterprise;
		public string invoiceFormat;
		public string integrated;
		public string sentToAlpha;
		public string userEmail;
		public static AlphaMetadata FromJObject(JObject jObject)
		{
			AlphaMetadata alphaMetadata = new AlphaMetadata();

			alphaMetadata.organization = jObject.GetValue("organization")?.ToString();
			alphaMetadata.enterprise = jObject.GetValue("enterprise")?.ToString();
			alphaMetadata.invoiceFormat = jObject.GetValue("invoiceFormat")?.ToString();
			alphaMetadata.integrated = jObject.GetValue("integrated")?.ToString();
			alphaMetadata.sentToAlpha = jObject.GetValue("sentToAlpha")?.ToString();
			alphaMetadata.userEmail = jObject.GetValue("userEmail")?.ToString();

			return alphaMetadata;
		}
	}
	
}