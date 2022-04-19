/*
{************************************************************************************}
{                                                                                    }
{   DO NOT MODIFY THIS FILE!                                                         }
{                                                                                    }
{   It will be overwritten without prompting when a new version becomes              }
{   available. All your changes will be lost.                                        }
{                                                                                    }
{   This file contains the default template and is required for the form             }
{   rendering. Improper modifications may result in incorrect behavior of            }
{   the appointment form.                                                            }
{                                                                                    }
{   In order to create and use your own custom template, perform the following       }
{   steps:                                                                           }
{       1. Save a copy of this file with a different name in another location.       }
{       2. Specify the file location as the 'OptionsForms.AppointmentFormTemplateUrl'}
{          property of the ASPxScheduler control.                                    }
{       3. If you need custom fields to be displayed and processed, you should       }
{          accomplish steps 4-9; otherwise, go to step 10.                           }
{       4. Create a class, derived from the AppointmentFormTemplateContainer,        }
{          containing custom properties. This class definition can be located        }
{          within a class file in the App_Code folder.                               }
{       5. Replace AppointmentFormTemplateContainer references in the template       }
{          page with the name of the class you've created in step 4.                 }
{       6. Handle the AppointmentFormShowing event to create an instance of the      }
{          template Container class, defined in step 4, and specify it as the        }
{          destination Container instead of the default one.                         }
{       7. Define a class, which inherits from the                                   }
{          DevExpress.Web.ASPxScheduler.Internal.AppointmentFormController.          }
{          This class provides data exchange between the form and the appointment.   }
{          You should override ApplyCustomFieldsValues() method of the base class.   }
{       8. Define a class, which inherits from the                                   }
{          DevExpress.Web.ASPxScheduler.Internal.AppointmentFormSaveCallbackCommand. }
{          This class creates an instance of the AppointmentFormController inheritor }
{          (defined in step 7) via the CreateAppointmentFormController method and    }
{          overrides the AssignControllerValues method  of the base class to collect }
{          user data from the form's editors.                                        }
{       9. Handle the BeforeExecuteCallbackCommand event. The event handler code     }
{          should create an instance of the class defined in step 8, and specify it  }
{          as the destination command instead of the default one.                    }
{      10. Modify the overall appearance of the page and its layout.                 }
{                                                                                    }
{************************************************************************************}
*/
using System;
using System.Web.UI;
using DevExpress.XtraScheduler;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using System.Collections;
using System.Collections.Generic;
using DevExpress.XtraScheduler.Localization;
using DevExpress.Web.ASPxScheduler.Localization;
using DevExpress.Utils;
using System.Web.UI.WebControls;
using DbCore;
using System.Data;
using DevExpress.Utils.Localization;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

public partial class AppointmentCustomForm : SchedulerFormControl
{
    public override string ClassName { get { return "ASPxAppointmentForm"; } }

    public bool CanShowReminders
    {
        get
        {
            return ((AppointmentFormTemplateContainer)Parent).Control.Storage.EnableReminders;
        }
    }

    public bool ResourceSharing
    {
        get
        {
            return ((AppointmentFormTemplateContainer)Parent).Control.Storage.ResourceSharing;
        }
    }

