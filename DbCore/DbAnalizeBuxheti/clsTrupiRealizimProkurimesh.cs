using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsTrupiRealizimProkurimesh
    {
        #region atribute
        private int rowNum;
        private int idTrupiRp;
        private int idKokaRp;
        private int rpkId;
        private double fondiLimit;
        private double vleraKontrates;
        private string llojProcedure;
        private string koheTenderi;
        private string operatoriEkonomik;
        private string burimiFinancimit;
        private int idKrijuesi;
        private int idModifikuesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idStatusDok;
        private string zeri;
        private string zeriPrind;
        #endregion atribute

        #region properties

        public int IdTrupiRp
        {
            get { return idTrupiRp; }
            set { idTrupiRp = value;}
        }
        public int IdKokaRp
        {
            get { return idKokaRp; }
            set { idKokaRp = value; }
        }
        public int RpkId
        {
            get { return rpkId; }
            set { rpkId = value; }
        }
        public double FondiLimit
        {
            get { return fondiLimit; }
            set { fondiLimit = value; }
        }
        public double VleraKontrates
        {
            get { return vleraKontrates;}
            set { vleraKontrates = value; }
        }
        public string LlojProcedure
        {
            get { return llojProcedure; }
            set { llojProcedure = value; }
        }
        public string KoheTenderi
        {
            get { return koheTenderi;}
            set { koheTenderi = value; }
        }
        public string OperatoriEkonomik
        {
            get { return operatoriEkonomik; }
            set { operatoriEkonomik = value; }
        }
        public string BurimiFinancimit
        {
            get { return burimiFinancimit; }
            set { burimiFinancimit = value; }
        }
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }
        public int IdModifikuesi
        {
            get { return idModifikuesi; }
            set { idModifikuesi = value; }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
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
        public string Zeri
        {
            get { return zeri; }
            set { zeri = value; }
        }
        public string ZeriPrind
        {
            get { return zeriPrind; }
            set { zeriPrind = value; }
        }

        public int RowNum { get { return rowNum; } }
        #endregion properties

        #region metoda publike
        internal clsMesazh Ruaj(clsDatabaseAnalizeBuxheti dbAB)
        {
            int idTrupi = -1;
            clsMesazh mesazh = dbAB.RuajTrupRealizimProkurimesh(out idTrupi, idKokaRp, rpkId, fondiLimit, vleraKontrates,llojProcedure,koheTenderi,operatoriEkonomik, burimiFinancimit , IdStatusDok, IdKrijuesi);
            if (mesazh.Status)
                IdTrupiRp = idTrupi;
            return mesazh;
        }

        public static clsMesazh FshiZerat(int rpkId)
        {
            using (clsDatabaseAnalizeBuxheti dbAb = new clsDatabaseAnalizeBuxheti())
            {
                return dbAb.fshiZeProkurimi(rpkId);
            }
        }

        public static clsMesazh updateDelZeri(int rpkId, int idkokarp, clsDatabaseAnalizeBuxheti dbAb)
        {
           
                return dbAb.updateDelZeProkurimi(rpkId, idkokarp);
            
        }
        public clsMesazh Modifiko()
        {
            clsMesazh mesazh = new clsMesazh("Pati nje problem ne modifikim!");
            clsDatabaseAnalizeBuxheti dbAb = new clsDatabaseAnalizeBuxheti();

            try
            {
                dbAb.beginTransaksion();
                mesazh = updateDelZeri(rpkId, idKokaRp, dbAb);
                if (!mesazh.Status)
                    throw new Exception("Pati nje problem ne modifikim!");
                mesazh = Ruaj(dbAb);
                if(!mesazh.Status)
                    throw new Exception("Pati nje problem ne modifikim!");
                dbAb.commitTransaksion();

            }
            catch(Exception ex)
            {
                dbAb.rollbackTransaksion();
                ImbLogger.Error(ex.Message);
            }
            return mesazh;
        }
        #endregion metoda publike

        #region metoda internal
        internal void mbush(IDataRecord record)
        {

            try
            {
                int.TryParse(record["ROWNUM"].ToString(), out rowNum);
                int.TryParse(record["IDTRUPIRP"].ToString(), out idTrupiRp);
                int.TryParse(record["IDKOKARP"].ToString(), out idKokaRp);
                int.TryParse(record["RPKID"].ToString(), out rpkId);
                double.TryParse(record["FONDI_LIMIT"].ToString(), out fondiLimit);
                double.TryParse(record["VLERA_KONTRATES"].ToString(), out vleraKontrates);
                llojProcedure = record["LLOJ_PROCEDURE"].ToString();
                koheTenderi = record["KOHE_TENDERI"].ToString();
                operatoriEkonomik = record["OPERATORI_EKONOMIK"].ToString();
                burimiFinancimit = record["BURIMI_FINANCIMIT"].ToString();
                int.TryParse(record["IDKRIJUESI"].ToString(), out idKrijuesi);
                int.TryParse(record["IDMODIFIKUESI"].ToString(), out idModifikuesi);
                int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
                DateTime.TryParse(record["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(record["DTMODIFIKIMI"].ToString(), out dtKrijimi);
                zeri = record["ZERI"].ToString();
                zeriPrind = record["ZERI_PRIND"].ToString();
            }
            catch (InvalidCastException ex)
            {
                ImbLogger.Info($"Error ne marrjen e te dhenave nga databaza: {ex.Message}");
            }

        }

        public static clsTrupiRealizimProkurimesh Krijo(IDataRecord record)
        {
            clsTrupiRealizimProkurimesh trp = new clsTrupiRealizimProkurimesh();
            trp.mbush(record);
            return trp;
        }

        

        #endregion metoda internal
    }
}
