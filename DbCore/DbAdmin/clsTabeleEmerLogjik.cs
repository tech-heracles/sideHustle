using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje tabele qe ndodhet ne databaze.
    ///  (Te dhenat  merren nga tabela : T_EMERLOGJIKTABELE)
    /// </summary>
    public class clsTabeleEmerLogjik
    {
        #region Atribute

        private int idTabele;
        private int nrTabele;
        private String emerRealTabele;
        private String emerLogjikTabele;

        private colKolonatEmerLogjik oColKolonat;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTabeleEmerLogjik(int idtabele, int nrtabele, String real, String logjik)
        {
            idTabele = idtabele;
            nrTabele = nrtabele;
            emerRealTabele = real;
            emerLogjikTabele = logjik;

            oColKolonat = new colKolonatEmerLogjik();
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTabeleEmerLogjik(int nrtabele, String real, String logjik)
        {
            nrTabele = nrtabele;
            emerRealTabele = real;
            emerLogjikTabele = logjik;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTabeleEmerLogjik()
        {
        }

        public clsTabeleEmerLogjik(DataRow rreshti)
        {
            
            mbushTabeleEmerLogjik(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTabele
        {
            get { return idTabele; }
            set { idTabele = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e tabeles.
        /// </summary>
        public int NrTabele
        {
            get { return nrTabele; }
            set { nrTabele = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin real te tabeles, ate emer qe i eshte vene kur tabela eshte krijuar ne databaze
        /// </summary>
        public String EmerRealTabele
        {
            get { return emerRealTabele; }
            set { emerRealTabele = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin pershkrues te tabeles
        /// </summary>
        public String EmerLogjikTabele
        {
            get { return emerLogjikTabele; }
            set { emerLogjikTabele = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me te kolonat e tabeles. Collectioni permban objekte te tipit
        /// <see cref="clsKoloneEmerLogjik"/>
        /// </summary>
        public colKolonatEmerLogjik OColKolonat
        {
            get { return oColKolonat; }
            set { oColKolonat = value; }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushTabeleEmerLogjik(DataRow dbDataRowTabeleEmerLogjik)
        {
            if (dbDataRowTabeleEmerLogjik != null)
            {
                try
                {
                    int.TryParse(dbDataRowTabeleEmerLogjik["IDTABELE"].ToString(), out idTabele);
                    int.TryParse(dbDataRowTabeleEmerLogjik["NRTABELE"].ToString(), out nrTabele);
                    emerRealTabele = dbDataRowTabeleEmerLogjik["EMERREALTABELE"].ToString();
                    emerLogjikTabele = dbDataRowTabeleEmerLogjik["EMERLOGJIKTABELE"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se emrit logjik te tabeles nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}