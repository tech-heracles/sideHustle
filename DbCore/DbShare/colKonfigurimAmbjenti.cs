using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DbCore.DbShare
{
    public class colKonfigurimAmbjenti : System.Collections.Generic.List<clsKonfigurimAmbjenti>
    {
        #region Metoda Publike

        public new clsKonfigurimAmbjenti this[int index]
        {
            get { return ((clsKonfigurimAmbjenti)base[index]); }
        }

        public bool mbushKonfigDefaultKategori(int idKategori)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.merrKonfigDefaultKategori(idKategori));
            data.Dispose();
            return mbush;
        }
        public bool mbushKonfigDefaultKategori(int idKategori, int idNdermarrje)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.merrKonfigDefaultKategori(idKategori, idNdermarrje));
            data.Dispose();
            return mbush;
        }
        public bool mbushKonfigurimKategori(int idKategori, int idndermarje, int idperdorues)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.merrKonfigurimKategori(idKategori, idndermarje, idperdorues));
            data.Dispose();
            return mbush;
        }
        public bool mbushKonfigurimSuperKategori(int idSuperKategori, int idndermarje, int idperdorues)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.merrKonfigurimSipasSuperKategori(idSuperKategori, idndermarje, idperdorues));
            data.Dispose();
            return mbush;
        }
        public bool mbushGjitheKonfigurimetKomponentes(int idkomponente, int idndermarje, int idperdorues, int idGjuha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheGjitheKonfigurimetKomponentes(idkomponente, idndermarje, idperdorues, idGjuha));
            data.Dispose();
            return mbush;
        }

        public bool mbushGjitheKonfigurimeAmbjentesh(int idnderm, int idperdorues, int idsuperkat, int idGjuha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheGjitheKonfigurimeAmbjentesh(idnderm, idperdorues, idsuperkat, idGjuha));
            data.Dispose();
            return mbush;
        }

        public bool mbushGjitheKonfigurimeAmbjenteshMePershkrimEng(int idnderm, int idperdorues, int idsuperkat, int idGjuha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjentiMePershkrimEng(data.ktheGjitheKonfigurimeAmbjenteshMePershkrimEng(idnderm, idperdorues, idsuperkat, idGjuha));
            data.Dispose();
            return mbush;
        }

        public DataTable mbushGjitheKonfigurimeAmbjenteshDT(int idnderm, int idperdorues, int idsuperkat, int idGjuha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            DataTable dt = data.ktheGjitheKonfigurimeAmbjenteshDT(idnderm, idperdorues, idsuperkat, idGjuha);
            data.Dispose();
            return dt;
        }

        public bool mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(int idKategori, int idNivel, int idperdorues, bool meLloj=true)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjSipasIdKategoriIdNivelMeLloj(idKategori, idNivel, idperdorues, meLloj));
            data.Dispose();
            return mbush;
        }

        public bool mbushKonfigAmbjSipasIdKategoriIdNivel(int idKategori, int idNivel, int idperdorues, int idGjuha, bool meKonfVartes)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjSipasIdKategoriIdNivel(idKategori, idNivel, idperdorues, idGjuha, meKonfVartes));
            data.Dispose();
            return mbush;
        }
        public bool mbushKonfigAmbjSipasIdKategoriIdNivelDMT(int idKategori, int idNivel, int idperdorues, int idGjuha, bool doktransf)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjSipasIdKategoriIdNivelDokTrans(idKategori, idNivel, idperdorues, idGjuha, doktransf));
            data.Dispose();
            return mbush;
        }
        public bool mbushKonfigDefaultSipasKomponentes(int idKomponente, int idNdermarrje)
        {
            using (var data = new clsDatabaseShare())
                return mbushcolKonfigurimAmbjenti(data.merrKonfigDefaultKomponentes(idKomponente, idNdermarrje));
        }
        

        public bool mbushKonfigAmbjSipasIdKategoriKodNivel(int idKategori, string kodNivel, int idNdermarrje, int idperdorues, int idGjuha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjSipasKodKategoriIdNivel(idKategori, kodNivel, idNdermarrje, idperdorues, idGjuha));
            data.Dispose();
            return mbush;
        }

        public bool mbushKonfigAmbjSipasIdKategoriKodNivelPaVartese(int idKategori, string kodNivel, int idNdermarrje, int idperdorues, int idGjuha, bool kushtVartes)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjSipasKodKategoriIdNivelPaVartese(idKategori, kodNivel, idNdermarrje, idperdorues, idGjuha, kushtVartes));
            data.Dispose();
            return mbush;
        }
        public bool mbushKonfigAmbjVarteseSipasIdKategoriKodNivel(int idKategori, string kodNivel, int idNdermarrje, int idperdorues, int idGjuha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjVarteseSipasKodKategoriIdNivel(idKategori, kodNivel, idNdermarrje, idperdorues, idGjuha));
            data.Dispose();
            return mbush;
        }

        public bool mbushKonfigAmbjSipasIdKategoriIdNivel(int idKategori, int idNivel, int idperdorues, clsDatabaseShare data)
        {
            return mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjSipasIdKategoriIdNivelMeLloj(idKategori, idNivel, idperdorues));
        }

        public bool mbushKonfigAmbjSipasIdKategori(int idKategori, int idnderm, int idperdorues, int idGjuha, bool merrSipasAutorizimit = true,bool meLloj=true)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjSipasIdKategori(idKategori, idnderm, idperdorues, idGjuha, merrSipasAutorizimit, meLloj));
            data.Dispose();
            return mbush;
        }
        public bool mbushKonfigAmbjentiGjeneruarNgaFK(int idNdermarrje  )
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjentiGjeneruarNgaFK(idNdermarrje));
            data.Dispose();
            return mbush;
        }

        public bool mbushKonfigAmbjSipasKategorive(string idKategori, int idnderm, int idperdorues, int idGjuha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjSipasKategorive(idKategori, idnderm, idperdorues, idGjuha));
            data.Dispose();
            return mbush;
        }

        public bool mbushKonfigAmbjSipasIdKategoriPaKonfVartese(int idKategori, int idnderm, int idperdorues, int idGjuha, bool merrSipasAutorizimit = true, bool meLloj = true)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjSipasIdKategoriPaKonfVartese(idKategori, idnderm, idperdorues, idGjuha, merrSipasAutorizimit, meLloj));
            data.Dispose();
            return mbush;
        }

        public static DataTable ktheKonfigAmbjSipasIdKategoriDTSmall(int idKategori, int idnderm, int idperdorues, int idGjuha)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
               return data.ktheKonfigAmbjSipasIdKategoriDTSmall(idKategori, idnderm, idperdorues, idGjuha);
            }
         
        }

        public bool mbushKonfigAmbjSipasKategoriveDheGjuhes(string idKategorite, int idnderm, int idperdorues, int idGjuha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjentiMePershkrimEng(data.mbushKonfigAmbjSipasKategoriveDheGjuhes(idKategorite, idnderm, idperdorues, idGjuha));
            data.Dispose();
            return mbush;
        }
        public bool mbushKonfigAmbjSipasIdKategoriMePershkrimEngMeKusht(int idKategori, int idnderm, int idperdorues,string kushti, int idGjuha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjentiMePershkrimEng(data.ktheKonfigAmbjSipasIdKategoriMePershkrimEngMeKusht(idKategori, idnderm, idperdorues, kushti, idGjuha));
            data.Dispose();
            return mbush;
        
        }
        
        public bool mbushKonfigAmbjSipasIdKategoriJoVartese(int idKategori, int idnderm, int idperdorues)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjSipasIdKategoriJoVartese(idKategori, idnderm, idperdorues));
            data.Dispose();
            return mbush;
        }
        
        public bool mbushKonfigAmbjSipasIdKomponente(int idkomponente, int idnderm, int idperdorues)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushcolKonfigurimAmbjenti(data.ktheKonfigAmbjSipasIdKomponente(idkomponente, idnderm, idperdorues));
            data.Dispose();
            return mbush;
        }

        #endregion

        #region Metoda Private

        private bool mbushcolKonfigurimAmbjenti(DataTable dt)
        {
           
                foreach (DataRow rreshti in dt.Rows)
                {

                    Add(new clsKonfigurimAmbjenti(rreshti));
                }
            return true;
        }
        private bool mbushcolKonfigurimAmbjentiMePershkrimEng(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsKonfigurimAmbjenti trupi = new clsKonfigurimAmbjenti();
                    trupi.mbushKonfigurimAmbjentiShtim(rreshti);
                    Add(trupi);
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
