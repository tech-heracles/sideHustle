using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  lidhjen e dokumentave qe konvertohen ne njeri tjetri
    ///  (Te dhenat  merren nga tabela : T_URDHERPOROSI_PLANIFIKIM)
    /// </summary>
    public class clsUrdherPorosiPlanifikim
    {
        /// <summary>
        /// konstante per gabimin e marrjes se te dhenave nga db
        /// </summary>
        private const string STR_ERRORGabimGjateMarrjesSeKonvertimitNgaDbja = "ERROR: Gabim gjate marrjes se urdherporosi planifikim nga db-ja";
        #region Atributet
        /// <summary>
        /// id ritese e tabeles
        /// </summary>
        private int id;
        /// <summary>
        /// id e urdherit te porosise
        /// </summary>
        private int idUrdherPorosia;
        /// <summary>
        /// id e planifikimit
        /// </summary>
        private int idPlanifikimi;
        /// <summary>
        /// id e konfigurimit te urdherit
        /// </summary>
        private int idKonfigUrdher;
        /// <summary>
        /// id e planifikimit te urdherit
        /// </summary>
        private int idKonfigPlanifikimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="idkonfigplanifikimi">id e konfigurimit te planifikimit</param>
        /// <param name="idkonfigurdher">id e konfigurimit te urdherit</param>
        /// <param name="idplanifikimi">id e planifikimit</param>
        /// <param name="idurdher"> id e urdherit</param>
        public clsUrdherPorosiPlanifikim(int id, int idurdher, int idplanifikimi, int idkonfigurdher, int idkonfigplanifikimi)
        {
            this.id = id;
            idPlanifikimi =  idplanifikimi;
            idUrdherPorosia =idurdher;
            idKonfigUrdher = idkonfigurdher;
            idKonfigPlanifikimi = idkonfigplanifikimi;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsUrdherPorosiPlanifikim()
        {
        }

        public clsUrdherPorosiPlanifikim(DataRow rreshti)
        {
            
            mbushUrdherPorosiPlanifikim(rreshti);
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
        /// Kthen/Vendos ID-ne e urdher porosise
        /// </summary>
        public int IdUrdherPorosia
        {
            get { return idUrdherPorosia; }
            set { idUrdherPorosia = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e planifikimit
        /// </summary>
        public int IdPlanifikimi
        {
            get
            {
                return idPlanifikimi;
            }
            set
            {
                idPlanifikimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit te urdher porosise
        /// </summary>
        public int IdKonfigUrdher
        {
            get { return idKonfigUrdher; }
            set { idKonfigUrdher = value; }
        }
        /// <summary>
        /// kthen vendos id e konfigurimit te dokumentit te planifikimit
        /// </summary>
        public int IdKonfigPlanifikimi
        {
            get
            {
                return idKonfigPlanifikimi;
            }
            set
            {
                idKonfigPlanifikimi = value;
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e konvertimit ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_ruajt = data.ruajUrdherPorosiPlanifikim(Id, IdPlanifikimi, IdUrdherPorosia, IdKonfigUrdher, IdKonfigPlanifikimi);
            data.Dispose();
            return u_ruajt;
        }



        /// <summary>
        /// Fshin rreshtin perkates nga tabela perkatese ne databaze sipas ID-se qe i eshte caktuar objektit te 
        /// sipas idudher porosia
        /// </summary>
        /// <param name="idurdher"> id e urdherit</param>
        public static clsMesazh fshiSipasIdUrdherPorosia(int idurdher)
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_fshi = data.fshiUrdherPorosiPlanifikimiSipasIdUrdheri(idurdher);
            data.Dispose();
            return u_fshi;
        }
        /// <summary>
        /// Fshin rreshtin perkates nga tabela perkatese ne databaze sipas ID-se qe i eshte caktuar objektit te 
        /// sipas id planifikimi
        /// </summary>
        /// <param name="idplanifikimi">id e  planifikimit</param>
        public static clsMesazh fshiSipasIdPlanifikimi(int idplanifikimi)
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_fshi = data.fshiUrdherPorosiPlanifikimSipasIdPlanfikimi(idplanifikimi);
            data.Dispose();
            return u_fshi;
        }

        #endregion

        #region Metoda Internal
        /// <summary>
        /// perdoret per te krijuar objektin clsUrdherPorosiPlanifikim nga te dhenat e db
        /// </summary>
        /// <param name="dbDataRow">data row me te dhenat e tipit urdherPorosi Planifikim</param>
        /// <returns>kthen true ose false nese objekti u krijua me sukses</returns>
        internal bool mbushUrdherPorosiPlanifikim(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["ID"].ToString(), out id);
                    int.TryParse(dbDataRow["IDPLANIFIKIMI"].ToString(), out idPlanifikimi);
                    int.TryParse(dbDataRow["IDURDHERPOROSIA"].ToString(), out idUrdherPorosia);
                    int.TryParse(dbDataRow["IDKONFIGURDHER"].ToString(), out idKonfigUrdher);
                    int.TryParse(dbDataRow["IDKONFIGPLANIFIKIMI"].ToString(), out idKonfigPlanifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(STR_ERRORGabimGjateMarrjesSeKonvertimitNgaDbja);
                }
            }
            else
                return false;
        }

        #endregion
    }
}
