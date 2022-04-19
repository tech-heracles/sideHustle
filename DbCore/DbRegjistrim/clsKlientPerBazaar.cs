using System;
using System.Data;
using DbCore.DbAdmin;

namespace DbCore.DbRegjistrim
{
    public class clsKlientPerBazaar
    {
        #region Atribute  
        private int idKlientPerBazaar;
        private string msisdn;
        private bool statusi;
        private int idKokaShitje;

        private DateTime dtKrijimi;
        private DateTime dtModifikimi;


        private int idKrijuesi;
        private int idModifikuesi;

        private int idNdermarrje;
        private int idStatusDok;
        private LlojMsisdn llojMsisdn;
        #endregion

        #region Properties
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

        public int IdKlientPerBazaar
        {
            get { return idKlientPerBazaar; }

            set { idKlientPerBazaar = value; }
        }

        public int IdKrijuesi
        {
            get { return idKrijuesi; }

            set { idKrijuesi = value; }
        }

        public int IdModifikuesi { get { return idModifikuesi; } set { idModifikuesi = value; } }

        public int IdNdermarrje
        {
            get { return idNdermarrje; }

            set { idNdermarrje = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }

            set { idStatusDok = value; }
        }

        public string Msisdn
        {
            get { return msisdn; }

            set { msisdn = value; }
        }

        public bool Statusi
        {
            get { return statusi; }

            set { statusi = value; }
        }

        public int IdKokaShitje
        {
            get
            {
                return idKokaShitje;
            }

            set
            {
                idKokaShitje = value;
            }
        }

        public LlojMsisdn LlojMsisdn
        {
            get
            {
                return llojMsisdn;
            }

            set
            {
                llojMsisdn = value;
            }
        }
        #endregion

        #region konstruktoret
        public clsKlientPerBazaar()
        {
        }

        public clsKlientPerBazaar(string msisdn)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            mbushKlientPerBazaar(db.MerrKlientMeKuponSipasNdermarrjesDheMsisdn(msisdn, clsNdermarrje.ktheIdNdermarrjeMeme()));
            db.Dispose();
        }

        public clsKlientPerBazaar(int idklientPerBazaar, string msisdn, LlojMsisdn llojMsisdn, bool statusi, int idNdermarrje, int idKrijuesi, int idModifikuesi, DateTime dtKrijimi, DateTime dtModifikimi, int idStatusDok)
        {
            IdKlientPerBazaar = idklientPerBazaar;
            Msisdn = msisdn;
            Statusi = statusi;

            IdKrijuesi = idKrijuesi;
            IdModifikuesi = idModifikuesi;
            DtKrijimi = dtKrijimi;
            DtModifikimi = dtModifikimi;
            IdStatusDok = idStatusDok;
            IdNdermarrje = idNdermarrje;
            LlojMsisdn = llojMsisdn;
            clsMesazh mesazh = KontrolloKlientPerBazaar();
            if (!mesazh)
                throw new Exception(mesazh.PershkrimMesazhi);
        }

        #endregion

        #region metoda private
        private bool EkzistonNjeKlientMeKeteMsisdn()
        {

            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                return dbRegj.EkzistonNjeKlientPerBazaarMeKeteMsisdn(msisdn, llojMsisdn, idNdermarrje);

        }

        private void mbushKlientPerBazaar(DataRow dataRow)
        {


            idKlientPerBazaar = int.Parse(dataRow["IDKLIENTPERBAZAAR"].ToString());
            msisdn = dataRow["MSISDN"].ToString();
            statusi = bool.Parse(dataRow["STATUSI"].ToString());
            IdStatusDok = int.Parse(dataRow["IDSTATUSDOK"].ToString());
            idKrijuesi = int.Parse(dataRow["IDKRIJUESI"].ToString());
            int.TryParse(dataRow["IDMODIFIKUESI"].ToString(), out idModifikuesi);
            idNdermarrje = int.Parse(dataRow["IDNDERMARRJE"].ToString());
            dtKrijimi = DateTime.Parse(dataRow["DTKRIJIMI"].ToString());
            DateTime.TryParse(dataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);
            Enum.TryParse(dataRow["LLOJMSISDN"].ToString(), out llojMsisdn);
            int.TryParse(dataRow["IDSHITJE"].ToString(), out idKokaShitje);
        }
        #endregion

