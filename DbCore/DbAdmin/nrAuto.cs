using System;
using System.Collections.Generic;
using DbCore.IMBUtils.Messages;
using Newtonsoft.Json;

namespace DbCore.DbAdmin
{

    public struct NrAuto
    {
        public string kodKontrolli;
        public int idNrAuto;
        public string vlereNrAuto;
        public bool isModified;
        public string pershkrimMesazhi;


        public static IDictionary<string, object> ShtoNrAuto(IDictionary<string, object> devHf, NrAuto nrAuto)
        {
            devHf.Add(nrAuto.kodKontrolli, JsonConvert.SerializeObject(nrAuto));
            return devHf;
        }

        public static NrAuto LexoNrAuto(string value) =>
            JsonConvert.DeserializeObject<NrAuto>(value); 
        
       

        public static IDictionary<string, object> ShtoNeRegjistrime(IDictionary<string, object> devHfregjistrime, IDictionary<string, object> devHf, string kodkontrolli, string kontrollregjistrime)
        {
            if (!devHf.ContainsKey(kodkontrolli))
                return devHfregjistrime; // todo duhet te shikohet cfare do te ktheje nqs kontrolli nuk ka vlere
            object value;
            devHf.TryGetValue(kodkontrolli, out value);
            var nr = LexoNrAuto(value?.ToString());
            nr.kodKontrolli = kontrollregjistrime;
            return ShtoNrAuto(devHfregjistrime, nr);
        }

        /// <summary>
        ///  merr vleren e nr autos te nje kontrolli te caktuar
        /// </summary>
        /// <param name="list">lista me nr auto</param>
        /// <param name="kodkontrol">kontrolli</param>
        /// <returns> vleren e ruajtur tek lista e nr auto</returns>
        public static string ktheVlerenEre(List<NrAuto> list, string kodkontrol)
        {
            foreach (var nr in list)
                if (nr.kodKontrolli == kodkontrol)
                    return nr.vlereNrAuto;
            return string.Empty;
        }

       

        public static IDictionary<string, object> VendosVleratNrAuto(IDictionary<string, object> hfNrAuto, 
            Dictionary<string, string> controls)
        {
            var list = new List<NrAuto>();

            foreach (var elem in hfNrAuto)
            {
                string kodi;
                if (controls == null)
                    throw new Exception("Kontrolli i numrit automatik nuk gjehet ne faqe");
                controls.TryGetValue(elem.Key, out kodi);

                var nrAuto = LexoNrAuto(elem.Value.ToString()); 
                if (nrAuto.vlereNrAuto != kodi)
                    nrAuto.isModified = true; 
                list.Add(nrAuto);
            }

            for (int i = 0, listCount = list.Count; i < listCount; i++)
                hfNrAuto = ShtoNrAuto(hfNrAuto, list[i]);

            return hfNrAuto;
        }
        public static IDictionary<string, object> VendosVleratNrAutoPerKod(IDictionary<string, object> hfNrAuto,
            string kodi)
        {
            var list = new List<NrAuto>();

            foreach (var elem in hfNrAuto)
            {
                var nrAuto = LexoNrAuto(elem.Value.ToString());
                if (nrAuto.vlereNrAuto != kodi)
                    nrAuto.isModified = true;
                list.Add(nrAuto);
            }
            for (int i = 0, listCount = list.Count; i < listCount; i++)
                hfNrAuto = ShtoNrAuto(hfNrAuto, list[i]);
            return hfNrAuto;
        }

        public static clsMesazh RuajVlera(out bool kaNdryshimNrAuto, List<NrAuto> list, DateTime dateDokumenti, int idPerdoruesi, int idndermarje)
        {
            var dbAdmin = new clsDatabaseAdmin();
            return ruajvlera(out kaNdryshimNrAuto, list, dateDokumenti, idPerdoruesi, idndermarje, dbAdmin);
        }

        public static clsMesazh ruajvlera(out bool kaNdryshimNrAuto, List<NrAuto> list, DateTime dateDokumenti, int idPerdoruesi, int idndermarje, clsDatabaseAdmin db)
        {
            var mesazhiRuajtjes = MessagesResource.Messages["msgRuajtjaPerFundoiMeSuksesMeVlerat"];
            kaNdryshimNrAuto = false;
            for (var i = 0; i < list.Count; i++)
            {
                var nr = list[i];
                if (nr.isModified)
                    continue;
                if (!string.IsNullOrEmpty(nr.pershkrimMesazhi))
                {
                    mesazhiRuajtjes += nr.pershkrimMesazhi;
                    kaNdryshimNrAuto = true;
                }
                if (i != list.Count - 1)
                    mesazhiRuajtjes += ", ";
                //Kontrollojme nese vlera e fushes automatike eshte e barabarte me vleren e sugjeruar

                var nrAutom = clsNrAutom.merrNumrinAutomatikSipasId(nr.idNrAuto, db);
                if (!nrAutom.eshteAktivNrAutomatik(dateDokumenti, db)) continue;
                var nrFundit = nrAutom.ktheNrAutomatikFundit(dateDokumenti, db);
                if (nrFundit != null)
                {
                    nrFundit.Vlera = nr.vlereNrAuto;
                    nrFundit.Data = dateDokumenti;
                    nrFundit.IdPerdoruesi = idPerdoruesi;
                    var mesazh = nrFundit.modifiko(db);
                    if (!mesazh.Status)
                        return mesazh;
                }
                else
                {
                    nrFundit = new clsNrAutomatikFundit
                    {
                        IdNrAutom = nr.idNrAuto,
                        Vlera = nr.vlereNrAuto,
                        Data = dateDokumenti,
                        IdNdermarje = idndermarje,
                        IdPerdoruesi = idPerdoruesi,
                        IdStatusDok = 1
                    };
                    var mesazh = nrFundit.ruaj(db);
                    if (!mesazh.Status)
                        return mesazh;
                }
            }
            return !kaNdryshimNrAuto
                ? new clsMesazh(true, "Kontrollet u kaluan me sukses")
                : new clsMesazh(true, mesazhiRuajtjes);
        }
    }
}