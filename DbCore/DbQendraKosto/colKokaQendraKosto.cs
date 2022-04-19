using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaQendraKosto
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokaQendraKosto : List<clsKokaQendraKosto>
    {

        #region Konstruktor

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colKokaQendraKosto() { }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKokaPlanifikim</param>
        public colKokaQendraKosto(IEnumerable<clsKokaQendraKosto> collection) : base(collection) { }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// merr gjithe qendra kosto sipas inderviti dhe autorizimeve ne forme data table
        /// </summary>
        /// <param name="idndermvit">id e ndermarje vitit</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <returns>data table me keto te dhena</returns>
        public static DataTable merrKokaQendraKostoDT(int idndermvit, int idperdoruesi)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            DataTable dt = db.merrKokaQenderKostoDT(idndermvit, idperdoruesi);
            db.Dispose();
            return dt;
        }

        ///// <summary>
        ///// merr gjithe dokumentat e qendrave te kostove per dokumentat e magazines ku ndodhet artikulli
        ///// </summary>
        ///// <param name="idartikulli"></param>
        ///// <param name="idmagazina"></param>
        ///// <param name="datanga"></param>
        ///// <param name="dataderi"></param>
        //public colKokaQendraKosto(int idartikulli, int idmagazina, DateTime datanga, DateTime dataderi)
        //{
        //    using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
        //    {
        //        mbushKokaQendraKostoPaTrup(db.ktheReshtaDokQKPerRivleresimFifoArtikulli(idartikulli, idmagazina, datanga, dataderi));
        //    }
        //}

        public colKokaQendraKosto(int idartikulli, int idmagazina, DateTime datanga, DateTime dataderi, int maxRetry, DbAdmin.clsLogRivleresimInventari log)
        {
            int retry = maxRetry;
            do
            {
                try
                {
                    using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
                    {
                        mbushKokaQendraKostoPaTrupRivleresim(db.ktheReshtaDokQKPerRivleresimFifoArtikulli(idartikulli, idmagazina, datanga, dataderi));
                    }
                    return;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    if (ex.Number == 1205 || ex.Number == 121 || ex.Number == 1236)
                    {
                        System.Threading.Thread.Sleep(1435);
                        log.logRetry(maxRetry - retry + 1, String.Format("colKokaQendraKosto({0},{1},{2},{3})", idartikulli, idmagazina, datanga, dataderi), ex.Number, ex.Message);
                        if (--retry == 0)
                        {
                            log.logError(maxRetry + ": " + MessagesResource.Messages["errorMessageTentativatPerRivleresimMbaruan"]);
                            throw new MyException(maxRetry + ": " + MessagesResource.Messages["errorMessageTentativatPerRivleresimMbaruan"]);
                        }
                    }
                    else
                    {
                        log.logError(ex.Number, ex.Message);
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    log.logError(ex.Message);
                    throw ex;
                }
            } while (retry > 0);
            throw new MyException(MessagesResource.Messages["errorNukUArritLeximiI"] + String.Format("colKokaQendraKosto({0},{1},{2},{3})", idartikulli, idmagazina, datanga, dataderi));
        }

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbProdhimi.clsKokaQendraKosto"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKokaQendraKosto this[int index]
        {
            get { return ((clsKokaQendraKosto)base[index]); }
        }
        
        #endregion

        #region Metoda Private

        private bool mbushKokaQendraKostoPaTrupRivleresim(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsKokaQendraKosto(rreshti, true));
            }
            return true;
        }

        #endregion

    }
}
