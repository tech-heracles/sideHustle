using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace RestApi.WebAPI.Models
{
    class ShperndarjeShpenzimiRepository
    {

        internal static object KtheFaturaPerShperndarje(HttpSessionState session)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(session);
            int vitNdermarrje = DbCore.mySessionObjects.ktheNdermarrjeVit(session);
            DateTime ndermarrjeVitiFund = new clsNdermarrjeViti(vitNdermarrje).NdermarrjeVitiFund;
            DataTable dt = colDokumentat.mbushGjitheDokumentatRegjistrimDokumentashShperndarjeShpenzimesh(idNdermarrje, new DateTime().ToShortDateString(), ndermarrjeVitiFund.ToShortDateString());
            return dt;
        }
        public static object KtheFaturatFiltruaraShpenzimiObj(int id)
        {
            DataTable dokumenti = clsKokaMagazina.MerrDokumentMagazinePerShperndarjeShpenzimesh(id);
            clsMesazh faturaShperndare = KtheMsgFatureShperndare(id);
            if (dokumenti == null || dokumenti.Rows.Count <= 0)
            {
                return new { dokumenti = DBNull.Value, mesazhZhdoganuar = faturaShperndare };
            }

            object dokumentiObj = new
            {
                IdDokumenti = dokumenti.Rows[0]["IdDokumenti"],
                NrFature = dokumenti.Rows[0]["NrFature"],
                DtFature = dokumenti.Rows[0]["DtFature"],
                IdFurnitori = dokumenti.Rows[0]["IdKlientFurnitori"],
                Furnitori = dokumenti.Rows[0]["Furnitori"],
                IdTrupi = dokumenti.Rows[0]["IdTrupi"],
                IdKoka = dokumenti.Rows[0]["IdKoka"],
                Trupi = dokumenti
            };
            
            return new { dokumenti = dokumentiObj, mesazhZhdoganuar = faturaShperndare };
        }

        internal static object KtheACListeLlogarishSipasKlases(string infixText, int pershk, string klasa, HttpSessionState session)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(session);
            DataTable tmpTable = colLlogarite.merrLLogariteLikeKodOsePershkDT(idNdermarrje, idPerdoruesi, infixText, pershk, klasa);
            tmpTable.Columns["des"].ColumnName = "desc";
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
                autoCompleteItem[i].objekti = new { monedha = tmpTable.Rows[i]["MONEDHAKOD"].ToString() };
            }
            return autoCompleteItem;
        }

        internal static clsMesazh KtheMsgFatureShperndare(int idKokaMagazina)
        {
            try
            {
                clsShperndarjeShpenzimeTrupi trup = new clsShperndarjeShpenzimeTrupi();
                bool eShperndare = trup.eshteZShperndareFatura(idKokaMagazina);
                if (eShperndare)
                {
                    clsKokaMagazina koka = new clsKokaMagazina();
                    koka.mbushKokaMagazinaSipasID(idKokaMagazina);
                    if (koka.IdKokaMagazina == 0)
                        throw new Exception();
                    return new clsMesazh(true, "Fatura " + koka.NrDok + " eshte e shperndare!");
                }
            }
            catch (Exception err)
            {
                string mesazhi = "Gabim gjate kontrollit te fatures!";
                ImbLogger.Error(mesazhi + err.Message);
                return new clsMesazh(false, mesazhi);
            }
            return new clsMesazh(true, "");
        }

        internal static object FshiDokumentShperndarjeShpenzimi(int idDokumenti, bool kontrolloRivleresim, HttpSessionState Session)
        {
            colTrupiMagazina tr = new colTrupiMagazina();
            colTrupiMagazina trupat = new colTrupiMagazina();
            bool rivleresim = false;
            clsShperndarjeShpenzimeKoka clsKoka = new clsShperndarjeShpenzimeKoka(idDokumenti);
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            bool lidhur = dbAdmin.eshteDokumentiILidhur(clsKoka.IdKokaShperndarjeShpenz, clsKoka.IdNivel, "T_SHPERNDARJESHPENZIMEKOKA", "IDSHPERNDARJESHPENZ");

            if (lidhur)
                return new { mesazh = new MesazhGabimi(MessagesResource.Messages["msgDokLidhurNukFshihet"]), statusi1 = false, pergjigje = "", rivleresim = rivleresim, pyetjeRivleresimi = DBNull.Value };

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DtDok, idNdermarrje);
            if (ekycur)
                return new { mesazh = new MesazhGabimi(MessagesResource.Messages["msgPeriudhaEKycur"]), statusi1 = false, pergjigje = "", rivleresim = rivleresim, pyetjeRivleresimi = DBNull.Value };

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DtDok, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.ShperndarjeShpenzimesh, clsKoka.IdKonfigAmbjente))
            {
                return new { mesazh = new MesazhGabimi(MessagesResource.Messages["msgPeriodIsClosed"]), statusi1 = false, pergjigje = "", rivleresim = rivleresim, pyetjeRivleresimi = DBNull.Value };
            }

            if (kontrolloRivleresim && clsKoka.IdStatusDok != 0)
            {
                colKokaMagazina col = new colKokaMagazina();
                col.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdKonfigAmbjente, clsKoka.IdKokaShperndarjeShpenz);
                foreach (clsKokaMagazina m in col)
                {
                    if (m.IdKokaMagazina != 0)
                        if (m.rivleresim())
                        {
                            rivleresim = true;
                            tr.mbushGjitheTrupiMagazinaNgaKoka(m.IdKokaMagazina);
                            trupat.AddRange(tr);
                        }
                }
            }

            DbCore.clsMesazh mesazhi = new DbCore.clsMesazh();
            clsKoka.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            mesazhi = clsKoka.fshi();

            DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
            dbAdmin.Dispose();
            if (mesazhi.Status)
            {
                if (rivleresim)
                    return new { mesazh = DBNull.Value, statusi1 = true, pergjigje = "fshi", rivleresim = rivleresim, pyetjeRivleresimi = MessagesResource.Messages["regjMagVeprimiSjellNdryshimNeCmimDalje"] };
                else
                    return new { mesazh = DBNull.Value, statusi1 = true, pergjigje = "fshi", rivleresim = rivleresim, pyetjeRivleresimi = DBNull.Value };
            }
            else
                return new { mesazh = mesazhi, statusi1 = false, pergjigje = "", rivleresim = false, pyetjeRivleresimi = DBNull.Value };
        }

        internal static object RuajShperndarjeShpenzimesh(string komponente, object kokaDokumentit, object trupiDokumentit, int idKonfigAmbjente, int statusDokumenti, bool kontrolloRivleresim, int menyreKontabilizimi, bool iLidhur, object llogarite, HttpSessionState Session)
        {
            try
            {
                string shfaqmesazhapolupemagazina = "jo";
                bool meKontabilizim = false;
                string pyetjeRivleresimi = "";
                clsShperndarjeShpenzimeKoka shperndarjeShpenzimeshKoka;

                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                DbCore.DbShare.clsKonfigurimAmbjenti konfam = new DbCore.DbShare.clsKonfigurimAmbjenti();

                bool rivleresim = false;
                colTrupiMagazina tr = new colTrupiMagazina();
                colTrupiMagazina trupat = new colTrupiMagazina();

                shperndarjeShpenzimeshKoka = krijoKokenShperndarjeShpenzimesh(statusDokumenti, kokaDokumentit, trupiDokumentit, idKonfigAmbjente, menyreKontabilizimi, out shfaqmesazhapolupemagazina, konfam, llogarite, Session);

                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);

                if (shperndarjeShpenzimeshKoka.IdKokaShperndarjeShpenz <= 0)
                {
                    if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                    {
                        return new { mesazh = new MesazhGabimi(MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"]), statusi1 = false, pergjigje = "", rivleresim = rivleresim, pyetjeRivleresimi = DBNull.Value, shfaqmesazhapolupemagazina = shfaqmesazhapolupemagazina, url = "" };
                    }

                    if (menyreKontabilizimi == 1 || menyreKontabilizimi == 2)
                        meKontabilizim = true;
                    else meKontabilizim = false;

                    if (statusDokumenti == 0)
                        meKontabilizim = false;

                    if (meKontabilizim)
                        mesazh = shperndarjeShpenzimeshKoka.ruajMeNrAuto(true, konfam);//me kontabilizim
                    else
                        mesazh = shperndarjeShpenzimeshKoka.ruajMeNrAuto(false, konfam);

                    colKokaMagazina col = new colKokaMagazina();
                    if (statusDokumenti != 0 && shperndarjeShpenzimeshKoka.IdKokaShperndarjeShpenz != 0 && kontrolloRivleresim)
                    {
                        col.mbushKokaMagazinaSipasIDGjenerues(shperndarjeShpenzimeshKoka.IdKonfigAmbjente, shperndarjeShpenzimeshKoka.IdKokaShperndarjeShpenz);
                        foreach (clsKokaMagazina m in col)
                        {
                            if (m.IdKokaMagazina != 0 && m.rivleresim())
                            {
                                rivleresim = true;
                                tr.mbushGjitheTrupiMagazinaNgaKoka(m.IdKokaMagazina);
                                trupat.AddRange(tr);
                            }
                        }
                    }
                }
                else if (shperndarjeShpenzimeshKoka.IdKokaShperndarjeShpenz > 0)
                {
                    if ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))
                        return new { mesazh = new MesazhGabimi(MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"]), statusi1 = false, pergjigje = "", rivleresim = rivleresim, pyetjeRivleresimi = DBNull.Value, shfaqmesazhapolupemagazina = shfaqmesazhapolupemagazina, url = "" };

                    colKokaMagazina col = new colKokaMagazina();

                    if (statusDokumenti != 0 && kontrolloRivleresim)
                    {
                        col.mbushKokaMagazinaSipasIDGjenerues(shperndarjeShpenzimeshKoka.IdKonfigAmbjente, shperndarjeShpenzimeshKoka.IdKokaShperndarjeShpenz);
                        foreach (clsKokaMagazina m in col)
                        {
                            if (m.IdKokaMagazina != 0 && m.rivleresim())
                            {
                                rivleresim = true;
                                tr.mbushGjitheTrupiMagazinaNgaKoka(m.IdKokaMagazina);
                                trupat.AddRange(tr);
                            }
                        }
                    }

                    DbCore.DbAdmin.clsDatabaseAdmin dbAdm = new DbCore.DbAdmin.clsDatabaseAdmin();
                    bool lidhur = dbAdm.eshteDokumentiILidhur(shperndarjeShpenzimeshKoka.IdKokaShperndarjeShpenz, shperndarjeShpenzimeshKoka.IdNivel, "T_SHPERNDARJESHPENZIMEKOKA", "IDSHPERNDARJESHPENZ");

                    if (lidhur != iLidhur)
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = MessagesResource.Messages["msgDokumentiEshteILidhur"];
                    }
                    else
                    {
                        if (lidhur == true)
                            if (menyreKontabilizimi == 1 || menyreKontabilizimi == 2)
                                mesazh = shperndarjeShpenzimeshKoka.modifiko(true, true, konfam);
                            else
                                mesazh = shperndarjeShpenzimeshKoka.modifiko(false, true, konfam);
                        else
                            if (menyreKontabilizimi == 1 || menyreKontabilizimi == 2)
                            mesazh = shperndarjeShpenzimeshKoka.modifiko(true, false, konfam);
                        else
                            mesazh = shperndarjeShpenzimeshKoka.modifiko(false, false, konfam);
                    }

                    if (statusDokumenti != 0 && kontrolloRivleresim)
                    {
                        col.mbushKokaMagazinaSipasIDGjenerues(shperndarjeShpenzimeshKoka.IdKonfigAmbjente, shperndarjeShpenzimeshKoka.IdKokaShperndarjeShpenz);
                        foreach (clsKokaMagazina m in col)
                        {

                            if (m.IdKokaMagazina != 0)
                                if (m.rivleresimPas())
                                {
                                    rivleresim = true;
                                    tr.mbushGjitheTrupiMagazinaNgaKoka(m.IdKokaMagazina);
                                    trupat.AddRange(tr);
                                }
                        }
                    }
                    dbAdm.Dispose();
                }

                DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
                string url = "";
                if (!mesazh.Status)
                    return new { mesazh = mesazh, statusi1 = false, pergjigje = "ruaj", rivleresim = rivleresim, pyetjeRivleresimi = DBNull.Value, shfaqmesazhapolupemagazina = shfaqmesazhapolupemagazina, url = "" };
                else
                {
                    if (rivleresim)
                        pyetjeRivleresimi = MessagesResource.Messages["regjMagVeprimiSjellNdryshimNeCmimDalje"];
                    else
                        mesazh = new MesazhSuksesi(MessagesResource.Messages["msgRuajtjeMeSukses"]);

                    List<object> fletetKontabel = new List<object>();
                    if (shfaqmesazhapolupemagazina != "jo")
                    {
                        colKokatFletetKontabel kokaFleteKontabel = new colKokatFletetKontabel(shperndarjeShpenzimeshKoka.IdKokaShperndarjeShpenz, 7);
                        foreach (clsKokaFleteKontabel kok in kokaFleteKontabel)
                            url += "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + ";";
                    }

                    return new { mesazh = mesazh, statusi1 = true, pergjigje = "ruaj", rivleresim = rivleresim, pyetjeRivleresimi = pyetjeRivleresimi, shfaqmesazhapolupemagazina = shfaqmesazhapolupemagazina, url = url };
                }
            }
            catch (MyException e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                return new { mesazh = new MesazhGabimi(e.Message), statusi1 = false, pergjigje = "", rivleresim = false, pyetjeRivleresimi = DBNull.Value, shfaqmesazhapolupemagazina = "jo", url = "" };
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return new { mesazh = new MesazhGabimi(MessagesResource.Messages["msgGabimRuajtjeShperndarje"]), statusi1 = false, pergjigje = "", rivleresim = false, pyetjeRivleresimi = DBNull.Value, shfaqmesazhapolupemagazina = "jo", url = "" };
            }

        }

        private static clsShperndarjeShpenzimeKoka krijoKokenShperndarjeShpenzimesh(int statusDokumenti, object kokaDokumentit, object trupiDokumentit, int idKonfigAmbjente, int menyreKontabilizimi, out string shfaqmesazhapolupe, clsKonfigurimAmbjenti konfamortizimi, object llogarite, HttpSessionState Session)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konfmag;
            clsKonf.mbushKonfigDefaultKomponentes(518, idNdermarrje); bool gjithmone = false;

            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti(idKonfigAmbjente);
            konfmag = new DbCore.DbShare.clsKonfigurimAmbjenti(konfig.IdKonfigurimi);
            DbCore.DbShare.clsKusht kusht = new DbCore.DbShare.clsKusht(konfmag.IdKonfigAmbjente, "ZDAM");
            konfamortizimi.mbushKonfigAmbjSipasId(kusht.Vlera, idGjuha);

            if (clsAlternativaKushti.getAlternativa(konfmag.IdKonfigAmbjente, "GJKGJ") == "Po")
                gjithmone = true;

            //object[] result = ruajTrupin(kokaDokumentitShperndare, trupiDokumentitShperndare, Session);
            bool mekontabilizim = false;

            if (menyreKontabilizimi == 1 || menyreKontabilizimi == 2)
                mekontabilizim = true;
            else
                mekontabilizim = false;

            if (statusDokumenti == 0)
                mekontabilizim = false;

            clsShperndarjeShpenzimeKoka koka = Newtonsoft.Json.JsonConvert.DeserializeObject<clsShperndarjeShpenzimeKoka>(kokaDokumentit.ToString());
            koka.DtDok = koka.DtDok.ToLocalTime();
            koka.DtRegjistrimi = koka.DtRegjistrimi.ToLocalTime();
            koka.OColTrupi = Newtonsoft.Json.JsonConvert.DeserializeObject<colShperndarjeShpenzimeTrupi>(trupiDokumentit.ToString());

            colShperndarjeShpenzimeFaturat colFaturat = new colShperndarjeShpenzimeFaturat();
            for (int i = 0; i < koka.OColTrupi.Count; i++)
            {
                clsShperndarjeShpenzimeFaturat fat = new clsShperndarjeShpenzimeFaturat();
                fat.IdFatura = koka.OColTrupi[i].IdFatura;
                colFaturat.Add(fat);
            }

            koka.OColLlogarite = Newtonsoft.Json.JsonConvert.DeserializeObject<colShperndarjeShpenzimeLlogarite> (llogarite.ToString());
            koka.OColFaturat = colFaturat;
            koka.OColKokaMag = new colKokaMagazina();
            koka.OColFletetKontabel = new colKokatFletetKontabel();
            clsMesazh mesazh = koka.krijoMagazineNgaShperndarjeShpenzimesh(koka, konfmag, mekontabilizim, out shfaqmesazhapolupe, koka.IdKokaShperndarjeShpenz, gjithmone);
            if (!mesazh.Status)
                throw new DbCore.MyException(mesazh.PershkrimMesazhi);
            return koka;
        }

        private static object[] ruajTrupin(object kokaDokumentitShperndare, object trupiDokumentitShperndare, HttpSessionState Session)
        {

            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(kokaDokumentitShperndare.ToString());
            object[] shpenzimi = (object[])serializusi.DeserializeObject(trupiDokumentitShperndare.ToString());
            colShperndarjeShpenzimeTrupi col = new colShperndarjeShpenzimeTrupi();
            colShperndarjeShpenzimeFaturat colFaturat = new colShperndarjeShpenzimeFaturat();
            for (int i = 0; i < dokumenti.Length; i++)
            {
                if (dokumenti[i] != null)
                {
                    clsShperndarjeShpenzimeTrupi trup = new clsShperndarjeShpenzimeTrupi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), (Dictionary<string, object>)dokumenti[i], shpenzimi[i]);
                    if (trup.IdFatura != -1)
                    {
                        col.Add(trup);
                        clsShperndarjeShpenzimeFaturat fat = new clsShperndarjeShpenzimeFaturat();
                        fat.IdFatura = trup.IdFatura;
                        colFaturat.Add(fat);
                    }
                }
            }
            object[] o = { col, colFaturat };
            return o;
        }

        private static colShperndarjeShpenzimeLlogarite merrLlogarite(object llogarite)
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] llogariteArr = (object[])serializusi.DeserializeObject(llogarite.ToString());

            colShperndarjeShpenzimeLlogarite trupat = new colShperndarjeShpenzimeLlogarite();
            clsShperndarjeShpenzimeLlogarite trupi;

            for (int i = 0; i < llogariteArr.Length; i++)
            {
                trupi = (clsShperndarjeShpenzimeLlogarite)llogariteArr[i];
                trupat.Add(trupi);
            }
            return trupat;
        }

        internal static object MerrDokumentShperndarjeShpenzimi(int idDokumenti, HttpSessionState Session)
        {
            clsShperndarjeShpenzimeKoka kokaDokumentit = new clsShperndarjeShpenzimeKoka(idDokumenti);
            DataTable trupiDt = clsShperndarjeShpenzimeKoka.MerrTrupDokumentShperndarjeShpenzimiFull(idDokumenti);
            DataView trupiDtView = trupiDt.AsDataView();
            DataTable llogariteDt = clsShperndarjeShpenzimeKoka.MerrLlogariteShperndarjeShpenzimiFull(idDokumenti);
            DataTable trupiKokat = trupiDtView.ToTable(true, "IdDokumenti", "NrFature", "DtFature", "Furnitori", "IdKlientFurnitori", "IdTrupi", "IdKoka");
            List<object> trupiKoka = new List<object>();
            foreach(DataRow row in trupiKokat.Rows)
            {
                trupiKoka.Add(new
                {
                    IdDokumenti = row["IdDokumenti"],
                    NrFature = row["NrFature"],
                    DtFature = row["DtFature"],
                    IdFurnitori = row["IdKlientFurnitori"],
                    Furnitori = row["Furnitori"],
                    IdTrupi = row["IdTrupi"],
                    IdKoka = row["IdKoka"],
                    Trupi = trupiDt.Select($"IdDokumenti = {Convert.ToInt32(row["IdDokumenti"])}").CopyToDataTable()
                });
            }

            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            int idNdermarrje = mySessionObjects.ktheIdPerdoruesi(Session);
            int idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
            int idGjuha = mySessionObjects.ktheGjuhe(Session);

            return new { koka = kokaDokumentit, trupi = trupiKoka, llogarite = llogariteDt };
        }

        internal static object ShperndarjeShpenzimiKryejRivleresim(HttpSessionState Session)
        {
            System.Globalization.CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            colTrupiMagazina trupat = mySessionObjects.merrTrupatNgaSesioni(Session);
            clsMesazh mesazh = new clsMesazh(true, rm.GetString("regjMagMesazhSuksesiRuajteDokDheRivleresim", ci));
            clsLogRivleresimInventari log = new clsLogRivleresimInventari();
            try
            {
                log = new clsLogRivleresimInventari(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                throw new MyException(rm.GetString("msgGabimGjateRuajtjesSeRivleresimitNeLog", ci));
            }

            foreach (clsTrupiMagazina t in trupat)
                mesazh = clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today, clsArtikulli.ktheKontrollCmimiPerDetajim(t.IdArtikulli), log, ci, rm, mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));

            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new colTrupiMagazina());
            return new { mesazh = mesazh };
        }
    }
}
