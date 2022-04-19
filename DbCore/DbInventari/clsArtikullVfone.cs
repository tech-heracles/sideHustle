using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbInventari
{  /// <summary>
   ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne artikujt vfone
   ///  (Te dhenat  merren nga tabela : T_ARTIKULLVFONE)
   /// </summary>

    public class clsArtikullVfone
    {

        #region Atribute

        private int id;
        private int idArtikulli;
        private string kodvfone;
        private decimal pike;
        private decimal vlere;


        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit kryesor
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikulli; }
            set { idArtikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodvfone
        /// </summary>
        public string KodVfone
        {
            get { return kodvfone; }
            set { kodvfone = value; }
        }

        /// <summary>
        /// Kthen/Vendos pike.
        /// </summary>
        public decimal Pike
        {
            get { return pike; }
            set { pike = value; }
        }
        /// <summary>
        /// Kthen/Vendos vlere.
        /// </summary>
        public decimal Vlere
        {
            get { return vlere; }
            set { vlere = value; }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// kontruktori me parametra
        /// </summary>

        public clsArtikullVfone(int id, int idart, string kodvfone, decimal pike, decimal vlere)
        {
            this.id = id;

            idArtikulli = idart;
            this.kodvfone = kodvfone;
            this.pike = pike;
            this.vlere = vlere;


        }

        /// <summary>
        /// kontruktori pa parametra
        /// </summary>
        public clsArtikullVfone()
        {

        }

        #endregion

        #region Metoda Publike

        #endregion


        #region Metoda Internal

        /// <summary>
        /// metode per mbushjen e artikullit perberes nga databaza
        /// </summary>
        /// <param name="dbDataRow">merr nje datarow qe duhet mbushur me te dhena nga databaza</param>
        /// <returns>kthen true nese eshte i vertete, ne te kundert false</returns>
        internal bool mbushArtikullVfone(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {

                try
                {
                    int.TryParse(dbDataRow["ID"].ToString(), out id);
                    int.TryParse(dbDataRow["IDARTIKULLI"].ToString(), out idArtikulli);
                    kodvfone = dbDataRow["KODVFONE"].ToString();
                    decimal.TryParse(dbDataRow["PIKE"].ToString(), out pike);
                    decimal.TryParse(dbDataRow["VLERE"].ToString(), out vlere);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se artikullit vfone nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion



    }
}
