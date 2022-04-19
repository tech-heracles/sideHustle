<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMHistoriku.aspx.cs" Inherits="PlatinumWeb.CRMHistoriku" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha CRM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/images/CRM/faviconCRM.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/images/CRM/faviconCRM.ico" type="image/ico" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link type="text/css" rel="stylesheet" href="~/js/srcCRM/css/jquery.mmenu.all.css" />
    <link type="text/css" rel="stylesheet" href="AlphaCRM.css" />
    <link href="css/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/srcCRM/js/jquery.mmenu.min.all.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/CRMHistoriku.aspx-IMB.4.7.js&v49"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('nav#menu').mmenu({
                classes: "mm-light",
            });
        });
    </script>
    <style>
        .bt .dxbButton_Moderno {
            background-image: none !important;
        }
    </style>
</head>
<body>
    <div id="page">
        <div class="header">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 1%;">
                        <a href="#menu"></a>
                    </td>
                    <td style="width: 94%; vertical-align: top;">Historiku</td>
                    <td style="width: 5%;">
                        <div id="emriLogout" class="emriLogout">
                            <div id="userInfo">
                                <div id="emri">
                                    <dx:ASPxLabel ID="lblUserEmri" ClientInstanceName="lblUserEmri" runat="server" Text="" Font-Size="14" ForeColor="White" Font-Names="Calibri"></dx:ASPxLabel>
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
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000"></asp:ScriptManager>
                <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"></dx:ASPxHiddenField>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table Width="100%">
                            <tr>
                                <td>
                                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound" ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True" OnItemClick="ASPxMenu1_ItemClick">
                                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                        <ClientSideEvents ItemClick="function(s, e) {   menu_click(s,e);   }" Init="function(s) {s.SetClientVisible(true);}" />
                                        <ItemImage Height="32px" Width="32px"></ItemImage>
                                        <SubMenuItemImage Height="16px" Width="16px"></SubMenuItemImage>
                                        <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
                                            <Paddings PaddingBottom="1px" PaddingTop="9px" />
                                        </ItemStyle>
                                        <SubMenuItemStyle Width="32px"></SubMenuItemStyle>
                                    </dx:ASPxMenu>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%" BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                                <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <SubMenuStyle GutterWidth="17px" />
                                            </dx:ASPxMenu>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top">
                            <LoadingDivStyle Opacity="30"></LoadingDivStyle>
                        </dx:ASPxLoadingPanel>
                        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientInstanceName="popFshi" CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes" Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="300px" ClientIDMode="AutoID" CssPostfix="Glass">
                            <HeaderStyle>
                                <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                            </HeaderStyle>
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                                    <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?"></dx:ASPxLabel>
                                                <br />
                                                <br />
                                                <div style="text-align: right;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk" Text="Ok">
                                                                    <ClientSideEvents Click="Click_ButtonOk" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                                    <ClientSideEvents Click="function(s, e) { popFshi.Hide(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxPanel>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="dvAktiviteti" style="display: none">
                    <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" TabSpacing="3px" ClientInstanceName="PageControl" Width="100%" ActiveTabIndex="0">
                        <ContentStyle>
                            <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                        </ContentStyle>
                        <TabPages>
                            <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                                <ContentCollection>
                                    <dxw:ContentControl>
                                        <table class="renditKontrolle">
                                            <tbody>
                                                <tr>
                                                    <td class="renditKontrolleCaption">
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" Style="font-size: large" Text="Modeli:"></dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth33">
                                                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" Height="24px" Width="100%" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Style="font-size: medium" AnimationType="None">
                                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                                                            <DropDownButton>
                                                                <Image>
                                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                </Image>
                                                            </DropDownButton>
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td class="renditKontrolleLabelMeWidth33">
                                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi"></dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth33"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dx:ASPxGridView ID="gvHistoriku" SettingsBehavior-ColumnResizeMode="NextColumn" ToolTip="Historiku" ClientInstanceName="gvHistoriku" runat="server" Width="100%" OnDataBound="gvHistoriku_DataBound" OnHeaderFilterFillItems="gvHistoriku_HeaderFilterFillItems" OnCustomCallback="gvHistoriku_CustomCallback" OnAutoFilterCellEditorInitialize="gvHistoriku_AutoFilterCellEditorInitialize">
                                            <Styles>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px"></Header>
                                            </Styles>
                                            <SettingsPager PageSize="15"></SettingsPager>
                                            <ClientSideEvents
                                                RowDblClick="Row_DblClick"
                                                FocusedRowChanged="function(s, e) { mbush=true; }"
                                                SelectionChanged="function(s, e) { mbush=true; }"
                                                BeginCallback="function(s, e) { BeginCallback(s,e); }" />
                                        </dx:ASPxGridView>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="Historiku" Text="Detajet">
                                <ContentCollection>
                                    <dxw:ContentControl ID="content">
                                        <table id="tblBurimet" class="renditKontrolle" style="width:100%;">
                                            <tbody>
                                            </tbody>
                                        </table>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAgjenti" ID="lblAgjenti" runat="server" Text="Agjenti:" ClientInstanceName="lblAgjenti"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtAgjenti" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtAgjenti">
                                            <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere" ValidationExpression="^[\s\S]{0,20}$"></RegularExpression>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKlienti" ID="lblKlienti" runat="server" Text="Klienti:" ClientInstanceName="lblKlienti"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtKlienti" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKlienti">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtVeprimi" ID="lblDtVeprimi" runat="server" Text="Date Veprimi:" ClientInstanceName="lblDtVeprimi"></dx:ASPxLabel>
                                        <dx:ASPxDateEdit ID="dteDtVeprimi" runat="server" ClientInstanceName="dteDtVeprimi" ShowShadow="False" Width="100%">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi" ID="lblPershkrimi" runat="server" Text="Shenime:" ClientInstanceName="lblPershkrimi"></dx:ASPxLabel>
                                        <dx:ASPxMemo ID="txtPershkrimi" runat="server" ClientInstanceName="txtPershkrimi" Rows="3" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxMemo>
                                        <%-- te reja --%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txbKohezgjatja" ID="lblKohezgjatja" runat="server" Text="Kohezgjatja e takimit:" ClientInstanceName="lblKohezgjatja"></dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txbKohezgjatja" ReadOnly="true" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txbKohezgjatja">
                                            <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dtDateFillimRealizimi" ID="lblDateFillimRealizimi" runat="server" Text="Data e fillimit te detyres:" ClientInstanceName="lblDateFillimRealizimi"></dx:ASPxLabel>
                                        <dx:ASPxDateEdit ID="dtDateFillimRealizimi" runat="server" ReadOnly="true" ClientInstanceName="dtDateFillimRealizimi" ShowShadow="False" Width="100%" DisplayFormatString="dd:MM:yyyy hh:mm">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dtDatePerfundimRealizimi" ID="lblDatePerfundimRealizimi" runat="server" Text="Data e perfundimit te detyres:" ClientInstanceName="lblDatePerfundimRealizimi"></dx:ASPxLabel>
                                        <dx:ASPxDateEdit ID="dtDatePerfundimRealizimi" runat="server" ClientInstanceName="dtDatePerfundimRealizimi" DisplayFormatString="dd:MM:yyyy hh:mm"
                                            ShowShadow="False" Width="100%">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btnCaktoNeHarte" ID="lblCaktoNeHarte" runat="server" Text="Cakto ne harte:" ClientInstanceName="lblCaktoNeHarte"></dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btnCaktoNeHarte" runat="server" ClientInstanceName="btnCaktoNeHarte" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%" ReadOnly="true">
                                            <ClientSideEvents ButtonClick="function(s, e){ hapLupeHarte(s, e, 'koordinataTakimi'); }" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btnKoordinatatEKlientit" ID="lblKoordinatatEKlientit" runat="server" Text="Koordinatat e klientit" ClientInstanceName="lblKoordinatatEKlientit"></dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btnKoordinatatEKlientit" runat="server" ClientInstanceName="btnKoordinatatEKlientit" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%" ReadOnly="true">
                                            <ClientSideEvents ButtonClick="function(s, e){ hapLupeHarte(s, e, 'koordinataKlienti'); }" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False"></DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <br />
                                        <br />
                                        <dx:ASPxCallbackPanel EnableHierarchyRecreation="false" ID="DetajetPanel" ClientInstanceName="DetajetPanel" OnCallback="DetajetPanel_Callback" runat="server" Width="100%">
                                            <PanelCollection>
                                                <dx:PanelContent>
                                                    <div style="width: 100%">
                                                        <div style="display: inline-block;">
                                                            <dx:ASPxGridView ID="gvTrupi" runat="server" ClientInstanceName="gvTrupi" OnHtmlRowCreated="gvTrupi_HtmlRowCreated" Width="100%" OnDataBound="gvTrupi_DataBound">
                                                            </dx:ASPxGridView>
                                                        </div>
                                                        <div style="display: inline-block; margin-top: 50px;">
                                                            <dx:ASPxGridView ID="gvDetyrat" runat="server" ClientInstanceName="gvDetyrat" OnHtmlRowCreated="gvDetyrat_HtmlRowCreated" Width="100%" OnDataBound="gvDetyrat_DataBound">
                                                            </dx:ASPxGridView>
                                                        </div>
                                                    </div>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxCallbackPanel>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                        </TabPages>
                        <ClientSideEvents ActiveTabChanged="tabChanged" />
                    </dxtc:ASPxPageControl>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hfKonffillestar" runat="server" />
                        <asp:HiddenField ID="hfLupaLlog" runat="server" />
                        <asp:HiddenField ID="hfLidhur" runat="server" />
                        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:HiddenField ID="hfKontrollet" runat="server" />
                        <asp:HiddenField ID="hfStatusi" runat="server" />
                        <asp:HiddenField ID="hfKodi" runat="server" />
                        <asp:HiddenField ID="hfEmertimi" runat="server" />
                        <asp:HiddenField ID="hfKoha" runat="server" />
                        <asp:HiddenField ID="hfKostoBurimi" runat="server" />
                        <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                        <asp:HiddenField ID="hfKosto" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta"></dx:ASPxHiddenField>
                <asp:UpdatePanel ID="update" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal" CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                            <ClientSideEvents Closing="closing" />
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server"></dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </form>
        </div>
        <nav id="menu">
            <ul id="ulMenu"></ul>
        </nav>
    </div>
</body>
</html>
