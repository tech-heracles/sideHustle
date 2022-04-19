using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje KPF(Kod i pasqyres financiare) Llogarite standarte
    /// KPF-te jane grupime te disa llogarive nen nje kod te caktuar. Ky grupim sherben per konfigurimin e pasqyrave financiare.
    ///  (Te dhenat  merren nga tabela : T_KPF)
    /// </remarks>
    public class clsKPF
    {
        #region Atribute

        private int niveli1;//per shfaqjen ne forme peme
        private int niveli2;//per shfaqjen ne forme peme
        private int niveli3;//per shfaqjen ne forme peme
        private int niveli4;//per shfaqjen ne forme peme
        private int niveli5;//per shfaqjen ne forme peme
        private int niveli6;
        private int niveli7;
        private double gjendja;
        private int idKPF;
        private String kodiKPF;
        private string emertimiKPF;
        private int niveliKPF;
        private string idAutorizimi;
        private Boolean inaktiv;
        private int idKonfig;
        private string shenimeKPF;
        private int grupiKPF;
        private int idNdermarje;
        //private int idNderViti;
        private int idPerdoruesi;
        private string urlImage1;//per shfaqjen ne forme peme
        private string urlImage2;//per shfaqjen ne forme peme
        private string urlImage3;//per shfaqjen ne forme peme
        private string urlImage4;//per shfaqjen ne forme peme
        private string urlImage5;//per shfaqjen ne forme peme
        private string urlImage6;
        private string urlImage7;
        private string prind;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string emertimiKPF_fr;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsKPF(int idkpf, string kodikpf, int nivelikpf, string emertimikpf,
                        bool inakt, string shenimekpf,
                        int grupikpf, int idndermarje, int idperdoruesi, string urlimage1,
                        string urlimage2, string urlimage3, string urlimage4, string urlimage5, string urlimage6, string urlimage7,
           int idkonfig, string prind, int idstatusdok)
        {
            idKPF = idkpf;
            kodiKPF = kodikpf;
            niveliKPF = nivelikpf;
            emertimiKPF = emertimikpf;

            inaktiv = inakt;

            shenimeKPF = shenimekpf;
            grupiKPF = grupikpf;
            idNdermarje = idndermarje;
            //idNderViti = idndervit;
            idPerdoruesi = idperdoruesi;
            urlImage1 = urlimage1;
            urlImage2 = urlimage2;
            urlImage3 = urlimage3;
            urlImage4 = urlimage4;
            urlImage5 = urlimage5;
            urlImage6 = urlimage6;
            urlImage7 = urlimage7;
            this.idKonfig = idkonfig;
            this.prind = prind;
            idStatusDok = idstatusdok;

        }

        /// <summary>
        /// Kontruktor i klases
        /// </summary>
        public clsKPF(string kodikpf, int nivelikpf, string emertimikpf,
                    bool inakt, string shenimekpf, int grupikpf,
                    int idndermarje, int idperdoruesi, string urlimage1,
                    string urlimage2, string urlimage3, string urlimage4, string urlimage5, string urlimage6, string urlimage7, int idkonfig, string prind, int idstatusdok)
        {

            kodiKPF = kodikpf;
            niveliKPF = nivelikpf;
            emertimiKPF = emertimikpf;

            inaktiv = inakt;
            idStatusDok = idstatusdok;
            shenimeKPF = shenimekpf;
            grupiKPF = grupikpf;
            idNdermarje = idndermarje;
            //idNderViti = idndervit;
            idPerdoruesi = idperdoruesi;
            urlImage1 = urlimage1;
            urlImage2 = urlimage2;
            urlImage3 = urlimage3;
            urlImage4 = urlimage4;
            urlImage5 = urlimage5;
            urlImage6 = urlimage6;
            urlImage7 = urlimage7;
            this.idKonfig = idkonfig;
            this.prind = prind;
        }

        /// <summary>
        /// konstruktor me 4 parametra
        /// </summary>
        /// <param name="grupiKpf">grupi i kpf-se</param>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="kodi">kodi i kpf-se</param>
        public clsKPF(int grupiKpf, int idNdermarje, int idperdoruesi, string kodi)
        {
            clsDatabaseKontabilitet dbKPF = new clsDatabaseKontabilitet();
            mbushKPF(dbKPF.ktheGjitheKPFteSipasGrupitPozitiveAndAutorizimeLike(grupiKpf, idNdermarje, idperdoruesi, kodi));
            dbKPF.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kpf-se</param>
        public clsKPF(int id)
        {
            clsDatabaseKontabilitet dbKPF = new clsDatabaseKontabilitet();
            mbushKPF(dbKPF.merrKPF(id));
            dbKPF.Dispose();
        }
        public clsKPF(int id, clsDatabaseKontabilitet dbKPF)
        {

            mbushKPF(dbKPF.merrKPF(id));

        }
        public clsKPF(string kodi, int idnderm, int grupi, clsDatabaseKontabilitet dbKPF)
        {

            mbushKPF(dbKPF.merrKPFSipasKodit(kodi, idnderm, grupi));

        }
        /// <summary>
        /// konstruktor i klases
        /// </summary>
        public clsKPF()
        {
        }

        public clsKPF(DataRow rreshti)
        {
            
            mbushKPF(rreshti);
        }

        #endregion

        #region Properties

        public int Niveli1
        {
            get
            {
                return niveli1;
            }
            set
            {
                niveli1 = value;
            }
        }

        public int Niveli2
        {
            get
            {
                return niveli2;
            }
            set
            {
                niveli2 = value;
            }
        }

        public int Niveli3
        {
            get
            {
                return niveli3;
            }
            set
            {
                niveli3 = value;
            }
        }

        public int Niveli4
        {
            get
            {
                return niveli4;
            }
            set
            {
                niveli4 = value;
            }
        }

        public int Niveli5
        {
            get
            {
                return niveli5;
            }
            set
            {
                niveli5 = value;
            }
        }
        public int Niveli6
        {
            get
            {
                return niveli6;
            }
            set
            {
                niveli6 = value;
            }
        }
        public int Niveli7
        {
            get
            {
                return niveli7;
            }
            set
            {
                niveli7 = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdKPF
        {
            get { return idKPF; }
            set { idKPF = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin
        /// </summary>
        public string KodiKPF
        {
            get { return kodiKPF; }
            set { kodiKPF = value; }
        }

        /// <summary>
        /// Kthen/Vendos emertimin
        /// </summary>
        public string EmertimiKPF
        {
            get
            {
                return emertimiKPF;
            }
            set
            {
                emertimiKPF = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nivelin
        /// </summary>
        public int NiveliKPF
        {
            get { return niveliKPF; }
            set { niveliKPF = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e autorizimit
        /// </summary>
        public string IdAutorizimi
        {
            get { return idAutorizimi; }
            set { idAutorizimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos faktin nese KPF-ja eshte aktive apo jo
        /// </summary>
        public Boolean Inaktiv
        {
            get { return inaktiv; }
            set { inaktiv = value; }
        }

        /// <summary>
        /// Kthen/Vendos shenimet
        /// </summary>
        public string ShenimeKPF
        {
            get
            {
                return shenimeKPF;
            }
            set
            {
                shenimeKPF = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos grupin e KPF
        /// </summary>
        public int GrupiKPF
        {
            get
            {
                return grupiKPF;
            }
            set
            {
                grupiKPF = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes
        /// </summary>
        public int IdNdermarje
        {
            get
            {
                return idNdermarje;
            }
            set
            {
                idNdermarje = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos gjendjen
        /// </summary>
        public double Gjendja
        {
            get
            {
                return gjendja;
            }
            set
            {
                gjendja = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe po kryen veprimin
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
        }

        public string UrlImage1
        {
            get
            {
                return urlImage1;
            }
            set
            {
                urlImage1 = value;
            }
        }

        public string UrlImage2
        {
            get
            {
                return urlImage2;
            }
            set
            {
                urlImage2 = value;
            }
        }

        public string UrlImage3
        {
            get
            {
                return urlImage3;
            }
            set
            {
                urlImage3 = value;
            }
        }

        public string UrlImage4
        {
            get
            {
                return urlImage4;
            }
            set
            {
                urlImage4 = value;
            }
        }

        public string UrlImage5
        {
            get
            {
                return urlImage5;
            }
            set
            {
                urlImage5 = value;
            }
        }
        public string UrlImage6
        {
            get
            {
                return urlImage6;
            }
            set
            {
                urlImage6 = value;
            }
        }
        public string UrlImage7
        {
            get
            {
                return urlImage7;
            }
            set
            {
                urlImage7 = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos prindin
        /// </summary>
        public string Prind
        {
            get { return prind; }
            set { prind = value; }
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
        public string EmertimiKPF_fr
        {
            get { return emertimiKPF_fr; }
            set { emertimiKPF_fr = value; }
        }

        #endregion

        #region Metoda Publike

        public clsMesazh ruaj()
        {
            clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
            db.beginTransaksion();
            clsMesazh ruajtur = ruaj(db);
            if (!ruajtur.Status)
                db.rollbackTransaksion();
            else db.commitTransaksion();
            return ruajtur;

        }
        public clsMesazh ruaj(clsDatabaseKontabilitet db)
        {
            clsMesazh ruajtur = ruajKPFAndBuxhete(this.idKPF, this.kodiKPF, this.niveliKPF, this.emertimiKPF, this.inaktiv, this.shenimeKPF, this.grupiKPF, this.idNdermarje, this.idPerdoruesi, this.idKonfig, this.idNdermarje, this.idAutorizimi, this.idStatusDok, db, this.emertimiKPF_fr);
            return ruajtur;
        }
        public clsMesazh modifiko()
        {
            clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
            db.beginTransaksion();
            clsMesazh ruajtur = modifiko(db);
            if (!ruajtur.Status)
                db.rollbackTransaksion();
            else db.commitTransaksion();
            return ruajtur;

        }
        public clsMesazh modifiko(clsDatabaseKontabilitet db)
        {
            clsMesazh ruajtur = modifikoKPFAndBuxhete(this.idKPF, this.kodiKPF, this.niveliKPF, this.emertimiKPF, this.inaktiv, this.shenimeKPF, this.grupiKPF, this.idNdermarje, this.idPerdoruesi, this.idKonfig, this.idStatusDok, db, this.emertimiKPF_fr);
            return ruajtur;
        }
        /// <summary>
        /// Ruan KPF dhe buxhetet ne tabelat perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajKPFAndBuxhete"/>
        /// </summary>
        /// <param name="idkpf">id e kpf-se</param>
        /// <param name="kodikpf">kodi i kpf-se</param>
        /// <param name="nivelikpf">niveli i kpf-se</param>
        /// <param name="emertimikpf">emertimi i kpf-se</param>
        /// <param name="inakt">aktive ose inaktive</param>
        /// <param name="shenimekpf">shenime</param>
        /// <param name="grupikpf">grupi i kpfse</param>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <param name="idndervit">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idkonfig">id konfigurimit</param>
        /// <param name="idndermarje">Id e ndermarrjes</param>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruajKPFAndBuxhete(int idkpf, string kodikpf, int nivelikpf, string emertimikpf, bool inakt, string shenimekpf, int grupikpf, int idnder,
            int idperdoruesi, int idkonfig, int idndermarje, string idAutorizimi, int idstatusdok, clsDatabaseKontabilitet dbKont, string emertimikpf_fr)
        {//ruan KPFne dhe buxhetet perkatese
            //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("KPF");
            clsMesazh mesazh = new clsMesazh();
            DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);

            try
            {
                mesazh = dbKont.ruajKPF(out idkpf, kodikpf, nivelikpf, emertimikpf, inakt, shenimekpf, grupikpf, idnder, idperdoruesi, idkonfig, idstatusdok, emertimikpf_fr);
                this.idKPF = idkpf;
                //clsKPF KPF = new clsKPF(idkpf);
                if (mesazh.Status)
                {
                    DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbKont );
                    
                    if (mesazhAdmin.Status)
                    {
                        if (idAutorizimi != "")
                        {
                            DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
                            string[] pars1 = idAutorizimi.Split(',');
                            for (int i = 0; i < pars1.Length; i++)
                            {
                                DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
                                lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                                //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
                                colLidhjet.Add(lidhje);
                            }
                            foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjet)
                            {
                                if (mesazhAdmin.Status)
                                {
                                    o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("KPF", dbKont);
                                    o.IdLidhese = idkpf;
                                    mesazhAdmin = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                                }
                                else
                                {

                                    mesazh.Status = false;
                                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                                    return mesazh;
                                }
                            }
                        }
                        if (mesazhAdmin.Status)
                        {

                            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                            return mesazh;
                        }
                        else
                        {

                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                            return mesazh;
                        }
                    }
                    else
                    {

                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                        return mesazh;
                    }
                }
                else
                {

                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te modifikuar nje objekt clsKPF dhe autorizimet e lidhur me te.
        /// <param name="idkpf">id e kpf-se</param>
        /// <param name="kodikpf">kodi i kpf-se</param>
        /// <param name="nivelikpf">niveli i kpf-se</param>
        /// <param name="emertimikpf">emertimi i kpf-se</param>
        /// <param name="inakt">aktive ose inaktive</param>
        /// <param name="shenimekpf">shenime</param>
        /// <param name="grupikpf">grupi i kpfse</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idndervit">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idkonfig">id konfigurimit</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh modifikoKPFAndBuxhete(int idkpf, string kodikpf, int nivelikpf, string emertimikpf, bool inakt, string shenimekpf, int grupikpf, int idndermarje,
            int idperdoruesi, int idkonfig, int idstatusdok, clsDatabaseKontabilitet dbKont, string emertimikpf_fr)
        {
            clsKPF KPF = new clsKPF(idkpf, dbKont);
            DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
            if (IdAutorizimi != "")
            {

                string[] pars1 = IdAutorizimi.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
                    lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                    colLidhjet.Add(lidhje);
                }

            } 
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbKont);
            
            try
            {
                DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(idkpf, "KPF", dbAdmin);
                clsMesazh mesazh = new clsMesazh();
                DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
                mesazh = dbKont.modifikoKPF(idkpf, kodikpf, nivelikpf, emertimikpf, inakt, shenimekpf, grupikpf, idndermarje, idperdoruesi, idkonfig, idstatusdok, emertimikpf_fr);
                if (mesazh.Status)
                {
                    mesazhAdmin = clsFunksione.modifikoLidhjeAutorizimSipasLlojitTeBuxhetit(colLidhjet, "KPF", KPF.IdKPF, colLidhjetAutorizim, dbKont, dbAdmin, false, idperdoruesi);
                   
                    if (mesazhAdmin.Status)
                    {
                        mesazh = new clsMesazh(true, "Modifikimi perfundoi me sukses!");
                        return mesazh;
                    }
                    else
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                        return mesazh;
                    }
                }
                else
                {
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh modifikoKPFAndBuxhete(int idkpf, string kodikpf, int nivelikpf, string emertimikpf, bool inakt, string shenimekpf, int grupikpf, int idnder, int idndervit, " +
        //    "int idperdoruesi, int idkonfig)", true)]
        //public clsMesazh modifiko()
        //{
        //    clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
        //    clsMesazh u_modifikua = data.modifikoKPFAndBuxhete(this);
        //    return u_modifikua;
        //}

        /// <summary>
        /// Ekzekuton nje transaksion per te fshire nje objekt clsKPF dhe autorizimet e lidhur me te.
        /// <param name="idkpf">id e kpf-se</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh fshiKPFAndBuxhete(int idkpf, int idPerdorues)
        {//fshin KPFne dhe buxhetet perkatese
            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet();
            //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("KPF");
            DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.colLidhjetAutorizim(idkpf, "KPF");
            //DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idkpf, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("KPF"));
            clsMesazh mesazh = new clsMesazh();
            DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
            try
            {
                dbKont.beginTransaksion();

                DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbKont );
                foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjeAutorizim)
                {
                    if (mesazhAdmin.Status)
                    {
                        mesazhAdmin = dbAdmin.fshiLidhjeAutorizim(o.IdLidhjeAutorizim);
                    }
                    else
                    {
                        dbKont.rollbackTransaksion();

                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                        return mesazh;
                    }
                }
                if (mesazhAdmin.Status)
                {
                    mesazh = dbKont.fshiKPFStatus(idkpf, idPerdorues);
                    if (mesazh.Status)
                    {
                        dbKont.commitTransaksion();

                        mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                        return mesazh;
                    }
                    else
                    {
                        dbKont.rollbackTransaksion();

                        return mesazh;
                    }
                }
                else
                {
                    dbKont.rollbackTransaksion();

                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbKont.rollbackTransaksion();

                return new clsMesazh(false, ce.Message);
            }
        }
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh fshiKPFAndBuxhete(int idkpf)", true)]
        public clsMesazh fshi()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            clsMesazh u_fshi = data.fshiKPFStatus(this.idKPF, this.idPerdoruesi);
            return u_fshi;
        }

        /// <summary>
        /// Merr nje objekt KPF. Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.merrKPF"/>
        /// </summary>
        public void merr()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            data.merrKPF(this.KodiKPF);
            data.Dispose();
        }
        /// <summary>
        /// Merr nje objekt KPF. Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.merrKPF"/>
        /// </summary>
        public static bool eshtePrind(string kodi, int ndermarje, int grupi, int niveli)
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            bool eshte = data.eshtePrind(kodi, ndermarje, grupi, niveli);
            data.Dispose();
            return eshte;
        }
        /// <summary>
        /// Merr nje collection me objekte KPF.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ktheGjitheKPFteSipasGrupit"/>
        /// <param name="grupiKPF">Grupi KPF</param>
        /// <param name="idNdermVit">Id lidhese ndermarrje - vit</param>
        /// </summary>
        public colKPFte merriTeGjithe(int grupiKPF, int idndermarje)
        {
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //return data.merrGjitheKPFteSipasGrupit(grupiKPF, idNdermVit);
            colKPFte data = new colKPFte();
            data.mbushGjitheKPFteSipasGrupit(grupiKPF, idndermarje);
            return data;
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen id e kpf-se sipas nr te llogarise dhe idndermarrjes
        /// </summary>
        /// <param name="kodi">kodi i kpf-se</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>id e KPF-se</returns>
        public static int mbushIDKPF(string kodi, int idnderm, int grupi)
        {
            clsDatabaseKontabilitet dbKPF = new clsDatabaseKontabilitet();
            int id = (dbKPF.merrKPF(kodi, idnderm, grupi));
            dbKPF.Dispose();
            return id;
        }
        public static int mbushIDKPF(string kodi, int idnderm, int grupi, clsDatabaseKontabilitet dbKPF)
        {

            int id = (dbKPF.merrKPF(kodi, idnderm, grupi));

            return id;
        }

        public clsMesazh kontrollotransferim(clsKPF kod, int idndermarje, clsDatabaseKontabilitet db, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!db.ekzistonKPF(kod.kodiKPF, idndermarje, kod.grupiKPF))
            {
                DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
                DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare(db );

                konf.mbushKonfigAmbjSipasKod("LLS", idndermarje, dbshare);

                kod.idKonfig = konf.IdKonfigAmbjente;
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;

            }
            else
            {
                clsKPF kodnderm = new clsKPF(kod.kodiKPF, idndermarje, kod.grupiKPF, db);
                kod.idKPF = kodnderm.idKPF;
                if (kodnderm.dtModifikimi < kod.dtModifikimi)
                {

                    kod.idPerdoruesi = idperdoruesi;
                    kod.IdNdermarje = idndermarje;

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
        /// mbush KPF-ne nga databaza
        /// </summary>
        /// <param name="dbDataRowLlogari">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushKPF(DataRow dbDataRowKPF)
        {
            if (dbDataRowKPF != null)
            {
                try
                {
                    int.TryParse(dbDataRowKPF["niveli1"].ToString(), out niveli1);
                    int.TryParse(dbDataRowKPF["niveli2"].ToString(), out niveli2);
                    int.TryParse(dbDataRowKPF["niveli3"].ToString(), out niveli3);
                    int.TryParse(dbDataRowKPF["niveli4"].ToString(), out niveli4);
                    int.TryParse(dbDataRowKPF["niveli5"].ToString(), out niveli5);
                    int.TryParse(dbDataRowKPF["niveli6"].ToString(), out niveli6);
                    int.TryParse(dbDataRowKPF["niveli7"].ToString(), out niveli7);
                    double.TryParse(dbDataRowKPF["gjendja"].ToString(), out gjendja);
                    int.TryParse(dbDataRowKPF["IDKPF"].ToString(), out idKPF);
                    kodiKPF = dbDataRowKPF["KODIKPF"].ToString();
                    int.TryParse(dbDataRowKPF["NIVELIKPF"].ToString(), out niveliKPF);
                    emertimiKPF = dbDataRowKPF["EMERTIMIKPF_sq"].ToString();
                    idAutorizimi = "";
                    bool.TryParse(dbDataRowKPF["INAKTIV"].ToString(), out inaktiv);
                    shenimeKPF = dbDataRowKPF["EMERTIMIKPF_en"].ToString();
                    int.TryParse(dbDataRowKPF["GRUPIKPF"].ToString(), out grupiKPF);
                    int.TryParse(dbDataRowKPF["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowKPF["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowKPF["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowKPF["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowKPF["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKPF["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    emertimiKPF_fr = dbDataRowKPF["EMERTIMIKPF_fr"].ToString();
                    if (niveli1 == 1)
                    {
                        urlImage1 = "~/images/folder_closed.png"; ; //pathi i imazhit
                    }
                    else
                    {
                        urlImage1 = "~/images/blankimage.bmp";
                    }

                    if (niveli2 == 1)
                    {
                        urlImage2 = "~/images/folder_closed.png"; ; //pathi i imazhit
                    }
                    else
                    {
                        urlImage2 = "~/images/blankimage.bmp";
                    }

                    if (niveli3 == 1)
                    {
                        urlImage3 = "~/images/folder_closed.png"; ; //pathi i imazhit
                    }
                    else
                    {
                        urlImage3 = "~/images/blankimage.bmp";
                    }

                    if (niveli4 == 1)
                    {
                        urlImage4 = "~/images/folder_closed.png"; ; //pathi i imazhit
                    }
                    else
                    {
                        urlImage4 = "~/images/blankimage.bmp";
                    }

                    if (niveli5 == 1)
                    {
                        urlImage5 = "~/images/folder_closed.png"; ; //pathi i imazhit
                    }
                    else
                    {
                        urlImage5 = "~/images/blankimage.bmp";
                    }
                    if (niveli6 == 1)
                    {
                        urlImage6 = "~/images/folder_closed.png"; ; //pathi i imazhit
                    }
                    else
                    {
                        urlImage6 = "~/images/blankimage.bmp";
                    }
                    if (niveli7 == 1)
                    {
                        urlImage7 = "~/images/folder_closed.png"; ; //pathi i imazhit
                    }
                    else
                    {
                        urlImage7 = "~/images/blankimage.bmp";
                    }
                    if (dbDataRowKPF["Prind"] != null)
                        prind = dbDataRowKPF["Prind"].ToString();
                    else prind = "";
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se KPF-ve nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
