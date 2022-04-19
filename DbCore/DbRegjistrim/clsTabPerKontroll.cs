using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{ 
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne tabperkontroll qe mban tipet e kontrolleve per dokumenta qe lidhen me njeri tjetrin
    ///  (Te dhenat  merren nga tabela : T_TABPERKONTROLL)
    /// </remarks>
    public class clsTabPerKontroll
    {
        #region Atributet

        private int id;
        private string kodi;
        private string emerTabele;
        private string pershkrimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="kodi">kodi</param>
        /// <param name="emerTabele">emri i tabeles</param>
        /// <param name="pershkrimi">pershkrimi</param>
        public clsTabPerKontroll(int id, string kodi, string emerTabele, string pershkrimi)
        {
            this.id = id;
            this.kodi = kodi;
            this.emerTabele = emerTabele;
            this.pershkrimi = pershkrimi;
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsTabPerKontroll()
        {
        }

        public clsTabPerKontroll(DataRow rreshti)
        {
            
            mbushTabPerKontroll(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos Id-ne qe gjenerohet automatikisht
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin
        /// </summary>
        public string Kodi
        {
            get
            {
                return kodi;
            }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen vendos Emerin e tables qe i perket kontrolli
        /// </summary>
        public string EmerTabele
        {
            get { return emerTabele; }
            set
            {
                emerTabele = value;
            }
        }

        /// <summary>
        /// Kthen vendos Pershkrimin
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush tab per kontroll nga databaza
        /// </summary>
        /// <param name="dbDataRowTab">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTabPerKontroll(DataRow dbDataRowTab)
        {
            if (dbDataRowTab != null)
            {
                try
                {
                    int.TryParse(dbDataRowTab["ID"].ToString(), out id);
                    kodi = dbDataRowTab["KODI"].ToString();
                    emerTabele = dbDataRowTab["EMERTABELE"].ToString();
                    pershkrimi = dbDataRowTab["PERSHKRIMI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se tab per kontroll nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion 

        #region Metoda Publike

        /// <summary>
        /// mbush tab per kontroll sipas id-se
        /// </summary>
        /// <param name="id">id e tab per kontroll</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushTabPerKontrollSipasId(int id)
        {
            clsDatabaseRegjistrim dbTab= new clsDatabaseRegjistrim();
            bool mbush = mbushTabPerKontroll(dbTab.ktheTabPerKontrollSipasId(id));
            dbTab.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush tab per kontroll sipas kodit
        /// </summary>
        /// <param name="kodi">kodi i tab per kontroll</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushTabPerKontrollSipasKodit(int kodi)
        {
            clsDatabaseRegjistrim dbTab = new clsDatabaseRegjistrim();
            bool mbush = mbushTabPerKontroll(dbTab.ktheTabPerKontrollSipasKodit(kodi));
            dbTab.Dispose();
            return mbush;
        }

        #endregion

    }
}