
using System;
using Newtonsoft.Json.Linq;

namespace DbCore.classes
{
	public class WTN
	{
		public string docNo;
		public string businUnitCode;
		public string carrierCode;
		public DateTime startDate;
		public DateTime endDate;
		public string destinationWarehouse;
		public string startWarehouse;
		public string isEscortRequired;
		public string isGoodsFlammable;
		public DateTime docDate;
		public object items;
		public string nivfsh;
		public string nslfsh;
		public double totalValue;
		public double totalVatValue;
		public string veichlePlates;
		public string description;
		public string warehouseMan;
		public string destinationAddress;
		public string operatorCode;
		public AlphaMetadata alphaMetadata;
		
		public static WTN FromJObject(JObject jObject)
		{
			WTN wtn = new WTN();
			wtn.docNo = jObject.GetValue("docNo")?.ToString();
			wtn.businUnitCode = jObject.GetValue("businUnitCode")?.ToString();
			wtn.carrierCode = jObject.GetValue("carrierCode")?.ToString();
			wtn.operatorCode = jObject.GetValue("operatorCode")?.ToString();
			
			wtn.destinationWarehouse = jObject.GetValue("destinationWarehouse")?.ToString();
			wtn.startWarehouse = jObject.GetValue("warehouse")?.ToString();
			wtn.description = jObject.GetValue("description")?.ToString();
			wtn.isEscortRequired = jObject.GetValue("isEscortRequired")?.ToString();
			wtn.isGoodsFlammable = jObject.GetValue("isGoodsFlammable")?.ToString();
			wtn.warehouseMan = jObject.GetValue("warehouseMan")?.ToString();
			wtn.destinationAddress = jObject.GetValue("destinationWarehouseAddress")?.ToString();
			wtn.alphaMetadata = AlphaMetadata.FromJObject(jObject.GetValue("alphaMetadata")?.ToObject<dynamic>());
			DateTime docDate,startDate,endDate;
			if (DateTime.TryParse(jObject.GetValue("docDate")?.ToString(), out docDate))
			{
				wtn.docDate = docDate;
			}
			if (DateTime.TryParse(jObject.GetValue("dateStart")?.ToString(), out startDate))
			{
				wtn.endDate = startDate;
			}if (DateTime.TryParse(jObject.GetValue("destinDate")?.ToString(), out endDate))
			{
				wtn.startDate = endDate;
			}

			wtn.items = jObject.GetValue("items");

			wtn.nivfsh = jObject.GetValue("nivfsh")?.ToString();
			wtn.nslfsh = jObject.GetValue("nslfsh")?.ToString();
			wtn.totalValue = Double.Parse(jObject.GetValue("totalValue")?.ToString());
			wtn.totalVatValue = Double.Parse(jObject.GetValue("totalVatValue")?.ToString());
			wtn.veichlePlates = jObject.GetValue("veichlePlates")?.ToString();

			return wtn;
		}
		
	}
}