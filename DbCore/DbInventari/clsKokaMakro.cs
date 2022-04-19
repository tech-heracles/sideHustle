using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne koken e makros
    ///  (Te dhenat  merren nga tabela : T_KOKAMAKRO)
    /// </summary>
    public class clsKokaMakro
    {

        #region Atributet

        private int idKokaMakro;
        private string kodiKokaMakro;
        private string pershkrimiKokaMakro;
        private string idNivelAutorizimi;
        //private int idNderViti;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colTrupatMakro oColTrupatMakro;
        private DataRow rreshti;

        #endregion

        #region Kontruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idKokaMakro">id ritese e kokes se makros</param>
        /// <param name="kodKokaMakro"> kodi i makros</param>
        /// <param name="pershkrimKokaMakro"> pershkrimi i makros</param>
        /// <param name="idNderViti">id e ndermarje vitit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        public clsKokaMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idPerdoruesi, int idNdermarje, int idstatusdok)
        {
            this.idKokaMakro = idKokaMakro;
            this.kodiKokaMakro = kodKokaMakro;
            this.pershkrimiKokaMakro = pershkrimKokaMakro;
            //this.idNderViti = idNderViti;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idstatusdok;
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes se makros</param>
        public clsKokaMakro(int id)
        {
            clsDatabaseInventari dbKokaMakro = new clsDatabaseInventari();
            mbushKokaMakro(dbKokaMakro.merrKokaMakro(id));
            dbKokaMakro.Dispose();
        }
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKokaMakro()
        {
        }

        public clsKokaMakro(string kodi, int idndermarje)
        {
            clsDatabaseInventari dbKokaMakro = new clsDatabaseInventari();
            mbushKokaMakro(dbKokaMakro.merrKokaMakroSipasKodit(kodi, idndermarje));
            dbKokaMakro.Dispose();
        }

        public clsKokaMakro(DataRow rreshti)
        {
            
            mbushKokaMakro(rreshti);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKokaMakro
        {
            get
            {
                return idKokaMakro;
            }
            set
            {
                idKokaMakro = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos kodin e makros.
        /// </summary>
        public string KodiKokaMakro
        {
            get
            {
                return kodiKokaMakro;
            }
            set
            {
                kodiKokaMakro = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e makros.
        /// </summary>
        public string PershkrimiKokaMakro
        {
            get
            {
                return pershkrimiKokaMakro;
            }
            set
            {
                pershkrimiKokaMakro = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nivelet e autorizimit.
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
        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarje vitit.
        /// </summary>
        //public int IdNderViti
        //{
        //    get { return idNderViti; }
        //    set { idNderViti = value; }
        //}
        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos  nje koleksion me trupin e makros.
        /// </summary>
        public colTrupatMakro OColTrupatMakro
        {
            get
            {
                return oColTrupatMakro;
            }
            set
            {
                oColTrupatMakro = value;
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

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan nje objekt makro sebashku me trupin dhe autorizimet
        /// Nje objekt makro ka nje koleksion me trupat dhe autorizimet, 
        /// ruajtja e nje makro imponon ruajtjen edhe te nje colection-i me trupat dhe autorizimet
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe makro bashke me trupat dhe autorizimet konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje makro sebashku me trupat dhe autorizimet
        /// </summary>
        /// <param name="idKokaMakro">id ritese e kokes se makros</param>
        /// <param name="kodKokaMakro"> kodi i makros</param>
        /// <param name="pershkrimKokaMakro"> pershkrimi i makros</param>
        /// <param name="idNderViti">id e ndermarje vitit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>     
        public clsMesazh ruajMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idPerdoruesi, int idNdermarje, colTrupatMakro oColTrupatMakro, int idstatusdok)
        {//ruan makro
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet db = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();

            clsDatabaseInventari dbInv = new clsDatabaseInventari();



            //DbCore.DbKontabiliteti.colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("Makro");
            clsKokaMakro makro = new clsKokaMakro(idKokaMakro, kodKokaMakro, pershkrimKokaMakro, idPerdoruesi, idNdermarje, idstatusdok);
            clsMesazh mesazh = new clsMesazh();
            try
            {
                dbInv.beginTransaksion();
                idKokaMakro = dbInv.ruajKokaMakro(idKokaMakro, kodKokaMakro, pershkrimKokaMakro, idPerdoruesi, idNdermarje, idstatusdok);
                if (idKokaMakro == 0)
                {
                    dbInv.rollbackTransaksion();
                    return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtes se makros");
                }

                if (makro.IdNivelAutorizimi != "")
                {
                    DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
                    string[] pars1 = makro.IdNivelAutorizimi.Split(',');
                    for (int i = 0; i < pars1.Length; i++)
                    {
                        DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
                        lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                        //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
                        colLidhjet.Add(lidhje);
                    }
                    DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbInv);
                    DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(dbInv );
                    foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjet)
                    {
                        o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro", dbKont);
                        o.IdLidhese = idKokaMakro;
                        mesazh = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                        if (!mesazh.Status)
                        {
                            dbInv.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                }
                foreach (clsTrupiMakro o in oColTrupatMakro)
                {
                    o.IdKokaMakro = idKokaMakro;
                    int idT;
                    mesazh = dbInv.ruajTrupiMakro(out idT, o.IdKokaMakro, o.IdLlojMakro, o.IdProdukti, o.Pershkrimi, o.IdFunksionMakro, o.Vlera, o.Renditja);
                    if (!mesazh.Status)
                    {
                        dbInv.rollbackTransaksion();
                        return mesazh;
                    }
                }
                dbInv.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                dbInv.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }
      
        /// <summary>
        /// Modifikon nje objekt makro sebashku me trupin dhe autorizimet
        /// Nje objekt makro ka nje koleksion me trupat dhe autorizimet, 
        /// modifikimi e nje makro imponon modifikimin edhe te nje colection-i me trupat dhe autorizimet
        /// Mqs cdo rresht i ri qe modifikohet ne DB kerkon thirrjen e nje SP-je me parametra dhe makro bashke me trupat dhe autorizimet konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje makro sebashku me trupat dhe autorizimet
        /// </summary>
        /// <param name="idKokaMakro">id ritese e kokes se makros</param>
        /// <param name="kodKokaMakro"> kodi i makros</param>
        /// <param name="pershkrimKokaMakro"> pershkrimi i makros</param>
        /// <param name="idNderViti">id e ndermarje vitit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit te te dhenave ne DB</returns>     
        public clsMesazh modifikoMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idPerdoruesi, int idNdermarje, colTrupatMakro oColTrupatMakro, int idstatusdok)
        {
            //modifikon Makro
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet db = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();

            clsDatabaseInventari dbInv = new clsDatabaseInventari();
            try
            {
                dbInv.beginTransaksion();

                //DbCore.DbKontabiliteti.colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("Makro");
                //DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idKokaMakro, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro"));
                DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(idKokaMakro, "Makro");
                colTrupatMakro trupat = new colTrupatMakro(idKokaMakro);
                clsKokaMakro makro = new clsKokaMakro(idKokaMakro, kodKokaMakro, pershkrimKokaMakro, idPerdoruesi, idNdermarje, idstatusdok);
                //colTrupatMakro trupat = merrTrupatMakroSipasKokes(makro.IdKokaMakro);
                clsMesazh mesazh = new clsMesazh();
                DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);

                mesazh = dbInv.modifikoKokaMakro(idKokaMakro, kodKokaMakro, pershkrimKokaMakro, idPerdoruesi, idNdermarje, idstatusdok);
                if (mesazh.Status)
                {
                    DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
                    if (makro.IdNivelAutorizimi != "")
                    {

                        string[] pars1 = makro.IdNivelAutorizimi.Split(',');
                        for (int i = 0; i < pars1.Length; i++)
                        {
                            DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
                            lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                            //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
                            colLidhjet.Add(lidhje);
                        }

                    }
                    DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbInv );
                    DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(dbInv );
                    for (int i = 0; i < colLidhjet.Count; i++)
                    {
                        if (mesazh.Status)
                        {
                            int idAutorizimKoka = colLidhjet[i].IdAutorizimeKoka;
                            if (idAutorizimKoka == -1)
                                continue;
                            colLidhjet[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro", dbKont);
                            colLidhjet[i].IdLidhese = idKokaMakro;
                            DbCore.DbAdmin.clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizim.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                            if (lidhjeNjejte != null)
                            {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2) 
                                colLidhjetAutorizim.Remove(lidhjeNjejte);
                                continue;
                            }
                            mesazhAdmin = dbAdmin.ruajLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka, 1);
                        }
                        else
                        {
                            dbInv.rollbackTransaksion();
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                            return mesazh;
                        }
                    }
                    //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
                    for (int j = 0; j < colLidhjetAutorizim.Count; j++)
                    {
                        if (mesazh.Status)
                        {
                            mesazhAdmin = dbAdmin.fshiLidhjeAutorizim(colLidhjetAutorizim[j].IdLidhjeAutorizim);
                        }
                        else
                        {
                            dbInv.rollbackTransaksion();
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                            return mesazh;
                        }
                    }
                    
                    if (mesazhAdmin.Status)
                    {
                        if (trupat.Count < oColTrupatMakro.Count)//rasti kur jane shtuar rreshta trupi
                        {
                            for (int i = 0; i < oColTrupatMakro.Count; i++)
                            {
                                if (mesazh.Status)
                                {
                                    oColTrupatMakro[i].IdKokaMakro = idKokaMakro;
                                    if (i < trupat.Count)
                                    {

                                        oColTrupatMakro[i].IdTrupiMakro = trupat[i].IdTrupiMakro;
                                        mesazh = dbInv.modifikoTrupiMakro(oColTrupatMakro[i].IdTrupiMakro, oColTrupatMakro[i].IdKokaMakro, oColTrupatMakro[i].IdLlojMakro, oColTrupatMakro[i].IdProdukti, oColTrupatMakro[i].Pershkrimi, oColTrupatMakro[i].IdFunksionMakro, oColTrupatMakro[i].Vlera, oColTrupatMakro[i].Renditja);
                                        //mesazh=  modifikoTrupiMakro(makro.OColTrupatMakro[i]);
                                    }
                                    else
                                    {
                                        int idoC;
                                        mesazh = dbInv.ruajTrupiMakro(out idoC, oColTrupatMakro[i].IdKokaMakro, oColTrupatMakro[i].IdLlojMakro, oColTrupatMakro[i].IdProdukti, oColTrupatMakro[i].Pershkrimi, oColTrupatMakro[i].IdFunksionMakro, oColTrupatMakro[i].Vlera, oColTrupatMakro[i].Renditja);
                                        //mesazh=  ruajTrupiMakro(makro.OColTrupatMakro[i]);
                                    }
                                }
                                else
                                {
                                    dbInv.rollbackTransaksion();

                                    return mesazh;
                                }
                            }
                        }
                        else//rasti kur jane fshire rreshta
                        {
                            int count = 0;
                            for (int i = 0; i < trupat.Count; i++)
                            {
                                if (mesazh.Status)
                                {
                                    if (count < oColTrupatMakro.Count)
                                    {

                                        oColTrupatMakro[i].IdKokaMakro = idKokaMakro;

                                        oColTrupatMakro[i].IdTrupiMakro = trupat[i].IdTrupiMakro;
                                        mesazh = dbInv.modifikoTrupiMakro(oColTrupatMakro[i].IdTrupiMakro, oColTrupatMakro[i].IdKokaMakro, oColTrupatMakro[i].IdLlojMakro, oColTrupatMakro[i].IdProdukti, oColTrupatMakro[i].Pershkrimi, oColTrupatMakro[i].IdFunksionMakro, oColTrupatMakro[i].Vlera, oColTrupatMakro[i].Renditja);
                                        //mesazh=  modifikoTrupiMakro(makro.OColTrupatMakro[i]);
                                    }
                                    else
                                    {
                                        mesazh = dbInv.fshiTrupiMakro(trupat[i].IdTrupiMakro);
                                        //mesazh=  fshiTrupiMakro(trupat[i]);
                                    }
                                    count++;
                                }
                                else
                                {
                                    dbInv.rollbackTransaksion();

                                    return mesazh;
                                }
                            }
                        }

                        if (mesazh.Status)
                        {
                            dbInv.commitTransaksion();

                            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                            return mesazh;
                        }
                        else
                        {
                            dbInv.rollbackTransaksion();

                            return mesazh;
                        }
                    }
                    else
                    {
                        dbInv.rollbackTransaksion();

                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                        return mesazh;
                    }
                }
                else
                {
                    dbInv.rollbackTransaksion();

                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbInv.rollbackTransaksion();

                return new clsMesazh(false, ce.Message);
            }
        }
        /// <summary>
        /// Modifikon objektin Makron ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoMakro"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh modifikoMakro(int idKokaMakro, string kodKokaMakro, string pershkrimKokaMakro, int idNderViti, int idPerdoruesi, int idNdermarje)", true)]
        //public clsMesazh modifiko()
        //{
        //    clsDatabaseInventari data = new clsDatabaseInventari();
        //    clsMesazh u_modifikua = data.modifikoMakro(this.IdKokaMakro, this.KodiKokaMakro, this.PershkrimiKokaMakro, this.IdNderViti, this.IdPerdoruesi, this.IdNdermarje);
        //    //clsMesazh u_modifikua = data.modifikoMakro(this);
        //    return u_modifikua;
        //}

        /// <summary>
        /// fshin nje objekt makro sebashku me trupin dhe autorizimet
        /// Nje objekt makro ka nje koleksion me trupin  dhe autorizimet, 
        /// fshirja e nje makro imponon fshirjen edhe te nje colection-i me trupin dhe autorizimet
        /// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe makro bashke me trupin dhe autorizimet konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon fshirjen e rregullt te nje makro sebashku me trupin dhe autorizimet
        /// </summary>
        /// <param name="idKokaMakro">id ritese e kokes se makros</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public clsMesazh fshiMakro(int idKokaMakro)
        {//fshin makro
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet db = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            clsDatabaseInventari dbInv = new clsDatabaseInventari();
            try
            {
                dbInv.beginTransaksion();
                //DbCore.DbKontabiliteti.colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("Makro");
                DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.colLidhjetAutorizim(idKokaMakro, "Makro");
                //DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idKokaMakro, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro"));
                colTrupatMakro trupat = new colTrupatMakro(idKokaMakro);
                //colTrupatMakro  trupat =merrTrupatMakroSipasKokes (makro.IdKokaMakro);
                clsMesazh mesazh = new clsMesazh(true);

                foreach (clsTrupiMakro o in trupat)
                {
                    if (mesazh.Status)
                    {
                        mesazh = dbInv.fshiTrupiMakro(o.IdTrupiMakro);
                        //mesazh = fshiTrupiMakro(o);
                    }
                    else
                    {
                        dbInv.rollbackTransaksion();

                        return mesazh;
                    }
                }
                if (mesazh.Status)
                {
                    DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbInv );
                    foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjeAutorizim)
                    {
                        if (mesazh.Status)
                            mesazh = dbAdmin.fshiLidhjeAutorizim(o.IdLidhjeAutorizim);

                        else
                        {
                            dbInv.rollbackTransaksion();

                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = mesazh.PershkrimMesazhi;
                            return mesazh;
                        }
                    }
                    if (mesazh.Status)
                    {
                        mesazh = dbInv.fshiKokaMakro(idKokaMakro);
                        if (mesazh.Status)
                        {
                            dbInv.commitTransaksion();

                            mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                            return mesazh;
                        }
                        else
                        {
                            dbInv.rollbackTransaksion();

                            return mesazh;
                        }
                    }
                    else
                    {
                        dbInv.rollbackTransaksion();
                        return mesazh;
                    }
                }
                else
                {
                    dbInv.rollbackTransaksion();
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbInv.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }
        /// <summary>
        /// Fshin objektin Makron ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiMakro"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        //[Obsolete("Perdor: public clsMesazh fshiMakro(int idKokaMakro)", true)]
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiKokaMakroStatus(this.IdKokaMakro, this.idPerdoruesi);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiMakro(this);
            return u_fshi;
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id e kokes makro sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kod">kodi i artikullit</param>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>id e kokes makro</returns>
        public static int ktheIdKokaMakro(string kod, int idndermarje)
        {
            clsDatabaseInventari dbKokaMakro = new clsDatabaseInventari();
            int idKoka = (dbKokaMakro.merrKokaMakro(kod, idndermarje));
            dbKokaMakro.Dispose();
            return idKoka;
        }



        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbushja e kokave te makros nga databaza
        /// </summary>
        /// <param name="dbDataRowKokaMakro">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushKokaMakro(DataRow dbDataRowKokaMakro)
        {
            if (dbDataRowKokaMakro != null)
            {

                try
                {
                    int.TryParse(dbDataRowKokaMakro["IDKOKAMAKRO"].ToString(), out idKokaMakro);
                    kodiKokaMakro = dbDataRowKokaMakro["KODIKOKAMAKRO"].ToString();
                    pershkrimiKokaMakro = dbDataRowKokaMakro["PERSHKRIMIKOKAMAKRO"].ToString();

                    //DbCore.DbKontabiliteti.colLlojeBuxhetesh colLloj = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet().merrLlojBuxhetiSipasKodit("Makro");
                    //DbAdmin.colLidhjetAutorizim lidhje = new DbAdmin.colLidhjetAutorizim(idKokaMakro, "Makro");
                    ////DbAdmin.colLidhjetAutorizim lidhje = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idKokaMakro, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Makro"));
                    //if (lidhje.Count != 0)
                    //{
                    //    idNivelAutorizimi = DbAdmin.clsAutorizimKoka.ktheKodAutorizim(lidhje[0].IdAutorizimeKoka);
                    //    //new DbAdmin.clsDatabaseAdmin().ktheAutorizim(lidhje[0].IdAutorizimeKoka)[0].KodiAutorizim;
                    //    for (int i = 1; i < lidhje.Count; i++)
                    //        idNivelAutorizimi += "," + DbAdmin.clsAutorizimKoka.ktheKodAutorizim(lidhje[i].IdAutorizimeKoka);
                    //        //idNivelAutorizimi += "," + new DbAdmin.clsDatabaseAdmin().ktheAutorizim(lidhje[i].IdAutorizimeKoka)[0].KodiAutorizim;
                    //}
                    //else
                    //    idNivelAutorizimi = "";

                    int.TryParse(dbDataRowKokaMakro["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    //int.TryParse(dbDataRowKokaMakro["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowKokaMakro["IDNDERMARJE"].ToString(), out idNdermarje);
                    DateTime.TryParse(dbDataRowKokaMakro["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKokaMakro["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokave te makro-s nga db-ja");
                }
            }
            else
                return false;

        }

        #endregion
    }
}
