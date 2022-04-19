using DbCore;
using DbCore.DbAdmin;
using System.Collections.Generic;
using DbCore.DbCRM;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Web;
using System.Web.UI;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using System.Linq;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class CRMRouteAgjenti : MyPageBase
    {
        public static string PrepareMessage(string subjectText, string detailInfoText, bool showDetailedErrorInfo)
        {
            string subject = String.Format("{0}\n", subjectText);
            string detailInfo = String.Format("Detailed information is included below.\n\n- {0}", detailInfoText);
            if (!showDetailedErrorInfo)
                detailInfo = String.Empty;
            subject = NewLinesToBr(HttpUtility.HtmlEncode(detailInfoText));
            detailInfo = NewLinesToBr(HttpUtility.HtmlEncode(detailInfo));
            return String.Format("{0},{1}|{2}{3}", subject.Length, detailInfo.Length, subject, detailInfo);
        }

        // ASPxSchedulerStorage Storage { get { return SkedulerAgjenti.Storage; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
            if (!IsPostBack)
            {
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("Artikull", false);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idVitNdermarrje", idviti);

                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, "CRMRouteAgjenti.aspx");
                hfTeDrejta["Shtim"]= tedrejtaInfo.DShtim;
                hfTeDrejta["Modifikim"]= tedrejtaInfo.DMod;

                int id = Convert.ToInt32(Request.QueryString["id"]);
                DataTable ds = new DataTable();
                ds = colSkeduler.merrTakimeSipasNdermarrjesAndAgjent(idNdermarrje, 0);
                mySessionObjects.ruajdtNeSession(Session, ds);
                MapAppointmentData();
                ASPxScheduler1.AppointmentDataSource = ds;
                ASPxScheduler1.DataBind();
                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnAgjenti);
                ConfigureAspxComboBox.shtoKolonaPerAgjentin(btnAgjenti);
                ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(IdNdermarrja, IdPerdoruesi, btnAgjenti);
                lblUserEmri.Text = DbCore.mySessionObjects.ktheEmerPerdorues(Session);

                ASPxScheduler1.Start = DateTime.Today;
                AspxWebControlUtils.vendosDateEditMask(fromDateStart, fromDateEnd, toDateStart, toDateEnd);
                AspxWebControlUtils.InicializoDate(DateTime.Now, fromDateStart, fromDateEnd, toDateStart, toDateEnd);
            }
            else
            {
                DataTable ds = mySessionObjects.merrDtNgaSessioni(Session);
                MapAppointmentData();
                ASPxScheduler1.AppointmentDataSource = ds;
                ASPxScheduler1.DataBind();
            }
            
      
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(Convert.ToInt32(hfState["idGjuha"]), Convert.ToInt32(hfState["idVitNdermarrje"]), Convert.ToInt32(hfState["idPerdoruesi"]), Convert.ToInt32(hfState["idNdermarrje"]), ASPxMenu1);
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
        }

        protected void ASPxScheduler1_AppointmentRowDeleting(object sender, ASPxSchedulerDataDeletingEventArgs e)
        {
            for (int i = 0; i < ASPxScheduler1.SelectedAppointments.Count; i++)
            {
                int idTakimi = merrNextAppointmentID();

                if (idTakimi > 0)
                {
                    DbCore.DbCRM.clsSkeduler sked = new clsSkeduler(idTakimi);

                    if (sked.StartDate < DateTime.Today)
                    {
                        throw new Exception("Nuk mund te fshini takim ose detyre para dates se sotme!");
                    }
                    else if (sked.Status==1 && sked.StartDate== DateTime.Today) {
                         throw new Exception("Nuk mund te fshini takim ose detyre te sotme, te perfunduar!");
                    }
                    else
                    {
                        clsMesazh mesazh = DbCore.DbCRM.clsSkeduler.fshi(idTakimi, mySessionObjects.ktheIdPerdoruesi(Session));
                        if (!mesazh.Status == true)
                        {
                            throw new Exception(mesazh.PershkrimMesazhi);
                        }
                        else
                        {
                            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Fshirja perfundoi me sukses!:Green");
                        }
                    }

                    e.Cancel = true;
                    mbushgride();
                }
            }
        }

        protected void ASPxScheduler1_AppointmentRowUpdating(object sender, ASPxSchedulerDataUpdatingEventArgs e)
        {
            if ((DateTime)e.OldValues["STARTDATE"] < DateTime.Today)
            {
                throw new Exception("Nuk mund te modifikoni takim para dates se sotme!");
            }
            if ((DateTime)e.NewValues["STARTDATE"] < DateTime.Today)
            {
                throw new Exception("Nuk mund te shtohet takim para dates se sotme!");
            }

            int idTakimi = merrNextAppointmentID();
            clsSkeduler sked =null;
            if (idTakimi == -1)//inserti eshte me kopjim
                idTakimi = Convert.ToInt32(e.NewValues["IDAUTO"]);

            sked = new clsSkeduler(idTakimi);
            hfklienti.Value = Convert.ToString(sked.IdKlienti);
            hfLlojDetyre.Value = Convert.ToString(sked.Lloji);

            if (hfklienti.Value == "" || hfklienti.Value == "0")
            {
                throw new Exception("Zgjidhni nje takim ose detyre!");
            }
            int idagjenti = int.Parse(btnAgjenti.Value.ToString());

            DbCore.DbAdmin.clsAgjentShitje agjent = new DbCore.DbAdmin.clsAgjentShitje(idagjenti);


            DateTime startDate =Convert.ToDateTime(e.NewValues["STARTDATE"]);
            DateTime endDate = Convert.ToDateTime(e.NewValues["ENDTIME"]);



            DbCore.DbCRM.clsSkeduler ske = new DbCore.DbCRM.clsSkeduler(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), agjent.IdPerdoruesMobile, int.Parse(hfklienti.Value), (DateTime)e.NewValues["STARTDATE"], mySessionObjects.ktheIdPerdoruesi(Session), DateTime.Now, mySessionObjects.ktheIdPerdoruesi(Session), DateTime.Now, 1, (DateTime)e.NewValues["STARTDATE"], (DateTime)e.NewValues["ENDTIME"], (Boolean)e.NewValues["ALLDAY"], e.NewValues["DESCRIPTION"].ToString(), mySessionObjects.merrIdNdermarrjeSesioni(Session),-1, null, null, 0);

            ske.IdAuto = idTakimi;
            if (startDate.Date == sked.StartDate.Date && endDate.Date == sked.EndTime.Date)
            {
               ske.KoordinateFillimi= sked.KoordinateFillimi;
               ske.KoordinateMbarimi = sked.KoordinateMbarimi;
               ske.Status = sked.Status;
               ske.Lloji = sked.Lloji;
            }
           

            clsMesazh mesazh = ske.modifiko();
            if (!mesazh.Status == true)
            {
                throw new Exception(mesazh.PershkrimMesazhi);
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
            }
            e.Cancel = true;
            mbushgride();
        }

        protected void SkedulerAgjenti_AppointmentRowInserting(object sender, ASPxSchedulerDataInsertingEventArgs e)
        {
            if ((DateTime)e.NewValues["STARTDATE"] < DateTime.Today)
            {
                throw new Exception("Nuk mund te shtoni takim para dates se sotme!");
            }
            else
            {
                int idTakimi = merrNextAppointmentID();
                if (idTakimi > -1)//inserti eshte me kopjim
                {
                    clsSkeduler sked = new clsSkeduler(idTakimi);
                    hfklienti.Value = Convert.ToString(sked.IdKlienti);
                    hfLlojDetyre.Value = Convert.ToString(sked.Lloji);
                }
                //else inserti eshte nga forma

                if (hfklienti.Value == "" || hfklienti.Value == "0" || hfLlojDetyre.Value == "" || hfLlojDetyre.Value == "0")
                {
                    throw new Exception("Zgjidhni nje klient ose detyre!");
                }
                int idagjenti = int.Parse(btnAgjenti.Value.ToString());
                DbCore.DbAdmin.clsAgjentShitje agjent = new DbCore.DbAdmin.clsAgjentShitje(idagjenti);
                if (agjent.IdPerdoruesMobile == 0)
                {
                    throw new Exception("Ju lutem zgjidhni nje perdorues per agjentin!");
                }

                DbCore.DbCRM.clsSkeduler ske = new DbCore.DbCRM.clsSkeduler(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), agjent.IdPerdoruesMobile, int.Parse(hfklienti.Value), (DateTime)e.NewValues["STARTDATE"], mySessionObjects.ktheIdPerdoruesi(Session), DateTime.Now, mySessionObjects.ktheIdPerdoruesi(Session), DateTime.Now, 1, (DateTime)e.NewValues["STARTDATE"], (DateTime)e.NewValues["ENDTIME"], (Boolean)e.NewValues["ALLDAY"], e.NewValues["DESCRIPTION"].ToString(), mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(hfLlojDetyre.Value), null, null, 0);
                clsMesazh mesazh = ske.ruaj();
                if (mesazh.Status)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Takimi u shtua me sukses!:Green");
                    e.Cancel = true;
                   // mbushgride();
                    ASPxScheduler1.JSProperties["cpUShtuaTakim"] = true;
                }
                else
                {
                    throw new Exception(mesazh.PershkrimMesazhi);
                }
            }
        }

        protected void ASPxScheduler1_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            mbushgride();
        }

        protected void ASPxScheduler1_CustomErrorText(object handler, ASPxSchedulerCustomErrorTextEventArgs e)
        {
            e.ErrorText = PrepareMessage("", e.Exception.Message, false);
        }

        protected void ASPxScheduler1_HtmlTimeCellPrepared(object handler, ASPxSchedulerTimeCellPreparedEventArgs e)
        {
            if (e.Interval.Start < (DateTime.Now.Date))
                e.Cell.BackColor = Color.FromName("#F2F2F2");
            else
                e.Cell.BackColor = Color.White;
      
        }

        protected void ASPxScheduler1_PopupMenuShowing(object sender, DevExpress.Web.ASPxScheduler.PopupMenuShowingEventArgs e)
        {

            //customizohet menuja
            if (e.Menu.Id == DevExpress.XtraScheduler.SchedulerMenuItemId.DefaultMenu)
            {
                int idagjenti = 0;
                if (btnAgjenti.Value == null)
                    e.Menu.Enabled = false;
                else
                    e.Menu.Enabled = int.TryParse(btnAgjenti.Value.ToString(), out idagjenti);
                e.Menu.Items.Clear();
                e.Menu.Items.Add("Shto Klient", "Klient");                
                e.Menu.Items.Add("Shto Detyre", "Detyre");
                //e.Menu.Items.RemoveAt(1);
                //e.Menu.Items.RemoveAt(1);
            }
            else
            {
                e.Menu.Items.Clear();
                e.Menu.Items.Add("Shiko", "Shiko");
                e.Menu.Items.Add("Fshi", "Fshi");
                e.Menu.Items.Add("Shiko detaje ne historik", "ShikoHistorik");
                e.Menu.Items.Add("Shiko raportin e anketave", "ShikoRapAnketa");
            }
            //mos e fshini kete rresht,bug devexpressi
            e.Menu.ClientSideEvents.ItemClick = "function(s,e){console.log(e)}";
        }

        protected void popupDateRanges_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
        {
        }

        /// <summary>
        /// ne callback te popupit behet klonimi i takimeve 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e">     </param>
        protected void popupDateRanges_WindowCallback(object source, DevExpress.Web.PopupWindowCallbackArgs e)
        {
            int idPerdoruesi = Convert.ToInt32(hfState["idPerdoruesi"]);
            int idNdermarrje = Convert.ToInt32(hfState["idNdermarrje"]);

            var fromDtStart = Convert.ToDateTime(fromDateStart.Value);
            var fromDtEnd = Convert.ToDateTime(fromDateEnd.Value);
            var toDtStart = Convert.ToDateTime(toDateStart.Value);
            var toDtEnd = Convert.ToDateTime(toDateEnd.Value);

            int idagjenti = int.Parse(btnAgjenti.Value.ToString());
            bool klonoDetyra = DetyraChkb.Checked;
            bool klonoKlient = KlientChkb.Checked;

            DbCore.DbAdmin.clsAgjentShitje agjent = new DbCore.DbAdmin.clsAgjentShitje(idagjenti);

            clsMesazh mesazh = ValidoDatat(fromDtStart, fromDtEnd, toDtStart, toDtEnd);

            if (mesazh.Status == true)
            {
                mesazh = colSkeduler.KontrolloTakimet(fromDtStart, fromDtEnd, toDtStart, toDtEnd, idNdermarrje, agjent.IdPerdoruesMobile, klonoDetyra, klonoKlient);

                if (mesazh.Tipi == TipMesazhi.Sukses)
                {
                    ///nese nuk ka data me takime bosh ose data te zena
                    ///vazhdo me ruajtjen
                    mesazh = colSkeduler.KlonoTakimet(fromDtStart, fromDtEnd, toDtStart, toDtEnd, agjent.IdPerdoruesMobile, idPerdoruesi, idNdermarrje, klonoDetyra, klonoKlient);

                    if (mesazh.Tipi == TipMesazhi.Sukses)
                    {
                        //per te mbyllur popupin
                        popupDateRanges.JSProperties["cpKlonimMeSukses"] = true;
                        // mySessionObjects.ruajMesazhNeSesion(Session, "Klonimi u krye me sukses!:Green"); 
                        clsMenuInfo.ShtoMesazh(MenuInfo, mesazh, pnlMesazhi);
                        return;
                    }
                }
                else if (mesazh.Tipi == TipMesazhi.Informim)
                {
                    //kjo vlere vendoset ne client side,ne rastin kur perdoruesi shtyp ok.
                    //nese ka ndryshuar datat ath i kerkohet perseri konfirmim
                    if (e.Parameter == "Konfirmuar")
                    {
                        mesazh = colSkeduler.KlonoTakimet(fromDtStart, fromDtEnd, toDtStart, toDtEnd, agjent.IdPerdoruesMobile, idPerdoruesi, idNdermarrje, klonoDetyra, klonoKlient);

                        if (mesazh.Tipi == TipMesazhi.Sukses)
                        {
                            popupDateRanges.JSProperties["cpKlonimMeSukses"] = true;
                            clsMenuInfo.ShtoMesazh(MenuInfo, mesazh, pnlMesazhi);

                            return;
                        }
                    }
                    else
                    {
                        popupDateRanges.JSProperties["cpKerkoKonfirmim"] = true;
                        lblInfo.Text = mesazh.PershkrimMesazhi + " Shtyp butonin Ruaj Per te vazhduar!";
                        return;
                    }
                }
            }

            //rastet e tjera
            lblInfo.ForeColor = Color.Red;
            lblInfo.Text = mesazh.PershkrimMesazhi;
        }

        protected void Ruaj_Click(object sender, EventArgs e)
        {
        }

        private static string NewLinesToBr(string text)
        {
            text = text.Replace("\r", string.Empty);
            return text.Replace("\n", "<br/>");
        }

        private void MapAppointmentData()
        {
            if (!IsPostBack || Request.Params["__CALLBACKID"] == "ASPxScheduler1")
            {
                ASPxSchedulerStorage storage = ASPxScheduler1.Storage;
                storage.BeginUpdate();
                try
                {
                    ASPxAppointmentMappingInfo appMappings =
                      storage.Appointments.Mappings;
                    appMappings.AppointmentId = "IDAUTO";
                    appMappings.Start = "STARTDATE";
                    appMappings.End = "ENDTIME";
                    appMappings.Subject = "SUBJEKT";
                    appMappings.Description = "DESCRIPTION";
                    if(storage.Appointments.CustomFieldMappings.Count == 0)
                        storage.Appointments.CustomFieldMappings.Add(new ASPxAppointmentCustomFieldMapping("TeKlienti", "TEKLIENTI"));
                    //appMappings.Location = "LOCATION";  gisjana
                    appMappings.AllDay = "ALLDAY";
                    // appMappings.Type = "TYPE"; appMappings.RecurrenceInfo = "RECURRENCEINFO";
                    // appMappings.ReminderInfo = "REMINDERINFO";
                    appMappings.Label = "LABEL";
                    appMappings.Status = "STATUS";
                    // appMappings.ResourceId = "RESOURCESIDS"; 
                }
                finally
                {
                    storage.EndUpdate();
                }
            }
            
        }

        private void mbushgride()
        {
            int idNdermarrje = int.Parse(hfState.Get("idNdermarrje").ToString());
            DataTable ds = new DataTable();
            int idagjenti = 0;
            bool suksesParse = false;
            if (btnAgjenti.Value != null)
                suksesParse = int.TryParse(btnAgjenti.Value.ToString(), out idagjenti);
            if (btnAgjenti.Value != null && !suksesParse)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Agjenti nuk ekziston!:Red");
                return;
            }
            mySessionObjects.ruajObjectNeSesion(Session, idagjenti);
            ds = colSkeduler.merrTakimeSipasNdermarrjesAndAgjent(idNdermarrje, idagjenti);
            mySessionObjects.ruajdtNeSession(Session, ds);
            MapAppointmentData();
            ASPxScheduler1.AppointmentDataSource = ds;
            ASPxScheduler1.DataBind();
            DevExpress.XtraScheduler.SchedulerViewType activ = ASPxScheduler1.ActiveViewType;

            DateTime start = ASPxScheduler1.Start;

            if (activ != DevExpress.XtraScheduler.SchedulerViewType.Day)
                ASPxScheduler1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Day;
            else ASPxScheduler1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month;

            ASPxScheduler1.ActiveViewType = activ;
            ASPxScheduler1.Start = start;
        }

        /// <summary>
        /// merr nga hiddenfield id-te e takimeve te selektuar per tu kopjuar ose zhvendosur 
        /// </summary>
        /// <returns></returns>
        private int merrNextAppointmentID()
        {
            var vlerat = SelectedIDs.Value;
            int nextID = -1;

            if (vlerat.Length > 0)
            {
                if (!vlerat.Contains(","))//ka vetem nje vlere
                {
                    nextID = Convert.ToInt32(vlerat);
                    SelectedIDs.Value = null;
                }
                else
                {
                    //merr te paren ne radh edhe hiqe nga hiddenfieldi
                    nextID = Convert.ToInt32(vlerat.Split(',')[0]);
                    SelectedIDs.Value = vlerat.Substring(vlerat.IndexOf(',') + 1);
                }
            }
            return nextID;
        }

        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            //clsMenuInfo.ShtoMenuItemInfo(this, MenuInfoPopup);
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "CRMRouteAgjenti.aspx", this, MenuInfo, null, null, null, null, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), false, true);
        }

        /// <summary>
        /// kontrollon nese periudhat jane te njejta 
        /// </summary>
        /// <param name="fromDtStart"></param>
        /// <param name="fromDtEnd">  </param>
        /// <param name="toDtStart">  </param>
        /// <param name="toDtEnd">    </param>
        /// <returns></returns>
        private clsMesazh ValidoDatat(DateTime fromDtStart, DateTime fromDtEnd, DateTime toDtStart, DateTime toDtEnd)
        {
            if (toDtStart < DateTime.Today)
                return new clsMesazh(false, "Nuk mund te shtojme takime para dates se sotme!");
            if ((fromDtStart > fromDtEnd) || (toDtStart > toDtEnd))
                return new clsMesazh(false, "Periudha e pavlefshme!");
            if ((fromDtEnd - fromDtStart) != (toDtEnd - toDtStart))
                return new clsMesazh(false, "Periudhat e zgjedhura duhet te kene te njejten kohezgjatje!");

            return new clsMesazh(true);
        }



        protected void btnAgjenti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {

            if (Request.Params["__CALLBACKID"].Contains("btnAgjenti"))
      {
                colAgjenteShitje colAgjentet = new colAgjenteShitje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                if (IsCallback)
                {
                    if (string.IsNullOrWhiteSpace(e.Filter)) return;
                    var dt = DbCore.DbAdmin.colAgjenteShitje.merrAgjenteShitjeSipasAutorizimeDt(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    btnAgjenti.DataSource = dt.Select($" KodiAgjentShitje like '%{e.Filter}%' or EmriAgjentShitje like '{e.Filter}'").GetDataTable(dt);
                    btnAgjenti.TextField = "KodiAgjentShitje";
                    btnAgjenti.ValueField = "IdAgjentShitje";
                    btnAgjenti.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    btnAgjenti.DataBind();
                }
            }
        }




        protected void btnAgjenti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback || !Request.Params["__CALLBACKID"].ToString().Contains("btnAgjenti"))
                return;
            if (e.Value == null)
                return;

            ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(IdNdermarrja, IdPerdoruesi, btnAgjenti);
        }

        protected void ASPxScheduler1_InitAppointmentDisplayText(object sender, DevExpress.XtraScheduler.AppointmentDisplayTextEventArgs e)
        {
            //Appointment apt = e.Appointment;
            //e.Text = String.Format("[{0}] {1}", apt.Location, apt.Subject);
            //e.Description = String.Format("Details: {0}", apt.Description);
        }

      

      
    }
}