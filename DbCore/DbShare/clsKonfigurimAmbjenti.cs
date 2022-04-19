using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using DbCore.DbAdmin;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using Newtonsoft.Json;

namespace DbCore.DbShare
{
    public class clsKonfigurimAmbjenti
    {
        #region Attribute
        public static int idKonfigurimDefault = -1;
        private int idKonfigAmbjente;
        private String kodKonfigAmbjente;
        private String pershkrimKonfigAmbjente;
        private int idKategori;
        private int radha;
        private bool defaultKonfigAmbjente;
        private int idNivel;
        private int idSkemeKontabel;
        private int idNdermarje;
        private int idKonfigurimi;
        private int idStatusDok;
        private int idPerdoruesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colAtributeTrupi oColAtributet;
        private colKusht oColKushtet;
        private string idNivelAutorizimi;
        private int idKonfigFormatNr;
        private string pershkrimKonfigAmbjenteEng;
        private int lloji;
        private int formatMobile;
        private string pershkrimKonfigAmbjente_fr;
        private DataRow rreshti;


        #endregion

        #region Kontruktoret

        public clsKonfigurimAmbjenti(int idkonfigambjente, String kodkonfigambjente, String pershkrimkonfigambjente, int idkategori, int radhaKonfig, bool defaultkonfigambjente, int nivel, int SK, int idnderm, int idkonf, int idstatusdok, int idperdorues, int idKonfigFormatNumri, string pershkrimKonfigAmbjenteEng, int formatmobile, int Lloji)
        {
            idKonfigAmbjente = idkonfigambjente;
            kodKonfigAmbjente = kodkonfigambjente;
            pershkrimKonfigAmbjente = pershkrimkonfigambjente;
            idKategori = idkategori;
            radha = radhaKonfig;
            defaultKonfigAmbjente = defaultkonfigambjente;
            idNivel = nivel;
            idSkemeKontabel = SK;
            idNdermarje = idnderm;
            idKonfigurimi = idkonf;
            idStatusDok = idstatusdok;
            idPerdoruesi = idperdorues;
            idKonfigFormatNr = idKonfigFormatNumri;
            lloji = Lloji;
            formatMobile = formatmobile;
            this.pershkrimKonfigAmbjenteEng = pershkrimKonfigAmbjenteEng;
            oColAtributet = new colAtributeTrupi();
            oColKushtet = new colKusht();
        }

        public clsKonfigurimAmbjenti(int idkonfigambjente, String kodkonfigambjente, String pershkrimkonfigambjente, int idkategori, int radhaKonfig, bool defaultkonfigambjente, int nivel, int SK, int idnderm, int idkonf, int idstatusdok, int idperdorues, int idKonfigFormatNumri, int formatmobile, int Lloji = 1)
        {
            idKonfigAmbjente = idkonfigambjente;
            kodKonfigAmbjente = kodkonfigambjente;
            pershkrimKonfigAmbjente = pershkrimkonfigambjente;
            idKategori = idkategori;
            radha = radhaKonfig;
            defaultKonfigAmbjente = defaultkonfigambjente;
            idNivel = nivel;
            idSkemeKontabel = SK;
            idNdermarje = idnderm;
            idKonfigurimi = idkonf;
            idStatusDok = idstatusdok;
            idPerdoruesi = idperdorues;
            idKonfigFormatNr = idKonfigFormatNumri;
            lloji = Lloji;
            formatMobile = formatmobile;
            oColAtributet = new colAtributeTrupi();
            oColKushtet = new colKusht();
        }

