using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbCRM
{
    public class colKlientAnketa : System.Collections.Generic.List<clsKlientAnketa>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colKlientAnketa(int idklienti)
        {
            using (clsDatabaseCRM db = new clsDatabaseCRM())
            {
                mbushKlientAnketa(db.merrKlientAnketaSipasKlientit(idklienti));
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbCRM.clsKlientAnketa"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKlientAnketa this[int index]
        {
            get { return ((clsKlientAnketa)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsKlientAnketa ne nje arraylist
        /// </summary>
        public bool shtoKlientAnketa(clsKlientAnketa KlientAnketa)
        {
            base.Add(KlientAnketa);
            if (base.Contains(KlientAnketa))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsKlientAnketa ne nje arraylist
        /// </summary>
        public bool fshiKlientAnketa(clsKlientAnketa KlientAnketa)
        {
            base.Remove(KlientAnketa);
            if (base.Contains(KlientAnketa))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsKlientAnketa ne nje arraylist
        /// </summary>
        public bool fshiGjitheKlientAnketa()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsKlientAnketa ne nje arraylist
        /// </summary>
        public void fshiKeteKlientAnketa(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoKlientAnketaNeIndeksin(int index, clsKlientAnketa KlientAnketa)
        {
            base.Insert(index, KlientAnketa);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiKlientAnketa(clsKlientAnketa KlientAnketa)
        {
            return base.IndexOf(KlientAnketa);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonKlientAnketa(clsKlientAnketa KlientAnketa)
        {
            if (base.Contains(KlientAnketa))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriKlientAnketa()
        {
            return base.Count;
        }
        
        public static clsMesazh ruajAnketa(int idklienti, int idperdoruesi, List<object> idanketa, int idndermarje)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseCRM db = new clsDatabaseCRM();
            db.beginTransaksion();
            mesazh = db.fshiKlientAnketaStatusSipasKlientitAktive(idklienti, idperdoruesi);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }
            for (int i = 0; i < idanketa.Count; i++)
            {
                if (clsKlientAnketa.ekzistonKlientAnketa(idklienti, (int)idanketa[i], db))
                    continue;
                clsKokaAnketa kok = new DbCore.DbCRM.clsKokaAnketa((int)idanketa[i]);
                if (DbCore.DbCRM.clsKokaAnketa.kaPrerjeAnketashKlienti(idndermarje, idklienti, kok.DtFillimi, kok.DtMbarimi, db))
                {
                    db.rollbackTransaksion();
                    return new clsMesazh("Ka prerje te periudhave te anketes " + kok.Kodi + "  me anketat e tjera !");
                }
                DbCore.DbCRM.clsKlientAnketa klient = new DbCore.DbCRM.clsKlientAnketa(0, idklienti, (int)idanketa[i], 1, DateTime.Now, DateTime.Now, idperdoruesi, idperdoruesi, idndermarje);
                mesazh = klient.ruaj(db);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return mesazh;
                }
            }
            db.commitTransaksion();
            return mesazh;
        }
        
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbCRM.clsKlientAnketa"/> 
        /// </summary>
        private bool mbushKlientAnketa(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsKlientAnketa KlientAnketa = new clsKlientAnketa();
                    KlientAnketa.mbushKlientAnketa(rreshti);
                    this.Add(KlientAnketa);
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
