using System.Data;

namespace DbCore.DbShare
{
    public class colRaporti : System.Collections.Generic.List<clsRaporti>
    {
        public new clsRaporti this[int index] => base[index];

        #region Ctor
        
        public colRaporti(int idGjuha, int idSuperRaporti)
        {
            using (var databaseShare = new clsDatabaseShare())
                Mbush(idGjuha, databaseShare.ktheGjitheSubRaportet(idSuperRaporti));
        }

        public colRaporti(int idGjuha)
        {
            using (var databaseShare = new clsDatabaseShare())
                Mbush(idGjuha, databaseShare.merrTeGjitheRaportetMeFormatPrintimi());
        }

        #endregion

        #region Metoda Private

        private void Mbush(int idGjuha, DataTable dt) 
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsRaporti(idGjuha, rreshti));
            }
        }

        #endregion
    }
}
