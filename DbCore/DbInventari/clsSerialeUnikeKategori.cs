using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.DataBase;
using DbCore.DbAdmin;
using System.Text.RegularExpressions;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{
    public class clsSerialeUnikeKategori : IDataBase
    {
        #region Atribute

        private int id;
        private string kategori;
        private string pershkrimi;
        private string tipFormati;
        private string simboliNdares;
        private int idPerdoruesi;
        private int idNdermarje;
        private bool meEmertimKolone;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colSerialeUnikeFormate colFormateSeriali;
        private colSerialeUnikeFusha colSerialeFusha;
        private string konfigurimeFtp;

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
        public string Kategori
        {
            get { return kategori; }
            set { kategori = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e serialit unik.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos tip e serialit unik.
        /// </summary>
        public string TipFormati
        {
            get { return tipFormati; }
            set { tipFormati = value; }
        }

        /// <summary>
        /// Kthen/Vendos simbolin ndares te serialit unik.
        /// </summary>
        public string SimboliNdares
        {
            get { return simboliNdares; }
            set { simboliNdares = value; }
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

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
        }

        public colSerialeUnikeFormate ColFormateSeriali {
            get
            {
                if (colFormateSeriali == null)
                    colFormateSeriali = new colSerialeUnikeFormate();
                return colFormateSeriali;
            }
            set { colFormateSeriali = value; }
        }

        public colSerialeUnikeFusha ColSerialeFusha
        {
            get { if (colSerialeFusha == null) colSerialeFusha = new colSerialeUnikeFusha();
                return colSerialeFusha;
            }
            set { colSerialeFusha = value; }
        }

        public int IdStatusDok
        {
            get
            {
                return idStatusDok;
            }

            set
            {
                idStatusDok = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos konfigurimet ftp te kategorise se serialeve.
        /// </summary>
        public string KonfigurimeFtp {
            get
            {
                return konfigurimeFtp;
            }
            set
            {
                konfigurimeFtp = value;
            }
        }

        public bool MeEmertimKolone
        {
            get
            {
                return meEmertimKolone;
            }
            set
            {
                meEmertimKolone = value;
            }
        }
        #endregion

        #region Konstruktor

        public clsSerialeUnikeKategori()
        {
            this.colFormateSeriali = new colSerialeUnikeFormate();
            this.colSerialeFusha = new colSerialeUnikeFusha();
        }
        public clsSerialeUnikeKategori(int id)
        {
            using (clsDatabaseInventari serialUnike = new clsDatabaseInventari())
                serialUnike.mbushSerialeKategoriSipasID(id, this);
        }
        public clsSerialeUnikeKategori(string kategori, string pershkrimi, string tipFormati, string simboliNdares, int idPerdoruesi, int idNdermarje, int idstatusdok, string konfigurimeFtp, bool meEmertimKolone)
        {
            this.kategori = kategori;
            this.pershkrimi = pershkrimi;
            this.tipFormati = tipFormati;
            this.simboliNdares = simboliNdares;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idstatusdok;
            this.colFormateSeriali = new colSerialeUnikeFormate();
            this.colSerialeFusha = new colSerialeUnikeFusha();
            this.konfigurimeFtp = konfigurimeFtp;
            this.MeEmertimKolone = meEmertimKolone;
        }

        public clsSerialeUnikeKategori(IDataRecord rreshti)
        {
            Mbush(rreshti);
        }
        public clsSerialeUnikeKategori(string kategori, int idNdermarje, bool mbushFormate)
        {
            using (clsDatabaseInventari kategoriSerialUnik = new clsDatabaseInventari())
                kategoriSerialUnik.merrSerialeKategori(kategori, idNdermarje, this);

            if(mbushFormate)
                this.ColFormateSeriali.mbushSipasKategorise(this.ID);
        }

        public clsSerialeUnikeKategori(string kategori, int idNdermarje):this(kategori, idNdermarje, false)
        {
        }


        #endregion

        #region Metoda Publike

        public clsMesazh Ruaj()
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (var scope = new MyTransactionScope())
            {
                if (ekziston())
                    return new MesazhGabimi("Ekziston nje kategori me kete kod. Ju lutem shenoni nje tjeter!");
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
            kategori = record["KATEGORI"].ToString();
            pershkrimi = record["PERSHKRIM"].ToString();
            tipFormati = record["TIPFORMATI"].ToString();
            simboliNdares = record["SIMBOLI_NDARES"].ToString();
            bool.TryParse(record["MEEMERTIMKOLONE"].ToString(), out meEmertimKolone);
            int.TryParse(record["IDNDERMARJE"].ToString(), out idNdermarje);
            int.TryParse(record["IDPERDORUES"].ToString(), out idPerdoruesi);
            int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
            DateTime.TryParse(record["DTKRIJIMI"].ToString(), out dtKrijimi);
            DateTime.TryParse(record["DTMODIFIKIMI"].ToString(), out dtModifikimi);
            colFormateSeriali = new colSerialeUnikeFormate();
            colSerialeFusha = new colSerialeUnikeFusha();
        }

        public bool ekziston()
        {
            clsSerialeUnikeKategori serialiUnikKategori = new clsSerialeUnikeKategori(kategori, idNdermarje);
            return serialiUnikKategori.ID != 0 ? true : false;
        }

        public clsSerialeUnike GjejSerialinUnik(string serialiPerKontroll, out int idFormati)
        {
            foreach (clsSerialeUnikeFormate formati in this.ColFormateSeriali)
            {
                foreach (clsSerialeUnike seriali in formati.ColSeriale)
                {
                    if (seriali.Validate(serialiPerKontroll) && Regex.Match(serialiPerKontroll, seriali.FormuleSpecifike).Success)
                    {
                        idFormati = formati.ID;
                        return seriali;
                    }
                }
            }
            idFormati = 0;
            return null;
        }

        #endregion

        #region Metoda Private

        private clsMesazh RuajNeDB()
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (clsDatabaseInventari kategoriDb = new clsDatabaseInventari())
            {
                mesazh = kategoriDb.ruajSerialeUnikeKategori(out id, kategori, pershkrimi, tipFormati, simboliNdares, idNdermarje, idPerdoruesi, idStatusDok, meEmertimKolone);
                if (mesazh.Status)
                    mesazh = colSerialeFusha.Ruaj(id);
            }
            if (mesazh.Status)
                mesazh = clsSerialeunikeKategoriXKonfigurimeftp.ruajLidhjetKategoriKonfigurim(konfigurimeFtp, id, idNdermarje);
            return mesazh;
        }

        private clsMesazh ModifikoNeDB()
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (clsDatabaseInventari kategoriDb = new clsDatabaseInventari())
            {
                mesazh = kategoriDb.modifikoSerialeUnikeKategori(id, pershkrimi, tipFormati, simboliNdares, idPerdoruesi, meEmertimKolone);
                if (mesazh.Status)
                    mesazh = colSerialeFusha.Modifiko(id);
            }
            if (mesazh.Status)
                mesazh = clsSerialeunikeKategoriXKonfigurimeftp.modifikoLidhjetKategoriKonfigurim(konfigurimeFtp, id, idNdermarje);

            if (mesazh.Status)
                mesazh.PershkrimMesazhi = MessagesResource.Messages["msgModifikimiMeSukses"];
            return mesazh;
        }

        private clsMesazh FshiNeDB()
        {
            clsMesazh mesazh = new MesazhGabimi();
            using (clsDatabaseInventari kategoriDb = new clsDatabaseInventari())
            {
                mesazh = kategoriDb.FshiSerialeUnikeKategori(id);
            }
            return mesazh;
        }

        #endregion
    }
}
