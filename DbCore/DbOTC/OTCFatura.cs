using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbOTC
{
    public class OTCFatura : IDataBaseReader, IComparable<OTCFatura>
    {
        #region ATRIBUTE
        private int _idFatura;
        private string _nrFature;
        private string _nrSerial;
        private string _nrKontrate;
        private string _emertimiKlientit;
        private decimal _vleraFillestareFatures;
        private decimal _interesi;
        private string _muajiFatures;
        private string _vitiFatures;
        private DateTime _dtFatures;
        private int _idPagesa;
        private OTCLlojSherbimi _llojFature;
        //private UnpaidBills bill;
        private LlojFatureOSHEE _llojFatureOSHEE;
        #endregion
        #region PROPERTIES
        public int IdFatura
        {
            get { return _idFatura; }
            set { _idFatura = value; }
        }

        public string NrFature
        {
            get { return _nrFature; }
            set { _nrFature = value; }
        }

        public string NrSerial
        {
            get { return _nrSerial; }
            set { _nrSerial = value; }
        }

        public string NrKontrate
        {
            get { return _nrKontrate; }
            set { _nrKontrate = value; }
        }

        public string EmertimiKlientit
        {
            get { return _emertimiKlientit; }
            set { _emertimiKlientit = value; }
        }

        public string KodKlienti { get; set; }

        public decimal VleraFillestareFatures
        {
            get { return _vleraFillestareFatures; }
            set { _vleraFillestareFatures = value; }
        }

        public decimal Interesi
        {
            get { return _interesi; }
            set { _interesi = value; }
        }

        public string MuajiFatures
        {
            get { return _muajiFatures; }
            set { _muajiFatures = value; }
        }

        public string VitiFatures
        {
            get { return _vitiFatures; }
            set { _vitiFatures = value; }
        }

        public DateTime DtFatures
        {
            get { return _dtFatures; }
            set { _dtFatures = value; }
        }

        public int IdPagesa
        {
            get { return _idPagesa; }
            set { _idPagesa = value; }
        }

        public OTCLlojSherbimi llojFature
        {
            get { return _llojFature; }
            set { _llojFature = value; }
        }

        public LlojFatureOSHEE LlojFatureOSHEE
        {
            get
            {
                return _llojFatureOSHEE;
            }

            set
            {
                this._llojFatureOSHEE = value;
            }
        }

        #endregion

        public OTCFatura()
        {

        }

        public OTCFatura(IDataRecord record)
        {

            Mbush(record);
        }

        public clsMesazh Ruaj()
        {

            using (var db = new clsDatabaseOTC())
            {
                IdFatura = db.RuajFature(NrFature, NrSerial, NrKontrate, EmertimiKlientit, KodKlienti, VleraFillestareFatures, Interesi, MuajiFatures, VitiFatures, DtFatures, IdPagesa, LlojFatureOSHEE);
            }
            return new clsMesazh(true, "Fatura u ruajt me sukses!");
        }

        public clsMesazh EkzistonFaturaSipasNrSerial()
        {
            using (clsDatabaseOTC dbOTC = new clsDatabaseOTC())
            {
                return dbOTC.ekzistonFaturaSipasNrSerial(NrFature, (int)llojFature);
            }
        }

        public void Mbush(IDataRecord record)
        {

            NrFature = record["NrFature"]?.ToString();
            NrSerial = record["NrSerial"]?.ToString();
            NrKontrate = record["NrKontrate"]?.ToString();
            EmertimiKlientit = record["EmertimiKlientit"]?.ToString();
            KodKlienti = record["KodKlienti"]?.ToString();
            decimal.TryParse(record["VleraFillestareFatures"]?.ToString(), out _vleraFillestareFatures);
            decimal.TryParse(record["Interesi"]?.ToString(), out _interesi);
            MuajiFatures = record["MuajiFatures"]?.ToString();
            VitiFatures = record["VitiFatures"]?.ToString();
            DateTime.TryParse(record["DtFatures"]?.ToString(), out _dtFatures);
            int.TryParse(record["IdPagesa"]?.ToString(), out _idPagesa);
            int.TryParse(record["IdFatura"]?.ToString(), out _idFatura);
            Enum.TryParse(record["LLOJFATURE"].ToString(), out _llojFature);
            Enum.TryParse(record["LLOJFATUREOSHEE"].ToString(), out _llojFatureOSHEE);
        }

        private string MerrNrKontrateOshee()
        {
            if ((KodKlienti ?? "").Length < 6) return "";

            return KodKlienti.Substring(KodKlienti.Length - 6);
        }
        
        /// <summary>
        /// Renditja Duhet te behet fillimisht sipas llojit te fatures, ku prioritet marrin Akt Marreveshjet, dhe pas marreveshjes duhet renditur
        /// sipas dates se fatures ne rendin zbrites
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(OTCFatura other)
        {
            int krahasoLlojFature = other._llojFatureOSHEE - _llojFatureOSHEE;

            if (krahasoLlojFature != 0)
                return krahasoLlojFature;

            if (VitiFatures != other.VitiFatures)
                return Convert.ToInt32(other.VitiFatures) - Convert.ToInt32(VitiFatures);

            return other.merrIndexMuaji() - merrIndexMuaji();
        }


        private int merrIndexMuaji()
        {
            switch (this.MuajiFatures.ToUpper())
            {
                case "JAN": return 1;
                case "SHK": return 2;
                case "MAR": return 3;
                case "PRI": return 4;
                case "MAJ": return 5;
                case "QER": return 6;
                case "KOR": return 7;
                case "GUS": return 8;
                case "SHT": return 9;
                case "TET": return 10;
                case "NEN": return 11;
                case "DHJ": return 12;
                default: return 0;
            }
        }
    }
}
