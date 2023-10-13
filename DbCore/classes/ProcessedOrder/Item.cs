using DbCore.DbInventari;
using DbCore.DbRegjistrim;

namespace DbCore.classes
{
    public class Item
    {
        public string name { get; set; }
        public string code { get; set; }
        public string barcode { get; set; }
        public string[] barcodes { get; set; }

        public string unit { get; set; }
        public string unitFisc { get; set; }
        public string category { get; set; }
        public string exemptReason { get; set; }
        public double price { get; set; }
        public string priceLevel { get; set; }
        public double priceWithVat { get; set; }
        public bool noVat { get; set; }
        public int vatPercentage { get; set; }
        public double quantity { get; set; }
        public double value { get; set; }
        public double valueWithVat { get; set; }
        public double valueWithVatNoDiscount { get; set; }
        public double discount { get; set; }
        public double vatValue { get; set; }
        public string warehouse { get; set; }
        public string expirationDate { get; set; }
        public Item()
        {

        }
        public Item(string name, string code, string barcode, string[] barcodes, string unit, string unitFisc, string category, string exemptReason, double price, string priceLevel, int priceWithVat, bool noVat, int vatPercentage, double quantity, double value, double valueWithVat, double valueWithVatNoDiscount, double discount, double vatValue, string warehouse, string expirationDate)
        {
            this.name = name;
            this.code = code;
            this.barcode = barcode;
            this.barcodes = barcodes;
            this.unit = unit;
            this.unitFisc = unitFisc;
            this.category = category;
            this.exemptReason = exemptReason;
            this.price = price;
            this.priceLevel = priceLevel;
            this.priceWithVat = priceWithVat;
            this.noVat = noVat;
            this.vatPercentage = vatPercentage;
            this.quantity = quantity;
            this.value = value;
            this.valueWithVat = valueWithVat;
            this.valueWithVatNoDiscount = valueWithVatNoDiscount;
            this.discount = discount;
            this.vatValue = vatValue;
            this.warehouse = warehouse;
            this.expirationDate = expirationDate;
        }
        public object toObject()
        {
            return new
            {
                name = this.name,
                code = this.code,
                barcode = this.barcode,
                barcodes = this.barcodes,
                unit = this.unit,
                unitFisc = this.unitFisc,
                category = this.category,
                exemptReason = this.exemptReason,
                price = this.price,
                priceLevel = this.priceLevel,
                priceWithVat = this.priceWithVat,
                noVat = this.noVat,
                vatPercentage = this.vatPercentage,
                quantity = this.quantity,
                value = this.value,
                valueWithVat = this.valueWithVat,
                valueWithVatNoDiscount = this.valueWithVatNoDiscount,
                discount = this.discount,
                vatValue = this.vatValue,
                warehouse = this.warehouse,
                expirationDate = this.expirationDate
            };
        }
        public static Item fromClsTrupiShitje(clsTrupiShitje item, int enterpriseId)
        {
            clsArtikulli art = new clsArtikulli();
            art.mbushArtikull(item.Kodi, enterpriseId);
            colKodbare kodBaretArtikulli = new colKodbare(art.IdArtikulli);

            clsNjesiArtikulli njesia = new clsNjesiArtikulli(item.IdNjesia);
            clsTaksa taksa = new clsTaksa(item.Tvsh);
            clsNjesiAdministrative magazina = new clsNjesiAdministrative(item.IdMagazina);
            Item processItem = new Item();
            processItem.barcodes = new string[kodBaretArtikulli.Count];
            for (int i = 0; i < kodBaretArtikulli.Count; i++)
            {
                if (kodBaretArtikulli[i].IdKodbari == item.IdBarkodi) processItem.barcode = kodBaretArtikulli[i].Pershkrimi;
                processItem.barcodes[i] = kodBaretArtikulli[i].Pershkrimi;
            }
            processItem.name = item.Pershkrimi;
            processItem.code = item.Kodi;
            processItem.unit = njesia.KodNjesia;
            processItem.unitFisc = njesia.KodEinvoice;
            processItem.category = "";
            processItem.exemptReason = "";
            processItem.price = item.Cmimi;
            processItem.priceLevel = "";
            processItem.priceWithVat = taksa.IdTaksa == 0 ? item.Cmimi : item.Cmimi * (1 + ((int)taksa.NormaPerqindje / 100));
            processItem.noVat = taksa.IdTaksa == 0 ? true : false;
            processItem.vatPercentage = (int)taksa.NormaPerqindje;
            processItem.quantity = item.Sasia;
            processItem.value = item.VleftaPaTvsh;
            processItem.valueWithVat = item.VleftaMeTvsh;
            processItem.valueWithVatNoDiscount = item.VleftaMeTvsh;
            processItem.discount = item.ZbritjeVlere;
            processItem.vatValue = item.VleftaMeTvsh - item.VleftaPaTvsh;
            processItem.warehouse = magazina.Kodi;
            processItem.expirationDate = "";

            return processItem;

        }


    }
}
