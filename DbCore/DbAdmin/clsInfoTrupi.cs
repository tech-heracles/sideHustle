using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// Kjo klase permban metodat e nevojshme per te perdorur te dhenat e tabeles T_INFOTRUPI
    /// Keto te dhena percaktojne konfigurimin e infos se artikullit per faturat e shitjes dhe blerjes
    /// </summary>
    public class clsInfoTrupi
    {
        #region Atribute

        private int idInfoTrupi;
        private int idInfoKoka;
        private string emerKolone;
        private string pershkrimKolone;
        private bool visibility;
        private int rendi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori me parametra i klases
        /// </summary>
        /// <param name="idTrupi">Id e trupit te infos</param>
        /// <param name="idKoka">Id e kokes se infos</param>
        /// <param name="emri">emri i kolones</param>
        /// <param name="pershkrimi">pershkrimi i kolones</param>
        /// <param name="visible">visibility i kolones</param>
        /// <param name="rendiKolona">rendi i kolones</param>
        public clsInfoTrupi(int idTrupi, int idKoka, string emri, string pershkrimi, bool visible, int rendiKolona)
        {
            this.idInfoTrupi = idTrupi;
            this.idInfoKoka = idKoka;
            this.emerKolone = emri;
            this.pershkrimKolone = pershkrimi;
            this.visibility = visible;
            this.rendi = rendiKolona;
        }

        /// <summary>
        /// Konstruktori qe krijon objektin clsInfoTrupi me id qe i kalohet si parameter
        /// </summary>
        /// <param name="idTrupi"></param>
        public clsInfoTrupi(int idTrupi)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            mbushInfoTrupi(dbAdmin.merrInfoTrupiSipasID(idTrupi));
            dbAdmin.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsInfoTrupi()
        {

        }

        public clsInfoTrupi(DataRow rreshti)
        {
            
            mbushInfoTrupi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdInfoTrupi
        {
            get { return idInfoTrupi; }
            set { idInfoTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e info artikulli koka.
        /// </summary>
        public int IdInfoKoka
        {
            get { return idInfoKoka; }
            set { idInfoKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e kolones per fushat e infos se artikullit
        /// </summary>
        public string EmerKolone
        {
            get { return emerKolone; }
            set { emerKolone = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e kolones(vleren qe do shfaqet ne ambjent) per fushat e infos se artikullit
        /// </summary>
        public string PershkrimKolone
        {
            get { return pershkrimKolone; }
            set { pershkrimKolone = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren true ose false qe percakton per secilen nga fushat e infos se artikullit nese do shfaqen ose jo
        /// </summary>
        public bool Visibility
        {
            get { return visibility; }
            set { visibility = value; }
        }


        /// <summary>
        /// Kthen/Vendos renditjen e shfaqjes se fushave te infos se artikullit ne ambjent
        /// </summary>
        public int Rendi
        {
            get { return rendi; }
            set { rendi = value; }
        }

        #endregion

        #region Metodat publike

        #endregion

        #region Metodat internal

        internal bool mbushInfoTrupi(DataRow dbDataRowInfoTrupi)
        {
            if (dbDataRowInfoTrupi != null)
            {
                try
                {
                    int.TryParse(dbDataRowInfoTrupi["IDINFOTRUPI"].ToString(), out idInfoTrupi);
                    int.TryParse(dbDataRowInfoTrupi["IDINFOKOKA"].ToString(), out idInfoKoka);
                    emerKolone = dbDataRowInfoTrupi["EMERKOLONE"].ToString();
                    pershkrimKolone = dbDataRowInfoTrupi["PERSHKRIMKOLONE"].ToString();
                 visibility  = Convert.ToBoolean(dbDataRowInfoTrupi["VISIBLITY"] );
                    int.TryParse(dbDataRowInfoTrupi["RENDI"].ToString(), out rendi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes trupit te infos se artikullit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
