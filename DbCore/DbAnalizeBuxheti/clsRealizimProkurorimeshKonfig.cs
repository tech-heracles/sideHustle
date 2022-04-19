using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsRealizimProkurimesh
    {
        #region atribute

        private int rpkId;
        private string kodi;
        private string pershkrimi;
        private int idPrindi;
        private int idPerdoruesi;
        private int idModifikuesi;
        private int idNdermarrje;
        private int idNdermVit;
        private int idStatusDok;
        private int niveli;
        private string kodiPrindit;
        private int niveliPrindit;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;

        #endregion atribute

        #region properties

        public int RpkId {
            get { return rpkId; }
            set { rpkId = value; }
        }
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }
        public int IdPrindi
        {
            get
            {
                return idPrindi;
            }
            set
            {
                idPrindi = value;
            }
        }
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        public int IdModifikuesi
        {
            get { return idModifikuesi; }
            set { idModifikuesi = value; }
        }
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }
        public int IdNdermVit
        {
            get { return idNdermVit; }
            set { idNdermVit = value; }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        public int Niveli
        {
            get { return niveli; }
            set { niveli = value; }
        }
        public int NiveliPrindit
        {
            get { return niveliPrindit; }
            set { niveliPrindit = value; }
        }
        public string KodiPrindit
        {
            get { return kodiPrindit; }
            set { kodiPrindit = value; }
        }
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }
        #endregion properties

        #region konstruktore
        public clsRealizimProkurimesh() { }

        #endregion konstruktore

        #region metoda publike
        public clsMesazh Ruaj()
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();

            clsMesazh mesazh = null;
            int _rpkId = -1;

            try
            {
                if (KaRegjistrimeMeKeteRealizimProkurimesh(rpkId))
                {
                    return new clsMesazh(false, "Nuk mund te vendoset ky prind per  " + Kodi + " sepse eshte perdorur ne regjistrime!");
                }
                dbAB.beginTransaksion();

                mesazh = dbAB.RuajRealizimProkurimeshKonfig(out _rpkId, kodi, pershkrimi, idPrindi, idPerdoruesi, idNdermarrje, niveli, idNdermVit);
                if (mesazh.Status)
                {
                    dbAB.commitTransaksion();
                    RpkId = _rpkId;
                }
                else
                    dbAB.rollbackTransaksion();
            }
            catch (Exception err)
            {
                dbAB.rollbackTransaksion();
                mesazh = new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se konfigurimit te realizimit te prokurimeve \n" + err.Message);
            }
            return mesazh;
        }

        public static clsMesazh Fshi(int rpkId, int idModifikuesi)
        {
            clsMesazh mesazh = null;

            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                try
                {
                    mesazh = dbAB.fshiKonfigurimRealizimProkurimeshUpdDel(rpkId, idModifikuesi);

                }
                catch (Exception err)
                {
                    mesazh = new clsMesazh(false, "Ndodhi nje gabim gjate fshirjes se konfigurimit te realizimit te prokurimit \n" + err.Message);
                }
            }
            return mesazh;
        }

        public clsMesazh Fshi()
        {
            return Fshi(rpkId, idModifikuesi);
        }

        public static bool KaRegjistrimeMeKeteRealizimProkurimesh(int rpkId)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.KaRegjistrimeMeKeteRealizimProkurimesh(rpkId);
            }
        }

        public static bool KaFemij(int shokID)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.kaFemijRealizimi(shokID);
            }
        }

        public clsMesazh Modifiko(colRealizimProkurimeshKonfig newCol)
        {



            if (IdPrindi == rpkId)
                return new clsMesazh(false, "Zerit me kodin " + Kodi + "Nuk mund ti caktohet prind vetvetja!");

            if (KaRegjistrimeMeKeteRealizimProkurimesh(IdPrindi))
                return new clsMesazh(false, "Prindi i zerit me kod " + Kodi + " eshte perdorur ne regjistrim!");

            IEnumerable<clsRealizimProkurimesh> teGjitheVecKetij = newCol.Where(x => x.RpkId != RpkId);
            clsRealizimProkurimesh prindiPotencial = newCol.Find(x => x.RpkId == IdPrindi);

            if (AnalizeBuxheti.FormohetCikel(teGjitheVecKetij, this, prindiPotencial))
                return new clsMesazh(false, "Nuk mund te vendoset ky prind sepse formohet cikel!");

            return Modifiko(new clsDatabaseAnalizeBuxheti());
        }
        public clsMesazh Modifiko(clsDatabaseAnalizeBuxheti dbAB)
        {
            clsMesazh mesazh = null;
            try
            {
                mesazh = dbAB.ModifikoRealizimProkurimeshKonfig(RpkId, Kodi, Pershkrimi, IdPrindi, IdNdermarrje, IdModifikuesi, Niveli, IdNdermVit);
                if (mesazh.Status)
                    dbAB.commitTransaksion();
                else dbAB.rollbackTransaksion();
            }
            catch (Exception err)
            {
                dbAB.rollbackTransaksion();
                mesazh = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te zerit te realizimit te prokurimit\n" + err.Message);
            }
            return mesazh;
        }

        public static int MerrNivelinSipasID(int idPrindi)
        {

            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.MerrNivelRealizimProkurimesh(idPrindi);
            }

        }
        #endregion metoda publike

        #region metoda internal
        internal void mbush(IDataRecord record)
        {

            try
            {
                int.TryParse(record["RPKID"].ToString(), out rpkId);
                kodi = record["KODI"].ToString();
                pershkrimi = record["PERSHKRIMI"].ToString();
                int.TryParse(record["IDPRINDI"].ToString(), out idPrindi);
                int.TryParse(record["IDKRIJUESI"].ToString(), out idPerdoruesi);
                int.TryParse(record["IDMODIFIKUESI"].ToString(), out idModifikuesi);
                int.TryParse(record["IDNDERMARRJE"].ToString(), out idNdermarrje);
                int.TryParse(record["IDNDERMVIT"].ToString(), out idNdermVit);
                int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
                int.TryParse(record["NIVELI"].ToString(), out niveli);
                DateTime.TryParse(record["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(record["DTMODIFIKIMI"].ToString(), out dtKrijimi);
                int.TryParse(record["NIVELIPRINDIT"].ToString(), out niveliPrindit);
                kodiPrindit = record["KODIPRINDIT"].ToString();
            }
            catch(InvalidCastException ex)
            {
                ImbLogger.Info($"Error ne marrjen e te dhenave nga databaza: {ex.Message}");
            }

        }

        public static clsRealizimProkurimesh Krijo(IDataRecord record)
        {
            clsRealizimProkurimesh clsRPK = new clsRealizimProkurimesh();
            clsRPK.mbush(record);
            return clsRPK;
        }
        #endregion metoda internal
    }
}
