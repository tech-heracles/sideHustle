using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiRecetaOptike
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsTrupiRecetaOptikePunime"/>
    public class colTrupiRecetaOptikePunime : List<clsTrupiRecetaOptikePunime>, IDataBase
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colTrupiRecetaOptikePunime()
        {
        }
        public colTrupiRecetaOptikePunime(int idKoka)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                db.ktheTrupiRecetaPunimeSipasID(idKoka, this);
            }

        }
        public colTrupiRecetaOptikePunime(int idNdermarrje, int idNdermViti)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                db.ktheTrupiRecetaPunimeSipasNdermarrjes(idNdermarrje, idNdermViti);
            }

        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKokaSkemaQK</param>
        public colTrupiRecetaOptikePunime(IEnumerable<clsTrupiRecetaOptikePunime> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsTrupiRecetaOptikePunime"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTrupiRecetaOptikePunime this[int index]
        {
            get
            {
                return ((clsTrupiRecetaOptikePunime)base[index]);
            }
        }

        public clsMesazh Ruaj()
        {

            foreach (var trup in this)
            {
                var mesazh = trup.Ruaj();
                if (!mesazh) return mesazh;
            }
            return new MesazhSuksesi("Ruajtja u krye me sukses!");
        }

        public void RiRendit()
        {

        }

        public void Mbush(IDataRecord record)
        {
            Add(new clsTrupiRecetaOptikePunime(record));
        }

        public clsMesazh Modifiko()
        {
            foreach (var trup in this)
            {
                var mesazh = trup.Modifiko();
                if (!mesazh) return mesazh;
            }
            return new clsMesazh(true, "Modifikimi u krye me sukses!");
        }

        public clsMesazh Fshi()
        {
            foreach (var trup in this)
            {
                var mesazh = trup.Fshi();
                if (!mesazh) return mesazh;
            }
            return new clsMesazh(true, "Fshirja përfundoi me sukses!");
        }

        #endregion

        #region Metoda Private




        #endregion


    }
}

