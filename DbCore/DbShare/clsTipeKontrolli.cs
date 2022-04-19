using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbShare
{
    public class clsTipeKontrolli
    {
        #region Attributet
        private int idTipKontrolli;
        private string tipiKontrollit;
        #endregion
        #region Properties
        public int IdTipKontrolli
        {
            get
            {
                return idTipKontrolli;
            }
            set
            {
                if (idTipKontrolli == value)
                    return;
                idTipKontrolli = value;
            }
        }
        public string TipiKontrollit
        {
            get
            {
                return tipiKontrollit;
            }
            set
            {
                if (tipiKontrollit == value)
                    return;
                tipiKontrollit = value;
            }
        }
        #endregion

        internal bool mbushKontrollin(System.Data.DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["idTipi"].ToString(), out idTipKontrolli);
                    tipiKontrollit = rreshti["emerTipi"].ToString();                    
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se tipit te kontrollit nga db-ja");
                }
            }
            else
                return false;
        }
    }
}
