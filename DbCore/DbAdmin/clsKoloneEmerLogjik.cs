using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje kolone te nje tabele te caktuar. Perdoret
    ///  ne raste te tilla si : kur formojme sp-te per raportet apo per auditimin
    ///  (Te dhenat  merren nga tabela : T_EMERLOGJIKKOLONE)
    /// </summary>
    public class clsKoloneEmerLogjik
    {
        #region Atribute

        private int idKolone;
        private int idTabele;
        private int nrKolone;
        private String emerRealKolone;
        private String emerLogjikKolone;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsKoloneEmerLogjik(int idkolone, int idtabele, int nrkolone, String real, String logjik)
        {
            idKolone = idkolone;
            idTabele = idtabele;
            nrKolone = nrkolone;
            emerRealKolone = real;
            emerLogjikKolone = logjik;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsKoloneEmerLogjik(int idtabele, int nrkolone, String real, String logjik)
        {
            idTabele = idtabele;
            nrKolone = nrkolone;
            emerRealKolone = real;
            emerLogjikKolone = logjik;
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsKoloneEmerLogjik()
        {
        }

        public clsKoloneEmerLogjik(DataRow rreshti)
        {
            
            mbushKolone(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKolone
        {
            get { return idKolone; }
            set { idKolone = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e tabeles se ciles i perkete kjo kolone.
        /// </summary>
        public int IdTabele
        {
            get { return idTabele; }
            set { idTabele = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e kolones.
        /// </summary>
        public int NrKolone
        {
            get { return nrKolone; }
            set { nrKolone = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e kolones, njelloj sic e ka emrin kolona ne tabeles e SQL-se.
        /// </summary>
        public String EmerRealKolone
        {
            get { return emerRealKolone; }
            set { emerRealKolone = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin llogjik te kolones.
        /// </summary>
        public String EmerLogjikKolone
        {
            get { return emerLogjikKolone; }
            set { emerLogjikKolone = value; }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushKolone(DataRow dbDataRowKolone)
        {
            if (dbDataRowKolone != null)
            {
                try
                {
                    int.TryParse(dbDataRowKolone["IDKOLONE"].ToString(), out idKolone);
                    int.TryParse(dbDataRowKolone["IDTABELE"].ToString(), out idTabele);
                    int.TryParse(dbDataRowKolone["NRKOLONE"].ToString(), out nrKolone);
                    emerRealKolone = dbDataRowKolone["EMERREALKOLONE"].ToString();
                    emerLogjikKolone = dbDataRowKolone["EMERLOGJIKKOLONE"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kolonave nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}