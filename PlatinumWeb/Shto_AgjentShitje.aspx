<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_AgjentShitje.aspx.cs"
    Inherits="PlatinumWeb.Shto_AgjentShitje" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Shto_AgjentShitje.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdPnl1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                OnItemClick="ASPxMenu1_ItemClick">
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
                  
                <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
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
                            </dx:ASPxPanel >
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvAgjenteShitje" style="display: none">
            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
                ActiveTabIndex="0"   TabSpacing="3px" Width="100%" Height="520px">
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                <table class="renditKontrolle">
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" Style="font-size: large" Text="Modeli:">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth33">
                                            <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                                Style="font-size: medium" Height="24px" Width="100%" AnimationType="None">
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
                                            <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth33"></td>
                                    </tr>
                                </table>
                                <dx:ASPxGridView ID="grid_AgjenteShitje" runat="server" ClientInstanceName="grid_AgjenteShitje"
                                    OnAfterPerformCallback="grid_AgjenteShitje_AfterPerformCallback" Width="100%"
                                    OnCustomCallback="grid_AgjenteShitje_CustomCallback" OnDataBound="grid_AgjenteShitje_DataBound"
                                    OnProcessColumnAutoFilter="grid_AgjenteShitje_ProcessColumnAutoFilter">
                                    <ClientSideEvents RowDblClick="function(s, e) {
            OnGridDoubleClick(e.visibleIndex); kaloTab=true;    	
}"
                                        FocusedRowChanged="function(s, e) {
            mbush=true;	
}"
                                        BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
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
                    <dxtc:TabPage Name="Agjent Shitje" Text="Agjent Shitje">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                <table id="tblAgjenteShitje" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                                <%--<div id="dvlblKodi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="kodiTextBox" ID="lblKodi" runat="server" Text="Kodi:" ClientInstanceName="lblKodi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvkodi_TextBox">--%>
                                <dx:ASPxTextBox ID="kodiTextBox" runat="server" ClientInstanceName="kodiTextBox"
                                    Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <%--<div id="dvlblEmri">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="emriTextBox" ID="lblEmri" runat="server" Text="Emri:" ClientInstanceName="lblEmri">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvemri_TextBox">--%>
                                <dx:ASPxTextBox ID="emriTextBox" runat="server" ClientInstanceName="emriTextBox"
                                    Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <%--<div id="dvlblMbiemri">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="mbiemriTextBox" ID="lblMbiemri" runat="server" Text="Mbiemri:" ClientInstanceName="lblMbiemri">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvmbiemri_TextBox">--%>
                                <dx:ASPxTextBox ID="mbiemriTextBox" runat="server" ClientInstanceName="mbiemriTextBox"
                                    Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <%--    <RequiredField IsRequired="false" ErrorText="Jepni mbiemrin" />--%>
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                <%--</div>--%>

                                <%--<div id="dvlblTelefoni">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="telTextBox" ID="lblTelefoni" runat="server" Text="Telefon:" ClientInstanceName="lblTelefoni">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtel_TextBox">--%>
                                <dx:ASPxTextBox ID="telTextBox" runat="server" ClientInstanceName="telTextBox"
                                    Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <%--  <RegularExpression ValidationExpression="[0-9]*" ErrorText="Lejohen vetem numra!" />--%>
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <%--<div id="dvlblFax">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="faxTextBox" ID="lblFax" runat="server" Text="Fax:" ClientInstanceName="lblFax">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvfax_TextBox">--%>
                                <dx:ASPxTextBox ID="faxTextBox" runat="server" ClientInstanceName="faxTextBox"
                                    Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <%--    <RegularExpression ValidationExpression="[0-9]*" ErrorText="Lejohen vetem numra!" />--%>
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <%--<div id="dvlblEmail">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="emailTextBox" ID="lblEmail" runat="server" Text="E-mail:" ClientInstanceName="lblEmail">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvemail_TextBox">--%>
                                <dx:ASPxTextBox ID="emailTextBox" runat="server" ClientInstanceName="emailTextBox"
                                    Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <%--<RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ErrorText="Format i gabuar e-mail!" />--%>
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <%--<div id="dvlblPerqindje">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="perqindjeASPxTextBox" ID="lblPerqindje" runat="server" Text="Perqindje:" ClientInstanceName="lblPerqindje">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvperqindje_ASPxTextBox">--%>
                                <dx:ASPxTextBox ID="perqindjeASPxTextBox" runat="server" ClientInstanceName="perqindjeASPxTextBox" Width="100%">
                                    <ValidationSettings Display="Dynamic" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <%--<div id="dvlblQyteti">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="qytetiASPxComboBox" ID="lblQyteti" runat="server" Text="Qyteti:" ClientInstanceName="lblQyteti">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvqyteti_ASPxComboBox">--%>
                                <dx:ASPxComboBox ID="qytetiASPxComboBox" runat="server" Width="100%" ClientInstanceName="qytetiASPxComboBox"
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
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblLlogari">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLlogari" ID="lblLlogari" runat="server" Text="Llogari:" ClientInstanceName="lblLlogari">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtLlogari">--%>
                                <dx:ASPxComboBox ID="txtLlogari" 
                                    ClientInstanceName="txtLlogari" 
                                    Width="100%" 
                                    runat="server"
                                    SettingsLoadingPanel-ImagePosition="Top" 
                                    ShowShadow="False"
                                    OnItemRequestedByValue="txtLlogari_ItemRequestedByValue"
                                    OnItemsRequestedByFilterCondition="txtLlogari_ItemsRequestedByFilterCondition">
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <ClientSideEvents ButtonClick="function(s, e) {
                                            grida=false;
                                            Llogari_Click();}"
                                        LostFocus="function(s, e) {
                                                                    LostFocusLlogaria();
                                                                 }"
                                        TextChanged="function(s, e) {
	                                                                   nrLlogariChange();
                                                                   }" />
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblAutorizimi"> --%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAutorizimi" ID="lblAutorizimi"
                                    runat="server" Text="Nivel Autorizimi:" ClientInstanceName="lblAutorizimi">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%--<div id="dvcmbAutorizimi"> --%>
                                <div>
                                        <select id="cmbAutorizimi">
                                        </select>
                                        <asp:HiddenField ID="cmbAutorizimiHf" ClientIDMode="Static" runat="server" />
                                               
                                    </div>
                                   <dx:ASPxLabel Wrap="False" AssociatedControlID="lblPerdoruesi" ID="lblPerdoruesi"
                                    runat="server" Text="Perdoruesi:" ClientInstanceName="lblPerdoruesi">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%--<div id="dvcmbAutorizimi"> --%>
                                <dx:ASPxComboBox ID="btnPerdoruesi" runat="server" ClientInstanceName="btnPerdoruesi" 
                                    SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                    <ClientSideEvents ButtonClick="function(s, e) {	Perdorues_Click();}" />
                                         
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        ValidationGroup="entries1" ValidateOnLeave="false">
                                        <RequiredField IsRequired="True"></RequiredField>
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>    
                                 <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbDrejtori" ID="lblDrejtori"
                                    runat="server"  ClientInstanceName="lblDrejtori">
                                </dx:ASPxLabel>
                                <%--</div> --%>
                                <%--<div id="dvcmbAutorizimi"> --%>
                                <dx:ASPxComboBox ID="cmbDrejtori" runat="server" ClientInstanceName="cmbDrejtori" 
                                    SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%" >
                                    <ClientSideEvents ButtonClick="Drejtori_Click" />
                                         
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        ValidationGroup="entries1" ValidateOnLeave="false">
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>                      
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="activeTabChanged" />
             
                <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
            </dxtc:ASPxPageControl >
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogaria" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfAutorizime" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                    <%--<dx:ASPxLabel ID="pergjigja" runat="server" Text="">
                </dx:ASPxLabel>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                        CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                        EnableAnimation="False" PopupVerticalAlign="WindowCenter" AllowResize="True"
                        AppearAfter="10" ClientIDMode="AutoID" Height="400px">
                        <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');  
}" />
                        <ContentStyle>
                            <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                                PaddingTop="1px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl >
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
                    </dx:ASPxPopupControl >
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Zgjidh llogarine "
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <iframe id="Container" name="ContainerLlogari" frameborder="0" runat="server"></iframe>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        </div>
    </form>
</body>
</html>
