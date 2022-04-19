<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_FleteKontabel.aspx.cs" Inherits="PlatinumWeb.Shto_FleteKontabel" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server"
        id="themeJQuery" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" media="screen" rel="stylesheet" type="text/css" />
    <!-- DevExtreme themes -->
    <link rel="stylesheet" type="text/css" href="Content/dx.common.css" />
    <link rel="stylesheet" type="text/css" href="Content/dx.generic.alphaweb-compact.css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <!-- A DevExtreme library -->
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/myMesazh-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/memoryObject.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/JsGlobal.js;~/Scripts/jszip.min.js;~/Scripts/dx.viz-web.js;~/js/localization/DevExtreme.Perkthime.js;~/js/json2.js;~/js/myDxDataGrid.js;~/js/GridaRegjQendraKosto.js;~/js/aspx.js/Shto_FleteKontabel.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
    <style>
        .dx-overlay-content {
            width: 300px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">

        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ try{window.parent.SessionTimeout.sendKeepAlive();} catch(ee){}}" />--%>
        </dx:ASPxGlobalEvents>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                <ClientSideEvents ItemClick="function(s, e) {
	                        menu_click(s,e);
                            }"
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
                            <div id="dvMenu" style="display: none">
                                <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                            BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                            <ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}" />
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
                                        <dx:ASPxLabel ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                            OnClick="ButtonOk_Click2" Text="Ok">
                                                            <ClientSideEvents Click="function(s, e) {
	popFshi.Hide();
    Utils.shfaqLoadingGif();;
}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                            <ClientSideEvents Click="function(s, e) {
		popFshi.Hide();
}" />
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
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popMesazhQK" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popMesazhQK" CloseAction="None" EnableAnimation="False" EnableViewState="False"
                    Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" Width="300px" ShowCloseButton="False">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl7" runat="server">
                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel17" runat="server" ClientIDMode="AutoID" Width="271px">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent17" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel Wrap="true" ID="lblMsgbox4" runat="server" ClientIDMode="AutoID" Text="Deshironi te beni shperndarjen ne qendrat e kostos?"
                                            ClientInstanceName="lblmesazhqendra">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonOkQK" runat="server" CausesValidation="False" ClientInstanceName="ButtonOkQK"
                                                            AutoPostBack="false" Text="Po">
                                                            <ClientSideEvents Click="function(s, e) {
	popMesazhQK.Hide();
  hapPopUp(s,e);
}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonCancelQK" runat="server" ClientIDMode="AutoID" Text="Jo"
                                                            AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s, e) {
		popMesazhQK.Hide();
        JopopupClick(s,e);
}" />
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
                <iframe id="Container" runat="server" frameborder="0" height="0" name="Container"
                    width="0"></iframe>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
            </Triggers>
            <ContentTemplate>
                <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
                <asp:HiddenField ID="hfStatusQendra" runat="server" />
                <asp:HiddenField ID="hfNrRef" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" Value="0" />
                <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo" />
                <asp:HiddenField ID="hfIdKonfigAmbjenteQK" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="730px" ClientInstanceName="splitter"
            PaneMinSize="700px">
            <Panes>
                <%-- Header pane--%>
                <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ScrollBars="Vertical">
                    <Separators Size="10px">
                    </Separators>
                    <ContentCollection>
                        <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                            <asp:Panel ID="ContentPanel" runat="server">
                                <asp:UpdatePanel ID="pnlLidhur" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table id='hl' runat="server">
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <br />
                                <div id="accordition">
                                    <div>
                                        <h3 id="kokeKonfigurimi"><span id="kokeKonfigurimidiv" class="ui-not-accordion-header-text">Koke Dokumenti:</span></h3>
                                        <div>
                                            <table id="tblKonfigurimi" runat="server">
                                            </table>
                                            <table id="tblFleteKontabel" class="renditKontrolle">
                                                <tbody>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="trup">
                                        <h3 id="trupKonfigurimi"><span id="trupKonfigurimidiv" class="ui-not-accordion-header-text">Trup Dokumenti</span></h3>
                                        <div>
                                            <div id="divgride1" style="display: none; width: 100%">
                                                <div id="divgride2">
                                                    <table id="rowed5">
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <h3 id="fundKonfigurimi"><span id="fundKonfigurimidiv" class="ui-not-accordion-header-text">Fund Dokumenti</span></h3>
                                        <div>
                                            <table id="tblTotale" align="right" class="renditKontrolle">
                                                <tbody>
                                                </tbody>
                                            </table>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <div id="divQk" style="display: none">
                                                <hr />
                                                <table id="tblQendra" class="renditKontrolle">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                                <br />
                                                <br />
                                                <div id="QKDataGrid" style="min-width: 800px; margin-top: 20px;" class=""></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div id="dvFillim" class="atributeDiveFshehur">
                                    <dx:ASPxLabel Wrap="False" ID="konfigurimi_Label" runat="server" Text="Zgjidhni konfigurimin:"
                                        ClientInstanceName="konfigurimi_Label" AssociatedControlID="konfigurimi_ComboBox">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="konfigurimi_ComboBox" runat="server" ClientInstanceName="konfigurimi_ComboBox"
                                        ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
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
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi"
                                        ClientInstanceName="lblKonfigurimi" Text="">
                                    </dx:ASPxLabel>
                                    <dx:ASPxLabel Wrap="False" ID="lblNrDokumenti" runat="server" ClientInstanceName="lblNrDokumenti"
                                        Text="Nr. Dokumenti" AssociatedControlID="txtNrDokumenti">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtNrDokumenti" runat="server" Width="100%" ClientInstanceName="txtNrDokumenti">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblData" runat="server" Text="Data" ClientInstanceName="lblData"
                                        AssociatedControlID="Data_DateEdit">
                                    </dx:ASPxLabel>
                                    <dx:ASPxDateEdit ID="Data_DateEdit" runat="server" ClientInstanceName="Data_DateEdit"
                                        Width="100%" ShowShadow="False">
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <ClientSideEvents ValueChanged="function(s, e) {kurset();}" GotFocus="function(s, e){ dateGotFocus( s, e); }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <dx:ASPxLabel Wrap="False" ID="lblNrReference" runat="server" Text="Nr. Reference"
                                        ClientInstanceName="lblNrReference" AssociatedControlID="txtNrReference">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtNrReference" runat="server" Width="100%" ClientInstanceName="txtNrReference">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblPershkrimi" runat="server" Text="Pershkrimi" ClientInstanceName="lblPershkrimi"
                                        AssociatedControlID="txtPershkrimi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxMemo ID="txtPershkrimi" runat="server" ClientInstanceName="txtPershkrimi"
                                        Width="100%" Rows="3">
                                        <ClientSideEvents TextChanged="function(s, e) {pershkrimi();}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <dx:ASPxLabel Wrap="False" ID="lblRuajNeGrup" runat="server" Text="Ruaj ne grup"
                                        ClientInstanceName="lblRuajNeGrup" AssociatedControlID="txtNrGrupKontabilizimi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="txtNrGrupKontabilizimi" runat="server" ClientInstanceName="txtNrGrupKontabilizimi"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%" ShowShadow="False">
                                        <ClientSideEvents ButtonClick="function(s, e) {Shfaq();}" />
                                        <LoadingPanelImage>
                                        </LoadingPanelImage>
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
                                    <dx:ASPxLabel Wrap="False" ID="lblSkemaKontabel" runat="server" Text="Skema Kontabel"
                                        ClientInstanceName="lblSkemaKontabel" AssociatedControlID="btneSkemaFK">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="btneSkemaFK" runat="server" ClientInstanceName="btneSkemaFK"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%" ShowShadow="False">
                                        <ClientSideEvents ButtonClick="function(s, e) {ShfaqSkemeFleteKontabel();}" LostFocus="function(s, e) {lostFocusSkemaKontabel(s.GetText());}" />
                                        <LoadingPanelImage>
                                        </LoadingPanelImage>
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
                                    <dx:ASPxButton ID="btnRuaj1" runat="server" Text=" " CausesValidation="False" Width="100%"
                                        ClientInstanceName="btnRuaj1" AutoPostBack="False">
                                        <ClientSideEvents LostFocus="function(s,e){myJQGrid.focusGrid({emergride: '#rowed5', isLidhur: lidhur, idKoloneGrideFokus: arrayIdKolonaGrides[0]});}" Click="function(s, e){merrTeDhena(s,e);popRuajSkeme.Show();}" />
                                        <Image Url="~/images/03.bmp" />
                                    </dx:ASPxButton>
                                    <dx:ASPxButton ID="btnLlogarit" Width="100%" runat="server" Text="Llogarit Azhornim"
                                        ClientInstanceName="btnLlogarit" AutoPostBack="False" CausesValidation="False">
                                        <ClientSideEvents Click="function(s, e) { Utils.shfaqLoadingGif();; LlogaritAzhornim(); 
}" />
                                    </dx:ASPxButton>
                                </div>
                                <dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server" ClientInstanceName="hfNrAutoShitje">
                                </dx:ASPxHiddenField>
                                <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
                                </dx:ASPxHiddenField>
                                <br />

                                <div id="dvFundi" class="atributeDiveFshehur">
                                    <dx:ASPxLabel Wrap="False" ID="lblTotali" runat="server" Text="Totali" ClientInstanceName="lblTotali"
                                        AssociatedControlID="txtDebiMonedhaBaze">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtDebiMonedhaBaze" runat="server" Text="0" ClientInstanceName="txtDebiMonedhaBaze"
                                        Width="100%">
                                        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtKrediMonedhaBaze" runat="server" Text="0" ClientInstanceName="txtKrediMonedhaBaze"
                                        Width="100%">
                                        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblDiferenca" runat="server" Text="Diferenca" ClientInstanceName="lblDiferenca"
                                        AssociatedControlID="txtDebiDiferenca">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtDebiDiferenca" runat="server" Text="0" ClientInstanceName="txtDebiDiferenca"
                                        Width="100%">
                                        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtKrediDiferenca" runat="server" Text="0" ClientInstanceName="txtKrediDiferenca"
                                        Width="100%">
                                        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblDateRegjistrimi" runat="server" Text="Date Regjistrimi"
                                        ClientInstanceName="lblDateRegjistrimi" AssociatedControlID="DateRegjistrimi_DateEdit">
                                    </dx:ASPxLabel>
                                    <dx:ASPxDateEdit ID="DateRegjistrimi_DateEdit" runat="server" Width="100%" ShowShadow="False"
                                        ClientInstanceName="DateRegjistrimi_DateEdit">
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <dx:ASPxComboBox ID="cmbPeriudha" runat="server" Width="100%" ShowShadow="False"
                                        Visible="False" ClientInstanceName="cmbPeriudha" SettingsLoadingPanel-ImagePosition="Top">
                                        <LoadingPanelImage>
                                        </LoadingPanelImage>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblPeriudha" runat="server" Text="Periudha kontabel"
                                        ClientInstanceName="lblPeriudha" ClientVisible='false' AssociatedControlID="cmbPeriudha">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="btnPeriudha" runat="server" ClientInstanceName="btnPeriudha"
                                        ClientVisible="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                        <LoadingPanelImage>
                                        </LoadingPanelImage>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <ClientSideEvents ButtonClick="function(s, e) {ShfaqPeriudhen();}" LostFocus="function(s, e) {
                                                                valueChangedPeriudha();}" />
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblPeriudhaAktuale" ClientInstanceName="lblPeriudhaAktuale"
                                        runat="server" Text="" ClientVisible='false' AssociatedControlID="btnPeriudha">
                                    </dx:ASPxLabel>
                                    <dx:ASPxLabel Wrap="False" ID="lblLlojDokumenti" runat="server" Text="Lloj Dokumenti"
                                        ClientInstanceName="lblLlojDokumenti">
                                    </dx:ASPxLabel>
                                    <dx:ASPxButtonEdit ID="lblKodiSkemaKontabel" runat="server" ClientInstanceName="lblKodiSkemaKontabel"
                                        ReadOnly="True" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) {NrDokumentiClick();}" />
                                        <Buttons>
                                            <dx:EditButton>
                                            </dx:EditButton>
                                        </Buttons>
                                        <ValidationSettings>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </dx:ASPxButtonEdit>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <div id="div1" style="display: none">
                                    <hr />

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji" runat="server"
                                        Text="Lloj qendre:" ClientInstanceName="lblLloji">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji" ShowShadow="False"
                                        Width="100%" SettingsLoadingPanel-ImagePosition="Top">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {cmbQendraKosto.SetValue(null);}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField ErrorText="*" IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbQendraKosto" ID="lblQendraKosto"
                                        runat="server" Text="Qendra Kosto" ClientInstanceName="lblQendraKosto">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbQendraKosto" ClientInstanceName="cmbQendraKosto" runat="server" ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top"
                                        EnableCallbackMode="True" Width="100%" OnItemRequestedByValue="cmbQendraKosto_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbQendraKosto_ItemsRequestedByFilterCondition">
                                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickQendraKosto();}" LostFocus="function(s,e){TextChangedQendraKosto();}" />
                                        <LoadingPanelImage>
                                        </LoadingPanelImage>
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
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbObjektiva" ID="lblObjektiva"
                                        runat="server" Text="Objektiva" ClientInstanceName="lblObjektiva">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbObjektiva" ClientInstanceName="cmbObjektiva" runat="server"
                                        ShowShadow="False" Width="100%" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top"
                                        EnableCallbackMode="True" OnItemRequestedByValue="cmbObjektiva_ItemRequestedByValue">
                                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickObjektiva();}" LostFocus="function(s,e){TextChangedObjektiva();}" />
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
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>

                                </div>
                            </asp:Panel>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
            </Panes>
            <Styles>
            </Styles>
            <Images>
            </Images>
        </dx:ASPxSplitter>
        <asp:UpdatePanel ID="pnlpopup" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="ASPxPopupControl1" runat="server" AllowDragging="True"
                    AllowResize="True" AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                    <ClientSideEvents CloseUp="function(s,
    e) { closePopup(s,e);
    
   
     }" />
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popRuajSkeme" runat="server" AllowDragging="True" ClientInstanceName="popRuajSkeme"
                    CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Ruaj Skemen e Fletes Kontabel"
                    Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowHeader="true" Width="350px">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="RuajFiltrin_ASPxRoundPanel" runat="server" Width="300px"
                                HeaderText="Ruaj Skemen e FletesKontebel" EnableTheming="False" GroupBoxCaptionOffsetY="-22px">
                                <ContentPaddings PaddingBottom="5px" PaddingLeft="2px" PaddingTop="5px" />
                                <HeaderStyle Height="23px">
                                    <Paddings PaddingBottom="0px" PaddingLeft="3px" PaddingTop="0px" />
                                </HeaderStyle>
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="Kodi_ASPxLabel" runat="server" Text="Kodi:">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox ID="Kodi_ASPxTextBox" runat="server" ClientInstanceName="Kodi_ASPxTextBox"
                                                        Width="100%">
                                                        <ValidationSettings CausesValidation="True">
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                    <asp:CustomValidator ID="Kodi_CustomValidator" runat="server" ControlToValidate="Kodi_ASPxTextBox"
                                                        Display="None" ErrorMessage="Ky kod ekziston" OnServerValidate="Kodi_CustomValidator_ServerValidate">*</asp:CustomValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                        HighlightCssClass="validorCalloutHighlight" TargetControlID="Kodi_CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="Shenime_ASPxLabel" runat="server" Text="Emertimi:">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox ID="Shenime_ASPxTextBox" runat="server" Width="100%">
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Niveli i Autorizimit:">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox ID="cmbAutorizimi" runat="server" ClientInstanceName="cmbAutorizimi"
                                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                KPF=0;
	                                                            Autorizime_Click();
                                                            }" />
                                                        <LoadingPanelImage>
                                                        </LoadingPanelImage>
                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings SetFocusOnError="True">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <dx:ASPxButton ID="Ruaj_ASPxButton" runat="server" OnClick="Ruaj_ASPxButton_Click"
                                                        Text="Ruaj">
                                                        <ClientSideEvents Click="function(s, e) {
                                                              if(Kodi_ASPxTextBox.GetText()!='' )                          
	                                                            popRuajSkeme.Hide();
                                                            }" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>
        <dxcp:ASPxCallbackPanel EnableHierarchyRecreation="false" ID="ASPxCallbackPanel1" ClientInstanceName="callBackPanel"
            runat="server" Width="200px" OnCallback="ASPxCallbackPanel_Callback1">
            <ClientSideEvents EndCallback="function(s, e) {endCallBack();}" />
            <PanelCollection>
                <dxp:PanelContent>
                    <asp:UpdatePanel ID="pnlhf" runat="server">
                        <ContentTemplate>

                            <asp:HiddenField ID="hfGrupKontabilizimi" runat="server" />
                            <asp:HiddenField ID="hfNrLlogaria" runat="server" />
                            <asp:HiddenField ID="hfEmerLlogaria" runat="server" />
                            <asp:HiddenField ID="hfPershkrimi" runat="server" />
                            <asp:HiddenField ID="hfMonedha" runat="server" />
                            <asp:HiddenField ID="hfKursi" runat="server" />
                            <asp:HiddenField ID="hfDK" runat="server" />
                            <asp:HiddenField ID="hfDebi" runat="server" />
                            <asp:HiddenField ID="hfKredi" runat="server" />
                            <asp:HiddenField ID="hfDebiMon" runat="server" />
                            <asp:HiddenField ID="hfKrediMon" runat="server" />
                            <asp:HiddenField ID="hfSkemaKontabel" runat="server" />
                            <asp:HiddenField ID="hfKolonaGride" runat="server" />
                            <asp:HiddenField ID="HfGridCol" runat="server" />
                            <asp:HiddenField ID="hfLidhur" runat="server" />
                            <%-- per te ruajtur vlerat e konfigurimit te  formati te numrave--%>
                            <asp:HiddenField ID="hfFormatNr" runat="server" />
                            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                            <asp:HiddenField ID="hfKontrollet" runat="server" />
                            <asp:HiddenField ID="hfKonffillestar" runat="server" />
                            <asp:HiddenField ID="hfMonedhaNder" runat="server" />
                            <asp:HiddenField ID="hfIdMonedhaNder" runat="server" />
                            <asp:HiddenField ID="HfColTrup" runat="server" />
                            <asp:HiddenField ID="HfColLlog" runat="server" />
                            <asp:HiddenField ID="HfColMon" runat="server" />
                            <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
                            <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </dxp:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
        <%-- per te ruajtur vlerat e konfigurimit te lupave, merret nga konfig i dok me web service --%>
        <asp:HiddenField ID="hfLupaDokumenti" runat="server" />
        <asp:HiddenField ID="hfLupaMagazina" runat="server" />
        <asp:HiddenField ID="hfLupaSkemaFK" runat="server" />
        <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
        <asp:HiddenField ID="hfGridaLlogaria" runat="server" />
        <asp:HiddenField ID="gridDataObject" runat="server" />
        <asp:HiddenField ID="hfAzhornim" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState">
        </dx:ASPxHiddenField>
    </form>
</body>
</html>
