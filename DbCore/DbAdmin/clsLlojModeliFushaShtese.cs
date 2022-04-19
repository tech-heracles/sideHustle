using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne
    ///  llojet e modeleve per fushat shtese si : Klient/Furnitor, Llogari etj... 
    ///  (Te dhenat  merren nga tabela : T_LLOJMODELIFUSHASHTESE)
    /// </summary>
    public class clsLlojModeliFushaShtese
    {
        #region Atributet

        private int idLlojModeliFushaShtese;
        private string pershkrimiLlojModeliFushaShtese;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases.
        /// </summary>
        public clsLlojModeliFushaShtese(int idllojmodeliFushaShtese, String pershkrimillojmodeliFushaShtese)
        {
            idLlojModeliFushaShtese = idllojmodeliFushaShtese;
            pershkrimiLlojModeliFushaShtese = pershkrimillojmodeliFushaShtese;
        }

        /// <summary>
        /// Konstruktori i klases.
        /// </summary>
        public clsLlojModeliFushaShtese(String kodi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushLlojModelFushaShtese(data.ktheLlojModeliFushaShteseSipasKodit(kodi));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases.
        /// </summary>
        public clsLlojModeliFushaShtese()
        {
        }

        public clsLlojModeliFushaShtese(DataRow rreshti)
        {
            
            mbushLlojModelFushaShtese(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLlojModeliFushaShtese
        {
            get { return idLlojModeliFushaShtese; }
            set { idLlojModeliFushaShtese = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e ketij lloji te modelit per fushat shtese.
        /// </summary>
        public String PershkrimiLlojModeliFushaShtese
        {
            get { return pershkrimiLlojModeliFushaShtese; }
            set { pershkrimiLlojModeliFushaShtese = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin llojit te modelit per fushat shtese ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = data.ruajLlojModeliFushaShtese(this.IdLlojModeliFushaShtese, this.PershkrimiLlojModeliFushaShtese);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin llojit te modelit per fushat shtese ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = data.modifikoLlojModeliFushaShtese(this.IdLlojModeliFushaShtese, this.PershkrimiLlojModeliFushaShtese);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin llojit te modelit per fushat shtese nga tabela perkatese ne databaze.
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiLlojModeliFushaShtese(this.IdLlojModeliFushaShtese);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Nuk perdoret
        /// </summary>
        public void merr()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            data.merrLlojModeliFushaShtese(this.IdLlojModeliFushaShtese);
            data.Dispose();
        }

        /// <summary>
        /// Kthen nje collection me objekte te tipit <see cref="DbCore.DbAdmin.clsLlojModeliFushaShtese"/> , te cilat i merr
        /// nga tabela perkatese ne databaze
        /// </summary>
        public colLlojModeleshFushaShtese merriTeGjithe()
        {
            colLlojModeleshFushaShtese data = new colLlojModeleshFushaShtese();
            data.mbushGjitheLlojModeleshFushaShtese();
            return data;

        }

        #endregion

        #region Metoda Internal

        internal bool mbushLlojModelFushaShtese(DataRow dbDataRowLlojModel)
        {
            if (dbDataRowLlojModel != null)
            {
                try
                {
                    int.TryParse(dbDataRowLlojModel["IDLLOJMODELIFUSHASHTESE"].ToString(), out idLlojModeliFushaShtese);
                    pershkrimiLlojModeliFushaShtese = dbDataRowLlojModel["PERSHKRIMILLOJMODELIFUSHASHTESE"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se llojit te modelit te fushave shtese nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
