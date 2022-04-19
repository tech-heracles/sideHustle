using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbInventari
{
    public class clsSerialeUnike : IDataBase
    {
        #region Atribute

        private int id;
        private string kodi;
        private string emertimi;
        private enumSerialeUnike_Lloje llojSeriali;
        private string nrKaraktere;
        private string formuleSpecifike;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private bool aktiv;
        private bool serialKryesor;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos kategorine e serialit unik.
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos emertimin e serialit unik.
        /// </summary>
        public string Emertimi
        {
            get { return emertimi; }
            set { emertimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos llojin e serialit unik.
        /// </summary>
        public enumSerialeUnike_Lloje LlojSeriali
        {
            get { return llojSeriali; }
            set { llojSeriali = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr karaktere te serialit unik.
        /// </summary>
        public string NrKaraktere
        {
            get { return nrKaraktere; }
            set { nrKaraktere = value; }
        }

        /// <summary>
        /// Kthen/Vendos formulen specifike te serialit unik.
        /// </summary>
        public string FormuleSpecifike
        {
            get { return formuleSpecifike; }
            set { formuleSpecifike = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
        }

        public bool Aktiv
        {
            get
            {
                return aktiv;
            }

            set
            {
                aktiv = value;
            }
        }

        public bool SerialKryesor
        {
            get
            {
                return serialKryesor;
            }

            set
            {
                serialKryesor = value;
            }
        }

        #endregion

        #region Konstruktore

        public clsSerialeUnike()
        {

        }

        public clsSerialeUnike(string kodi, int idNdermarje)
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                serialUnike.merrSerialinUnik(kodi, idNdermarje, this);
        }
        public clsSerialeUnike(int id)
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                serialUnike.mbushSerialeSipasID(id, this);
        }

        public clsSerialeUnike(string kodi, string emertimi, enumSerialeUnike_Lloje llojSeriali, string nrKaraktere, string formuleSpecifike, int idPerdoruesi, int idNdermarje, int idStatusDok, bool aktiv , bool serialKryesor)
        {
            this.kodi = kodi;
            this.emertimi = emertimi;
            this.llojSeriali = llojSeriali;
            this.nrKaraktere = nrKaraktere;
            this.formuleSpecifike = formuleSpecifike;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idStatusDok;
            this.aktiv = aktiv;
            this.serialKryesor = serialKryesor;
        }

        public clsSerialeUnike(IDataRecord rreshti)
        {
            Mbush(rreshti);
        }

        #endregion

        #region Metoda Publike

        public clsMesazh Ruaj()
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (var scope = new MyTransactionScope())
            {
                if (ekziston())
                    return new MesazhGabimi("Ekziston nje serial me kete kod. Ju lutem shenoni nje tjeter!");
                mesazh = RuajNeDB();
                if (mesazh) scope.Complete();
            }
            return mesazh;

        }

        public clsMesazh Modifiko()
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (var scope = new MyTransactionScope())
            {
                mesazh = ModifikoNeDB();
                if (mesazh) scope.Complete();
            }
            return mesazh;
        }

        public clsMesazh Fshi()
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (var scope = new MyTransactionScope())
            {
                mesazh = FshiNeDB();
                if (mesazh) scope.Complete();
            }
            return mesazh;
        }

        public void Mbush(IDataRecord record)
        {
            int.TryParse(record["ID"].ToString(), out id);
            kodi = record["KODI"].ToString();
            emertimi = record["EMERTIMI"].ToString();
            llojSeriali = (enumSerialeUnike_Lloje)int.Parse(record["IDLLOJ_SERIALE"].ToString());
            nrKaraktere = record["NR_KARAKTERE"].ToString();
            formuleSpecifike = record["FORMULE_SPECIFIKE"].ToString();
            int.TryParse(record["IDNDERMARJE"].ToString(), out idNdermarje);
            int.TryParse(record["IDPERDORUES"].ToString(), out idPerdoruesi);
            int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
            bool.TryParse(record["aktiv"].ToString(), out aktiv);
            bool.TryParse(record["serial_kryesor"].ToString(), out serialKryesor);
            DateTime.TryParse(record["DTKRIJIMI"].ToString(), out dtKrijimi);
            DateTime.TryParse(record["DTMODIFIKIMI"].ToString(), out dtModifikimi);
        }

        public bool ekziston()
        {
            clsSerialeUnike serialiUnik = new clsSerialeUnike(kodi, idNdermarje);
            return serialiUnik.ID != 0 ? true : false;
        }

        public bool Validate(string serial)
        {
            if (!kontrolloGjatesiKarakteresh(serial))
                return false;
            if (!kontrolloLlojSeriali(serial))
                return false;

            return true;

        }

        #endregion

        #region Metoda Internal

        // Me perpara ruhej lidhja e Serialit me Kategorine e serialeve. Tani eshte kthyer qe te ruhet lidhja e Serialit me Formatin e serialeve.
        internal clsMesazh RuajLidhje(int idFormatSerialesh)
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                return serialUnike.ruajLidhjeSerialeUnikeFormat(id, idFormatSerialesh);
        }

        #endregion

        #region Metoda Private

        private clsMesazh RuajNeDB()
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                return serialUnike.ruajSerialeUnike(out id, kodi, emertimi, (int)llojSeriali, nrKaraktere, formuleSpecifike, idNdermarje, idPerdoruesi, idStatusDok, aktiv, serialKryesor);
        }

        private clsMesazh ModifikoNeDB()
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                return serialUnike.modifikoSerialeUnike(id, emertimi, (int)llojSeriali, nrKaraktere, formuleSpecifike, idPerdoruesi, idStatusDok, aktiv, serialKryesor);
        }

        private clsMesazh FshiNeDB()
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                return serialUnike.fshiSerialeUnike(id);
        }


        private bool kontrolloGjatesiKarakteresh(string seriali)
        {
            
            DataTable dt = new DataTable();
            try
            {
                var result = dt.Compute($"{seriali.Length} {this.NrKaraktere}", "");
                return (bool)result;
            }catch(Exception ex)
            {
                ImbLogger.Error(ex, $"Deshtoi verifikimi i serialit {seriali}");
                return true;
            }
        }

        private bool kontrolloLlojSeriali(string seriali)
        {
            switch (this.llojSeriali)
            {
                case enumSerialeUnike_Lloje.PA_KUFIZIM:
                    return true;
                case enumSerialeUnike_Lloje.NUMERIKE:
                    double nr;
                    return double.TryParse(seriali, out nr);
                case enumSerialeUnike_Lloje.ALFANUMERIKE:
                    return true;
                default:
                    return true;
            }
        }
        #endregion
    }
}
