<%--
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
--%>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="AppointmentCustomForm" CodeBehind="AppointmentCustomForm.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<script type="text/javascript">
    var identikuesPerPopupKlientFurnitori = 'CRM';
</script>
<div runat="server" id="ValidationContainer">
    <table class="dxscAppointmentForm" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %> style="width: 100%; height: 230px;">
        <tr>
            <td class="dxscDoubleCell" colspan="2" hidden="hidden">
                <table class="dxscLabelControlPair" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
                    <tr>
                        <td class="dxscLabelCell">
                            <dxe:ASPxLabel ID="lblSubject" runat="server" AssociatedControlID="tbSubject">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="dxscControlCell">
                            <dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbSubject" runat="server" Width="100%" Text='<%# ((AppointmentFormTemplateContainer)Container).Subject %>' />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="dxscSingleCell" hidden="hidden">
                <table class="dxscLabelControlPair" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
                    <tr>
                        <td class="dxscLabelCell">
                            <dxe:ASPxLabel ID="lblLocation" runat="server" AssociatedControlID="tbLocation">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="dxscControlCell">
                            <dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbLocation" runat="server" Width="100%" Text='<%# ((AppointmentFormTemplateContainer)Container).Appointment.Location %>' />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="dxscSingleCell" hidden="hidden">
                <table class="dxscLabelControlPair" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
                    <tr>
                        <td class="dxscLabelCell" style="padding-left: 25px;">
                            <dxe:ASPxLabel ID="lblLabel" runat="server" AssociatedControlID="edtLabel" ClientInstanceName="lbl">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="dxscControlCell">
                            <dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtLabel" runat="server" Width="100%" ValueType="System.Int32" DataSource='<%# ((AppointmentFormTemplateContainer)Container).LabelDataSource %>'>
                                <ClientSideEvents Init="function(s, e) { lbl.SetText(window.parent.btnAgjenti.GetValue()); }" />
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="dxscSingleCell">
                <table class="dxscLabelControlPair" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
                    <tr>
                        <td class="dxscLabelCell">
                            <dxe:ASPxLabel ID="lblStartDate" runat="server" AssociatedControlID="edtStartDate" Wrap="false">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="dxscControlCell">
                            <dxe:ASPxDateEdit ID="edtStartDate" runat="server" Width="100%" Date='<%# ((AppointmentFormTemplateContainer)Container).Start %>' EditFormat="Date" DateOnError="Undo" AllowNull="false" EnableClientSideAPI="true">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" EnableCustomValidation="True" Display="Dynamic"
                                    ValidationGroup="DateValidatoinGroup">
                                </ValidationSettings>
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="dxscSingleCell">
                <table class="dxscLabelControlPair" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
                    <tr>
                        <td class="dxscLabelCell" style="padding-left: 25px;">
                            <dxe:ASPxLabel runat="server" ID="lblEndDate" Wrap="false" AssociatedControlID="edtEndDate" />
                        </td>
                        <td class="dxscControlCell">
                            <dxe:ASPxDateEdit ID="edtEndDate" runat="server" Date='<%# ((AppointmentFormTemplateContainer)Container).End %>' EditFormat="Date" Width="100%"  DateOnError="Undo" AllowNull="false" EnableClientSideAPI="true">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" EnableCustomValidation="True" Display="Dynamic"
                                    ValidationGroup="DateValidatoinGroup">
                                </ValidationSettings>
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="dxscSingleCell" hidden="hidden">
                <table class="dxscLabelControlPair" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
                    <tr>
                        <td class="dxscLabelCell">
                            <dxe:ASPxLabel ID="lblStatus" runat="server" AssociatedControlID="edtStatus" Wrap="false">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="dxscControlCell">
                            <dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtStatus" runat="server" Width="100%" ValueType="System.Int32" DataSource='<%# ((AppointmentFormTemplateContainer)Container).StatusDataSource %>' />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="dxscSingleCell" style="padding-left: 22px;" hidden="hidden">
                <table class="dxscLabelControlPair" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
                    <tr>
                        <td style="width: 20px; height: 20px;">
                            <dxe:ASPxCheckBox ClientInstanceName="_dx" ID="chkAllDay" runat="server" Checked='<%# ((AppointmentFormTemplateContainer)Container).Appointment.AllDay %>'>
                            </dxe:ASPxCheckBox>
                        </td>
                        <td style="padding-left: 2px;">
                            <dxe:ASPxLabel ID="lblAllDay" runat="server" AssociatedControlID="chkAllDay" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>

            <% if (CanShowReminders)
               { %>
            <td class="dxscSingleCell">
                <% }
               else
               { %>
            <td class="dxscDoubleCell" colspan="2">
                <% } %>
                <table class="dxscLabelControlPair" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
                    <tr>
                        <td class="dxscLabelCell" >
                            <dxe:ASPxLabel ID="lblResource" runat="server" AssociatedControlID="edtResource">
                            </dxe:ASPxLabel>

                        </td>
                        <td class="dxscControlCell">
                            <% if (ResourceSharing)
                               { %>
                            <dxe:ASPxDropDownEdit ID="ddResource" runat="server" Width="100%" ClientInstanceName="ddResource" Enabled='<%# ((AppointmentFormTemplateContainer)Container).CanEditResource %>' AllowUserInput="False" Visible="False" Height="21px">
                                <DropDownWindowTemplate>
                                    <dxe:ASPxListBox ID="edtMultiResource" runat="server" Width="100%" SelectionMode="CheckColumn" DataSource='<%# ResourceDataSource %>' Border-BorderWidth="0" />
                                </DropDownWindowTemplate>
                            </dxe:ASPxDropDownEdit>
                            <% }
                               else
                               { %>
                            <dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtResource" runat="server" Width="100%" Enabled='<%# ((AppointmentFormTemplateContainer)Container).CanEditResource %>' DataSource='<%# ResourceDataSource %>' Visible="False">
                            </dxe:ASPxComboBox>
                            <dxe:ASPxComboBox ClientInstanceName="ddKlienti" IncrementalFilteringMode="Contains" ID="ddKlient" OnCallback="ddKlient_Callback" runat="server" Width="100%" EnableCallbackMode="true" EnableSynchronization="True" CallbackPageSize="20">
                                <ClientSideEvents TextChanged="MerrVleratNgaCombot" ButtonClick="ButtonClickLupaKlientDetyra"  Init="function(s,e){if(s.GetValue()==undefined)s.PerformCallback($('#hfLlojDetyre').val());}" />
                            </dxe:ASPxComboBox>

                            <% } %>             
                        </td>

                    </tr>
                </table>
            </td>

            <% if (CanShowReminders)
               { %>
            <td class="dxscSingleCell" hidden="hidden">
                <table class="dxscLabelControlPair" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
                    <tr>
                        <td class="dxscLabelCell" style="padding-left: 22px;">
                            <table class="dxscLabelControlPair" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
                                <tr>
                                    <td style="width: 20px; height: 20px;">
                                        <dxe:ASPxCheckBox ID="chkReminder" runat="server">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                    <td style="padding-left: 2px;">
                                        <dxe:ASPxLabel ID="lblReminder" runat="server" AssociatedControlID="chkReminder" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="dxscControlCell" style="padding-left: 3px">
                            <dxe:ASPxComboBox ID="cbReminder" runat="server" Width="100%" DataSource='<%# ((AppointmentFormTemplateContainer)Container).ReminderDataSource %>' />
                        </td>
                    </tr>
                </table>
            </td>
            <% } %>
        </tr>
        <tr>
            <td class="dxscDoubleCell" colspan="2" style="height: 90px;">
                <dxe:ASPxMemo ClientInstanceName="_dx" ID="tbDescription" runat="server" Width="100%" Rows="6" Text='<%# ((AppointmentFormTemplateContainer)Container).Appointment.Description %>' />
            </td>
        </tr>
    </table>
