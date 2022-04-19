using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbShare
{
    public class colTipeKontrolli:List<clsTipeKontrolli>
    {
        /// <summary>
        /// Konstruktor bosh
        /// </summary>
        public colTipeKontrolli()
        { 
        }
        
        public clsTipeKontrolli merrTipin(int idTipiKontrollit)
        {
            foreach (clsTipeKontrolli tipi in this)
                if (tipi.IdTipKontrolli == idTipiKontrollit)
                    return tipi;                
            return null;
        }
        public bool mbushTipet()
        {
            using (clsDatabaseShare shareDb = new clsDatabaseShare())
            {
                bool mbush = mbushTipet(shareDb.merrTipet());
                return mbush;
            }
        }
        /// <summary>
        /// Mbush koleksionin 
        /// </summary>
        private bool mbushTipet(DataTable dt) 
        { 
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsTipeKontrolli tipKontrolli = new clsTipeKontrolli();
                    if(tipKontrolli.mbushKontrollin(rreshti))
                        Add(tipKontrolli);
                }
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            return true;
        }
    }
}
