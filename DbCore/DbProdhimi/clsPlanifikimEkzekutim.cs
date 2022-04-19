using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  lidhjen e dokumentave te planifikimit dhe ekzekutimit
    ///  (Te dhenat  merren nga tabela : T_PLANIFIKIM_EKZEKUTIM)
    /// </summary>
    public class clsPlanifikimEkzekutim
    {
        /// <summary>
        /// konstante per gabimin e marrjes se te dhenave nga db
        /// </summary>
        private const string STR_ERRORGabimGjateMarrjesSePlanifikimEkzekutimitNga = "ERROR: Gabim gjate marrjes se planifikim ekzekutimit nga db-ja";

        #region Atributet

        /// <summary>
        /// id ritese
        /// </summary>
        private int id;
        /// <summary>
        /// id e dokumentit te planifikimit
        /// </summary>
        private int idPlanifikimi;
        /// <summary>
        /// id e dokumentit te ekzekutimit
        /// </summary>
        private int idEkzekutimi;
        /// <summary>
        /// id e konfigurimit te ambjentit te dokumentit te planfikimit
        /// </summary>
        private int idKonfigAmbjentePlanifikimi;
        /// <summary>
        /// id e konfigurimit te ambjentit te dokumentit te ekzekutimit
        /// </summary>
        private int idKonfigAmbjenteEkzekutimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        /// <param name="id">id ritese</param>
        /// <param name="idEkzekutimi">id e dokumentit te ekzekutimit</param>
        /// <param name="idKonfigAmbjenteEkzekutimi">id e konfigurimit te dokumentit te ekzekutimit</param>
        /// <param name="idKonfigAmbjentePlanifikimi">id e konfigurimit te dokumentit te planifikimit</param>
        /// <param name="idPlanifikimi">id e dokumentit te planifikimit</param>
        public clsPlanifikimEkzekutim(int id, int idEkzekutimi, int idPlanifikimi, int idKonfigAmbjentePlanifikimi, int idKonfigAmbjenteEkzekutimi)
        {
            this.id = id;
            this.idEkzekutimi = idEkzekutimi;
            this.idPlanifikimi = idPlanifikimi;
            this.idKonfigAmbjentePlanifikimi = idKonfigAmbjentePlanifikimi;
            this.idKonfigAmbjenteEkzekutimi = idKonfigAmbjenteEkzekutimi;
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsPlanifikimEkzekutim()
        {
        }

        public clsPlanifikimEkzekutim(DataRow rreshti)
        {
            
            mbushPlanifikimEkzekutim(rreshti);
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
        /// Kthen/Vendos ID-ne e dokumentit te planifikimit
        /// </summary>
        public int IdPlanifikimi
        {
            get { return idPlanifikimi; }
            set { idPlanifikimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit te ekzekutimit
        /// </summary>
        public int IdEkzekutimi
        {
            get
            {
                return idEkzekutimi;
            }
            set
            {
                idEkzekutimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit te planifikimit
        /// </summary>
        public int IdKonfigAmbjentePlanifikimi
        {
            get { return idKonfigAmbjentePlanifikimi; }
            set { idKonfigAmbjentePlanifikimi = value; }
        }

        /// <summary>
        /// kthen vendos id e konfigurimit te dokumentit te ekzekutimit
        /// </summary>
        public int IdKonfigAmbjenteEkzekutimi
        {
            get
            {
                return idKonfigAmbjenteEkzekutimi;
            }
            set
            {
                idKonfigAmbjenteEkzekutimi = value;
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e konvertimit ne tabelen perkatese ne databaze.
        /// </summary>
        /// <returns> clsmesazh me statusin e ruajtjes se dokumentit</returns>
        public clsMesazh ruaj()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_ruajt = data.ruajPlanifikimEkzekutim(Id, IdEkzekutimi, IdPlanifikimi, IdKonfigAmbjentePlanifikimi, IdKonfigAmbjenteEkzekutimi);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Fshin rreshtin perkates nga tabela perkatese ne databaze sipas ID-se qe i eshte caktuar objektit sipas id planifikimi
        /// </summary>
        /// <param name="idplanifikimi">id e dok te planifikimit</param>
        /// <returns> clsMesazh me statusin e fshirjes se dokumentit</returns>
        public static clsMesazh fshiSipasIdPlanifikimi(int idplanifikimi)
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_fshi = data.fshiSipasIdPlanifikimi(idplanifikimi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Fshin rreshtin perkates nga tabela perkatese ne databaze sipas ID-se se dok te ekzekutimit
        /// </summary>
        /// <param name="idEkzekutimi">id e dok te ekzekutimit</param>
        /// <returns>clsMesazh me statusin e dokumentit te ekzekutimit</returns>
        public clsMesazh fshiSipasIdEkzekutimi(int idEkzekutimi)
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_fshi = data.fshiSipasIdEkzekutimi(idEkzekutimi);
            data.Dispose();
            return u_fshi;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// perdoret per te krijuar objektin clsPlanifikimEkzekutimi nga te dhenat e db
        /// </summary>
        /// <param name="dbDataRow">data row me te dhenat e tipit planifikim ekzekutim</param>
        /// <returns>kthen true ose false nese objekti u krijua me sukses</returns>
        internal bool mbushPlanifikimEkzekutim(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["ID"].ToString(), out id);
                    int.TryParse(dbDataRow["IDEKZEKUTIMI"].ToString(), out idEkzekutimi);
                    int.TryParse(dbDataRow["IDPLANIFIKIMI"].ToString(), out idPlanifikimi);
                    int.TryParse(dbDataRow["IDKONFIGPLANIFIKIMI"].ToString(), out idKonfigAmbjentePlanifikimi);
                    int.TryParse(dbDataRow["IDKONFIGEKZEKUTIMI"].ToString(), out idKonfigAmbjenteEkzekutimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(STR_ERRORGabimGjateMarrjesSePlanifikimEkzekutimitNga);
                }
            }
            else
                return false;
        }

        #endregion
    }
}
