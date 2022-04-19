<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_NivelTvsh.aspx.cs"
    Inherits="PlatinumWeb.Shto_NivelTvsh" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

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
    <style type="text/css">
        #ContainerAutorizime
        {
            width: 482px;
            height: 356px;
        }
    </style>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Shto_NivelTvsh.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
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
            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server"   TabSpacing="3px"
                ClientInstanceName="PageControl" Width="100%" Height="600px" ActiveTabIndex="1">
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                        <ContentCollection>
                            <dxw:ContentControl>
                                <table class="renditKontrolle">
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                runat="server" Style="font-size: large" Text="Modeli:">
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
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td class="renditKontrolleLabelMeWidth33">
                                            <dx:ASPxLabel ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth33"></td>
                                    </tr>
                                </table>
                                <dx:ASPxGridView Width="100%" ID="gvShtoNivelTvsh" ClientInstanceName="gvShtoNivelTvsh"
                                    runat="server" OnAfterPerformCallback="gvShtoNivelTvsh_AfterPerformCallback"
                                    OnCustomCallback="gvShtoNivelTvsh_CustomCallback" OnCustomJSProperties="gvShtoNivelTvsh_CustomJSProperties"
                                    OnDataBound="gvShtoNivelTvsh_DataBound" OnHeaderFilterFillItems="gvShtoNivelTvsh_HeaderFilterFillItems"
                                    OnProcessColumnAutoFilter="gvShtoNivelTvsh_ProcessColumnAutoFilter">
                                    <Templates>
                                        <TitlePanel>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Zgjidh kolonat" AutoPostBack="false"
                                                            ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                            <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,gvShtoNivelTvsh)}"
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
}"
                                        RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }" SelectionChanged="function(s, e){OnGridSelectionChanged(e);}"
                                        BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage  Name="Taksa" Text="Taksa">
                      
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                <table id="tblTaksa" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                                <%--<div id="dvlblLloji">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji" runat="server"
                                    ClientInstanceName="lblLloji" Text="Lloji:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcmbLloji">--%>
                                <dx:ASPxComboBox ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji" ShowShadow="False"
                                    SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                    <ClientSideEvents ValueChanged="Value_Changed" />
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                        SetFocusOnError="true" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField ErrorText="*" IsRequired="True" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblKodi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                    ClientInstanceName="lblKodi" Text="Kodi:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtKodi">--%>
                                <dx:ASPxTextBox ID="txtKodi" runat="server" AutoPostBack="false" ClientInstanceName="txtKodi"
                                    Width="100%">
                                    <ClientSideEvents TextChanged="function(s, e) {

}" />
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        SetFocusOnError="true" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere" ValidationExpression="^[\s\S]{0,20}$"></RegularExpression>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <%--<div id="dvlblPershkrimi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi" ID="lblPershkrimi"
                                    runat="server" ClientInstanceName="lblPershkrimi" Text="Pershkrimi:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtPershkrimi">--%>
                                <dx:ASPxMemo ID="txtPershkrimi" runat="server" AutoPostBack="false" ClientInstanceName="txtPershkrimi"
                                    Width="100%" Rows="3">
                                    <ClientSideEvents TextChanged="function(s, e) { } " />
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                        SetFocusOnError="true" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxMemo>
                                <%--</div>--%>
                                <%--<div id="dvlblNjesia">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNjesia" ID="lblNjesia" runat="server"
                                    ClientInstanceName="lblNjesia" Text="Njesia:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcmbNjesia">--%>
                                <dx:ASPxComboBox ID="cmbNjesia" runat="server" ClientInstanceName="cmbNjesia" ShowShadow="False"
                                    SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                        SetFocusOnError="true" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField ErrorText="*" IsRequired="True" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblPerqindja">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPerqindja" ID="lblPerqindja"
                                    runat="server" ClientInstanceName="lblPerqindja" Text="Perqindja/Vlera:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvtxtPerqindja">--%>
                                <dx:ASPxTextBox ID="txtPerqindja" runat="server" ClientInstanceName="txtPerqindja"
                                    Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RegularExpression ErrorText="Lejohen vetem numra!" ValidationExpression="[0-9.,]*"></RegularExpression>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                                <%--</div>--%>
                                <%--<div id="dvlblLlogDebi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlogDebi" ID="lblLlogDebi" runat="server"
                                    ClientInstanceName="lblLlogDebi" Text="Llogaria Debi:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcmbLlogDebi">--%>
                                <dx:ASPxComboBox ID="cmbLlogDebi" 
                                    runat="server" 
                                    AutoPostBack="false" 
                                    ClientInstanceName="cmbLlogDebi"
                                    ShowShadow="False" 
                                    ValueType="System.String" 
                                    SettingsLoadingPanel-ImagePosition="Top"
                                    Width="100%"
                                    OnItemRequestedByValue="cmbLlogDebi_ItemRequestedByValue"
                                    OnItemsRequestedByFilterCondition="cmbLlogDebi_ItemsRequestedByFilterCondition">
                                    <ClientSideEvents ButtonClick=" function(s,e) 
                                                                                            {fitim='debi'
                                                                                            Llogari_Click(); }"
                                        LostFocus="function(s, e) {
                                                                                                               
	                                                        }"
                                        SelectedIndexChanged="function(s, e) {      cmbLlogDebi.SetText(cmbLlogDebi.GetText().split(';')[0]);
	                                                                    }" />
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                        SetFocusOnError="true" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField ErrorText="*" IsRequired="True" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblLlogKredi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlogKredi" ID="lblLlogKredi"
                                    runat="server" ClientInstanceName="lblLlogKredi" Text="Nengrupi:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcmbLlogKredi">--%>
                                <dx:ASPxComboBox ID="cmbLlogKredi" 
                                    runat="server" 
                                    ClientInstanceName="cmbLlogKredi"
                                    ShowShadow="False" 
                                    ValueType="System.String" 
                                    SettingsLoadingPanel-ImagePosition="Top"
                                    Width="100%"
                                    OnItemRequestedByValue="cmbLlogKredi_ItemRequestedByValue"
                                    OnItemsRequestedByFilterCondition="cmbLlogKredi_ItemsRequestedByFilterCondition">
                                    <ClientSideEvents ButtonClick=" function(s,e) {
                                                                            fitim='kredi';
                                                                            Llogari_Click();
                                                                            }"
                                        SelectedIndexChanged="function(s, e) {      cmbLlogKredi.SetText(cmbLlogKredi.GetText().split(';')[0]);
	                                                                    }" />
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                        SetFocusOnError="true" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField ErrorText="*" IsRequired="True" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                <%--</div>--%>
                                <%--<div id="dvlblLlogDog">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlogDog" ID="lblLlogDog" runat="server"
                                    ClientInstanceName="lblLlogDog" Text="Llogari Dogane:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcmbLlogDog">--%>
                                <dx:ASPxComboBox ID="cmbLlogDog" 
                                    runat="server" 
                                    ClientInstanceName="cmbLlogDog"
                                    ShowShadow="False" 
                                    ValueType="System.String" 
                                    SettingsLoadingPanel-ImagePosition="Top"
                                    Width="100%"
                                    OnItemRequestedByValue="cmbLlogDog_ItemRequestedByValue"
                                    OnItemsRequestedByFilterCondition="cmbLlogDog_ItemsRequestedByFilterCondition">
                                    <ClientSideEvents ButtonClick=" function(s,e) {
                                                                            fitim='dog';
                                                                            Llogari_Click();
                                                                            }"
                                        SelectedIndexChanged="function(s, e) {      cmbLlogDog.SetText(cmbLlogDog.GetText().split(';')[0]);
	                                                                    }" />
                                    <DropDownButton>
                                        <Image>
                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        </Image>
                                    </DropDownButton>
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                        SetFocusOnError="true" ValidationGroup="entries">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                        <RequiredField ErrorText="*" IsRequired="True" />
                                    </ValidationSettings>
                                    <DisabledStyle Font-Bold="False">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
								
								<dx:ASPxLabel Wrap="False" AssociatedControlID="cmbTipiPerjashtimit" ID="lblTipiPerjashtimit" runat="server"
                                    ClientInstanceName="lblTipiPerjashtimit" ClientVisible="false">
                                </dx:ASPxLabel>
                                <dx:ASPxComboBox ID="cmbTipiPerjashtimit" 
                                    runat="server" 
                                    ClientInstanceName="cmbTipiPerjashtimit"
                                    SettingsLoadingPanel-ImagePosition="Top"
                                    Width="100%" ClientVisible="false">
                                </dx:ASPxComboBox>
								
                                <%--</div>--%>
                                <%--<div id="dvlblAktiv">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAktiv" ID="lblAktiv" runat="server"
                                    ClientInstanceName="lblAktiv" Text="Aktiv:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcbAktiv">--%>
                                <dx:ASPxCheckBox ID="cbAktiv" runat="server" ClientInstanceName="cbAktiv" Width="100%">
                                </dx:ASPxCheckBox>
                                <%--</div>--%>
                                <%--<div id="dvlblTakseNdermarje">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbTakseNdermarje" ID="lblTakseNdermarje"
                                    runat="server" ClientInstanceName="lblTakseNdermarje" Text="Takse Ndermarje:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcbTakseNdermarje">--%>
                                <dx:ASPxCheckBox ID="cbTakseNdermarje" runat="server" ClientInstanceName="cbTakseNdermarje"
                                    Width="100%">
                                </dx:ASPxCheckBox>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbPerjashtuar" ID="lblPerjashtuar"
                                    runat="server" ClientInstanceName="lblPerjashtuar" Text="Takse Ndermarje:">
                                </dx:ASPxLabel>
                                <%--</div>--%>
                                <%--<div id="dvcbTakseNdermarje">--%>
                                <dx:ASPxCheckBox ID="cbPerjashtuar" runat="server" ClientInstanceName="cbPerjashtuar"
                                    Width="100%">
                                </dx:ASPxCheckBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAplikoTvshNeFleteDoganore" ID="lblAplikoTvshNeFleteDoganore"
                                    runat="server" ClientInstanceName="lblAplikoTvshNeFleteDoganore" Text="Apliko TVSH ne flete doganore:">
                                </dx:ASPxLabel>
                                <dx:ASPxCheckBox ID="cbAplikoTvshNeFleteDoganore" runat="server" ClientInstanceName="cbAplikoTvshNeFleteDoganore"
                                    Width="100%">
                                </dx:ASPxCheckBox>

                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbFurnizimeZero" ID="lblFurnizimeZero"
                                    runat="server" ClientInstanceName="lblFurnizimeZero" Text="Furnizime me 0%:">
                                </dx:ASPxLabel>
                                <dx:ASPxCheckBox ID="cbFurnizimeZero" runat="server" ClientInstanceName="cbFurnizimeZero"
                                    Width="100%">
                                </dx:ASPxCheckBox>

                                   <dx:ASPxLabel Wrap="False" AssociatedControlID="cbShitjePaTvsh" ID="lblShitjePaTvshTaksa"
                                        runat="server" Text="Shitje pa tvsh" ClientInstanceName="lblShitjePaTvshTaksa">
                                    </dx:ASPxLabel>

                                      <dx:ASPxCheckBox ID="cbShitjePaTvshTaksa" runat="server" ClientInstanceName="cbShitjePaTvshTaksa">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>


                                <%--<div id="dvlblAutorizimi">--%>
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAutorizimi" ID="lblAutorizimi"
                                    runat="server" ClientInstanceName="lblAutorizimi" Text="Nivel Autorizimi:">
                                </dx:ASPxLabel>
                                 <div>
                                        <select id="cmbAutorizimi">
                                        </select>
                                        <asp:HiddenField ID="cmbAutorizimiHf" ClientIDMode="Static" runat="server" />
                                               
                                    </div>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="Active_TabChanged" />
            </dxtc:ASPxPageControl >
        </div>
        <asp:UpdatePanel ID="pnlKryesor" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hfAutorizime" runat="server" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
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
