using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne koken e templateve te artikujve perberes
    ///  (Te dhenat  merren nga tabela : T_ARTIKULLIPERBERESTEMPLATEKOKA)
    /// </summary>
   public class clsArtikullPerberesTemplateKoka
   {
        #region Atribute

        private int idKoka;
        private String kodi;
        private String pershkrimi;
        private int idNdermarje;

        public colArtikulliPerberesTemplateTrupi oColTrupi;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin.
        /// </summary>
        public String Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin.
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }
           public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        #endregion

        #region Konstuktoret

        /// <summary>
        /// kontruktori me parametra
        /// </summary>
        /// <param name="id"> id ritese e kokes</param>
        /// <param name="k">kodi </param>
        /// <param name="p"> pershkrimi</param>
        public clsArtikullPerberesTemplateKoka(int id, string k, string p, int idnder)
        {
            idKoka= id;
            kodi = k;
            pershkrimi = p;
            idNdermarje = idnder;
            oColTrupi = new colArtikulliPerberesTemplateTrupi();
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsArtikullPerberesTemplateKoka()
        { 
        }

        /// <summary>
        /// konstruktori me nje parameter integer
        /// </summary>
        public clsArtikullPerberesTemplateKoka(int id)
        {
            clsDatabaseInventari dbArtikujPerberes = new clsDatabaseInventari();
            mbushArtikullPerberesTemplateKoka(dbArtikujPerberes.ktheTemplateArtikulliPerberesSipasID(id));
            dbArtikujPerberes.Dispose();
        }

        public clsArtikullPerberesTemplateKoka(DataRow rreshti)
        {
            
            mbushArtikullPerberesTemplateKoka(rreshti);
        }

        #endregion

        #region Metoda publike

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id e kokes sipas kodit
        /// </summary>
        /// <param name="kod">kodi i artikullit perberes</param>
        /// <returns>id e kokes</returns>
        public static int merrIDKokaArtikullPerberesTemplateKoka(string kodi, int idndermarje)
        {
            clsDatabaseInventari dbArtikujPerberesTemplateKoka = new clsDatabaseInventari();
            int idKoka = (dbArtikujPerberesTemplateKoka.ktheIdKokaTemplateArtikulliPerberesSipasKodit(kodi, idndermarje));
            dbArtikujPerberesTemplateKoka.Dispose();
            return idKoka;
        }

        #endregion

        #region Metoda internal

        internal bool mbushArtikullPerberesTemplateKoka(DataRow dbDataRowArtikullPerberesTemplateKoka)
        {
            if (dbDataRowArtikullPerberesTemplateKoka != null)
            {

                try
                {
                    int.TryParse(dbDataRowArtikullPerberesTemplateKoka["IDKOKA"].ToString(), out idKoka);
                    kodi = dbDataRowArtikullPerberesTemplateKoka["KODI"].ToString();
                    pershkrimi = dbDataRowArtikullPerberesTemplateKoka["PERSHKRIMI"].ToString();
                          int.TryParse(dbDataRowArtikullPerberesTemplateKoka["IDNDERMARJE"].ToString(), out idNdermarje);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se artikullit perberes me template koke nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
   }
}
