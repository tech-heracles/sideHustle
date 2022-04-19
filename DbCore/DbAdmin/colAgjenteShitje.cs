using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{

    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsDrejtim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// 
    public class colAgjenteShitje : System.Collections.Generic.List<clsAgjentShitje>
    {
        #region konstruktoret

        public colAgjenteShitje()
        {
        }
        public colAgjenteShitje(IEnumerable<clsAgjentShitje> collection) : base(collection) { }
        public colAgjenteShitje(int idndermarje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushAgjentetShitjes(data.ktheGjitheAgjentetShitjes(idndermarje));
            }
        }
        public colAgjenteShitje(int idndermarje, int idperdorues)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushAgjentetShitjes(data.ktheGjitheAgjentetShitjesNdermarrjeAutorizimDT(idndermarje, idperdorues));
            }
        }
        //ktheGjitheAgjentetShitjesNdermarrjeAutorizimDT
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbAdmin.clsAgjentShitje"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>      
        public new clsAgjentShitje this[int index]
        {
            get { return ((clsAgjentShitje)base[index]); }
        }


        public static DataTable merrAgjenteShitjeSipasAutorizimeDt(int idNdermarrje, int idPerdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataTable tabela = data.ktheGjitheAgjentetShitjesNdermarrjeAutorizimDT(idNdermarrje, idPerdorues);
            data.Dispose();
            return tabela;

            //clsDatabaseKontabilitet dbartikuj = new clsDatabaseKontabilitet();
            //DataTable tabela = dbartikuj.ktheKFNdermarrjesAndAutorizimeDT(idNdermarrje, idPerdorues);
            //dbartikuj.Dispose();
            //return tabela;
        }

        public static DataRow merrSipasAgjenteveNdermarrjesAndAutorizimeDR(int idnderm, int idperdorues, int idagjenti)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            DataRow rreshti = db.kthAgjentetNdermarrjesAndAutorizimeDR(idnderm, idperdorues, idagjenti);
            db.Dispose();
            return rreshti;
        }
        #endregion

        #region Metoda Private
        
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataseti
        /// nepermjet kesaj metode informacioni kalohet nga dataset-i ne nje liste me objekte te tipit 
        ///  <see cref="DbCore.DbAdmin.clsAgjentShitje"/> 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private bool mbushAgjentetShitjes(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    Add(new clsAgjentShitje(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion 
    }
}
