using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DbCore.DbShare
{
    public class colMenuItem : List<clsMenuItem>
    {
        #region Metoda publike

        public colMenuItem(int idgjuha)
        {

        }

        public void mbushMenuItem(int idgjuha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            //data.krijoManager();
            mbushMenuItem(idgjuha, data.merrGjitheMenuItems());
            data.Dispose();
        }

        public new clsMenuItem this[int index]
        {
            get { return ((clsMenuItem)base[index]); }
        }

        public colMenuItem merrMenuItem(int idgjuha)
        {
            if (this.Count == 0)
                mbushMenuItem(idgjuha);
            return this;
        }

        public bool merrMenuItemSipasName(int idgjuha, string name)
        {
            clsDatabaseShare Db = new clsDatabaseShare();
            bool mbush = mbushMenuItem(idgjuha, Db.merrMenuItemSipasName(name));
            Db.Dispose();
            return mbush;
        }
        
        //[Obsolete("Perdor: merrMenuItemSipasKomponentes(String emerKomponente,int idPerdoruesi, int idNdermarrje, int idViti)", false)]
        public bool merrMenuItemSipasKomponentes(int idgjuha, int idkomponente)
        {
            clsDatabaseShare Db = new clsDatabaseShare();
            bool mbush = mbushMenuItem(idgjuha, Db.merrMenuItemSipasKomponentes(idkomponente));
            Db.Dispose();
            return mbush;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emerKomponente"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>     
        /// <param name="shtim"></param>
        /// <returns></returns>
        public bool merrMenuItemSipasKomponentes(int idgjuha,String emerKomponente,int idPerdoruesi, int idNdermarrje, int idViti, bool shtim)
        {
            clsDatabaseShare Db = new clsDatabaseShare();
            bool mbush = mbushMenuItem(idgjuha, Db.merrMenuItemSipasKomponentes(emerKomponente, idPerdoruesi, idNdermarrje, idViti, shtim));
            Db.Dispose();
            return mbush;
        }

        /// <summary>
        /// Mbush objektin colMenuItems me menuitems te komponentes dhe me te drejtat perkatese. Perdoret per ambjentet e regjistrimit, ku jane edhe menute draft.
        /// </summary>
        /// <param name="emerKomponente"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>     
        /// <param name="shtim"></param>
        /// <returns></returns>
        public bool merrMenuItemSipasKomponentesRegjistrime(int idgjuha, String emerKomponente, int idPerdoruesi, int idNdermarrje, int idViti, bool shtim)
        {
            clsDatabaseShare Db = new clsDatabaseShare();
            bool mbush = mbushMenuItem(idgjuha, Db.merrMenuItemSipasKomponentesRegjistrime(emerKomponente, idPerdoruesi, idNdermarrje, idViti, shtim));
            Db.Dispose();
            return mbush;
        }

        /// <summary>
        /// Mbush objektin colMenuItems me menuitems te komponentes dhe me te drejtat perkatese. Perdoret per ambjentet e regjistrimit, ku jane edhe menute draft.
        /// </summary>
        /// <param name="emerKomponente"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>     
        /// <param name="shtim"></param>
        /// <param name="idNivelRegjistrimi"></param>
        /// <returns></returns>
        public bool merrMenuItemSipasKomponentesRegjistrimeDheNivelRegjistrimi(int idgjuha, String emerKomponente, int idPerdoruesi, int idNdermarrje, int idViti, int idNivelRegjistrimi, bool shtim)
        {
            clsDatabaseShare Db = new clsDatabaseShare();
            bool mbush = mbushMenuItem(idgjuha, Db.merrMenuItemSipasKomponentesRegjistrimeDheNivelRegjistrimi(emerKomponente, idPerdoruesi, idNdermarrje, idViti, idNivelRegjistrimi, shtim));
            Db.Dispose();
            return mbush;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emerKomponente"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>     
        /// <param name="shtim"></param>
        /// <returns></returns>
        public bool merrMenuItemPerKubin(int idgjuha, String emerKomponente, int idPerdoruesi, int idNdermarrje, int idViti, bool shtim)
        {
            clsDatabaseShare Db = new clsDatabaseShare();
            bool mbush = mbushMenuItem(idgjuha, Db.merrMenuItemPerRaporte(emerKomponente, idPerdoruesi, idNdermarrje, idViti, shtim));
            Db.Dispose();
            return mbush;
        }
       
        #endregion

        #region Metoda private

        private bool mbushMenuItem(int idgjuha, DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {

                    //clsMenuItem koka = new clsMenuItem(idgjuha);
                    //koka.mbushMenuItem(idgjuha, rreshti);
                    this.Add(new clsMenuItem(idgjuha, rreshti));
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
