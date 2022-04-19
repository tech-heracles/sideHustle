using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbArkaBanka
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje Urdhere me objekte te tipit clsTrupiUrdherPagese
    ///  dhe lejon te manipulohet kjo Urdhere nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupiUrdherPagese : List<clsTrupiUrdherPagese>
    {
        #region Konstruktoret
        public colTrupiUrdherPagese()
        {

        }
        public colTrupiUrdherPagese(int idkoka)
        {
            using (clsDatabaseArkaBanka db = new clsDatabaseArkaBanka())
            {
                mbushTrupat(db.ktheGjitheTrupiUrdherPageseNgaKoka(idkoka));
            }
        }
        public colTrupiUrdherPagese(IEnumerable<clsTrupiUrdherPagese> collection)
            : base(collection)
        {

        }
        #endregion
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsTrupiUrdherPagese"/>  qe ndodhet ne nje index te caktuar te arrayUrdher-es
        /// </summary> 
        public new clsTrupiUrdherPagese this[int index]
        {
            get { return ((clsTrupiUrdherPagese)base[index]); }
        }

        /// <summary>
        /// mbush trupin e Urdherpageses
        /// </summary>
        /// <param name="idKoka">id e kokes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiUrdherPagese(int idKoka, clsDatabaseArkaBanka db)
        {
            //if (db == null)
            //    db = new clsDatabaseArkaBanka();
            return mbushTrupat(db.ktheGjitheTrupiUrdherPageseNgaKoka(idKoka));

        }


        /// <summary>
        /// kthen grupet
        /// </summary>
        /// <returns></returns>
        public colKonfigUrdherPagese ktheGrupe()
        {
            colKonfigUrdherPagese col = new colKonfigUrdherPagese();
            foreach (clsTrupiUrdherPagese trup in this)
            {
                clsKonfigUrdherPagese komp = new  clsKonfigUrdherPagese (trup.IdGrupi);
                col.Add(komp);
            }
            return col;
        }
        
        /// <summary>
        /// kthen tituj
        /// </summary>
        /// <returns></returns>
        public colKonfigUrdherPagese ktheTituj()
        {
            colKonfigUrdherPagese col = new colKonfigUrdherPagese();
            foreach (clsTrupiUrdherPagese trup in this)
            {
                clsKonfigUrdherPagese komp = new clsKonfigUrdherPagese(trup.IdTitulli);
                col.Add(komp);
            }
            return col;
        }
        /// <summary>
        /// kthen kapituj
        /// </summary>
        /// <returns></returns>
        public colKonfigUrdherPagese ktheKapituj()
        {
            colKonfigUrdherPagese col = new colKonfigUrdherPagese();
            foreach (clsTrupiUrdherPagese trup in this)
            {
                clsKonfigUrdherPagese komp = new clsKonfigUrdherPagese(trup.IdKapitulli);
                col.Add(komp);
            }
            return col;
        } 
        /// <summary>
        /// kthen llogari si artikuj
        /// </summary>
        /// <returns></returns>
        public DbCore.DbKontabiliteti.colLlogarite ktheLlogari()
        {
            DbCore.DbKontabiliteti.colLlogarite col = new DbCore.DbKontabiliteti.colLlogarite();
            foreach (clsTrupiUrdherPagese trup in this)
            {
                DbCore.DbKontabiliteti.clsLlogari komp = new  DbCore.DbKontabiliteti.clsLlogari (trup.IdLlogArtikulli);
                col.Add(komp);
            }
            return col;
        }
        /// <summary>
        /// kthen llogari si analiza
        /// </summary>
        /// <returns></returns>
        public DbCore.DbKontabiliteti.colLlogarite ktheLlogariAnaliza()
        {
            DbCore.DbKontabiliteti.colLlogarite col = new DbCore.DbKontabiliteti.colLlogarite();
            foreach (clsTrupiUrdherPagese trup in this)
            {
                DbCore.DbKontabiliteti.clsLlogari komp = new DbCore.DbKontabiliteti.clsLlogari(trup.IdLlogAnaliza);
                col.Add(komp);
            }
            return col;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsTrupiUrdherPagese"/> 
        /// </summary>
        private bool mbushTrupat(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrupiUrdherPagese trupi = new clsTrupiUrdherPagese();
                    //trupi.mbushTrupUrdherPagese(rreshti);
                    Add(new clsTrupiUrdherPagese(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion
    }
}
