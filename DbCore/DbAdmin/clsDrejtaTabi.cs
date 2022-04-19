using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.SessionState;
using AlphaWeb.Core.Interfaces.Data;
using CacheLayer;
using DbCore.IMBUtils.Cache;

namespace DbCore.DbAdmin
{
    public class clsDrejtaTabi : IDataBaseReader
    {

        #region Atributet
        //public const string mySessionKey = "clsDrejtaTabi";

        private int idDrejta;
        private int idPeme; //id qe i vendoset te drejtave per krijimin e pemes
        private Nullable<Int32> idPrindi;
        private string tabi;
        private bool dPlot;
        private bool dMod;
        private bool dFsh;
        private bool dAmb;
        private bool dShtim;
        private bool dGjitheDok;
        private bool eshteGjethe;
        private bool expanded = false; //perdoret per krijimin e pemes
        private int niveli;
        private bool loaded = false; //perdoret per krijimin e pemes
        private int idRoli;
        private int idNdermarrje;
        private int idViti;
        private int idPerdorues;

        #endregion

        #region Properties

        public int IdDrejta
        {
            get
            {
                return idDrejta;
            }
            set
            {
                if (idDrejta == value)
                    return;
                idDrejta = value;
            }
        }

        public int IdPeme
        {
            get
            {
                return idPeme;
            }
            set
            {
                if (idPeme == value)
                    return;
                idPeme = value;
            }
        }

        public Nullable<Int32> IdPrindi
        {
            get
            {
                return idPrindi;
            }
            set
            {
                if (idPrindi == value)
                    return;
                idPrindi = value;
            }
        }

        public string Tabi
        {
            get
            {
                return tabi;
            }
            set
            {
                tabi = value;
            }
        }

        public int Niveli
        {
            get
            {
                return niveli;
            }
            set
            {
                niveli = value;
            }
        }

        public bool EshteGjethe
        {
            get
            {
                return eshteGjethe;
            }
            set
            {
                if (eshteGjethe == value)
                    return;
                eshteGjethe = value;
            }
        }

        public bool Expanded
        {
            get
            {
                return expanded;
            }
            set
            {
                if (expanded == value)
                    return;
                expanded = value;
            }
        }

        public bool Loaded
        {
            get
            {
                return loaded;
            }
            set
            {
                loaded = value;
            }
        }

        public bool DAmb
        {
            get
            {
                return dAmb;
            }
            set
            {
                if (dAmb == value)
                    return;
                dAmb = value;
            }
        }

        public bool DFsh
        {
            get
            {
                return dFsh;
            }
            set
            {
                if (dFsh == value)
                    return;
                dFsh = value;
            }
        }

        public bool DPlot
        {
            get
            {
                return dPlot;
            }
            set
            {
                if (dPlot == value)
                    return;
                dPlot = value;
            }
        }

        public bool DMod
        {
            get
            {
                return dMod;
            }
            set
            {
                if (dMod == value)
                    return;
                dMod = value;
            }
        }

        public bool DShtim
        {
            get
            {
                return dShtim;
            }
            set
            {
                dShtim = value;
            }
        }

        /// <summary>
        /// Get/Set a mund t'i shohe perdoruesi te gjitha dokumentat, apo vetem ato qe ka krijuar vet
        /// </summary>
        public bool DGjitheDok
        {
            get
            {
                return dGjitheDok;
            }
            set
            {
                if (dGjitheDok == value)
                    return;
                dGjitheDok = value;
            }
        }

        public int IdRoli
        {
            get
            {
                return idRoli;
            }
            set
            {
                idRoli = value;
            }
        }

        public int IdNdermarrje
        {
            get
            {
                return idNdermarrje;
            }
            set
            {
                idNdermarrje = value;
            }
        }

        public int IdViti
        {
            get
            {
                return idViti;
            }
            set
            {
                idViti = value;
            }
        }

