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
    /// <seealso cref="clsTrupiRecetaOptikeSyri"/>
    public class colTrupiRecetaOptikeSyri : List<clsTrupiRecetaOptikeSyri>, IDataBase
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        /// 

        public colTrupiRecetaOptikeSyri()
        {
            using (var db = new clsDatabaseRegjistrim())
            {
                db.ktheTrupinRecetaKokaPerDokTeRi(this);
            }
        }
        public colTrupiRecetaOptikeSyri(int idKoka)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                db.ktheGjitheTrupiRecetaNgaKoka(idKoka, this);
            }

        }

        public colTrupiRecetaOptikeSyri(int idNdermarrje, int idNdermViti)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                db.ktheGjitheTrupiRecetaSipasNdermarrjes(idNdermarrje, idNdermViti);
            }

        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKokaSkemaQK</param>
        public colTrupiRecetaOptikeSyri(IEnumerable<clsTrupiRecetaOptikeSyri> collection)
            : base(collection)
        {

        }


        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsTrupiRecetaOptikeSyri"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTrupiRecetaOptikeSyri this[int index]
        {
            get
            {
                return ((clsTrupiRecetaOptikeSyri)base[index]);
            }
        }
        /// <summary>
        /// mbush trupin e RECETES sipas id se kokes se recetes
        /// </summary>
        /// <param name="idkoka">id koka e planifikimit</param>
        /// <param name="db">clsDatabaseRegjistrim ne rast transaksioni</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public void mbushTrupRecete(int idkoka)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                db.ktheGjitheTrupiRecetaNgaKoka(idkoka, this);

        }



        public void Mbush(IDataRecord record)
        {
            Add(new clsTrupiRecetaOptikeSyri(record));
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

        public clsMesazh Modifiko() { throw new NotImplementedException(); }

        public clsMesazh Fshi() { throw new NotImplementedException(); }


        #endregion

        #region Metoda Private

        #endregion

    }
}
