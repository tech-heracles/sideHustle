using System;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne gjendjen min dhe max te artikujve sipas magazinave
    ///  (Te dhenat  merren nga tabela : T_GJENDJEARTIKULLI)
    /// </summary>

    public class clsGjendjeArtikulli
    {

        #region Atribute

        private int idGjendjeArtikulli;
        private int idArtikulli;
        private int idMagazina;
        private double gjendjaMin;
        private double gjendjaMax;
        private string magazina;

        #endregion

        #region Properties

        public double GjendjaMax
        {
            get { return gjendjaMax; }
            set { gjendjaMax = value; }
        }


        public double GjendjaMin
        {
            get { return gjendjaMin; }
            set { gjendjaMin = value; }
        }


        public int IdMagazina
        {
            get { return idMagazina; }
            set { idMagazina = value; }
        }

        public int IdArtikulli
        {
            get { return idArtikulli; }
            set { idArtikulli = value; }
        }
        public int IdGjendjeArtikulli
        {
            get { return idGjendjeArtikulli; }
        }
        public string Magazina {
            get { return magazina; }
        
        }
        #endregion

        #region Konstruktoret
        /// <summary>
        /// kontruktori pa parametra
        /// </summary>
        public clsGjendjeArtikulli()
        {

        }


        public clsGjendjeArtikulli(DataRow rreshti)
        {
            
            mbushGjendjeArtikulli(rreshti);
        }

        #endregion


        #region Metoda Internal

        /// <summary>
        /// metode per mbushjen e gjendjes se artikullit nga databaza
        /// </summary>
        /// <param name="dbDataRow">merr nje datarow qe duhet mbushur me te dhena nga databaza</param>
        /// <returns>kthen true nese eshte i vertete, ne te kundert false</returns>
        internal bool mbushGjendjeArtikulli(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDGJENDJEARTIKULLI"].ToString(), out idGjendjeArtikulli);
                    int.TryParse(dbDataRow["IDARTIKULLI"].ToString(), out idArtikulli);
                    int.TryParse(dbDataRow["IDMAGAZINA"].ToString(), out idMagazina);
                    gjendjaMin = Convert.ToDouble(dbDataRow["GJENDJAMIN"]);
                    gjendjaMax = Convert.ToDouble(dbDataRow["GJENDJAMAX"]);
                    magazina = dbDataRow["magazina"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se gjendjen se artikullit nga db-ja");
                }
            }
            else
                return false;
        }
        #endregion
    }
}
