using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne tipet e garancive (vjecare, mujore, etj.)
    ///  (Te dhenat  merren nga tabela : T_GARANCIA)
    /// </summary>
  public  class clsGarancia
    {
        #region Atribute

        private int idLlojGarancia;
        private string kodGarancia;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e llojit te garancise.
        /// </summary>
        public int IdLlojGarancia
        {
            get { return idLlojGarancia; }
            set { idLlojGarancia = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e garancise.
        /// </summary>
        public string KodGarancia
        {
            get { return kodGarancia; }
            set { kodGarancia = value; }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori pa parametra
        /// </summary>
        public clsGarancia( )
        {
        }

        /// <summary>
        /// konstruktor i klases me id
        /// </summary>
        /// <param name="idja">id-ja garancise qe do lexohet nga db-ja</param>
        /// <param name="dbInventari"></param>
        public clsGarancia(int idja)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            if (!mbushGaranci(dbInventari.merrGaranciSipasId(idja)))
                idLlojGarancia = -1;
            dbInventari.Dispose();
        }
      /// <summary>
      /// konstruktor me 2 parametra
      /// </summary>
      /// <param name="id"></param>
      /// <param name="kodi"></param>
        public clsGarancia(int id, string kodi)
        {
            idLlojGarancia = id;
            kodGarancia = kodi;
        }

        /// <summary>
        /// konstruktor i klases sipas kodit te garancise
        /// </summary>
        /// <param name="idja">id-ja garancise qe do lexohet nga db-ja</param>
        /// <param name="dbInventari"></param>
        public clsGarancia(string kodi)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            if (!mbushGaranci(dbInventari.merrGaranciSipasKod(kodi)))
                idLlojGarancia = -1;
            dbInventari.Dispose();
        }

        public clsGarancia(DataRow rreshti)
        {
            
            mbushGaranci(rreshti);
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// mbush garancine sipas kodit
        /// </summary>
        /// <param name="kod">kodi i garancise</param>
        // <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushGarancineSipasKodit(string kod)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushGaranci(dbInventari.merrGaranciSipasKod(kod));
            dbInventari.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush garancine sipas id
        /// </summary>
        /// <param name="kod">kodi i garancise</param>
        // <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushGarancineSipasId(int idGarancia)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushGaranci(dbInventari.merrGaranciSipasId(idGarancia));
            dbInventari.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// Metode per mbushjen e garancise nga databaza.
        /// </summary>
        /// <param name="dbDataRowGarancia">Si parameter merr nje DataRow.</param>
        /// <returns>Kthen true nese mbushja ndodh me sukses. Ne te kundert false.</returns>
        /// <param name="dbinventari"></param>
        internal bool mbushGaranci(DataRow dbDataRowGarancia)
        {
            if (dbDataRowGarancia == null)
                return false;
            try
            {
                int.TryParse(dbDataRowGarancia["IDLLOJGARANCIA"].ToString(), out idLlojGarancia);
                kodGarancia = dbDataRowGarancia["KODGARANCIA"].ToString();
                return true;
            }
            catch (InvalidCastException)
            {
                throw new MyException("ERROR: Gabim gjate cast-it!");
            }
            catch (Exception)
            {
                throw new MyException("ERROR: Gabim gjate marrjes se llojit te garancise nga db-ja");
            }
        }

        #endregion
    }
}
