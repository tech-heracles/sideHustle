<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMRouteAgjenti.aspx.cs" Inherits="PlatinumWeb.CRMRouteAgjenti" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v18.2.Core, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>
<%@ Register Src="~/DynamicDataNew/VerticalAppointmentTemplateCustom.ascx" TagPrefix="uc2" TagName="VerticalAppointmentTemplate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha CRM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/images/CRM/faviconCRM.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/images/CRM/faviconCRM.ico" type="image/ico" />
    <link type="text/css" rel="stylesheet" href="~/js/srcCRM/css/jquery.mmenu.all.css" />
    <link type="text/css" rel="stylesheet" href="AlphaCRM.css" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="css/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/srcCRM/js/jquery.mmenu.min.all.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/CRMRouteAgjenti.aspx-IMB.4.7.js&v49"
        type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $('nav#menu').mmenu({
                classes: "mm-light",
            });
        });
    </script>
</head>
<body>
    <div id="page">
        <div class="header">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 1%;">
                        <a href="#menu"></a>
                    </td>
                    <td style="width: 94%; vertical-align: top;">Route i Agjenteve</td>
                    <td style="width: 5%;">
                        <div id="emriLogout" class="emriLogout">
                            <div id="userInfo">
                                <div id="emri">
                                    <dx:ASPxLabel ID="lblUserEmri" ClientInstanceName="lblUserEmri" runat="server" Text=""
                                        Font-Size="14" ForeColor="White" Font-Names="Calibri">
                                    </dx:ASPxLabel>
                                </div>
                                <div id="logout">
                                    <a style="position: relative; color: white; background-image: none;" class="fa fa-sign-out fa-2x"><i class="fa fa-sign-out  fa-lg"></i>&nbsp;</a>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content">
            <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
                </asp:ScriptManager>
                 
                <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                    Font-Size="9pt" Modal="True" ImagePosition="Top">
                    <LoadingDivStyle Opacity="30">
                    </LoadingDivStyle>
                </dx:ASPxLoadingPanel>
                <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                    ViewStateMode="Enabled">
                </dx:ASPxHiddenField>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td>
                                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                        ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                        OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                        <ClientSideEvents ItemClick="function(s, e) {
	                        menu_click(s,e);
                            }" Init="function(s) {s.SetClientVisible(true);}" />
                                        <ItemImage Height="32px" Width="32px">
                                        </ItemImage>
                                        <SubMenuItemImage Height="16px" Width="16px">
                                        </SubMenuItemImage>
                                        <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
                                            <Paddings PaddingBottom="1px" PaddingTop="9px" />
                                        </ItemStyle>
                                        <SubMenuItemStyle Width="32px">
                                        </SubMenuItemStyle>
                                    </dx:ASPxMenu>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="dvMenu" style="display: none">
                                        <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                                    BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                                    <ClientSideEvents Init="Init_MenuInfo" />
                                                    <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <SubMenuStyle GutterWidth="17px" />
                                                </dx:ASPxMenu>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table width="73%">
                    <tr>
                        <td>
                            <dx:ASPxLabel Wrap="False" ID="lblAgjenti" AssociatedControlID="btnAgjenti" runat="server"
                                Text="Agjenti" ClientInstanceName="lblAgjenti" ForeColor="Black">
                            </dx:ASPxLabel>
                        </td>
                        <%-- </div>
                                    <div id="dvbtnAgjenti">--%>
                        <td>
                            <dx:ASPxComboBox ID="btnAgjenti" Width="100%" runat="server" ClientInstanceName="btnAgjenti"
                                ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" OnItemRequestedByValue="btnAgjenti_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btnAgjenti_ItemsRequestedByFilterCondition"> 
                                <ClientSideEvents ButtonClick="function(s,e){ButtonClickAgjenti('1');}" LostFocus="btnAgjentiLostFocus" CloseUp="btnAgjentiCloseUp" />
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                    ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                        </td>
                        <td width="10%">
                            <dx:ASPxButton runat="server" ForeColor="Transparent" BackColor="Transparent" ToolTip="Mbrapa" ID="btnPrev" EnableTheming="False"
                                RenderMode="Link" Style="margin-left: 0px; border: none; background-color: transparent; margin-left: 30px"
                                ClientInstanceName="btnPrev" AutoPostBack="false" ImageSpacing="0px" Width="23px" Height="23px"
                                HorizontalAlign="Center" AllowFocus="False" FocusRectBorder-BorderStyle="None">
                                <ClientSideEvents Click="Click_btnPrev" />
                                <Image Url="images/CRM/1422991992_arrow-left-01-24.png" Width="32px" Height="32px">
                                </Image>
                            </dx:ASPxButton>
                        </td>
                        <td width="7%">
                            <dx:ASPxButton runat="server" ForeColor="Transparent" BackColor="Transparent" ToolTip="Para" ID="btnNext" EnableTheming="False"
                                RenderMode="Link" Style="margin-left: 0px; border: none; background-color: transparent"
                                ClientInstanceName="btnNext" AutoPostBack="false" ImageSpacing="0px" Width="23px" Height="23px"
                                HorizontalAlign="Center" AllowFocus="False" FocusRectBorder-BorderStyle="None">
                                <ClientSideEvents Click="Click_btnNext" />

                                <Image Url="images/CRM/1422992681_arrow-right-01-32.png">
                                </Image>
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnDetaje" runat="server" Text="Detaje Klienti(Historiku)" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){detajeClick(s,e,'Historiku')}" Init="btnDetajeInit" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnDetaje2" runat="server" Text="Detaje Klienti(Anketa)" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){detajeClick(s,e,'Raporti')}" Init="btnDetajeInit" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>

                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupDateRanges" runat="server" AllowDragging="True" ClientInstanceName="popupDateRanges"
                    CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Klono Takimet"
                    Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="300px"
                    ShowHeader="true" Enabled="True" OnCustomJSProperties="popupDateRanges_CustomJSProperties" OnWindowCallback="popupDateRanges_WindowCallback">
                    <ClientSideEvents EndCallback="popupDateRangesEndCallback" CloseUp="mbyllPopupKlonimi" Shown="klonoPopupShown" />
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">

                            <dx:ASPxPanel EnableHierarchyRecreation="false" runat="server" Width="100%" ID="ASPxPanel1">
                                <PanelCollection>
                                    <dx:PanelContent runat="server">
                                        <div id="klonimInfo">
                                            <dx:ASPxLabel ID="lblInfo" runat="server" ClientInstanceName="lblInfo"></dx:ASPxLabel>
                                        </div>
                                        <div>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="DetyraChkb" ClientInstanceName="DetyraChkb" Text="Klono Detyrat" runat="server">
                                                            <ClientSideEvents CheckedChanged="chkbCheckedChanged" />
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td></td>
                                                    <td style="float: right">

                                                        <dx:ASPxCheckBox ID="KlientChkb" ClientInstanceName="KlientChkb" Text="Klono Klientet" runat="server">
                                                            <ClientSideEvents CheckedChanged="chkbCheckedChanged" />
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                </tr>
                                            </table>

                                            <hr />
                                        </div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 100px">
                                                                <dx:ASPxLabel ID="fromDateStartLbl" runat="server" Text="Nga"></dx:ASPxLabel>
                                                            </td>
                                                            <td style="width: auto">
                                                                <dx:ASPxDateEdit ID="fromDateStart" ClientInstanceName="fromDateStart" Width="200px" runat="server">
                                                                    <ClientSideEvents DateChanged="DateChanged" />
                                                                    <ValidationSettings ErrorDisplayMode="None" ValidationGroup="periudha" RequiredField-IsRequired="true">
                                                                    </ValidationSettings>
                                                                </dx:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <table>

                                                        <tr>
                                                            <td style="width: 100px">
                                                                <dx:ASPxLabel ID="fromDateEndLbl" Width="100%" runat="server" Text="Deri"></dx:ASPxLabel>
                                                            </td>
                                                            <td style="width: auto">
                                                                <dx:ASPxDateEdit ID="fromDateEnd" ClientInstanceName="fromDateEnd" runat="server" Width="200px">
                                                                    <ClientSideEvents DateChanged="DateChanged" />
                                                                    <ValidationSettings ErrorDisplayMode="None" ValidationGroup="periudha" RequiredField-IsRequired="true"></ValidationSettings>
                                                                </dx:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="min-width: 100px; padding-left: 50px">Klono</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 100px">
                                                                <dx:ASPxLabel ID="toDateStartLbl" runat="server" Text="Ne"></dx:ASPxLabel>
                                                            </td>
                                                            <td style="width: auto">
                                                                <dx:ASPxDateEdit ID="toDateStart" ClientInstanceName="toDateStart" runat="server" Width="200px">
                                                                    <ClientSideEvents DateChanged="DateChanged" />
                                                                    <ValidationSettings ErrorDisplayMode="None" ValidationGroup="periudha" RequiredField-IsRequired="true"></ValidationSettings>
                                                                </dx:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 100px">
                                                                <dx:ASPxLabel ID="toDateEndLbl" runat="server" Text="Deri"></dx:ASPxLabel>
                                                            </td>
                                                            <td style="width: auto">
                                                                <dx:ASPxDateEdit ID="toDateEnd" ClientInstanceName="toDateEnd" Width="200px" runat="server">
                                                                    <ClientSideEvents DateChanged="DateChanged" />
                                                                    <ValidationSettings ErrorDisplayMode="None" ValidationGroup="periudha" RequiredField-IsRequired="true"></ValidationSettings>
                                                                </dx:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="text-align: right; padding-top: 30px;">

                                                    <dx:ASPxButton ID="Ruaj" ClientInstanceName="btnKlono" ClientEnabled="false" AutoPostBack="False" CausesValidation="false" runat="server" Text="Ruaj">
                                                        <ClientSideEvents Click="KlonoTakimet" Init="InitBtnKlono" />
                                                    </dx:ASPxButton>
                                                    <dx:ASPxButton ID="Anullo" AutoPostBack="false" runat="server" Text="Anullo">
                                                        <ClientSideEvents Click="Click_Anullo"/>
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>

                <table>
                    <tr>
                        <td>
                            <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" ActiveViewType="Month" Theme="Moderno" ClientIDMode="AutoID" Start="2014-12-28" OnCustomCallback="ASPxScheduler1_CustomCallback" ClientInstanceName="skeduler" Width="100%" OnPopupMenuShowing="ASPxScheduler1_PopupMenuShowing" OnAppointmentRowInserting="SkedulerAgjenti_AppointmentRowInserting" OnAppointmentRowUpdating="ASPxScheduler1_AppointmentRowUpdating" OnAppointmentRowDeleting="ASPxScheduler1_AppointmentRowDeleting" OnCustomErrorText="ASPxScheduler1_CustomErrorText" OnHtmlTimeCellPrepared="ASPxScheduler1_HtmlTimeCellPrepared" OnInitAppointmentDisplayText="ASPxScheduler1_InitAppointmentDisplayText">
                                <ClientSideEvents AppointmentDeleting="RuajTakimeIDneHiddenField" AppointmentDrop="RuajTakimeIDneHiddenField"
                                    MenuItemClicked="function(s,e){MenuItemClicked(s,e)}" 
                                    AppointmentDoubleClick="AppointementDblClick"                                   
                                    AppointmentsSelectionChanged="function(s, e) { OnAppointmentsSelectionChanged(s, e.appointmentIds); }" />
                                <Views>
                                    <DayView ShortDisplayName="Dita" AppointmentDisplayOptions-AppointmentAutoHeight="true" WorkTime-Start="00:00:00" WorkTime-End="01:00:00" ShowWorkTimeOnly="true" AppointmentDisplayOptions-AllDayAppointmentsStatusDisplayType="Bounds">
                                        <TimeRulers>
                                            <cc1:TimeRuler Visible="false"></cc1:TimeRuler>
                                        </TimeRulers>
                                        <AppointmentDisplayOptions EndTimeVisibility="Never" StatusDisplayType="Bounds" StartTimeVisibility="Never" />
                                    </DayView>
                                    <WorkWeekView Enabled="False" AppointmentDisplayOptions-StatusDisplayType="Bounds" AppointmentDisplayOptions-AllDayAppointmentsStatusDisplayType="Bounds">
                                        <TimeRulers>
                                            <cc1:TimeRuler></cc1:TimeRuler>
                                        </TimeRulers>
                                    </WorkWeekView>
                                    <WeekView ShortDisplayName="Java" AppointmentDisplayOptions-StatusDisplayType="Bounds">
                                    </WeekView>
                                    <MonthView CompressWeekend="False" AppointmentDisplayOptions-StartTimeVisibility="Never" AppointmentDisplayOptions-EndTimeVisibility="Never" AppointmentDisplayOptions-StatusDisplayType="Bounds" ShortDisplayName="Muaji">
                                        <MonthViewStyles>
                                            <DateCellBody Height="80px">
                                            </DateCellBody>
                                        </MonthViewStyles>
                                    </MonthView>
                                    <TimelineView Enabled="False" AppointmentDisplayOptions-StatusDisplayType="Bounds">
                                    </TimelineView>
                                </Views>
                                <ClientSideEvents EndCallback="function(s, e) {EndCallbackGrida(s,e);}" />
                                <Styles>
                                    <DateHeader BackColor="White">
                                    </DateHeader>
                                    <DayHeader BackColor="White">
                                    </DayHeader>
                                </Styles>
                                <OptionsForms AppointmentFormTemplateUrl="DynamicDataNew/FieldTemplates/AppointmentCustomForm.ascx" AppointmentInplaceEditorFormTemplateUrl="~/DevExpress/ASPxSchedulerForms/InplaceEditor.ascx" GotoDateFormTemplateUrl="~/DevExpress/ASPxSchedulerForms/GotoDateForm.ascx" RecurrentAppointmentDeleteFormTemplateUrl="~/DevExpress/ASPxSchedulerForms/RecurrentAppointmentDeleteForm.ascx" RecurrentAppointmentEditFormTemplateUrl="~/DevExpress/ASPxSchedulerForms/RecurrentAppointmentEditForm.ascx" RemindersFormTemplateUrl="~/DevExpress/ASPxSchedulerForms/ReminderForm.ascx" />
                                <OptionsToolTips AppointmentDragToolTipUrl="~/DevExpress/ASPxSchedulerForms/AppointmentDragToolTip.ascx" AppointmentToolTipUrl="~/DevExpress/ASPxSchedulerForms/AppointmentToolTip.ascx" SelectionToolTipUrl="~/DevExpress/ASPxSchedulerForms/SelectionToolTip.ascx" />
                            </dxwschs:ASPxScheduler>
                        </td>
                    </tr>
                </table>
                <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="pnlAptSelection" ClientInstanceName="pnlAptDetails" runat="server" Width="100%" HeaderText="Detaje rreth takimit">
                    <ClientSideEvents Init="initpnlAptDetails" />
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div>
                                <table class="OptionsTable">
                                    <tr>
                                        <td style="width: 20%">Klienti/Detyra:
                                        </td>
                                        <td style="width: 80%">
                                            <div id="aptsubj"></div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 20%">Shenime:
                                        </td>
                                        <td>
                                            <div id="aptdesc"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">Te Klienti:
                                        </td>
                                        <td>
                                            <div id="teklienti"></div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
                <div class='my-legend' style="margin-top: 20px;">
                    <div class='legend-scale'>
                        <ul class='legend-labels' style="color: #000">
                            <li><span style='background-color: rgb(255, 194, 190);'></span>Krijuar ne CRM, i paperfunduar</li>
                            <li><span style='background-color: rgb(193, 244, 156)'></span>Krijuar ne CRM, i perfunduar</li>
                            <li><span style='background: white;'></span>Krijuar ne CRM pas dates se sotme</li>
                            <li><span style='background-color: rgb(168, 213, 255)'></span>Krijuar ne Mobile, i paperfunduar</li>
                            <li><span style='background-color: rgb(224, 207, 233);'></span>Krijuar ne Mobile, i perfunduar</li>
                            <li><span style="background-color: rgb(141,232,223)"></span>Takim ne klient prospekt</li>
                        </ul>
                    </div>
                </div>
                <asp:HiddenField ID="hfklienti" runat="server" />
                <asp:HiddenField ID="hfLlojDetyre" runat="server" />
                <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
                </dx:ASPxHiddenField>
                <asp:HiddenField runat="server" ID="SelectedIDs" />
                <asp:UpdatePanel ID="UpdatePanel9" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                            CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID"
                            AutoUpdatePosition="True" Font-Bold="False">
                            <ClientSideEvents CloseUp="function(s,e) { closePopup(s,e); }" />
                            <ContentStyle>
                                <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                                    PaddingTop="1px" />
                            </ContentStyle>
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </form>
        </div>
        <nav id="menu">
            <ul id="ulMenu">
            </ul>
        </nav>
    </div>
</body>
</html>
