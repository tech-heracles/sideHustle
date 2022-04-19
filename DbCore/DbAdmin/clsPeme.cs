using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje element te nje peme. 
    ///  Perdoret tek te drejtat e perdoruesve.
    /// </summary>
    public class clsPeme
    {
        #region Atribute

        private int idAti;
        private int idBiri;
        private String emriDege;
        private bool teplota;
        private bool modifikim;
        private bool lexim;
        private bool fshirje;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsPeme(int idati, int idbiri, String emridege, bool plot, bool lex, bool mod, bool fshi)
        {
            idAti = idati;
            idBiri = idbiri;
            emriDege = emridege;
            teplota = plot;
            lexim = lex;
            modifikim = mod;
            fshirje = fshi;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e modulit</param>
        public clsPeme(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushPeme(data.kthePemenEModulit(id));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsPeme()
        {
        }

        public clsPeme(DataRow rreshti)
        {
            
            mbushPeme(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e atit te cilit i perket kjo dege e pemes
        /// </summary>
        public int IdAti
        {
            get { return idAti; }
            set { idAti = value; }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne kesaj dege te pemes
        /// </summary>
        public int IdBiri
        {
            get { return idBiri; }
            set { idBiri = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin kesaj dege te pemes
        /// </summary>
        public String EmriDege
        {
            get { return emriDege; }
            set { emriDege = value; }
            
        }
        public bool  TePlota
        {
            get { return teplota; }
            set { teplota = value; }

        }
        public bool Lexim
        {
            get { return lexim; }
            set { lexim = value; }

        }
        public bool Modifikim
        {
            get { return modifikim; }
            set { modifikim = value; }

        }
        public bool Fshirje
        {
            get { return fshirje; }
            set { fshirje = value; }

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Kthen nje collection me objekte te tipit  <see cref="clsPeme"/>
        /// </summary>
        public colPeme merriTeGjithe()
        {
            colPeme data = new colPeme();
            data.mbushPemen();
            return data;

        }

        #endregion

        #region Metoda Internal

        internal bool mbushPeme(DataRow dbDataRowPeme)
        {
            if (dbDataRowPeme != null)
            {
                try
                {
                    int.TryParse(dbDataRowPeme["idMod"].ToString(), out idAti);
                    int.TryParse(dbDataRowPeme["IDAMBJMODULI"].ToString(), out idBiri);
                    emriDege = dbDataRowPeme["emri"].ToString();
                    teplota = false;
                    lexim = false;
                    modifikim = false;
                    fshirje = false;
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se pemes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
 