</div>

<dxsc:AppointmentRecurrenceForm ID="AppointmentRecurrenceForm1" runat="server"
    IsRecurring='<%# ((AppointmentFormTemplateContainer)Container).Appointment.IsRecurring %>'
    DayNumber='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceDayNumber %>'
    End='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceEnd %>'
    Month='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceMonth %>'
    OccurrenceCount='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceOccurrenceCount %>'
    Periodicity='<%# ((AppointmentFormTemplateContainer)Container).RecurrencePeriodicity %>'
    RecurrenceRange='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceRange %>'
    Start='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceStart %>'
    WeekDays='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceWeekDays %>'
    WeekOfMonth='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceWeekOfMonth %>'
    RecurrenceType='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceType %>'
    IsFormRecreated='<%# ((AppointmentFormTemplateContainer)Container).IsFormRecreated %>'>
</dxsc:AppointmentRecurrenceForm>

<table <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %> style="width: 100%; height: 35px;">
    <tr>
        <td class="dx-ac" style="width: 100%; height: 100%;" <%= DevExpress.Web.Internal.RenderUtils.GetAlignAttributes(this, "center", null) %>>
            <table class="dxscButtonTable" style="height: 100%">
                <tr>
                    <td class="dxscCellWithPadding">
                        <dxe:ASPxButton runat="server" ID="btnOk" UseSubmitBehavior="false" AutoPostBack="false"
                            EnableViewState="false" Width="91px" EnableClientSideAPI="true">
                            <ClientSideEvents Click="RuajTakim" />
                        </dxe:ASPxButton>
                    </td>
                    <td class="dxscCellWithPadding">
                        <dxe:ASPxButton runat="server" ID="btnCancel" UseSubmitBehavior="false" AutoPostBack="false" EnableViewState="false"
                            Width="91px" CausesValidation="False" EnableClientSideAPI="true" />
                    </td>
                    <td class="dxscCellWithPadding">
                        <dxe:ASPxButton runat="server" ID="btnDelete" UseSubmitBehavior="false"
                            AutoPostBack="false" EnableViewState="false" Width="91px"
                            Enabled='<%# ((AppointmentFormTemplateContainer)Container).CanDeleteAppointment %>'
                            CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %> style="width: 100%;">
    <tr>
        <td class="dx-al" style="width: 100%;" <%= DevExpress.Web.Internal.RenderUtils.GetAlignAttributes(this, "left", null) %>>
            <dxsc:ASPxSchedulerStatusInfo runat="server" ID="schedulerStatusInfo" Priority="1" MasterControlID='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).ControlId %>' />
        </td>
    </tr>
