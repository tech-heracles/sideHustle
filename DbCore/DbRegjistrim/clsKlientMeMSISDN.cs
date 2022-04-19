using System;
using System.Data;
using DbCore.DbAdmin;
using System.Text.RegularExpressions;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.Integrime;

namespace DbCore.DbRegjistrim
{
    public class clsKlientMeMSISDN : IDataBaseReader
    {
        #region Atribute  
        private int idKlientMeMSISDN;
        private string msisdn;
        private string kodiFitues;
        private bool statusPerdorimi;
        private LlojPromocionMsisdn lloji;
        private string msisdnERe;

        private DateTime dtDok;
        private int idPerdoruesi;
        private int idNdermarrje;
        private int idMag;
        private IDataRecord record;

        #endregion

        #region Properties

        public int IdKlientMeMSISDN {
            get
            {
                return idKlientMeMSISDN;
            }

            set
            {
                idKlientMeMSISDN = value;
            }
        }
        public string Msisdn {
            get
            {
                return msisdn;
            }

            set
            {
                msisdn = value;
            }
        }

        public string KodiFitues {
            get
            {
                return kodiFitues;
            }

            set
            {
                kodiFitues = value;
            }
        }

        public bool StatusPerdorimi {
            get
            {
                return statusPerdorimi;
            }

            set
            {
                statusPerdorimi = value;
            }
        }

        public LlojPromocionMsisdn Lloji {
            get
            {
                return lloji;
            }

            set
            {
                lloji = value;
            }
        }

        public string MsisdnERe {
            get
            {
                return msisdnERe;
            }

            set
            {
                msisdnERe = value;
            }
        }

        public DateTime DtDok {
            get
            {
                return dtDok;
            }

            set
            {
                dtDok = value;
            }
        }

        public int IdPerdoruesi {
            get
            {
                return idPerdoruesi;
            }

            set
            {
                idPerdoruesi = value;
            }
        }

        public int IdNdermarrje {
            get
            {
                return idNdermarrje;
            }

            set
            {
                idNdermarrje = value;
            }
        }

        public int IdMag {
            get
            {
                return idMag;
            }

            set
            {
                idMag = value;
            }
        }

        #endregion

        #region konstruktoret
        public clsKlientMeMSISDN()
        {
        }
        
        public clsKlientMeMSISDN(int idKlientMeMSISDN, string msisdn, string kodiFitues, bool statusPerdorimi, LlojPromocionMsisdn lloji, string msisdnERe, DateTime dtDok, int idPerdoruesi, int idNdermarrje, int idMag)
        {
            IdKlientMeMSISDN = idKlientMeMSISDN;
            Msisdn = msisdn;
            KodiFitues = kodiFitues;
            StatusPerdorimi = statusPerdorimi;
            Lloji = lloji;
            MsisdnERe = msisdnERe;
            DtDok = dtDok;
            IdPerdoruesi = idPerdoruesi;
            IdNdermarrje = idNdermarrje;
            IdMag = idMag;
        }

        public clsKlientMeMSISDN(IDataRecord record)
        {
            Mbush(record);
        }

        public clsKlientMeMSISDN(int idKlientMeMsisdn)
        {
            using (var db = new clsDatabaseRegjistrim())
                db.MerrKlientMeMsisdnSipasID(idKlientMeMsisdn, this);
        }

        public clsKlientMeMSISDN(string msisdn)
        {
            using (var db = new clsDatabaseRegjistrim())
            {
                db.MerrKlientMeMsisdn(msisdn, this);
            }
        }
        #endregion

        #region metoda private


        #endregion

        #region metoda publike

