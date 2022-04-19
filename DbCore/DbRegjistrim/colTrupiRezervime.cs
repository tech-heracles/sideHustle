using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using DbCore.IMBUtils.Logging;
using DbCore.DbInventari;

namespace DbCore.DbRegjistrim
{
   public class colTrupiRezervime : System.Collections.Generic.List<clsTrupiRezervime>
    {

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsTrupiRezervime"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsTrupiRezervime this[int index]
        {
            get { return ((clsTrupiRezervime)base[index]); }
        }

        public DbInventari.colArtikujt ktheColArtikuj()
        {
            DbInventari.colArtikujt colArt = new DbInventari.colArtikujt();
            foreach (clsTrupiRezervime trupMag in this)
            {                
                colArt.Add(new DbInventari.clsArtikulli(trupMag.IdArtikulli));
            }
            return colArt;
        }

        public colNjesiAdministrative ktheColMag(int idPerdorues)
        {
            colNjesiAdministrative colMag = new colNjesiAdministrative();
            foreach (clsTrupiRezervime trupMag in this)
            {
                clsNjesiAdministrative mag = new clsNjesiAdministrative(trupMag.IdMag);
                colMag.Add(mag);
            }
            return colMag;
        }
    
        public DbInventari.colNjesiteArtikulli ktheColNjesiArt()
        {
            DbInventari.colNjesiteArtikulli colNjesi = new DbInventari.colNjesiteArtikulli();
            foreach (clsTrupiRezervime trupMag in this)
            {
                DbInventari.clsNjesiArtikulli njesiArt = new DbInventari.clsNjesiArtikulli(trupMag.IdNjesia);
                colNjesi.Add(njesiArt);
            }
            return colNjesi;
        }
        /// <summary>
        /// mbush trupin e rezervimit sipas id se kokes se magazines
        /// </summary>
        /// <param name="idKokaMagazina">id koka e rezevimit</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiRezervimi(int idKoka, clsDatabaseRegjistrim db)
        {
            return mbushTrupatRezervim(db.ktheTrupiRezervimi(idKoka));
        }
        /// <summary>
        /// mbush trupin e rezervimit sipas id se kokes se rezevimit
        /// </summary>
        /// <param name="idKokaRerzervime">id koka e rezevimit</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiRezervimi(int idKoka)
        {
            clsDatabaseRegjistrim dbtrup = new clsDatabaseRegjistrim();
            bool sukses = mbushTrupatRezervim(dbtrup.ktheTrupiRezervimi(idKoka));
            dbtrup.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush te gjithe trupin e rezervimit nga kokat
        /// </summary>
        /// <param name="idKokaRezervimi">id e koka rezervimit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushGjitheTrupiRezervimiNgaKoka(int idKoka)
        {
            using (clsDatabaseRegjistrim dbtrup = new clsDatabaseRegjistrim())
            {
                bool mbush = mbushTrupatRezervim(dbtrup.ktheTrupiRezervimi(idKoka));

                return mbush;
            }
        }
        public void mbushGjitheTrupiRezervimiNgaKoka(int idKoka,clsDatabaseRegjistrim dbtrup)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda mbushGjitheTrupiRezervimiNgaKoka me idKoka:{idKoka}");
            mbushTrupatRezervim(dbtrup.ktheTrupiRezervimi(idKoka));
            
        }
        
