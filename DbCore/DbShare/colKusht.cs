using System.Collections.Generic;
using System.Data;

namespace DbCore.DbShare
{
    public class colKusht : List<clsKusht>
    {
        #region Metoda Publike

        public new clsKusht this[int index] => base[index];
        
        public void mbushGjitheKushteDefaultNivel(int idNivel, int idKomponente)
        {
            using (var data = new clsDatabaseShare())
                MbushKushtet(data.ktheGjitheKushteDefaultNivel(idNivel, idKomponente));
        }

        public static DataTable mbushGjitheKushteAlternativa(int idKonfAmbjente)
        {
            using (var data = new clsDatabaseShare())
                return data.ktheGjitheKushteAlternativa(idKonfAmbjente);
        }

        public void mbushGjitheKushteKonfigurimi(int idKonfAmbjente)
        {
            using (var data = new clsDatabaseShare())
                MbushKushtet(data.ktheGjitheKushteKonfigurimi(idKonfAmbjente));
        }

        public static colKusht KrijoKushte(object[] ok, string prioriteti)
        {
            var colKushte = new colKusht();
            foreach (var oo in ok)
            {
                var kusht = new clsKusht();
                kusht.krijoKusht((Dictionary<string, object>)oo, prioriteti);
                colKushte.Add(kusht);
            }
            return colKushte;
        }

        #endregion

        #region Metoda Private

        private void MbushKushtet(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
                Add(new clsKusht(rreshti));
        }

        #endregion
    }
}