        public clsKonfigurimAmbjenti()
        {
        }
        public clsKonfigurimAmbjenti(int idKonfigAmbjente)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                mbushKonfigurimAmbjenti(data.ktheKonfigAmbjSipasId(idKonfigAmbjente));
                ImbLogger.LogErrorShitje("Missing - cache idKonfigAmbjente");
                System.Diagnostics.Debug.WriteLine($"Missing - cache idKonfigAmbjente");
            }
        }
        public clsKonfigurimAmbjenti(int idKonfigAmbjente, clsDatabaseShare data)
        {
            if (idKonfigAmbjente == 0)
                return;
            mbushKonfigurimAmbjenti(data.TransCache.getKonfigAmbiente(idKonfigAmbjente, data));
            //mbushKonfigurimAmbjenti(data.ktheKonfigAmbjSipasId(idKonfigAmbjente));
        }
        public clsKonfigurimAmbjenti(int idKonfigAmbjente, int idGjuha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            mbushKonfigurimAmbjenti(data.ktheKonfigAmbjSipasId(idKonfigAmbjente, idGjuha));
            System.Diagnostics.Debug.WriteLine($"Missing - cache idKonfigAmbjente");
            data.Dispose();
        }
        public clsKonfigurimAmbjenti(string kodKonfigurim, int idNdermarrje, clsDatabaseShare data)
        {

            mbushKonfigurimAmbjenti(data.TransCache.getKonfigAmbiente(kodKonfigurim, idNdermarrje, data));
            //mbushKonfigurimAmbjenti(data.ktheKonfigAmbjSipasKod(kodKonfigurim, idNdermarrje, true));
        }

        public clsKonfigurimAmbjenti(DataRow rreshti)
        {

            mbushKonfigurimAmbjenti(rreshti);
        }

        /// <summary>
        /// Kthen id-ne e konfigurimit default per nivelin.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idNivel"></param>
        /// <returns></returns>
        public static int ktheIdKonfigurimi(int idNdermarrje, int idNivel)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                System.Diagnostics.Debug.WriteLine($"Missing - cache idKonfigAmbjente");
                return data.merrIdKonfigDefaultNivel(idNivel, idNdermarrje);

            }
        }

        public static int ktheIdKonfigurimiMeAutorizim(int idNdermarrje, int idNivel, int idPerdoruesi)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                System.Diagnostics.Debug.WriteLine($"Missing - cache idKonfigAmbjente");
                return data.merrIdKonfigDefaultNivelMeAutorizim(idNivel, idNdermarrje, idPerdoruesi);

            }
        }

        /// <summary>
        /// Kthen id-ne e konfigurimit default per nivelin.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idNivel"></param>
        /// <returns></returns>
        public static int ktheIdKategori(int idKonfigAmbjenti)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            System.Diagnostics.Debug.WriteLine($"Missing - cache idKonfigAmbjente");
            int idKategori = ktheIdKategori(idKonfigAmbjenti, data);
            data.Dispose();
            return idKategori;
        }

        /// <summary>
        /// Kthen id-ne e konfigurimit default per nivelin.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idNivel"></param>
        /// <returns></returns>
        public static int ktheIdKategori(int idKonfigAmbjenti, clsDatabaseShare data)
        {
            System.Diagnostics.Debug.WriteLine($"Missing - cache idKonfigAmbjente");
            int idKategori = data.merrIdKategoriNgaKonfigAmbjente(idKonfigAmbjenti);
            return idKategori;
        }

        /// <summary>
        /// Kthen id-ne e konfigurimit default per nivelin.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idNivel"></param>
        /// <returns></returns>
        public static int ktheIdNivel(int idKonfigAmbjenti)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                System.Diagnostics.Debug.WriteLine($"Missing - cache idKonfigAmbjente");
                int idNivel = data.merrIdNivelNgaKonfigAmbjente(idKonfigAmbjenti);
                return idNivel;
            }
        }
        public static int ktheIdNivel(int idKonfigAmbjenti, clsDatabaseShare data)
        {
            System.Diagnostics.Debug.WriteLine($"Missing - cache idKonfigAmbjente");
            return data.merrIdNivelNgaKonfigAmbjente(idKonfigAmbjenti);
            
        }
        /// <summary>
        /// Kthen kodin e konfigurimit sipas id-se se tij.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idNivel"></param>
        /// <returns></returns>
        public static string ktheKodKonfigurimi(int idKonfigAmbjenti)
        {
            System.Diagnostics.Debug.WriteLine($"Missing - cache idKonfigAmbjente");
            clsDatabaseShare data = new clsDatabaseShare();
            string kodi = ktheKodKonfigurimi(idKonfigAmbjenti, data);
            data.Dispose();
            return kodi;
        }

        /// <summary>
        /// Kthen kodin e konfigurimit sipas id-se se tij.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idNivel"></param>
        /// <returns></returns>
        public static string ktheKodKonfigurimi(int idKonfigAmbjenti, clsDatabaseShare data)
        {
            System.Diagnostics.Debug.WriteLine($"Missing - cache idKonfigAmbjente");
            string kodi = data.merrKodNivelNgaKonfigAmbjente(idKonfigAmbjenti);
            return kodi;
        }
        public static int ktheIdNiveliSipasIdKonfigurimi(int idkonfigAmbjenti)
        {
            System.Diagnostics.Debug.WriteLine($"Missing - cache idKonfigAmbjente");
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.merrIdNiveliSipasIdKonfigurimi(idkonfigAmbjenti);
            }
        }

        public static int ktheIdNiveliSipasIdKonfigurimiMeAutorizim(int idkonfigAmbjenti, int idPerdoruesi)
        {
            System.Diagnostics.Debug.WriteLine($"Missing - cache idKonfigAmbjente");
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.merrIdNiveliSipasIdKonfigurimiMeAutorizim(idkonfigAmbjenti, idPerdoruesi);
            }
        }

        #endregion

        #region Properties

        public int Lloji
        {
            get { return lloji; }
            set { lloji = value; }
        }
        public int IdKonfigAmbjente
        {
            get { return idKonfigAmbjente; }
            set { idKonfigAmbjente = value; }
        }

        public String KodKonfigAmbjente
        {
            get { return kodKonfigAmbjente == null ? "" : kodKonfigAmbjente; }
            set { kodKonfigAmbjente = value; }
        }

        /// <summary>
        /// Pershkrimi i konfigurimit ne shqip
        /// </summary>
        public String PershkrimKonfigAmbjente
        {
            get { return pershkrimKonfigAmbjente; }
            set { pershkrimKonfigAmbjente = value; }
        }

        public int IdKategori
        {
            get { return idKategori; }
            set { idKategori = value; }
        }

        public int Radha
        {
            get { return radha; }
            set { radha = value; }
        }

        /// <summary>
        /// Kthen/Vendos nivelet e autorizimeve
        /// </summary>
        public string IdNivelAutorizimi
        {
            get
            {
                return idNivelAutorizimi;
            }
            set
            {
                this.idNivelAutorizimi = value;
            }

        }

        public bool DefaultKonfigAmbjente
        {
            get { return defaultKonfigAmbjente; }
            set { defaultKonfigAmbjente = value; }
        }

        public int IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }

        public int IdSkemeKontabel
        {
            get { return idSkemeKontabel; }
            set { idSkemeKontabel = value; }
        }

        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        public int IdKonfigurimi
        {
            get { return idKonfigurimi; }
            set { idKonfigurimi = value; }
        }

        public colAtributeTrupi OColAtributet
        {
            get { return oColAtributet; }
            set { oColAtributet = value; }
        }

        public colKusht OColKushtet
        {
            get { return oColKushtet; }
            set { oColKushtet = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        public int IdKonfigFormatNr
        {
            get { return idKonfigFormatNr; }
            set { idKonfigFormatNr = value; }
        }

        /// <summary>
        /// Pershkrimi i konfigurimit ne anglisht
        /// </summary>
        public string PershkrimKonfigAmbjenteEng
        {
            get { return pershkrimKonfigAmbjenteEng; }
            set { pershkrimKonfigAmbjenteEng = value; }
        }

        /// <summary>
        /// Pershkrimi i konfigurimit ne frengjisht
        /// </summary>
        public string PershkrimKonfigAmbjente_fr
        {
            get { return pershkrimKonfigAmbjente_fr; }
            set { pershkrimKonfigAmbjente_fr = value; }
        }

        /// <summary>
        /// format printimi mobile
        /// </summary>
        public int FormatMobile
        {
            get { return formatMobile; }
            set { formatMobile = value; }
        }
        #endregion

        #region Metoda Publike

        public clsMesazh ruajKonfigurimDokumentash(clsKonfigurimAmbjenti konf, colGridaKoka colGridaKoka, colGridaTrupi oTrupLupa, int idGjuha,out int idGridaKokaRe)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseShare dbshare = new clsDatabaseShare();
            dbshare.beginTransaksion();
            try
            {
                idGridaKokaRe = 0;
                int idK;
                mesazh = dbshare.ruajKonfigurim(out idK, konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente, konf.IdKategori, konf.Radha, konf.DefaultKonfigAmbjente, konf.IdNivel, konf.IdSkemeKontabel, konf.IdNdermarje, konf.IdKonfigurimi, konf.idStatusDok, konf.idPerdoruesi, konf.idKonfigFormatNr, konf.PershkrimKonfigAmbjenteEng, konf.lloji, konf.formatMobile, konf.PershkrimKonfigAmbjente_fr);
                if (!mesazh.Status)
                {
                    dbshare.rollbackTransaksion();
                    return mesazh;
                }

                konf.IdKonfigAmbjente = idK;


                foreach (clsAtributeTrupi o in konf.OColAtributet)
                {
                    o.IdKonfigAmbjente = konf.IdKonfigAmbjente;
                    int tipi = clsKontroll.ktheTipKontrolli(o.IdKontroll, dbshare);
                    if (tipi != 0 && tipi != 6)
                        o.VlereDefaultSipasGjuhes(idGjuha);
                    mesazh = dbshare.ruajAtribut(o.IdKontroll, o.IdKonfigAmbjente, o.VlereDefault, o.Visible, o.Enabled, o.IdKonfigAmbjenteLupa, o.Identifikues, o.Rreshti, o.Kolona, o.Detyrueshme, o.IdNrAutomatik, o.VlereDefaultEng, o.ShfaqMobile, o.RenditjaMobile, o.Unike, o.VlereDefault_fr);
                     
                    //mesazh = dbshare.ruajAtributSqEng(o.IdKontroll, o.IdKonfigAmbjente, o.VlereDefault, o.Visible, o.Enabled, o.IdKonfigAmbjenteLupa, o.Identifikues, o.Rreshti, o.Kolona, o.Detyrueshme, o.IdNrAutomatik, o.VlereDefaultEng, o.ShfaqMobile, o.RenditjaMobile, o.Unike, o.VlereDefault_fr);
                    if (!mesazh.Status)
                    {
                        dbshare.rollbackTransaksion();
                        return mesazh;
                    }
                }
                clsDatabaseAdmin data = new clsDatabaseAdmin(dbshare);

                foreach (clsGridaKoka gridakoka in colGridaKoka)
                {
                    gridakoka.IdKonfigurim = konf.IdKonfigAmbjente;
                    if (gridakoka.IdGridaKoka == 0)
                    {
                        mesazh = dbshare.ruajGridaKoka(gridakoka);
                        idGridaKokaRe = gridakoka.IdGridaKoka;
                        if (!mesazh.Status)
                        {
                            dbshare.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                    foreach (DbCore.DbAdmin.clsGridaTrupi o in gridakoka.OColGridaTrupi)
                    {
                        o.IdKoka = gridakoka.IdGridaKoka;
                        mesazh = dbshare.ruajGridaTrupi(o);
                        if (!mesazh.Status)
                        {
                            dbshare.rollbackTransaksion();
                            return mesazh;
                        }

                        if (o.IdKonfigAmbjenteLupa == 1 && !string.IsNullOrEmpty(o.KodLupa))
                        {
                            foreach (DbCore.DbAdmin.clsGridaTrupi obj in oTrupLupa)
                            {
                                if (o.KodiTrupi != obj.KodiTrupi) continue;
                                obj.IdTrupi = o.IdTrupi;
                                mesazh = obj.ruajMultipleLupa(konf.IdKonfigAmbjente, data);
                                if (!mesazh.Status)
                                {
                                    dbshare.rollbackTransaksion();
                                    return mesazh;
                                }
                            }
                        }
                    }
                }


                foreach (clsKusht k in konf.OColKushtet)
                {
                    k.IdKonfigurimAmbjente = konf.IdKonfigAmbjente;
                    mesazh = k.ruaj();
                    if (!mesazh.Status)
                    {
                        dbshare.rollbackTransaksion();
                        return mesazh;
                    }

                    if (k.Kodi == "LLD")
                    {
                        foreach (clsKonfLlojRreshtiVlere konfLlojRreshti in k.ColKonfLlojRreshtiVlere)
                        {
                            konfLlojRreshti.IdKushTemplate = k.IdKushtTemplate;
                            mesazh = konfLlojRreshti.ruaj();
                            if (!mesazh.Status)
                            {
                                dbshare.rollbackTransaksion();
                                return mesazh;
                            }
                        }
                        //if (k.Vlera != 0)
                        //{
                        //    char[] lloj = k.Vlera.ToString().ToCharArray();
                        //    for (int i = 0; i < lloj.Length; i++)
                        //    {
                        //        clsKonfLlojRreshtiVlere konfllreshtvler = new clsKonfLlojRreshtiVlere();
                        //        konfllreshtvler.IdKushTemplate = k.IdKushtTemplate;
                        //        konfllreshtvler.IdLlojRreshti = Convert.ToInt32(lloj[i].ToString());
                        //        konfllreshtvler.Rend = i + 1;
                        //        mesazh = konfllreshtvler.ruaj();
                        //        if (!mesazh.Status)
                        //        {
                        //            dbshare.rollbackTransaksion();
                        //            return mesazh;
                        //        }
                        //    }
                        //}
                    }
                }

                if (konf.IdNivelAutorizimi != "")
                {
                    colLidhjetAutorizim colLidhjet = new colLidhjetAutorizim();
                    string[] pars1 = konf.IdNivelAutorizimi.Split(',');
                    for (int i = 0; i < pars1.Length; i++)
                    {
                        clsLidhjeAutorizim lidhje = new clsLidhjeAutorizim();
                        lidhje.IdAutorizimeKoka = clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                        colLidhjet.Add(lidhje);
                    }
                    foreach (clsLidhjeAutorizim o in colLidhjet)
                    {
                        o.IdLloji = 17;
                        o.IdLidhese = konf.IdKonfigAmbjente;
                        mesazh = data.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                        if (!mesazh.Status)
                        {
                            dbshare.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                }

                if (!mesazh.Status)
                {
                    dbshare.rollbackTransaksion();
                    return mesazh;
                }

                dbshare.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                idGridaKokaRe = 0;
                dbshare.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public static int ktheIdSipasKategoriDheNderm(int idKategori, int idNdermarrje, clsDatabaseShare data)
        {
            return data.ktheIdKonfigAmbjentiSipasKategoriDheNderm(idKategori, idNdermarrje);
        }

        public static int ktheIdSipasKategoriDheNderm(int idKategori, int idNdermarrje)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.ktheIdKonfigAmbjentiSipasKategoriDheNderm(idKategori, idNdermarrje);
            }
        }

       
        public clsMesazh modifikoKonfigurimDokumentash(clsKonfigurimAmbjenti konf, colGridaKoka colGridaKoka, colGridaTrupi oTrupLupa, int idGjuha)
        {
            colLidhjetAutorizim colLidhjetAutorizim = new colLidhjetAutorizim(konf.IdKonfigAmbjente, 17);
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseShare db = new clsDatabaseShare();
            db.beginTransaksion();

            try
            {
                mesazh = db.modifikoKonfigurim(konf.IdKonfigAmbjente, konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente, konf.IdKategori, konf.Radha, konf.DefaultKonfigAmbjente, konf.IdNivel, konf.IdSkemeKontabel, konf.IdNdermarje, konf.IdKonfigurimi, konf.idStatusDok, konf.idPerdoruesi, konf.idKonfigFormatNr, konf.PershkrimKonfigAmbjenteEng, konf.lloji, konf.formatMobile, konf.PershkrimKonfigAmbjente_fr);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return mesazh;
                }

                foreach (clsAtributeTrupi o in konf.OColAtributet)
                {
                    o.IdKonfigAmbjente = konf.IdKonfigAmbjente;
                    int tipi = clsKontroll.ktheTipKontrolli(o.IdKontroll, db);
                    if (tipi == 0 || tipi == 6)
                        mesazh = db.modifikoAtribut(o.IdKontroll, o.IdKonfigAmbjente, o.VlereDefault, o.Visible, o.Enabled, o.IdKonfigAmbjenteLupa, o.Identifikues, o.Rreshti, o.Kolona, o.Detyrueshme, o.IdNrAutomatik, idGjuha, o.VlereDefaultEng, o.ShfaqMobile, o.RenditjaMobile, o.Unike, o.VlereDefault_fr);
                    else
                    {
                        o.VlereDefaultSipasGjuhes(idGjuha);
                        mesazh = db.modifikoAtributSqEng(o.IdKontroll, o.IdKonfigAmbjente, o.VlereDefault, o.Visible, o.Enabled, o.IdKonfigAmbjenteLupa, o.Identifikues, o.Rreshti, o.Kolona, o.Detyrueshme, o.IdNrAutomatik, o.VlereDefaultEng, o.ShfaqMobile, o.RenditjaMobile, o.Unike, o.VlereDefault_fr);
                    }

                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        return mesazh;
                    }
                }

                clsDatabaseAdmin data = new clsDatabaseAdmin(db);
                clsGridaTrupi tr = new clsGridaTrupi();
                mesazh = tr.fshiLupaMultiple(data, konf.IdKonfigAmbjente);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return mesazh;
                }
                foreach (clsGridaKoka grKok in colGridaKoka)
                {
                    foreach (clsGridaTrupi o in grKok.OColGridaTrupi)
                    {
                        mesazh = o.ruajTrup(data, idGjuha);
                        if (!mesazh.Status)
                        {
                            db.rollbackTransaksion();
                            return mesazh;
                        }

                        if (o.IdKonfigAmbjenteLupa == 1 && !string.IsNullOrEmpty(o.KodLupa))
                        {
                            foreach (clsGridaTrupi obj in oTrupLupa)
                            {
                                if (o.KodiTrupi != obj.KodiTrupi) continue;
                                obj.IdTrupi = o.IdTrupi;
                                mesazh = obj.ruajMultipleLupa(konf.IdKonfigAmbjente, data);

                                if (!mesazh.Status)
                                {
                                    db.rollbackTransaksion();
                                    return mesazh;
                                }
                            }
                        }
                    }
                }
                
                foreach (clsKusht k in konf.OColKushtet)
                {
                    k.IdKonfigurimAmbjente = konf.IdKonfigAmbjente;
                    mesazh = k.modifiko();
                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        return mesazh;
                    }

                    if (k.Kodi == "LLD")
                    {
                        clsKonfLlojRreshtiVlere konfllojrrsht = new clsKonfLlojRreshtiVlere();
                        konfllojrrsht.IdKushTemplate = k.IdKushtTemplate;

                        mesazh = konfllojrrsht.fshi();
                        if (!mesazh.Status)
                        {
                            db.rollbackTransaksion();
                            return mesazh;
                        }
                        foreach (clsKonfLlojRreshtiVlere konfLlojRreshti in k.ColKonfLlojRreshtiVlere)
                        {
                            konfLlojRreshti.IdKushTemplate = k.IdKushtTemplate;
                            mesazh = konfLlojRreshti.ruaj();
                            if (!mesazh.Status)
                            {
                                db.rollbackTransaksion();
                                return mesazh;
                            }
                        }
                        //char[] lloj = k.Vlera.ToString().ToCharArray();
                        //for (int i = 0; i < lloj.Length; i++)
                        //{
                        //    clsKonfLlojRreshtiVlere konfllreshtvler = new clsKonfLlojRreshtiVlere();
                        //    konfllreshtvler.IdKushTemplate = k.IdKushtTemplate;
                        //    konfllreshtvler.IdLlojRreshti = Convert.ToInt32(lloj[i].ToString());
                        //    konfllreshtvler.Rend = i + 1;
                        //    mesazh = konfllreshtvler.ruaj();
                        //    if (!mesazh.Status)
                        //    {
                        //        db.rollbackTransaksion();
                        //        return mesazh;
                        //    }
                        //}
                        //j++;
                    }
                }
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return mesazh;
                }

                colLidhjetAutorizim colLidhjet = new colLidhjetAutorizim();
                if (konf.IdNivelAutorizimi != "")
                {
                    string[] pars1 = konf.IdNivelAutorizimi.Split(',');
                    for (int i = 0; i < pars1.Length; i++)
                    {
                        clsLidhjeAutorizim lidhje = new clsLidhjeAutorizim();
                        lidhje.IdAutorizimeKoka = clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                        colLidhjet.Add(lidhje);
                    }
                }
                for (int i = 0; i < colLidhjet.Count; i++)
                {
                    int idAutorizimKoka = colLidhjet[i].IdAutorizimeKoka;
                    if (idAutorizimKoka == -1)
                        continue;
                    colLidhjet[i].IdLloji = 17;
                    colLidhjet[i].IdLidhese = konf.IdKonfigAmbjente;
                    clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizim.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                    if (lidhjeNjejte != null)
                    {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2) 
                        colLidhjetAutorizim.Remove(lidhjeNjejte);
                        continue;
                    }
                    mesazh = data.ruajLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka, 1);
                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        return mesazh;
                    }
                }
                //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
               
                mesazh = colLidhjetAutorizim.FshiLidhjeAutorizim(colLidhjetAutorizim, konf.idPerdoruesi, data);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return mesazh;
                }


                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return mesazh;
                }
                db.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;

            }
            catch (Exception ce)
            {
                db.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh fshiKonfigurimDokumentash(int idGjuha, clsKonfigurimAmbjenti konf)
        {
            colLidhjetAutorizim colLidhjeAutorizim = new colLidhjetAutorizim(konf.IdKonfigAmbjente, 17);
            clsDatabaseShare db = new clsDatabaseShare();

            try
            {
                db.beginTransaksion();
                clsMesazh mesazh = new clsMesazh();
                clsMesazh mesazhAdmin = new clsMesazh(true);
                clsGridaKoka gridakoka = new clsGridaKoka();
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(db);
                gridakoka.IdKonfigurim = konf.IdKonfigAmbjente;
                gridakoka.mbushGridaKokaByKonfigurim(konf.IdKonfigAmbjente);

                mesazhAdmin = dbAdmin.fshiTrupin(gridakoka.IdGridaKoka);
                if (mesazhAdmin.Status)
                {
                    mesazhAdmin = dbAdmin.fshiKoka(gridakoka.IdGridaKoka);
                    if (mesazhAdmin.Status)
                    {
                        mesazh = db.fshiAtribut(konf.IdKonfigAmbjente);
                        if (mesazh.Status)
                        {
                            mesazh = db.fshiKushtTemplateByKonfigurim(konf.IdKonfigAmbjente);
                            if (mesazh.Status)
                            {
                                foreach (clsLidhjeAutorizim o in colLidhjeAutorizim)
                                {
                                    if (mesazhAdmin.Status)
                                    {
                                        mesazhAdmin = dbAdmin.fshiLidhjeAutorizim(o.IdLidhjeAutorizim);
                                    }
                                    else
                                    {
                                        db.rollbackTransaksion();
                                        mesazh.Status = false;
                                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                                        return mesazh;
                                    }
                                }
                                if (mesazhAdmin.Status && mesazh.Status)
                                {
                                    mesazh = db.fshiKonfigurim(konf.IdKonfigAmbjente);
                                    if (mesazhAdmin.Status && mesazh.Status)
                                    {
                                        db.commitTransaksion();
                                        mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                                        return mesazh;
                                    }
                                    else
                                    {
                                        db.rollbackTransaksion();
                                        return mesazh;
                                    }
                                }
                                else
                                {
                                    db.rollbackTransaksion();
                                    mesazh.Status = false;
                                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                                    return mesazh;
                                }
                            }
                            else
                            {
                                db.rollbackTransaksion();
                                return mesazh;
                            }
                        }
                        else
                        {
                            db.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                    else
                    {
                        db.rollbackTransaksion();
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                        return mesazh;
                    }
                }
                else
                {
                    db.rollbackTransaksion();
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                db.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh ruaj(colGridaKoka colGridaKoka, colGridaTrupi lupa, int idGjuha,int idKonfigPrindi)
        {
            clsKonfigurimAmbjenti data = new clsKonfigurimAmbjenti();
            int idGridaKokaRe = 0;
            clsMesazh u_ruajt = data.ruajKonfigurimDokumentash(this, colGridaKoka, lupa, idGjuha,out idGridaKokaRe);
            if (idKonfigPrindi > 0)
            {
                colFiltratGrida colFiltra = new colFiltratGrida();
                colFiltra.shtoFiltraGridaNgaKlonimi(idKonfigPrindi, idGridaKokaRe);
            }

            return u_ruajt;
        }

        public clsMesazh modifiko(colGridaKoka colGridaKoka, colGridaTrupi lupa, int idGjuha)
        {
            clsKonfigurimAmbjenti data = new clsKonfigurimAmbjenti();
            clsMesazh u_modifikua = data.modifikoKonfigurimDokumentash(this, colGridaKoka, lupa, idGjuha);
            return u_modifikua;
        }

        public clsMesazh fshi()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_fshi = data.fshiKonfigurimStatus(this.idKonfigAmbjente, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// kthen konfigurimin ne default
        /// </summary>
        /// <param name="idkonfig"></param>
        /// <param name="indermarje"></param>
        /// <param name="idndermarjenga"></param>
        /// <param name="idperdorues"></param>
        /// <returns></returns>
        public static clsMesazh ktheDefault(int idkonfig, int indermarje, int idndermarjenga, int idperdorues)
        {
            using (var scope = new MyTransactionScope())
            using(clsDatabaseShare data = new clsDatabaseShare())
            {
                try
                {
                    clsMesazh ruaj = data.ktheDefault(idkonfig, indermarje, idndermarjenga, idperdorues);
                    if (!ruaj)
                        return ruaj;
                    
                   scope.Complete(); 
                   return ruaj; 
                }
                catch(Exception ex )
                {
                    ImbLogger.Error(ex);
                    return new MesazhGabimi(ex.Message);
                }
            }
        }

        public clsKonfigurimAmbjenti merrSipasIdKategoriIdNivel(int idperdorues, bool meLloj = true)
        {
            colKonfigurimAmbjenti data = new colKonfigurimAmbjenti();
            data.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(this.IdKategori, this.IdNivel, idperdorues, meLloj);
            if (data.Count > 0)
                return data[0];
            else return new clsKonfigurimAmbjenti();
        }

        public static int merrRadheMax(int idnivel)
        {
            clsDatabaseShare db = new clsDatabaseShare();
            int nr = db.merrRadheMax(idnivel);
            db.Dispose();
            return nr;
        }

        public clsKonfigurimAmbjenti merrSipasId()
        {
            clsKonfigurimAmbjenti data = new clsKonfigurimAmbjenti();
            if (data.mbushKonfigAmbjSipasId(this.IdKonfigAmbjente))
                return data;
            else
                return new clsKonfigurimAmbjenti();
        }

        public clsKonfigurimAmbjenti merrSipasKodit()
        {
            clsKonfigurimAmbjenti data = new clsKonfigurimAmbjenti();
            data.mbushKonfigAmbjSipasKod(this.KodKonfigAmbjente, this.IdNdermarje);
            return data;
        }

        public bool mbushKonfigDefaultKomponentes(int idKomponente, int idndermarrje)
        {
            using (var data = new clsDatabaseShare())
            {
                var dt = data.merrKonfigDefaultKomponentes(idKomponente, idndermarrje);
                if (dt == null || dt.Rows.Count == 0) return false;
                return mbushKonfigurimAmbjenti(dt.Rows[0]);
            }
        }

        public bool mbushKonfiguriminMeKod(string kodKonfig, int idndermarje, int idGjuha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushKonfigurimAmbjenti(data.merrKonfiguriminMeKod(kodKonfig, idndermarje, idGjuha));
            data.Dispose();
            return mbush;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kodKonfig"></param>
        /// <param name="idndermarje"></param>
        /// <returns></returns>
        public static int ktheIdKonfigurimiMeKod(string kodKonfig, int idndermarje)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.merrIdKonfigurimiMeKod(kodKonfig, idndermarje);
            }
        }

        /// <summary>
        /// Merr nje rresht me gjithe id e konfigurmeve qe duhen per rillogaritjen e amortizimit
        /// </summary>
        /// <param name="idndermarje"></param>
        /// <returns></returns>
        public static DataRow ktheIdKonfigurimiTeAmortizimeve(int idndermarje)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.merrIdKonfigurimiTeAmortizimeve(idndermarje);
            }
        }

        public bool mbushKonfiguriminMeID(int idKonfig)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return mbushKonfigurimAmbjentiShtim(data.merrKonfiguriminMeID(idKonfig));
            }
        }
        /// <summary>
        /// merr konfigurimet pa llojin 
        /// </summary>
        /// <param name="idKonfig"></param>
        /// <returns></returns>
        public bool mbushkonfigPaLloj(int idKonfig)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return mbushKonfigurimAmbjentiShtim(data.merrkonfigurimPaLloj(idKonfig));
            }
        }

        public bool mbushKonfigAmbjSipasId(int idKonfigAmbjente)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushKonfigurimAmbjenti(data.ktheKonfigAmbjSipasId(idKonfigAmbjente));
            data.Dispose();
            return mbush;
        }
        public bool mbushKonfigAmbjSipasId(int idKonfigAmbjente, clsDatabaseShare data)
        {
            return mbushKonfigurimAmbjenti(data.ktheKonfigAmbjSipasId(idKonfigAmbjente));
        }

        public bool mbushKonfigAmbjSipasId(int idKonfigAmbjente, int idGjuha, bool meLloj = true)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushKonfigurimAmbjenti(data.ktheKonfigAmbjSipasId(idKonfigAmbjente, idGjuha, meLloj));
            data.Dispose();
            return mbush;
        }

        public bool mbushKonfigAmbjSipasId(int idKonfigAmbjente, clsDatabaseShare data, int idGjuha)
        {
            return mbushKonfigurimAmbjenti(data.ktheKonfigAmbjSipasId(idKonfigAmbjente, idGjuha));
        }

        public bool mbushKonfigAmbjSipasKod(string kodKonfig, int idndermarje, bool meLloj = true)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return mbushKonfigAmbjSipasKod(kodKonfig, idndermarje, data, meLloj);
            }
        }

        public bool mbushKonfigAmbjSipasKod(string kodKonfig, int idndermarje, clsDatabaseShare data, bool meLloj = true)
        {
            bool mbush = mbushKonfigurimAmbjenti(data.ktheKonfigAmbjSipasKod(kodKonfig, idndermarje, meLloj));
            return mbush;
        }

        public static bool kaAutorizimKonfigurimi(int idkonfig, int idperdorues)
        {
            if (idkonfig == 0) return true;
            clsDatabaseShare data = new clsDatabaseShare();
            bool kaAutorizim = data.kaAutorizimKonfigurim(idkonfig, idperdorues);
            data.Dispose();
            return kaAutorizim;
        }
        public static bool kaTeDrejteDheAutorizimTeHapeNivelRegjistrimiSipasKonfigurimit(int idkonfig, int idperdorues, int idViti, string komponente)
        {
            if (idkonfig == 0) return true;
            clsDatabaseShare data = new clsDatabaseShare();
            bool kaAutorizimDheTeDrejte = data.kaTeDrejteDheAutorizimTeHapeNivelRegjistrimiSipasKonfigurimit(idkonfig, idperdorues, idViti, komponente);
            data.Dispose();
            return kaAutorizimDheTeDrejte;
        }

        public static bool kaTeDrejteDheAutorizimTeHapeAmbientSipasKategorise(int idKategoria, int idperdorues, int idViti, int idKomponente, int idNdermarrje)
        {
            if (idKategoria == 0) return true;
            clsDatabaseShare data = new clsDatabaseShare();
            bool kaAutorizimDheTeDrejte = data.kaTeDrejteDheAutorizimTeHapeAmbientSipasKategorise(idKategoria, idperdorues, idViti, idKomponente, idNdermarrje);
            data.Dispose();
            return kaAutorizimDheTeDrejte;
        }
        public static bool getAutorizimKonfigurimi(int idKonfigAmbjente, int idPerdoruesi, clsDatabaseShare shareDB)
        {
            return shareDB.TransCache.getAutorizimeSipasIdKonfigDheIdPerdorues(idKonfigAmbjente, idPerdoruesi, shareDB);
        }

        public static string kthePershkriminSipasID(int idkonfig)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.kthePershkrimKonfigAmbjSipasKod(idkonfig);
            }

        }

        public static clsMesazh ekzistonKonfigurimSipasIDKONFIG(int idKofigGjen, int idNdermarrje)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.ekzistonKonfigurimSipasIDKONFIG(idKofigGjen, idNdermarrje);
            }
        }

        private clsKonfigurimAmbjenti KrijoKonfigurim(int idGjuha,
            int idPerdorues, 
            int idNdermarrja, 
            int idKategoria, 
            int idKonfigurim,
            int idNiveli, 
            string textFormatNr, 
            string shtimModifikim, 
            string pershkrimKonfig, 
            string pershkrimKonfigEng,
            string pershkrimKonfigFr,
            string kodKonfig, 
            string idNivelAutorizim, 
            string llojiValue, 
            string prioriteti, 
            object[] atributet,
            object[] kushtet,
            string radhaText,
            int idSkemaKontabel,
            ref int idKomponente)
        {

            var clsnivel = new clsNivelRegjistrimi();
            clsnivel.mbushNivelRegjistrimiSipasIdPaKonvertime(idNiveli);

            var konfekzistues = new clsKonfigurimAmbjenti
            {
                IdKategori = idKategoria,
                IdNivel = clsnivel.IdNivel
            };

            int idKonfFormatNr = 0;

            if (textFormatNr != "")
            {
                var formatNr = new clsFormatiKonfig();
                formatNr.mbushFormatNrKonfigSipasKodit(textFormatNr, idNdermarrja);
                idKonfFormatNr = formatNr.IdFormatKonfig;
            }

            int radha = shtimModifikim != "modifikim" && shtimModifikim != "klonim"
                ? merrRadheMax(konfekzistues.IdNivel) + 1
                : Convert.ToInt32(radhaText);


            konfekzistues.Radha = radha;
            konfekzistues = konfekzistues.merrSipasIdKategoriIdNivel(IdPerdoruesi, false);

            var oKat = new clsKategoriNivelDok { IdKategori = idKategoria };
            oKat = oKat.merrSipasId();

            if (oKat != null)
            {
                idKomponente = oKat.IdKomponente;
                if (idKomponente == 508)
                    idKomponente = 506;
            }
            if (oKat.IdKategori == 9)
            {
                idKomponente = clsnivel.Radha;
            }


            var kat = new clsKategoriNivelDok();
            if (idKomponente != -1)
                kat.IdKomponente = idKomponente;

            var oKonfig = new clsKonfigurimAmbjenti
            {
                IdNivel = clsnivel.IdNivel,
                IdKategori = idKategoria
            };
            oKonfig = oKonfig.merrSipasIdKategoriIdNivel(IdPerdoruesi, false);

            var koka = new clsKonfigurimAmbjenti
            {
                IdNivelAutorizimi = idNivelAutorizim,
                IdKategori = idKategoria,
                IdNivel = clsnivel.IdNivel,
                Radha = radha,
                KodKonfigAmbjente = kodKonfig,
                PershkrimKonfigAmbjente = pershkrimKonfig,
                PershkrimKonfigAmbjenteEng = pershkrimKonfigEng,
                PershkrimKonfigAmbjente_fr = pershkrimKonfigFr,
                DefaultKonfigAmbjente = shtimModifikim == "modifikim" && konfekzistues.DefaultKonfigAmbjente,
                IdStatusDok = 1,
                IdPerdoruesi = idPerdorues,
                IdKonfigFormatNr = idKonfFormatNr
            };

            int lloji;
            if (!int.TryParse(llojiValue, out lloji))
                throw new MyException("Lloji jo i vlefshem!");
            
            koka.Lloji = lloji;
            koka.OColAtributet = new colAtributeTrupi();
            koka.IdSkemeKontabel = idSkemaKontabel;
            koka.IdNdermarje = idNdermarrja;
            koka.IdKonfigurimi = idKonfigurim;
            koka.OColAtributet = colAtributeTrupi.KrijoAtribute(atributet, llojiValue, idNdermarrja);
            koka.OColKushtet = colKusht.KrijoKushte(kushtet, prioriteti);
            

            if (koka.OColAtributet.Count == 0)
                koka.OColAtributet.mbushAtributetKontrolleveSipasKonfigurimit(oKonfig.IdKonfigAmbjente);

            if (koka.OColKushtet.Count == 0)
            {
                var k = new clsKusht();
                koka.OColKushtet = k.merrTeGjitheKushteKonfigurimi(oKonfig.IdKonfigAmbjente);
            }

            return koka;
        }

        public void Ruaj(int idGjuha,
            int idPerdorues,
            int idNdermarrja,
            int idKategoria,
            int idKonfigurim,
            int idNiveli,
            string textFormatNr,
            string shtimModifikim,
            string pershkrimKonfig,
            string pershkrimKonfigEng,
            string pershkrimKonfigFr,
            string kodKonfig,
            string idNivelAutorizim,
            string llojiValue,
            string prioriteti,
            object[] atributet,
            object[] kushtet,
            string radhaText,
            int idSkemaKontabel,
            ref int idKomponente,
            colGridaTrupi colTrupi,
            int idKonfigAmbjent)
        {
            var koka = KrijoKonfigurim(idGjuha, idPerdorues, idNdermarrja, idKategoria, idKonfigurim, idNiveli,
                textFormatNr, shtimModifikim, pershkrimKonfig, pershkrimKonfigEng, pershkrimKonfigFr, kodKonfig, idNivelAutorizim,
                llojiValue, prioriteti, atributet, kushtet, radhaText, idSkemaKontabel, ref idKomponente);

            colGridaTrupi oColTrupiLupat;
            var colGridaKoka = DbCore.DbAdmin.colGridaKoka.KrijoColGridaKoka(colTrupi, idNdermarrja, idKomponente,
                idKonfigurim, idGjuha, out oColTrupiLupat);

            clsMesazh mesazh;
            if (shtimModifikim == "shtim" || shtimModifikim == "klonim")
            {
                mesazh = koka.ruaj(colGridaKoka, oColTrupiLupat, idGjuha, idKonfigAmbjent);
                if (!mesazh.Status)
                    throw new MyException(mesazh.PershkrimMesazhi);
            }
            else
            {
                koka.IdKonfigAmbjente = idKonfigAmbjent;
                var konfivjeter = new clsKonfigurimAmbjenti();
                konfivjeter.mbushkonfigPaLloj(koka.IdKonfigAmbjente);
                koka.DefaultKonfigAmbjente = konfivjeter.DefaultKonfigAmbjente;

                mesazh = koka.modifiko(colGridaKoka, oColTrupiLupat, idGjuha);
                if (!mesazh.Status)
                    throw new MyException(mesazh.PershkrimMesazhi);
            }

        }

        #endregion

        #region Metoda internal
        public bool mbushAutorizime()
        {
            using (DbAdmin.clsDatabaseAdmin dbadm = new DbAdmin.clsDatabaseAdmin())
            {
                DbCore.DbAdmin.colLidhjetAutorizim lidhje = new DbCore.DbAdmin.colLidhjetAutorizim(this.idKonfigAmbjente, 17, dbadm);
                if (lidhje.Count != 0)
                {
                    IdNivelAutorizimi = DbCore.DbAdmin.clsAutorizimKoka.ktheKodAutorizim(lidhje[0].IdAutorizimeKoka, dbadm);
                    for (int i = 1; i < lidhje.Count; i++)
                        IdNivelAutorizimi += "," + DbCore.DbAdmin.clsAutorizimKoka.ktheKodAutorizim(lidhje[i].IdAutorizimeKoka, dbadm);
                }
                else
                    IdNivelAutorizimi = "";
                return true;
            }
        }
        public void mbushKonfigurimAmbjenti(clsKonfigurimAmbjenti ka)
        {

            idKonfigAmbjente = ka.idKonfigAmbjente;
            kodKonfigAmbjente = ka.kodKonfigAmbjente;
            pershkrimKonfigAmbjente = ka.pershkrimKonfigAmbjente;
            idKategori = ka.idKategori;
            radha = ka.radha;
            defaultKonfigAmbjente = ka.defaultKonfigAmbjente;
            idNivel = ka.idNivel;
            idSkemeKontabel = ka.idSkemeKontabel;
            idNdermarje = ka.idNdermarje;
            idKonfigurimi = ka.idKonfigurimi;
            idStatusDok = ka.idStatusDok;
            idPerdoruesi = ka.idPerdoruesi;
            dtKrijimi = ka.dtKrijimi;
            dtModifikimi = ka.dtModifikimi;
            oColAtributet = ka.oColAtributet;
            oColKushtet = ka.oColKushtet;
            idNivelAutorizimi = ka.idNivelAutorizimi;
            idKonfigFormatNr = ka.idKonfigFormatNr;
            pershkrimKonfigAmbjenteEng = ka.pershkrimKonfigAmbjenteEng;
            lloji = ka.lloji;
            formatMobile = ka.formatMobile;
            rreshti = ka.rreshti;
        }
        internal bool mbushKonfigurimAmbjenti(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    kodKonfigAmbjente = rreshti["KODKONFIGAMBJENTE"].ToString();
                    pershkrimKonfigAmbjente = rreshti["PERSHKRIMKONFIGAMBJENTE"].ToString();
                    int.TryParse(rreshti["IDKATDOK"].ToString(), out idKategori);
                    int.TryParse(rreshti["RADHA"].ToString(), out radha);
                    bool.TryParse(rreshti["DEFAULTKONFIGAMBJENTE"].ToString(), out defaultKonfigAmbjente);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDSKEMKONT"].ToString(), out idSkemeKontabel);
                    int.TryParse(rreshti["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(rreshti["IDKONFIGURIMI"].ToString(), out idKonfigurimi);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(rreshti["IDKONFIGFORMATNR"].ToString(), out idKonfigFormatNr);
                    int.TryParse(rreshti["LLOJI"].ToString(), out lloji);
                    int.TryParse(rreshti["FORMATPRINTIMIMOBILE"].ToString(), out formatMobile);
                    oColAtributet = new colAtributeTrupi();
                    oColKushtet = new colKusht();                  
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se konfigurim ambjenti nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se konfigurim ambjenti nga db-ja");
                }

            }
            else
            {
                ImbLogger.LogWarningShitje("Nuk u mbush konfigurim ambjenti nga DB.");
                return false;
            }
        }

        internal bool mbushKonfigurimAmbjentiShtim(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    kodKonfigAmbjente = rreshti["KODKONFIGAMBJENTE"].ToString();
                    if (rreshti.Table.Columns.Contains("PERSHKRIMKONFIGAMBJENTE_sq"))
                        pershkrimKonfigAmbjente = rreshti["PERSHKRIMKONFIGAMBJENTE_sq"].ToString();
                    else pershkrimKonfigAmbjente = rreshti["PERSHKRIMKONFIGAMBJENTE"].ToString();
                    int.TryParse(rreshti["IDKATDOK"].ToString(), out idKategori);
                    int.TryParse(rreshti["RADHA"].ToString(), out radha);
                    bool.TryParse(rreshti["DEFAULTKONFIGAMBJENTE"].ToString(), out defaultKonfigAmbjente);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDSKEMKONT"].ToString(), out idSkemeKontabel);
                    int.TryParse(rreshti["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(rreshti["IDKONFIGURIMI"].ToString(), out idKonfigurimi);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(rreshti["IDKONFIGFORMATNR"].ToString(), out idKonfigFormatNr);
                    if (rreshti["PERSHKRIMKONFIGAMBJENTE_en"] != null)
                        pershkrimKonfigAmbjenteEng = rreshti["PERSHKRIMKONFIGAMBJENTE_en"].ToString();
                    if (rreshti["PERSHKRIMKONFIGAMBJENTE_fr"] != null)
                        pershkrimKonfigAmbjente_fr = rreshti["PERSHKRIMKONFIGAMBJENTE_fr"].ToString();
                    int.TryParse(rreshti["LLOJI"].ToString(), out lloji);
                    int.TryParse(rreshti["FORMATPRINTIMIMOBILE"].ToString(), out formatMobile);
                    oColAtributet = new colAtributeTrupi();
                    oColKushtet = new colKusht();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se konfigurim ambjenti nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}