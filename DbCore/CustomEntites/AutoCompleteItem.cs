using System.Data;

namespace DbCore
{

    //todo getson rename,konvert ne class 
    public struct AutoCompleteItem
    {
        public string label { get; set; }
        public string value { get; set; }
        public string desc { get; set; }
        public string kategori { get; set; }
        public object objekti { get; set; }
      

        public static AutoCompleteItem Krijo(IDataRecord record)
        {
            return new AutoCompleteItem
            {
                label = record["label"].ToString(),
                value = record["value"].ToString(),
                desc = record["desc"].ToString()
            };
        }
        public static AutoCompleteItem KrijoPaKategori(IDataRecord record)
        {
            AutoCompleteItem autoObj = Krijo(record);

            autoObj.kategori = record["kategori"].ToString();
            return autoObj;
        }


    }
}
