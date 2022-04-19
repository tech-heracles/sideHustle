using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{
    public class clsPeriudhaInfoArtikulli
    {

        #region Atribute

        private int idPeriudha;
        private String periudhaPershkrim;

        #endregion
        
        #region Konstruktoret

        /// <summary>
        /// Konstruktori me paramtera i klases
        /// </summary>
        public clsPeriudhaInfoArtikulli(int id,  String pershkrimi)
        {
            idPeriudha = id;
            periudhaPershkrim = pershkrimi;
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsPeriudhaInfoArtikulli()
        { 
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e objektit qe perfaqson periudhen.
        /// </summary>
        public int IdPeriudha
        {
            get { return idPeriudha; }
            set { idPeriudha = value; }
        }
        /// <summary>
        /// Kthen/Vendos Pershkrimin-ne e objektit qe perfaqson periudhen.
        /// </summary>
        public string PeriudhaPershkrim
        {
            get { return periudhaPershkrim; }
            set { periudhaPershkrim = value; }
        }

        #endregion

    }
}
