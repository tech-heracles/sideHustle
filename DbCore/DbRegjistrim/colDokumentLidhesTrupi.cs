using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsDokumentLidhesTrupi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colDokumentLidhesTrupi : System.Collections.Generic.List<clsDokumentLidhesTrupi>
    {

        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colDokumentLidhesTrupi()
        {
        }

        /// <summary>
        /// Konstrukor me 1 parameter
        /// </summary>
        /// <param name="idDokumenti">id e dokumentit</param>
        public colDokumentLidhesTrupi(int idDokumenti)
        {
            using (clsDatabaseRegjistrim dbDokumentLidhesTrupi = new clsDatabaseRegjistrim())
            {
                mbushDokumentatLidhesTrupa(dbDokumentLidhesTrupi.merrTrupin(idDokumenti));
            }
        }
        public colDokumentLidhesTrupi(int idDokumenti, clsDatabaseRegjistrim dbDokumentLidhesTrupi)
        {

            mbushDokumentatLidhesTrupa(dbDokumentLidhesTrupi.merrTrupin(idDokumenti));

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsDokumentLidhesTrupi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>    
        public new clsDokumentLidhesTrupi this[int index]
        {
            get { return ((clsDokumentLidhesTrupi)base[index]); }
        }

        public clsDokumentLidhesTrupi ktheDokumentKryesor()
        {
            foreach (clsDokumentLidhesTrupi dok in this)
            {
                if (dok.Statusi == "0")
                {
                    return dok;
                }
            }
            return new clsDokumentLidhesTrupi();
        }
        public static DbCore.DbRegjistrim.colDokumentLidhesTrupi krijoTrup(DbCore.DbRegjistrim.colDokumentat dokLidhes, DbCore.DbRegjistrim.colDokumentat dokKryesore)
        {
            DbCore.DbRegjistrim.colDokumentLidhesTrupi trupi = new DbCore.DbRegjistrim.colDokumentLidhesTrupi();

            foreach (DbCore.DbRegjistrim.clsDokumenti d in dokKryesore)
            {
                DbCore.DbRegjistrim.clsDokumentLidhesTrupi t = new DbCore.DbRegjistrim.clsDokumentLidhesTrupi();
                t.IdDokumenti = d.IdDokumenti;
                 string kodniveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheKodNivelRegjistrimi(d.IdNiveli);
                 if (kodniveli != null)
                    t.LlojDokumenti = d.IdNiveli.ToString();
                else
                    t.LlojDokumenti = "";
                t.Statusi = "0"; //dokumentat kryesore i ruajme me status 0
                t.VleraLidhjes = d.Vlefta;

                trupi.Add(t);
            }

            foreach (DbCore.DbRegjistrim.clsDokumenti d in dokLidhes)
            {
                DbCore.DbRegjistrim.clsDokumentLidhesTrupi t = new DbCore.DbRegjistrim.clsDokumentLidhesTrupi();
                t.IdDokumenti = d.IdDokumenti; 
                string kodniveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheKodNivelRegjistrimi(d.IdNiveli);
         
                if (kodniveli != null)
                    t.LlojDokumenti = d.IdNiveli.ToString();
                else t.LlojDokumenti = "";
                t.Statusi = "1"; //dokumentat lidhes i ruajme me status 1

                t.VleraLidhjes = d.Vlefta;

                trupi.Add(t);
            }
            return trupi;
        }
        /// <summary>
        /// grupon sipas niveli dokumentat lidhes dhe kontrollon nese ndonjeri prej tyre eshte modifikuar
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool EshteVlefshemTrupiDokumentitLidhes(clsDatabaseRegjistrim db)
        {
            try
            {
                //grupon sipas nivelit qe te behet kontrolli njehere per cdo nivel
                var dokSipasNivelit = this.GroupBy(x => x.LlojDokumenti).Select(x => new { niveli = x.Key, dokumentat = x.Select(d => d.IdDokumenti) });
                foreach (var x in dokSipasNivelit)
                {
                    var eshteModifikuar = db.EshteModifikuarNdonjeNgaDokumentatLidhes(x.dokumentat.ToList(), x.niveli);
                    //nese te pakten njeri nga dokumentat eshte modifikuar ath dokumentat nuk jane te vlefshem dhe ruajta duhet te nderpritet
                    if (eshteModifikuar) return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return false;
            }
        }
        public bool EshteVlefshemTrupiDokumentitLidhes()
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                return EshteVlefshemTrupiDokumentitLidhes(db);
        }

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="clsDokumentLidhesTrupi"/> 
        /// </summary>
        private bool mbushDokumentatLidhesTrupa(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsDokumentLidhesTrupi(rreshti));
            }
            return true;
        }
        #endregion



    }
}