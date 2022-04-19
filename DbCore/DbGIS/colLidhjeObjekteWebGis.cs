using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class colLidhjeObjekteWebGis : List<clsLidhjeObjekteWebGis>
    {
        #region Konstruktori

        public colLidhjeObjekteWebGis()
        {
        }
        public colLidhjeObjekteWebGis(IEnumerable<clsLidhjeObjekteWebGis> collection)
            : base(collection)
        {
        }

        #endregion

        #region Metoda Publike

        public new clsLidhjeObjekteWebGis this[int index]
        {
            get
            {
                return ((clsLidhjeObjekteWebGis)base[index]);
            }
        }
        public bool shtoListe(clsLidhjeObjekteWebGis list)
        {
            this.Add(list);
            if (base.Contains(list))
                return true;
            else return false;
        }
        public bool ekzistonListe(clsLidhjeObjekteWebGis list)
        {
            if (base.Contains(list))
                return true;
            else return false;
        }
        //public bool merrTeGjitheListeAtributeSipasIdList()
        //{
        //    using (clsDatabaseGIS db = new clsDatabaseGIS())
        //    {
        //        return mbushLidhjeObjekteWebGis(db.merrTeGjitheObjekteWebGis());
        //    }
        //}
        public static DataTable MerrObjekteSipasBoundBoxit(int idNdermarrje, string boundBox, int idPerdoruesi, int idViti, int gjuha)
        {

            return new clsDatabaseGIS().MerrObjekteSipasBoundBoxit(idNdermarrje, boundBox, idPerdoruesi, idViti, gjuha);
        }
        public static DataTable ktheGjitheObjektetGIS(int idnderm, int idnderviti, int idgjuha)
        {
            clsDatabaseGIS dbGis = new clsDatabaseGIS();
            DataTable tabela = dbGis.ktheGjitheObjektetGISDT(idnderm, idnderviti, idgjuha);
            dbGis.Dispose();
            return tabela;
        }
        #endregion

        #region Metoda Interial
        private bool mbushLidhjeObjekteWebGis(DataTable dt)
        {
            try
            {
                for (int i = 0, count = dt.Rows.Count; i < count; i++)
                {
                    Add(new clsLidhjeObjekteWebGis(dt.Rows[i]));
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
