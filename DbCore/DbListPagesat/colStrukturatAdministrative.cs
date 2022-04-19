using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using CacheLayer;
using DbCore.IMBUtils.Cache;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsStrukturaAdministrative
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsStrukturaAdministrative"/>
    public class colStrukturatAdministrative : List<clsStrukturaAdministrative>, IDataBaseReader
    {
        public int idNdermarrje { get; private set; }
        public int IdPrindi { get; private set; }
        public string IdPrinderit { get; private set; }

        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        /// 
        public colStrukturatAdministrative()
        {

        }
        /// <summary>
        /// merr gjithe strukturat administrative te punonjesve
        /// </summary>
        /// <param name="punonjesitIDs"></param>
        public colStrukturatAdministrative(List<int> punonjesitIDs)
        {
            using (var db = new clsDatabazeListPagesa())
                db.MerrStruktruraAdministrative(punonjesitIDs, this);
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// merr strukturat administrative te ndermarjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colStrukturatAdministrative(int idndermarje)
        {
            mbushGjitheStrukturaAdmPrindiSipasNdermarjes(idndermarje,false);
        }

        /// <summary>
        ///  konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsStrukturaAdministrative </param>
        public colStrukturatAdministrative(IEnumerable<clsStrukturaAdministrative> collection) : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsStrukturaAdministrative"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsStrukturaAdministrative this[int index] => ((clsStrukturaAdministrative)base[index]);

        /// <summary>
        /// merr strukturat sipas ndermarje ne forme data table 
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto struktura</returns>
        public static DataTable merrStruktureAdmNdermarjeDT(int idnderm)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrStrukturaAdmNdermarjeDT(idnderm);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// mbush te gjitha strukturat sipas ndermarrjes
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public void mbushGjitheStrukturaAdmSipasNdermarjes(int idnder)
        {
            idNdermarrje = idnder;
            var session = GlobalCacheManager.MySessionCache.FillObjectFromCache<colStrukturatAdministrative>(x => x.idNdermarrje == idnder, () =>
            {
                using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                    db.ktheGjitheStrukturatAdmSipasNdermarjes(idnder, this);
                return this;
            }, this, "TeGjithaSipasNdermarrjes");
        }

        /// <summary>
        /// mbush te gjitha strukturat administrative prind sipas ndermarrjes
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public void mbushGjitheStrukturaAdmPrindiSipasNdermarjes(int idnder,bool merrtegjitha)
        {
            GlobalCacheManager.MySessionCache.FillObjectFromCache<colStrukturatAdministrative>(x => x.idNdermarrje == idnder,
                () =>
                {
                    using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                        db.ktheGjitheStrukturaAdministrativePrindiSipasNdermarjes(idnder, merrtegjitha,this);
                    return this;
                }, this, "PrindiSipasNdermarjes");

        }
        /// <summary>
        /// mbush te gjitha strukturat administrative prind sipas ndermarrjes ne nivel raportues
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public void mbushGjitheStrukturaAdmPrindiSipasNdermarjesRaportuese(int idnder)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                db.ktheGjitheStrukturaAdministrativePrindiSipasNdermarjesRaportuese(idnder, this);
        }
        /// <summary>
        /// merr struktura adm bij te nje strukture adm  prind
        /// </summary>
        /// <param name="idprindi">id e prindit</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public void mbushStrukturaAdmSipasPrindit(int idprindi,bool merrtegjitha)
        {
            IdPrindi = idprindi;
            GlobalCacheManager.MySessionCache.FillObjectFromCache<colStrukturatAdministrative>(x => x.IdPrindi == idprindi, () =>
              {
                  using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                      db.ktheStruktureSipasPrindit(idprindi, merrtegjitha, this);
                  return this;
              }, this, "sipasPrindit");

        }
        /// <summary>
        /// merr strukturat administrative bija te shume strukturave adm prind
        /// </summary>
        /// <param name="idPrinderit"></param>
        /// <returns></returns>
        public void mbushStrukturaAdmSipasShumePrind(string idPrinderit)
        {
            IdPrinderit = idPrinderit;
            GlobalCacheManager.MySessionCache.FillObjectFromCache<colStrukturatAdministrative>(x => x.IdPrinderit == idPrinderit, () =>
           {
               using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                   db.ktheStrukturatBijaSipasStruktPrind(idPrinderit, this);
               return this;
           }, this, "sipasPrinderve");
        }

        #endregion

        #region Metoda Private

        public void Mbush(IDataRecord record)
        {
            Add(new clsStrukturaAdministrative(record));
        }

        #endregion
    }
}
