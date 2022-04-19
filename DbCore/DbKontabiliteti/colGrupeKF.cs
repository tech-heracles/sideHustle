using System.Collections.Generic;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    public class colGrupeKF : List<clsGrupeKF>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsGrupeKF"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsGrupeKF this[int index] => base[index];

        public void MerrGrupeKfSipasLlojit(int llojkodifikimi, int idndermarrje)
        {
            using (var databaseKontabilitet = new clsDatabaseKontabilitet())
                Mbush(databaseKontabilitet.merrGrupKFSipasLlojit(llojkodifikimi, idndermarrje));
        }

        public static DataTable KtheGrupimKlienteFurnitore(int idndermarje)
        {
            using (var dbgrupimklientfurnitor = new clsDatabaseKontabilitet())
                return dbgrupimklientfurnitor.ktheGrupimKlienteFurnitorePerEksport(idndermarje);
        }

        public void MerrGrupeKfSipasLlojKodifikimiDheLlojKf(int llojkodifikimi, int idndermarrje, int llojikf)
        {
            using (var databaseKontabilitet = new clsDatabaseKontabilitet())
                Mbush(databaseKontabilitet.merrGrupKFSipasLlojitKodifikimDheLlojKF(llojkodifikimi, idndermarrje, llojikf));
        }

        public void MerrGrupeKfSipasLlojitKf(int idndermarrje, int llojikf)
        {
            using (var databaseKontabilitet = new clsDatabaseKontabilitet())
                Mbush(databaseKontabilitet.merrGrupKFSipasLlojitKF(idndermarrje, llojikf));
        }

        public void MbushGjitheGrupetKfSipasNdermarrjesJoPrind(int idndermarje, int llojkodifikim)
        {
            using (var databaseKontabilitet = new clsDatabaseKontabilitet())
                Mbush(databaseKontabilitet.ktheGjitheGrupetKFSipasNdermarrjesJoPrind(idndermarje, llojkodifikim));
        }

        public void MbushGjitheGrupetKfSipasNdermarrjesJoPrindDheLlojit(int idndermarje, int llojkodifikimi, bool llojikf)
        {
            using (var databaseKontabilitet = new clsDatabaseKontabilitet())
                Mbush(databaseKontabilitet.mbushGjitheGrupetKFSipasNdermarrjesJoPrindDheLlojit(idndermarje, llojkodifikimi, llojikf));
        }

        public void KtheBijte(int idGrup, int idNdermarje)
        {
            using (var databaseKontabilitet = new clsDatabaseKontabilitet())
                Mbush(databaseKontabilitet.ktheBijte(idGrup, idNdermarje));
        }

        #endregion

        #region Metoda Private

        private void Mbush(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsGrupeKF(rreshti));
            }
        }

        #endregion
    }
}
