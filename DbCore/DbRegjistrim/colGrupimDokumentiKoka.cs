using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class colGrupimDokumentiKoka : System.Collections.Generic.List<clsGrupimDokumentiKoka>
    {


        #region Konstruktoret

        public colGrupimDokumentiKoka() { }
        public colGrupimDokumentiKoka(IEnumerable<clsGrupimDokumentiKoka> grupet) : base(grupet)
        {

        }
        public colGrupimDokumentiKoka(int idndermarje, List<int> grupet, int idkonfig, int idPerdorues) : this(new clsDatabaseRegjistrim().merrGrupimSipasGrupeveDheKonfigurimit(idndermarje, grupet, idkonfig, idPerdorues))
        {

        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsGrupimDokumentiKoka"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsGrupimDokumentiKoka this[int index]
        {
            get { return ((clsGrupimDokumentiKoka)base[index]); }
        }


        /// <summary>
        /// mbush gjithe kodifikimet e artikullit sipas ndermarrjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        public bool mbushGjitheGrupetDokSipasNdermarrjes(int idndermarje, int idPerdorues)
        {
            using (clsDatabaseRegjistrim dbKodifikimKF = new clsDatabaseRegjistrim())
            {
                return mbushGrupet(dbKodifikimKF.ktheGjitheGrupetSipasNdermarrjes(idndermarje, idPerdorues));
            }
        }

        public bool merrGrupeSipasGrupit(int grupi, int idndermarrje, int idPerdorues)
        {
            using (clsDatabaseRegjistrim dbKodifikimKF = new clsDatabaseRegjistrim())
            {
                return mbushGrupet(dbKodifikimKF.merrGrupDokumentashSipasGrupit(grupi, idndermarrje, idPerdorues));
            }
        }

        public bool merrGrupeDokSipasGrupitDtSmall(int grupi, int idndermarrje, int idPerdorues)
        {
            using (clsDatabaseRegjistrim dbKodifikimKF = new clsDatabaseRegjistrim())
            {
                return mbushGrupetDtSmall(dbKodifikimKF.merrGrupDokumentashSipasGrupitDtSmall(grupi, idndermarrje, idPerdorues));
            }
        }
        public static DataTable merrGrupeSipasGrupitDtSmall(int grupi, int idndermarrje, int idPerdorues)
        {
            using (clsDatabaseRegjistrim dbKodifikimKF = new clsDatabaseRegjistrim())
            {
                return dbKodifikimKF.merrGrupDokumentashSipasGrupitDtSmall(grupi, idndermarrje, idPerdorues);
            }

        }

        public bool merrGrupeSipasKonfigurimit(int grupi, int idndermarrje, int idkonfig, int idPerdorues)
        {
            using (clsDatabaseRegjistrim dbKodifikimKF = new clsDatabaseRegjistrim())
            {
                return mbushGrupet(dbKodifikimKF.merrGrupimSipasGrupitDheKonfigurimit(idndermarrje, grupi, idkonfig, idPerdorues));
            }
        }
        public bool merrGrupeSipasKategorise(int grupi, int idndermarrje, string idkatdok, int idPerdorues)
        {
            using (clsDatabaseRegjistrim dbKodifikimKF = new clsDatabaseRegjistrim())
            {
                return mbushGrupet(dbKodifikimKF.merrGrupimSipasGrupitDheKategorise(idndermarrje, grupi, idkatdok, idPerdorues));
            }
        }

        public static colGrupimDokumentiKoka[] ktheGrupimDokumentashNderm(string kodkonfig, int idNdermarrje, int idPerdoruesi)
        {
            colGrupimDokumentiKoka[] result = new colGrupimDokumentiKoka[3];
            DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasKod(kodkonfig, idNdermarrje);
            colGrupimDokumentiKoka col = new colGrupimDokumentiKoka();            
            col.merrGrupeSipasKonfigurimit(1, idNdermarrje, konf.IdKonfigAmbjente, idPerdoruesi);
            result[0] = col;
            col = new colGrupimDokumentiKoka();            
            col.merrGrupeSipasKonfigurimit(2, idNdermarrje, konf.IdKonfigAmbjente, idPerdoruesi);
            result[1] = col;
            col = new colGrupimDokumentiKoka();
            col.merrGrupeSipasKonfigurimit(3, idNdermarrje, konf.IdKonfigAmbjente, idPerdoruesi);
            result[2] = col;
            return result;
        }

        #endregion


        #region Metoda Private

        private bool mbushGrupet(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                //clsGrupimDokumentiKoka grupKF = new clsGrupimDokumentiKoka();
                //grupKF.mbushGrup(rreshti);
                this.Add(new clsGrupimDokumentiKoka(rreshti));
            }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        private bool mbushGrupetDtSmall(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                clsGrupimDokumentiKoka grupKF = new clsGrupimDokumentiKoka();
                grupKF.mbushGrupDtSmall(rreshti);
                this.Add(grupKF);
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