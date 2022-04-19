using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne trupin e nje skeme kontabel
    ///  (Te dhenat  merren nga tabela : T_TRUPISKEMAKONTAB)
    ///</remarks>
    public class clsSkemaKontabelTrupi
    {
        #region Atribute

        private int idSkemaKontabelTrupi;
        private String debiKrediSkemaKontabelTrupi;
        private int idKoka;
        private int idSkemaModel;
        private String kodiSkemaModel;
        private int idLlogariSkemaKontabelTrupi;
        private String llogariSkemaKontabelTrupi;

        #endregion

        #region Konstruktor
        
        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsSkemaKontabelTrupi(int idskemakontabeltrupi, String debikrediskemakontabeltrupi,
                                     int idkoka, int idskemamodel, int idllogariskemakontabeltrupi,
                                     string llogariskemakontabeltrupi)
        {
            idSkemaKontabelTrupi = idskemakontabeltrupi;
            debiKrediSkemaKontabelTrupi = debikrediskemakontabeltrupi;
            idKoka = idkoka;
            idSkemaModel = idskemamodel;
            idLlogariSkemaKontabelTrupi = idllogariskemakontabeltrupi;
            llogariSkemaKontabelTrupi = llogariskemakontabeltrupi;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsSkemaKontabelTrupi(String debikrediskemakontabeltrupi,
                                     int idkoka, int idskemamodel, int idllogariskemakontabeltrupi)
        {
            debiKrediSkemaKontabelTrupi = debikrediskemakontabeltrupi;
            idKoka = idkoka;
            idSkemaModel = idskemamodel;
            idLlogariSkemaKontabelTrupi = idllogariskemakontabeltrupi;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsSkemaKontabelTrupi()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdSkemaKontabelTrupi
        {
            get { return idSkemaKontabelTrupi; }
            set { idSkemaKontabelTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e skemes kontabel
        /// </summary>
        public String KodiSkemaModel
        {
            get { return kodiSkemaModel; }
            set { kodiSkemaModel = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese llogaria preket ne debi apo ne kredi
        /// </summary>
        public String DebiKrediSkemaKontabelTrupi
        {
            get { return debiKrediSkemaKontabelTrupi; }
            set { debiKrediSkemaKontabelTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se skemes kontabel
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e skemes model nga ku eshte ndertuar kjo skeme kontabel
        /// </summary>
        public int IdSkemaModel
        {
            get { return idSkemaModel; }
            set { idSkemaModel = value; }
        }

        /// <summary>
        /// Kthen/Vendos llogarine
        /// </summary>
        public string LlogariSkemaKontabelTrupi
        {
            get { return llogariSkemaKontabelTrupi; }
            set { llogariSkemaKontabelTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise
        /// </summary>
        public int IdLlogariSkemaKontabelTrupi
        {
            get { return idLlogariSkemaKontabelTrupi; }
            set { idLlogariSkemaKontabelTrupi = value; }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e skemes kontabel nga databaza
        /// </summary>
        /// <param name="dbDataRowSkemaKontTrupi">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushSkemaKontabelTrupi(DataRow dbDataRowSkemaKontTrupi)
        {
            if (dbDataRowSkemaKontTrupi != null)
            {
                try
                {
                    int.TryParse(dbDataRowSkemaKontTrupi["TRUPISKEMAKONTABID"].ToString(), out idSkemaKontabelTrupi);
                    int.TryParse(dbDataRowSkemaKontTrupi["KOKASKEMAKONTABID"].ToString(), out idKoka);
                    int.TryParse(dbDataRowSkemaKontTrupi["IDSKEMAMODEL"].ToString(), out idSkemaModel);
                    debiKrediSkemaKontabelTrupi = dbDataRowSkemaKontTrupi["TRUPISKEMAKONTABDEBIKREDI"].ToString();
                    int.TryParse(dbDataRowSkemaKontTrupi["TRUPISKEMAKONTABLLOGARI"].ToString(), out idLlogariSkemaKontabelTrupi);
                    kodiSkemaModel = dbDataRowSkemaKontTrupi["KOKASKEMAKONTABKODI"].ToString();
                    llogariSkemaKontabelTrupi = dbDataRowSkemaKontTrupi["NRLLOGARI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te skemes kontabel nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
