using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne kokat e skemave work flow
    ///  (Te dhenat  merren nga tabela : T_KOKASKEMAWORKFLOW)
    /// </summary>
    public class clsKokaSkemaWorkFlow
    {
        #region Atribute

        private int idKoka;
        private string kodi;
        private string emertimi;
        private int idPerdoruesi;
        private bool njoftim;
        private int nr;
        private int formula;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idKonfig;
        private colTrupiSkemaWorkFlow oColTrupat;
        private bool dergoEmailPasAprovimitFinal;
        private string emailAprovimi;
        private DataRow rreshti;

        #endregion

        #region Properties
        /// <summary>
        /// dergohet email tek emailaprovimi pas aprovimit final
        /// </summary>
        public bool DergoEmailPasAprovimitFinal
        {
            get
            {
                return dergoEmailPasAprovimitFinal;
            }
            set
            {
                dergoEmailPasAprovimitFinal = value;
            }
        }

        /// <summary>
        /// 
        /// emailet ku do dergohet emaili
        /// </summary>
        public string EmailAprovimi
        {
            get
            {
                return emailAprovimi;
            }
            set
            {
                emailAprovimi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
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
        /// Kthen/Vendos kodin .
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }
        /// <summary>
        /// kthen vendos njoftim nese do njoftohen perdoruesit me email apo jo
        /// </summary>
        public bool Njoftim
        {
            get
            {
                return njoftim;
            }
            set
            {
                njoftim = value;
            }
        }

        /// <summary>
        /// formula e llogaritjes se dates se kujteses
        /// </summary>
        public int Formula
        {
            get
            {
                return formula;
            }
            set
            {
                formula = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos emertimin.
        /// </summary>
        public string Emertimi
        {
            get
            {
                return emertimi;
            }
            set
            {
                emertimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
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
        /// nr i diteve javeve muajve
        /// </summary>
        public int Nr
        {
            get
            {
                return nr;
            }
            set
            {
                nr = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nje koleksion me trupat e skemes.
        /// </summary>
        public colTrupiSkemaWorkFlow OColTrupat
        {
            get
            {
                return oColTrupat;
            }
            set
            {
                oColTrupat = value;
            }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        /// <summary>
        /// kthen/vendos id e konfigurimit
        /// </summary>
        public int IdKonfig
        {
            get
            {
                return idKonfig;
            }
            set
            {
                idKonfig = value;
            }
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

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idkoka"> id ritese e kokes </param>
        /// <param name="kodi"> kodi</param>
        /// <param name="emertimi"> emertimi</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="formula">formula</param>
        /// <param name="njoftim"> njoftim</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        public clsKokaSkemaWorkFlow(int idkoka, string kodi, string emertimi, int idPerdoruesi, int formula, bool njoftim, int idnderm, int idstatusdok, int idkonfig, int nr, bool dergoemail, string emailaprovimi)
        {
            this.idKoka = idkoka;
            this.kodi = kodi;
            this.emertimi = emertimi;
            this.idPerdoruesi = idPerdoruesi;
            this.formula = formula;
            this.njoftim = njoftim;
            this.idNdermarje = idnderm;
            this.idStatusDok = idstatusdok;
            this.idKonfig = idkonfig;
            this.nr = nr;
        }
        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idkoka"> id ritese e kokes </param>
        /// <param name="kodi"> kodi</param>
        /// <param name="emertimi"> emertimi</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="formula">formula</param>
        /// <param name="njoftim"> njoftim</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        public clsKokaSkemaWorkFlow(int idkoka, string kodi, string emertimi, int idPerdoruesi, int formula, bool njoftim, int idnderm, int idstatusdok, int idkonfig, int nr, colTrupiSkemaWorkFlow trupi, bool isshtim, bool dergoemail, string emailaprovimi)
        {
            try
            {
                this.idKoka = idkoka;
                this.kodi = kodi;
                this.emertimi = emertimi;
                this.idPerdoruesi = idPerdoruesi;
                this.formula = formula;
                this.njoftim = njoftim;
                this.idNdermarje = idnderm;
                this.idStatusDok = idstatusdok;
                this.idKonfig = idkonfig;
                this.dergoEmailPasAprovimitFinal = dergoemail;
                this.emailAprovimi = emailaprovimi;
                this.nr = nr;
                oColTrupat = trupi;
                clsMesazh mesazh = kontrolloSkeme(isshtim);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idkoka"> id ritese e kokes </param>
        /// <param name="kodi"> kodi</param>
        /// <param name="emertimi"> emertimi</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="formula">formula</param>
        /// <param name="njoftim"> njoftim</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        public clsKokaSkemaWorkFlow(string kodi, string emertimi, int idPerdoruesi, int formula, bool njoftim, int idnderm, int idstatusdok, int idkonfig, int nr, bool dergoemail, string emailaprovimi)
        {

            this.kodi = kodi;
            this.emertimi = emertimi;
            this.idPerdoruesi = idPerdoruesi;
            this.formula = formula;
            this.njoftim = njoftim;
            this.idNdermarje = idnderm;
            this.idStatusDok = idstatusdok;
            this.idKonfig = idkonfig;
            this.nr = nr;
            this.dergoEmailPasAprovimitFinal = dergoemail;
            this.emailAprovimi = emailaprovimi;
        }
        /// <summary>
        /// konstruktori me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes kategori zbritje</param>
        public clsKokaSkemaWorkFlow(int id)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            mbushKokaSkema(db.merrKokaSkemaWorkFlowSipasId(id));
            db.Dispose();
        }
        public clsKokaSkemaWorkFlow(int id, clsDatabaseAdmin db)
        {

            mbushKokaSkema(db.merrKokaSkemaWorkFlowSipasId(id));

        }
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKokaSkemaWorkFlow()
        {
        }

        public clsKokaSkemaWorkFlow(DataRow rreshti)
        {
            
            mbushKokaSkema(rreshti);
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kontroll nese objekti i aktivitete i ka te dhenat e sakta 
        /// </summary>
        /// <param name="shtim">tregon nese eshte shtim apo modifikim</param>
        /// <returns> clsMesazh  me statusin nese te dhenat jane te sakta apo jo</returns>
        private clsMesazh kontrolloSkeme(bool shtim)
        {
            if (kodi == "")
                return new clsMesazh(false, "Plotesoni kodin e skemes");
            if (emertimi == "")
                return new clsMesazh(false, "Plotesoni emertimin e skemes");
            if (formula == 0)
                return new clsMesazh(false, "Plotesoni tipin e formules");

            if (shtim && ekziston(kodi, idNdermarje))
                return new clsMesazh(false, "Ekziston nje skeme me kete kod!Ju lutem shenoni nje kod tjeter!");

            if (nr < 0)
                return new clsMesazh(false, "Numri i formules duhet te jete numer pozitiv!");
            if (oColTrupat.Count == 0)
                return new clsMesazh(false, "Duhet te zgjidhni te pakten nje perdorues!");
            string strPattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            List<clsTrupiSkemaWorkFlow> list = oColTrupat.OrderBy(x => x.Niveli).ToList();
            oColTrupat = new colTrupiSkemaWorkFlow();
            oColTrupat.AddRange(list);
            double vleralimitmax = int.MinValue;
            int nivelifundit = 0;
            int nivelimax = oColTrupat[oColTrupat.Count - 1].Niveli;
            bool kaNivel1 = false;
            int niveliparaardhes = 0;
            double vleralimitparaardhese = int.MinValue;


            foreach (clsTrupiSkemaWorkFlow trup in oColTrupat)
            {
                if (trup.Niveli == 1)
                    kaNivel1 = true;
                if (trup.NiveliApr != 0 && trup.NiveliApr < trup.Niveli)
                {
                    return new clsMesazh(false, "Kushti i nivelit duhet te jete me i larte se niveli!");
                }
                if (trup.Niveli == nivelimax)
                {

                    if (trup.VleraLimit != 0)
                        return new clsMesazh(false, "Niveli i fundit nuk mund te kete vlere limit!");
                    if (trup.ColGrupeKF.Count > 0)
                        return new clsMesazh(false, "Niveli i fundit nuk mund te kete grup klienti!");
                }
                if (trup.Lloji == 1 && njoftim == true)
                {
                    if (trup.Email == "" || trup.Email == null)
                        return new clsMesazh(false, "Keni perdorues pa adrese emaili!");

                    if (trup.Email != "" && !System.Text.RegularExpressions.Regex.IsMatch(trup.Email, strPattern))
                        return new clsMesazh(false, "Adresa e emailit nuk eshte e vlefshme!");
                }
                if (trup.VleraLimit != 0 && trup.VleraLimit > vleralimitmax)
                    vleralimitmax = trup.VleraLimit;
                else
                {
                    if (trup.VleraLimit != 0 && nivelifundit != trup.Niveli)
                        return new clsMesazh(false, "Vlera limit e nje niveli me te larte nuk mund te jete me e vogel se ajo e nje niveli me te ulet!");
                }
                if (niveliparaardhes == trup.Niveli && vleralimitparaardhese != trup.VleraLimit)
                    return new clsMesazh(false, "Perdorues te te njejtit nivel duhet te kene te njejten vlere limit!");
                else if (niveliparaardhes != trup.Niveli)
                {
                    niveliparaardhes = trup.Niveli;
                    vleralimitparaardhese = trup.VleraLimit;
                }

                if (trup.Lloji == 2)
                {
                    colRolPerdorues role = new colRolPerdorues();
                    role.mbushRolePerdoruesSipasRoli(trup.IdPerdRol);
                    if (role.Count == 0)
                        return new clsMesazh(false, "Roli " + trup.Perdoruesi + " nuk ka asnje perdorues!");
                }

                if (trup.Delegimi != "")
                {
                    clsMesazh mes = kontrolloDelegimNivelePoshte(trup, oColTrupat, trup.Niveli);
                    if (!mes.Status)
                        return mes;
                }
                //if (njoftim == true && trup.Delegimi != "")
                //{
                //    clsPerdorues per = new clsPerdorues(trup.IdDelegimi);
                //    if (per.PerdoruesEmail == "")
                //        return new clsMesazh(false, "Keni delegues pa adrese emaili!");
                //}
                nivelifundit = trup.Niveli;
            }
            if (!kaNivel1)
                return new clsMesazh(false, "Duhet te kete te pakten nje perdorues me nivelin 1!");
            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        private clsMesazh kontrolloDelegimNivelePoshte(clsTrupiSkemaWorkFlow p, colTrupiSkemaWorkFlow oColTrupat, int nivelifundit)
        {
            foreach (clsTrupiSkemaWorkFlow trup in oColTrupat)
            {
                if (trup.Niveli < nivelifundit)
                {
                    if (trup.Lloji == 1)
                    {
                        if (p.Delegimi == trup.Perdoruesi)
                            return new clsMesazh(false, "Deleguesi nuk mund te jete pjese e skemes te nje niveli me te ulet!");
                    }
                    else
                    {
                        DbCore.DbAdmin.colRolPerdorues role = new DbCore.DbAdmin.colRolPerdorues();
                        role.mbushRolePerdoruesSipasRoli(trup.IdPerdRol);
                        foreach (DbCore.DbAdmin.clsRolPerdorues perd in role)
                        {
                            if (perd.IdPerdorues == p.IdDelegimi)
                                return new clsMesazh(false, "Deleguesi nuk mund te jete pjese e skemes te nje niveli me te ulet!");

                        }
                    }
                }
            }
            return new clsMesazh(true);
        }

        /// <summary>
        /// Ruan nje objekt skema workflow sebashku me trupin
        /// Nje objekt skema workflow ka nje koleksion me trupat, 
        /// ruajtja e nje skeme workflow imponon ruajtjen edhe te nje colection-i me trupat
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe skema bashke me trupat konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje skeme sebashku me trupat
        /// </summary>
        /// <param name="idkoka"> id ritese e kokes </param>
        /// <param name="kodi"> kodi</param>
        /// <param name="emertimi"> emertimi</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="formula">formula</param>
        /// <param name="njoftim"> njoftim</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public clsMesazh ruajSkemeWorkFlow(CultureInfo ci, System.Resources.ResourceManager rm)
        {//ruan kategoriZbritje
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool statusVeprimi;     
            clsMesazh mesazh = new clsMesazh();
            try
            {
                db.beginTransaksion();
                idKoka = db.ruajKokaSkemaWorkFlow(idKoka, kodi, emertimi, idPerdoruesi, njoftim, formula, idNdermarje, idStatusDok, idKonfig, nr, dergoEmailPasAprovimitFinal, emailAprovimi);
                this.IdKoka = idKoka;
                if (idKoka == 0)
                { statusVeprimi = false; mesazh.Status = false; }
                else { statusVeprimi = true; mesazh.Status = true; }

                if (statusVeprimi)
                {
                    foreach (clsTrupiSkemaWorkFlow o in oColTrupat)
                    {
                        if (mesazh.Status)
                        {
                            o.IdKoka = idKoka;
                            int idT;
                            if (o.Lloji == 1)
                            {
                                DataRow dr = db.merrPerdorues(o.IdPerdRol);
                                if (dr != null && ((dr["PERDORUESEMAIL"].ToString() != o.Email)) && !(String.IsNullOrEmpty(dr["PERDORUESEMAIL"].ToString()) && String.IsNullOrEmpty(o.Email)))
                                {
                                    mesazh = db.modifikoPerdoruesEmail(o.IdPerdRol, o.Email);
                                    if (!mesazh.Status)
                                    {
                                        db.rollbackTransaksion();

                                        return mesazh;
                                    }
                                }
                            }
                            mesazh = db.ruajTrupiSkemaWorkFlow(out idT, o.IdKoka, o.Lloji, o.IdPerdRol, o.Niveli, o.VleraLimit, o.Modifiko, o.IdDelegimi, o.NiveliApr, o.LlojGrupiKf, o.Dite , o.Pershkrimi);
                            if (!mesazh.Status)
                            {
                                db.rollbackTransaksion();

                                return mesazh;
                            }
                            foreach (DbKontabiliteti.clsGrupeKF grup in o.ColGrupeKF)
                            {
                                int id = 0;
                                mesazh = db.ruajGrupeKfperWorkflow(out id, idT, grup.IdGrupi);
                                if (!mesazh.Status)
                                {
                                    db.rollbackTransaksion();

                                    return mesazh;
                                }
                            }
                        }
                        else
                        {
                            db.rollbackTransaksion();

                            return mesazh;
                        }
                    }
                    if (mesazh.Status)
                    {                     
                        db.commitTransaksion();
                        mesazh = new clsMesazh(true, rm.GetString("msgAdministrimiRuajtjaPerfundoiSukses", ci));
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

                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                db.rollbackTransaksion();

                return new clsMesazh(false, ce.Message);
            }
        }


        /// <summary>
        /// Modifikon nje objekt skeme sebashku me trupin
        /// Nje objekt skeme ka nje koleksion me trupat, 
        /// modifikimi e nje skeme imponon modifikimin edhe te nje colection-i me trupat
        /// Mqs cdo rresht i ri qe modifikohet ne DB kerkon thirrjen e nje SP-je me parametra dhe skeme bashke me trupat konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje skeme sebashku me trupat
        /// </summary>
        /// <param name="idkoka"> id ritese e kokes </param>
        /// <param name="kodi"> kodi</param>
        /// <param name="emertimi"> emertimi</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="formula">formula</param>
        /// <param name="njoftim"> njoftim</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        ///  <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>     
        public clsMesazh modifikoSkemeWorkFlow(CultureInfo ci)
        {

            colTrupiSkemaWorkFlow trupat = new colTrupiSkemaWorkFlow();
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            clsMesazh mesazh = new clsMesazh();
            try
            {
                db.beginTransaksion();
                mesazh = db.modifikoKokaSkemaWorkFlow(idKoka, kodi, emertimi, idPerdoruesi, njoftim, formula, idNdermarje, idStatusDok, idKonfig, nr, dergoEmailPasAprovimitFinal, emailAprovimi);

                if (mesazh.Status)
                {
                    mesazh = db.fshiGrupeKfperWorkflowSipasIdKoka(idKoka);
                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        return mesazh;
                    }
                    mesazh = db.fshiTrupiSkemaWorkFlowSipasKokes(idKoka);
                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        return mesazh;
                    }
                    foreach (clsTrupiSkemaWorkFlow o in oColTrupat)
                    {
                        if (mesazh.Status)
                        {
                            o.IdKoka = idKoka;
                            int idT;
                            if (o.Lloji == 1)
                            {
                                DataRow dr = db.merrPerdorues(o.IdPerdRol);
                                if (dr != null && dr["PERDORUESEMAIL"].ToString() != o.Email && !(String.IsNullOrEmpty(dr["PERDORUESEMAIL"].ToString()) && String.IsNullOrEmpty(o.Email)))
                                {
                                    mesazh = db.modifikoPerdoruesEmail(o.IdPerdRol, o.Email);
                                    if (!mesazh.Status)
                                    {
                                        db.rollbackTransaksion();

                                        return mesazh;
                                    }
                                }
                            }
                            mesazh = db.ruajTrupiSkemaWorkFlow(out idT, o.IdKoka, o.Lloji, o.IdPerdRol, o.Niveli, o.VleraLimit, o.Modifiko, o.IdDelegimi, o.NiveliApr, o.LlojGrupiKf, o.Dite , o.Pershkrimi);
                            if (!mesazh.Status)
                            {
                                db.rollbackTransaksion();

                                return mesazh;
                            }
                            foreach (DbKontabiliteti.clsGrupeKF grup in o.ColGrupeKF)
                            {
                                int id = 0;
                                mesazh = db.ruajGrupeKfperWorkflow(out id, idT, grup.IdGrupi);
                                if (!mesazh.Status)
                                {
                                    db.rollbackTransaksion();

                                    return mesazh;
                                }
                            }
                        }
                        else
                        {
                            db.rollbackTransaksion();

                            return mesazh;
                        }
                    }
                    if (mesazh.Status)
                    {
                        db.commitTransaksion();
                        mesazh = new clsMesazh(true, rm.GetString("msgModifikimiMeSukses",ci));
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

                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                db.rollbackTransaksion();

                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh fshiSkemeWorkFlow(int idKoka, int idperdoruesi,CultureInfo ci, System.Resources.ResourceManager rm)
        {

            clsDatabaseAdmin db = new clsDatabaseAdmin();
            clsMesazh mesazh = new clsMesazh(true);
            try
            {
                db.beginTransaksion();
                if (mesazh.Status)
                {
                    mesazh = db.fshiKokaSkemaWorkFlowStatus(idKoka, idperdoruesi);
                    //mesazh =  fshiKokaKategoriZbritje(kategorizbritje);
                    if (mesazh.Status)
                    {
                        db.commitTransaksion();
                        mesazh = new clsMesazh(true, rm.GetString("msgFshirjaMeSukses",ci));
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

                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                db.rollbackTransaksion();

                return new clsMesazh(false, ce.Message);
            }
        }


        public void merr()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            data.merrKokaSkemaWorkFlow(this.idKoka);
            data.Dispose();
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id e kokes skemes sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kod">kodi </param>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>id e kokes skemes</returns>
        public static int ktheIdKokaSkemaWorkFlow(string kod, int idnder)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            int idKoka = (dbartikuj.merrIdKokaSkemaWorkFlowSipasKodit(kod, idnder));
            dbartikuj.Dispose();
            return idKoka;
        }
        public static bool ekziston(string kod, int idnder)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool ekziston = db.ekzistonKokaSkemaWorkFlow(kod, idnder);
            db.Dispose();
            return ekziston;
        }
        public static bool kaDokumentaPerAprovimSkemaWorkFlow(int idkoka)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool ekziston = db.kaDokumentaPerAprovimSkemaWorkFlow(idkoka);
            db.Dispose();
            return ekziston;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush kokat sipas skemes nga databaza
        /// </summary>
        /// <param name="dbDataRow">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushKokaSkema(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {

                try
                {
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    kodi = dbDataRow["KODI"].ToString();
                    emertimi = dbDataRow["EMERTIMI"].ToString();
                    emailAprovimi = dbDataRow["EMAILAPROVIMI"].ToString();
                    bool.TryParse(dbDataRow["DERGOEMAILPASAPROVIMITFINAL"].ToString(), out dergoEmailPasAprovimitFinal);
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    bool.TryParse(dbDataRow["NJOFTIM"].ToString(), out njoftim);
                    int.TryParse(dbDataRow["NR"].ToString(), out nr);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRow["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRow["FORMULA"].ToString(), out formula);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se skemave workflow nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
