using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbShare
{
    public class clsGrupKontrolli
    {
        #region Atribute
        private int idGrupi;
        private string emerGrupi;
        private int rend;
        #endregion
        
        #region Properties
        public int IdGrupi
        {
            get
            {
                return idGrupi;
            }
            //set
            //{
            //    if (idGrupi == value)
            //        return;
            //    idGrupi = value;
            //}
        }
        public string EmerGrupi
        {
            get
            {
                return emerGrupi;
            }
            //set
            //{
            //    if (emerGrupi == value)
            //        return;
            //    emerGrupi = value;
            //}
        }
        /// <summary>
        /// numer rendites sherben per shfaqen e filtrave
        /// </summary>
        public int Rend
        {
            get
            {
                return rend;
            }            
        }
        #endregion

        #region Konstruktore
        public clsGrupKontrolli(int idGrupi)
        {
            clsDatabaseShare sharedb = new clsDatabaseShare();
            mbushGrupKontrolli(sharedb.merrGrupKontrolli(idGrupi));
            sharedb.Dispose();
        }
        #endregion        
        #region Internal Methods
        internal bool mbushGrupKontrolli(DataRow rreshti)
        {
            if (rreshti == null)
                return false;
            int.TryParse(rreshti["IDGRUP"].ToString(), out idGrupi);            
            emerGrupi = rreshti["GRUPEMER"].ToString();
            int.TryParse(rreshti["REND"].ToString(), out rend);
            return true;
        }
        #endregion
        #region Private Methods

        #endregion
    }
}
