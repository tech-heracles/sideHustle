using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web.Script.Serialization;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.DataBase;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    /// <summary>
    /// Rivleresimi i inventarit: per magazinen (ose te gjitha) dhe periudhen e zgjedhur shfaqen artikujt me levizje;
    /// per artikujt e zgjedhur rillogaritet kostoja e rreshtave te magazines (clsTrupiMagazina.bejRivleresim, i njejti
    /// motor si rivleresimi pas ndryshimit te nje dokumenti), dite pas dite, vetem ne ditet qe kane levizje.
    /// </summary>
    public partial class RivleresimMagazine : MyPageBase
    {
        private const string KomponentiMenuse = "RivleresimMagazine.aspx";
        private const string SesioniArtikujt = "RivleresimMagazine_Artikujt";
        private const string SesioniFiltri = "RivleresimMagazine_Filtri";

        /// <summary>Magazina dhe periudha me te cilat u mbush grida; rivleresimi perdor po keto vlera.</summary>
        [Serializable]
        private class FiltriRivleresimit
        {
            public int IdMag;
            public string KodMag;
            public DateTime Nga;
            public DateTime Deri;
        }

        private string mesazhPerGriden;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(idPerd, idNdermarrje);

            if (!IsPostBack)
            {
                mbushHiddenFieldMePerkthime();
                mbushComboMagazinat(idNdermarrje, idPerd);
                vendosDatatDefault();
                Session[SesioniArtikujt] = null;
                Session[SesioniFiltri] = null;
            }
            AspxWebControlUtils.vendosDateEditMask(dtePeriudhaNga);
            AspxWebControlUtils.vendosDateEditMask(dtePeriudhaDeri);

            // grida lidhet ne cdo kerkese qe zgjedhja e artikujve te lexohet edhe ne ProgressBar1_RunTask
            gvRivleresim.DataSource = Session[SesioniArtikujt] as DataTable;
            gvRivleresim.DataBind();
        }

        private void mbushHiddenFieldMePerkthime()
        {
            hfState.Set("msgZgjidhPeriudhen", "Zgjidhni periudhën (nga - deri).");
            hfState.Set("MsgDataGabim", "Data 'deri' nuk mund të jetë para datës 'nga'.");
            hfState.Set("msgZgjidhArtikujt", "Shfaqni artikujt dhe zgjidhni ata që doni të rivlerësoni.");
            hfState.Set("msgKonfirmoRivleresimin", "Do të rivlerësohen {0} artikuj. Gjatë rivlerësimit kostoja e daljeve ndryshon. Të vazhdoj?");
            hfState.Set("msgRivleresimiUNdal", "Rivlerësimi u ndal te");
            hfState.Set("msgRivleresimiGabim", "Ndodhi një gabim gjatë rivlerësimit. Shikoni logun e rivlerësimit.");
        }

        private void percaktoTemplateMenu(int idPerdorues, int idNdermarrje)
        {
            try
            {
                clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session),
                    idPerdorues, idNdermarrje, ASPxMenu1, KomponentiMenuse, this, MenuInfo, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "RivleresimMagazine: toolbar-i nuk u ngarkua");
            }
            // butonat e faqes shtohen edhe kur databaza nuk i ka per kete komponente
            if (ASPxMenu1.Items.FindByName("Rivleresim") == null)
                ASPxMenu1.Items.Add(new MenuItem("Rivlerëso", "Rivleresim", "~/images/Refresh_Snap.png"));
            if (ASPxMenu1.Items.FindByName("Stop") == null)
                ASPxMenu1.Items.Add(new MenuItem("Ndalo", "Stop", "~/images/Ball-stop-32.png"));
        }

        private void mbushComboMagazinat(int idNdermarrje, int idPerdorues)
        {
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, cmbMagazina, idPerdorues, false, 0, true);
            cmbMagazina.Items.Insert(0, new ListEditItem("Të gjitha magazinat", 0));
            cmbMagazina.SelectedIndex = 0;
        }

        private void vendosDatatDefault()
        {
            clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            DateTime sot = DateTime.Today;
            dtePeriudhaNga.Value = periudha.FillimiPeriudha;
            dtePeriudhaDeri.Value = sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha ? sot : periudha.MbarimiPeriudha;
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        protected void gvRivleresim_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            gvRivleresim.Selection.UnselectAll();
            if (e.Parameters == "pastro")
            {
                Session[SesioniArtikujt] = null;
                Session[SesioniFiltri] = null;
            }
            else if (e.Parameters == "shfaq")
            {
                if (dtePeriudhaNga.Value == null || dtePeriudhaDeri.Value == null)
                {
                    mesazhPerGriden = "Zgjidhni periudhën (nga - deri).";
                    return;
                }
                var filtri = new FiltriRivleresimit
                {
                    IdMag = cmbMagazina.Value == null ? 0 : Convert.ToInt32(cmbMagazina.Value),
                    KodMag = cmbMagazina.Value == null || Convert.ToInt32(cmbMagazina.Value) == 0 ? "" : cmbMagazina.Text,
                    Nga = dtePeriudhaNga.Date.Date,
                    Deri = dtePeriudhaDeri.Date.Date
                };
                if (filtri.Deri < filtri.Nga)
                {
                    mesazhPerGriden = "Data 'deri' nuk mund të jetë para datës 'nga'.";
                    return;
                }
                DataTable artikujt = colTrupiMagazina.merrArtikujMeLevizjePerRivleresim(
                    DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), filtri.IdMag, filtri.Nga, filtri.Deri);
                Session[SesioniArtikujt] = artikujt;
                Session[SesioniFiltri] = filtri;
                if (artikujt.Rows.Count == 0)
                    mesazhPerGriden = "Nuk ka lëvizje magazine në këtë periudhë" + (filtri.IdMag == 0 ? "." : " për magazinën " + filtri.KodMag + ".");
            }
            gvRivleresim.DataSource = Session[SesioniArtikujt] as DataTable;
            gvRivleresim.DataBind();
        }

        protected void gvRivleresim_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpMesazh"] = mesazhPerGriden;
        }

        private static string Json(bool? status, string mesazh)
        {
            return new JavaScriptSerializer().Serialize(new { Status = status, Mesazh = mesazh });
        }

        protected void ProgressBar1_RunTask(object sender, EO.Web.ProgressTaskEventArgs e)
        {
            e.UpdateProgress(0);
            var filtri = Session[SesioniFiltri] as FiltriRivleresimit;
            var artikujt = Session[SesioniArtikujt] as DataTable;
            if (filtri == null || artikujt == null)
            {
                e.UpdateProgress(0, Json(false, "Shfaqni fillimisht artikujt për periudhën."));
                return;
            }
            var teZgjedhur = new HashSet<int>(gvRivleresim.GetSelectedFieldValues("IdArtikulli").Select(x => Convert.ToInt32(x)));
            if (teZgjedhur.Count == 0)
            {
                e.UpdateProgress(0, Json(false, "Nuk është zgjedhur asnjë artikull."));
                return;
            }

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(filtri.Nga, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.RivleresimInventari, 0))
            {
                e.UpdateProgress(0, Json(false, "Periudha e rivlerësimit është e mbyllur në datën " + filtri.Nga.ToString("dd/MM/yyyy") + "."));
                return;
            }

            // kodet e artikujve dhe magazinave per mesazhet dhe logun
            var kodArtikulli = artikujt.Rows.Cast<DataRow>().ToDictionary(r => Convert.ToInt32(r["IdArtikulli"]), r => Convert.ToString(r["Kodi"]));
            var kodMagazina = new Dictionary<int, string>();
            foreach (ListEditItem item in cmbMagazina.Items)
                if (item.Value != null && Convert.ToInt32(item.Value) != 0)
                    kodMagazina[Convert.ToInt32(item.Value)] = item.Text;

            // ditet me levizje te artikujve te zgjedhur, sipas artikullit -> dites -> magazines
            var punet = colTrupiMagazina.merrDitetMeLevizjePerRivleresim(idNdermarrje, filtri.IdMag, filtri.Nga, filtri.Deri)
                .Rows.Cast<DataRow>()
                .Select(r => new { IdArt = Convert.ToInt32(r["IdArtikulli"]), IdMag = Convert.ToInt32(r["IdMag"]), Dita = Convert.ToDateTime(r["Dita"]) })
                .Where(x => teZgjedhur.Contains(x.IdArt))
                .GroupBy(x => x.IdArt)
                .ToList();
            int nrMagazinash = punet.SelectMany(g => g).Select(x => x.IdMag).Distinct().Count();

            clsLogRivleresimInventari log;
            try
            {
                log = new clsLogRivleresimInventari(idPerdorues, idNdermarrje, punet.Count, nrMagazinash, filtri.Nga, filtri.Deri);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "RivleresimMagazine: logu i rivleresimit nuk u krijua");
                e.UpdateProgress(0, Json(false, "Logu i rivlerësimit nuk u ruajt; rivlerësimi nuk filloi."));
                return;
            }

            string kodAktual = "";
            DateTime ditaAktuale = DateTime.MinValue;
            int nrDitesh = 0;
            var prioritetiFillestar = System.Threading.Thread.CurrentThread.Priority;
            try
            {
                System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.BelowNormal;
                for (int i = 0; i < punet.Count; i++)
                {
                    int idArt = punet[i].Key;
                    kodAktual = kodArtikulli.TryGetValue(idArt, out var k) ? k : idArt.ToString();
                    int metodeKostoje = clsArtikulli.ktheMetodeKostoje(idArt);
                    bool kontrollCmimPerDetajim = clsArtikulli.ktheKontrollCmimiPerDetajim(idArt);
                    log.logRivleresim(kodAktual, punet[i].First().Dita);

                    foreach (var pune in punet[i])
                    {
                        if (e.IsStopped)
                        {
                            log.logStopRivleresim("Ndaluar nga perdoruesi", kodAktual, pune.Dita, String.Empty);
                            return;
                        }
                        ditaAktuale = pune.Dita;
                        clsMesazh mesazh = clsTrupiMagazina.bejRivleresim(kontrollCmimPerDetajim, idArt, pune.IdMag, metodeKostoje, pune.Dita, log, ci, rm, idNdermarrje, idPerdorues);
                        if (!mesazh.Status)
                        {
                            string kodMag = kodMagazina.TryGetValue(pune.IdMag, out var km) ? km : pune.IdMag.ToString();
                            log.logStopRivleresim("Gabim", kodAktual, pune.Dita, mesazh.PershkrimMesazhi);
                            e.UpdateProgress(e.Value, Json(false, String.Format("Rivlerësimi u ndal te artikulli {0}, magazina {1}, data {2:dd/MM/yyyy}: {3} " +
                                "Artikujt para tij janë rivlerësuar.", kodAktual, kodMag, pune.Dita, mesazh.PershkrimMesazhi)));
                            return;
                        }
                        nrDitesh++;
                    }
                    int perqindja = Math.Min(99, (int)((i + 1) * 100L / punet.Count));
                    e.UpdateProgress(perqindja, Json(null, String.Format("{0} ({1}/{2})", kodAktual, i + 1, punet.Count)));
                }
                log.logFinishedRivleresim("");
                e.UpdateProgress(100, Json(true, String.Format("Rivlerësimi përfundoi: {0} artikuj, {1} ditë-magazina, {2:dd/MM/yyyy} - {3:dd/MM/yyyy}.",
                    punet.Count, nrDitesh, filtri.Nga, filtri.Deri)));
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "RivleresimMagazine: gabim te artikulli " + kodAktual);
                try { log.logStopRivleresim("Gabim", kodAktual, ditaAktuale, ex.Message); } catch { }
                e.UpdateProgress(e.Value, Json(false, "Rivlerësimi u ndal te artikulli " + kodAktual + ": " + ex.Message));
            }
            finally
            {
                System.Threading.Thread.CurrentThread.Priority = prioritetiFillestar;
            }
        }
    }
}
