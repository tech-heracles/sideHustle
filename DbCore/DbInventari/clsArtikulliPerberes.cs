using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.DbRegjistrim;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne artikujt perberes
    ///  (Te dhenat  merren nga tabela : T_ARTIKULLIPERBERES)
    /// </summary>

    public class clsArtikulliPerberes
    {
        #region Atribute

        private int idArtikulliPerberes;
        private int lloji;
        private int idArtikulliKryesor;
        private int idLidheseArt;
        private decimal koeficienti;
        private decimal scrap;
        private int idLidheseAkt;
        private bool gjithmoneNgaStoku;
        private DateTime dtNdryshimi;
        private string njesia;
        private int idImportTAbSQL;
        private DataRow rreshti;
        private float sasiSet;
        private float sasiPlotesuar;
        private int idMag;
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdArtikulliPerberes
        {
            get { return idArtikulliPerberes; }
            set { idArtikulliPerberes = value; }
        }
        /// <summary>
        /// Kthen/Vendos Llojin e artikullit.
        /// </summary>
        public int Lloji
        {
            get { return lloji; }
            set { lloji = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit kryesor
        /// </summary>
        public int IdArtikulliKryesor
        {
            get { return idArtikulliKryesor; }
            set { idArtikulliKryesor = value; }
        }
        /// <summary>
        /// Kthen/Vendos id-ne e artikullit perberes.
        /// </summary>
        public int IdLidheseArt
        {
            get { return idLidheseArt; }
            set { idLidheseArt = value; }
        }
        /// <summary>
        /// kthen vendos id e aktivitetit perberes
        /// </summary>
        public int IdLidheseAkt
        {
            get { return idLidheseAkt; }
            set { idLidheseAkt = value; }
        }
        /// <summary>
        /// Kthen/Vendos koeficientin.
        /// </summary>
        public decimal Koeficienti
        {
            get { return koeficienti; }
            set { koeficienti = value; }
        }
        /// <summary>
        /// Kthen/Vendos scrapin.
        /// </summary>
        public decimal Scrap
        {
            get { return scrap; }
            set { scrap = value; }
        }
        /// <summary>
        /// kthen vendos nese artikulli prodhim apo prodhim ne proces do prodhohet gjate procesit apo do merret nga inventari
        /// </summary>
        public bool GjithmoneNgaStoku
        {
            get
            {
                return gjithmoneNgaStoku;
            }
            set
            {
                gjithmoneNgaStoku = value;
            }
        }

        /// <summary>
        /// data kur aktivizohet kjo recepture
        /// </summary>
        public DateTime DtNdryshimi
        {
            get
            {
                return dtNdryshimi;
            }
            set
            {
                dtNdryshimi = value;
            }
        }

        public int IdLidhese
        {
            get
            {
                if (lloji == 1)
                    return idLidheseArt;
                else return idLidheseAkt;
            }
            set
            {
                if (lloji == 1)
                    idLidheseArt = value;
                else idLidheseAkt = value;
            }
        }

        public string Njesia
        {
            get
            {
                return njesia;
            }
            set
            {
                njesia = value;
            }
        }

        public int IdImportTAbSQL
        {
            get
            {
                return idImportTAbSQL;
            }
            set
            {
                idImportTAbSQL = value;
            }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// kontruktori me parametra
        /// </summary>
        /// <param name="id"> id  ritese e artikullit perberes</param>
        /// <param name="lloj">lloji i artikullit</param>
        /// <param name="idart"> id e artikullit kryesor</param>
        /// <param name="koef"> koeficienti</param>
        /// <param name="datederi">data deri kur eshte aktive receptura</param>
        /// <param name="dtndryshimi">data kur eshte aktive receptura</param>
        /// <param name="idlidhakt">id e aktivitetit perberes</param>
        /// <param name="idlidhart">id e artikullit perberes</param>
        /// <param name="ngastoku">nese artikullin prodhim ne proces do e marrim nga stoku apo do e prodhojme</param>
        /// <param name="sc">scrapi</param>
        public clsArtikulliPerberes(int lloj, int idart, int idlidhart, decimal koef, decimal sc, int idlidhakt, bool ngastoku, DateTime dtndryshimi, int idImportSQL)
        {
            lloji = lloj;
            idArtikulliKryesor = idart;
            idLidheseArt = idlidhart;
            koeficienti = koef;
            scrap = sc;
            idLidheseAkt = idlidhakt;
            gjithmoneNgaStoku = ngastoku;
            dtNdryshimi = dtndryshimi;
            njesia = "";
            IdImportTAbSQL = idImportSQL;
        }

        /// <summary>
        /// kontruktori pa parametra
        /// </summary>
        public clsArtikulliPerberes()
        {

        }

        public clsArtikulliPerberes(DataRow rreshti)
        {
            
            mbushArtikullPerberes(rreshti);
        }

        #endregion

        #region Metoda Publike
        public clsArtikulliPerberes krijoPerImport(string kodiartikulli, string lloji, string kodi, DateTime data, decimal koeficienti, decimal firo, int idndermarje, int idImportSQL)
        {
            try
            {
                clsArtikulli artkryesor = new clsArtikulli(kodiartikulli, idndermarje);
                
                int idArtikulliKryesor = artkryesor.IdArtikulli;
                if (artkryesor.IdArtikulli <= 0)
                    throw new Exception("Artikulli kryesor nuk ekziston!");
                if (artkryesor.Klasa < 4)
                    throw new Exception("Artikulli nuk i perket klasave prodhim, prodhim ne proces ose i perbere");

                if (lloji != "")
                {
                    if (lloji == "Artikull")
                        this.lloji = 1;
                    else if (lloji == "Aktivitete")
                        this.lloji = 2;
                    else throw new Exception("Lloj i pa identifikuar!");
                }
                if (artkryesor.Klasa == 4 && this.lloji == 2)
                    throw new Exception("Artikujt e perbere nuk mund te kene aktivitete per receptura!");

                if (this.lloji == 1)
                {
                    clsArtikulli art = new clsArtikulli(kodi, idndermarje);

                    if (art.Klasa == 4)
                        throw new Exception("Nuk lejohet artikulli i perbere: " + kodi + " ne recepture");

                    if (art.IdArtikulli <= 0)
                        throw new Exception("Artikulli i receptures " + kodi + " nuk ekziston!");
                    idLidheseArt = art.IdArtikulli;

                }
                else
                {
                    DbProdhimi.clsAktiviteteKoka akt = new DbProdhimi.clsAktiviteteKoka(kodi, idndermarje);
                    idLidheseAkt = akt.IdKoka;
                    if (akt.IdKoka <= 0)
                        throw new Exception("Aktiviteti i receptures " + kodi + " nuk ekziston!");
                }
                if (clsArtikulliPerberes.ekzistonArtikullPerberesPerKeteDate(idArtikulliKryesor, data, IdLidhese, this.lloji))
                    throw new Exception("Ekziston receptura " + kodi + " tek artikulli" + kodiartikulli);
                if (kodiartikulli == kodi)
                    throw new Exception("Nuk lejohet te vendoset vete artikulli si recepture!");
                this.IdImportTAbSQL = idImportSQL;
                return new clsArtikulliPerberes(this.lloji, idArtikulliKryesor, idLidheseArt, koeficienti, firo, idLidheseAkt, false, data, idImportSQL);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static bool ekzistonArtikullPerberesPerKeteDate(int id, DateTime data, int idlidhese, int lloji, clsDatabaseInventari db)
        {
            return db.ekzistonArtikullPerberesPerKeteDate(id, data, idlidhese, lloji);
        }
        public static bool ekzistonArtikullPerberesPerKeteDate(int id, DateTime data, int idlidhese, int lloji)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            return clsArtikulliPerberes.ekzistonArtikullPerberesPerKeteDate(id, data, idlidhese, lloji, db);
        }

        public float getsasiSet() { return sasiSet; }
        public void setsasiSet(float sasiSet) { this.sasiSet = sasiSet; }
        public float getsasiPlotesuar() { return sasiPlotesuar; }
        public void setsasiPlotesuar(float sasiPlotesuar) { this.sasiPlotesuar = sasiPlotesuar; }
        public void shtoSasiPlotesuar(float sasiPlotesuar) { this.sasiPlotesuar += sasiPlotesuar; }
        public bool eshtePlotesuarSasia() { return this.sasiSet * (float)this.koeficienti == this.sasiPlotesuar; }
        public float getSasiENevojshme() { return this.sasiSet * (float)this.koeficienti - this.sasiPlotesuar; }
        public int getIdMag() { return idMag; }
        public void setIdMag(string kodMag, int idNdermarrje) { this.idMag = clsNjesiAdministrative.ktheIdMagazine(kodMag, idNdermarrje); }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// metode per mbushjen e artikullit perberes nga databaza
        /// </summary>
        /// <param name="dbDataRowArtikullPerberes">merr nje datarow qe duhet mbushur me te dhena nga databaza</param>
        /// <returns>kthen true nese eshte i vertete, ne te kundert false</returns>
        internal bool mbushArtikullPerberes(DataRow dbDataRowArtikullPerberes)
        {
            if (dbDataRowArtikullPerberes != null)
            {

                try
                {
                    int.TryParse(dbDataRowArtikullPerberes["IDARTIKULLIPERBERES"].ToString(), out idArtikulliPerberes);
                    int.TryParse(dbDataRowArtikullPerberes["LLOJI"].ToString(), out lloji);
                    int.TryParse(dbDataRowArtikullPerberes["IDARTIKULLKYESOR"].ToString(), out idArtikulliKryesor);
                    int.TryParse(dbDataRowArtikullPerberes["IDLIDHESEART"].ToString(), out idLidheseArt);
                    decimal.TryParse(dbDataRowArtikullPerberes["KOEFICIENTI"].ToString(), out koeficienti);
                    decimal.TryParse(dbDataRowArtikullPerberes["SCRAP"].ToString(), out scrap);
                    int.TryParse(dbDataRowArtikullPerberes["IDLIDHESEAKT"].ToString(), out idLidheseAkt);
                    bool.TryParse(dbDataRowArtikullPerberes["GJITHMONENGASTOKU"].ToString(), out gjithmoneNgaStoku);
                    DateTime.TryParse(dbDataRowArtikullPerberes["DTNDRYSHIMI"].ToString(), out dtNdryshimi);
                    njesia = dbDataRowArtikullPerberes["NJESIA"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se artikullit perberes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
