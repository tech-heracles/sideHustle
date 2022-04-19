using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.DbAdmin;
using DbCore.DbShare;
using DbCore.DbInventari;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e nje dokumenti shperndarje shpenzimesh
    ///  (Te dhenat  merren nga tabela : T_SHPERNDARJESHPENZIMETRUPI)
    /// </summary>
    public class clsShperndarjeShpenzimeTrupi
    {
        #region Atribute 

        private int idTrupi;
        private int idKoka;
        private int idFatura;
        private String nrDok;
        private DateTime dtDok;
        private int llojDok;
        private colShperndarjeShpenzimeTrupiFaturat oColTrupiFaturat;
        private DataRow rreshti;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtdok">data e dokumentit</param>
        /// <param name="idfatura"> id e fatures</param>
        /// <param name="idkoka">id e kokes se dokumentit shperndarje shpenzimesh</param>
        /// <param name="idtrupi">id ritese e trupit te dokumentit</param>
        /// <param name="llojdok">lloji i dokumnentit</param>
        /// <param name="nrdok"> nr i dokumentit</param>
        public clsShperndarjeShpenzimeTrupi(int idtrupi, int idkoka, int idfatura, string nrdok, DateTime dtdok, int llojdok)
        {
            idTrupi = idtrupi;
            idKoka = idkoka;
            idFatura = idfatura;
            nrDok = nrdok;
            dtDok = dtdok;
            llojDok = llojdok;
            oColTrupiFaturat = new colShperndarjeShpenzimeTrupiFaturat();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsShperndarjeShpenzimeTrupi()
        {
        }
        public clsShperndarjeShpenzimeTrupi(int idNdermarrje, Dictionary<string, object> rreshtDokuKlient, object shpenzimi)
        {
            if (rreshtDokuKlient["NrDok"]!=null)
            {
                this.DtDok = DateTime.Parse(rreshtDokuKlient["DtDok"].ToString());
                this.IdFatura = int.Parse(rreshtDokuKlient["IdKokaMagazina"].ToString());
                this.llojDok = 39;
                this.NrDok = rreshtDokuKlient["NrDok"].ToString();
                this.OColTrupiFaturat = new colShperndarjeShpenzimeTrupiFaturat(idNdermarrje, rreshtDokuKlient, shpenzimi);
            }
            else IdFatura = -1;
        }

        public clsShperndarjeShpenzimeTrupi(DataRow rreshti)
        {
            
            mbushShperndarjeShpenzimeTrupi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit shperndarje shpenzimesh.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e fatures 
        /// </summary>
        public int IdFatura
        {
            get { return idFatura; }
            set { idFatura = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr e dokumentit.
        /// </summary>
        public String NrDok
        {
            get { return nrDok; }
            set { nrDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos data e dokumentit.
        /// </summary>
        public DateTime DtDok
        {
            get { return dtDok; }
            set { dtDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos lloji i dokumentit.
        /// </summary>
        public int LlojDok
        {
            get { return llojDok; }
            set { llojDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje koleksion me trupat e faturave te dokumentit shperndarje shpenzimesh.
        /// </summary>
        public colShperndarjeShpenzimeTrupiFaturat OColTrupiFaturat
        {
            get { return oColTrupiFaturat; }
            set { oColTrupiFaturat = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// perdoret per te ruajtur trupin e dokumentit te shperndarje shpenzimesh sebashku me trupat e faturave me te cilat lidhet
        /// </summary>
        /// <param name="trupi"> trupi i dokumentit shperndarje shpenzimesh</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public clsMesazh ruajShperndarjeShpenzimeshTrupinDheFaturat(int idtrupi, int idkoka, int idfatura, string nrdok, DateTime dtdok, int llojdok, colShperndarjeShpenzimeTrupiFaturat oColTrupiFaturat, clsDatabaseRegjistrim dbRegj)
        {
          
            clsMesazh msg = new clsMesazh();
            try
            {
                msg = dbRegj.ruajShperndarjeShpenzimeshTrupi(out idtrupi, idkoka, idfatura, nrdok, dtdok, llojdok);
                if (msg.Status)
                {
                    foreach (clsShperndarjeShpenzimeTrupiFaturat o in oColTrupiFaturat)
                    {
                        o.IdShperndarjeShpenzTrupi = idtrupi;
                        int idtrupfat;
                        msg = dbRegj.ruajShperndarjeShpenzimeshTrupiFaturat(out idtrupfat, o.IdShperndarjeShpenzTrupi, o.IdArtikull, o.IdTrupiShitje, o.Vlera, o.Shperndaj);
                        o.IdTrupiFaturat = idtrupfat;
                    }
                }
                return msg;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }
        public bool eshteZShperndareFatura(int id)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool shperndare = db.eshteShperndareFatura(id);
            db.Dispose();
            return shperndare;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e shperndarjes se shpenzimeve nga databaza
        /// </summary>
        /// <param name="dbDataRowShperShpeTrupi">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushShperndarjeShpenzimeTrupi(DataRow dbDataRowShperShpeTrupi)
        {
            if (dbDataRowShperShpeTrupi != null)
            {
                try
                {
                    int.TryParse(dbDataRowShperShpeTrupi["IDSHPERNDARJESHPENZTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowShperShpeTrupi["IDSHPERNDARJESHPENZKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRowShperShpeTrupi["IDFATURA"].ToString(), out idFatura);
                    nrDok = dbDataRowShperShpeTrupi["NRDOK"].ToString();
                    DateTime.TryParse(dbDataRowShperShpeTrupi["DTDOK"].ToString(), out dtDok);
                    int.TryParse(dbDataRowShperShpeTrupi["LLOJDOK"].ToString(), out llojDok);
                    oColTrupiFaturat = new colShperndarjeShpenzimeTrupiFaturat();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te shperndarjes se shpenzimeve nga db-ja");
                }
            }
            else
                return false;
        }

        internal clsMesazh krijoTrupShperndarjeShpenzNgaImporti(int idNdermarrje, string llojDokHyrje, string nrDokHyrje, DateTime dtDokHyrje, List<Dictionary<string, object>> artikullVlerShpenz, ref List<Dictionary<string, object>> llogarite)
        {
            clsKonfigurimAmbjenti konfigDokHyrje = new clsKonfigurimAmbjenti(llojDokHyrje, idNdermarrje, new clsDatabaseShare());
            clsNivelRegjistrimi nivelDokHyrje = new clsNivelRegjistrimi();
            nivelDokHyrje.mbushNivelRegjistrimiSipasIdPaKonvertime(konfigDokHyrje.IdNivel);

            if(nivelDokHyrje.Kodi != "FH")
                return new MesazhGabimi($"Dokumenti i magazines me numer {nrDokHyrje} dhe date {dtDokHyrje.ToShortDateString()} nuk i perket kategorise Flete Hyrje.");

            clsKokaMagazina kokaMag = new clsKokaMagazina();
            kokaMag.merrKokaMagazinaSipasIdKonfigNrdokDtdok(konfigDokHyrje.IdKonfigAmbjente, nrDokHyrje, dtDokHyrje);
            
            if(kokaMag.IdKokaMagazina <= 0)
                return new MesazhGabimi($"Dokumenti i magazines me numer {nrDokHyrje} dhe date {dtDokHyrje.ToShortDateString()} nuk ekziston.");

            if (kokaMag.IdStatusDok != 1)
                return new MesazhGabimi($"Dokumenti i magazines me numer {nrDokHyrje} dhe date {dtDokHyrje.ToShortDateString()} nuk eshte me status Ruajtur.");

            kokaMag.mbushTrupMagazine(false);

            this.IdFatura = kokaMag.IdKokaMagazina;
            this.LlojDok = 39;
            this.DtDok = dtDokHyrje;
            this.nrDok = nrDokHyrje;

            colShperndarjeShpenzimeTrupiFaturat colTrupiFatura = new colShperndarjeShpenzimeTrupiFaturat();
            clsArtikulli artikull;
            clsNjesiAdministrative magazina;
            if(artikullVlerShpenz.Count > 0)
            {
                foreach (Dictionary<string, object> item in artikullVlerShpenz)
                {
                    artikull = new clsArtikulli(item["KodArtikulli"].ToString(), idNdermarrje);
                    magazina = new clsNjesiAdministrative(item["Magazina"].ToString(), idNdermarrje);
                    clsTrupiMagazina trupiMag = kokaMag.OcolTrupiMagazina.FirstOrDefault(x => x.IdArtikulli == artikull.IdArtikulli && x.IdMag == magazina.IdNjesiAdministrative);
                    if (trupiMag == null || trupiMag.IdTrupiMagazina <= 0)
                        return new MesazhGabimi($"Artikulli {artikull.KodArtikulli} nuk i perket dokumentit te hyrjes nr {nrDokHyrje}.");
                    clsShperndarjeShpenzimeTrupiFaturat trupiFatura = colTrupiFatura.FirstOrDefault(x => x.IdTrupiShitje == trupiMag.IdTrupiMagazina);
                    if (trupiFatura == null)
                    {
                        trupiFatura = new clsShperndarjeShpenzimeTrupiFaturat();
                        trupiFatura.IdArtikull = artikull.IdArtikulli;
                        trupiFatura.IdTrupiShitje = trupiMag.IdTrupiMagazina;
                        trupiFatura.Vlera = Convert.ToDouble(item["VleraShpenz"]);
                        trupiFatura.Shperndaj = true;
                        colTrupiFatura.Add(trupiFatura);
                    }
                    else
                        trupiFatura.Vlera += Convert.ToDouble(item["VleraShpenz"]);

                    Dictionary<string, object> llogTot = llogarite.FirstOrDefault(x => x["Llogaria"].ToString() == item["Llogaria"].ToString());
                    if (llogTot == null)
                        llogarite.Add(new Dictionary<string, object>() { { "Llogaria", item["Llogaria"].ToString() }, { "TotVleraShpenz", Convert.ToDouble(item["VleraShpenz"]) } });
                    else
                        llogTot["TotVleraShpenz"] = Convert.ToDouble(llogTot["TotVleraShpenz"]) + Convert.ToDouble(item["VleraShpenz"]);
                }
            }
            foreach(clsTrupiMagazina trupMag in kokaMag.OcolTrupiMagazina)
            {
                clsShperndarjeShpenzimeTrupiFaturat trupiFatura = colTrupiFatura.FirstOrDefault(x => x.IdTrupiShitje == trupMag.IdTrupiMagazina);
                if (trupiFatura != null && trupiFatura.IdTrupiShitje > 0)
                    continue;
                trupiFatura = new clsShperndarjeShpenzimeTrupiFaturat();
                trupiFatura.IdArtikull = trupMag.IdArtikulli;
                trupiFatura.IdTrupiShitje = trupMag.IdTrupiMagazina;
                trupiFatura.Vlera = artikullVlerShpenz.Count > 0 ? 0 : trupMag.Vlefta;
                trupiFatura.Shperndaj = true;
                colTrupiFatura.Add(trupiFatura);
            }
            this.OColTrupiFaturat = colTrupiFatura;
            return new MesazhSuksesi();
        }

        #endregion
    }
}