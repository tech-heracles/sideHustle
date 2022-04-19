using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje element
    ///  te trupit te autorizimit(Te dhenat merren nga tabela :T_AUTORIZIMTRUPI)
    /// </summary> 
    public class clsAutorizimTrupi
    { 
        #region Atributet

        private int idAutorizimTrupi;
        private int idAutorizimKoka;
        private int idPerdoruesi;
        private string perdoruesUsername;
        private DataRow rreshti;

        #endregion
       
        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        //konstruktoret
        public clsAutorizimTrupi(int idAutorizimTrupi, int idAutorizimKoka, int idPerdoruesi)
        {
            this.idAutorizimKoka=idAutorizimKoka;
            this.idAutorizimTrupi=idAutorizimTrupi;
            this.idPerdoruesi=idPerdoruesi ;
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        //konstruktoret
        public clsAutorizimTrupi()
        {
        }

        public clsAutorizimTrupi(DataRow rreshti)
        {
            
            mbushAutorizimTrupi(rreshti);
        }

        #endregion
       
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne kokes se autorizimit te cilit i perket ky objekt i trupit.
        /// </summary>
        public int IdAutorizimKoka {
            get
            {
                return idAutorizimKoka;
            }
            set
            {
                idAutorizimKoka = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int  IdAutorizimTrupi {
            get
            {
                return idAutorizimTrupi;
            }
            set
            {
                idAutorizimTrupi = value;
            }
        }

        /// <summary>
        /// Kthen ID-ne e perdoruesit qe eshte caktuar ne kete grup autorizimi
        /// </summary>
        public int  IdPerdorues {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }

        /// <summary>
        /// Kthen Username-in e perdoruesit qe eshte caktuar ne kete grup autorizimi
        /// </summary>
        public string PerdoruesUsername
        {
            get
            {
                return perdoruesUsername;
            }
            set
            {
                perdoruesUsername = value;
            }
        }

        #endregion      

        #region Metoda Internal

        internal bool mbushAutorizimTrupi(DataRow dbDataRowAutorizimTrupi)
        {
            if (dbDataRowAutorizimTrupi != null)
            {
                try
                {
                    int.TryParse(dbDataRowAutorizimTrupi["IDAUTORIZIMTRUPI"].ToString(), out idAutorizimTrupi);
                    int.TryParse(dbDataRowAutorizimTrupi["IDAUTORIZIMKOKA"].ToString(), out idAutorizimKoka);
                    int.TryParse(dbDataRowAutorizimTrupi["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te autorizimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

        public clsMesazh ruajAutorizimTrupiNeseNukEkziston(int idAutorizimKoka, int idPerdoruesi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return ruajAutorizimTrupiNeseNukEkziston(idAutorizimKoka, idPerdoruesi, data);
        }

        public clsMesazh ruajAutorizimTrupiNeseNukEkziston(int idAutorizimKoka, int idPerdoruesi, clsDatabaseAdmin data)
        {
            return data.ruajAutorizimTrupiNeseNukEkziston(idAutorizimKoka, idPerdoruesi);
        }
    }

    #region Metoda Publike


    #endregion
}

