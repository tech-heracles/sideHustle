using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbProdhimi
{
    /// <summary>
    /// kjo klase sherben per te mbushur griden e gjenerimit tek projekti i prodhimit 
    /// nuk perfaqeson ndonje tabele
    /// </summary>
    public class clsProjektProdhimi
    {
        #region Attributet
        /// <summary>
        /// id 
        /// </summary>
        private int id;
        /// <summary>
        /// id e kokes se urdherit te shitjes nga krijohet rreshti
        /// </summary>
        private int idKoka;
        /// <summary>
        /// id e artikullit
        /// </summary>
        private int idArtikulli;
        /// <summary>
        /// nr i projektit
        /// </summary>
        private string nrProjekti;
        /// <summary>
        /// pershkrimi i artikullit
        /// </summary>
        private string pershkrimArtikull;
        /// <summary>
        /// sasia aktuale
        /// </summary>
        private double sasiAktuale;
        /// <summary>
        /// sasia porositur
        /// </summary>
        private double sasiaPorositur;

        /// <summary>
        /// gjeresia e porositur
        /// </summary>
        private double gjeresiPorositur;

        /// <summary>
        /// gjatesi porositur
        /// </summary>
        private double gjatesiPorositur;
        /// <summary>
        /// data
        /// </summary>
        private DateTime data;
        /// <summary>
        /// id e klientit
        /// </summary>
        private int idKlienti;
        /// <summary>
        /// emeri i klientit
        /// </summary>
        private string emerKlienti;
        /// <summary>
        /// nr i urdherit te shitjes nga ka ardhur
        /// </summary>
        private string nrUrdherShitje;
       /// <summary>
       /// id e magazines
       /// </summary>
        private int idMag;
        /// <summary>
        /// id e njesise se artikullit
        /// </summary>
        private int idNjesi;
        /// <summary>
        /// kodi i artikullit
        /// </summary>
        private string kodArtikulli;
        /// <summary>
        /// Kod i klientit
        /// </summary>
        private string kodKlienti;
        /// <summary>
        /// shenimet
        /// </summary>
        private string shenime;
        /// <summary>
        /// sasia sipas permasave
        /// </summary>
        private double sasiPermase;
        /// <summary>
        /// id e trupit te shitjes
        /// </summary>
        private int idTrupiShitje;
        /// <summary>
        /// kodi i detajimit 1 te artikullit
        /// </summary>
        private string kodDetajim1;
        /// <summary>
        /// kodi i detajimit 2 te artikullit
        /// </summary>
        private string kodDetajim2;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="idkoka">id e urdher shitje</param>
        /// <param name="idArt">id e artikullit</param>
        /// <param name="nrprojekti">nr i projektit</param>
        /// <param name="pershkArt">pershkrim artikullit</param>
        /// <param name="sasiaktuale">sasi aktuale</param>
        /// <param name="sasiporositur">sasi porositur</param>
        /// <param name="data">data</param>
        /// <param name="idklienti">id e klientit</param>
        /// <param name="emerklienti">emri i klientit</param>
        /// <param name="nrurdhershitje">nr i urdherit te shitjes</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idnjesia">id e njesise</param>
        /// <param name="kodartikulli">kodi i artikullit</param>
        /// <param name="kodklienti">kod i klientit</param>
        public clsProjektProdhimi(int id, int idkoka, int idArt, string nrprojekti, string pershkArt, double sasiaktuale, double sasiporositur, DateTime data, int idklienti, string emerklienti, string nrurdhershitje, int idmag, int idnjesia, string kodartikulli, string kodklienti, double gjeresiporositur, double gjatesiporositur, double sasiPermase, string shenime, int idtrupi, string kodDetajim1, string kodDetajim2)
        {
            this.id = id;
            idKoka = idkoka;
            idArtikulli = idArt;
            nrProjekti = nrprojekti;
            pershkrimArtikull = pershkArt;
            sasiAktuale = sasiaktuale;
            sasiaPorositur = sasiporositur;
            this.data = data;
            idKlienti = idklienti;
            emerKlienti = emerklienti;
            nrUrdherShitje = nrurdhershitje;
            idMag = idmag;
            idNjesi = idnjesia;
            kodArtikulli = kodartikulli;
            kodKlienti = kodklienti;
            gjeresiPorositur = gjeresiporositur;
            idTrupiShitje = idtrupi;
            gjatesiPorositur = gjatesiporositur;
            this.sasiPermase = sasiPermase;
            this.shenime = shenime;
            this.kodDetajim1 = kodDetajim1;
            this.kodDetajim2 = kodDetajim2;
        }
        public clsProjektProdhimi( Dictionary<string, object> rreshtDokuKlient)
        {
            string nrprojekti = rreshtDokuKlient["txtNrProjekti"].ToString();
            if (nrprojekti == "")
                return;
            int idkoka = Convert.ToInt32(rreshtDokuKlient["txtIdKoka"]);
            int idtrupi = 0;
            int.TryParse(rreshtDokuKlient["txtIdTrupiShitje"].ToString(), out idtrupi);
            int idmag = 0;
            int.TryParse(rreshtDokuKlient["txtIdMag"].ToString(), out idmag);
            string kodart = rreshtDokuKlient["txtKodArtikulli"].ToString();
            string pershkrimart = rreshtDokuKlient["txtPershkrimArtikull"].ToString();
            string sasiakt = rreshtDokuKlient["txtSasiAktuale"].ToString();
            DateTime data = Convert.ToDateTime(rreshtDokuKlient["txtData"].ToString());
            string gjeresi = rreshtDokuKlient["txtGjeresiPorositur"].ToString();
            string gjatesi = rreshtDokuKlient["txtGjatesiPorositur"].ToString();
            string sasipor = rreshtDokuKlient["txtSasiaPorositur"].ToString();
            string sasipermasa = rreshtDokuKlient["txtSasiPermase"].ToString();
            string kodklienti = rreshtDokuKlient["txtKodKlienti"].ToString();
            string emerklienti = rreshtDokuKlient["txtEmerKlienti"].ToString();
            string nrurdher = rreshtDokuKlient["txtNrUrdherShitje"].ToString();
            string idnjesi = rreshtDokuKlient["txtIdNjesi"].ToString();
            string idart = rreshtDokuKlient["txtIdArtikulli"].ToString();
            string idklient = rreshtDokuKlient["txtIdKlienti"].ToString();
            string shenime = rreshtDokuKlient["txtShenime"].ToString();
            string kodDetajim1 = rreshtDokuKlient["txtDetajim1"].ToString();
            string kodDetajim2 = rreshtDokuKlient["txtDetajim2"].ToString();

            if (nrprojekti != "")
            {
                if (nrprojekti != null && nrprojekti != "null" && nrprojekti != "")
                {
                    
                    this.idTrupiShitje = idtrupi;
                    this.idKoka = idkoka;
                    this.nrProjekti = nrprojekti;
                    this.kodArtikulli = kodart;
                    this.pershkrimArtikull = pershkrimart;
                    this.idArtikulli = int.Parse(idart);
                    this.data = data;
                    this.emerKlienti = emerklienti;
                    this.idKlienti = int.Parse(idklient);
                    this.idMag = idmag;
                    this.idNjesi = int.Parse(idnjesi);
                    this.kodKlienti = kodklienti;
                    this.nrUrdherShitje = nrurdher;
                    this.shenime = shenime;
                    this.kodDetajim1 = kodDetajim1;
                    this.kodDetajim2 = kodDetajim2;
                    if (gjeresi != null && gjeresi != "null" && gjeresi != "")
                        this.gjeresiPorositur = double.Parse(gjeresi);
                    if (gjatesi != null && gjatesi != "null" && gjatesi != "")
                        this.gjatesiPorositur = double.Parse(gjatesi); 
                    if (sasipermasa != null && sasipermasa != "null" && sasipermasa != "")
                        this.sasiPermase = double.Parse(sasipermasa);
                    if (sasiakt != null && sasiakt != "null" && sasiakt != "")
                        this.sasiAktuale = double.Parse(sasiakt);
                    if (sasipor != null && sasipor != "null" && sasipor != "")
                        this.SasiaPorositur = double.Parse(sasipor);
                   
                }
            }            
        }
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsProjektProdhimi()
        {
        }



        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit te urdher shitjes.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit .
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikulli; }
            set { idArtikulli = value; }
        }

        /// <summary>
        /// id e trupit te shitjes
        /// </summary>
        public int IdTrupiShitje
        {
            get { return idTrupiShitje; }
            set { idTrupiShitje = value; }
        }

        /// <summary>
        /// Kthen/Vendos Nr i projektit.
        /// </summary>
        public String NrProjekti
        {
            get { return nrProjekti; }
            set { nrProjekti = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i artikullit.
        /// </summary>
        public String PershkrimArtikull
        {
            get { return pershkrimArtikull; }
            set { pershkrimArtikull = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e sasi aktuale.
        /// </summary>
        public double SasiAktuale
        {
            get { return sasiAktuale; }
            set { sasiAktuale = value; }
        }

        /// <summary>
        /// Kthen/Vendos sasia e porositur.
        /// </summary>
        public double SasiaPorositur
        {
            get { return sasiaPorositur; }
            set { sasiaPorositur = value; }
        }

        /// <summary>
        /// gjeresia e porositur
        /// </summary>
        public double GjeresiPorositur
        {
            get { return gjeresiPorositur; }
            set { gjeresiPorositur = value; }
        }
       
        /// <summary>
        /// gjatesi porositur
        /// </summary>
        public double GjatesiPorositur
        {
            get { return gjatesiPorositur; }
            set { gjatesiPorositur = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten.
        /// </summary>
        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// id e klientit
        /// </summary>
        public int IdKlienti
        {
            get { return idKlienti; }
            set { idKlienti = value; }
        }

        /// <summary>
        /// emeri i klientit
        /// </summary>
        public string EmerKlienti
        {
            get { return emerKlienti; }
            set { emerKlienti = value; }
        }

        /// <summary>
        /// nr i urdherit te shitjes nga ka ardhur
        /// </summary>
        public string NrUrdherShitje
        {
            get { return nrUrdherShitje; }
            set { nrUrdherShitje = value; }
        }

        /// <summary>
        /// id e magazines
        /// </summary>
        public int IdMag
        {
            get { return idMag; }
            set { idMag = value; }
        }

        /// <summary>
        /// id e njesise se artikullit
        /// </summary>
        public int IdNjesi
        {
            get { return idNjesi; }
            set { idNjesi = value; }
        }

        /// <summary>
        /// kodi i artikullit
        /// </summary>
        public string KodArtikulli
        {
            get { return kodArtikulli; }
            set { kodArtikulli = value; }
        }

        /// <summary>
        /// Kod i klientit
        /// </summary>
        public string KodKlienti
        {
            get { return kodKlienti; }
            set { kodKlienti = value; }
        }

        /// <summary>
        /// shenime
        /// </summary>
        public string Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

        /// <summary>
        /// Kthen/Vendos sasine sipas permasave.
        /// </summary>
        public double SasiPermase
        {
            get { return sasiPermase; }
            set { sasiPermase = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e detajimit 1 te artikullit
        /// </summary>
        public string KodDetajim1
        {
            get { return kodDetajim1; }
            set { kodDetajim1 = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e detajimit 2 te artikullit
        /// </summary>
        public string KodDetajim2
        {
            get { return kodDetajim2; }
            set { kodDetajim2 = value; }
        }
        #endregion
    }
}
