using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne monedhat.
    ///  (Te dhenat  merren nga tabela : T_MONEDHA)
    /// </summary>
    public class clsMonedha
    {
        #region Atributet

        private int idMonedha;
        private String kodiMonedha;
        private String pershkrimiMonedha;
        private bool aktivMonedha;
        private int idPerdoruesi;
        private int idLlogFitimi;
        private int idLlogHumbje;
        private int idNdermarje;
        private colKurset oColKurset;
        private string idNivelAutorizimi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colLidhjetAutorizim oColLidhjetAutorizim;
        private int idFormatNrKursi;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsMonedha(int idmonedha, String kodimonedha, String pershkrimimonedha, bool aktivmonedha, int idperdoruesi, int idllogfitimi, int idlloghumbje, int idndermarje, int idstatusdok, colLidhjetAutorizim colLidhjet, int idFormatNrKursi)
        {
            idMonedha = idmonedha;
            kodiMonedha = kodimonedha;
            pershkrimiMonedha = pershkrimimonedha;
            aktivMonedha = aktivmonedha;
            idPerdoruesi = idperdoruesi;
            idLlogFitimi = idllogfitimi;
            idLlogHumbje = idlloghumbje;
            idNdermarje = idndermarje;
            this.idStatusDok = idstatusdok;
            oColKurset = new colKurset();
            this.oColLidhjetAutorizim = colLidhjet;
            this.idFormatNrKursi = idFormatNrKursi;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsMonedha(String kodimonedha, String pershkrimimonedha, bool aktivmonedha, int idperdoruesi, int idllogfitimi, int idlloghumbje, int idnderm, int idstatusdok, colLidhjetAutorizim colLidhjet, int idFormatNrKursi)
        {
            kodiMonedha = kodimonedha;
            pershkrimiMonedha = pershkrimimonedha;
            aktivMonedha = aktivmonedha;
            idPerdoruesi = idperdoruesi;
            idLlogFitimi = idllogfitimi;
            idLlogHumbje = idlloghumbje;
            idNdermarje = idnderm;
            this.idStatusDok = idstatusdok;
            oColKurset = new colKurset();
            this.oColLidhjetAutorizim = colLidhjet;
            this.idFormatNrKursi = idFormatNrKursi;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e monedhes</param>
        public clsMonedha(int id)
        {
            if (id < 1)
                return;
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushMonedha(data.merrMonedhe(id));
            }
        }

        /// <summary>
        /// konstruktor me 2 parametra, id dhe databaseadmin
        /// </summary>
        /// <param name="id">id e monedhes</param>
        public clsMonedha(int idMonedha, clsDatabaseAdmin data)
        {            
            if (idMonedha < 1)
                return;
            mbushMonedha(data.TransCache.getMonedha(idMonedha, data));
            //mbushMonedha(data.merrMonedhe(idMonedha));
        }
        public clsMonedha(DataRow dr)
        {
            mbushMonedha(dr);
        }
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsMonedha()
        {
        }
        public clsMonedha(string kod, int IdNdermarrja, clsDatabaseAdmin data)
        {
            mbushMonedha(data.TransCache.getMonedha(kod, IdNdermarrja, data));
            //mbushMonedha(data.ktheMonedhen(kod, IdNdermarrja));
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdMonedha
        {
            get { return idMonedha; }
            set { idMonedha = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e moenedhes psh : LEK, EUR.
        /// </summary>
        public String KodiMonedha
        {
            get { return kodiMonedha; }
            set { kodiMonedha = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e moenedhes psh : Monedha Shqiptare,Monedha Europiane etj.
        /// </summary>
        public String PershkrimiMonedha
        {
            get { return pershkrimiMonedha; }
            set { pershkrimiMonedha = value; }
        }

        /// <summary>
        /// Tregon nese nje monedhe eshte aktive apo jo. Nese nuk eshte monedha nuk duhet te shfaqet ne asnje liste monedhash.
        /// </summary>
        public bool AktivMonedha
        {
            get { return aktivMonedha; }
            set { aktivMonedha = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe ka krijuar kete monedhe.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise qe do sherbeje si llogari fitimi i monedhes.Kjo nevojitet ne rastet e 
        /// diferencave nga kursi.
        /// </summary>
        public int IdLlogFitimi
        {
            get { return idLlogFitimi; }
            set { idLlogFitimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise qe do sherbeje si llogari humbje e monedhes.Kjo nevojitet ne rastet e 
        /// diferencave nga kursi.
        /// </summary>
        public int IdLlogHumbje
        {
            get { return idLlogHumbje; }
            set { idLlogHumbje = value; }
        }

        /// <summary>
        /// Kthen/Vendos niveli i autorizimit.
        /// </summary>
        public string IdNivelAutorizimi
        {
            get { return idNivelAutorizimi; }
            set { this.idNivelAutorizimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes se ciles i perket kjo monedhe.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objektet tipit <see cref="DbCore.DbAdmin.clsKurset"/>
        /// </summary>
        public colKurset OColKurset
        {
            get { return oColKurset; }
            set { oColKurset = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="clsLidhjeAutorizim"/>
        /// </summary>
        public colLidhjetAutorizim OColLidhjetAutorizim
        {
            get { return oColLidhjetAutorizim; }
            set { oColLidhjetAutorizim = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e formatit te numrit per kusin.
        /// </summary>
        public int IdFormatNrKursi
        {
            get { return idFormatNrKursi; }
            set { idFormatNrKursi = value; }
        }

        #endregion

        #region Metoda Publike

        public static string ktheMonedhenENdermarrjes(int idNdermarrje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            string kodMonedhe = data.ktheKodMonedheNdermarrje(idNdermarrje);
            data.Dispose();
            return kodMonedhe;
        }

        public static int ktheIdMonedhenENdermarrjes(int idNdermarrje)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.ktheidMonedheNdermSipasID(idNdermarrje);
            }
        }
        public static string ktheKodMonedheSipasId(int idMonedha, clsDatabaseAdmin dbadmin)
        {
                return new clsMonedha(idMonedha, dbadmin).KodiMonedha;
        }
        public static string ktheKodMonedheSipasId(int idMonedha)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.merrKodMonedhe(idMonedha);
            }
        }

        public static int ktheIdMonedheSipasKodit(string kodMonedha, int idNdermarrje)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.merrIdSipasKodMonedhe(kodMonedha, idNdermarrje);
            }
        }

        public static string ktheFormatNrMonedheSipasId(int idMonedha)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.merrFormatNrMonedhe(idMonedha);
            }
        }

        public static string kthePershkrimMonedheSipasId(int idMonedha)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.merrPershkrimMonedhe(idMonedha);
            }
        }



        public bool mbushMonedhenENdermarrjes(int idNdermarrje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushMonedha(data.ktheMonedhenNdermarrjes(idNdermarrje));
            data.Dispose();
            return sukses;
        }

        public bool mbushMonedhenEKlientit(string kodKlientFurn, int idNdermarrje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushMonedha(data.ktheMonedhenKlientit(kodKlientFurn, idNdermarrje));
            data.Dispose();
            return sukses;
        }

        public bool mbushMonedhenSipasKodArkaBanka(string kodArkaBanka, int idNdermarrje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushMonedha(data.ktheMonedhenSipasKodArkaBanka(kodArkaBanka, idNdermarrje));
            data.Dispose();
            return sukses;
        }


        public void mbushMonedhenENdermarrjes(int idNdermarrje, clsDatabaseAdmin data)
        {
            mbushMonedha(data.TransCache.getMonedheNdermarrje(idNdermarrje, data));
            //return mbushMonedha(data.ktheMonedhenNdermarrjes(idNdermarrje));            
        }
        public void merrMonedheNdermarjeNgaCacheja(int idndermarje,clsDatabaseAdmin db)
        {
            mbushMonedha(db.TransCache.merrMonedheNdermarrjeNgaCacheja(idndermarje, db));
        }
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            db.beginTransaksion();
            clsMesazh ruajtur = ruaj(db);
            if (!ruajtur.Status)
                db.rollbackTransaksion();
            else db.commitTransaksion();
            return ruajtur;
        }

        /// <summary>
        /// Ruan objektin e monedhes ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh ruaj(DbCore.DbAdmin.clsDatabaseAdmin dbAdmin)
        {

            //clsDatabaseAdmin data = new clsDatabaseAdmin();
            colMonedhat monedhatdefault = new colMonedhat();
            monedhatdefault.mbushGjitheMonedhatPozitive(-1, 0, dbAdmin);
            //colMonedhat monedhatdefault = merrGjitheMonedhatPozitive(-1,0);           
            clsMesazh mesazh;
            try
            {                
                mesazh = dbAdmin.ekzistonMonedhaPershkrim(pershkrimiMonedha, IdNdermarje);
                if (mesazh.Status)
                {
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                int idM;
                mesazh = dbAdmin.ruajMonedhe(out idM, KodiMonedha, PershkrimiMonedha, AktivMonedha, IdPerdoruesi, IdLlogFitimi, IdLlogHumbje, IdNdermarje, IdStatusDok, idFormatNrKursi);
                if (!mesazh.Status)
                {
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                IdMonedha = idM;
                foreach (clsKurset kursi in OColKurset)
                {
                    kursi.IdMonedha = IdMonedha;
                    if (!(kursi.VleraKursi == 0))
                    {
                        mesazh = kursi.krijoKurs(dbAdmin);
                        if (!mesazh.Status)
                        {
                            return new clsMesazh(false, mesazh.PershkrimMesazhi);
                        }
                    }
                }

                DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(dbAdmin );
                if (this.oColLidhjetAutorizim != null)
                {
                    foreach (DbAdmin.clsLidhjeAutorizim o in this.oColLidhjetAutorizim)
                    {
                        o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Monedha", dbKont);
                        o.IdLidhese = IdMonedha;
                        mesazh = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                }
                
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh modifiko()
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            db.beginTransaksion();
            clsMesazh ruajtur = modifiko(db);
            if (!ruajtur.Status)
                db.rollbackTransaksion();
            else db.commitTransaksion();
            return ruajtur;

        }

        /// <summary>
        /// Modifikon  objektin e monedhes ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh modifiko(DbCore.DbAdmin.clsDatabaseAdmin dbAdmin)
        {
            try
            {
                
                clsMesazh mesazh;
                clsMonedha mon = new clsMonedha();
                mon.mbushMonedhePershk(pershkrimiMonedha, idNdermarje, dbAdmin);
                if (mon.idMonedha > 0 && mon.idMonedha != idMonedha)
                {
                    return new clsMesazh("Ekziston nje monedhe me kete pershkrim! Ju lutem zgjidhni nje tjeter!");
                }
                //mesazh = dbAdmin.ekzistonMonedhaPershkrim(pershkrimiMonedha, IdNdermarje);
                //if (mesazh.Status)
                //{
                //    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                //}
                mesazh = dbAdmin.modifikoMonedhe(IdMonedha, KodiMonedha, PershkrimiMonedha, AktivMonedha, IdPerdoruesi, IdLlogFitimi, IdLlogHumbje, IdNdermarje, IdStatusDok, idFormatNrKursi);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                //colKurset kurs = merrKursetMonedhes(monedha.IdMonedha);
                //foreach (clsKurset k in kurs)
                //    fshiKurs(k);
                foreach (clsKurset kursi in OColKurset)
                {
                    kursi.IdMonedha = IdMonedha;
                    // if (o.VleraKursi > 0)
                    // {
                    //if (dbAdmin.ekzistonKurs(kursi.LlojKursi, kursi.DataKursit, kursi.VleraKursi, kursi.IdMonedha))
                    //{
                    //    //  mesazh = fshiKurs(o);
                    //    //   if (mesazh.Status)
                    //    //       mesazh = ruajKurs(o);
                    //    //  else { dbManager.Transaction.Rollback(); return mesazh; }
                    //}
                    //else
                    mesazh = kursi.krijoKurs(dbAdmin);
                    // }
                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }
                }
                DbCore.DbAdmin.colLidhjetAutorizim colLidhjet = new DbCore.DbAdmin.colLidhjetAutorizim();
                if (IdNivelAutorizimi != "")
                {
                    string[] pars1 = IdNivelAutorizimi.Split(',');
                    for (int i = 0; i < pars1.Length; i++)
                    {
                        DbCore.DbAdmin.clsLidhjeAutorizim lidhje = new DbCore.DbAdmin.clsLidhjeAutorizim();
                        lidhje.IdAutorizimeKoka = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                        //lidhje.IdAutorizimeKoka = new DbCore.DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
                        colLidhjet.Add(lidhje);
                    }
                }
                DbCore.DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbCore.DbAdmin.colLidhjetAutorizim(IdMonedha, 12, dbAdmin);
                for (int i = 0; i < colLidhjet.Count; i++)
                {
                    if (mesazh.Status)
                    {
                        int idAutorizimKoka = oColLidhjetAutorizim[i].IdAutorizimeKoka;
                        if (idAutorizimKoka == -1)
                            continue;
                        colLidhjet[i].IdLloji = 12;
                        colLidhjet[i].IdLidhese = IdMonedha;
                        DbCore.DbAdmin.clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizim.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                        if (lidhjeNjejte != null)
                        {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2) 
                            colLidhjetAutorizim.Remove(lidhjeNjejte);
                            continue;
                        }
                        mesazh = dbAdmin.ruajLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka, 1);
                    }
                    else
                        return mesazh;
                }
                //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
                for (int j = 0; j < colLidhjetAutorizim.Count; j++)
                {
                    if (mesazh.Status)
                    {
                        mesazh = dbAdmin.fshiLidhjeAutorizim(colLidhjetAutorizim[j].IdLidhjeAutorizim);
                    }
                    else
                        return mesazh;
                }
                //if (colLidhjetAutorizim.Count < colLidhjet.Count)//rasti kur jane shtuar rreshta trupi
                //{
                //    for (int i = 0; i < colLidhjet.Count; i++)
                //    {
                //        colLidhjet[i].IdLloji = 12;
                //        colLidhjet[i].IdLidhese = IdMonedha;
                //        if (i < colLidhjetAutorizim.Count)
                //        {
                //            colLidhjet[i].IdLidhjeAutorizim = colLidhjetAutorizim[i].IdLidhjeAutorizim;
                //            mesazh = dbAdmin.modifikoLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka,1);
                //        }
                //        else
                //            mesazh = dbAdmin.ruajLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka, 1);
                //        if (!mesazh.Status)
                //        {

                //            return mesazh;
                //        }
                //    }
                //}
                //else//rasti kur jane fshire rreshta
                //{
                //    int count = 0;
                //    for (int i = 0; i < colLidhjetAutorizim.Count; i++)
                //    {
                //        if (count < colLidhjet.Count)
                //        {
                //            colLidhjet[i].IdLloji = 12;
                //            colLidhjet[i].IdLidhese = IdMonedha;
                //            colLidhjet[i].IdLidhjeAutorizim = colLidhjetAutorizim[i].IdLidhjeAutorizim;
                //            mesazh = dbAdmin.modifikoLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka,1);
                //        }
                //        else
                //        {
                //            mesazh = dbAdmin.fshiLidhjeAutorizim(colLidhjetAutorizim[i].IdLidhjeAutorizim);
                //        }
                //        count++;
                //        if (!mesazh.Status)
                //        {
                //            return mesazh;
                //        }
                //    }
                //}

                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Fshin objektin e monedhes nga tabela perkatese ne databaze.
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiMonedheStatus(this.IdMonedha, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Kthen nje collection me objekte te tipit <see cref="DbCore.DbAdmin.clsMonedha"/> qe i 
        /// perkasin ndermarrjes ku jemi aktualisht te loguar.
        /// </summary>
        public colMonedhat merriTeGjithe(int idndermarje, int idperdorues)
        {
            colMonedhat data = new colMonedhat();
            data.mbushGjitheMonedhat(idndermarje, idperdorues);
            return data;

        }

        /// <summary>
        /// mbush monedhen sipas kodit dhe ndermarrjes
        /// </summary>
        /// <param name="kod">kodi i monedhes</param>
        /// <param name="IdNdermarrja">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public void mbushMonedhen(string kod, int IdNdermarrja)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                mbushMonedha(data.ktheMonedhen(kod, IdNdermarrja));
        }

        public void mbushMonedhen(string kod, int IdNdermarrja, clsDatabaseAdmin data)
        {
            mbushMonedha(data.TransCache.getMonedha(kod, IdNdermarrja, data));
        }

        public bool mbushMonedhenSipasLlogari(int idLlogari)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushMonedha(data.ktheMonedhenSipasLlogari(idLlogari));
            data.Dispose();
            return sukses;
        }

/// <summary>
/// mbush monedhen sipas pershkrimit dhe id ndermarrjes
/// </summary>
/// <param name="pershMonedha">pershkrimi i monedhes</param>
/// <param name="idNdermarrja">id e ndermarrjes</param>
/// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
public bool mbushMonedhePershk(string pershMonedha, int idNdermarrja)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushMonedha(data.ktheMonedhePershk(pershMonedha, idNdermarrja));
            data.Dispose();
            return sukses;
        }
        public bool mbushMonedhePershk(string pershMonedha, int idNdermarrja, clsDatabaseAdmin data)
        {
            bool sukses = mbushMonedha(data.ktheMonedhePershk(pershMonedha, idNdermarrja));

            return sukses;
        }

        public clsMesazh fshiMonedheAndKurse(clsMonedha monedha)
        {
            clsMesazh mesazh;
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            dbAdmin.beginTransaksion();
            try
            {
                DbCore.DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbCore.DbAdmin.colLidhjetAutorizim(monedha.IdMonedha, 12, dbAdmin);
                //DbCore.DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbCore.DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji();
                foreach (DbCore.DbAdmin.clsLidhjeAutorizim o in colLidhjeAutorizim)
                {
                    mesazh = dbAdmin.fshiLidhjeAutorizim(o.IdLidhjeAutorizim);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                }
                colKurset kurs = new colKurset();
                kurs.mbushKursetMonedhes(monedha.IdMonedha);
                //colKurset kurs = dbAdmin.merrKursetMonedhes(monedha.IdMonedha);
                foreach (clsKurset k in kurs)
                {
                    mesazh = dbAdmin.fshiKurs(k.LlojKursi, k.DataKursit, k.IdMonedha);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                }
                mesazh = dbAdmin.fshiMonedhe(monedha.IdMonedha);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                dbAdmin.commitTransaksion(); ;
                mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public static bool ekziston(string kodi, int idndermarje)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool ekziston = db.ekzistonMonedha(kodi, idndermarje);
            db.Dispose();
            return ekziston;
        }

        public static bool ekzistonPershkrim(string pershkrim, int idndermarje)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool ekziston = db.ekzistonMonedhaPershkrim(pershkrim, idndermarje).Status;
            db.Dispose();
            return ekziston;
        }

        public clsMesazh kontrollotransferim(clsMonedha kod, int idndermarje, clsDatabaseAdmin db, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!db.ekzistonMonedha(kod.kodiMonedha, idndermarje))
            {
                if (kod.idLlogFitimi > 0)
                {
                    DbKontabiliteti.clsDatabaseKontabilitet dbkont = new DbKontabiliteti.clsDatabaseKontabilitet(db );
                    DbKontabiliteti.clsLlogari llog = new DbKontabiliteti.clsLlogari(kod.idLlogFitimi, dbkont);
                    llog = new DbKontabiliteti.clsLlogari(llog.NrLlogari, idndermarje, dbkont);
                    if (llog.IdLlogari < 0)
                        return new clsMesazh(false, "Llogaria e monedhes nuk ekziston!");/// e nderpresim per shkak se mund te hyje ne cikel llogaria pret monedhen dhe monedha pret llogarine
                    kod.idLlogFitimi = llog.IdLlogari;
                }
                if (kod.idLlogHumbje > 0)
                {
                    DbKontabiliteti.clsDatabaseKontabilitet dbkont = new DbKontabiliteti.clsDatabaseKontabilitet(db );
                    DbKontabiliteti.clsLlogari llog = new DbKontabiliteti.clsLlogari(kod.idLlogHumbje, dbkont);
                    llog = new DbKontabiliteti.clsLlogari(llog.NrLlogari, idndermarje, dbkont);
                    if (llog.IdLlogari < 0)
                        return new clsMesazh(false, "Llogaria e monedhes nuk ekziston!");
                    kod.idLlogHumbje = llog.IdLlogari;
                }
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;

            }
            else
            {
                clsMonedha kodnderm = new clsMonedha();
                kodnderm.mbushMonedhen(kod.kodiMonedha, idndermarje, db);
                kod.idMonedha = kodnderm.idMonedha;
                if (kodnderm.dtModifikimi < kod.dtModifikimi)
                {
                    if (kod.idLlogFitimi > 0)
                    {
                        DbKontabiliteti.clsDatabaseKontabilitet dbkont = new DbKontabiliteti.clsDatabaseKontabilitet(db );
                        DbKontabiliteti.clsLlogari llog = new DbKontabiliteti.clsLlogari(kod.idLlogFitimi, dbkont);
                        llog = new DbKontabiliteti.clsLlogari(llog.NrLlogari, idndermarje, dbkont);
                        if (llog.IdLlogari < 0)
                            return new clsMesazh(false, "Llogaria e monedhes nuk ekziston!");/// e nderpresim per shkak se mund te hyje ne cikel llogaria pret monedhen dhe monedha pret llogarine
                        kod.idLlogFitimi = llog.IdLlogari;
                    }
                    if (kod.idLlogHumbje > 0)
                    {
                        DbKontabiliteti.clsDatabaseKontabilitet dbkont = new DbKontabiliteti.clsDatabaseKontabilitet(db );
                        DbKontabiliteti.clsLlogari llog = new DbKontabiliteti.clsLlogari(kod.idLlogHumbje, dbkont);
                        llog = new DbKontabiliteti.clsLlogari(llog.NrLlogari, idndermarje, dbkont);
                        if (llog.IdLlogari < 0)
                            return new clsMesazh(false, "Llogaria e monedhes nuk ekziston!");
                        kod.idLlogHumbje = llog.IdLlogari;
                    }
                    kod.idPerdoruesi = idperdoruesi;
                    kod.idNdermarje = idndermarje;
                    mesazh = kod.modifiko(db);
                    if (!mesazh.Status)
                        return mesazh;


                }

            }
            return mesazh;
        }

        #endregion

        #region Metoda Internal
        internal void mbushMonedha(clsMonedha mon)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushMonedha!");
            idMonedha = mon.idMonedha;
            kodiMonedha = mon.kodiMonedha;
            pershkrimiMonedha = mon.pershkrimiMonedha;
            aktivMonedha = mon.aktivMonedha;
            idPerdoruesi = mon.idPerdoruesi;
            idLlogFitimi = mon.idLlogFitimi;
            idLlogHumbje = mon.idLlogHumbje;
            idNdermarje = mon.idNdermarje;
            oColKurset = mon.oColKurset;
            idNivelAutorizimi = mon.idNivelAutorizimi;
            idStatusDok = mon.idStatusDok;
            dtKrijimi = mon.dtKrijimi;
            dtModifikimi = mon.dtModifikimi;
            oColLidhjetAutorizim = mon.oColLidhjetAutorizim;
            idFormatNrKursi = mon.idFormatNrKursi;
            ImbLogger.LogTraceShitje("Mbaroi metoda mbushMonedha!");
        }
        internal bool mbushMonedha(DataRow dbDataRowMonedha)
        {
            if (dbDataRowMonedha != null)
            {
                try
                {
                    int.TryParse(dbDataRowMonedha["IDMONEDHA"].ToString(), out idMonedha);
                    kodiMonedha = dbDataRowMonedha["MONEDHAKOD"].ToString();
                    pershkrimiMonedha = dbDataRowMonedha["MONEDHAPERSHK"].ToString();
                    bool.TryParse(dbDataRowMonedha["MONEDHAAKTIV"].ToString(), out aktivMonedha);
                    int.TryParse(dbDataRowMonedha["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowMonedha["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowMonedha["IDLLOGFITIMI"].ToString(), out idLlogFitimi);
                    int.TryParse(dbDataRowMonedha["IDLLOGHUMBJE"].ToString(), out idLlogHumbje);
                    int.TryParse(dbDataRowMonedha["IDFORMATNRKURSI"].ToString(), out idFormatNrKursi);
                    DateTime.TryParse(dbDataRowMonedha["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowMonedha["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    oColKurset = new colKurset();
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se monedhave nga databaza");
                    throw new Exception("ERROR: Gabim gjate marrjes se monedhave nga databaza");
                }
                catch (Exception)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se monedhave nga databaza");
                    throw new MyException("ERROR: Gabim gjate marrjes se monedhave nga databaza!");
                }

            }
            else
            {
                ImbLogger.LogTraceShitje("Nuk u mbush monedha.");
                return false;
            }
        }

        #endregion
    }
}