using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne taksat e fletes doganore
    ///  (Te dhenat  merren nga tabela : T_FLETEDOGANORETAKSA)
    /// </summary>
    public class clsFleteDoganoreTaksa
    {
        #region Atribute

        private int idFleteDoganoreTaksa;
        private int idFleteDoganoreKoka;        
        private int idTaksa;
        private string pershkrimi;  
        private Decimal vlefta;
        private bool tvsh;
        private string kodTaksa;
        private string llogariDebi;
        private string llogariKredi;
        private string normaTaksa;
        private DataRow rreshti;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsFleteDoganoreTaksa() 
        { 
        }
        public clsFleteDoganoreTaksa(object kodi, object pershkrimi, object vlera, object tvsh, object norma, int idndermarje)
        {
            if (kodi != null && kodi.ToString() != "null" && kodi.ToString() != "")
            {
                KodTaksa = kodi.ToString();
                Pershkrimi = pershkrimi.ToString();
                Vlefta = Convert.ToDecimal(vlera.ToString());
                Tvsh = Convert.ToBoolean(tvsh.ToString());
                NormaTaksa = norma.ToString();
                if (new clsTaksa(KodTaksa,idndermarje) != null)
                {
                    IdTaksa = new clsTaksa(KodTaksa,idndermarje).IdTaksa;
                   
                }
            }
            else IdTaksa = -1;
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e fletes doganore</param>
        public clsFleteDoganoreTaksa(int id)
        {
            clsDatabaseRegjistrim dbFleteDoganoreTaksa = new clsDatabaseRegjistrim();
            mbushFleteDoganoreTaksa(dbFleteDoganoreTaksa.ktheFleteDoganoreTaksaSipasId(id));
            dbFleteDoganoreTaksa.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idFDK"> id e kokes se fletes doganore me te cilen lidhet</param>
        /// <param name="idFDT"> id ritese e takses se fletes doganore</param>
        /// <param name="idTak">id e takses me te cilen lidhet</param>
        /// <param name="pershk">pershkrimi</param>
        /// <param name="t"> tvsh</param>
        /// <param name="vl"> vlefta</param>
        public clsFleteDoganoreTaksa(int idFDT, int idFDK, int idTak, string pershk, decimal vl, bool t, string k )
        {
            idFleteDoganoreTaksa = idFDT;
            idFleteDoganoreKoka = idFDK;
            idTaksa = idTak;
            pershkrimi = pershk;
            vlefta = vl;
            tvsh = t;
            kodTaksa = k;
        }

        public clsFleteDoganoreTaksa(DataRow rreshti)
        {
            
            mbushFleteDoganoreTaksa(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdFleteDoganoreTaksa
        {
            get { return idFleteDoganoreTaksa; }
            set { idFleteDoganoreTaksa = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e kokes se fletes doganore.
        /// </summary>
        public int IdFleteDoganoreKoka
        {
            get { return idFleteDoganoreKoka; }
            set { idFleteDoganoreKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e takses.
        /// </summary>
        public int IdTaksa
        {
            get { return idTaksa; }
            set { idTaksa = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi.
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimi.
        /// </summary>
        public String NormaTaksa
        {
            get { return normaTaksa; }
            set { normaTaksa = value; }
        }
        /// <summary>
        /// Kthen/Vendos vlefta.
        /// </summary>
        public Decimal Vlefta
        {
            get { return vlefta; }
            set { vlefta = value; }
        }

        /// <summary>
        /// Kthen/Vendos tvsh.
        /// </summary>
        public Boolean Tvsh
        {
            get { return tvsh; }
            set { tvsh = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e takses.
        /// </summary>
        public String KodTaksa
        {
            get { return kodTaksa; }
            set { kodTaksa = value; }
        }

        public String LlogariKredi
        {
            get { return llogariKredi; }
            set { llogariKredi = value; }
        }

        public String LlogariDebi
        {
            get { return llogariDebi; }
            set { llogariDebi = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e  takses se fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajFleteDoganoreTaksa"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            int id;
            clsMesazh u_ruajt = data.ruajFleteDoganoreTaksa(out id, this.IdFleteDoganoreKoka, this.IdTaksa, this.Pershkrimi, this.Vlefta, this.Tvsh);
            this.IdFleteDoganoreTaksa = id;
            data.Dispose();
            return u_ruajt;
        }

        /// </summary>
        /// Modifikon objektin e  takses se fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoFleteDoganoreTaksa"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoFleteDoganoreTaksa(this.IdFleteDoganoreTaksa, this.IdFleteDoganoreKoka, this.IdTaksa, this.Pershkrimi, this.Vlefta, this.Tvsh);
            data.Dispose();
            return u_modifikua;
        }

        /// </summary>
        /// Fshin objektin e  takses se fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiFleteDoganoreTaksaSipasId"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiFleteDoganoreTaksaSipasId(this.IdFleteDoganoreTaksa);
            data.Dispose();
            return u_fshi;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush taksa flete doganore nga databaza
        /// </summary>
        /// <param name="dbDataRowFleteDoganoreTaksa">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushFleteDoganoreTaksa(DataRow dbDataRowFleteDoganoreTaksa)
        {
            if (dbDataRowFleteDoganoreTaksa != null)
            {
                try
                {
                    int.TryParse(dbDataRowFleteDoganoreTaksa["IDFLETEDOGANORETAKSA"].ToString(), out idFleteDoganoreTaksa);
                    int.TryParse(dbDataRowFleteDoganoreTaksa["IDFLETEDOGANORE"].ToString(), out idFleteDoganoreKoka);
                    int.TryParse(dbDataRowFleteDoganoreTaksa["IDTAKSA"].ToString(), out idTaksa);
                    pershkrimi = dbDataRowFleteDoganoreTaksa["PERSHKRIMI"].ToString();
                    Decimal.TryParse(dbDataRowFleteDoganoreTaksa["VLEFTA"].ToString(), out vlefta);
                    Boolean.TryParse(dbDataRowFleteDoganoreTaksa["TVSH"].ToString(), out tvsh);
                    clsTaksa taksa = new clsTaksa(idTaksa);
                    kodTaksa =taksa.KodTaksa;
                    normaTaksa = taksa.NormaPerqindje.ToString();
                    llogariDebi = "632";
                    llogariKredi = "447";
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se takses se fleteve Doganore nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
