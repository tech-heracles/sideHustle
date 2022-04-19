using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{

    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaSkemaWorkFlow
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokaSkemaWorkFlow : System.Collections.Generic.List<clsKokaSkemaWorkFlow>
    {

        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colKokaSkemaWorkFlow()
        {

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        public colKokaSkemaWorkFlow(int idnderm)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            mbushKokaSkema(db.ktheKokaSkemaWorkFlowSipasNdermarrjes(idnderm));
            db.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKokaSkemaWorkFlow"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKokaSkemaWorkFlow this[int index]
        {
            get { return ((clsKokaSkemaWorkFlow)base[index]); }
        }
     
        public static DataRow merrKokaSkemaWorkFlowNdermarrjesDR(int idkoka)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataRow rreshti = dbartikuj.merrSipasKokaSkemaWorkFlowDR(idkoka);
            dbartikuj.Dispose();
            return rreshti;

        }
        public static DataTable merrKokaSkemaWorkFlowNdermarrjesDT(int idnderm)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataTable tabela = dbartikuj.merrKokaSkemaWorkFlowNdermarrjesDT(idnderm);
            dbartikuj.Dispose();
            return tabela;
        }
        public static DataTable merrAprovuesDokumenti(int idKoka)
            {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataTable tabela = dbartikuj.merrAprovuesDokumenti(idKoka);
            dbartikuj.Dispose();
            return tabela;
            }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="clsKokaSkemaWorkFlow"/> 
        /// </summary>
        private bool mbushKokaSkema(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaSkemaWorkFlow koka = new clsKokaSkemaWorkFlow();
                    //koka.mbushKokaSkema(rreshti);
                    this.Add(new clsKokaSkemaWorkFlow(rreshti));
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
