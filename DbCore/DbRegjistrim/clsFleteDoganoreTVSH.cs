using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
    {
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne TVSHt e fletes doganore
    ///  (Te dhenat  merren nga tabela : T_FLETEDOGANORETVSH)
    /// </summary>
    public class clsFleteDoganoreTVSH
        {
        #region Atribute

        private int idFleteDoganoreTVSH;
        private int idFleteDoganoreKoka;
        private int idTaksa;
        private string pershkrimi;
        private Decimal vleftaFaturuar;
        private decimal vleftaTvsh;
        private string kodTVSH;
        private string normaTVSH;
        private bool aqt;
        private DataRow rreshti;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsFleteDoganoreTVSH()
            {
            }
        public clsFleteDoganoreTVSH(object kodi, object pershkrimi, object vlera, object vleftatvsh, object norma, int idndermarje, object aqt)
        {
            if (kodi!= null && kodi.ToString() != "null" && kodi.ToString() != "")
            {
                KodTVSH = kodi.ToString();
                Pershkrimi = pershkrimi.ToString();
                VleftaFaturuar = Convert.ToDecimal(vlera.ToString());
                VleftaTvsh = Convert.ToDecimal(vleftatvsh.ToString());
                if (aqt.ToString() == "Unchecked")
                    this.aqt = false;
                else this.aqt = true;
                normaTVSH = norma.ToString();

                if (new clsTaksa(KodTVSH, idndermarje) != null)
                {
                    IdTaksa = new clsTaksa(KodTVSH, idndermarje).IdTaksa;
                }
            }
           
            else IdTaksa = -1;
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e fletes doganore</param>
        public clsFleteDoganoreTVSH(int id)
            {
            clsDatabaseRegjistrim dbFleteDoganoreTVSH = new clsDatabaseRegjistrim();
            mbushFleteDoganoreTVSH(dbFleteDoganoreTVSH.ktheFleteDoganoreTVSHSipasId(id));
            dbFleteDoganoreTVSH.Dispose();
            }

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idFDK"> id e kokes se fletes doganore me te cilen lidhet</param>
        /// <param name="idFDT"> id ritese e takses se fletes doganore</param>
        /// <param name="idTak">id e takses me te cilen lidhet</param>
        /// <param name="pershk">pershkrimi</param>
        /// <param name="t"> vlefta tvsh</param>
        /// <param name="vl"> vlefta faturuar</param>
        public clsFleteDoganoreTVSH(int idFDT, int idFDK, int idTak, string pershk, decimal vl, decimal t, string k, bool aqt)
            {
            idFleteDoganoreTVSH = idFDT;
            idFleteDoganoreKoka = idFDK;
            idTaksa = idTak;
            pershkrimi = pershk;
            vleftaFaturuar = vl;
            vleftaTvsh = t;
            kodTVSH = k;
            this.aqt = aqt;
            }

        public clsFleteDoganoreTVSH(DataRow rreshti)
        {
            
            mbushFleteDoganoreTVSH(rreshti);
        }

        #endregion

        #region Properties
        /// <summary>
        /// tregon nqs tvsh eshte per artikuj aqt apo jo
        /// </summary>
        public bool Aqt
        {
            get
            {
                return aqt;
            }
            set
            {
                aqt = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdFleteDoganoreTVSH
            {
            get { return idFleteDoganoreTVSH; }
            set { idFleteDoganoreTVSH = value; }
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
        public String NormaTVSH
            {
            get { return normaTVSH; }
            set { normaTVSH = value; }
            }
        /// <summary>
        /// Kthen/Vendos vlefta.
        /// </summary>
        public Decimal VleftaFaturuar
            {
            get { return vleftaFaturuar; }
            set { vleftaFaturuar = value; }
            }

        /// <summary>
        /// Kthen/Vendos tvsh.
        /// </summary>
        public decimal VleftaTvsh
            {
            get { return vleftaTvsh; }
            set { vleftaTvsh = value; }
            }

        /// <summary>
        /// Kthen/Vendos kodin e takses.
        /// </summary>
        public String KodTVSH
            {
            get { return kodTVSH; }
            set { kodTVSH = value; }
            }

        
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e  takses se fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajFleteDoganoreTVSH"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            int id;
            clsMesazh u_ruajt = data.ruajFleteDoganoreTVSH(out id, this.IdFleteDoganoreKoka, this.IdTaksa, this.Pershkrimi, this.VleftaFaturuar, this.VleftaTvsh, this.aqt);
            this.IdFleteDoganoreTVSH = id;
            data.Dispose();
            return u_ruajt;
        }

        /// </summary>
        /// Modifikon objektin e  takses se fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoFleteDoganoreTVSH"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
            {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoFleteDoganoreTVSH(this.IdFleteDoganoreTVSH, this.IdFleteDoganoreKoka, this.IdTaksa, this.Pershkrimi, this.VleftaFaturuar, this.VleftaTvsh, this.aqt);
            data.Dispose();
            return u_modifikua;
            }

        /// </summary>
        /// Fshin objektin e  takses se fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiFleteDoganoreTVSHSipasId"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
            {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiFleteDoganoreTVSHSipasId(this.IdFleteDoganoreTVSH);
            data.Dispose();
            return u_fshi;
            }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush TVSH flete doganore nga databaza
        /// </summary>
        /// <param name="dbDataRowFleteDoganoreTVSH">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushFleteDoganoreTVSH(DataRow dbDataRowFleteDoganoreTVSH)
            {
            if (dbDataRowFleteDoganoreTVSH != null)
                {
                try
                    {
                    int.TryParse(dbDataRowFleteDoganoreTVSH["IDFLETEDOGANORETVSH"].ToString(), out idFleteDoganoreTVSH);
                    int.TryParse(dbDataRowFleteDoganoreTVSH["IDFLETEDOGANORE"].ToString(), out idFleteDoganoreKoka);
                    int.TryParse(dbDataRowFleteDoganoreTVSH["IDTAKSA"].ToString(), out idTaksa);
                    pershkrimi = dbDataRowFleteDoganoreTVSH["PERSHKRIMI"].ToString();
                    Decimal.TryParse(dbDataRowFleteDoganoreTVSH["VLEFTAFATURUAR"].ToString(), out vleftaFaturuar);
                    Decimal.TryParse(dbDataRowFleteDoganoreTVSH["VLEFTATVSH"].ToString(), out vleftaTvsh);
                    Boolean.TryParse(dbDataRowFleteDoganoreTVSH["AQT"].ToString(), out aqt);
                    clsTaksa taksa = new clsTaksa(idTaksa);
                    kodTVSH = taksa.KodTaksa;
                    normaTVSH = taksa.NormaPerqindje.ToString();
                   
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
