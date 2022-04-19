using System.Collections.Generic;
using System.Data;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbShare
{
    public class colAtributeTrupi : List<clsAtributeTrupi>
    {
        #region Metoda Publike

        /// <summary>
        /// mbushet nje liste me obj te tipit AtributeTrupi
        /// ka te beje me konfigurmin e secilit kontroll te nje faqeje ne varesi te te dhenave qe kthehen nga dataseti
        /// perdoret per te hapur secilen faqe, regjistrim etj ne varesi te konfigurmit te saj te krijuar paraprakisht
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public new clsAtributeTrupi this[int index] => base[index];

        public void mbushAtributetKontrolleveKomponentes(int idKomponente, int idKategori)
        {
            using (var data = new clsDatabaseShare())
                MbushAtributetTrupiEng(data.merrAtributetKontrolleveKomponentes(idKomponente, idKategori));
        }

        public void mbushAtributetKontrolleveSipasKonfigurimit(int idkonf)
        {
            using (var data = new clsDatabaseShare())
                MbushAtributetTrupiEng(data.merrAtributetKontrolleveSipasKonfigurimit(idkonf));
        }

        public void mbushKontrolletKonfigurimitKomponentes(int idGjuha, int idKomponente, int idKonfigurim)
        {
            using (var data = new clsDatabaseShare())
                MbushAtributetTrupi(data.merrKontrolletKonfigurimitKomponentes(idGjuha, idKomponente, idKonfigurim));
        }

        public static colAtributeTrupi KrijoAtribute(object[] o, string lloji, int idNdermarrja)
        {
            var colAt = new colAtributeTrupi();
            foreach (var oo in o)
            {
                var atribut = new clsAtributeTrupi();
                int idNrAuto = 0;
                if (!string.IsNullOrEmpty(((Dictionary<string, object>)oo)["IdNrAutomatik"]?.ToString()))
                    int.TryParse(((Dictionary<string, object>)oo)["IdNrAutomatik"].ToString(), out idNrAuto);

                if ((lloji == "2" || lloji == "3") && idNrAuto != 0 && clsNrAutom.ktheIntervalSipasId(idNrAuto) == 0)
                    throw new MyException("Numri automatik " + clsNrAutom.ktheKodNrAutoSipasId(idNrAuto) + ", nuk ka te plotesuar fushen intervali!");

                atribut.KrijoArtibut((Dictionary<string, object>)oo, idNdermarrja);
                colAt.Add(atribut);
            }

            return colAt;
        }

        #endregion

        #region Metoda Private

        private void MbushAtributetTrupi(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
                Add(new clsAtributeTrupi(rreshti));
        }

        private void MbushAtributetTrupiEng(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                var trupi = new clsAtributeTrupi();
                trupi.mbushAtributTrupiEng(rreshti);
                Add(trupi);
            }
        }

        #endregion
    }
}
