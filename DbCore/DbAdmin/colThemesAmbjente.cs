using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.SessionState;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsThemesAmbjente
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colThemesAmbjente : System.Collections.Generic.List<clsThemesAmbjente>, IDataBaseReader
    {
        #region Metoda Publike
        /// <summary>
        /// kthen objektin <see cref="DbCore.DbAdmin.clsThemesAmbjente"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsThemesAmbjente this[int index]
        {
            get { return ((clsThemesAmbjente)base[index]); }
        }


        /// <summary>
        /// mbush gjithe themes sipas ndermarrjes dhe perdoruesit
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <param name="idperd">id e perdoruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        //public bool mbushGjitheThemeAmbjenteSipasNdermPerd(int idperd)
        //{
        //    clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
        //    bool sukses = mbushThemesAmbjentet(dbAdmin.ktheGjitheThemesAmbjenteSipasNdermPerd(idperd));
        //    dbAdmin.Dispose();
        //    return sukses;
        //}

        /// <summary>
        /// mbush gjithe themes sipas ndermarrjes dhe perdoruesit
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <param name="idperd">id e perdoruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        public static DataTable merrgjitheThemesAmbjentePerPerd(int idperd)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            DataTable tabela = dbAdmin.ktheGjitheThemesAmbjenteSipasNdermPerd(idperd);
            dbAdmin.Dispose();
            return tabela;
        }

        //public bool merrGjitheThemesAmbjenteVjeterZgjedhurPerPerd(int idperd)
        //{
        //    clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
        //    dbAdmin.ktheDataTableThemeZgjedhurPerdorues(idperd,this);
        //    dbAdmin.Dispose();
        //    return true;
        //}

        public bool merrGjitheThemesAmbjenteVjeterZgjedhurPerPerd(int idperd, clsDatabaseAdmin dbAdmin)
        {
            try
            {
                dbAdmin.ktheDataTableThemeZgjedhurPerdorues(idperd, this);
                return true;
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return false;
            }
        }

        #endregion

        #region Metoda Private

        public void Mbush(IDataRecord dataRecord)
        {
            Add(new clsThemesAmbjente(dataRecord));

        }

        #endregion
    }
}
