using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Script.Serialization;

namespace DbCore.DbAdmin
{
    public class colTeDrejtaRoli : List<clsTeDrejtaRoli>
    {
        public static string keyFieldName = "idDrejta";
        public static string parentFieldName = "idPrindi";
        public static int rootPrindi = 0;
        //TODO GETSON per tu pastruar
        #region Konstruktoret

        /// <summary>
        /// konstruktori bosh
        /// </summary>
        public colTeDrejtaRoli()
        {

        }
        #endregion

        #region Metoda Publike
        /// <summary>
        /// get dhe set sipas indexit te dhene te nje elementi te colection-it
        /// </summary>
        /// <param name="index">indeksi i dhene qe duhet te jete me i madh baraz se 0 dhe me i vogel se list.Count</param>
        /// <returns>elementin ne rastin kur eshte get</returns>
        public new clsTeDrejtaRoli this[int index]
        {
            get { return ((clsTeDrejtaRoli)base[index]); }
        }

        public static DataTable merrTeDrejtaRoliMeEmraKomponentesh(int idperdoruesi, int idNdermarrje, int idviti)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                DataTable dt = db.merrTeDrejtaMeEmraKomponentesh(idperdoruesi, idNdermarrje, idviti);
                return dt;
            }
        }

        public static DataTable merrTeDrejtaRoliDheRaporteshMeEmraKomponentesh(int idperdoruesi, int idNdermarrje, int idviti)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                DataTable dt = db.merrTeDrejtaRoliDheRaporteshMeEmraKomponentesh(idperdoruesi, idNdermarrje, idviti);
                return dt;
            }
        }
        
        public static DataTable mbushTeDrejtaRoliPerCRM(int idperdoruesi, int idNdermarrje, int idviti)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            DataTable dt = db.merrTeDrejtaRoliPerCRM(idperdoruesi, idNdermarrje, idviti);
            db.Dispose();
            return dt;
        }

        public clsMesazh mbushTeDrejtaRoliPerGIS(int idperdoruesi, int idNdermarrje, int idviti)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            DataTable dt = db.merrTeDrejtaRoliPerGIS(idperdoruesi, idNdermarrje, idviti);
            if (!mbushTeDrejtaPerTreeGride(dt))
            {
                db.Dispose();
                return new clsMesazh(false, "ERROR: Gabim gjate mbushjes se colectionit nga db-ja");
            }
            db.Dispose();
            return new clsMesazh(true, "Mbushja koleksionit u krye me suskses");
        }

        public clsMesazh mbushTeDrejtaPerdoruesi(int idPerdoruesi, int idNdermarrje, int idViti)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            DataTable dt = db.merrTeDrejtaPerdoruesi(idPerdoruesi, idNdermarrje, idViti);
            if (!mbushTeDrejtat(dt))
            {
                db.Dispose();
                return new clsMesazh(false, "ERROR: Gabim gjate mbushjes se colectionit nga db-ja");
            }
            db.Dispose();
            return new clsMesazh(true, "Mbushja koleksionit u krye me suskses");
        }

        public clsMesazh mbushTeDrejtaPerdoruesiNiveleRegjistrimiPerKomponente(int idPerdoruesi, int idNdermarrje, int idViti, string komponente)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            DataTable dt = db.merrTeDrejtaPerdoruesiNiveleRegjistrimiPerKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
            if (!mbushTeDrejtaPerTreeGride(dt))
            {
                db.Dispose();
                return new clsMesazh(false, "ERROR: Gabim gjate mbushjes se colectionit nga db-ja");
            }
            db.Dispose();
            return new clsMesazh(true, "Mbushja koleksionit u krye me suskses");
        }

        public static DataTable merrTeDrejtatPerKompRaportesh(int roli, int idNdermarrje, int idviti, clsDatabaseAdmin db)
        {
            //if (db == null)
            //    db = new clsDatabaseAdmin();
            DataTable dt = db.merrTeDrejtatPerKompRaportesh(roli, idNdermarrje, idviti);
            return dt;
        }

        public bool kaTeDrejta(int idKomponente)
        {
            clsTeDrejtaRoli eDrejta = this.Find(idKomponente);
            if (eDrejta != null)
                return eDrejta.DAmb;
            return false;
        }
        
        public clsMesazh klonoTeDrejtaMeRaport(int idRoli, int idndermarrje, int idviti, int idndermarjedest, int idvitidest)
        {
            clsTeDrejtaRoliKoka klonoTeDrejta = new clsTeDrejtaRoliKoka();
            clsMesazh mesazh = new clsMesazh();
            mesazh = klonoTeDrejta.klonoTeDrejtatNgaNjeNdermarrjeDest(idRoli, idndermarrje, idviti, idndermarjedest, idvitidest);
            return mesazh;          
        }

        public colTeDrejtaRoli krijoPemePerTreeGrid(int idGjuha, int roli, int idNdermarrje, int idviti, int idllojlicence)
        {
            if (roli == 0 || idNdermarrje == 0 || idviti == 0)
                return null;
            if (this.Count == 0)
            {
                clsDatabaseAdmin db = new clsDatabaseAdmin();
                DataTable dt = db.merrTeDrejtaMeRaporte(roli, idNdermarrje, idviti, idGjuha);
                if (!this.mbushTeDrejtaPerTreeGride(dt))
                {
                    db.Dispose();
                    throw new Exception("ERROR: Gabim gjate mbushjes se colectionit nga db-ja");
                }
                db.Dispose();
            }
            if (this.Count == 0)
                return null;
            int maxIdDretja = 0;

            foreach (clsTeDrejtaRoli edrejte in this)
            {
                if (edrejte.Permbledhes != Permbledhes.Jo)
                    return this; //rreshtat permbledhes ekzistojne nuk ka nevoj te shtohen, pema kthehet si eshte
                if (edrejte.IdPeme > maxIdDretja)
                    maxIdDretja = edrejte.IdPeme;
            }
            colModulet modulet = new colModulet();
            modulet.mbushModulet(idGjuha);
            colTeDrejtaRoli teDrejtat = new colTeDrejtaRoli();
            foreach (clsModuli moduli in modulet)
            {
                colTeDrejtaRoli teDrejtaModuli = this.filtroTeDrejtaRoliModuliPaRaporte(moduli.IdModuli);
                if (teDrejtaModuli.Count == 0)
                    continue; //nese lista eshte bosh do te thote qe ky modul eshte krijuar me vone 
                colKomponentet komponenteAmbjente = new colKomponentet(moduli.IdModuli, idllojlicence);
                if (komponenteAmbjente.Count == 0)
                {
                    continue;// ka klasa qe jane shtuar me mbrapa
                }
                int permbledhesModuli = teDrejtat.Count;
                int idPemeGjeneruar = ++maxIdDretja;
                teDrejtat.Add(new clsTeDrejtaRoli(idPemeGjeneruar, null, teDrejtaModuli[0].IdNdermarrje, teDrejtaModuli[0].IdViti, teDrejtaModuli[0].IdModul,
                    teDrejtaModuli[0].TextModuli, 0, "", teDrejtaModuli[0].IdRoli, true, true, true, true, Permbledhes.Moduli, idllojlicence, true, true, true, true, true, false, 0, true, idPemeGjeneruar, -1, null, false, false, true, true, true, true, true, true, true, true, true, teDrejtaModuli[0].IdDrejtaKoka, teDrejtaModuli[0].IdLayer, -1, -1));

               
                foreach (clsKomponente komponente in komponenteAmbjente)
                {
                    if ((moduli.IdModuli == 8 && komponente.EmriKomponente.IndexOf("?idmod=") != -1) || komponente.IdKomponente == 708)
                     {
                        clsTeDrejtaRoli edrejta = teDrejtaModuli.filtroTeDrejtaRoli(moduli.IdModuli, komponente.IdKomponente);
                        if (edrejta == null)
                            continue;
                        edrejta.EshteGjethe = false;
                        edrejta.IdPrindi = idPemeGjeneruar;
                        edrejta.IdPrindPeme = idPemeGjeneruar;
                        edrejta.DPlot = edrejta.DAmb && edrejta.DFsh && edrejta.DMod && edrejta.DShtim && edrejta.DGjitheDok && edrejta.DShtimDraft && edrejta.DModifikimDraft && edrejta.DKerko && edrejta.DEksporto && edrejta.DPrinto && edrejta.DArkiva && edrejta.DKonverto && edrejta.DPezullo && edrejta.DAutoKonverto;
                        if (edrejta != null)
                        {
                            if (!edrejta.DAmb)
                                teDrejtat[permbledhesModuli].DAmb = false;
                            if (!edrejta.DFsh)
                                teDrejtat[permbledhesModuli].DFsh = false;
                            if (!edrejta.DMod)
                                teDrejtat[permbledhesModuli].DMod = false;
                            if (!edrejta.DShtim)
                                teDrejtat[permbledhesModuli].DShtim = false;
                            if (!edrejta.DPlot)
                                teDrejtat[permbledhesModuli].DPlot = false;
                            if (!edrejta.DGjitheDok)
                                teDrejtat[permbledhesModuli].DGjitheDok = false;
                            if (!edrejta.DShtimDraft)
                                teDrejtat[permbledhesModuli].DShtimDraft = false;
                            if (!edrejta.DModifikimDraft)
                                teDrejtat[permbledhesModuli].DModifikimDraft = false;
                            if (!edrejta.DKerko)
                                teDrejtat[permbledhesModuli].DKerko = false;
                            if (!edrejta.DEksporto)
                                teDrejtat[permbledhesModuli].DEksporto = false;
                            if (!edrejta.DPrinto)
                                teDrejtat[permbledhesModuli].DPrinto = false;
                            if (!edrejta.DArkiva)
                                teDrejtat[permbledhesModuli].DArkiva = false;
                            if (!edrejta.DKonverto)
                                teDrejtat[permbledhesModuli].DKonverto = false;
                            if (!edrejta.DPezullo)
                                teDrejtat[permbledhesModuli].DPezullo = false;
                            if (!edrejta.DAutoKonverto)
                                teDrejtat[permbledhesModuli].DAutoKonverto = false;
                            teDrejtat[permbledhesModuli].IdLlojLicence = idllojlicence;
                            edrejta.IdLlojLicence = idllojlicence;
                        }
                        colTeDrejtaRoli teDrejtaRap = this.filtroTeDrejtaRoliSipasPrindit(edrejta.IdDrejta);
                        foreach (clsTeDrejtaRoli dr in teDrejtaRap)
                        {
                            if (komponente.EmriKomponente.IndexOf("?idmod=19") != -1 || komponente.IdKomponente == 708)
                                dr.Tipi = 1;
                            else
                                dr.Tipi = 2;
                            dr.IdPrindPeme = edrejta.IdPeme;
                            dr.IdLlojLicence = idllojlicence;
                            dr.DPlot = dr.DAmb && dr.DFsh && dr.DMod && dr.DShtim && dr.DGjitheDok && dr.DShtimDraft && dr.DModifikimDraft && dr.DKerko && dr.DEksporto && dr.DPrinto && dr.DArkiva && dr.DKonverto && dr.DPezullo && dr.DAutoKonverto;
                        }
                        teDrejtat.Add(edrejta);
                        teDrejtat.AddRange(teDrejtaRap);
                    }
                    else if((moduli.IdModuli == 12 && (komponente.IdKomponente == 506 || komponente.IdKomponente == 505)) 
                            || (moduli.IdModuli == 13 && (komponente.IdKomponente == 508 || komponente.IdKomponente == 507)) 
                            || (moduli.IdModuli == 1 && (komponente.IdKomponente == 176 || komponente.IdKomponente == 177 
                            || komponente.IdKomponente == 175 || komponente.IdKomponente == 174))
                            || (moduli.IdModuli == 57 && (komponente.IdKomponente == 3071 || komponente.IdKomponente == 3072 
                            || komponente.IdKomponente == 3073 || komponente.IdKomponente == 3074
                            || komponente.IdKomponente == 3075 || komponente.IdKomponente == 3076 || komponente.IdKomponente == 3077 || komponente.IdKomponente == 3078)))
                    {
                        clsTeDrejtaRoli edrejta = teDrejtaModuli.filtroTeDrejtaRoli(moduli.IdModuli, komponente.IdKomponente);
                        if (edrejta == null)
                            continue;
                        edrejta.Tipi = komponente.Tipi;
                        edrejta.EshteGjethe = false;
                        edrejta.IdPrindi = idPemeGjeneruar;
                        edrejta.IdPrindPeme = idPemeGjeneruar;
                        edrejta.DPlot = edrejta.DAmb && edrejta.DFsh && edrejta.DMod && edrejta.DShtim && edrejta.DGjitheDok && edrejta.DShtimDraft && edrejta.DModifikimDraft && edrejta.DKerko && edrejta.DEksporto && edrejta.DPrinto && edrejta.DArkiva && edrejta.DKonverto && edrejta.DPezullo;
                        if (edrejta != null)
                        {
                            if (!edrejta.DAmb)
                                teDrejtat[permbledhesModuli].DAmb = false;
                            if (!edrejta.DFsh)
                                teDrejtat[permbledhesModuli].DFsh = false;
                            if (!edrejta.DMod)
                                teDrejtat[permbledhesModuli].DMod = false;
                            if (!edrejta.DShtim)
                                teDrejtat[permbledhesModuli].DShtim = false;
                            if (!edrejta.DPlot)
                                teDrejtat[permbledhesModuli].DPlot = false;
                            if (!edrejta.DGjitheDok)
                                teDrejtat[permbledhesModuli].DGjitheDok = false;
                            if (!edrejta.DShtimDraft)
                                teDrejtat[permbledhesModuli].DShtimDraft = false;
                            if (!edrejta.DModifikimDraft)
                                teDrejtat[permbledhesModuli].DModifikimDraft = false;
                            if (!edrejta.DKerko)
                                teDrejtat[permbledhesModuli].DKerko = false;
                            if (!edrejta.DEksporto)
                                teDrejtat[permbledhesModuli].DEksporto = false;
                            if (!edrejta.DPrinto)
                                teDrejtat[permbledhesModuli].DPrinto = false;
                            if (!edrejta.DArkiva)
                                teDrejtat[permbledhesModuli].DArkiva = false;
                            if (!edrejta.DKonverto)
                                teDrejtat[permbledhesModuli].DKonverto = false;
                            if (!edrejta.DPezullo)
                                teDrejtat[permbledhesModuli].DPezullo = false;
                            teDrejtat[permbledhesModuli].IdLlojLicence = idllojlicence;
                            edrejta.IdLlojLicence = idllojlicence;
                        }
                        colTeDrejtaRoli teDrejtaRap = this.filtroTeDrejtaRoliSipasPrindit(edrejta.IdDrejta);
                        foreach (clsTeDrejtaRoli dr in teDrejtaRap)
                        {
                            dr.Tipi = edrejta.Tipi;
                            dr.IdPrindPeme = edrejta.IdPeme;
                            dr.IdLlojLicence = idllojlicence;
                            dr.DPlot = dr.DAmb && dr.DFsh && dr.DMod && dr.DShtim && dr.DGjitheDok && dr.DShtimDraft && dr.DModifikimDraft && dr.DKerko && dr.DEksporto && dr.DPrinto && dr.DArkiva && dr.DKonverto && dr.DPezullo;
                        }
                        teDrejtat.Add(edrejta);
                        teDrejtat.AddRange(teDrejtaRap);
                    }
                    else //per raportet
                    {
                        clsTeDrejtaRoli edrejta = teDrejtaModuli.filtroTeDrejtaRoli(moduli.IdModuli, komponente.IdKomponente);
                        if (edrejta != null)
                        {
                            edrejta.Tipi = komponente.Tipi;
                            edrejta.IdPrindi = idPemeGjeneruar;
                            edrejta.IdPrindPeme = idPemeGjeneruar;
                            if (!edrejta.DAmb)
                                teDrejtat[permbledhesModuli].DAmb = false;
                            if (!edrejta.DFsh)
                                teDrejtat[permbledhesModuli].DFsh = false;
                            if (!edrejta.DMod)
                                teDrejtat[permbledhesModuli].DMod = false;
                            if (!edrejta.DShtim)
                                teDrejtat[permbledhesModuli].DShtim = false;
                            if (!edrejta.DPlot)
                                teDrejtat[permbledhesModuli].DPlot = false;
                            if (!edrejta.DGjitheDok)
                                teDrejtat[permbledhesModuli].DGjitheDok = false;
                            if (!edrejta.DShtimDraft)
                                teDrejtat[permbledhesModuli].DShtimDraft = false;
                            if (!edrejta.DModifikimDraft)
                                teDrejtat[permbledhesModuli].DModifikimDraft = false;
                            if (!edrejta.DKerko)
                                teDrejtat[permbledhesModuli].DKerko = false;
                            if (!edrejta.DEksporto)
                                teDrejtat[permbledhesModuli].DEksporto = false;
                            if (!edrejta.DPrinto)
                                teDrejtat[permbledhesModuli].DPrinto = false;
                            if (!edrejta.DArkiva)
                                teDrejtat[permbledhesModuli].DArkiva = false;
                            if (!edrejta.DKonverto)
                                teDrejtat[permbledhesModuli].DKonverto = false;
                            if (!edrejta.DPezullo)
                                teDrejtat[permbledhesModuli].DPezullo = false;
                            if (!edrejta.DAutoKonverto)
                                teDrejtat[permbledhesModuli].DAutoKonverto = false;
                            teDrejtat[permbledhesModuli].IdLlojLicence = idllojlicence;
                            edrejta.IdLlojLicence = idllojlicence;
                            teDrejtat.Add(edrejta);
                        }
                    }
                }
            }
            return teDrejtat;
        }

        /// <summary>
        /// Kthen nen listen te krijuar nga colectioni duke filtruar me idmodul
        /// </summary>
        /// <param name="idModuli">id-ja e modulit qe do filtrohet</param>
        /// <returns>lista eshte nje liste e re e krijuar</returns>
        public colTeDrejtaRoli filtroTeDrejtaRoliModuli(int idModuli)
        {
            colTeDrejtaRoli teDrejtat = new colTeDrejtaRoli();
            foreach (clsTeDrejtaRoli eDrejte in this)
            {
                if (eDrejte.IdModul == idModuli)
                    teDrejtat.Add(eDrejte);
            }
            return teDrejtat;
        }

        /// <summary>
        /// Kthen nen listen te krijuar nga colectioni duke filtruar me idmodul
        /// </summary>
        /// <param name="idModuli">id-ja e modulit qe do filtrohet</param>
        /// <returns>lista eshte nje liste e re e krijuar</returns>
        public colTeDrejtaRoli filtroTeDrejtaRoliModuliPaRaporte(int idModuli)
        {
            colTeDrejtaRoli teDrejtat = new colTeDrejtaRoli();
            foreach (clsTeDrejtaRoli eDrejte in this)
            {
                if (eDrejte.IdModul == idModuli && eDrejte.IdRaport == -1)
                    teDrejtat.Add(eDrejte);
            }
            return teDrejtat;
        }
     
        public clsTeDrejtaRoli filtroTeDrejtaRoli(int idModuli, int idKomponente)
        {
            foreach (clsTeDrejtaRoli eDrejte in this)
                if (eDrejte.IdModul == idModuli && eDrejte.IdKomponente == idKomponente)
                    return eDrejte;
            return null; //nuk gjendet
        }

        public colTeDrejtaRoli filtroTeDrejtaRoliSipasPrindit(int idPrindi)
        {
            colTeDrejtaRoli teDrejtat = new colTeDrejtaRoli();
            foreach (clsTeDrejtaRoli eDrejte in this)
                if (eDrejte.IdPrindi == idPrindi)
                    teDrejtat.Add(eDrejte);
            return teDrejtat;
        }

        public clsTeDrejtaRoli Find(int idKomponente)
        {
            return this.Find(delegate(clsTeDrejtaRoli eDrejte)
                            {
                                return eDrejte.IdKomponente == idKomponente;
                            });
        }

        public bool merrTeDrejtatSipasRolitNdermVitit(int idRoli, int idNdermarrje, int idViti)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool sukses = mbushTeDrejtat(db.merrTeDrejta(idRoli, idNdermarrje, idViti));
            db.Dispose();
            return sukses;
        }

        /// <summary>
        /// konverton te gjithe klasen ne DataTable per insert/update qe te behet ne grup
        /// </summary>
        /// <param name="edrejtaRolKoka">klasa qe do te konvertohet</param>
        /// <returns>kthen nje datatable pas konvertimit</returns>
        public DataTable mbushTeDrejtatRolTrup()
        {
            if (this.Count == 0) return null;

            DataTable teDrejtatRolTrupi = krijoDataTableHeadersPerTeDrejtaRolTrupi();
            foreach (clsTeDrejtaRoli edrejtaRolTrupi in this)
            {
                teDrejtatRolTrupi.LoadDataRow(mbushTeDrejtenRolTrup(teDrejtatRolTrupi, edrejtaRolTrupi).ItemArray, false);
            }
            return teDrejtatRolTrupi;
        }
        
        public static DataTable merrVetemTeDrejtaAmbjete(int idperdoruesi, int idNdermarrje, int idviti)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                DataTable dt = db.merrVetemTeDrejtaAmbjete(idperdoruesi, idNdermarrje, idviti);
                return dt;
            }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Kjo metode perdoret nga vet klasa per te mbushur vet collection-in nga nje dataTable e dhene. 
        /// Kujdes collection-i mund te kete te dhena paraprake.
        /// </summary>
        /// <param name="dt">dataTable i te njejtes forme me colection-in</param>
        /// <returns>Kthen true nese eshte ekzekutuar me sukses, false nese ka ndodhur ndonje exception</returns>
        private bool mbushTeDrejtat(DataTable dt)
        {
           
                foreach (DataRow rreshti in dt.Rows)
                {
                    this.Add(new clsTeDrejtaRoli(rreshti));
                }
      
            return true;
        }

        /// <summary>
        /// Kjo metode perdoret nga vet klasa per te mbushur vet collection-in nga nje dataTable e dhene. 
        /// Kujdes collection-i mund te kete te dhena paraprake.
        /// </summary>
        /// <param name="dt">dataTable i te njejtes forme me colection-in</param>
        /// <returns>Kthen true nese eshte ekzekutuar me sukses, false nese ka ndodhur ndonje exception</returns>
        private bool mbushTeDrejtaPerTreeGride(DataTable dt)
        {
           
                foreach (DataRow rreshti in dt.Rows)
                {

                    clsTeDrejtaRoli eDrejta = new clsTeDrejtaRoli();
                    eDrejta.mbushTeDrejtePerTreeGride(rreshti);
                    this.Add(eDrejta);
                }
       
            return true;
        }

        /// <summary>
        /// krijon koken e DataTable ekuivalente te klases qe do te sherbeje per insert/update
        /// </summary>
        /// <returns>kthen koken e DataTable te krijuar</returns>
        private DataTable krijoDataTableHeadersPerTeDrejtaRolTrupi()
        {
            DataTable teDrejtatRolTrupi = new DataTable("teDrejtatRolTrupi");
            teDrejtatRolTrupi.Columns.Add("IDDREJTA", (new System.Decimal()).GetType());
            teDrejtatRolTrupi.Columns.Add("IDKOMPONENTE", (new System.Decimal()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_MOD", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_FSH", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_AMB", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_SHTIM", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_GJITHEDOK", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_SHTIMDRAFT", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_MODIFIKIMDRAFT", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("IDDREJTAKOKA", (new System.Decimal()).GetType());
            teDrejtatRolTrupi.Columns.Add("IDDREJTAPRIND", (new System.Decimal()).GetType());
            teDrejtatRolTrupi.Columns.Add("IDRAPORTI", (new System.Decimal()).GetType());
            teDrejtatRolTrupi.Columns.Add("IDNIVELREGJISTRIMI", (new System.Decimal()).GetType());
            teDrejtatRolTrupi.Columns.Add("IDKATEGORIA", (new System.Decimal()).GetType());
            teDrejtatRolTrupi.Columns.Add("TABI", (Type.GetType("System.String")));
            teDrejtatRolTrupi.Columns.Add("IDLAYER", (new System.Decimal()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_KERKO", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_EKSPORTO", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_PRINTO", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_ARKIVA", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_KONVERTO", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_PEZULLO", (new System.Boolean()).GetType());
            teDrejtatRolTrupi.Columns.Add("D_AUTOKONVERTO", (new System.Boolean()).GetType());
            return teDrejtatRolTrupi;
        }

        /// <summary>
        /// konverton nje objekt te klases ne formatin e nje DataRow qe do te sherbeje per veprimin e Insert/Update ne grup
        /// </summary>
        /// <param name="dataTableHeader">sherben per krijimin e rreshtit nga datable i krijuar per kete rast</param>
        /// <param name="edrejtaRolTrup">eshte objeti i klases qe do te konvertohet</param>
        /// <returns>kthen rreshtin qe perban klasen te konvertuar ne DataRow</returns>
        private DataRow mbushTeDrejtenRolTrup(DataTable dataTableHeader, clsTeDrejtaRoli edrejtaRolTrup)
        {
            DataRow rreshti = dataTableHeader.NewRow();
            //rreshti["IDNDERMARRJE"] = edrejtaRolTrup.IdNdermarrje;
            //rreshti["IDVITI"] = edrejtaRolTrup.IdViti;
            //rreshti["IDROLI"] = edrejtaRolTrup.IdRoli;
            rreshti["IDDREJTA"] = edrejtaRolTrup.IdDrejta;
            rreshti["IDKOMPONENTE"] = edrejtaRolTrup.IdKomponente;
            rreshti["D_MOD"] = edrejtaRolTrup.DMod;
            rreshti["D_FSH"] = edrejtaRolTrup.DFsh;
            rreshti["D_AMB"] = edrejtaRolTrup.DAmb;
            rreshti["D_SHTIM"] = edrejtaRolTrup.DShtim;
            rreshti["D_GJITHEDOK"] = edrejtaRolTrup.DGjitheDok;
            rreshti["D_SHTIMDRAFT"] = edrejtaRolTrup.DShtimDraft;
            rreshti["D_MODIFIKIMDRAFT"] = edrejtaRolTrup.DModifikimDraft;
            rreshti["IDDREJTAKOKA"] = edrejtaRolTrup.IdDrejtaKoka;
            rreshti["IDDREJTAPRIND"] = edrejtaRolTrup.IdDrejtaPrindi;
            rreshti["IDRAPORTI"] = edrejtaRolTrup.IdRaporti;
            rreshti["IDNIVELREGJISTRIMI"] = edrejtaRolTrup.IdNivelRegjistrimi;
            rreshti["IDKATEGORIA"] = edrejtaRolTrup.IdKategoria;
            rreshti["TABI"] = edrejtaRolTrup.Tabi;
            rreshti["IDLAYER"] = edrejtaRolTrup.IdLayer;
            rreshti["D_KERKO"] = edrejtaRolTrup.DKerko;
            rreshti["D_EKSPORTO"] = edrejtaRolTrup.DEksporto;
            rreshti["D_PRINTO"] = edrejtaRolTrup.DPrinto;
            rreshti["D_ARKIVA"] = edrejtaRolTrup.DArkiva;
            rreshti["D_KONVERTO"] = edrejtaRolTrup.DKonverto;
            rreshti["D_PEZULLO"] = edrejtaRolTrup.DPezullo;
            rreshti["D_AUTOKONVERTO"] = edrejtaRolTrup.DAutoKonverto;
            return rreshti;
        }

        #endregion

    }
}