using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DbCore.DbShare
{
    public class colKategoriArkive : System.Collections.Generic.List<clsKategoriArkive>
    {
        #region Konstruktoret

        public colKategoriArkive(){ }

        public colKategoriArkive(int idNdermarrje)
        {
            using (clsDatabaseShare db = new clsDatabaseShare())
            {
                mbushKategoriArkive(db.ktheKategoriArkiveSipasIdNdermarrje(idNdermarrje));
            }
        }

        public colKategoriArkive(int idNdermarrje, int idKatDok)
        {
            using (clsDatabaseShare db = new clsDatabaseShare())
            {
                mbushKategoriArkive(db.ktheKategoriArkiveSipasIdNdermarrjeDheIdKatDok(idNdermarrje, idKatDok));
            }
        }

        #endregion

        #region Metoda Publike

        public bool mbushKategoriArkiveSipasIdNdermarrjeDheIdKatDok(int idNdermarrje, int idKatDok)
        {
            using (clsDatabaseShare dbShare = new clsDatabaseShare())
            {
                return mbushKategoriArkive(dbShare.ktheKategoriArkiveSipasIdNdermarrjeDheIdKatDok(idNdermarrje, idKatDok));
            }
        }

        #endregion


        #region Metoda Internal

        private bool mbushKategoriArkive(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {                
                Add(new clsKategoriArkive(rreshti));
            }
            return true;
        }

        #endregion
    }
}