    public IEnumerable ResourceDataSource
    {
        get
        {
            return ((AppointmentFormTemplateContainer)Parent).ResourceDataSource;
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Localize();
        tbSubject.Focus();
    }

    void Localize()
    {
        lblSubject.Text = "Subjekti";
        lblLocation.Text = "Vendndodhja";
        lblLabel.Text = "Etiketa";
        lblStartDate.Text = "Data Fillimit";
        lblEndDate.Text = "Data Mbarimit";
        lblStatus.Text = "Statusi";
        lblAllDay.Text = "Gjithe Diten";
        lblResource.Text = "Veprimi";
        //LlojDetyre.Text = "Lloj Detyre";

        if (CanShowReminders)
            lblReminder.Text = ASPxSchedulerLocalizer.GetString(ASPxSchedulerStringId.Form_Reminder);
        btnOk.Text = ASPxSchedulerLocalizer.GetString(ASPxSchedulerStringId.Form_ButtonOk);
        btnCancel.Text = ASPxSchedulerLocalizer.GetString(ASPxSchedulerStringId.Form_ButtonCancel);
        btnDelete.Text = ASPxSchedulerLocalizer.GetString(ASPxSchedulerStringId.Form_ButtonDelete);
        btnOk.Wrap = DefaultBoolean.False;
        btnCancel.Wrap = DefaultBoolean.False;
        btnDelete.Wrap = DefaultBoolean.False;






        //clsFunksione.mbushComboKategoriDetyre(ddLlojDetyre);
        //kjo cmb do jete edhe per klient edhe per detyra
        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(ddKlient);
        ConfigureAspxComboBox.shtoKolonaPerDetyraOseKlient(ddKlient);

    }

    public override void DataBind()
    {
        base.DataBind();

        AppointmentFormTemplateContainer container = (AppointmentFormTemplateContainer)Parent;
        Appointment apt = container.Appointment;
        edtLabel.SelectedIndex = apt.LabelId;
        edtStatus.SelectedIndex = apt.StatusId;
        if (apt.Id != null)
        {
            DbCore.DbCRM.clsSkeduler sked = new DbCore.DbCRM.clsSkeduler();
            sked.mbushTakim(int.Parse(apt.Id.ToString()));

            //ddLlojDetyre.Value=sked.Lloji;
            mbushComboVeprimi(sked.Lloji);
            ddKlient.Value = sked.IdKlienti;

        }
            PopulateResourceEditors(apt, container);

            AppointmentRecurrenceForm1.Visible = false;

            if (apt.HasReminder)
            {
                cbReminder.Value = apt.Reminder.TimeBeforeStart.ToString();
                chkReminder.Checked = true;
            }
            else
            {
                cbReminder.ClientEnabled = false;
            }
        
        //btnOk.ClientSideEvents.Click = container.SaveHandler;
        btnCancel.ClientSideEvents.Click = container.CancelHandler;
        btnDelete.ClientSideEvents.Click = container.DeleteHandler;
        JSProperties.Add("cpHasExceptions", apt.HasExceptions);
        //btnDelete.Enabled = !container.IsNewAppointment;
    }

    private void PopulateResourceEditors(Appointment apt, AppointmentFormTemplateContainer container)
    {
        if (ResourceSharing)
        {
            ASPxListBox edtMultiResource = ddResource.FindControl("edtMultiResource") as ASPxListBox;
            if (edtMultiResource == null)
                return;
            SetListBoxSelectedValues(edtMultiResource, apt.ResourceIds);
            List<String> multiResourceString = GetListBoxSelectedItemsText(edtMultiResource);
            string stringResourceNone = SchedulerLocalizer.GetString(SchedulerStringId.Caption_ResourceNone);
            ddResource.Value = stringResourceNone;
            if (multiResourceString.Count > 0)
                ddResource.Value = String.Join(", ", multiResourceString.ToArray());
            ddResource.JSProperties.Add("cp_Caption_ResourceNone", stringResourceNone);
        }
        else
        {
            if (!Object.Equals(apt.ResourceId, EmptyResourceId.Id))
                edtResource.Value = apt.ResourceId.ToString();
            else
                edtResource.Value = SchedulerIdHelper.EmptyResourceId;
        }
    }
    List<String> GetListBoxSelectedItemsText(ASPxListBox listBox)
    {
        List<String> result = new List<string>();
        foreach (ListEditItem editItem in listBox.Items)
        {
            if (editItem.Selected)
                result.Add(editItem.Text);
        }
        return result;
    }


    void SetListBoxSelectedValues(ASPxListBox listBox, IEnumerable values)
    {
        listBox.Value = null;
        foreach (object value in values)
        {
            ListEditItem item = listBox.Items.FindByValue(value.ToString());
            if (item != null)
                item.Selected = true;
        }
    }

    protected override void PrepareChildControls()
    {
        AppointmentFormTemplateContainer container = (AppointmentFormTemplateContainer)Parent;
        ASPxScheduler control = container.Control;

        AppointmentRecurrenceForm1.EditorsInfo = new EditorsInfo(control, control.Styles.FormEditors, control.Images.FormEditors, control.Styles.Buttons);
        base.PrepareChildControls();
    }

    protected override ASPxEditBase[] GetChildEditors()
    {
        ASPxEditBase[] edits = new ASPxEditBase[] {
            lblSubject, tbSubject,
            lblLocation, tbLocation,
            lblLabel, edtLabel,
            lblStartDate, edtStartDate,
            lblEndDate, edtEndDate,
            lblStatus, edtStatus,
            lblAllDay, chkAllDay,
            lblResource, edtResource,
            tbDescription,
            ddKlient,ddResource,
            //ddLlojDetyre,ddResource1,
             GetMultiResourceEditor()
        };

        return edits;
    }

    ASPxEditBase GetMultiResourceEditor()
    {
        if (ddResource != null)
            return ddResource.FindControl("edtMultiResource") as ASPxEditBase;
        return null;
    }

    protected override ASPxButton[] GetChildButtons()
    {
        ASPxButton[] buttons = new ASPxButton[] {
            btnOk, btnCancel, btnDelete
        };
        return buttons;
    }

    protected override Control[] GetChildControls()
    {
        return new Control[] { ValidationContainer, AppointmentRecurrenceForm1 };
    }

    protected override WebControl GetDefaultButton()
    {
        return btnOk;
    }

    protected void ddLlojDetyre_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {

    }

    protected void ddKlient_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter != "")
        {
            if (e.Parameter.Contains("rimbush"))
            {
                string[] parameters = e.Parameter.Split(';');
                CacheLayer.GlobalCacheManager.MySessionCache["AppFormKF"] = null;
                mbushComboVeprimi(int.Parse(parameters[2]));
                ddKlient.Value = int.Parse(parameters[1]);
                return;
            }
            mbushComboVeprimi(int.Parse(e.Parameter));
        }
    }


    protected void ddKlient_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        //if (ddLlojDetyre.Value.Equals("1"))//klient
        //{
        //    int idagjenti = int.Parse(mySessionObjects.merrObjectNgaSesioni(Session).ToString());
        //    DbCore.DbAdmin.clsAgjentShitje agjenti = new DbCore.DbAdmin.clsAgjentShitje(idagjenti);

        //    clsFunksione.mbushComboKlientFurnitori(agjenti.IdPerdoruesMobile, mySessionObjects.merrIdNdermarrjeSesioni(Session), ddKlient, 1, true);
        //    //clsFunksione.shtoKolonaPerKF(ddKlient);
        //}
        //else if (ddLlojDetyre.Value.Equals("2"))//detyre
        //{
        //    clsFunksione.mbushComboDetyra(mySessionObjects.merrIdNdermarrjeSesioni(Session), ddKlient);
        //    // clsFunksione.shtoKolonaPerDetyra(ddLlojDetyre);
        //}
    }

    protected void ddKlient_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        //if (ddLlojDetyre.Value.Equals("1"))//klient
        //{
        //    int idagjenti = int.Parse(mySessionObjects.merrObjectNgaSesioni(Session).ToString());
        //    DbCore.DbAdmin.clsAgjentShitje agjenti = new DbCore.DbAdmin.clsAgjentShitje(idagjenti);
        //    clsFunksione.shtoKolonaPerKF(ddKlient);
        //    //clsFunksione.mbushComboKlientFurnitori(agjenti.IdPerdoruesMobile, mySessionObjects.merrIdNdermarrjeSesioni(Session), ddKlient, 1, true);
        //    DbCore.DbKontabiliteti.colKlienteFurnitore.merrSipasKFNdermarrjesAndAutorizimeDTLupe(mySessionObjects.merrIdNdermarrjeSesioni(Session), agjenti.IdPerdoruesi, true);

        //}
        //else if (ddLlojDetyre.Value.Equals("2"))//detyre
        //{
        //    //clsFunksione.mbushComboDetyra(mySessionObjects.merrIdNdermarrjeSesioni(Session), ddKlient);
        //    clsFunksione.shtoKolonaPerDetyra(ddLlojDetyre);
        //    DbCore.DbCRM.colDetyra.merrDetyratPerNdermarrje(mySessionObjects.merrIdNdermarrjeSesioni(Session));


        //}
    }
    private void mbushComboVeprimi(int lloji)
    {

        DataTable dataSource = null;

        if (lloji == 1)
        {
            int idagjenti = int.Parse(mySessionObjects.merrObjectNgaSesioni(Session).ToString());
            DbCore.DbAdmin.clsAgjentShitje agjenti = new DbCore.DbAdmin.clsAgjentShitje(idagjenti);

            if (!Page.IsPostBack)
            {
                CacheLayer.GlobalCacheManager.MySessionCache["AppFormKF"] = DbCore.DbKontabiliteti.colKlienteFurnitore.mbushKlienteOseFurnitore(true, mySessionObjects.merrIdNdermarrjeSesioni(Session), agjenti.IdPerdoruesMobile, true);
                ndryshoFieldNamePerKf(dataSource);

            }
            dataSource = CacheLayer.GlobalCacheManager.MySessionCache["AppFormKF"] as DataTable;
            if (dataSource == null)
            {
                dataSource = DbCore.DbKontabiliteti.colKlienteFurnitore.mbushKlienteOseFurnitore(true, mySessionObjects.merrIdNdermarrjeSesioni(Session), agjenti.IdPerdoruesMobile, true);
                CacheLayer.GlobalCacheManager.MySessionCache["AppFormKF"] = dataSource;
                ndryshoFieldNamePerKf(dataSource);
            }
        }
        else
        {
            if (!Page.IsPostBack)
            {
                CacheLayer.GlobalCacheManager.MySessionCache["AppFormDetyra"] = DbCore.DbCRM.colDetyra.merrDetyratPerNdermarrjeKategoriAutorizim(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session), kategoria: 1);
            }
            dataSource = CacheLayer.GlobalCacheManager.MySessionCache["AppFormDetyra"] as DataTable;
            if (dataSource == null)
            {
                dataSource = DbCore.DbCRM.colDetyra.merrDetyratPerNdermarrjeKategoriAutorizim(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session), kategoria: 1);
                CacheLayer.GlobalCacheManager.MySessionCache["AppFormDetyra"] = dataSource;
                ndryshoFieldNamePerDetyra(dataSource);
            }


        }
        dataSource.AcceptChanges();
        ddKlient.DataSource = dataSource;
        ddKlient.DataBind();
    }
    private static void ndryshoFieldNamePerDetyra(DataTable dataSource)
    {
        dataSource.Columns["IdDetyra"].ColumnName = "Id";
        dataSource.Columns["Kodi"].ColumnName = "Kodi";
        dataSource.Columns["Pershkrimi"].ColumnName = "Emertimi";
    }
    private static void ndryshoFieldNamePerKf(DataTable dataSource)
    {
        dataSource.Columns["IdKlientFurnitor"].ColumnName = "Id";
        dataSource.Columns["KodKlientFurnitor"].ColumnName = "Kodi";
        dataSource.Columns["EmertimiKF"].ColumnName = "Emertimi";
    }
    protected override void PrepareLocalization(SchedulerLocalizationCache localizationCache)
    {
        localizationCache.Add(SchedulerStringId.Msg_RecurrenceExceptionsWillBeLost);
        localizationCache.Add(SchedulerStringId.Msg_Warning);
    }
}