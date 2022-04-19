using System.Collections.Generic;
using System.Data;

namespace DbCore.DbShare
{
    public class colParameter : List<clsParameter>
    {
        #region Ctor

        public colParameter() { }

        public colParameter(int idSp)
        {
            using (var shareDb = new clsDatabaseShare())
                Mbush(shareDb.merrParametraSp(idSp));
        }

        public colParameter(int idSp, int idRaporti)
        {
            using (var shareDb = new clsDatabaseShare())
                Mbush(shareDb.merrParametraSpRaport(idSp, idRaporti));
        }

        #endregion

        #region Metoda Publike
        
        /// <summary>
        /// Kthen nje colloection me parametrat e perbashket te raporteve qe i kalohen si param.
        /// </summary>
        /// <param name="idRaport1">ID e raportit te pare</param>
        /// <param name="idRaport2">ID e raportit te dyte</param>
        /// <returns></returns>
        public static colParameter merrParametraPebashketRaportesh(int idRaport1, int idRaport2)
        {
            using (var dbshare = new clsDatabaseShare())
            {
                var param = new colParameter();
                param.Mbush(dbshare.merrParametatPerbshketRapotesh(idRaport1, idRaport2));
                return param;
            }
        }

        #endregion

        #region Metoda Private

        private void Mbush(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsParameter(rreshti));
            }
        }

        #endregion
    }
}
