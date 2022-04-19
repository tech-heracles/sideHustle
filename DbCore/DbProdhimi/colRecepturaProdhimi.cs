using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbRegjistrim;
using DbCore.DbInventari;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Globalization;
using System.Web;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsRecepturaProdhimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colRecepturaProdhimi : List<clsRecepturaProdhimi>
    {
        #region konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colRecepturaProdhimi()
        {

        }

        /// <summary>
        /// konstruktori me nje parameter
        /// </summary>
        /// <param name="idprodukti">id e produktit</param>
        public colRecepturaProdhimi(int idprodukti)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushReceptura(db.ktheGjitheRecepturatSipasProduktit(idprodukti));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsRecepturaProdhimi</param>
        public colRecepturaProdhimi(IEnumerable<clsRecepturaProdhimi> collection)
            : base(collection)
        {

        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbProdhimi.clsRecepturaProdhimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsRecepturaProdhimi this[int index]
        {
            get { return ((clsRecepturaProdhimi)base[index]); }
        }

        /// <summary>
        /// merr artikujt qe ndodhne ne trup
        /// </summary>
        /// <returns>nje koleksion me te gjithe artikujt qe ndodhen ne trup</returns>
        public DbInventari.colArtikujt ktheColArtikuj()
        {
            DbInventari.colArtikujt colArt = new DbInventari.colArtikujt();
            foreach (clsRecepturaProdhimi trup in this)
            {                
                colArt.Add(new DbInventari.clsArtikulli(trup.IdArtikulli));
            }
            return colArt;
        }
        public DbInventari.colDetajimeArtikulli ktheColDetajime()
        {
            DbInventari.colDetajimeArtikulli colDet = new DbInventari.colDetajimeArtikulli();
            foreach (clsRecepturaProdhimi trup in this)
            {                
                colDet.Add(new DbInventari.clsDetajimArtikulli(trup.IdDetajimi));
            }
            return colDet;
        }
        public DbInventari.colDetajimeArtikulli ktheColDetajime2()
        {
            DbInventari.colDetajimeArtikulli colDet = new DbInventari.colDetajimeArtikulli();
            foreach (clsRecepturaProdhimi trup in this)
            {
                colDet.Add(new DbInventari.clsDetajimArtikulli(trup.IdDetajimi2));
            }
            return colDet;
        }

        /// <summary>
        /// merr burimet qe ndodhne ne trup
        /// </summary>
        /// <returns>nje koleksion me te gjithe burimet qe ndodhen ne trup</returns>
        public colBurimet ktheColBurimet()
        {
            colBurimet colBur = new colBurimet();
            foreach (clsRecepturaProdhimi trup in this)
            {
                clsBurime bur = new clsBurime(trup.IdBurimi);
                colBur.Add(bur);
            }
            return colBur;
        }

        /// <summary>
        /// merr gjithe magazinat qe ndodhen ne trup
        /// </summary>
        /// <returns>nje koleksion me te gjitha magazinat qe ndodhen ne trup</returns>
        public colNjesiAdministrative ktheColMag(int idPerdorues)
        {
            colNjesiAdministrative colMag = new colNjesiAdministrative();
            foreach (clsRecepturaProdhimi trup in this)
            {
                clsNjesiAdministrative mag = new clsNjesiAdministrative(trup.IdMag);
                colMag.Add(mag);
            }
            return colMag;
        }

        /// <summary>
        /// mbush recepuren sipas idprodukti
        /// </summary>
        /// <param name="idprodukti">id produktit</param>        
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushRecepturaSipasIdProdukti(int idprodukti)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushRecepturaSipasIdProdukti(idprodukti, db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush recepuren sipas idprodukti
        /// </summary>
        /// <param name="idprodukti">id produktit</param>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushRecepturaSipasIdProdukti(int idprodukti, clsDatabazeProdhimi db)
        {
            //if (db == null)
            //    db = new clsDatabazeProdhimi();
            bool mbush = mbushReceptura(db.ktheGjitheRecepturatSipasProduktit(idprodukti));
            return mbush;
        }

        /// <summary>
        /// mbush recepuren sipas idartikulli
        /// </summary>
        /// <param name="idartikulli">id artikullit</param>
        /// <param name="data"> data e dokumentit</param>        
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushRecepturaSipasIdArtikulli(int idartikulli, DateTime data,int idplanifikimi, string sasiburimi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushRecepturaSipasIdArtikulli(idartikulli, data, idplanifikimi, sasiburimi, db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush recepuren sipas idartikulli
        /// </summary>
        /// <param name="idartikulli">id artikullit</param>
        /// <param name="data"> data e dokumentit</param>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushRecepturaSipasIdArtikulli(int idartikulli, DateTime data,int idplanifikimi, string sasiburimi, clsDatabazeProdhimi db)
        {
            //if (db == null)
            //    db = new clsDatabazeProdhimi();
            bool mbush = mbushReceptura(db.ktheGjitheRecepturatSipasArtikullit(idartikulli,data, idplanifikimi,sasiburimi));
            return mbush;
        }

        /// <summary>
        /// mbush recepuren sipas idkoka
        /// </summary>
        /// <param name="idkoka">id koka</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushRecepturaSipasIdKoka(int idkoka)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushRecepturaSipasIdKoka(idkoka, db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush recepuren sipas idkoka
        /// </summary>
        /// <param name="idkoka">id koka</param>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushRecepturaSipasIdKoka(int idkoka, clsDatabazeProdhimi db)
        {
            //if (db == null)
            //    db = new clsDatabazeProdhimi();
            bool mbush = mbushReceptura(db.ktheGjitheRecepturaProdhimiNgaKoka(idkoka));
            return mbush;
        }

        public static IEnumerable<Dictionary<String, Object>> merrRec(int idartikulli, int idmag, int idmagprodukti, double sasi, DateTime data, clsProduktProdhimi trup, decimal koef, double sasiplan, int idplanifikimi, string sasiburimi, int idPerdorues)
        {
            double shuma = 0;
            var recepturat = new ConcurrentBag<Dictionary<String, Object>>();
            if (idartikulli <= 0)
                return recepturat;
            colRecepturaProdhimi colrecartikulli = new colRecepturaProdhimi();
            colrecartikulli.mbushRecepturaSipasIdArtikulli(idartikulli, data, idplanifikimi, sasiburimi);
            if (colrecartikulli.Count == 0)
                return recepturat;
            List<int> idArtikujsh = new List<int>(colrecartikulli.Select(y => y.IdArtikulli).Distinct());
            colArtikujt colArt = new colArtikujt(idArtikujsh);

            List<int> idNjesirtikujsh = new List<int>(colArt.Select(y => y.Njesi1Artikulli).Distinct());
            colNjesiteArtikulli colNjesiArt = new colNjesiteArtikulli(idNjesirtikujsh);

            List<int> idMagazinash = new List<int>(colrecartikulli.Select(y => y.IdMag).Distinct());
            colNjesiAdministrative colMagazinat = new colNjesiAdministrative(idMagazinash);
            var currentContext = HttpContext.Current;
            int count = colrecartikulli.Count;
            var options = new  ParallelOptions
            {
                MaxDegreeOfParallelism = count,
                 
                
            };
           // ExecutionContext.SuppressFlow();
            Parallel.ForEach(colrecartikulli,options, (rec) =>
            {
                HttpContext.Current = currentContext;
                Dictionary<String, Object> receptura = new Dictionary<string, object>();
                rec.IdProdukti = trup.Id;
                if (rec.Id != -5)
                {
                    rec.SasiaAktuale *= sasi * Convert.ToDouble(koef);
                }
                rec.Sasia *= sasiplan * Convert.ToDouble(koef);
                rec.Scrap *= sasi * Convert.ToDouble(koef);

                if (idmag != 0)
                    rec.IdMag = idmag;
                else if (rec.IdMag == 0)
                    rec.IdMag = idmagprodukti;

                clsNjesiAdministrative mag = colMagazinat.Find(x => x.IdNjesiAdministrative == rec.IdMag);
                if (mag == null)
                    mag = new clsNjesiAdministrative();

                receptura.Add("mag", mag);
                if (rec.Lloji == 2)
                {
                    receptura.Add("rec", rec);
                    switch (rec.NjesiArtikull)
                    {
                        case 1:
                            receptura.Add("njesi", new clsNjesiArtikulli("sec", "sec", 0, 0, 0,""));
                            break;
                        case 2:
                            receptura.Add("njesi", new clsNjesiArtikulli("min", "min", 0, 0, 0,""));
                            break;
                        case 3:
                            receptura.Add("njesi", new clsNjesiArtikulli("ore", "ore", 0, 0, 0,""));
                            break;
                        case 4:
                            receptura.Add("njesi", new clsNjesiArtikulli("dite", "dite", 0, 0, 0,""));
                            break;
                    }
                    receptura.Add("det1", new clsDetajimArtikulli());
                    receptura.Add("det2", new clsDetajimArtikulli());
                    receptura.Add("artikulli", new clsArtikulli());
                }
                else
                {
                    clsArtikulli art = colArt.Find(x => x.IdArtikulli == rec.IdArtikulli);
                    clsNjesiArtikulli njesi = colNjesiArt.Find(x => x.IdNjesia == art.Njesi1Artikulli);
                    receptura.Add("njesi", njesi);
                    clsTrupiMagazina tr = new clsTrupiMagazina();
                    rec.Kosto = tr.llogaritCmimMesatar(art, rec.IdMag, data, -1, rec.SasiaAktuale, idPerdorues);
                    receptura.Add("rec", rec);

                    Dictionary<String, Object>[] detajimDheSasi = new Dictionary<String, Object>[2];
                    //per tu pare
                    detajimDheSasi = clsArtikulli.ktheArtDetajimet(art, idPerdorues, data, rec.IdMag, 0);
                    receptura.Add("det1", detajimDheSasi[0] == null ? null : (clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[0])["detajim"]);
                    receptura.Add("det2", detajimDheSasi[1] == null ? null : (clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[1])["detajim"]);
                    receptura.Add("artikulli", art);
                }

                rec.KostoTotale = rec.Kosto * rec.SasiaAktuale;
                shuma += rec.Kosto * rec.SasiaAktuale;
                recepturat.Add(receptura);
            });
            trup.KostoTotale = shuma;
            return recepturat.AsEnumerable();
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhenat e tipit receptura</param>
        /// <returns>true ose false ne se coleksioni u mbush ne rregull</returns>
        private bool mbushReceptura(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsRecepturaProdhimi trupi = new clsRecepturaProdhimi();
                    //trupi.mbushRecepture(rreshti);
                    Add(new clsRecepturaProdhimi(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion
    }
}