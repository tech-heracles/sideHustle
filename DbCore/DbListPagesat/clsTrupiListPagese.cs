using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Types;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e nje dokumenti  listpagese
    ///  (Te dhenat  merren nga tabela : T_TRUPILISTPAGESE)
    /// </summary>
    public class clsTrupiListPagese
    {
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se trupit te listpageses nga db-ja";
        private static NLog.Logger logu = NLog.LogManager.GetCurrentClassLogger();
        #region Atribute
        private string nrPersonal;
        private int idTrupi;
        private int idKoka;
        private int idPunonjes;
        private decimal paguar;
        private colKompListPagese oColKomp;
        private string shenime;
        private DataRow rreshti;
        private decimal cost;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>

        public clsTrupiListPagese(int idtrupi, int idkoka, int idpunonjes, decimal paguar, string shenime,decimal cost)
        {
            idTrupi = idtrupi;
            idKoka = idkoka;
            idPunonjes = idpunonjes;
            this.paguar = paguar;
            this.shenime = shenime;
            this.cost=cost;
            oColKomp = new colKompListPagese();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idKoka">id e kokes</param>
        public clsTrupiListPagese(int idtrupi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushTrupListPagese(db.ktheTrupiListPageseSipasID(idtrupi));
            db.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTrupiListPagese()
        {
        }

        /// <summary>
        /// mbush trupin sipas te dhenave te grides
        /// </summary>
        /// <param name="rreshtDoku"></param>
        /// <param name="komp"></param>
        /// <param name="idndermarje"></param>
        public clsTrupiListPagese(int idTrupi,Dictionary<string, string> rreshtDoku, colKompListPagese colKomp, int idndermarje,ref int colKompCounter,ref int colKompMuajiCounter, System.Web.SessionState.HttpSessionState session)
        {
            string txtNrpersonal = rreshtDoku["txtNrPersonal"];
            string txtPaguar = rreshtDoku["txtPaguar"]; 
            string txtCost = rreshtDoku["txtCosto"];
            string txtShenime = rreshtDoku["txtShenime"];
            if (txtNrpersonal != "")
            {
                if (txtNrpersonal != null && txtNrpersonal != "null")
                {
                    //GIMPROVE nxire jashte kete se e djeg tek vodi
                    clsPunonjes pun = new clsPunonjes(txtNrpersonal, idndermarje);
                    if (pun.IdPunonjes == 0)
                        throw new Exception("Nje nga punonjest nuk ekziston! punonjesi me nrPersonal :" + txtNrpersonal);
                    IdPunonjes = pun.IdPunonjes;
                    if (txtPaguar != null && txtPaguar != "null" && txtPaguar != "")
                        Paguar = decimal.Parse(txtPaguar);
                    if (txtCost != null && txtCost != "null" && txtCost != "")
                        Cost = decimal.Parse(txtCost);
                    Shenime = txtShenime;
                    IdTrupi = idTrupi;
                    OColKomp = colKomp;
                    OColKomp.MbushKomponenteMuajiNgaSessioni(IdTrupi, ref colKompCounter, ref colKompMuajiCounter, colKomp, session, pun.IdPunonjes);

                }
            }            
        }
        public clsTrupiListPagese(int idTrupi,clsTrupiListPagese trupiNgaGrida,int idPunononjes,ref int colKompCounter, ref int colKompMuajiCounter, System.Web.SessionState.HttpSessionState session)
        {

        }
        public clsTrupiListPagese(DataRow rreshti)
        {
           
            mbushTrupListPagese(rreshti);
        }
      
        #endregion

        #region Properties

        public decimal Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit te listpageses.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e punonjesit
        /// </summary>
        public int IdPunonjes
        {
            get { return idPunonjes; }
            set { idPunonjes = value; }
        }
        /// <summary>
        /// Kthen/Vendos paguar.
        /// </summary>
        public decimal Paguar
        {
            get { return paguar; }
            set { paguar = value; }
        }
        /// <summary>
        /// kthen mbush coleksionin me komponente
        /// </summary>
        public colKompListPagese OColKomp
        {
            get
            {
                return oColKomp;
            }
            set
            {
                oColKomp = value;
            }
        }
        public string Shenime
        {
            get
            {
                return shenime;
            }
            set
            {
                shenime = value;
            }
        }

        public string NrPersonal
        {
            get
            {
                return nrPersonal;
            }

            set
            {
                nrPersonal = value;
            }
        }
        #endregion

        #region Metoda publike

        /// <summary>
        /// ruan objektin e  trupit te dokumentit ne tabelen perkatese ne databaze.Therret funksionin

        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        [Obsolete]
        public clsMesazh ruaj()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            int id;
            clsMesazh u_ruajt = data.ruajTrupiListPagese(out id, IdKoka, IdPunonjes, Paguar, shenime,cost);
            IdTrupi = id;
            data.Dispose();
            return u_ruajt;            
        }



        /// <summary>
        /// Modifikon objektin e  trupit te dokumentit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbListPagesat.clsDatabazeListPagesa.modifikoTrupiListPagese"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        [Obsolete]
        public clsMesazh modifiko()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            clsMesazh u_modifikua = data.modifikoTrupiListPagese(IdTrupi, IdKoka, idPunonjes, paguar,shenime,cost);
            data.Dispose();
            return u_modifikua;            
        }

        /// <summary>
        /// Fshin objektin e  trupit te dokumentit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbListPagesat.clsDatabazeListPagesa.fshiTrupiListPageseSipasID"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        [Obsolete]
        public clsMesazh fshi()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            clsMesazh u_fshi = data.fshiTrupiListPageseSipasID(IdTrupi);
            data.Dispose();
            return u_fshi;            
        }

        /// <summary>
        /// Merr objektin e  trupit se dokumentit sipas id nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbListPagesat.clsDatabazeListPagesa.ktheTrupiSipasID"/> 
        /// </summary>
        /// <returns > nje objekt clsTrupiShitje me trupin e dokumentit te kerkuar</returns>
        [Obsolete]
        public clsTrupiListPagese merriSipasID()
        {
            clsTrupiListPagese data = new clsTrupiListPagese(IdTrupi);
            return data;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e shitjes nga databaza
        /// </summary>
        /// <param name="dbDataRowTrup">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupListPagese(DataRow dbDataRowTrup)
        {
            if (dbDataRowTrup != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrup["IDTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowTrup["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRowTrup["IDPUNONJES"].ToString(), out idPunonjes);
                    decimal.TryParse(dbDataRowTrup["PAGUAR"].ToString(), out paguar);     
                    decimal.TryParse(dbDataRowTrup["COST"].ToString(), out cost);
                    shenime = dbDataRowTrup["Shenime"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(gabimNeTeDhena);
                }
            }
            else
                return false;
        }



        /// <summary>
        /// krijon nje clsTrupListPagese duke u bazuar mbi nje sql data record,perdoret kur mbushet nje colection
        /// </summary>
        /// <param name="dbDataRowTrup"></param>
        /// <returns></returns>
        public static clsTrupiListPagese Krijo(IDataRecord dbDataRowTrup)
        {
            clsTrupiListPagese trupi = new clsTrupiListPagese();
            trupi.mbushTrup(dbDataRowTrup);
            return trupi;
        }
        public void mbushTrup(IDataRecord dbDataRowTrup)
        {
            try
            {

                int.TryParse(dbDataRowTrup["IDTRUPI"].ToString(), out idTrupi);
                int.TryParse(dbDataRowTrup["IDKOKA"].ToString(), out idKoka);
                int.TryParse(dbDataRowTrup["IDPUNONJES"].ToString(), out idPunonjes);
                Converter.Parse(dbDataRowTrup["PAGUAR"].ToString(), out paguar, "paguar");
                Converter.Parse(dbDataRowTrup["COST"].ToString(), out cost, "cost");
                shenime = dbDataRowTrup["Shenime"].ToString();

            }
            catch (MyException ex)
            {
                var username = clsPunonjes.MerrNrPersonalSipasID(IdPunonjes);
                throw new MyException(logu, "trupi nuk u mbush per punonjesin me username {0} error:{1}", username, ex.Message);
            }

        }
        #endregion
    }
}
