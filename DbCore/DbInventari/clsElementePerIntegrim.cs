using System;
using System.Data;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbInventari
{
    public class clsElementePerIntegrim
    {
        #region Atribute
        private int idElementi;
        private string kodi;
        private string emertimi;
        private int lloji;
        private bool aktiv;
        private string shenime;
        private DateTime dateRegjistrimi;
        private int idPerdoruesi;
        private int idModifikuesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;

        #endregion Atribute


        #region atributet
        public int IdElementi { get { return idElementi; } set { idElementi = value; } }
        public string Kodi { get { return kodi; } set { kodi = value; } }
        public string Emertimi { get { return emertimi; } set { emertimi = value; } }
        public int Lloji { get { return lloji; } set { lloji = value; } }
        public bool Aktiv { get { return aktiv; } set { aktiv = value; } }
        public string Shenime { get { return shenime; } set { shenime = value; } }
        public DateTime DateRegjistrimi { get { return dateRegjistrimi; } set { dateRegjistrimi = value; } }
        public int IdPerdoruesi { get { return idPerdoruesi; } set { idPerdoruesi = value; } }
        public int IdModifikuesi { get { return idModifikuesi; } set { idModifikuesi = value; } }
        public int IdNdermarje { get { return idNdermarje; } set { idNdermarje = value; } }
        public int IdStatusDok { get { return idStatusDok; } set { idStatusDok = value; } }
        public DateTime DtKrijimi { get { return dtKrijimi; } set { dtKrijimi = value; } }
        public DateTime DtModifikimi { get { return dtModifikimi; } set { dtModifikimi = value; } }
        

        #endregion atributet

        #region konstruktoret
        public clsElementePerIntegrim() { }

        public clsElementePerIntegrim(int idElementi, string kodi, string emertimi, int lloji, bool aktiv, string shenime,
                DateTime dateRegjistrimi, int idPerdoruesi, int idModifikuesi, int idNdermarje, int idStatusDok) {
            IdElementi = idElementi;
            Kodi = kodi;
            Emertimi = emertimi;
            Lloji = lloji;
            Aktiv = aktiv;
            Shenime = shenime;
            DateRegjistrimi = dateRegjistrimi;
            IdPerdoruesi = idPerdoruesi;
            IdModifikuesi = idModifikuesi;
            IdNdermarje = idNdermarje;
            IdStatusDok = idStatusDok;

        }

        

        public clsElementePerIntegrim(int idElementi)
        {
            using(clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                dbInv.MerrElementPerIntegrimSipasId(idElementi, this);
            }
        }

        #endregion konstruktoret

        public clsMesazh Ruaj()
        {
            using (clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                dbInv.beginTransaksion();
                clsMesazh mesazh = Ruaj(dbInv);
                if (mesazh.Status)
                    dbInv.commitTransaksion();
                else
                    dbInv.rollbackTransaksion();
                return mesazh;
            }
        }

        public clsMesazh Modifiko()
        {
            using (clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                clsMesazh mesazh = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te elementit");
                dbInv.beginTransaksion();
                mesazh = dbInv.modifikoElementinPerIntegrim(IdElementi, Emertimi, Aktiv, Shenime, IdPerdoruesi);

                if (mesazh.Status) {
                    IdModifikuesi = IdPerdoruesi;
                    dbInv.commitTransaksion();
                }
                else
                    dbInv.rollbackTransaksion();
                    
                return mesazh;
            }
                

        }

        public clsMesazh Ruaj(clsDatabaseInventari dbInv)
        {
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se elementit");
            try
            {
                int idElementi = -1;
                mesazhi = dbInv.ruajElementinPerIntegrim(out idElementi, Kodi, Emertimi, Lloji, Aktiv, Shenime, DateRegjistrimi, IdPerdoruesi, IdNdermarje);

                if (mesazhi.Status)
					IdElementi = idElementi;
                    
            }
            catch (Exception err)
            {
                ImbLogger.Error(err);
            }

            return mesazhi;
        }



        internal void Mbush(IDataRecord record)
        {
            try
            {
                int.TryParse(record["IDELEMENTI"].ToString(), out idElementi);
                kodi = record["KODI"].ToString();
                emertimi = record["EMERTIMI"].ToString();
                shenime = record["SHENIME"].ToString();
                int.TryParse(record["LLOJI"].ToString(), out lloji);
                bool.TryParse(record["AKTIV"].ToString(), out aktiv);
                DateTime.TryParse(record["DATEREGJISTRIMI"].ToString(), out dateRegjistrimi);
                int.TryParse(record["IDNDERMARJE"].ToString(), out idNdermarje);
                int.TryParse(record["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(record["IDMODIFIKUESI"].ToString(), out idModifikuesi);
                DateTime.TryParse(record["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(record["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);



            }
            catch (InvalidCastException)
            {
                throw new MyException("ERROR: Gabim gjate cast-it!");
            }
            catch (Exception)
            {
                throw new MyException("ERROR: Gabim gjate marrjes se elementit per integrim nga db-ja!");
            }



        }


        public static clsElementePerIntegrim Krijo(IDataRecord record)
        {
            clsElementePerIntegrim el = new clsElementePerIntegrim();
            el.Mbush(record);
            return el;
        }
        public static clsMesazh FshiUpdateStatusDok(int idElementi, int idModifikuesi)
        {
            using(clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                return FshiUpdateStatusDok(idElementi, idModifikuesi, dbInv);
            }
        }
        public static clsMesazh FshiUpdateStatusDok(int idElementi, int idModifikuesi, clsDatabaseInventari dbInv)
        {
            clsMesazh mesazh = new clsMesazh(false);
            mesazh = dbInv.fshiUpdateStatusDokElementiPerIntegrim(idElementi, idModifikuesi);
            return mesazh;
        }

        public static bool ekzistonElementMeKeteKod(string kodi, int idNderm)
        {
            using(clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                return dbInv.ekzistonElementPerIntegrimMeKeteKodPerKeteNdermarje(kodi, idNderm);
            }
        }

        public static int ktheIdElementi(string kodi, int idNdermarrje)
        {
            using(clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                return dbInv.MerrIdElementiPerIntegrimSipasKoditDheNdermarrjes(kodi, idNdermarrje);
            }
        }

        public static bool eshteElementILidhur(int idElementi)
        {
            using(clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                return dbInv.eshteILidhurKyElement(idElementi);
            }
        }

     
    }
}
