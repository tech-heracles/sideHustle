namespace DbCore.classes
{
	public class GiftCard
	{
		public string voucherNo { get; set; }
		public string code { get; set; }
		public double balance { get; set; }
		public string name { get; set; }
		public string barcode { get; set; }

		public GiftCard(string voucherNo, string code, double balance, string name, string barcode)
		{
			this.voucherNo = voucherNo;
			this.code = code;
			this.balance = balance;
			this.name = name;
			this.barcode = barcode;
		}
		public GiftCard()
		{
			this.voucherNo = "";
			this.code = "";
			this.balance = 0;
			this.name = "";
			this.barcode = "";
		}
	}
}