        public bool mbushGjitheTrupiRezervimiNgaKokaShitjes(int idShitjeKoka, int lloji, clsDatabaseRegjistrim dbtrup)
        {
            return mbushTrupatRezervim(dbtrup.ktheTrupiRezervimiSipasKokesShitjes(idShitjeKoka, lloji));
        }
        public colTrupiRezervime()
       {

       }
        //todo nedjan merr magazinen e perberesit sipas kushtit
        public colTrupiRezervime(colTrupiShitje coltrupishitje, DateTime dt,int idstatusdok, int idkonfig, bool own, clsDatabaseInventari dbInv, double zbritje, bool merrMagazinePerberesi)
        {
            

            foreach (clsTrupiShitje trup in coltrupishitje)
            {
                if (trup.IdLlojVeprimi != 1)
                    continue;

                
                int idMagazina;
                int idKodi;
                if (own)
                {
                    var VleratMagOwn = DbCore.DbInventari.clsArtikulli.ktheTeDhenaPerArtikullin(trup.Kodi).Split(',');
                    idMagazina = Int32.Parse(VleratMagOwn[0]);
                    idKodi = Int32.Parse(VleratMagOwn[2]);
                    trup.SasiRez = trup.Sasia;
                }
                else
                {
                    idMagazina = trup.IdMagazina;
                    idKodi = trup.IdKodi;
                }

                DbInventari.clsArtikulli art = new DbInventari.clsArtikulli(idKodi, dbInv);

                if (art.Klasa != 4)
                {
                    ShtoTrupRezervimi(trup, idkonfig, idMagazina, art, idKodi, dt, idstatusdok, trup.Kodi, trup.Pershkrimi, trup.IdNjesia, trup.SasiRez, trup.IdShitjeTrupi,art);
                }
                else
                {
                    colArtikulliPerberes artper = new colArtikulliPerberes(art.IdArtikulli, dt, dbInv);
                    var njesi = new clsNjesiArtikulli(art.Njesi1Artikulli, dbInv);
                    for (int j = 0, artPerCount = artper.Count; j < artPerCount; j++)
                    {
                        DbInventari.clsArtikulliPerberes aper = artper[j];
                        clsArtikulli perberes = new clsArtikulli(aper.IdLidheseArt, dbInv);
                        if (aper.Lloji == 1)    //artikull
                        {
                            double sasiaP = trup.SasiRez * (double)aper.Koeficienti * ((art.Njesi1Artikulli == njesi.IdNjesia) ? 1 : Convert.ToDouble(art.KoeficientArtikulli));
                            ShtoTrupRezervimi(trup, idkonfig, merrMagazinePerberesi ? perberes.IdMagazina : idMagazina, art, perberes.IdArtikulli, dt, idstatusdok, perberes.KodArtikulli, perberes.PershkrimArtikulli, njesi.IdNjesia, sasiaP, trup.IdShitjeTrupi,perberes);


                        }
                    }

                }
            }

            

        }

        private void ShtoTrupRezervimi(clsTrupiShitje trup, int idkonfig, int idMagazina, clsArtikulli art, int idKodi, DateTime dt, int idstatusdok, string kodi, string pershkrimi, int idnjesia, double sasirez,int idshitjetrupi, clsArtikulli perberes)
        {
            int id = 0;

            if (trup.IdShitjeTrupi != 0)
            {
                clsTrupiShitje tr = new clsTrupiShitje(trup.IdShitjeTrupi);

                clsKokaRezervime rezervimehyrje = new clsKokaRezervime();
                rezervimehyrje.mbushKokaRezervimiSipasIDGjenerues(tr.IdShitjeKoka, 1, idkonfig);
                rezervimehyrje.OcolTrupiRezervime.mbushGjitheTrupiRezervimiNgaKoka(rezervimehyrje.IdKokaRezervimi);
                foreach (clsTrupiRezervime trrez in rezervimehyrje.OcolTrupiRezervime)
                {
                    if (trrez.IdTrupiNgaVjen == trup.IdShitjeTrupi && (trrez.IdMag == idMagazina || trrez.IdMag == 0))
                        id = trrez.IdTrupiRezervime;
                }

            }
            if (perberes.IRezervueshem)
            {

                clsTrupiRezervime rez = new clsTrupiRezervime(id, 0, idKodi, trup.Kodi, trup.Pershkrimi, idnjesia, sasirez, art.Njesi1Artikulli == idnjesia ? 1 : Convert.ToDouble(art.KoeficientArtikulli), idMagazina, dt, idstatusdok, 1, trup.IdShitjeTrupi, 0, perberes);
                this.Add(rez);
            }
        }
     
        /// <summary>
        /// kthen sasine totale te artikullit sipas magazines 
        /// </summary>
        /// <param name="idKodi"></param>
        /// <param name="idMagazina"></param>
        /// <returns></returns>
        public double ktheSasiDaljeRezervimi(int idKodi, int idMagazina)
        {
            //colTrupiRezervime trupiRezervimit = oKokaRezervime.OcolTrupiRezervime;
            double totali = 0;
            foreach (clsTrupiRezervime o in this)
                if (o.IdArtikulli == idKodi && o.IdMag == idMagazina)
                    totali += o.Sasia * o.Koeficenti;
            return totali;
        }
        public double ktheSasiDaljeRezervimi(int idKodi)
        {
            //colTrupiRezervime trupiRezervimit = oKokaRezervime.OcolTrupiRezervime;
            double totali = 0;
            foreach (clsTrupiRezervime o in this)
                if (o.IdArtikulli == idKodi)
                    totali += o.Sasia * o.Koeficenti;
            return totali;
        }

        public colTrupiRezervime ShallowCopy()
        {
            var newTrup = new colTrupiRezervime();
            foreach (var trup in this)
                newTrup.Add(trup.ShallowCopy());

            return newTrup;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="DbCore.DbRegjistrim.clsTrupiRezervime"/> 
        /// </summary>
        private bool mbushTrupatRezervim(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrupiRezervime trupi = new clsTrupiRezervime();
                    //trupi.mbushTrupRezervimi(rreshti);
                    Add(new clsTrupiRezervime(rreshti));
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