        public int IdPerdorues
        {
            get
            {
                return idPerdorues;
            }

            set
            {
                idPerdorues = value;
            }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor bosh
        /// </summary>        
        public clsDrejtaTabi()
        {
        }

        /// <summary>
        /// Perdoret per te krijuar pemen
        /// </summary>
        /// <param name="idDrejta"></param>
        /// <param name="idPrindi"></param>
        /// <param name="tabi"></param>
        /// <param name="dMod"></param>
        /// <param name="dFsh"></param>
        /// <param name="dAmb"></param>
        /// <param name="dShtim"></param>
        /// <param name="dGjitheDok"></param>               
        public clsDrejtaTabi(int idDrejta, Nullable<Int32> idPrindi, string tabi, bool dMod, bool dFsh, bool dAmb, bool dShtim, bool dGjitheDok)
        {
            this.idDrejta = idDrejta;
            this.idPrindi = idPrindi;
            this.tabi = tabi;
            this.dMod = dMod;
            this.dFsh = dFsh;
            this.dAmb = dAmb;
            this.dShtim = dShtim;
            this.dGjitheDok = dGjitheDok;
            this.dPlot = this.dMod && this.dFsh && this.dAmb && this.dShtim && this.dGjitheDok;
        }

        public clsDrejtaTabi(int idDrejta, int idRoli, int idNdermarrje, int idViti, string tabi, bool dMod, bool dFsh, bool dAmb, bool dShtim, bool dGjitheDok)
        {
            this.idDrejta = idDrejta;
            this.idRoli = idRoli;
            this.idNdermarrje = idNdermarrje;
            this.idViti = idViti;
            this.tabi = tabi;
            this.dMod = dMod;
            this.dFsh = dFsh;
            this.dAmb = dAmb;
            this.dShtim = dShtim;
            this.dGjitheDok = dGjitheDok;
            this.dPlot = this.dMod && this.dFsh && this.dAmb && this.dShtim && this.dGjitheDok;
        }

        public clsDrejtaTabi(IDataRecord rreshti)
        {
            Mbush(rreshti);
        }

        #endregion
        #region Metoda Publike

        public static bool kaTeDrejtaTabeshPerNdermarrjeDheVit(int idRoli, int idNdermarrje, int idViti, clsDatabaseAdmin db)
        {

            bool ekziston = db.kaTeDrejtaTabeshPerNdermarrjeDheVit(idRoli, idNdermarrje, idViti);
            return ekziston;
        }

        public void merrTeDrejtaTabi(int idViti, int idPerdorues, int idNdermarrje, string tabi)
        {
            GlobalCacheManager.MySessionCache.FillObjectFromCache(x => x.IdPerdorues == idPerdorues && x.IdViti == idViti && x.IdNdermarrje == idNdermarrje && x.Tabi == tabi,
                () =>
                {
                    using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                        db.merrTeDrejteTabiSipasPerdNdermarrjeViti(idViti, idPerdorues, idNdermarrje, tabi, this);
                    return this;
                }, this);

        }

        public void Mbush(IDataRecord rreshti)
        {
            idDrejta = Convert.ToInt32(rreshti["IDDREJTA"]);
            idPrindi = Convert.ToInt32(rreshti["IDDREJTAPRIND"]);
            tabi = rreshti["TABI"].ToString();
            dMod = Convert.ToBoolean(rreshti["D_MOD"]);
            dFsh = Convert.ToBoolean(rreshti["D_FSH"]);
            dAmb = Convert.ToBoolean(rreshti["D_AMB"]);
            dShtim = Convert.ToBoolean(rreshti["D_SHTIM"]);
            dGjitheDok = Convert.ToBoolean(rreshti["D_GJITHEDOK"]);
            dPlot = this.dMod && this.dFsh && this.dAmb && this.dShtim && this.dGjitheDok;
            int.TryParse(rreshti["IDPERDORUES"].ToString(), out idPerdorues);
            int.TryParse(rreshti["IDVITI"].ToString(), out idViti);
            int.TryParse(rreshti["IDNDERMARRJE"].ToString(), out idNdermarrje);
        }
        #endregion
    }
}