        public clsMesazh Modifiko()
        {
            clsMesazh mesazh = new clsMesazh(false);

            using (var scope = new MyTransactionScope())
            using (var db = new clsDatabaseRegjistrim())
            {
                mesazh = ValidoMsisdn();
                if (!mesazh)
                    return mesazh;

                mesazh = ValidoMsisdnTeRe();
                if (!mesazh)
                    return mesazh;

                //mesazh = DergoPromocion();
                //if (!mesazh)
                //    return mesazh;

                mesazh = db.ModifikoKlientMeMsisdn(msisdn, kodiFitues, lloji, msisdnERe, idNdermarrje, idPerdoruesi, idMag);

                if (!mesazh)
                    return mesazh;

                scope.Complete();

                return mesazh;
            }
        }
        public clsMesazh Fshi()
        {
            clsMesazh mesazh = new clsMesazh(false);

            using (var scope = new MyTransactionScope())
            using (var db = new clsDatabaseRegjistrim())
            {
                mesazh = db.FshiKlientMeMsisdn(idKlientMeMSISDN);

                if (!mesazh)
                    return mesazh;

                scope.Complete();

                return mesazh;
            }
        }

        private clsMesazh DergoPromocion()
        {
            if (lloji != LlojPromocionMsisdn.Normal)
                return new clsMesazh(true);

            string kodPromocioni = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.GOLDEN_COST);
            string discountPromocioni = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.GOLDEN_DISCOUNT);
            string kostoPromocioni = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.GOLDEN_COST);

            var vfOneAdapter = new PromocioneAdapter();
            return vfOneAdapter.SubscribeBundle(msisdnERe, kodPromocioni, kostoPromocioni, discountPromocioni);

        }
        public clsMesazh ValidoMsisdn()
        {
            clsMesazh isValid = new clsMesazh(false, "Ndodhi nje gabim gjate validimit!");
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                if (lloji == LlojPromocionMsisdn.Golden)
                    isValid = dbRegj.ValidoKlientPerGoldenNumber(msisdn, kodiFitues);
                else if (lloji == LlojPromocionMsisdn.Normal)
                    isValid = dbRegj.ValidoKlientPerNormalNumber(kodiFitues);
            }
            return isValid;
        }

        public clsMesazh Valido()
        {
            var mesazh = validoFormatMSISDN(msisdn);

            if (!mesazh)
                return mesazh;

            if (string.IsNullOrEmpty(msisdn))
                return new clsMesazh(false, "Ky MSISDN nuk eshte perfitues i nje promocioni!");
            if (statusPerdorimi)
                return new clsMesazh(false, "Ky MSISDN e ka perdorur kodin e tij te promocionit");

            return new clsMesazh(true);
        }

        private clsMesazh validoFormatMSISDN(string msisdn)
        {
            if (string.IsNullOrEmpty(msisdn))
                return new clsMesazh(false, "Ky MSISDN nuk eshte perfitues i nje promocioni!");

            if (!msisdn.StartsWith("355"))
                return new clsMesazh(false, "Fusha e numrit te telefonit, MSISDN e re, nuk eshte ne formatin e duhur! Duhet te nis me 355!");

            if (!Regex.Match(msisdn, @"^[0-9]{12}$").Success)
                return new clsMesazh(false, "Fusha e numrit te telefonit, MSISDN e re, nuk eshte ne formatin e duhur! Duhet te permbaje vetem karaktere numerik dhe te pasohet nga 9 shifra pas 355!");

            return new clsMesazh(true, "Sintaksa e MSISDN te re u validua me sukses!");
        }
        public clsMesazh ValidoMsisdnTeRe()
        {

            return validoFormatMSISDN(msisdnERe);
        }

        public void Mbush(IDataRecord record)
        {
            idKlientMeMSISDN = int.Parse(record["IDKLIENTMEMSISDN"].ToString());
            msisdn = record["MSISDN"].ToString();
            kodiFitues = record["KODIFITUES"].ToString();
            statusPerdorimi = bool.Parse(record["STATUSPERDORIMI"].ToString());
            Enum.TryParse(record["LLOJI"].ToString(), out lloji);
            msisdnERe = record["MSISDNERE"].ToString();
            DateTime.TryParse(record["DTDOK"].ToString(), out dtDok);
            int.TryParse(record["IDNDERMARRJE"].ToString(), out idNdermarrje);
            int.TryParse(record["IDPERDORUES"].ToString(), out idPerdoruesi);
            int.TryParse(record["IDMAG"].ToString(), out idMag);
        }

        #endregion



    }
}