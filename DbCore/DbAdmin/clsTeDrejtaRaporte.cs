using System;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using CacheLayer;
using DbCore.IMBUtils.Cache;
using DbCore.DbShare;

namespace DbCore.DbAdmin
{
    public class clsTeDrejtaRaporte : IDataBaseReader
    {
        #region Atributet
        //public const string mySessionKey = "clsTeDrejtaRaporte";
        private int idDrejta;
        private int idPeme; //id qe i vendoset te drejtave per krijimin e pemes
        private int? idPrindi;
        private int idRaport;
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
        private int _idPerdoruesi;
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

        public int IdRaport
        {
            get
            {
                return idRaport;
            }
            set
            {
                idRaport = value;
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

        public int IdPerdorues { get { return _idPerdoruesi; } set { _idPerdoruesi = value; } }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor bosh
        /// </summary>        
        public clsTeDrejtaRaporte()
        {

        }




        public clsTeDrejtaRaporte(IDataRecord rreshti)
        {
            Mbush(rreshti);
        }

        #endregion

        #region Metoda Publike

        public void merrTeDrejtaPerRaportPerPerdorues(string emerRealRaporti, int idPerdorues, int idNdermarrje, int idViti)
        {
            int idRaport = clsRaporti.KtheIdRaportiSipasEmritReal(emerRealRaporti);
            GlobalCacheManager.MySessionCache.FillObjectFromCache(teDrejta => teDrejta.IdRaport == idRaport && teDrejta.IdPerdorues == idPerdorues && teDrejta.IdNdermarrje == idNdermarrje && teDrejta.IdViti == idViti, () =>
                       {
                           using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                               data.merrTeDrejteRaportiSipasPerdNdermarrjeViti(emerRealRaporti, idPerdorues, idNdermarrje, idViti, this);
                           return this;
                       }, this);
        }

        public void merrTeDrejtaPerRaportPerPerdorues(int idRaporti, int idPerdorues, int idNdermarrje, int idViti)
        {
            GlobalCacheManager.MySessionCache.FillObjectFromCache(teDrejta => teDrejta.IdRaport == idRaport && teDrejta.IdPerdorues == idPerdorues && teDrejta.IdNdermarrje == idNdermarrje && teDrejta.IdViti == idViti, () =>
            {
                using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                    data.merrTeDrejteRaportiSipasPerdNdermarrjeViti(idRaporti, idPerdorues, idNdermarrje, idViti, this);
                return this;
            }, this);
        }

        public void merrTeDrejtaPerRaportPerPerdoruesPerSubraportet(int idRaporti, int idPerdorues, int idNdermarrje, int idViti)
        {
            GlobalCacheManager.MySessionCache.FillObjectFromCache(teDrejta => teDrejta.IdRaport == idRaport && teDrejta.IdPerdorues == idPerdorues && teDrejta.IdNdermarrje == idNdermarrje && teDrejta.IdViti == idViti, () =>
            {
                using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                    data.merrTeDrejteRaportiSipasPerdNdermarrjeVitiPerSubRaportet(idRaporti, idPerdorues, idNdermarrje, idViti, this);
                return this;
            }, this);
        }
        public void Mbush(IDataRecord rreshti)
        {
            idDrejta = Convert.ToInt32(rreshti["IDDREJTA"]);
            idPrindi = Convert.ToInt32(rreshti["IDDREJTAPRIND"]);
            idRaport = Convert.ToInt32(rreshti["IDRAPORTI"]);
            dMod = Convert.ToBoolean(rreshti["D_MOD"]);
            dFsh = Convert.ToBoolean(rreshti["D_FSH"]);
            dAmb = Convert.ToBoolean(rreshti["D_AMB"]);
            dShtim = Convert.ToBoolean(rreshti["D_SHTIM"]);
            dGjitheDok = Convert.ToBoolean(rreshti["D_GJITHEDOK"]);
            idNdermarrje = Convert.ToInt32(rreshti["IDNDERMARRJE"]);
            idViti = Convert.ToInt32(rreshti["IDVITI"]);
            int.TryParse(rreshti["IDPERDORUESI"].ToString(), out _idPerdoruesi);
            dPlot = this.dMod && this.dFsh && this.dAmb && this.dShtim && this.dGjitheDok;
        }

        #endregion
    }
}