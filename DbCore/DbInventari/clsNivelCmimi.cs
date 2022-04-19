using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.DbAdmin;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nivelet e cmimeve
    ///  (Te dhenat  merren nga tabela : T_NIVELCMIMI)
    /// </summary>
    public class clsNivelCmimi
    {
        #region Atributet

        private int idNivelCmimi;
        private string kodNivelCmimi;
        private string pershkrimNivelCmimi;
        private int idPrindi;
        private int llojiNivelCmimi;
        private int idMonedha;
        private int brutoNetoNivelCmimi;
        private int prioritetiNivelCmimi;
        private int idPerdoruesi;
        //private int idNderViti;
        private string kodMonedha;
        private string lloji;
        private int idNdermarje;
        private int idKonfig;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private bool njesiTeVarura;
        private bool teVaruraNgaMonedha;
        private bool nivelCmimiBaze;
        private int idCmimRetail;
        private colLidhjetAutorizim oColLidhjetAutorizim;
        private int detajim;

        #endregion

        #region Properties
        /// <summary>
        /// Kthen/Vendos nese niveli eshte per detajim  dhe nese po, cili.
        /// </summary>
        public int Detajim
        {
            get { return detajim; }
            set { detajim = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdNivelCmimi
        {
            get { return idNivelCmimi; }
            set { idNivelCmimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos kod nivel cmimi.
        /// </summary>
        public String KodNivelCmimi
        {
            get { return kodNivelCmimi; }
            set { kodNivelCmimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrim nivel cmimi.
        /// </summary>
        public String PershkrimNivelCmimi
        {
            get { return pershkrimNivelCmimi; }
            set { pershkrimNivelCmimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e prindit.
        /// </summary>
        public int IdPrindi
        {
            get { return idPrindi; }
            set { idPrindi = value; }
        }
        /// <summary>
        /// Kthen/Vendos llojin nivel cmimi.
        /// <example> 0-cmim shitje , 1-cmim blerje</example>
        /// </summary>
        public int LlojiNivelCmimi
        {
            get { return llojiNivelCmimi; }
            set { llojiNivelCmimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e monedhes
        /// </summary>
        public int IdMonedha
        {
            get { return idMonedha; }
            set { idMonedha = value; }
        }
        /// <summary>
        /// Kthen/Vendos  bruto ose neto.
        /// </summary>
        public int BrutoNetoNivelCmimi
        {
            get { return brutoNetoNivelCmimi; }
            set { brutoNetoNivelCmimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos prioritetin e nivelit.
        /// </summary>
        public int PrioritetiNivelCmimi
        {
            get { return prioritetiNivelCmimi; }
            set { prioritetiNivelCmimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarje vitit.
        /// </summary>
        //public int IdNderViti
        //{
        //    get { return idNderViti; }
        //    set { idNderViti = value; }
        //}
        /// <summary>
        /// Kthen/Vendos kodin e monedhes.
        /// </summary>
        public String KodMonedha
        {
            get { return kodMonedha; }
            set { kodMonedha = value; }
        }
        /// <summary>
        /// Kthen/Vendos llojin.
        /// </summary>
        public string Lloji
        {
            get { return lloji; }
            set { lloji = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit.
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
        }
        public string KodBrutoNeto
        {
            get
            {
                if (brutoNetoNivelCmimi == 0)
                    return "";
                else return "TVSH";
            }
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
        public bool NjesiTeVarura
        {
            get
            {
                return njesiTeVarura;
            }
            set
            {
                njesiTeVarura = value;
            }
        }
        public bool TeVaruraNgaMonedha
        {
            get
            {
                return teVaruraNgaMonedha;
            }
            set
            {
                teVaruraNgaMonedha = value;
            }
        }
        public bool NivelCmimiBaze
        {
            get
            {
                return nivelCmimiBaze;
            }
            set
            {
                nivelCmimiBaze = value;
            }
        }
        public int IdCmimRetail
        {
            get { return idCmimRetail; }
            set { idCmimRetail = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsLidhjeAutorizim"/>
        /// </summary>
        public colLidhjetAutorizim OColLidhjetAutorizim
        {
            get { return oColLidhjetAutorizim; }
            set { oColLidhjetAutorizim = value; }
        }
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idNivelCmimi">id ritese e nivelit te cmimit</param>
        /// <param name="kodNivelCmimi">kodi i nivelit te cmimit</param>
        /// <param name="pershkrimNivelCmimi">pershkrimi i nivelit te cmimit</param>
        /// <param name="idPrindi"> id e prindit </param>
        /// <param name="llojiNivelCmimi"> lloji i nivelit te cmimit</param>
        /// <param name="idMonedha">id e monedhes</param>
        /// <param name="brutoNetoNivelCmimi"> bruto/Neto </param>
        /// <param name="prioritetiNivelCmimi"> prioriteti</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        public clsNivelCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idnderm, int idKonfig, int idstatusdok, bool njesiTeVarura, bool teVaruraNgaMonedha, bool nivelcmimbaze, int idCmimRetail , colLidhjetAutorizim colLidhjeAut)
        {
            this.idNivelCmimi = idNivelCmimi;
            this.kodNivelCmimi = kodNivelCmimi;
            this.pershkrimNivelCmimi = pershkrimNivelCmimi;
            this.idPrindi = idPrindi;
            this.llojiNivelCmimi = llojiNivelCmimi;
            this.idMonedha = idMonedha;
            this.brutoNetoNivelCmimi = brutoNetoNivelCmimi;
            this.prioritetiNivelCmimi = prioritetiNivelCmimi;
            this.idPerdoruesi = idPerdoruesi;
            //this.idNderViti = idNderViti;
            this.idNdermarje = idnderm;
            this.idKonfig = idKonfig;
            this.idStatusDok = idstatusdok;
            this.njesiTeVarura = njesiTeVarura;
            this.teVaruraNgaMonedha = teVaruraNgaMonedha;
            this.nivelCmimiBaze = nivelcmimbaze;
            this.idCmimRetail = idCmimRetail;
            this.OColLidhjetAutorizim = colLidhjeAut;
        }

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="kodNivelCmimi">kodi i nivelit te cmimit</param>
        /// <param name="pershkrimNivelCmimi">pershkrimi i nivelit te cmimit</param>
        /// <param name="idPrindi"> id e prindit </param>
        /// <param name="llojiNivelCmimi"> lloji i nivelit te cmimit</param>
        /// <param name="idMonedha">id e monedhes</param>
        /// <param name="brutoNetoNivelCmimi"> bruto/Neto </param>
        /// <param name="prioritetiNivelCmimi"> prioriteti</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        public clsNivelCmimi(string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idnderm, int idKonfig, int idstatusdok, bool njesiTeVarura, bool teVaruraNgaMonedha, bool nivelcmimbaze, int idCmimRetail,  colLidhjetAutorizim colLidhjeAut)
        {

            this.kodNivelCmimi = kodNivelCmimi;
            this.pershkrimNivelCmimi = pershkrimNivelCmimi;
            this.idPrindi = idPrindi;
            this.llojiNivelCmimi = llojiNivelCmimi;
            this.idMonedha = idMonedha;
            this.brutoNetoNivelCmimi = brutoNetoNivelCmimi;
            this.prioritetiNivelCmimi = prioritetiNivelCmimi;
            this.idPerdoruesi = idPerdoruesi;
            //this.idNderViti = idNderViti;
            this.idNdermarje = idnderm;
            this.idKonfig = idKonfig;
            this.idStatusDok = idstatusdok;
            this.njesiTeVarura = njesiTeVarura;
            this.teVaruraNgaMonedha = teVaruraNgaMonedha;
            this.nivelCmimiBaze = nivelcmimbaze;
            this.idCmimRetail = idCmimRetail;
            this.OColLidhjetAutorizim = colLidhjeAut;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNivelCmimi">id e nivelit te cmimit</param>
        public clsNivelCmimi(int idNivelCmimi)
        {
            clsDatabaseInventari dbNiveleCmimesh = new clsDatabaseInventari();
            mbushNivelCmimi(dbNiveleCmimesh.ktheNivelCmimi(idNivelCmimi));
            dbNiveleCmimesh.Dispose();
        }
        public clsNivelCmimi(int idNivelCmimi, clsDatabaseInventari dbNiveleCmimesh)
        {
            mbushNivelCmimi(dbNiveleCmimesh.ktheNivelCmimi(idNivelCmimi));
        }


        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsNivelCmimi()
        {
        }

        public clsNivelCmimi(DataRow rreshti)
        {
            
            mbushNivelCmimi(rreshti);
        }

        public clsNivelCmimi(string pershkrim, int idndermarje)
        {
            using (clsDatabaseInventari dbNiveleCmimesh = new clsDatabaseInventari())
                mbushNivelCmimi(dbNiveleCmimesh.TransCache.getNivelCmimi(pershkrim, idndermarje, dbNiveleCmimesh));
        }


        #endregion

        #region Metoda Publike
        public clsMesazh kontrollDetajimNjejteNdermarrje( int idnivelCmimi, int detajim)
        {
            if (detajim == 0)
                return new clsMesazh(true, "");
            using (clsDatabaseInventari dbNivelCmimi = new clsDatabaseInventari())
            {
                
                if (dbNivelCmimi.kontrollDetajimNjejteNdermarrje(idnivelCmimi, detajim, this.idNdermarje))
                    return new clsMesazh(true, "Njesoj");
                return new clsMesazh(false, "Nivelet e cmimit duhet te kene te njejtin detajim ne nivel ndermarrjeje");
            }

        }

        public static int merrDetajimCmimeshNdermarrje(int idNdermarje)
        {
            using (clsDatabaseInventari detajim = new clsDatabaseInventari())
            {
                return detajim.merrDetajimCmimeshNdermarrje(idNdermarje);
            }

        }
        
        public clsMesazh ruaj()
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            db.beginTransaksion();
            clsMesazh ruajtur = ruaj(db);
            if (!ruajtur.Status)
                db.rollbackTransaksion();
            else db.commitTransaksion();
            return ruajtur;

        }

        public clsMesazh ruaj(clsDatabaseInventari db)
        {

            clsMesazh ruajtur = ruajNivelCmimi(this.IdNivelCmimi, this.kodNivelCmimi, this.pershkrimNivelCmimi, this.idPrindi, this.llojiNivelCmimi, this.idMonedha, this.BrutoNetoNivelCmimi, this.prioritetiNivelCmimi, this.IdPerdoruesi, this.idNdermarje, this.idKonfig, this.idStatusDok, this.njesiTeVarura, this.teVaruraNgaMonedha, this.nivelCmimiBaze, this.oColLidhjetAutorizim, this.idCmimRetail, this.detajim, db);

            

            return ruajtur;
        }

        public clsMesazh modifiko()
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            db.beginTransaksion();
            clsMesazh ruajtur = modifiko(db);
            if (!ruajtur.Status)
                db.rollbackTransaksion();
            else db.commitTransaksion();
            return ruajtur;

        }

        public clsMesazh modifiko(clsDatabaseInventari db)
        {

            clsMesazh ruajtur = modifikoNivelCmimi(this.IdNivelCmimi, this.kodNivelCmimi, this.pershkrimNivelCmimi, this.idPrindi, this.llojiNivelCmimi, this.idMonedha, this.BrutoNetoNivelCmimi, this.prioritetiNivelCmimi, this.IdPerdoruesi, this.idNdermarje, this.idKonfig, this.idStatusDok, this.njesiTeVarura, this.teVaruraNgaMonedha, this.nivelCmimiBaze, this.oColLidhjetAutorizim,this.idCmimRetail, this.detajim, db);
            return ruajtur;
        }

        public clsMesazh ruajNivelCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idnderm, int idkonfig, int idstatusdok, bool njesiTeVarura, bool teVaruraNgaMonedha, bool nivelcmimbaze, colLidhjetAutorizim oColLidhjeAutorizimi, int idCmimRetail,int detajim, clsDatabaseInventari db)

        { //metoda per ruajtjen e nivelit te cmimeve
            clsMesazh mesazh;

            try
            {


                mesazh = db.ruajNivelCmimi(out idNivelCmimi, kodNivelCmimi, pershkrimNivelCmimi, idPrindi, llojiNivelCmimi, idMonedha, brutoNetoNivelCmimi, prioritetiNivelCmimi, idPerdoruesi, idnderm, idkonfig, idstatusdok, njesiTeVarura, teVaruraNgaMonedha, nivelcmimbaze, detajim, idCmimRetail);
                this.IdNivelCmimi = idNivelCmimi;

                if (!mesazh.Status)
                {

                    return mesazh;
                }
                DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(db );
                DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(db );
                foreach (DbAdmin.clsLidhjeAutorizim o in oColLidhjeAutorizimi)
                {
                    o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("NivelCmimi", dbKont);
                    o.IdLidhese = idNivelCmimi;
                    mesazh = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
                if (idPrindi != 0)
                {
                    colNiveleCmimesh colNivele = new colNiveleCmimesh();
                    colNivele.mbushNivelSipasPrindit(idPrindi, db);
                    clsNivelCmimi nivelPrindi = new clsNivelCmimi();
                    nivelPrindi.IdNivelCmimi = idPrindi;
                    colNivele.Add(new clsNivelCmimi(nivelPrindi.IdNivelCmimi, db));
                    if (colNivele.Count > 0)
                        if (prioritetiNivelCmimi <= colNivele[0].PrioritetiNivelCmimi)
                        {
                            foreach (clsNivelCmimi n in colNivele)
                                if (n.PrioritetiNivelCmimi >= prioritetiNivelCmimi && n.KodNivelCmimi != kodNivelCmimi)
                                {
                                    n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi + 1;

                                    mesazh = modifikoNivelCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNdermarje, idkonfig, n.idStatusDok, n.njesiTeVarura, n.TeVaruraNgaMonedha, n.nivelCmimiBaze, n.OColLidhjetAutorizim, n.IdCmimRetail,n.Detajim, db);


                                    if (!mesazh.Status)
                                    {

                                        return mesazh;
                                    }
                                }
                        }
                }

                return mesazh;
            }
            catch (Exception ce)
            {

                return new clsMesazh(false, ce.Message);
            }
        }


        /// <summary>
        /// modifikon nje nivel cmimi duke ndryshuar prioritetet e te gjithe nivele te tjera te te njejtit prind ne varesi te ndryshimit te nivelit qe u modifikua
        /// </summary>
        /// <param name="idNivelCmimi">id ritese e nivelit te cmimit</param>
        /// <param name="kodNivelCmimi">kodi i nivelit te cmimit</param>
        /// <param name="pershkrimNivelCmimi">pershkrimi i nivelit te cmimit</param>
        /// <param name="idPrindi"> id e prindit </param>
        /// <param name="llojiNivelCmimi"> lloji i nivelit te cmimit</param>
        /// <param name="idMonedha">id e monedhes</param>
        /// <param name="brutoNetoNivelCmimi"> bruto/Neto </param>
        /// <param name="prioritetiNivelCmimi"> prioriteti</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        /// 
        public clsMesazh modifikoNivelCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idnderm, int idkonfig, int idstatusdok, bool njesiTeVarura, bool teVaruraNgaMonedha, bool nivelcmimbaze, colLidhjetAutorizim oColLidhjeAutorizimi, int idCmimRetail, int detajim, clsDatabaseInventari db)
        {//metoda per modifikimin e nivelit te cmimit
            clsMesazh mesazh;
            try
            {
                clsNivelCmimi nivelipara = new clsNivelCmimi(idNivelCmimi, db);
                clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(db );
                DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(idNivelCmimi, "NivelCmimi", dbAdmin);

                mesazh = db.modifikoNivCmimi(idNivelCmimi, kodNivelCmimi, pershkrimNivelCmimi, idPrindi, llojiNivelCmimi, idMonedha, brutoNetoNivelCmimi, prioritetiNivelCmimi, idPerdoruesi, idnderm, idkonfig, idstatusdok, njesiTeVarura, teVaruraNgaMonedha, nivelcmimbaze,detajim, idCmimRetail);

                if (!mesazh.Status)
                {
                    //db.Dispose();

                    return mesazh;
                }
                DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(db );
                for (int i = 0; i < oColLidhjetAutorizim.Count; i++)
                {
                    int idAutorizimKoka = oColLidhjetAutorizim[i].IdAutorizimeKoka;
                    if (idAutorizimKoka == -1)
                        continue;
                    oColLidhjetAutorizim[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("NivelCmimi", dbKont);
                    oColLidhjetAutorizim[i].IdLidhese = idNivelCmimi;
                    clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizim.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                    if (lidhjeNjejte != null)
                    {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2) 
                        colLidhjetAutorizim.Remove(lidhjeNjejte);
                        continue;
                    }
                    mesazh = dbAdmin.ruajLidhjeAutorizim(oColLidhjetAutorizim[i].IdLidhjeAutorizim, oColLidhjetAutorizim[i].IdLidhese, oColLidhjetAutorizim[i].IdLloji, oColLidhjetAutorizim[i].IdAutorizimeKoka, 1);
                    if (!mesazh.Status)
                        return mesazh;
                }
                //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
                mesazh = colLidhjetAutorizim.FshiLidhjeAutorizim(colLidhjetAutorizim, IdPerdoruesi, dbAdmin);
                if (!mesazh.Status)
                    return mesazh;

                if (idPrindi == 0)
                {
                    colNiveleCmimesh nivelet = new colNiveleCmimesh();
                    nivelet.mbushNivelSipasPrindit(idNivelCmimi, db);
                    if (nivelet.Count > 0)
                        if (nivelet[0].LlojiNivelCmimi != llojiNivelCmimi)
                        {
                            foreach (clsNivelCmimi c in nivelet)
                            {
                                c.LlojiNivelCmimi = llojiNivelCmimi;

                                mesazh = db.modifikoNivCmimi(c.IdNivelCmimi, c.KodNivelCmimi, c.PershkrimNivelCmimi, c.IdPrindi, c.LlojiNivelCmimi, c.IdMonedha, c.BrutoNetoNivelCmimi, c.PrioritetiNivelCmimi, c.IdPerdoruesi, c.IdNdermarje, c.IdKonfig, c.idStatusDok, c.njesiTeVarura, c.teVaruraNgaMonedha, c.nivelCmimiBaze, c.detajim, c.IdCmimRetail);

                                if (!mesazh.Status)
                                {

                                    return mesazh;
                                }
                            }
                        }
                }
                if (idPrindi != 0)
                {
                    if (nivelipara.IdPrindi == idPrindi)
                    {
                        if (nivelipara.PrioritetiNivelCmimi != prioritetiNivelCmimi)
                        {
                            colNiveleCmimesh colNivele = new colNiveleCmimesh();
                            colNivele.mbushNivelSipasPrindit(idPrindi, db);
                            clsNivelCmimi nivelPrindi = new clsNivelCmimi();
                            nivelPrindi.IdNivelCmimi = idPrindi;
                            colNivele.Add(new clsNivelCmimi(nivelPrindi.IdNivelCmimi, db));
                            if (prioritetiNivelCmimi < nivelipara.PrioritetiNivelCmimi)
                            {
                                foreach (clsNivelCmimi n in colNivele)
                                    if (n.PrioritetiNivelCmimi >= prioritetiNivelCmimi && n.PrioritetiNivelCmimi < nivelipara.PrioritetiNivelCmimi && n.KodNivelCmimi != kodNivelCmimi)
                                    {
                                        n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi + 1;

                                        mesazh = db.modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNdermarje, n.IdKonfig, n.IdStatusDok, n.NjesiTeVarura, n.TeVaruraNgaMonedha, n.NivelCmimiBaze, n.Detajim, n.IdCmimRetail);



                                        if (!mesazh.Status)
                                        {

                                            return mesazh;
                                        }
                                    }
                            }
                            else
                            {
                                foreach (clsNivelCmimi n in colNivele)
                                    if (n.PrioritetiNivelCmimi <= prioritetiNivelCmimi && n.PrioritetiNivelCmimi > nivelipara.PrioritetiNivelCmimi && n.KodNivelCmimi != kodNivelCmimi)
                                    {
                                        n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi - 1;

                                        mesazh = db.modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNdermarje, n.IdKonfig, n.idStatusDok, n.njesiTeVarura, n.teVaruraNgaMonedha, n.nivelCmimiBaze, n.detajim, n.IdCmimRetail);



                                        if (!mesazh.Status)
                                        {

                                            return mesazh;
                                        }
                                    }
                            }
                        }
                    }
                    else
                    {
                        colNiveleCmimesh colNivele = new colNiveleCmimesh();
                        colNivele.mbushNivelSipasPrindit(idPrindi, db);
                        clsNivelCmimi nivelPrindi = new clsNivelCmimi();
                        nivelPrindi.IdNivelCmimi = idPrindi;
                        colNivele.Add(new clsNivelCmimi(nivelPrindi.IdNivelCmimi, db));
                        if (colNivele.Count > 0)
                            if (prioritetiNivelCmimi <= colNivele[0].PrioritetiNivelCmimi)
                            {
                                foreach (clsNivelCmimi n in colNivele)
                                    if (n.PrioritetiNivelCmimi >= prioritetiNivelCmimi && n.KodNivelCmimi != kodNivelCmimi)
                                    {
                                        n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi + 1;

                                        mesazh = db.modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNdermarje, n.IdKonfig, n.idStatusDok, n.njesiTeVarura, n.teVaruraNgaMonedha, n.nivelCmimiBaze, n.detajim, n.IdCmimRetail);



                                        if (!mesazh.Status)
                                        {

                                            return mesazh;
                                        }
                                    }
                            }
                    }
                }
                else
                {
                    if (nivelipara.PrioritetiNivelCmimi != prioritetiNivelCmimi)
                    {
                        colNiveleCmimesh colNivele = new colNiveleCmimesh();
                        colNivele.mbushNivelSipasPrindit(idNivelCmimi, db);
                        if (prioritetiNivelCmimi < nivelipara.PrioritetiNivelCmimi)
                        {
                            foreach (clsNivelCmimi n in colNivele)
                                if (n.PrioritetiNivelCmimi >= prioritetiNivelCmimi && n.PrioritetiNivelCmimi < nivelipara.PrioritetiNivelCmimi && n.KodNivelCmimi != kodNivelCmimi)
                                {
                                    n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi + 1;

                                    mesazh = db.modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNdermarje, n.IdKonfig, n.idStatusDok, n.njesiTeVarura, n.teVaruraNgaMonedha, n.nivelCmimiBaze, n.detajim, n.IdCmimRetail);

                                    if (!mesazh.Status)
                                    {
                                        return mesazh;
                                    }
                                }
                        }
                        else
                        {
                            foreach (clsNivelCmimi n in colNivele)
                                if (n.PrioritetiNivelCmimi <= prioritetiNivelCmimi && n.PrioritetiNivelCmimi > nivelipara.PrioritetiNivelCmimi && n.KodNivelCmimi != kodNivelCmimi)
                                {
                                    n.PrioritetiNivelCmimi = n.PrioritetiNivelCmimi - 1;

                                    mesazh = db.modifikoNivCmimi(n.IdNivelCmimi, n.KodNivelCmimi, n.PershkrimNivelCmimi, n.IdPrindi, n.LlojiNivelCmimi, n.IdMonedha, n.BrutoNetoNivelCmimi, n.PrioritetiNivelCmimi, n.IdPerdoruesi, n.IdNdermarje, n.IdKonfig, n.idStatusDok, n.njesiTeVarura, n.teVaruraNgaMonedha, n.nivelCmimiBaze, n.detajim, n.IdCmimRetail);
                                    if (!mesazh.Status)
                                    {
                                        return mesazh;
                                    }
                                }
                        }
                    }
                }
                
                return mesazh;
            }
            catch (Exception ce)
            {

                return new clsMesazh(false, ce.Message);
            }
        }

        ///// <summary>
        ///// Modifikon objektin nivel cmimi ne tabelen perkatese ne databaze.Therret funksionin
        ///// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoNivelCmimi"/> 
        ///// </summary>
        ///// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh modifikoNivelCmimi(int idNivelCmimi, string kodNivelCmimi, string pershkrimNivelCmimi, int idPrindi, int llojiNivelCmimi, int idMonedha, int brutoNetoNivelCmimi, int prioritetiNivelCmimi, int idPerdoruesi, int idNderViti, int idnderm)", true)]
        //public clsMesazh modifiko()
        //{
        //    clsDatabaseInventari data = new clsDatabaseInventari();
        //    clsMesazh u_modifikua = data.modifikoNivelCmimi(this.IdNivelCmimi, this.KodNivelCmimi, this.PershkrimNivelCmimi, this.IdPrindi, this.LlojiNivelCmimi, this.IdMonedha, this.BrutoNetoNivelCmimi, this.PrioritetiNivelCmimi, this.IdPerdoruesi, this.IdNderViti, this.IdNdermarje);
        //    //clsMesazh u_modifikua = data.modifikoNivelCmimi(this);
        //    return u_modifikua;
        //}

        /// <summary>
        /// Fshin objektin nivel cmimi ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiNivelCmimi"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiNivelCmimiStatus(this.IdNivelCmimi, this.idPerdoruesi);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiNivelCmimi(this);
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin nivel cmimi nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ktheNivelCmimi"/> 
        /// </summary>
        public void merr()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            data.ktheNivelCmimi(this.idNivelCmimi);
            data.Dispose();
            //data.merrNivelCmimi(this);
        }

        /// <summary>
        /// Merr datatable nivele cmimi  te nje ndermarjenga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ktheGjitheNiveleCmimeshSipasNdermarjes"/> 
        /// </summary>
        /// <returns > nje datatable me te gjithe nivelet e cmimeve te kesaj ndermarje</returns>
        public DataTable merriTeGjithe()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            DataTable nivele = data.ktheGjitheNiveleCmimeshSipasNdermarjes(this.IdNdermarje);
            data.Dispose();
            return nivele;
        }

        public static DataTable ktheNivelCmimiSipasId(int idNivelCmimi)
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            DataTable nivel = data.ktheNiveleCmimi(idNivelCmimi);
            data.Dispose();
            return nivel;
        }

        public static int merrIdMonedhaSipasNivelCmimi(int idNivelCmimi, int idNdermarrja)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            int idMonedha = dbartikuj.merrIdMonedhaSipasNivelCmimi(idNivelCmimi, idNdermarrja);
            dbartikuj.Dispose();
            return idMonedha;
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id e nivelit te cmimit sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kod">kodi i artikullit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>id e nivelit te cmimit</returns>
        public static int ktheIdNivelCmimi(string kod, int idndermarje)
        {
            clsDatabaseInventari dbNivelCmimi = new clsDatabaseInventari();
            int idNivelCmimi = (dbNivelCmimi.ktheIdNivelCmimiSipasKodit(kod, idndermarje));
            dbNivelCmimi.Dispose();
            return idNivelCmimi;
        }

        public static int ktheIdNivelCmimiNgaPershkrimi(string pershkrimi, int idndermarje)
        {
            clsDatabaseInventari dbNivelCmimi = new clsDatabaseInventari();
            int idNivelCmimi = (dbNivelCmimi.ktheIdNivelCmimiSipasPershkrimit(pershkrimi, idndermarje));
            dbNivelCmimi.Dispose();
            return idNivelCmimi;
        }

        public static bool kaNivelCmimiBaze(int idNdermarrje, int lloji)
        {
            clsDatabaseInventari dbNivelCmimi = new clsDatabaseInventari();
            bool kaNivelBaze = dbNivelCmimi.kaNivelCmimiBaze(idNdermarrje, lloji);
            dbNivelCmimi.Dispose();
            return kaNivelBaze;
        }

        public bool mbushNivelCmimiBaze(int idNdermarrje, int lloji)
        {
            clsDatabaseInventari dbNivelCmimi = new clsDatabaseInventari();
            bool sukses = mbushNivelCmimi(dbNivelCmimi.merrNivelCmimiBaze(idNdermarrje, lloji));
            dbNivelCmimi.Dispose();
            return sukses;
        }

        public static DataTable merrKodeNiveleCmimiSipasNdermarrjesDhePerdoruesit(int idNdermarrje, int idPerdorues)
        {
            using (clsDatabaseInventari dbNivelCmimi = new clsDatabaseInventari())
            {
                return dbNivelCmimi.merrKodeNiveleCmimiSipasNdermarrjesDhePerdoruesit(idNdermarrje, idPerdorues);
            }
        }

        public void mbushNivelCmimiBaze(int idNdermarrje, int lloji, clsDatabaseInventari dbNivelCmimi)
        {
            try
            {
                mbushNivelCmimi(dbNivelCmimi.TransCache.getNivelCmimiBaze(lloji, idNdermarrje, dbNivelCmimi));
            }
            catch (Exception ex)
            {
                IMBUtils.Logging.ImbLogger.Trace($"Nuk ekziston niveli i cmimit baze per llojin {lloji} ne ndermarrjen {idNdermarrje}");
            }
        }

        public bool ktheNivelCmimiSipasKodit(string kodi, int idndermarje)
        {
            clsDatabaseInventari dbNivelCmimi = new clsDatabaseInventari();
            bool sukses = ktheNivelCmimiSipasKodit(kodi, idndermarje, dbNivelCmimi);
            dbNivelCmimi.Dispose();
            return sukses;
        }

        public bool ktheNivelCmimiSipasKodit(string kodi, int idndermarje, clsDatabaseInventari dbNivelCmimi)
        {
            bool sukses = mbushNivelCmimi(dbNivelCmimi.ktheNivelCmimiSipasKodit(kodi, idndermarje));
            return sukses;
        }

        public clsMesazh kontrollotransferim(clsNivelCmimi kod, int idndermarje, clsDatabaseInventari db, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!db.ekzistonNivelCmimi(kod.kodNivelCmimi, idndermarje))
            {

                clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db );
                clsMonedha mon = new clsMonedha(kod.idMonedha, dbadm);
                mesazh = mon.kontrollotransferim(mon, idndermarje, dbadm, idperdoruesi);
                if (!mesazh.Status)
                    return mesazh;
                kod.idMonedha = mon.IdMonedha;
                if (kod.idPrindi > 0)//nqs ka prind kontrollojme prindin 
                {
                    clsNivelCmimi prindi = new clsNivelCmimi(kod.idPrindi, db);
                    mesazh = kontrollotransferim(prindi, idndermarje, db, idperdoruesi);
                    if (!mesazh.Status)
                        return mesazh;
                    kod.idPrindi = prindi.IdNivelCmimi;
                }
                DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
                DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare(db );

                konf.mbushKonfigAmbjSipasKod("NC", idndermarje, dbshare);

                kod.idKonfig = konf.IdKonfigAmbjente;
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;

            }
            else
            {
                clsNivelCmimi kodnderm = new clsNivelCmimi();
                kodnderm.ktheNivelCmimiSipasKodit(kod.kodNivelCmimi, idndermarje, db);
                kod.IdNivelCmimi = kodnderm.IdNivelCmimi;
                if (kodnderm.dtModifikimi < kod.dtModifikimi)
                {
                    clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db );
                    clsMonedha mon = new clsMonedha(kod.idMonedha, dbadm);
                    mesazh = mon.kontrollotransferim(mon, idndermarje, dbadm, idperdoruesi);
                    if (!mesazh.Status)
                        return mesazh;
                    kod.idMonedha = mon.IdMonedha;
                    if (kod.idPrindi > 0)//nqs ka prind kontrollojme prindin 
                    {
                        clsNivelCmimi prindi = new clsNivelCmimi(kod.idPrindi, db);
                        mesazh = kontrollotransferim(prindi, idndermarje, db, idperdoruesi);
                        if (!mesazh.Status)
                            return mesazh;
                        kod.idPrindi = prindi.IdNivelCmimi;
                    }
                    kod.idPerdoruesi = idperdoruesi;
                    kod.idNdermarje = idndermarje;

                    kod.idKonfig = kodnderm.idKonfig;
                    mesazh = kod.modifiko(db);
                    if (!mesazh.Status)
                        return mesazh;


                }

            }
            return mesazh;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbushja e nivelit te cmimit nga databaza
        /// </summary>
        /// <param name="dbDataRowNivelCmimi">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushNivelCmimi(DataRow dbDataRowNivelCmimi)
      {
            if (dbDataRowNivelCmimi != null)
            {
                try
                {
                    int.TryParse(dbDataRowNivelCmimi["IDNIVELCMIMI"].ToString(), out idNivelCmimi);
                    kodNivelCmimi = dbDataRowNivelCmimi["KODNIVELCMIMI"].ToString();
                    pershkrimNivelCmimi = dbDataRowNivelCmimi["PERSHKRIMNIVELCMIMI"].ToString();
                    int.TryParse(dbDataRowNivelCmimi["IDPRINDI"].ToString(), out idPrindi);
                    int.TryParse(dbDataRowNivelCmimi["LLOJINIVELCMIMI"].ToString(), out llojiNivelCmimi);
                    int.TryParse(dbDataRowNivelCmimi["IDMONEDHA"].ToString(), out idMonedha);
                    int.TryParse(dbDataRowNivelCmimi["BRUTONETONIVELCMIMI"].ToString(), out brutoNetoNivelCmimi);
                    int.TryParse(dbDataRowNivelCmimi["PRIORITETINIVELCMIMI"].ToString(), out prioritetiNivelCmimi);
                    int.TryParse(dbDataRowNivelCmimi["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    //int.TryParse(dbDataRowNivelCmimi["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowNivelCmimi["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowNivelCmimi["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowNivelCmimi["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowNivelCmimi["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowNivelCmimi["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    Boolean.TryParse(dbDataRowNivelCmimi["NJESITEVARURA"].ToString(), out njesiTeVarura);
                    Boolean.TryParse(dbDataRowNivelCmimi["TEVARURANGAMONEDHA"].ToString(), out teVaruraNgaMonedha);
                    Boolean.TryParse(dbDataRowNivelCmimi["NIVELCMIMIBAZE"].ToString(), out nivelCmimiBaze);
                    int.TryParse(dbDataRowNivelCmimi["Detajim"].ToString(), out detajim);
                    int.TryParse(dbDataRowNivelCmimi["IDCMIMRETAIL"].ToString(), out idCmimRetail);
                    KodMonedha = dbDataRowNivelCmimi.Table.Columns.Contains("MONEDHAKOD") ? dbDataRowNivelCmimi["MONEDHAKOD"].ToString() : "";


                    //clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db );
                    oColLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new MyException("ERROR: Gabim gjatë cast-it!");
                }
                catch (Exception)
                {
                    throw new MyException("ERROR: Gabim gjatë marrjes së nivelit të cmimit nga db-ja!");
                }
            }
            else
                return false;
        }

        public void mbushNivelCmimi(clsNivelCmimi niveli)
        {
            idNivelCmimi = niveli.IdNivelCmimi;
            kodNivelCmimi = niveli.KodNivelCmimi;
            pershkrimNivelCmimi = niveli.PershkrimNivelCmimi;
            idPrindi = niveli.IdPrindi;
            llojiNivelCmimi = niveli.LlojiNivelCmimi;
            idMonedha = niveli.IdMonedha;
            brutoNetoNivelCmimi = niveli.BrutoNetoNivelCmimi;
            prioritetiNivelCmimi = niveli.PrioritetiNivelCmimi;
            idPerdoruesi = niveli.IdPerdoruesi;
            idNdermarje = niveli.IdNdermarje;
            idKonfig = niveli.IdKonfig;
            idStatusDok = niveli.idStatusDok;
            dtKrijimi = niveli.DtKrijimi;
            dtModifikimi = niveli.DtModifikimi;
            njesiTeVarura = niveli.NjesiTeVarura;
            teVaruraNgaMonedha = niveli.TeVaruraNgaMonedha;
            nivelCmimiBaze = niveli.NivelCmimiBaze;
            detajim = niveli.Detajim;
            idCmimRetail = niveli.IdCmimRetail;
            KodMonedha = niveli.KodMonedha;
            oColLidhjetAutorizim = niveli.OColLidhjetAutorizim;
        }
        #endregion
    }
}