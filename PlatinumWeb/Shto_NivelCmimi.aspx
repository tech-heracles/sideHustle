<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_NivelCmimi.aspx.cs"
    Inherits="PlatinumWeb.Shto_NivelCmimi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


    
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
      <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/aspx.js/Shto_NivelCmimi.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
                </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"
                                ItemImagePosition="Top" OnDataBound="ASPxMenu1_DataBound" OnItemClick="ASPxMenu1_ItemClick"
                                SeparatorWidth="1px" ShowPopOutImages="True" Width="100%">
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
                                    <dx:ASPxMenu ID="MenuInfo" runat="server" BorderBetweenItemAndSubMenu="HideRootOnly"
                                        ClientIDMode="AutoID" ClientInstanceName="MenuInfo" ShowPopOutImages="True" Width="100%">
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
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popFshi" CloseAction="CloseButton" CssPostfix="Glass" EnableAnimation="False"
                    EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" Width="300px">
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
                            </dx:ASPxPanel >
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvNiveli" style="display: none">
            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
                  TabSpacing="3px" Width="100%" Height="600px" OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged"
                ActiveTabIndex="1">
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                <table class="renditKontrolle">
                                    <tbody>
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                    runat="server" ClientIDMode="AutoID" Style="font-size: large" Text="Modeli:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33">
                                                <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                    Height="24px" ShowShadow="False" Style="font-size: medium" ValueType="System.String"
                                                    SettingsLoadingPanel-ImagePosition="Top" Width="100%" AnimationType="None">
                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
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
                                            </td>
                                            <td class="renditKontrolleLabelMeWidth33">
                                                <dx:ASPxLabel ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi" ClientIDMode="AutoID"
                                                    ClientInstanceName="lblKonfigurimi">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dx:ASPxGridView ID="gvNivelCmimi" ClientInstanceName="gvNivelCmimi" runat="server"
                                    OnDataBound="gvNivelCmimi_DataBound" OnAfterPerformCallback="gvNivelCmimi_AfterPerformCallback"
                                    OnHeaderFilterFillItems="gvNivelCmimi_HeaderFilterFillItems" OnProcessColumnAutoFilter="gvNivelCmimi_ProcessColumnAutoFilter"
                                    Width="100%" OnCustomCallback="gvNivelCmimi_CustomCallback" OnCustomJSProperties="gvNivelCmimi_CustomJSProperties">
                                    <Templates>
                                        <TitlePanel>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Zgjidh kolonat" AutoPostBack="false"
                                                            ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                            <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,gvNivelCmimi)}"
                                                                Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="pnlruaj" runat="server">
                                                            <ContentTemplate>
                                                                <dx:ASPxButton ID="ASPxButton3" runat="server" Text="Ruaj kolonat" AutoPostBack="true"
                                                                    ClientVisible="false" Image-Url="images/new/disk_blue (3).png" Font-Size="8"
                                                                    OnClick="RuajKolona_Click">
                                                                    <ClientSideEvents Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                                </dx:ASPxButton>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                    </Templates>
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
            mbush=true;	
}" RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }" SelectionChanged="function(s, e){OnGridSelectionChanged(e);}"
                                        BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
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
                    <dxtc:TabPage  Name="Nivel Cmimi" Text="Nivel Cmimi">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                <table id="tblNivelCmimi" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                                <%--<div id="dvlblKodi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                    Text="Kodi:" ClientInstanceName="lblKodi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtKodi">--%>
                                <dx:ASPxTextBox ID="txtKodi" runat="server" AutoPostBack="false" ClientInstanceName="txtKodi"
                                    Width="100%">
                                    <ClientSideEvents Init="function(s, e) { s.Focus();  }" />
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" SetFocusOnError="True" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 50 karaktere" ValidationExpression="^[\s\S]{0,50}$">
                                        </RegularExpression>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <%--<div id="dvlblEmertimi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi" ID="lblEmertimi" runat="server"
                                    Text="Emertimi i nivelit:" ClientInstanceName="lblEmertimi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtEmertimi">--%>
                                <dx:ASPxTextBox ID="txtEmertimi" runat="server" AutoPostBack="false" ClientInstanceName="txtEmertimi"
                                    Width="100%" Rows="3">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" SetFocusOnError="True" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <%--<div id="dvlblPrindi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="btneEmertimPrindi" ID="lblPrindi"
                                    runat="server" Text="Emertimi i prindit:" ClientInstanceName="lblPrindi">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvbtneEmertimPrindi">--%>
                                <dx:ASPxComboBox ID="btneEmertimPrindi" runat="server" ClientInstanceName="btneEmertimPrindi"
                                    EnableCallbackMode="False" ReadOnly="false" ValidationSettings-CausesValidation="True"
                                     SettingsLoadingPanel-ImagePosition="Top"
                                    ShowShadow="False" Width="100%">
                                    <ClientSideEvents ButtonClick="function(s, e) { NivelCmimi_Click();	}" 
                                        SelectedIndexChanged="Selected_IndexChanged" />
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
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--  <asp:CustomValidator ID="CustomValidator8" runat="server" ControlToValidate="btneEmertimPrindi"
                                    Display="None" ErrorMessage="Ky nivel nuk ekziston" OnServerValidate="btneEmertimPrindi_CustomValidator_ServerValidate"
                                    ValidationGroup="entries">*</asp:CustomValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True"
                                    HighlightCssClass="validorCalloutHighlight" TargetControlID="CustomValidator8">
                                </cc1:ValidatorCalloutExtender>--%>
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
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True"
                                        ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblMonedha">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMonedha" ID="lblMonedha" runat="server"
                                    Text="Monedha:" ClientInstanceName="lblMonedha">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcmbMonedha">--%>
                                <dx:ASPxComboBox ID="cmbMonedha" runat="server" ClientInstanceName="cmbMonedha"
                                    ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True"
                                        ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblBrutoNeto">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbBrutoNeto" ID="lblBrutoNeto"
                                    runat="server" Text="Me TVSH/Pa TVSH:" ClientInstanceName="lblBrutoNeto">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcmbBrutoNeto">--%>
                                <dx:ASPxComboBox ID="cmbBrutoNeto" runat="server" ClientInstanceName="cmbBrutoNeto"
                                    ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
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
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblPrioriteti">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPrioriteti" ID="lblPrioriteti"
                                    runat="server" Text="Prioriteti:" ClientInstanceName="lblPrioriteti">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcmbPrioriteti">--%>
                                <dx:ASPxComboBox ID="cmbPrioriteti" runat="server" ClientInstanceName="cmbPrioriteti"
                                    ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True"
                                        ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblNjesiTeVarura">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbNjesiTeVarura" ID="lblNjesiTeVarura"
                                    runat="server" Text="Njesi Te Varura:" ClientInstanceName="lblNjesiTeVarura">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcbNjesiTeVarura">--%>
                                <dx:ASPxCheckBox ID="cbNjesiTeVarura" runat="server" ClientInstanceName="cbNjesiTeVarura"
                                    TextSpacing="2px" Width="100%">
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxCheckBox>
                                <%--</div>--%>
                                <%--<div id="dvlblTeVaruraNgaMonedha">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbTeVaruraNgaMonedha" ID="lblTeVaruraNgaMonedha"
                                    runat="server" Text="Te Varura Nga Monedha:" ClientInstanceName="lblTeVaruraNgaMonedha">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcbTeVaruraNgaMonedha">--%>
                                <dx:ASPxCheckBox ID="cbTeVaruraNgaMonedha" runat="server" ClientInstanceName="cbTeVaruraNgaMonedha"
                                    TextSpacing="2px" Width="100%">
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxCheckBox>
                                <%--</div>--%>                                
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbNivelCmimBaze" ID="lblNivelCmimBaze"
                                    runat="server" Text="Nivel cmimi baze:" ClientInstanceName="lblNivelCmimBaze">
                                </dx:ASPxLabel>
                                <dx:ASPxCheckBox ID="cbNivelCmimBaze" runat="server" ClientInstanceName="cbNivelCmimBaze"
                                    TextSpacing="2px" Width="100%">
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxCheckBox>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAutorizimi" ID="lblAutorizimi"
                                    runat="server" Text="Nivel Autorizimi:" ClientInstanceName="lblAutorizimi">
                                </dx:ASPxLabel>
                                  <div>
                                        <select id="cmbAutorizimi">
                                        </select>
                                        <asp:HiddenField ID="cmbAutorizimiHf" ClientIDMode="Static" runat="server" />
                                               
                                    </div>

                                 <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbDetajim" ID="lblDetajim"
                                    runat="server" Text="Detajimi:" ClientInstanceName="lblDetajim">
                                </dx:ASPxLabel>
                                <dx:ASPxComboBox ID="cmbDetajim" runat="server" ClientInstanceName="cmbDetajim"
                                    ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
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
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>

                                     <%--</div>--%>
                                <%--<div id="dvlblCmimRetail">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="btneCmimRetail" ID="lblCmimRetail"
                                    runat="server" Text="Merr cmimin nga:" ClientInstanceName="lblCmimRetail">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvbtneEmertimPrindi">--%>
                                <dx:ASPxComboBox ID="btneCmimRetail" runat="server" ClientInstanceName="btneCmimRetail"
                                    EnableCallbackMode="False" ReadOnly="false" ValidationSettings-CausesValidation="True"
                                     SettingsLoadingPanel-ImagePosition="Top"
                                    ShowShadow="False" Width="100%">
                                    <ClientSideEvents ButtonClick="function(s, e) { MerrNivelCmimiNga_Click();	}" 
                                        />
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
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="Active_TabChanged" />
                <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
            </dxtc:ASPxPageControl >
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hfPrindi" runat="server" />
                <asp:HiddenField ID="hfPrioriteteMax" runat="server" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="hfLupaPrindi" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>    
    <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
    </dx:ASPxHiddenField>
    <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" AllowResize="True"
                AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popupUniversal" CloseAction="CloseButton"
                EnableAnimation="False" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter">
                <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');
}" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl >
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
