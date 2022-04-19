using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using EO.Web.Internal;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne veprimet e te drejtave (shtim, modifikim, fshirje etj)
    ///  (Te dhenat  merren nga tabela : T_DREJTAVEPRIM)
    /// </summary>
    public class clsTeDrejtaVeprim
    {
        #region Atributet

        private int idDrejtaVeprim;
        private String kodiDrejtaVeprim;
        private String pershkrimiDrejtaVeprim;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTeDrejtaVeprim(int iddrejtaveprim, String kodidrejtaveprim, String pershkrimidrejtaveprim)
        {
            idDrejtaVeprim = iddrejtaveprim;
            kodiDrejtaVeprim = kodidrejtaveprim;
            pershkrimiDrejtaVeprim = pershkrimidrejtaveprim;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTeDrejtaVeprim(String kodidrejtaveprim, String pershkrimidrejtaveprim)
        {
            kodiDrejtaVeprim = kodidrejtaveprim;
            pershkrimiDrejtaVeprim = pershkrimidrejtaveprim;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTeDrejtaVeprim()
        { 
        }

        public clsTeDrejtaVeprim(DataRow rreshti)
        {
            
            mbushTeDrejtaVeprim(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdDrejtaVeprim
        {
            get { return idDrejtaVeprim; }
            set { idDrejtaVeprim = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e veprimit
        /// </summary>
        public String KodiDrejtaVeprim
        {
            get { return kodiDrejtaVeprim; }
            set { kodiDrejtaVeprim = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e veprimit
        /// </summary>
        public String PershkrimiDrejtaVeprim
        {
            get { return pershkrimiDrejtaVeprim; }
            set { pershkrimiDrejtaVeprim = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e veprimit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ruajTeDrejteVeprim"/> 
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = data.ruajTeDrejteVeprim(this.IdDrejtaVeprim, this.KodiDrejtaVeprim, this.PershkrimiDrejtaVeprim);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e veprimit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.modifikoTeDrejteVeprim"/> 
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = data.modifikoTeDrejteVeprim(this.IdDrejtaVeprim, this.KodiDrejtaVeprim, this.PershkrimiDrejtaVeprim);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e veprimit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.fshiTeDrejteVeprim"/> 
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiTeDrejteVeprim(this.IdDrejtaVeprim);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Kthen nje objekt te tipit <see cref="DbCore.DbAdmin.clsTeDrejtaVeprim"/>, te cilin e merr nga databaza 
        /// sipas ID-se qe i eshte caktuar objektit.
        /// </summary>
        public void merr()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            data.merrTeDrejteVeprim(this.IdDrejtaVeprim);
            data.Dispose();
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="cs"/> . Thirret funksioni
        /// <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ktheGjitheTeDrejtatVeprimet"/> 
        /// </summary>
        public colTeDrejtatVeprimet merriTeGjithe()
        {
            colTeDrejtatVeprimet data = new colTeDrejtatVeprimet();
            data.mbushGjitheTeDrejtatVeprimet();
            return data;

        }

        #endregion

        #region Metoda Internal

        internal bool mbushTeDrejtaVeprim(DataRow dbDataRowTeDrejtaVeprim)
        {
            if (dbDataRowTeDrejtaVeprim != null)
            {
                try
                {
                    idDrejtaVeprim = int.Parse(dbDataRowTeDrejtaVeprim["IDDREJTAVEPRIM"].ToString());
                    kodiDrejtaVeprim = dbDataRowTeDrejtaVeprim["DREJTAVEPRIMKODI"].ToString();
                    pershkrimiDrejtaVeprim = dbDataRowTeDrejtaVeprim["DREJTAVEPRIMPERSH"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se te drejtave te veprimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
