using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.DbRegjistrim;
using DbCore.DbListPagesat;

namespace DbCore.DbArkaBanka
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne trupin e nje dokumenti banke
    ///  (Te dhenat  merren nga tabela : T_VEPRIMBANKATRUPI)
    /// </summary>
    public class clsVeprimBankaTrupi
    {
        #region Atribute

        private int idTrupi;
        private int idKoka;
        private String lloji;
        private int idSubjekti;
        private String pershkrimiTrupi;
        private String debiKredi;
        private int idFatura;
        private double zbritja;
        private double kreditet;
        private double vleraPaguar;
        private double vleraPaguarMonedhaBaze;
        private double vleraPaArketueshme;
        private double kmk;
        private int idNivel;
        private int idOpsionePagese;
        private string nrTel;
        private string opsionePagese;
        private string _muaji;
        private string kodi;
        private double vleraFillestare;
        private double vleraMbetur;
        private string statusFature;
        private DataRow rreshti;
        private bool meKursFature;

        #endregion

        #region Konstruktoret


        public clsVeprimBankaTrupi(Dictionary<string, object> rreshtDokuKlient, object nivele, int idndermarje, DateTime data, int idmonedhabanka, string kursiKoka, string hfShtimModifikim, string hfKursiEkzistues, object id, int idperdoruesi, bool perBRM)
        {
            string kodi = Convert.ToString(rreshtDokuKlient["txtSubjekti"]);
            if (kodi == "null" || kodi == "")
            {
                IdSubjekti = -1;
                return;
            }

            string lloji = Convert.ToString(rreshtDokuKlient["txtLloji"]);
            //Nuk duhet te ishte ne fillim ky kushti?
            if (lloji == "Llogari" || perBRM)
                this.IdFatura = 0;
            else if (!(lloji == "Furnitor" || lloji == "Klient"  || lloji == "Llogari" || lloji == "Punonjes"))
                return;

            int idMonNderm = DbAdmin.clsNdermarrje.ktheIdMonedheNdermSipasID(idndermarje);
            string fatura = Convert.ToString(rreshtDokuKlient["txtFatura"]);
            string emertimi = Convert.ToString(rreshtDokuKlient["txtEmertimi"]);
            string dk = Convert.ToString(rreshtDokuKlient["txtDebiKredi"]);
            string vlefta = Convert.ToString(rreshtDokuKlient["txtVlera"]);
            string zbritja = Convert.ToString(rreshtDokuKlient["txtZbritja"]);
            string pershkrimi = Convert.ToString(rreshtDokuKlient["txtPershkrimi"]);
            string kreditet = Convert.ToString(rreshtDokuKlient["txtKreditet"]);
            string vleraarketuar = Convert.ToString(rreshtDokuKlient["txtVleraArketuar"]);
            string vleramonbaze = Convert.ToString(rreshtDokuKlient["txtVleraMonBaze"]);
            string opsionepagese = Convert.ToString(rreshtDokuKlient["txtOpsione"]);
            string nrtel = Convert.ToString(rreshtDokuKlient["txtNrTel"]);
            string kursTrupiGrida = Convert.ToString(rreshtDokuKlient["txtKursi"]);
            string muaji = rreshtDokuKlient["txtMuaji"].ToString();
            string kodifature = rreshtDokuKlient["txtKodi"].ToString();
            string vlerafillestare = rreshtDokuKlient["txtVleraFill"].ToString();
            string vlerambetur = rreshtDokuKlient["txtVleraMbetur"].ToString();
            string statusFature = rreshtDokuKlient["StatusFature"].ToString();
            bool meKursFature = Convert.ToBoolean(rreshtDokuKlient["MeKursFature"].ToString());

            this.KMK = 1;
            this.meKursFature = meKursFature;
            if (kursTrupiGrida != "null" && kursTrupiGrida != "" && kursTrupiGrida != "NaN") //A ka mundesi te mos plotesohet ndonjehere ky kushti? Apo kmk plotesohet gjithmone me ate te kokes momentalisht?
            {
                this.KMK = double.Parse(kursTrupiGrida);
            }

            this.Lloji = lloji;
            if (Lloji == "Llogari")
            {
                DbKontabiliteti.clsLlogari llog = new DbKontabiliteti.clsLlogari();
                llog.merrLlogariAktiveSipasKodit(kodi, idndermarje);
                this.IdSubjekti = llog.IdLlogari;
            }

            else if (Lloji == "Punonjes")
            {
                clsPunonjes pun = new clsPunonjes(kodi, idndermarje);
                
                this.IdSubjekti = pun.IdPunonjes;
                
            }


            else if (Lloji == "Furnitor" || Lloji == "Klient")
            {
                DbKontabiliteti.clsKlientFurnitor kf = new DbKontabiliteti.clsKlientFurnitor();
                if (!String.IsNullOrEmpty(kodi))
                {
                    kf.mbushKlientFurnitorSipasKodit(kodi, idndermarje, idperdoruesi);
                    if (kf.IdKlientFurnitor == 0)
                        throw new Exception($"Klient/Furnitori {kodi} nuk ekziston!");

                    if (!kf.AktivKF)
                        throw new Exception($"Klient/Furnitori {kodi} nuk është aktive!");
                }

                this.IdSubjekti = kf.IdKlientFurnitor;
            }
            else
                this.IdSubjekti = -1;

            this.DebiKredi = (dk != "null" && dk != "") ? dk : "";
            this.PershkrimiTrupi = (pershkrimi != "null" && pershkrimi != "") ? pershkrimi : "";
            this.nrTel = (nrtel != "null" && nrtel != "") ? nrtel : "";
            this.Muaji = muaji != "null" && muaji != "" ? muaji : "";

            if (opsionepagese != "null" && opsionepagese != "")
            {
                clsOpsionePagese op = new clsOpsionePagese(opsionepagese);
                this.idOpsionePagese = op.Id;
            }
            else
                this.idOpsionePagese = 0;

            this.statusFature = statusFature;
            this.kodi = (kodifature != "null" && kodifature != "") ? kodifature : "";
            this.vleraFillestare = (vlerafillestare != "null" && vlerafillestare != "") ? double.Parse(vlerafillestare) : 0;
            this.vleraMbetur = (vlerambetur != "null" && vlerambetur != "") ? double.Parse(vlerambetur) : 0;
            if (zbritja != "null" && zbritja != "" && zbritja != "NaN")
                this.Zbritja = double.Parse(zbritja);
            this.Kreditet = (kreditet != "null" && kreditet != "" && kreditet != "NaN") ? double.Parse(kreditet) : 0;
            if (vleraarketuar != "null" && vleraarketuar != "" && vleraarketuar != "NaN")
                this.VleraPaguar = double.Parse(vleraarketuar);
            if (vleramonbaze != "null" && vleramonbaze != "" && vleramonbaze != "NaN")
                this.VleraPaguarMonedhaBaze = double.Parse(vleramonbaze);
            if (vlefta != "null" && vlefta != "" && vlefta != "NaN")
                this.VleraPaArketueshme = double.Parse(vlefta);

            //if fatura I don't know
            if (fatura == "null" || fatura == "")
                return;
            string[] vleratFatures = fatura.Split(',');
            if (vleratFatures[0] == null || vleratFatures[0] == "null" || vleratFatures[0] == "")   //nese vlera eshte null ska kuptim dokumenti 
                return;
            this.IdFatura = 0;
            int idNiveli = 0;
            if (int.TryParse(nivele.ToString(), out idNiveli) && idNiveli > 0)
            {
                int idKategori = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(idNiveli);
                if (idKategori != 20 && !(id.ToString() == "0"))
                {
                    clsKokaShitje kokaShitje = new clsKokaShitje();
                    kokaShitje.mbushKokaShitjeSipasIDPaTrup(int.Parse(hiqCharNgaIDfatura(id.ToString())));
                    this.IdFatura = kokaShitje.IdShitjeKoka;
                    this.idNivel = kokaShitje.IdNivel;
                }
                else
                {
                    clsVeprimeKFKoka vep = new clsVeprimeKFKoka(int.Parse(hiqCharNgaIDfatura(id.ToString())));
                    this.IdFatura = vep.IdVeprimeKFKoka;
                    this.idNivel = vep.IdNivel;
                }
            }
        }

        public string hiqCharNgaIDfatura(string idFatura)
        {
            if (idFatura.Substring(0, 1) == "S")
                return idFatura.Substring(2, idFatura.Length - 2);
            else if (idFatura.Substring(0, 1) == "V")
                return idFatura.Substring(3, idFatura.Length - 3);
            else
                return idFatura;
        }

        public clsVeprimBankaTrupi(String llojisubjektit, int idsubjekti, String pershkrimitrupi, String debikredi, int idfatura, double zbritjafatures, double kreditetfatures, double vlerapaguar, double vlerapaguarmonedhabaze, double vlerapaark, double kursiFatures, int idnivel, int idopsionepagese, string nrtel, string muaji, string kodi, double vlerafillestare, double vlerambetur, string statusFature, bool meKursFature)
        {
            lloji = llojisubjektit;
            idSubjekti = idsubjekti;
            pershkrimiTrupi = pershkrimitrupi;
            debiKredi = debikredi;
            idFatura = idfatura;
            zbritja = zbritjafatures;
            kreditet = kreditetfatures;
            vleraPaguar = vlerapaguar;
            vleraPaguarMonedhaBaze = vlerapaguarmonedhabaze;
            vleraPaArketueshme = vlerapaark;
            kmk = kursiFatures;
            idNivel = idnivel;
            idOpsionePagese = idopsionepagese;
            nrTel = nrtel;
            this._muaji = muaji;
            this.kodi = kodi;
            this.vleraFillestare = vlerafillestare;
            this.vleraMbetur = vlerambetur;
            this.statusFature = statusFature;
            this.meKursFature = meKursFature;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsVeprimBankaTrupi()
        {
        }

        public clsVeprimBankaTrupi(DataRow rreshti)
        {            
            mbushVeprimBankaTrupi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit te ciles i perket ky trupi ne te cilin ndodhet ky objekt
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }
        /// <summary>
        /// id e opsioneve te pageses
        /// </summary>
        public int IdOpsionePagese
        {
            get
            {
                return idOpsionePagese;
            }
            set
            {
                idOpsionePagese = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e subjektit te zgjedhur sipas llojit(llogari, klient apo furnitor).
        /// </summary>
        public int IdSubjekti
        {
            get { return idSubjekti; }
            set { idSubjekti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e fatures nese eshte zgjedhur nje e tille.
        /// </summary>
        public int IdFatura
        {
            get { return idFatura; }
            set { idFatura = value; }
        }

        public string Kodi
        {
            get
            {
                return kodi;
            }
            set
            {
                kodi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos llojin e subjektit (llogari, klient apo furnitor).
        /// </summary>
        public String Lloji
        {
            get { return lloji; }
            set { lloji = value; }
        }

        /// <summary>
        /// Kthen/Vendos debi ose kredi.
        /// </summary>
        public String DebiKredi
        {
            get { return debiKredi; }
            set { debiKredi = value; }
        }

        public string Muaji
        {
            get
            {
                return _muaji;
            }
            set
            {
                _muaji = value;
            }
        }
        // nr i telefonit
        public string NrTel
        {
            get
            {
                return nrTel;
            }
            set
            {
                nrTel = value;
            }
        }

        public string OpsionePagese
        {
            get
            {
                return opsionePagese;
            }
            set
            {
                opsionePagese = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin per kete objekt te trupit.
        /// </summary>
        public String PershkrimiTrupi
        {
            get { return pershkrimiTrupi; }
            set { pershkrimiTrupi = value; }
        }

        public double VleraFillestare
        {
            get
            {
                return vleraFillestare;
            }
            set
            {
                vleraFillestare = value;
            }
        }
        public double VleraMbetur
        {
            get
            {
                return vleraMbetur;
            }
            set
            {
                vleraMbetur = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos zbritjen.
        /// </summary>
        public double Zbritja
        {
            get { return zbritja; }
            set { zbritja = value; }
        }

        /// <summary>
        /// Kthen/Vendos kreditet.
        /// </summary>
        public double Kreditet
        {
            get { return kreditet; }
            set { kreditet = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren e paguar.
        /// </summary>
        public double VleraPaguar
        {
            get { return vleraPaguar; }
            set { vleraPaguar = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren e paguar te konvertuar ne monedhe baze.
        /// </summary>
        public double VleraPaguarMonedhaBaze
        {
            get { return vleraPaguarMonedhaBaze; }
            set { vleraPaguarMonedhaBaze = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren e paarkeuar.
        /// </summary>
        public double VleraPaArketueshme
        {
            get { return vleraPaArketueshme; }
            set { vleraPaArketueshme = value; }
        }

        /// <summary>
        /// Kthen/Vendos kmk = kursi i fatures ne daten e dokumentit te bankes
        /// Kur modifikohet dokumenti si kmk per llogaritjen e diferencave nga kursi merret kmk-ja sa ishte kur u ruajt dokumenti pavaresisht si mund te kete ndryshuar kursi i fatures pas shtimit te dokumentit
        /// </summary>
        public double KMK
        {
            get { return kmk; }
            set { kmk = value; }
        }
        public int IdNivel
        {
            get
            {
                return idNivel;
            }
            set
            {
                idNivel = value;
            }
        }
        public bool MeKursFature { get { return meKursFature; } set { meKursFature = value; } }

        public string StatusFature
        {
            get { return statusFature; }
            set { statusFature = value; }
        }

        #endregion

        #region Metoda Publike
        public double percaktoKurs(int idMonNderm, int idMonArkaBanka, int idMonSubjekt, double kursiKoka, DateTime date, int idKonfigurim, int idndermarrje)
        {
            if (idMonNderm == idMonSubjekt)
                return 1;
            else if (idMonNderm != idMonArkaBanka && idMonArkaBanka == idMonSubjekt)
                return kursiKoka;
            else
            {
                double kursiFundit = (new DbAdmin.clsKurset(idMonSubjekt, date, idKonfigurim, idndermarrje)).VleraKursi;
                return kursiFundit;
            }
        }

        public clsMesazh validoSubjektArkaBankaNgaImporti(int idNdermarrje, string lloji, string subjekti, int idSubjekti, bool eshteAktiv, int monNdermarrje, int monDokumenti, ref int monSubjekti, string monSubjektPershkim, int idLlogSubjekti, bool llogariSubjektiAktive)
        {
            if (idSubjekti <= 0)
            {
                if (lloji == "Llogari")
                    if (!DbKontabiliteti.clsLlogari.ekzistonLlogari(subjekti, idNdermarrje))
                        return new clsMesazh(false, String.Format("{0} {1} nuk ekziston!", lloji, subjekti));

                if (lloji == "Klient" || lloji == "Furnitor")
                    if (!DbKontabiliteti.clsKlientFurnitor.EkzistonKlientFurnitor(subjekti, idNdermarrje))
                        return new clsMesazh(false, String.Format("{0} {1} nuk ekziston!", lloji, subjekti));
                if (lloji == "Punonjes")
                    if (!clsPunonjes.ekzistonPunonjes(subjekti, idNdermarrje))
                        return new clsMesazh(false, String.Format("{0} {1} nuk ekziston!", lloji, subjekti));
                return new clsMesazh(false, "Ju nuk keni autorizime për këtë " + lloji + "!");
            }

            if (!eshteAktiv)
                return new clsMesazh(false, String.Format("{0}: {1} nuk eshte aktiv!", lloji, subjekti));

            if (lloji == "Klient" || lloji == "Furnitor")
            {
                DbAdmin.clsMonedha monSubjekt = new DbAdmin.clsMonedha();
                monSubjekt.mbushMonedhePershk(monSubjektPershkim, idNdermarrje);
                monSubjekti = monSubjekt.IdMonedha;
            }
            if (lloji == "Punonjes")
            {
                if (idLlogSubjekti == 0)
                    return new clsMesazh(false, String.Format("Punonjesi {0} nuk ka llogari page!", subjekti));
                if(!llogariSubjektiAktive)
                    return new clsMesazh(false, String.Format("Punonjesi {0} nuk ka llogari page aktive!", subjekti));
            }
            if (monNdermarrje != monDokumenti && monNdermarrje != monSubjekti && monDokumenti != monSubjekti)
                return new clsMesazh(false, String.Format("Nuk mund të kryeni veprime me {0} në monedhë të ndryshme nga monedha e arka/bankes dhe monedha bazë!", lloji));

            return new clsMesazh(true, "Subjekti u validua me sukses!");
        }

        public clsMesazh krijoTrupArkaBankaNgaImporti(string llojiSubjektit, string subjekti, string fatura, string llojDokFature, string pershkrimi, double vlera, double vleraMonBaze, string debiKredi, DateTime dtFature, ref object[] nivele, int idNdermarrje, DateTime data, int idPerdoruesi, DbAdmin.clsMonedha monNderm, DbAdmin.clsMonedha monBanka, int llojKursi, int indeksTrupi, double kursiKoka, int idKonfigAmbjenti, bool merrKursFature)
        {
            // validime te pergjithshme
            if (llojiSubjektit != "Llogari" && llojiSubjektit != "Furnitor" && llojiSubjektit != "Klient" && llojiSubjektit != "Punonjes")
                return new clsMesazh(false, "Lloji i subjektit " + llojiSubjektit + " nuk eshte i sakte!");
            if (debiKredi != "Debi" && debiKredi != "Kredi")
                return new clsMesazh(false, "Debi/Kredi nuk eshte e sakte!");
            if ((llojDokFature != "" && (fatura == "" || dtFature == DateTime.MinValue)) || (fatura != "" && (llojDokFature == "" || dtFature == DateTime.MinValue)))
                return new clsMesazh(false, "Duhet te plotesoni llojin, numrin dhe daten e dokumentit lidhes!");

            //Vlera default per rreshtat . Nderkohe qe fushat (_muaji, kodi, vleraFillestare, vleraMbetur) perdoren vetem tek vodafone dhe aktualisht nuk perdoren ne importe keto fusha pasi vijne nga integrimi
            _muaji = "";
            kodi = "";
            vleraFillestare = 0;
            vleraMbetur = 0;

            this.debiKredi = debiKredi;
            this.pershkrimiTrupi = pershkrimi;
            this.nrTel = "";
            this.opsionePagese = "";

            this.IdFatura = 0;
            this.kmk = 1;
            this.lloji = llojiSubjektit;
            int monSubjekti = 0;
            clsMesazh mesazh = new clsMesazh();
            switch (llojiSubjektit)
            {
                case "Llogari":
                    DbKontabiliteti.clsLlogari llog = new DbKontabiliteti.clsLlogari(subjekti, idNdermarrje);
                    monSubjekti = llog.IdMonedha;

                    mesazh = validoSubjektArkaBankaNgaImporti(idNdermarrje, llojiSubjektit, subjekti, llog.IdLlogari, llog.Aktiv, monNderm.IdMonedha, monBanka.IdMonedha, ref monSubjekti, "", 0, true);
                    if (!mesazh.Status)
                        return mesazh;

                    this.IdSubjekti = llog.IdLlogari;
                    this.kmk = percaktoKurs(monNderm.IdMonedha, monBanka.IdMonedha, llog.IdMonedha, kursiKoka, data, idKonfigAmbjenti, idNdermarrje);
                    break;
                case "Klient":
                case "Furnitor":
                    DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor();
                    kf.mbushKlientFurnitorSipasKodit(subjekti, idNdermarrje, idPerdoruesi);
                    monSubjekti = 0;

                    mesazh = validoSubjektArkaBankaNgaImporti(idNdermarrje, llojiSubjektit, subjekti, kf.IdKlientFurnitor, kf.AktivKF, monNderm.IdMonedha, monBanka.IdMonedha, ref monSubjekti, kf.Monedha, 0, true);
                    if (!mesazh.Status)
                        return mesazh;

                    this.IdSubjekti = kf.IdKlientFurnitor;

                    if (llojDokFature != "")
                    {
                        DbCore.DbShare.clsKonfigurimAmbjenti konfigFature = new DbShare.clsKonfigurimAmbjenti();
                        konfigFature.mbushKonfigAmbjSipasKod(llojDokFature, idNdermarrje);
                        if (konfigFature.IdKonfigAmbjente == 0)
                            return new clsMesazh(false, "Lloji i dokumentit te fatures nuk ekziston!");

                        string kodNivelFature = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheKodNivelRegjistrimi(konfigFature.IdNivel);
                        if (kodNivelFature != "FSH" && kodNivelFature != "FB" && kodNivelFature != "ND" && kodNivelFature != "VKF")
                            return new clsMesazh(false, "Kjo kategori dokumenti nuk mund te likuidohet!");

                        nivele[indeksTrupi] = konfigFature.IdNivel;

                        if (konfigFature.IdKategori == 1 || konfigFature.IdKategori == 2)
                        {
                            DbCore.DbRegjistrim.clsKokaShitje kokaShitje = new DbCore.DbRegjistrim.clsKokaShitje();
                            kokaShitje.mbushKokaShitjeSipasIdKonfigAmbNrDokDtDok(konfigFature.IdKonfigAmbjente, fatura, dtFature);
                            if (kokaShitje.IdShitjeKoka <= 0)
                                return new clsMesazh(false, String.Format("Fatura me numer {0} nuk ekziston!", fatura));
                            if (kokaShitje.IdKlientFurnitor != idSubjekti)
                                return new clsMesazh(false, String.Format("Fatura me numer {0} e dates {1} nuk i perket klientit me kod {2}! ", kokaShitje.NrDok, kokaShitje.DtDok.Date.ToString("d"), subjekti));
                            if (kokaShitje.IdStatusDok == 0)
                                return new clsMesazh(false, String.Format("Fatura me numer {0} eshte me status Draft!", kokaShitje.NrDok));

                            double vleraMbetur = DbCore.DbRegjistrim.clsKokaShitje.ktheVlereMbeturPerDok(kokaShitje.IdShitjeKoka, idNdermarrje, konfigFature.IdKategori);
                            if (vleraMbetur == 0)
                                return new clsMesazh(false, String.Format("Fatura me numer {0} eshte e likuiduar!", kokaShitje.NrDok));

                            this.IdFatura = kokaShitje.IdShitjeKoka;
                            this.idNivel = kokaShitje.IdNivel;

                            if (merrKursFature && monNderm.IdMonedha == monBanka.IdMonedha && monBanka.IdMonedha != kokaShitje.IdMonedha)
                            {
                                clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
                                DataTable dt = db.merrKursFatureOseAzhornimiPerIdDok(kokaShitje.IdShitjeKoka.ToString());
                                dt.Dispose();
                                this.kmk = Convert.ToDouble(dt.Rows[0]["Kursi"].ToString());
                            }
                            else
                                this.kmk = percaktoKurs(monNderm.IdMonedha, monBanka.IdMonedha, kokaShitje.IdMonedha, kursiKoka, data, idKonfigAmbjenti, idNdermarrje);
                        }
                        else if (konfigFature.IdKategori == 20)
                        {
                            DbCore.DbRegjistrim.clsVeprimeKFKoka vep = new DbCore.DbRegjistrim.clsVeprimeKFKoka();
                            vep.MerrVeprimeKfKokaSipasIdKonfigAmbjenteNrDokDtDok(konfigFature.IdKonfigAmbjente, fatura, dtFature);
                            if (vep.IdVeprimeKFKoka == 0)
                                return new clsMesazh(false, String.Format("Fatura me numer {0} nuk ekziston!", fatura));

                            double vleraMbetur = DbCore.DbRegjistrim.clsKokaShitje.ktheVlereMbeturPerDok(vep.IdVeprimeKFKoka, idNdermarrje, konfigFature.IdKategori);
                            if (vleraMbetur == 0)
                                return new clsMesazh(false, String.Format("Fatura me numer {0} eshte e likuiduar!", vep.NrDok));

                            this.IdFatura = vep.IdVeprimeKFKoka;
                            this.idNivel = vep.IdNivel;
                            this.kmk = percaktoKurs(monNderm.IdMonedha, monBanka.IdMonedha, vep.IdMonedha, kursiKoka, data, idKonfigAmbjenti, idNdermarrje);
                        }
                    }
                    else
                    {
                        nivele[indeksTrupi] = 0;
                        this.kmk = percaktoKurs(monNderm.IdMonedha, monBanka.IdMonedha, monSubjekti, kursiKoka, data, idKonfigAmbjenti, idNdermarrje);
                    }
                    break;
                case "Punonjes":
                    clsPunonjes punonjes = new clsPunonjes(subjekti, idNdermarrje);

                    DbKontabiliteti.clsLlogari llogariPunonjesi = new DbKontabiliteti.clsLlogari();
                    if (punonjes.IdLlogari > 0)
                    {
                        llogariPunonjesi = new DbKontabiliteti.clsLlogari(punonjes.IdLlogari);
                        monSubjekti = llogariPunonjesi.IdMonedha;
                    }
                    mesazh = validoSubjektArkaBankaNgaImporti(idNdermarrje, llojiSubjektit, subjekti, punonjes.IdPunonjes, punonjes.Aktiv, monNderm.IdMonedha, monBanka.IdMonedha, ref monSubjekti, "", llogariPunonjesi.IdLlogari, llogariPunonjesi.Aktiv);

                    if (!mesazh.Status)
                        return mesazh;

                    this.IdSubjekti = punonjes.IdPunonjes;
                    this.kmk = percaktoKurs(monNderm.IdMonedha, monBanka.IdMonedha, llogariPunonjesi.IdMonedha, kursiKoka, data, idKonfigAmbjenti, idNdermarrje);
                    break;
                default:
                    return new clsMesazh(false, "Lloji i subjektit " + llojiSubjektit + " nuk eshte i sakte!");
            }

            this.vleraPaguar = vlera;
            this.vleraPaguarMonedhaBaze = vlera * kursiKoka;
            this.vleraPaArketueshme = vlera;
            return new clsMesazh(true, "Trupi u krijua me sukses!");
        }



        #endregion

        #region Metoda Internal

        internal bool mbushVeprimBankaTrupi(DataRow dbDataRowVeprime)
        {
            if (dbDataRowVeprime != null)
            {
                try
                {
                    int.TryParse(dbDataRowVeprime["IDTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowVeprime["IDKOKA"].ToString(), out idKoka);
                    lloji = dbDataRowVeprime["LLOJI"].ToString();
                    int.TryParse(dbDataRowVeprime["IDSUBJEKTI"].ToString(), out idSubjekti);
                    pershkrimiTrupi = dbDataRowVeprime["PERSHKRIMITRUPI"].ToString();
                    debiKredi = dbDataRowVeprime["DEBIKREDI"].ToString();
                    opsionePagese = dbDataRowVeprime["OpsionePagese"].ToString();
                    int.TryParse(dbDataRowVeprime["IDFATURA"].ToString(), out idFatura);
                    double.TryParse(dbDataRowVeprime["ZBRITJA"].ToString(), out zbritja);
                    double.TryParse(dbDataRowVeprime["KREDITET"].ToString(), out kreditet);
                    double.TryParse(dbDataRowVeprime["VLERAPAGUAR"].ToString(), out vleraPaguar);
                    double.TryParse(dbDataRowVeprime["VLERAPAGUARMONEDHABAZE"].ToString(), out vleraPaguarMonedhaBaze);
                    double.TryParse(dbDataRowVeprime["VLERAPAARKETUESHME"].ToString(), out vleraPaArketueshme);
                    double.TryParse(dbDataRowVeprime["KMK"].ToString(), out kmk);
                    int.TryParse(dbDataRowVeprime["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(dbDataRowVeprime["IDOPSIONEPAGESE"].ToString(), out idOpsionePagese);
                    nrTel = dbDataRowVeprime["NRTEL"].ToString();
                    _muaji = dbDataRowVeprime["MUAJI"].ToString();
                    kodi = dbDataRowVeprime["KODI"].ToString();
                    statusFature = dbDataRowVeprime["STATUSFATURE"].ToString();
                    double.TryParse(dbDataRowVeprime["VLERAFILLESTARE"].ToString(), out vleraFillestare);
                    double.TryParse(dbDataRowVeprime["VLERAMBETUR"].ToString(), out vleraMbetur);
                    bool.TryParse(dbDataRowVeprime["MEKURSFATURE"].ToString(), out meKursFature);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te veprimit banka nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}

