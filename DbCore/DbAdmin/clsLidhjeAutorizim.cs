using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne lidhjen qe i behet nje elementi te caktuar 
    ///  si psh artikull,llogari etj  me nje autorizim te caktuar.
    ///  (Te dhenat  merren nga tabela : T_AUTORIZIMTRUPI)
    /// </summary>
    public class clsLidhjeAutorizim
    {
        #region Atributet

        private int idLidhjeAutorizim;
        private int idLloji;
        private int idLidhese;
        private int idAutorizimeKoka;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsLidhjeAutorizim(int idLidhjeAutorizim, int idLidhese, int idLloji, int idAutorizimeKoka)
        {
            this.idLidhjeAutorizim = idLidhjeAutorizim;
            this.idLidhese = idLidhese;
            this.idLloji = idLloji;
            this.idAutorizimeKoka = idAutorizimeKoka;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsLidhjeAutorizim()
        {
        }

        public clsLidhjeAutorizim(DataRow rreshti)
        {
            
            mbushLidhjeAutorizim(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLidhjeAutorizim
        {
            get { return idLidhjeAutorizim; }
            set { idLidhjeAutorizim = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  qe perfaqson llojin e elementit, pra eshte : Klient, Llogari etj (Fusha 
        /// lloji i merr vlerat nga tabela T_LLOJBUXHETI).
        /// </summary>
        public int IdLloji
        {
            get { return idLloji; }
            set { idLloji = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne perfaqson elementin, pra :ID-ne e Klient,ID-ne e Llogari etj.
        /// </summary>
        public int IdLidhese
        {
            get
            {
                return idLidhese;
            }
            set
            {
                idLidhese = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e Autorizimit te cilit i perket kjo lidhje.
        /// </summary>
        public int IdAutorizimeKoka
        {
            get { return idAutorizimeKoka; }
            set { idAutorizimeKoka = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e lidhjes se autorizimit ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.ruajLidhjeAutorizim(out this.idLidhjeAutorizim, this.IdLidhese, this.IdLloji, this.IdAutorizimeKoka, 1);
        }


        /// <summary>
        /// Modifikon rreshtin perkates ne databaze duke perdorur te dhenat qe jane tek objekti i lidhjes se autorizimit.
        /// </summary>
        public clsMesazh modifiko()
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.modifikoLidhjeAutorizim(this.IdLidhjeAutorizim, this.IdLidhese, this.IdLloji, this.IdAutorizimeKoka, 1);

        }

        /// <summary>
        /// Fshin rreshtin perkates nga tabela perkatese ne databaze sipas ID-se qe i eshte caktuar objektit te 
        /// lidhjes se autorizimit.
        /// </summary>
        public clsMesazh fshi()
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.fshiLidhjeAutorizim(this.IdLidhjeAutorizim);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idPerdoruesi">Id e perdoruesit</param>
        /// <param name="idLidhese">Id e objektit per te cilin do kontrollohet nese ka autorizim perdoruesi</param>
        /// <param name="llojBuxheti">Lloji i buxhetit</param>
        /// <returns>True - ka autorizim, False - nuk ka autorizim</returns>
        public static bool KaAutorizimPerdoruesi(int idPerdoruesi, int idLidhese, string llojBuxheti)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.KaAutorizimPerdoruesi(idPerdoruesi, idLidhese, llojBuxheti);
        }

        #endregion

        #region Metoda Internal

        internal bool mbushLidhjeAutorizim(DataRow dbDataRowLidhjeAutorizim)
        {
            if (dbDataRowLidhjeAutorizim != null)
            {
                try
                {
                    int.TryParse(dbDataRowLidhjeAutorizim["IDLIDHJEAUTORIZIM"].ToString(), out idLidhjeAutorizim);
                    int.TryParse(dbDataRowLidhjeAutorizim["IDLIDHESE"].ToString(), out idLidhese);
                    int.TryParse(dbDataRowLidhjeAutorizim["IDLLOJI"].ToString(), out idLloji);
                    int.TryParse(dbDataRowLidhjeAutorizim["IDAUTORIZIMEKOKA"].ToString(), out idAutorizimeKoka);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se lidhjes me autorizimin nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