        /// <summary>
        /// validon objektin klientperbazaar
        /// </summary>
        /// <returns></returns>
        #region metoda publike
        public clsMesazh KontrolloKlientPerBazaar()
        {
            if (string.IsNullOrWhiteSpace(msisdn))
                return new clsMesazh(false, "MSISDN eshte fushe e detyrueshme!");
            if (EkzistonNjeKlientMeKeteMsisdn())
                return new clsMesazh(false, "Ekziston nje klient me msisdn : " + Msisdn);
            if (llojMsisdn != LlojMsisdn.Bazaar && llojMsisdn != LlojMsisdn.DeviceWithDiscount)
                return new clsMesazh("Lloj i mssisdn nuk ekziston!");
            return new clsMesazh(true);
        }



        public clsKlientPerBazaar KrijoKlientPerBazaarPerImport(string msisdn, LlojMsisdn llojMsisdn, int idKrijuesi)
        {
            idKlientPerBazaar = -1;
            idStatusDok = 1;
            statusi = false;//kupon i pa perdorur
            DtModifikimi = default(DateTime);
            IdModifikuesi = default(int);
            dtKrijimi = DateTime.Now;
            IdNdermarrje = clsNdermarrje.ktheIdNdermarrjeMeme();
            return new clsKlientPerBazaar(idKlientPerBazaar, msisdn, llojMsisdn, statusi, idNdermarrje, idKrijuesi, idModifikuesi, dtKrijimi, dtModifikimi, idStatusDok);
        }

        public clsMesazh Ruaj()
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            try
            {
                clsMesazh mesazh = new clsMesazh();
                mesazh = KontrolloKlientPerBazaar();
                if (!mesazh)
                    return mesazh;

                db.beginTransaksion();

                mesazh = db.RuajKlientPerBazaar(out idKlientPerBazaar, msisdn, llojMsisdn, statusi, idNdermarrje, idKrijuesi, dtKrijimi, idStatusDok);
                if (mesazh)
                    db.commitTransaksion();
                else
                    db.rollbackTransaksion();
                return mesazh;
            }
            catch (Exception e)
            {
                db.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se klientit per bazaar  me msisdn " + msisdn + "\n" + e.Message);
            }
        }

        /// <summary>
        /// vendos statusin perdorur per kuponin me kodin e caktuar
        /// </summary>
        /// <param name="kodKuponi"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        public static clsMesazh UpdateStatusPerdorur(clsDatabaseRegjistrim dbRegj, string msisdn, LlojMsisdn lloji, int idKokaShitje, int idPerdoruesi)
        {
            return dbRegj.UpdateKlientPerBazaarStatusPerdorur(msisdn, lloji, clsNdermarrje.ktheIdNdermarrjeMeme(), idKokaShitje, idPerdoruesi);
        }

        /// <summary>
        /// kontrollon nese msisdn ekziston dhe eshte i pa perdorur;
        /// mesazhet qe vijne
        /// 1-kodi eshte perdorur
        /// 2-kodi nuk ekziston
        /// 3-ok
        /// 4-gabim i cmendur :P
        /// </summary>
        /// <param name="msisdn"></param>
        /// <returns></returns>
        public static clsMesazh ValidoMsisdn(string msisdn, LlojMsisdn llojMsisdn)
        {
            if (!msisdn.StartsWith("3556"))
                msisdn = string.Format("3556{0}", msisdn);
            int idNdermarrje = clsNdermarrje.ktheIdNdermarrjeMeme();
            clsMesazh is_valid = new clsMesazh(true);
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                is_valid = dbRegj.ValidoMsisdnPerBazaar(msisdn, llojMsisdn, idNdermarrje);

            return is_valid;
        }


        #endregion



    }
}