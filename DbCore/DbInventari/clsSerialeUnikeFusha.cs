using DbCore.IMBUtils.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbInventari
{
    public class clsSerialeUnikeFusha : IDataBase
    {

        #region Atribute

        private int id;
        private string fusha;
        private string emertimi;
        private bool isSelected;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos kategorine e serialit unik.
        /// </summary>
        public string Fusha
        {
            get { return fusha; }
            set { fusha = value; }
        }

        public string Emertimi
        {
            get { return emertimi; }
            set { emertimi = value; }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        #endregion
        

        #region Konstruktoret
        public clsSerialeUnikeFusha()
        {
        }
    

        public clsSerialeUnikeFusha(IDataRecord rreshti)
        {
            Mbush(rreshti);
        }
        #endregion


        #region Metoda Internal

        internal clsMesazh RuajLidhjeFushaKategori(int idKategori)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                return db.ruajLidhjeSerialeFushaKategori(id, idKategori, emertimi);
        }

        #endregion

        #region Metoda Publike

        public void MerrFusheSipasEmritFushes()
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                db.mbushSerialeFushSipasFushes(fusha, this);
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public void Mbush(IDataRecord record)
        {
            int.TryParse(record["ID"].ToString(), out id);
            fusha = record["FUSHA"].ToString();
            emertimi = Convert.ToString(record["EMERTIMI"]);
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Ruaj()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
