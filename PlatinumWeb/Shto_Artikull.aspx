<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_Artikull.aspx.cs"
    Inherits="PlatinumWeb.Shto_Artikull" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Src="~/ucFushatShtese.ascx" TagPrefix="uc1" TagName="ucFushatShtese" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="js/css/le-frog/jquery-ui.css" media="screen" rel="stylesheet" type="text/css"
        runat="server" id="themeJQuery" />
		<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/jquery.ui.datepicker-sq.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/aspx.js/Shto_Artikull.aspx-IMB.2.1.js&v76"
        type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">


        <div>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
            </dx:ASPxGlobalEvents>
            <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
            </asp:ScriptManager>
             <asp:updatepanel runat="server">
                 <ContentTemplate>
                    <dx:ASPxButton runat="server" ID="pubSubButton" OnClick="SendItemsToPubSub" AutoPostBack="false"></dx:ASPxButton>
                 </ContentTemplate>
             </asp:updatepanel>
            <dx:ASPxHiddenField ID="hfKushtet" runat="server">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
            </dx:ASPxHiddenField>
            <asp:HiddenField ID="hfTmpColMag" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                    ImageSpacing="7px" OnItemClick="ASPxMenu1_ItemClick">
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                    <ClientSideEvents ItemClick="function(s, e) { menu_click(s,e); }"
                                        Init="function(s) {s.SetClientVisible(true);}" />
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
                                <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                            BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <SubMenuStyle GutterWidth="17px" />
                                        </dx:ASPxMenu>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel"
                        Font-Size="9pt" Modal="True" ImagePosition="Top">
                        <LoadingDivStyle Opacity="30">
                        </LoadingDivStyle>
                    </dx:ASPxLoadingPanel>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientInstanceName="popFshi"
                        CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes"
                        Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        Width="300px" ClientIDMode="AutoID" CssPostfix="Glass">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?" ClientInstanceName="lblMsgbox">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                OnClick="ButtonOk_Click2" Text="Ok">
                                                                <ClientSideEvents Click="function(s, e) {   popFshi.Hide();Utils.shfaqLoadingGif();}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                                <ClientSideEvents Click="function(s, e) {popFshi.Hide();e.processOnServer = false;}" />
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
            <div id="dvArtikulli" style="display: none">
                <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
                    TabSpacing="3px" Width="100%" Height="600px" ActiveTabIndex="3">
                    <%--  <ContentStyle>
						<Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
					</ContentStyle>--%>
                    <%--   <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />--%>
                    <TabPages>
                        <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl1" runat="server">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <table class="CustomRenditKontrolleDy2">
                                                    <tbody>
                                                        <tr>
                                                            <td class="renditKontrolleCaption">
                                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                                    runat="server" ClientIDMode="AutoID" Text="Modeli:">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth75">
                                                                <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                                    ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
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
                                                            <td class="renditKontrolleCellMeWidth25">
                                                                <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi"
                                                                    ClientInstanceName="lblKonfigurimi">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td id="tblKontrolleTeKonfigurueshmeTabPare">
                                                <table id="tblKonfig" class="CustomRenditKontrolleNje" style="grid-flow: columns;">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                                <dx:ASPxLabel ID="lblGjendje" runat="server" AssociatedControlID="cbGjendje" ClientInstanceName="lblGjendje"
                                                    Wrap="False" Text="Shfaq gjendjen e artikujve">
                                                </dx:ASPxLabel>
                                                <dx:ASPxCheckBox ID="cbGjendje" runat="server" ClientInstanceName="cbGjendje" Width="100%"
                                                    RightToLeft="False" Layout="Flow">
                                                    <ClientSideEvents CheckedChanged="function (s,e){ MerrGjendjeKosto(); }" />
                                                </dx:ASPxCheckBox>
                                                <dx:ASPxLabel ID="lblKosto" runat="server" AssociatedControlID="cbKosto" ClientInstanceName="lblKosto"
                                                    Wrap="False" Text="Shfaq koston e artikullit">
                                                </dx:ASPxLabel>
                                                <dx:ASPxCheckBox ID="cbKosto" runat="server" ClientInstanceName="cbKosto" Width="100%"
                                                    RightToLeft="False" Layout="Flow">
                                                    <ClientSideEvents CheckedChanged="function (s,e){ MerrGjendjeKosto(); }" />
                                                </dx:ASPxCheckBox>
                                                <dx:ASPxLabel ID="lblCmime" runat="server" AssociatedControlID="cbCmime" ClientInstanceName="lblCmime"
                                                    Wrap="False" Text="Shfaq cmimet">
                                                </dx:ASPxLabel>
                                                <dx:ASPxCheckBox ID="cbCmime" runat="server" ClientInstanceName="cbCmime" Width="100%"
                                                    RightToLeft="False" Layout="Flow">
                                                    <ClientSideEvents CheckedChanged="function (s,e){ MerrGjendjeKosto(); }" />
                                                </dx:ASPxCheckBox>
                                                <dx:ASPxLabel ID="lblCmimeMeTvsh" runat="server" AssociatedControlID="cbCmimeMeTvsh" ClientInstanceName="lblCmimeMeTvsh"
                                                    Wrap="False" Text="Shfaq cmimet me TVSH">
                                                </dx:ASPxLabel>
                                                <dx:ASPxCheckBox ID="cbCmimeMeTvsh" runat="server" ClientInstanceName="cbCmimeMeTvsh" Width="100%"
                                                    RightToLeft="False" Layout="Flow">
                                                    <ClientSideEvents CheckedChanged="function (s,e){ MerrGjendjeKosto(); }" />
                                                </dx:ASPxCheckBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <dx:ASPxGridView ID="ASPxGridView_Artikull" ClientInstanceName="ASPxGridView_Artikull" EnableCallBacks="true"
                                        runat="server" Width="100%" OnCustomJSProperties="ASPxGridView_Artikull_CustomJSProperties"
                                        OnDataBound="ASPxGridView_Artikull_DataBound" OnAfterPerformCallback="ASPxGridView_Artikull_AfterPerformCallback"
                                        OnCustomCallback="ASPxGridView_Artikull_CustomCallback" OnHeaderFilterFillItems="ASPxGridView_Artikull_HeaderFilterFillItems"
                                        OnProcessColumnAutoFilter="ASPxGridView_Artikull_ProcessColumnAutoFilter">
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>

                                        <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                            FocusedRowChanged="function(s, e) { onNdryshimFokusi(); }"
                                            SelectionChanged="function(s, e) { }"
                                            BeginCallback="function(s, e) {	BeginCallback(s,e); }" />

                                        <SettingsPager PageSize="15"></SettingsPager>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView_Artikull"
                                        ExportedRowType="Selected" />
                                    <br />
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Informacion" Text="Informacion">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl3" runat="server">
                                    <table id="tblInformacion" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblKodi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtKodi">--%>
                                    <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodi"
                                            Init="function(s, e) { s.Focus(); }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="True" RegularExpression-ValidationExpression="^[\s\S]{0,50}$"
                                            RegularExpression-ErrorText="Kodi nuk duhet te jete me shume se 50 karaktere">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 50 karaktere" ValidationExpression="^[\s\S]{0,50}$"></RegularExpression>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblPershkrimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi" ID="lblPershkrimi"
                                        runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPershkrimi">--%>
                                    <dx:ASPxMemo ID="txtPershkrimi" Rows="3" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtPershkrimi" MaxLength="1000">
                                        <ClientSideEvents TextChanged="TextChanged_txtPershkrimi" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>

                                    <%--</div>--%>
                                    <%--<div id="dvlblPershkrimiAng">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimiAng" ID="lblPershkrimiAng"
                                        runat="server" Text="Pershkrimi 2:" ClientInstanceName="lblPershkrimiAng">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPershkrimiAng">--%>
                                    <dx:ASPxMemo ID="txtPershkrimiAng" runat="server" Width="100%" Rows="3" AutoPostBack="false"
                                        ClientInstanceName="txtPershkrimiAng">
                                        <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>

                                    <%--<div id="dvlbtxtPershkrimiFurnitori">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimiFurnitori" ID="lblPershkrimiFurnitori"
                                        runat="server" Text="Pershkrimi te furnitori:" ClientInstanceName="lblPershkrimiFurnitori">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPershkrimiFurnitori">--%>
                                    <dx:ASPxMemo ID="txtPershkrimiFurnitori" runat="server" Width="100%" Rows="3" AutoPostBack="false"
                                        ClientInstanceName="txtPershkrimiFurnitori">
                                        <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                    <%--<div id="dvlbtxtSiperfaqjaM2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtSiperfaqjaM2" ID="lblSiperfaqjaM2"
                                        runat="server" Text="Sipërfaqja në m2:" ClientInstanceName="lblSiperfaqjaM2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtSiperfaqjaM2">--%>
                                    <dx:ASPxTextBox ID="txtSiperfaqjaM2" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtSiperfaqjaM2">
                                        <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlbtxtNrKontrate">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrKontrate" ID="lblNrKontrate"
                                        runat="server" Text="Nr. Kontrate:" ClientInstanceName="lblNrKontrate">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtNrKontrate">--%>
                                    <dx:ASPxTextBox ID="txtNrKontrate" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtNrKontrate">
                                        <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>

                                    <%--<div id="dvlbtxtNrPasurie">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrPasurie" ID="lblNrPasurie"
                                        runat="server" Text="Nr. Pasurie:" ClientInstanceName="lblNrPasurie">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtNrPasurie">--%>
                                    <dx:ASPxTextBox ID="txtNrPasurie" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtNrPasurie">
                                        <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlbtxtZonaKadastrale">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtZonaKadastrale" ID="lblZonaKadastrale"
                                        runat="server" Text="Zona Kadastrale:" ClientInstanceName="lblZonaKadastrale">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtZonaKadastrale">--%>
                                    <dx:ASPxTextBox ID="txtZonaKadastrale" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtZonaKadastrale">
                                        <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>

                                    <%--<div id="dvlbtxtShasia">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShasia" ID="lblShasia"
                                        runat="server" Text="Shasia:" ClientInstanceName="lblShasia">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtShasia">--%>
                                    <dx:ASPxTextBox ID="txtShasia" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtShasia">
                                        <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>

                                    <%--<div id="dvlbtxtMarka">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMarka" ID="lblMarka"
                                        runat="server" Text="Marka:" ClientInstanceName="lblMarka">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtMarka">--%>
                                    <dx:ASPxTextBox ID="txtMarka" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtMarka">
                                        <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlbtxtModeli">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtModeli" ID="lblModeliAsete"
                                        runat="server" Text="Modeli:" ClientInstanceName="lblModeliAsete">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtModeli">--%>
                                    <dx:ASPxTextBox ID="txtModeli" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtModeli">
                                        <ClientSideEvents TextChanged="function(s, e) { }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlbtxtVitProdhimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVitProdhimi" ID="lblVitProdhimi"
                                        runat="server" Text="Vit prodhimi:" ClientInstanceName="lblVitProdhimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtVitProdhimi">--%>
                                    <dx:ASPxTextBox ID="txtVitProdhimi" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtVitProdhimi">
                                        <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlbtxtTeDhenaTeknika">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTeDhenaTeknika" ID="lblTeDhenaTeknika"
                                        runat="server" Text="Të dhena teknika:" ClientInstanceName="lblTeDhenaTeknika">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtTeDhenaTeknika">--%>
                                    <dx:ASPxTextBox ID="txtTeDhenaTeknika" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtTeDhenaTeknika">
                                        <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblAktiv">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAktiv" ID="lblAktiv" runat="server"
                                        Text="Aktiv:" ClientInstanceName="lblAktiv">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbAktiv">--%>
                                    <dx:ASPxCheckBox ID="cbAktiv" runat="server" ClientInstanceName="cbAktiv" Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                    <%--<div id="dvlblPerPershore">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbPerPershore" ID="lblPerPershore" runat="server"
                                        Text="Per Peshore:" ClientInstanceName="lblPerPershore">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbPerPershore">--%>
                                    <dx:ASPxCheckBox ID="cbPerPershore" runat="server" ClientInstanceName="cbPerPershore" Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbMeBarkodLogjik" ID="lblMeBarkodLogjik" runat="server"
                                        Text="Aktiv:" ClientInstanceName="lblMeBarkodLogjik">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbMeBarkodLogjik" runat="server" ClientInstanceName="cbMeBarkodLogjik" Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtSkemeBarkodi" ID="lblSkemaBarkodit" runat="server"
                                        Text="Origjina e mallit:" ClientInstanceName="lblSkemaBarkodit">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtSkemeBarkodi" runat="server" Width="100%" ClientInstanceName="txtSkemeBarkodi">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbMeSerial" ID="lblMeSerial" runat="server"
                                        Text="Me serial:" ClientInstanceName="lblMeSerial">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbAktiv">--%>
                                    <dx:ASPxCheckBox ID="cbMeSerial" runat="server" ClientInstanceName="cbMeSerial" Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ClientSideEvents CheckedChanged="function (s,e){if($('#hfShtimModifikim').val()=='modifikim') btnSerial.SetEnabled(s.GetChecked()); else btnSerial.SetEnabled(false)}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxButton ID="btnSerial" runat="server" Text="Seriale"
                                        Width="100%" ClientVisible="false" ClientInstanceName="btnSerial" CausesValidation="False"
                                        AutoPostBack="False">
                                        <ClientSideEvents Click="function (s,e){ Seriali_Click(); }" />
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxButton>
                                    <%--</div>--%>
                                    <%--<div id="dvlblKodbari">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKodbari" ID="lblKodbari" runat="server"
                                        Text="Kodbari:" ClientInstanceName="lblKodbari">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtneKodbari">--%>
                                    <dx:ASPxButtonEdit ID="btneKodbari" runat="server" ClientInstanceName="btneKodbari"
                                        Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { Kodbare_Click(); }"
                                            TextChanged="function(s, e) {textChangedKodbari(s,e); }" />
                                        <Buttons>
                                            <dx:EditButton>
                                            </dx:EditButton>
                                        </Buttons>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="false"
                                            ValidationGroup="entries1">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxButtonEdit>
                                    <%--</div>--%>
                                    <%--<div id="dvlblOrigjina">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtOrigjina" ID="lblOrigjina" runat="server"
                                        Text="Origjina e mallit:" ClientInstanceName="lblOrigjina">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtOrigjina">--%>
                                    <dx:ASPxTextBox ID="txtOrigjina" runat="server" Width="100%" ClientInstanceName="txtOrigjina">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLloji">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji" runat="server"
                                        Text="Lloji:" ClientInstanceName="lblLloji">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbLloji">--%>
                                    <dx:ASPxComboBox ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblAutorizimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAutorizimi" ID="lblAutorizimi"
                                        runat="server" Text="Nivel Autorizimi:" ClientInstanceName="lblAutorizimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbAutorizimi">--%>
                                    <div>
                                        <select id="cmbAutorizimi">
                                        </select>
                                        <asp:HiddenField ID="cmbAutorizimiHf" ClientIDMode="Static" runat="server" />

                                    </div>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbPerTransferim" ID="lblPerTransferim"
                                        runat="server" Text="Artikull per transferim:" ClientInstanceName="lblPerTransferim">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbProdhimMePorosi">--%>
                                    <dx:ASPxCheckBox ID="cbPerTransferim" runat="server" ClientInstanceName="cbPerTransferim">
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbLoan" ID="lblLoan"
                                        runat="server" Text="Loan:" ClientInstanceName="lblLoan">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbProdhimMePorosi">--%>
                                    <dx:ASPxCheckBox ID="cbLoan" runat="server" ClientInstanceName="cbLoan">
                                    </dx:ASPxCheckBox>
                                    <%--</div>--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbDhurate" ID="lblDhurate"
                                        runat="server" Text="Artikull Dhurate:" ClientInstanceName="lblDhurate">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbProdhimMePorosi">--%>
                                    <dx:ASPxCheckBox ID="cbDhurate" runat="server" ClientInstanceName="cbDhurate">
                                        <ClientSideEvents CheckedChanged="function (s,e){Dhurate(true)}" />
                                    </dx:ASPxCheckBox>
                                    <%--</div>--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAplikim" ID="lblAplikim" runat="server"
                                        Text="Aplikim dhurate:" ClientInstanceName="lblAplikim">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbLloji">--%>
                                    <dx:ASPxComboBox ID="cmbAplikim" runat="server" ClientInstanceName="cmbAplikim" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function (s,e){Aplikim()}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPike" ID="lblPike" runat="server"
                                        Text="Pike:" ClientInstanceName="lblPike">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtMinimumi">--%>
                                    <dx:ASPxTextBox ID="txtPike" runat="server" Width="100%" ClientInstanceName="txtPike">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblMaksimum">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVlere" ID="lblVlere" runat="server"
                                        Text="Maksimumi:" ClientInstanceName="lblVlere">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtMaximumi">--%>
                                    <dx:ASPxTextBox ID="txtVlere" runat="server" Width="100%" ClientInstanceName="txtVlere">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtStokuMaxVfOne"
                                        ID="lblStokuMaxVfOne" runat="server"
                                        Text="Stoku max per VFONE" ClientInstanceName="lblStokuMaxVfOne">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtStokuMaxVfOne" runat="server" Width="100%"
                                        ClientInstanceName="txtStokuMaxVfOne">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip"
                                            CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9,.]*"
                                                ErrorText="Lejohen vetem numra!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodVFOne" ID="lblKodVFOne" runat="server"
                                        Text="Kod dhurate:" ClientInstanceName="lblKodVFOne">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtKodVFOne" runat="server" Width="100%"
                                        ClientInstanceName="txtKodVFOne">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip"
                                            CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxButton ID="btneAutomjeti" runat="server" Text="Shto Automjet"
                                        Width="100%" ClientVisible="false" ClientInstanceName="btneAutomjeti"
                                        CausesValidation="False" AutoPostBack="False">
                                        <ClientSideEvents Click="function (s,e){ Auto_Click(); }" />
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxButton>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLidhMeNdermRap" ID="lblLidhMeNdermRap" runat="server"
                                        Text="Lidh me ndermarrjen raportuese:" ClientInstanceName="lblLidhMeNdermRap">
                                    </dx:ASPxLabel>

                                    <dx:ASPxComboBox ID="cmbLidhMeNdermRap" runat="server" ClientInstanceName="cmbLidhMeNdermRap"
                                        Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { LidhMeNdermRap_Click();	}" />

                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="false"
                                            ValidationGroup="entries1">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="chkbAparatBazaar" ID="lblAparatBazaar" runat="server"
                                        Text="Aparat bazaar:" ClientInstanceName="lblAparatBazaar">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtMaximumi">--%>
                                    <dx:ASPxCheckBox ID="chkbAparatBazaar" runat="server" Width="100%" ClientInstanceName="chkbAparatBazaar">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>

                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodOferte" ID="lblKodOferte" runat="server"
                                        Text="Kod Oferte:" ClientInstanceName="lblKodOferte">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtMaximumi">--%>
                                    <dx:ASPxTextBox ID="txtKodOferte" runat="server" Width="100%" ClientInstanceName="txtKodOferte">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>

                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbArtikullVjeter" ID="lblArtikullVjeter" runat="server"
                                        Text="Artikull i vjeter:" ClientInstanceName="lblArtikullVjeter">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtMaximumi">--%>
                                    <dx:ASPxCheckBox ID="cbArtikullVjeter" runat="server" Width="100%" ClientInstanceName="cbArtikullVjeter">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>

                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodiIBarit" ID="lblKodiIBarit" runat="server" Text="Kodi i barit:" ClientInstanceName="lblKodiIBarit">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtKodiIBarit" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodiIBarit">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbIRimbursueshem" ID="lblIRimbursueshem" runat="server" Text="I rimbursueshem:" ClientInstanceName="lblIRimbursueshem">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbIRimbursueshem" runat="server" Width="100%" ClientInstanceName="cbIRimbursueshem">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>

                                    <br />
                                    <br />
                                    <div id="divgride11" style="display: none">
                                        <div id="divgride12">
                                            <table id="rowed7">
                                            </table>
                                        </div>
                                    </div>

                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Inventari" Text="Inventari">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl6" runat="server">
                                    <table id="tblInventari" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%-- <div id="dvlblMagazina">--%>
                                    <dx:ASPxLabel Wrap="False" ID="lblMagazina" AssociatedControlID="btneMagazina" runat="server"
                                        Text="Magazina" ClientInstanceName="lblMagazina">
                                    </dx:ASPxLabel>
                                    <%--  </div>--%>
                                    <%--    <div id="dvbtnMagazina">--%>
                                    <dx:ASPxComboBox ID="btnMagazina" Width="100%" runat="server" ClientInstanceName="btnMagazina"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickMagazina();}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidateOnLeave="false"
                                            ValidationGroup="entries1">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--   </div> --%>
                                    <%--<div id="dvlblKodi3">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi3" ID="lblKodi3" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi3">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtKodi3">--%>
                                    <dx:ASPxTextBox ID="txtKodi3" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi3">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodi3" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblPershkrimi3">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi3" ID="lblPershkrimi3"
                                        runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi3">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPershkrimi3">--%>
                                    <dx:ASPxTextBox ID="txtPershkrimi3" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtPershkrimi3">
                                        <ClientSideEvents TextChanged="TextChanged_txtPershkrimi3" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbRezervueshem" ID="lblRezervueshem"
                                        runat="server" Text="I rezervueshem:" ClientInstanceName="lblRezervueshem">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbProdhimMePorosi">--%>
                                    <dx:ASPxCheckBox ID="cbRezervueshem" runat="server" ClientInstanceName="cbRezervueshem">
                                    </dx:ASPxCheckBox>

                                    <%--</div>--%>
                                    <%--<div id="dvlblMetode">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMetode" ID="lblMetode" runat="server"
                                        Text="Metode Kostoje:" ClientInstanceName="lblMetode">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbMetode">--%>
                                    <dx:ASPxComboBox ID="cmbMetode" runat="server" ClientInstanceName="cmbMetode" ShowShadow="False"
                                        Width="100%" ReadOnly="false" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents TextChanged="function(s, e) {cmbMetodeTextChanged(s, e);}" EndCallback="function(s, e) {cmbMetodeEndCallback(s, e);}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvbtnMetode">--%>
                                    <dx:ASPxButton ID="btnMetode" runat="server" AutoPostBack="false" ClientInstanceName="btnMetode"
                                        Height="30px" Width="16px">
                                        <ClientSideEvents Click="function(s, e) { popupHelp.Show(); }" />
                                        <Image Url="~/images/help_icon.jpg">
                                        </Image>
                                    </dx:ASPxButton>
                                    <%--</div>--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="lblMetodePershkrimi" ID="lblShpjegimMetodeKosto"
                                        runat="server" ClientInstanceName="lblShpjegimMetodeKosto" Text="Pershkrim Metode Kosto">
                                    </dx:ASPxLabel>
                                    <%--<div id="dvlblMetodePershkrimi">--%>
                                    <dx:ASPxMemo ID="lblMetodePershkrimi" Rows="3" Width="100%" runat="server" Text=""
                                        ClientInstanceName="lblMetodePershkrimi">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                    <%--<div id="dvlblKontrollGjendjeArtikulli">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMetode" ID="lblKontrollGjendjeArtikulli"
                                        runat="server" Text="Kontroll gjendje" ClientInstanceName="lblKontrollGjendjeArtikulli">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcheckKontrollGjendjeArtikulli">--%>
                                    <dx:ASPxCheckBox ID="checkKontrollGjendjeArtikulli" runat="server" ClientInstanceName="checkKontrollGjendjeArtikulli">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ClientSideEvents CheckedChanged="function(s, e) { KontrollGjendjeDetajim(); }" />
                                    </dx:ASPxCheckBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblDetajim">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbDetajim" ID="lblDetajim" runat="server"
                                        Text="Detajim:" ClientInstanceName="lblDetajim">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbDetajim">--%>
                                    <dx:ASPxCheckBox ID="cbDetajim" runat="server" ClientInstanceName="cbDetajim">
                                        <ClientSideEvents CheckedChanged="function(s, e) { kontrolloDetajimLidhur(); }" />
                                    </dx:ASPxCheckBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblKategoriDetajimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategoriDetajimi" ID="lblKategoriDetajimi"
                                        runat="server" ClientInstanceName="lblKategoriDetajimi" Text="Kategoria e detajimit">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbKategoriDetajimi">--%>
                                    <dx:ASPxComboBox ID="cmbKategoriDetajimi" runat="server" ClientInstanceName="cmbKategoriDetajimi"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" ValueType="System.String"
                                        Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function (s,e){ KategoriaChanged(1);}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblKategoriDetajimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategoriDetajimi2" ID="lblKategoriDetajimi2"
                                        runat="server" ClientInstanceName="lblKategoriDetajimi2" Text="Kategoria e detajimit 2: ">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbKategoriDetajimi">--%>
                                    <dx:ASPxComboBox ID="cmbKategoriDetajimi2" runat="server" ClientInstanceName="cmbKategoriDetajimi2"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" ValueType="System.String"
                                        Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function (s,e){ KategoriaChanged(2);}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblDetajimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btnDetajim1Nga" ID="lblDetajimi"
                                        runat="server" Text="Detajimi 1:" ClientInstanceName="lblDetajimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtnDetajim1Nga">--%>
                                    <dx:ASPxButtonEdit ID="btnDetajim1Nga" runat="server" ClientInstanceName="btnDetajim1Nga"
                                        ReadOnly="True" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { DetajimArtikulli_Click(s); }" />
                                        <Buttons>
                                            <dx:EditButton>
                                            </dx:EditButton>
                                        </Buttons>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxButtonEdit>
                                    <%--</div>--%>
                                    <%--<div id="dvlblDetajimi2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btnDetajim2Nga" ID="lblDetajimi2"
                                        runat="server" Text="Detajimi 2:" ClientInstanceName="lblDetajimi2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtnDetajim2Nga">--%>
                                    <dx:ASPxButtonEdit ID="btnDetajim2Nga" runat="server" ClientInstanceName="btnDetajim2Nga"
                                        ReadOnly="True" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { DetajimArtikulli_Click(s); }" />
                                        <Buttons>
                                            <dx:EditButton>
                                            </dx:EditButton>
                                        </Buttons>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxButtonEdit>
                                    <%--</div>--%>
                                    <%--<div id="dvcheckKontrollCmimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="checkKontrollCmimi" ID="lblKontrollCmimi"
                                        runat="server" Text="Kontroll cmimi per detajimin" ClientInstanceName="lblKontrollCmimi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="checkKontrollCmimi" runat="server"
                                        ClientInstanceName="checkKontrollCmimi">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <%--</div>--%>
                                    <%--<div id="dvcheckKontrollGjendje">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="checkKontrollGjendje" ID="lblKontrollDet1"
                                        runat="server" Text="Kontroll gjendje per detajimin 1" ClientInstanceName="lblKontrollDet1">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="checkKontrollGjendje" runat="server"
                                        ClientInstanceName="checkKontrollGjendje">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <%--</div>--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="checkKontrollGjendjeDetajim2" ID="lblKontrollDet2"
                                        runat="server" Text="Kontroll gjendje per detajimin 2" ClientInstanceName="lblKontrollDet2">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="checkKontrollGjendjeDetajim2" runat="server"
                                        ClientInstanceName="checkKontrollGjendjeDetajim2">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <%--<div id="dvlblMinimum">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMinimumi" ID="lblMinimum" runat="server"
                                        Text="Minimumi:" ClientInstanceName="lblMinimum">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtMinimumi">--%>
                                    <dx:ASPxTextBox ID="txtMinimumi" runat="server" Width="100%" ClientInstanceName="txtMinimumi">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblMaksimum">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMaximumi" ID="lblMaksimum" runat="server"
                                        Text="Maksimumi:" ClientInstanceName="lblMaksimum">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtMaximumi">--%>
                                    <dx:ASPxTextBox ID="txtMaximumi" runat="server" Width="100%" ClientInstanceName="txtMaximumi">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblPeshaBruto">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPeshaBruto" ID="lblPeshaBruto"
                                        runat="server" Text="Pesha Bruto:" ClientInstanceName="lblPeshaBruto">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPeshaBruto">--%>
                                    <dx:ASPxTextBox ID="txtPeshaBruto" runat="server" ClientInstanceName="txtPeshaBruto"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Lejohen vetem numra!" ValidationExpression="[0-9,.]*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblPeshaNeto">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPeshaNeto" ID="lblPeshaNeto"
                                        runat="server" Text="Pesha Neto:" ClientInstanceName="lblPeshaNeto">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPeshaNeto">--%>
                                    <dx:ASPxTextBox ID="txtPeshaNeto" runat="server" ClientInstanceName="txtPeshaNeto"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Lejohen vetem numra!" ValidationExpression="[0-9,.]*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblVendodhja">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVendodhja" ID="lblVendodhja"
                                        runat="server" Text="Vendodhja:" ClientInstanceName="lblVendodhja">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtVendodhja">--%>
                                    <dx:ASPxTextBox ID="txtVendodhja" runat="server" Width="100%" ClientInstanceName="txtVendodhja">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbMbetjeShitshem" ID="lblMbetjeShitshem" runat="server"
                                        Text="Aktiv:" ClientInstanceName="lblMbetjeShitshem">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbMbetjeShitshem" runat="server" ClientInstanceName="cbMbetjeShitshem" Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblFormatSeriali" AssociatedControlID="cmbFormatSeriali" runat="server"
                                        Text="Format Seriali" ClientInstanceName="lblFormatSeriali">
                                    </dx:ASPxLabel>
                                    <%--  </div>--%>
                                    <%--    <div id="dvbtnMagazina">--%>
                                    <dx:ASPxComboBox ID="cmbFormatSeriali" Width="100%" runat="server" ClientInstanceName="cmbFormatSeriali"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickFormatSeriali();}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />

                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidateOnLeave="false"
                                            ValidationGroup="entries1">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />

                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />

                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblKaraktereTAC" runat="server" AssociatedControlID="txtKaraktereTAC"
                                        Text="Karaktere per TAC" ClientInstanceName="lblKaraktereTAC">
                                    </dx:ASPxLabel>

                                    <dx:ASPxTextBox ID="txtKaraktereTAC" runat="server" ClientInstanceName="txtKaraktereTAC"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Lejohen vetem numra!" ValidationExpression="[0-9,.]*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>



                                    <%--</div>--%>
                                    <br />
                                    <div id="divgrideGjendje1" style="display: none">
                                        <div id="divgrideGjendje2">
                                            <table id="rowed6">
                                            </table>
                                        </div>
                                        <div></div>
                                    </div>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Kontabiliteti" Text="Kontabiliteti">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl15" runat="server">
                                    <table id="tblKontabiliteti" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblKlasa">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKlasa" ID="lblKlasa" runat="server"
                                        Text="Klasa:" ClientInstanceName="lblKlasa">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbKlasa">--%>
                                    <dx:ASPxComboBox ID="cmbKlasa" runat="server" ClientInstanceName="cmbKlasa" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {cmbKlasaIndexChanged(s,e);}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvbtnArtikujtPerberes">--%>
                                    <dx:ASPxButton ID="btnArtikujtPerberes" runat="server" Text="Zgjidh artikujt perberes"
                                        Width="100%" ClientVisible="false" ClientInstanceName="btnArtikujtPerberes" CausesValidation="False"
                                        AutoPostBack="False">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxButton>
                                    <%--</div>--%>
                                    <%--<div id="dvlblSkema">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneSkema" ID="lblSkema" runat="server"
                                        Text="Skema:" ClientInstanceName="lblSkema">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtneSkema">--%>
                                    <dx:ASPxComboBox ID="btneSkema" runat="server" ClientInstanceName="btneSkema" EnableCallbackMode="True"
                                        OnItemRequestedByValue="btneSkema_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneSkema_ItemsRequestedByFilterCondition" EnableSynchronization="True"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { Skema_Click(); }"
                                            TextChanged="function(s, e) { btneSkemaTextChanged(s,e); }"
                                            LostFocus="function(s, e) { kontrolloSkema(); }"
                                            EndCallback="function(s, e) { if(btneSkema.GetEnabled() == false) { btneSkema.HideDropDown(); } }"
                                            BeginCallback="function(s, e) {	if(btneSkema.GetEnabled() == false) { btneSkema.HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogInv">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogInv" ID="lblLlogInv" runat="server"
                                        Text="Llogari Inventar:" ClientInstanceName="lblLlogInv">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtneLlogInv">--%>
                                    <dx:ASPxComboBox ID="btneLlogInv" runat="server" ClientInstanceName="btneLlogInv"
                                        EnableCallbackMode="True" OnItemRequestedByValue="btneLlogInv_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogInv_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { txtLlog.SetText('inv'); HapLLogariSipasLlojitClick('#hfLupaLlogInv');}" TextChanged="function(s,e){OnChange(s,e);}" Init="function(s, e) {}"
                                            EndCallback="function(s, e) { if(btneLlogInv.GetEnabled() == false) { btneLlogInv.HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogBle">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogBle" ID="lblLlogBle" runat="server"
                                        Text="Llogari blerje:" ClientInstanceName="lblLlogBle">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtneLlogBle">--%>
                                    <dx:ASPxComboBox ID="btneLlogBle" runat="server" ClientInstanceName="btneLlogBle"
                                        EnableCallbackMode="True" OnItemRequestedByValue="btneLlogBle_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogBle_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { txtLlog.SetText('ble'); HapLLogariSipasLlojitClick('#hfLupaLlogBle'); }"
                                            TextChanged="function(s, e){OnChange(s,e);}"
                                            Init="function(s, e) { }"
                                            EndCallback="function(s, e) { if(btneLlogBle.GetEnabled() == false) { btneLlogBle.HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogShit">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogShit" ID="lblLlogShit" runat="server"
                                        Text="Llogari Shitje:" ClientInstanceName="lblLlogShit">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtneLlogShit">--%>
                                    <dx:ASPxComboBox ID="btneLlogShit" runat="server" ClientInstanceName="btneLlogShit"
                                        EnableCallbackMode="True" OnItemRequestedByValue="btneLlogShit_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogShit_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { txtLlog.SetText('shit'); HapLLogariSipasLlojitClick('#hfLupaLlogShit'); }"
                                            TextChanged="function(s, e){OnChange(s,e);}"
                                            Init="function(s, e) { }"
                                            EndCallback="function(s, e) { if(btneLlogShit.GetEnabled() == false) { btneLlogShit.HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogTretet">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogTretet" ID="lblLlogTretet"
                                        runat="server" Text="Llogari tek te tretet:" ClientInstanceName="lblLlogTretet">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtneLlogTretet">--%>
                                    <dx:ASPxComboBox ID="btneLlogTretet" runat="server" ClientInstanceName="btneLlogTretet"
                                        EnableCallbackMode="True" OnItemRequestedByValue="btneLlogTretet_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogTretet_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { txtLlog.SetText('tretet'); HapLLogariSipasLlojitClick('#hfLupaLlogTretet'); }" TextChanged="function(s, e){OnChange(s,e);}"
                                            Init="function(s, e) {}" EndCallback="function(s, e) { if(btneLlogTretet.GetEnabled() == false) { btneLlogTretet.HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogShpe">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btnLlogShpe" ID="lblLlogShpe" runat="server"
                                        Text="Llogari Shpenzim:" ClientInstanceName="lblLlogShpe">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtnLlogShpe">--%>
                                    <dx:ASPxComboBox ID="btnLlogShpe" runat="server" ClientInstanceName="btnLlogShpe"
                                        EnableCallbackMode="True" OnItemRequestedByValue="btnLlogShpe_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btnLlogShpe_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { txtLlog.SetText('shpe'); HapLLogariSipasLlojitClick('#hfLupaLlogShpe');}" TextChanged="function(s, e){OnChange(s,e);}"
                                            Init="function(s, e) { }"
                                            EndCallback="function(s, e) { if(btneLlogShpe.GetEnabled() == false) { btnLlogShpe.HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>

                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogAmortizimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlogAmortizimi" ID="lblLlogAmortizimi"
                                        runat="server" Text="Llogari Amortizimi:" ClientInstanceName="lblLlogAmortizimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbLlogAmortizimi">--%>
                                    <dx:ASPxComboBox ID="cmbLlogAmortizimi" runat="server" ClientInstanceName="cmbLlogAmortizimi"
                                        EnableCallbackMode="True" OnItemRequestedByValue="cmbLlogAmortizimi_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbLlogAmortizimi_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { txtLlog.SetText('amor'); HapLLogariSipasLlojitClick('#hfLupaLlogAmortizimi'); }"
                                            TextChanged="function(s, e){OnChange(s,e);}"
                                            Init="function(s, e) { }"
                                            EndCallback="function(s, e) { if(cmbLlogAmortizimi.GetEnabled() == false) { cmbLlogAmortizimi .HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btnLLogariKomisioni" ID="lblLLogariKomisioni" runat="server"
                                        Text="Llogari Komisioni:" ClientInstanceName="lblLLogariKomisioni">
                                    </dx:ASPxLabel>

                                    <dx:ASPxComboBox ID="btnLLogariKomisioni" runat="server" ClientInstanceName="btnLLogariKomisioni"
                                        EnableCallbackMode="True" OnItemRequestedByValue="btnLLogariKomisioni_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btnLLogariKomisioni_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <%--duhet pare prap --%>
                                        <ClientSideEvents ButtonClick="function(s, e) { txtLlog.SetText('kom'); HapLLogariSipasLlojitClick('#hfLupaLlogKomision');}" TextChanged="function(s, e){OnChange(s,e);}"
                                            Init="function(s, e) {}" EndCallback="function(s, e) { if(btnLLogariKomisioni.GetEnabled() == false) { btnLLogariKomisioni.HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>



                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btnLlogPakesim" ID="lblLlogPakesim" runat="server"
                                        Text="Llogari pakesim vlere dalje:" ClientInstanceName="lblLlogPakesim">
                                    </dx:ASPxLabel>


                                    <dx:ASPxComboBox ID="btnLlogPakesim" runat="server" ClientInstanceName="btnLlogPakesim"
                                        EnableCallbackMode="True" OnItemRequestedByValue="btnLlogPakesim_ItemRequestedByValue"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { txtLlog.SetText('pakesim'); HapLLogariSipasLlojitClick('#hfLupaLlogPakesim'); }"
                                            Init="function(s, e) { }"
                                            EndCallback="function(s, e) { if(btnLlogPakesim.GetEnabled() == false) { btnLlogPakesim .HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogRez">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogRez" ID="lblLlogRez" runat="server"
                                        Text="Llogari Rezerve:" ClientInstanceName="lblLlogRez">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtneLlogRez">--%>
                                    <dx:ASPxComboBox ID="btneLlogRez" runat="server" ClientInstanceName="btneLlogRez"
                                        EnableCallbackMode="True" OnItemRequestedByValue="btneLlogRez_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogRez_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { txtLlog.SetText('rez'); HapLLogariSipasLlojitClick('#hfLupaLlogR'); }"
                                            Init="function(s, e) { }"
                                            EndCallback="function(s, e) { if(btneLlogRez.GetEnabled() == false) { btneLlogRez.HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogRez">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogPakesimRez" ID="lblLlogPakesimRez" runat="server"
                                        Text="Llogari Pakesim Rezerve:" ClientInstanceName="lblLlogPakesimRez">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtneLlogPakesimRez">--%>
                                    <dx:ASPxComboBox ID="btneLlogPakesimRez" runat="server" ClientInstanceName="btneLlogPakesimRez"
                                        EnableCallbackMode="True" OnItemRequestedByValue="btneLlogPakesimRez_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogPakesimRez_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { txtLlog.SetText('pakr'); HapLLogariSipasLlojitClick('#hfLupaLlogPakR'); }"
                                            Init="function(s, e) { }"
                                            EndCallback="function(s, e) { if(btneLlogPakesimRez.GetEnabled() == false) { btneLlogPakesimRez.HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>


                                    <%--<div id="dvlblSasiNjesi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtSasiNjesi" ID="lblSasiNjesi"
                                        runat="server" Text="Sasi Njesi:" ClientInstanceName="lblSasiNjesi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtSasiNjesi">--%>
                                    <dx:ASPxTextBox ID="txtSasiNjesi" runat="server" ClientInstanceName="txtSasiNjesi"
                                        DisplayFormatString="0.00" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Lejohen vetem numra!" ValidationExpression="[0-9,.]*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblScrap">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtScrap" ID="lblScrap" runat="server"
                                        Text="Firo ligjore  %:" ClientInstanceName="lblScrap">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtScrap">--%>
                                    <dx:ASPxTextBox ID="txtScrap" runat="server" ClientInstanceName="txtScrap" DisplayFormatString="0.00"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s,e){ndryshoScrap();}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Lejohen vetem numra!" ValidationExpression="[0-9,.]*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblProdhimMePorosi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbProdhimMePorosi" ID="lblProdhimMePorosi"
                                        runat="server" Text="Prodhim me porosi:" ClientInstanceName="lblProdhimMePorosi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcbProdhimMePorosi">--%>
                                    <dx:ASPxCheckBox ID="cbProdhimMePorosi" runat="server" ClientInstanceName="cbProdhimMePorosi">
                                    </dx:ASPxCheckBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblDateAkt">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDateAkt" ID="lblDateAkt" runat="server"
                                        Text="Date Aktivizimi:" ClientInstanceName="lblDateAkt">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdteDateAkt" style="visibility: hidden; display: 'none';">--%>
                                    <dx:ASPxDateEdit ID="dteDateAkt" runat="server" ClientInstanceName="dteDateAkt"
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
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <%--</div>--%>
                                    <%--<div id="dvlblNdryshimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="pnlNdryshimi" ID="lblNdryshimi"
                                        runat="server" Text="Data e ndryshimit:" ClientInstanceName="lblNdryshimi">
                                    </dx:ASPxLabel>




                                    <%--</div>--%>
                                    <%--<div id="dvcmbNdryshimi" style="visibility: hidden; display: 'none';">--%>
                                    <asp:UpdatePanel ID="cmbNdryshimi_pnlNdryshimi" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <dx:ASPxComboBox ID="cmbNdryshimi" runat="server" ClientInstanceName="cmbNdryshimi"
                                                Width="100%" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                                <ClientSideEvents SelectedIndexChanged="function (s,e){changeDate(s,e);}" />
                                                <DropDownButton>
                                                    <Image>
                                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    </Image>
                                                </DropDownButton>
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                                    ValidateOnLeave="false">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                            </dx:ASPxComboBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <%--</div>--%>
                                    <%--<div id="dvlblKMSH">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKMSH" ID="lblKMSH" runat="server"
                                        Text="Llogaritja KMSH:" ClientInstanceName="lblKMSH">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbKMSH">--%>
                                    <dx:ASPxComboBox ID="cmbKMSH" runat="server" ClientInstanceName="cmbKMSH" ShowShadow="False"
                                        ReadOnly="false" SettingsLoadingPanel-ImagePosition="Top">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblArtikuj">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneArtikuj" ID="lblArtikuj" runat="server"
                                        Text="Artikuj zevendesues:" ClientInstanceName="lblArtikuj">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtneArtikuj">--%>
                                    <dx:ASPxButtonEdit ID="btneArtikuj" Enabled="true" ReadOnly="true" ClientInstanceName="btneArtikuj"
                                        runat="server">
                                        <ClientSideEvents ButtonClick="function(s, e) { Artikull_Click(); }"
                                            TextChanged="TextChanged_btneArtikuj" />
                                        <Buttons>
                                            <dx:EditButton>
                                            </dx:EditButton>
                                        </Buttons>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxButtonEdit>
                                    <%--</div>--%>
                                    <%--<div id="dvlblZevendesim">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbZevendesim" ID="lblZevendesim"
                                        runat="server" Text="Zevendesim Automatik:" ClientInstanceName="lblZevendesim">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbZevendesim">--%>
                                    <dx:ASPxComboBox ID="cmbZevendesim" runat="server" Enabled="true" ClientInstanceName="cmbZevendesim"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
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
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>

                                    <br />
                                 
                                    <div id="divgride1" style="display: none">
                                        <div id="divgride2">
                                            <table id="rowed5">
                                            </table>
                                        </div>
                                    </div>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Regjistrime" Text="Regjistrime">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl2" runat="server">
                                    <table id="tblRegjistrime" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblKodiDoganor">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodiDoganor" ID="lblKodiDoganor"
                                        runat="server" Text="Kodi Doganor:" ClientInstanceName="lblKodiDoganor">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtKodiDoganor">--%>
                                    <dx:ASPxTextBox ID="txtKodiDoganor" runat="server" ClientInstanceName="txtKodiDoganor"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup='entries1'
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblFurnitori">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtFurnitori" ID="lblFurnitori"
                                        runat="server" Text="Furnitori Kryesor:" ClientInstanceName="lblFurnitori">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtFurnitori">--%>
                                    <dx:ASPxComboBox ID="txtFurnitori" runat="server" ClientInstanceName="txtFurnitori"
                                        Width="100%" ShowShadow="False" ValueType="System.Int32" EnableClientSideAPI="True"
                                        IncrementalFilteringMode="Contains" EnableSynchronization="True" EnableCallbackMode="True"
                                        DropDownRows="3" CallbackPageSize="3" OnItemRequestedByValue="txtFurnitori_ItemRequestedByValue"
                                        OnItemsRequestedByFilterCondition="txtFurnitori_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents ButtonClick="function(s,e){Furnitori_Click();}" SelectedIndexChanged="function (s,e){KlientFurnitoriChanged()}"
                                            GotFocus="function(s, e){s.SelectAll();}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblNivelTvsh">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNivelTvsh" ID="lblNivelTvsh"
                                        runat="server" Text="Nivel TVSH-je:" ClientInstanceName="lblNivelTvsh">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbNivelTvsh">--%>
                                    <dx:ASPxComboBox ID="cmbNivelTvsh" runat="server" ClientInstanceName="cmbNivelTvsh"
                                        Width="100%" OnPreRender="cmbNivelTvsh_PreRender" ShowShadow="False" ValueType="System.String"
                                        OnItemRequestedByValue="cmbNivelTvsh_ItemRequestedByValue" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { ndryshoTvsh(); }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNjesia1" ID="lblNjesia1" runat="server"
                                        Text="Njesia 1:" ClientInstanceName="lblNjesia1">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbNjesia1" runat="server" ClientInstanceName="cmbNjesia1"
                                        ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                        Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { cmbNjesia2.SetText(cmbNjesia1.GetText()); }"
                                            EndCallback="function(s, e) { if(cmbNjesia1.GetEnabled() == false) { cmbNjesia1.HideDropDown(); } }"
                                            BeginCallback="function(s, e) { if(cmbNjesia1.GetEnabled() == false) { cmbNjesia1.HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNjesia2" ID="lblNjesia2" runat="server"
                                        Text="Njesia 2:" ClientInstanceName="lblNjesia2">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbNjesia2" runat="server" ClientInstanceName="cmbNjesia2"
                                        ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                        Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { }"
                                            EndCallback="function(s, e) { if(cmbNjesia2.GetEnabled() == false) { cmbNjesia2.HideDropDown(); } }"
                                            BeginCallback="function(s, e) { if(cmbNjesia2.GetEnabled() == false) { cmbNjesia2.HideDropDown(); } }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKoelblPeshaBrutoficienti" ID="lblKoeficienti"
                                        runat="server" Text="Koeficienti:" ClientInstanceName="lblKoeficienti">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtKoeficienti" runat="server" ClientInstanceName="txtKoeficienti"
                                        Text="1" Width="100%">
                                        <ClientSideEvents LostFocus="function(s, e) { kontrolloNjesi(); }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Lejohen vetem numra!" ValidationExpression="[0-9,.]*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKodifikimi1" ID="lblKodifikimi1"
                                        runat="server" Text="Kodifikimi 1:" ClientInstanceName="lblKodifikimi1">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="btneKodifikimi1" runat="server" ClientInstanceName="btneKodifikimi1"
                                        EnableCallbackMode="False"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { KodifikimArtikulli_Click(1); grida='1'; }"
                                            LostFocus="function(s, e) { merrSkeme(s); LostFocusKodifikim(s); }"
                                            SelectedIndexChanged="function(s, e) { Updatenormat(s); }" TextChanged="function(s, e) { Updatenormat(s); }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKodifikimi2" ID="lblKodifikimi2"
                                        runat="server" Text="Kodifikimi 2:" ClientInstanceName="lblKodifikimi2" >
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="btneKodifikimi2" runat="server" ClientInstanceName="btneKodifikimi2"
                                        EnableCallbackMode="False"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents  LostFocus="function(s, e) { LostFocusKodifikim(s); }" ButtonClick="function(s, e) { KodifikimArtikulli_Click(2); grida='2';}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKodifikimi3" ID="lblKodifikimi3"
                                        runat="server" Text="Kodifikimi 3:" ClientInstanceName="lblKodifikimi3">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="btneKodifikimi3" runat="server" ClientInstanceName="btneKodifikimi3"
                                        EnableCallbackMode="False"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents LostFocus="function(s, e) { LostFocusKodifikim(s); }" ButtonClick="function(s, e) { KodifikimArtikulli_Click(3); grida='3'; }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbObjektiva" ID="lblObjektiva"
                                        runat="server" Text="Prindi:" ClientInstanceName="lblObjektiva">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbLlog">--%>
                                    <dx:ASPxComboBox ID="cmbObjektiva" ClientInstanceName="cmbObjektiva" Width="100%"
                                        runat="server" EnableCallbackMode="False"
                                        IncrementalFilteringDelay="7" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                        <ClientSideEvents ButtonClick="function(s, e) {Objektiva_Click();}" />
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGarancia" ID="lblLlojGarancia"
                                        runat="server" Text="Garancia:" ClientInstanceName="lblLlojGarancia">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbGarancia" runat="server" ClientInstanceName="cmbGarancia"
                                        ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                        Width="100%">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                            <RegularExpression ErrorText="Lejohen vetem numra!" ValidationExpression="[0-9,.]*" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGarancia" ID="lblGarancia" runat="server"
                                        Text="Garancia:" ClientInstanceName="lblGarancia">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtGarancia" runat="server" ClientInstanceName="txtGarancia"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	kontrolloTextGarancia(s, e); }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            SetFocusOnError="True" ValidationGroup="entries1">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Lejohen vetem numra!" ValidationExpression="[0-9,.]*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <br />
                                  
                                    <%--<div id="dvlblKodi2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi2" ID="lblKodi2" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtKodi2">--%>
                                    <dx:ASPxTextBox ID="txtKodi2" runat="server" Width="170px" AutoPostBack="false"
                                        ClientInstanceName="txtKodi2">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodi2" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblPershkrimi2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi2" ID="lblPershkrimi2"
                                        runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPershkrimi2">--%>
                                    <dx:ASPxTextBox ID="txtPershkrimi2" runat="server" Width="170px" AutoPostBack="false"
                                        ClientInstanceName="txtPershkrimi2">
                                        <ClientSideEvents TextChanged="TextChanged_txtPershkrimi2" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbShitshem" ID="lblShitshem" runat="server"
                                        Text="Aktiv:" ClientInstanceName="lblShitshem">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbShitshem" runat="server" ClientInstanceName="cbShitshem" Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbLLogaritKomision" ID="lblLLogaritKomision" runat="server"
                                        Text="Llogarit Komision:" ClientInstanceName="lblLLogaritKomision">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbLLogaritKomision" runat="server" ClientInstanceName="cbLLogaritKomision" Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>

                                    <br />
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Text="Buxheti">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl5" runat="server">
                                    <table id="tblBuxheti" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblKodi4">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi4" ID="lblKodi4" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi4">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtKodi4">--%>
                                    <dx:ASPxTextBox ID="txtKodi4" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi4">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodi4" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblPershkrimi4">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi4" ID="lblPershkrimi4"
                                        runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi4">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPershkrimi4">--%>
                                    <dx:ASPxTextBox ID="txtPershkrimi4" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtPershkrimi4">
                                        <ClientSideEvents TextChanged="TextChanged_txtPershkrimi4" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <dx:ASPxGridView ID="gvBuxheti" runat="server" ClientInstanceName="gvBuxheti"
                                        OnAfterPerformCallback="gvBuxheti_AfterPerformCallback" OnHtmlRowCreated="gvBuxheti_HtmlRowCreated">
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                    <br />
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Fushat Shtese" Text="Fushat Shtese">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl7" runat="server">
                                    <uc1:ucFushatShtese runat="server" ID="ucFushatShtese" />

                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Cmimet" Text="Cmimet">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl9" runat="server">
                                    <br />
                                    <dx:ASPxGridView ID="gvCmimet" runat="server" Width='100%' ClientInstanceName="gvCmimet"
                                        OnAfterPerformCallback="gvCmimet_AfterPerformCallback" OnHtmlRowCreated="gvCmimet_HtmlRowCreated"
                                        OnCustomCallback="gvCmimet_CustomCallback" OnDataBound="gvCmimet_DataBound" OnCustomJSProperties="gvCmimet_CustomJSProperties">
                                        <ClientSideEvents EndCallback="function(s, e){formatoFushaDevi(); EndCallback(s, e);}" BeginCallback="function(s, e){merrTeDhenat(s, e);}" />
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                    <br />
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Text="Amortizimi" Name="Amortizimi" ClientVisible="false">
                            <ContentCollection>
                                <dxw:ContentControl>
                                    <table id="tblAmortizimi" class="renditKontrolle">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDateAk2" ID="lblDateAk2" runat="server"
                                                        Text="Date Aktivizimi:" ClientInstanceName="lblDateAk2">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxDateEdit ID="dteDateAk2" runat="server" ClientInstanceName="dteDateAk2"
                                                        ShowShadow="False" Width="100%">
                                                        <ClientSideEvents DateChanged="gvAmortizimiDateChanged" />
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
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNdryshim2" ID="lblNdryshim2"
                                                        runat="server" Text="Data e ndryshimit:" ClientInstanceName="lblNdryshim2">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="cmbNdryshim2_pnlNdryshim2" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <dx:ASPxComboBox ID="cmbNdryshim2" runat="server" ClientInstanceName="cmbNdryshim2"
                                                                Width="100%" ShowShadow="False">
                                                                <ClientSideEvents SelectedIndexChanged="gvAmortizimiSelectedIndexChanged" />
                                                                <DropDownButton>
                                                                    <Image>
                                                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                    </Image>
                                                                </DropDownButton>
                                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                                                    ValidateOnLeave="false">
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                                    </ErrorFrameStyle>
                                                                    <RequiredField IsRequired="true" />
                                                                </ValidationSettings>
                                                                <DisabledStyle Font-Bold="False">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="lblRezRivleresimi" runat="server" AssociatedControlID="cbRezRivleresimi" ClientInstanceName="lblRezRivleresimi"
                                                        Wrap="False" Text="Me rezerve rivleresimi">
                                                    </dx:ASPxLabel>
                                                    <dx:ASPxCheckBox ID="cbRezRivleresimi" runat="server" ClientInstanceName="cbRezRivleresimi" Width="100%"
                                                        RightToLeft="False" Layout="Flow">
                                                        <ClientSideEvents CheckedChanged="function (s,e){ ShfaqGrideNormaAmortizimi(); }" />
                                                    </dx:ASPxCheckBox>


                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                    <%--</div>--%>
                                    <%--<div id="dvdteDateAk2">--%>

                                    <%--</div>--%>
                                    <%--<div id="dvlblNdryshim2">--%>

                                    <%--</div>--%>
                                    <%--<div id="dvcmbNdryshim2">--%>

                                    <br />
                                    <dx:ASPxGridView ID="gvAmortizimi" runat="server" Width="100%"
                                        ClientInstanceName="gvAmortizimi" OnHtmlRowCreated="gvAmortizimi_HtmlRowCreated"
                                        OnCustomCallback="gvAmortizimi_CustomCallback"
                                        OnCustomJSProperties="gvAmortizimi_CustomJSProperties" ClientIDMode="AutoID">
                                        <ClientSideEvents EndCallback="function(s,e){}" BeginCallback="function(s,e){}" />
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <SettingsPager PageSize="15">
                                        </SettingsPager>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>

                                    <br />

                                    <br />
                                    <dx:ASPxGridView ID="gvNormaAmortizimi" runat="server" Width="100%"
                                        ClientInstanceName="gvNormaAmortizimi" OnHtmlRowCreated="gvNormaAmortizimi_HtmlRowCreated"
                                        OnCustomCallback="gvNormaAmortizimi_CustomCallback"
                                        OnCustomJSProperties="gvNormaAmortizimi_CustomJSProperties" ClientIDMode="AutoID">
                                        <ClientSideEvents EndCallback="function(s,e){}" BeginCallback="function(s,e){}" />
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <SettingsPager PageSize="15">
                                        </SettingsPager>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>

                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="function(s, e) { tabsActiveTabChanged(s,e);}" />

                </dxtc:ASPxPageControl>
            </div>
            <dx:ASPxTextBox ID="txtLlog" runat="server" Text="" Visible="true" ClientInstanceName="txtLlog"
                Width="0%" EnableTheming="False" BackColor="White" Border-BorderColor="White"
                ForeColor="White">
                <Border BorderColor="White" />
            </dx:ASPxTextBox>
            <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hfArtikuj" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="gridDataObject" runat="server" />
                    <asp:HiddenField ID="gridRezerva" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hfAutorizime" runat="server" />
                    <asp:HiddenField ID="hfBuxheti1" runat="server" />
                    <asp:HiddenField ID="hfBuxheti2" runat="server" />
                    <%--<asp:HiddenField ID="hfFushatShtese" runat="server" />--%>
                    <asp:HiddenField ID="hfSkema" runat="server" />
                    <asp:HiddenField ID="hfFurntiori" runat="server" />
                    <asp:HiddenField ID="hfEmertimiF" runat="server" />
                    <asp:HiddenField ID="hfPrioritetiF" runat="server" />
                    <asp:HiddenField ID="hfArtikulli" runat="server" />
                    <asp:HiddenField ID="hfEmertimiA" runat="server" />
                    <asp:HiddenField ID="hfPrioritetiA" runat="server" />
                    <asp:HiddenField ID="hfKodifikime" runat="server" />
                    <asp:HiddenField ID="hfArtikujtPerberes" runat="server" />
                    <asp:HiddenField ID="hfGjendjeArtikulli" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfTemplateArtikujPerberes" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <%-- per konfigurimin e lupave--%>
                    <asp:HiddenField ID="hfLupaKodbari" runat="server" />
                    <asp:HiddenField ID="hfLupaKodifikim1" runat="server" />
                    <asp:HiddenField ID="hfLupaKodifikim2" runat="server" />
                    <asp:HiddenField ID="hfLupaKodifikim3" runat="server" />
                    <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
                    <asp:HiddenField ID="hfLupaFurnitori" runat="server" />
                    <asp:HiddenField ID="hfLupaKategoriDetajimi" runat="server" />
                    <asp:HiddenField ID="hfLupaDetajimNga" runat="server" />
                    <asp:HiddenField ID="hfLupaSkema" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogInv" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogBle" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogShit" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogTretet" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogShpe" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogPakesim" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogAmortizimi" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogKomision" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogR" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogPakR" runat="server" />
                    <asp:HiddenField ID="hfLupaArtikuj" runat="server" />
                    <asp:HiddenField ID="hfLupaArtikujPerberes" runat="server" />
                    <asp:HiddenField ID="hfLupaMagazina" runat="server" />
                    <asp:HiddenField ID="hfSkemaKlasa" runat="server" />
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfMetoda" runat="server" />
                    <asp:HiddenField ID="hfArtikujtEkzistues" runat="server" />
                    <asp:HiddenField ID="hfTrupiFillimit" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="hfKodetArtPerberes" runat="server" />
                    <asp:HiddenField ID="HfGridCol" runat="server" />
                    <asp:HiddenField ID="HfGridColGjendjeArt" runat="server" />
                    <asp:HiddenField ID="HfGridColV" runat="server" />
                    <asp:HiddenField ID="hfKodbaret" runat="server" />
                    <asp:HiddenField ID="hfLupaAutomjet" runat="server" />
                    <asp:HiddenField ID="hfVfone" runat="server" />
                    <asp:HiddenField ID="hfFormatSeriali" runat="server" />

                    <%-- Hidden fields per Arkiven--%>
                    <asp:HiddenField ID="hfArkivaDokId" runat="server" />
                    <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
                    </dx:ASPxHiddenField>
                    <asp:HiddenField ID="hfSeriale" runat="server" />
                    <%--<dx:ASPxHiddenField ID="hfKodbar" runat="server" ClientInstanceName="hfKodbar" OnCustomCallback="hfKodbar_CustomCallback">
				<ClientSideEvents EndCallback="function (s,e){ }" BeginCallback="function (s,e){}"
						CallbackError="function (s,e){ }" />
				 </dx:ASPxHiddenField>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            <dx:ASPxHiddenField ID="hfNrAutoKF" runat="server" ClientInstanceName="hfNrAutoKF">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfTeDrejtaCmimi" runat="server" ClientInstanceName="hfTeDrejtaCmimi">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
            </dx:ASPxHiddenField>
            <asp:HiddenField ID="hfMeme" runat="server" />
        </div>
        <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" ShowCollapseButton="true" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                    EnableAnimation="False" PopupVerticalAlign="WindowCenter" AllowResize="True"
                    AppearAfter="10" ClientIDMode="AutoID">
                    <ClientSideEvents Closing="function(s, e) { popupUniversal.SetContentUrl(''); }" />
                    <ContentStyle>
                        <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                            PaddingTop="1px" />
                    </ContentStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupHelp" runat="server" AllowDragging="True" ClientInstanceName="popupHelp"
                    CloseAction="CloseButton" EnableAnimation="False" HeaderText="Ndihma" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID"
                    Height="400px" SettingsLoadingPanel-ImagePosition="Top" Width="400px">
                    <ContentStyle VerticalAlign="Top">
                    </ContentStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl12" runat="server">
                            <dx:ASPxMemo ID="memoNdihma" runat="server" Height="300px" Width="350px" ClientInstanceName="memoNdihma"
                                ReadOnly="True">
                                <ValidationSettings>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </dx:ASPxMemo>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
