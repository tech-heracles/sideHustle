using System.Data;

namespace DbCore.WebAutoFear.Klasat
{
    public class Item : ItemBase
    {
        public Warehouse[] Warehouses { get; set; }
        public Price[] Prices { get; set; }
        public Quantity[] Quantities { get; set; }
        public Item[] AlternativeItems { get; set; }
        public Item[] ReplacementItems { get; set; }
        public Item[] AccessoryItems { get; set; }
        public Item[] SuggestionItems { get; set; }
        public Item[] LinkedItems { get; set; }
        public Item[] ObligatedItems { get; set; }
        public InstalledItemInfo InstalledItemInfo { get; set; }
        public AvailableState AvailState { get; set; }
        public ItemInfo[] AdditionalArticleInformation { get; set; }
        public ItemOrder ItemOrder { get; set; }
        public bool HasActionPrice { get; set; }
        public ShipmentMode[] ShipmentModes { get; set; }
        public ArticleAction[] ArticleActions { get; set; }

        public Item()
        {

        }

        public void ktheInfoArtikuj(string furnitore)
        {
            clsDatabaseWebAutoFear dbAutoFear = new clsDatabaseWebAutoFear();
            DataTable dtArtikuj = dbAutoFear.merrArtikujPerAutoFear(WholesalerArticleNumber);
            if (dtArtikuj.Rows.Count > 0)
            {
                DataTable dtArtikujBarkode = dbAutoFear.merrArtikujBarkodePerAutoFear(WholesalerArticleNumber);
                DataTable dtArtikujCmime = dbAutoFear.merrArtikujCmimePerAutoFear(WholesalerArticleNumber, furnitore);
                Description = dtArtikuj.Rows[0]["PERSHKRIMARTIKULLI"].ToString();
                WholesalerArticleNumber = dtArtikuj.Rows[0]["KODARTIKULLI"].ToString();
                SupplierId = int.Parse(dtArtikuj.Rows[0]["KODKLIENTFURNITORKRYESOR"].ToString());
                SupplierName = dtArtikuj.Rows[0]["EMERTIMIKF"].ToString();
                SupplierArticleNumber = dtArtikuj.Rows[0]["PERSHKRIMTEFURNITORI"].ToString();
                Warehouses = new Warehouse[dtArtikuj.Rows.Count];
                Quantities = new Quantity[dtArtikuj.Rows.Count];
                EAN = new string[dtArtikujBarkode.Rows.Count];
                Prices = new Price[dtArtikujCmime.Rows.Count];
                decimal shumaSasive = 0;
                for (int i = 0; i < dtArtikuj.Rows.Count; i++)
                {
                    Warehouses[i] = new Warehouse(dtArtikuj.Rows[i]["PERSHKRIMI"].ToString(), new Quantity(decimal.Parse(dtArtikuj.Rows[i]["GJENDJESASI"].ToString()), dtArtikuj.Rows[i]["KODNJESI1"].ToString(), dtArtikuj.Rows[i]["KODNJESI2"].ToString()), (dtArtikuj.Rows[i]["MAG"].ToString()).Equals(dtArtikuj.Rows[i]["MAGDEFAULT"].ToString()) ? true : false);
                    Quantities[i] = new Quantity(decimal.Parse(dtArtikuj.Rows[i]["GJENDJESASI"].ToString()), dtArtikuj.Rows[i]["KODNJESI1"].ToString(), dtArtikuj.Rows[i]["KODNJESI2"].ToString());
                    shumaSasive = shumaSasive + decimal.Parse(dtArtikuj.Rows[i]["GJENDJESASI"].ToString());
                }
                for (int i = 0; i < dtArtikujBarkode.Rows.Count; i++)
                {
                    EAN[i] = dtArtikujBarkode.Rows[i]["BARKODI"].ToString();
                }
                for (int i = 0; i < dtArtikujCmime.Rows.Count; i++)
                {
                    Prices[i] = new Price(dtArtikujCmime.Rows[i]["PERSHKRIMNIVELCMIMI"].ToString(), decimal.Parse(dtArtikujCmime.Rows[i]["CMIMI"].ToString()), decimal.Parse(dtArtikujCmime.Rows[i]["NORMAPERQINDJE"].ToString()), dtArtikujCmime.Rows[i]["TVSH"].ToString() == "1" ? true : false, 1);
                }
                if (shumaSasive > 0)
                    AvailState = new AvailableState(1, "i disponueshem");
                else
                    AvailState = new AvailableState(2, "Na telefononi");
            }
        }
    }
}