</table>

<script id="dxss_ASPxSchedulerAppoinmentForm" type="text/javascript">

    ASPxAppointmentForm = ASPx.CreateClass(ASPxClientFormBase, {
        Initialize: function () {
            this.controls.edtStartDate.Validation.AddHandler(ASPx.CreateDelegate(this.OnEdtStartDateValidate, this));
            this.controls.edtEndDate.Validation.AddHandler(ASPx.CreateDelegate(this.OnEdtEndDateValidate, this));
            this.controls.edtStartDate.ValueChanged.AddHandler(ASPx.CreateDelegate(this.OnUpdateStartEndValue, this));
            this.controls.edtEndDate.ValueChanged.AddHandler(ASPx.CreateDelegate(this.OnUpdateStartEndValue, this));
            if (this.controls.chkReminder)
                this.controls.chkReminder.CheckedChanged.AddHandler(ASPx.CreateDelegate(this.OnChkReminderCheckedChanged, this));
            if (this.controls.edtMultiResource)
                this.controls.edtMultiResource.SelectedIndexChanged.AddHandler(ASPx.CreateDelegate(this.OnEdtMultiResourceSelectedIndexChanged, this));
        },
        OnBtnOk: function (s, e) {
            e.processOnServer = false;
            var formOwner = this.GetFormOwner();
            if (!formOwner)
                return;
            if (this.controls.AppointmentRecurrenceForm1 && this.IsRecurrenceChainRecreationNeeded() && this.cpHasExceptions) {
                formOwner.ShowMessageBox(this.localization.SchedulerLocalizer.Msg_Warning, this.localization.SchedulerLocalizer.Msg_RecurrenceExceptionsWillBeLost, this.OnWarningExceptionWillBeLostOk.aspxBind(this));
            } else {
                formOwner.AppointmentFormSave();
            }
        },
        IsRecurrenceChainRecreationNeeded: function () {
            var isIntervalChanged = this.primaryIntervalJson != ASPx.Json.ToJson(this.appointmentInterval);
            return isIntervalChanged || this.controls.AppointmentRecurrenceForm1.IsChanged();
        },
        OnWarningExceptionWillBeLostOk: function () {
            this.GetFormOwner().AppointmentFormSave();
        },
        OnEdtMultiResourceSelectedIndexChanged: function (s, e) {
            var resourceNames = new Array();
            var items = s.GetSelectedItems();
            var count = items.length;
            if (count > 0) {
                for (var i = 0; i < count; i++)
                    resourceNames.push(items[i].text);
            }
            else
                resourceNames.push(ddResource.cp_Caption_ResourceNone);
            ddResource.SetValue(resourceNames.join(', '));
        },
        OnEdtStartDateValidate: function (s, e) {
            if (!e.isValid)
                return;
            var startDate = this.controls.edtStartDate.GetDate();
            var endDate = this.controls.edtEndDate.GetDate();
            e.isValid = startDate == null || endDate == null || startDate <= endDate;
            e.errorText = "The Start Date must precede the End Date.";
        },
        OnEdtEndDateValidate: function (s, e) {
            if (!e.isValid)
                return;
            var startDate = this.controls.edtStartDate.GetDate();
            var endDate = this.controls.edtEndDate.GetDate();
            e.isValid = startDate == null || endDate == null || startDate <= endDate;
            e.errorText = "The Start Date must precede the End Date.";
        },
        OnUpdateEndDateTimeValue: function (s, e) {
            var isAllDay = this.controls.chkAllDay.GetValue();
            var date = ASPxSchedulerDateTimeHelper.TruncToDate(this.controls.edtEndDate.GetDate());
            if (isAllDay)
                date = ASPxSchedulerDateTimeHelper.AddDays(date, 1);
            var time = ASPxSchedulerDateTimeHelper.ToDayTime(this.controls.edtEndTime.GetDate());
            var dateTime = ASPxSchedulerDateTimeHelper.AddTimeSpan(date, time);
            this.appointmentInterval.SetEnd(dateTime);
            this.UpdateDateTimeEditors();
            this.Validate();
        },
        OnUpdateStartDateTimeValue: function (s, e) {
            var date = ASPxSchedulerDateTimeHelper.TruncToDate(this.controls.edtStartDate.GetDate());
            var time = ASPxSchedulerDateTimeHelper.ToDayTime(this.controls.edtStartTime.GetDate());
            var dateTime = ASPxSchedulerDateTimeHelper.AddTimeSpan(date, time);
            this.appointmentInterval.SetStart(dateTime);
            this.UpdateDateTimeEditors();
            if (this.controls.AppointmentRecurrenceForm1)
                this.controls.AppointmentRecurrenceForm1.SetStart(dateTime);
            this.Validate();
        },
        OnChkReminderCheckedChanged: function (s, e) {
            var isReminderEnabled = this.controls.chkReminder.GetValue();
            if (isReminderEnabled)
                this.controls.cbReminder.SetSelectedIndex(3);
            else
                this.controls.cbReminder.SetSelectedIndex(-1);
            this.controls.cbReminder.SetEnabled(isReminderEnabled);
        },
        OnChkAllDayCheckedChanged: function (s, e) {
            this.UpdateTimeEditorsVisibility();
            var isAllDay = this.controls.chkAllDay.GetValue();
            this.appointmentInterval.SetAllDay(isAllDay);
            this.UpdateDateTimeEditors();
        },
        UpdateDateTimeEditors: function () {
            var isAllDay = this.controls.chkAllDay.GetValue();
            this.controls.edtStartDate.SetValue(this.appointmentInterval.GetStart());
            var end = this.appointmentInterval.GetEnd();
            if (isAllDay) {
                end = ASPxSchedulerDateTimeHelper.AddDays(end, -1);
            }
            this.controls.edtEndDate.SetValue(end);
            this.controls.edtStartTime.SetValue(this.appointmentInterval.GetStart());
            this.controls.edtEndTime.SetValue(end);
        },
        UpdateTimeEditorsVisibility: function () {
            var isAllDay = this.controls.chkAllDay.GetValue();
            var visible = (isAllDay) ? "none" : "";
            var startRoot = ASPx.GetParentById(this.controls.edtStartTime.GetMainElement(), "edtStartTimeLayoutRoot");
            var endRoot = ASPx.GetParentById(this.controls.edtEndTime.GetMainElement(), "edtEndTimeLayoutRoot");
            startRoot.style.display = visible;
            endRoot.style.display = visible;
        },
        Validate: function () {
            this.isValid = ASPxClientEdit.ValidateEditorsInContainer(null);
            this.controls.btnOk.SetEnabled(this.isValid && this.isRecurrenceValid);
        },
        OnRecurrenceRangeControlValidationCompleted: function (s, e) {
            if (!this.controls.AppointmentRecurrenceForm1)
                return;
            this.isRecurrenceValid = this.controls.AppointmentRecurrenceForm1.IsValid();
            this.controls.btnOk.SetEnabled(this.isValid && this.isRecurrenceValid);
        }
    });
</script>