using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbInventari
{ /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsArtikulliPerberes
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colArtikulliPerberes : System.Collections.Generic.List<clsArtikulliPerberes>
    {
        #region Konstruktore
        public colArtikulliPerberes() { }
        public colArtikulliPerberes(string ids, DateTime date)
        {
            merrSipasIdArtikujKryesore(ids, date);
        }

        public colArtikulliPerberes(int idArtikulli, DateTime date, clsDatabaseInventari dbInv)
        {
            ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(idArtikulli, date, dbInv);
        }

        #endregion

        #region Metoda publike


        private void merrSipasIdArtikujKryesore(string ids, DateTime date)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                mbushArtikujPerberes(db.ktheArtikujPerberesSipasIdArtikujve(ids, date));
        }
        /// <summary>
        /// merr perberesit sipas id se artikullit kryesore
        /// </summary>
        /// <param name="id">id e artikullit kryesore</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="db"></param>
        public bool merrSipasIdArtikullKryesore(int id, clsDatabaseInventari db)
        {
            return mbushArtikujPerberes(db.ktheArtikujPerberesSipasIdArtikullitKryesor(id));
        }

        /// <summary>
        /// merr perberesit sipas id se artikullit kryesore
        /// </summary>
        /// <param name="id">id e artikullit kryesore</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="db"></param>
        public DataTable merrSipasIdArtikullKryesoreDt(string idartikujsh,int idNdermarrje, DateTime data)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                return db.ktheArtikujPerberesSipasIdArtikujve_Kryesor(idartikujsh, idNdermarrje, data);
        }

        /// <summary>
        /// merr perberesin e pare sipas id se artikullit kryesore
        /// </summary>
        /// <param name="id">id e artikullit kryesore</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="db"></param>
        public bool merrSipasIdArtikullKryesorePare(int id,DateTime data, clsDatabaseInventari db)
        {
            return mbushArtikujPerberes(db.ktheArtikujPerberesSipasIdArtikullitKryesorPare(id,data));
        }
        public static DataTable merrRecepturaPerExport(int idndermarje)
        {
            using (clsDatabaseInventari dbKodifikimKF = new clsDatabaseInventari())
            {
                return dbKodifikimKF.merrRecepturaPerExport(idndermarje);
            }
        }

        public clsMesazh ruaj(int idndermarje, string emerTabKoka, string emerFusheNdermarrje)
        {
            clsMesazh mes = new clsMesazh();
            clsDatabaseInventari db = new clsDatabaseInventari();
            db.beginTransaksion();
            foreach (clsArtikulliPerberes art in this)
            {
                if (clsArtikulliPerberes.ekzistonArtikullPerberesPerKeteDate(art.IdArtikulliKryesor, art.DtNdryshimi, art.IdLidhese, art.Lloji, db))
                {
                    mes = new clsMesazh(false, "Ekziston kjo recepture per kete artikull");
                    db.rollbackTransaksion();
                    return mes;
                }
                int id = 0;
                mes = db.ruajArtikullPerbere(out id, art.Lloji, art.IdArtikulliKryesor, art.IdLidheseArt, art.Koeficienti, art.Scrap, art.IdLidheseAkt, art.GjithmoneNgaStoku, art.DtNdryshimi);
                if (!mes.Status)
                {
                    db.rollbackTransaksion();
                    return mes;
                }
                if (art.IdImportTAbSQL > 0)
                {
                    DbImporte.clsDatabazeImporte dbImport = new DbImporte.clsDatabazeImporte(db);
                    mes = DbImporte.colImportSQL.updateDokTabeleTemportal(art.IdImportTAbSQL.ToString(), idndermarje, 1, emerTabKoka, "IDIMPORTSHITJE", emerFusheNdermarrje, dbImport);

                    if (!mes.Status)
                    {
                        db.rollbackTransaksion();
                        return mes;
                    }
                }
            }
            db.commitTransaksion();
            return mes;
        }

        public String merrKoeficentArtikullPerberes(int id)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                return db.ktheKoeficentArtikullPerberesSipasIdArtikullitKryesor(id);
        }
        /// <summary>
        /// merr perberesit sipas id se artikullit kryesore
        /// </summary>
        /// <param name="id">id e artikullit kryesore</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="db"></param>
        public bool merrSipasIdArtikullLidhes(int id, clsDatabaseInventari db)
        {
            return mbushArtikujPerberes(db.ktheArtikujPerberesSipasIdArtikulliLidhes(id));
        }

        /// <summary>
        /// merr perberesit sipas id se artikullit kryesore
        /// </summary>
        /// <param name="id">id e artikullit kryesore</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="db"></param>
        public void merrSipasIdArtikullKryesore(int id)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                merrSipasIdArtikullKryesore(id, db);
           
        }

        /// <summary>
        /// merr perberesin e pare sipas id se artikullit kryesore
        /// </summary>
        /// <param name="id">id e artikullit kryesore</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="db"></param>
        public bool merrSipasIdArtikullKryesorePare(int id, DateTime data)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool sukses = merrSipasIdArtikullKryesorePare(id,data, db);
            db.Dispose();
            return sukses;
        }

        /// <summary>
        /// merr perberesit sipas id se artikullit kryesore
        /// </summary>
        /// <param name="id">id e artikullit kryesore</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="db"></param>
        public bool merrSipasIdArtikullLidhes(int id)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool sukses = merrSipasIdArtikullLidhes(id, db);
            db.Dispose();
            return sukses;
        }

        /// <summary>
        /// merr perberesit sipas id se artikullit kryesore dhe dates
        /// </summary>
        /// <param name="id">id e artikullit kryesore</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="db"></param>
        public bool merrSipasIdArtikullKryesoreDates(int id, DateTime data, clsDatabaseInventari db)
        {
            return mbushArtikujPerberes(db.ktheArtikujPerberesSipasIdArtikullitKryesorDheDates(id, data));
        }
        /// <summary>
        /// merr perberesit sipas id se artikullit kryesore dhe dates
        /// </summary>
        /// <param name="id">id e artikullit kryesore</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool merrSipasIdArtikullKryesoreDates(int id, DateTime data)
        {
            using (var db = new clsDatabaseInventari())
                return mbushArtikujPerberes(db.ktheArtikujPerberesSipasIdArtikullitKryesorDheDates(id, data));
           
        }
        public void ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(int id, DateTime data)
        {
            using (var db = new clsDatabaseInventari())
                ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(id, data, db);
        }
        public bool ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfertProdhim(int id, DateTime data)
        {
            using (var db = new clsDatabaseInventari())
                return mbushArtikujPerberes(db.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfertProdhim(id, data));
           
        }
        public void ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(int id, DateTime data,clsDatabaseInventari db)
        {
            this.AddRange(db.TransCache.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(id, data,db));
        }


        public double merrGjitheSasine(double sasia)
        {

            double sasiaPerGjitheArtPerberes = 0;
            for (int j = 0, artPerCount = this.Count; j < artPerCount; j++)
            {
                sasiaPerGjitheArtPerberes += sasia * (double)this[j].Koeficienti;
            }

            return sasiaPerGjitheArtPerberes;
        }
        /// <summary>
        /// merr datat ne te cilat kemi ndryshime ne artikujve perberes
        /// </summary>
        ///<param name="idkoka">id e kokes</param>
        /// <returns>liste me datat</returns>
        public static List<string> merrDataArtikujPerberes(int idkoka)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            List<string> list = new List<string>();
            DataTable dt = db.merrDataNdryshimiArktikujPerberes(idkoka);
            db.Dispose();
            using (dt)
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    list.Add(DateTime.Parse(rreshti["DTNDRYSHIMI"].ToString()).ToShortDateString());
                }
            }
            return list;

        }
        public static List<string> merrDataArtikujPerberes(int idkoka, clsDatabaseInventari db)
        {

            List<string> list = new List<string>();
            DataTable dt = db.merrDataNdryshimiArktikujPerberes(idkoka);

            using (dt)
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    list.Add(DateTime.Parse(rreshti["DTNDRYSHIMI"].ToString()).ToShortDateString());
                }
            }
            return list;

        }

        public void VendosSasitePerArtikujSet(List<clsArtikujMeSasi> sasite, int idNdermarrje)
        {
            foreach(var sasia in sasite)
            {
                var art = this.FindAll(ap => sasia.IdSeti == ap.IdArtikulliKryesor && sasia.IdArtikulli == ap.IdLidheseArt);
                if (art == null)
                    continue;
                art.ForEach(a =>
                {
                    a.setIdMag(sasia.Mag, idNdermarrje);
                    a.setsasiSet(sasia.Sasi);
                });
            }
        }

        #endregion

        #region Metoda private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// nepermjet kesaj metode thirret per nje metode qe ben mbushjen nga databaza
        /// <see cref="DbCore.DbInventari.clsArtikulliPerberes"/> 
        /// </summary>
        public bool mbushArtikujPerberes(DataTable dt)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushArtikujPerberes!S");
            foreach (DataRow rreshti in dt.Rows)
            {
                this.Add(new clsArtikulliPerberes(rreshti));
            }
            ImbLogger.LogTraceShitje("Mbaroi metoda mbushArtikujPerberes!S");
            return true;

        }

        #endregion
        
    }
}