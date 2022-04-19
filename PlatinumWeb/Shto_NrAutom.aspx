<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_NrAutom.aspx.cs" Inherits="PlatinumWeb.Shto_NrAutom" %>

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

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <%--    <script src="js/myMesazh-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
     
    <script src="js/myButtonClickLupa-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/aspx.js/Shto_NrAutom.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/Utils-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Shto_NrAutom.aspx-IMB.2.1.js&v76""
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
            <!-- Pjesa e menuse -->
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
            <!-- Fundi i pjeses se menuse-->
            <div id="dvNrAutomatik" style="display: none">
                <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
                      TabSpacing="3px" Width="100%" ActiveTabIndex="1">
                    <ClientSideEvents ActiveTabChanging="PageControl_ActiveTabChanging" />
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </ContentStyle>
                    <TabPages>
                        <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl3" runat="server">
                                    <table class="renditKontrolle">
                                        <tbody>
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" ClientInstanceName="konfigurimi_Label"
                                                        Style="font-size: large" Text="Modeli:">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33">
                                                    <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                                                        Height="24px" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Style="font-size: medium" Width="100%">
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
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
                                                </td>
                                                <td class="renditKontrolleLabelMeWidth33">
                                                    <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33"></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dx:ASPxGridView ID="ASPxGridView_Numrat" runat="server" ClientInstanceName="ASPxGridView_Numrat"
                                        Width="98%" OnAfterPerformCallback="ASPxGridView_Numrat_AfterPerformCallback"
                                        OnHeaderFilterFillItems="ASPxGridView_Numrat_HeaderFilterFillItems" OnAutoFilterCellEditorInitialize="ASPxGridView_Numrat_AutoFilterCellEditorInitialize"
                                        OnDataBound="ASPxGridView_Numrat_DataBound" OnProcessColumnAutoFilter="ASPxGridView_Numrat_ProcessColumnAutoFilter"
                                        OnCustomCallback="ASPxGridView_Numrat_CustomCallback" OnCustomJSProperties="ASPxGridView_Numrat_CustomJSProperties"
                                        SettingsBehavior-ColumnResizeMode="Control">
                                        <Templates>
                                            <TitlePanel>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Zgjidh kolonat" AutoPostBack="false"
                                                                ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                                <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,ASPxGridView_Numrat)}"
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
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                            FocusedRowChanged="function(s, e) {
      mbush=true;	
}"
                                            BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                                        <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
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
                        <dxtc:TabPage Name="Karakteristikat" Text="Karakteristikat" >
                         
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl1" runat="server">
                                    <table id="tblKarakteristikat" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvkodiLabel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="" ID="kodiLabel" runat="server" ClientInstanceName="kodiLabel" Text="Kodi:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvkodi_TextBox">--%>
                                    <dx:ASPxTextBox ID="kodi_TextBox" runat="server" ClientInstanceName="kodi_TextBox"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" ErrorText="Jepni kodin"
                                            SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvemertimiLabel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="emertimi_TextBox" ID="emertimiLabel" runat="server" ClientInstanceName="emertimiLabel"
                                        Text="Emertimi:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%-- <ClientSideEvents TextChanged="function(s, e) {
	
	 EmertimiNrAutom.SetText(emertimi_TextBox.GetText());
  ProcessTextChanged('EmertimiNrAutom', emertimi_TextBox.GetText()) ;
}" />--%>
                                    <%--<div id="dvemertimi_TextBox">--%>
                                    <dx:ASPxMemo ID="emertimi_TextBox" runat="server" ClientInstanceName="emertimi_TextBox"
                                        Width="100%" Rows="3">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <RequiredField IsRequired="true" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                    <%--<div id="dvkatDok_label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="kategoria_combobox" ID="katDok_label" runat="server" ClientInstanceName="katDok_label"
                                        Text="Kategoria:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvkategoria_combobox">--%>
                                    <dx:ASPxComboBox ID="kategoria_combobox" runat="server" ClientInstanceName="kategoria_combobox"
                                        Width="100%" ValueType="System.String" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){kategoriaIndexChangeNew(s);}" />
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvngaDataLabel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="nga_data_DateEdit" ID="ngaDataLabel" ClientInstanceName="ngaDataLabel" runat="server"
                                        Text="Fillon nga:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvnga_data_DateEdit">--%>
                                    <dx:ASPxDateEdit ID="nga_data_DateEdit" runat="server" ClientInstanceName="nga_data_DateEdit"
                                        Width="100%" ShowShadow="False">
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <%--</div>--%>
                                    <%--<div id="dvderiDataLabel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="deri_me_DateEdit" ID="deriDataLabel" ClientInstanceName="deriDataLabel" runat="server"
                                        Text="Deri me:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvderi_me_DateEdit">--%>
                                    <dx:ASPxDateEdit ID="deri_me_DateEdit" runat="server" ClientInstanceName="deri_me_DateEdit"
                                        Width="100%" ShowShadow="False">
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
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
                                    </dx:ASPxDateEdit>
                                    <%--</div>--%>
                                    <%-- <div id="dvllojPeriudhaLabel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="llojperiudha_combobox" ID="llojPeriudhaLabel" ClientInstanceName="llojPeriudhaLabel" runat="server"
                                        Text="Periudha:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvllojperiudha_combobox">--%>
                                    <dx:ASPxComboBox ID="llojperiudha_combobox" runat="server" ClientInstanceName="llojperiudha_combobox"
                                        Width="100%" ValueType="System.String" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                      <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLajmeroPerpara" ID="lblLajmeroPerpara" ClientInstanceName="lblLajmeroPerpara" runat="server" Text="Lajmero perpara ">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtLajmeroPerpara" NullText ="0" runat="server" ClientInstanceName="txtLajmeroPerpara"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="Duhet te jete numer!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLajmeroPerpara" ID="lblDokTeFundit" ClientInstanceName="lblDokTeFundit" runat="server" Text=" dokumentave te fundit.">
                                    </dx:ASPxLabel>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Alokimi" Text="Alokimi" >
                          
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl2" runat="server">
                                    <table id="tblAlokimi" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvfillonLabel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="fillon_TextBox" ID="fillonLabel" ClientInstanceName="fillonLabel" runat="server" Text="Numri i pare:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvfillon_TextBox">--%>
                                    <dx:ASPxTextBox ID="fillon_TextBox" runat="server" ClientInstanceName="fillon_TextBox"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="Numri i pare duhet te jete numer!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvmbaronLabel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="mbaron_TextBox" ID="mbaronLabel" ClientInstanceName="mbaronLabel" runat="server" Text="Numri i fundit:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvmbaron_TextBox">--%>
                                    <dx:ASPxTextBox ID="mbaron_TextBox" runat="server" ClientInstanceName="mbaron_TextBox"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="Numri i fundit duhet te jete numer!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvhapiLabel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="hapi_TextBox" ID="hapiLabel" ClientInstanceName="hapiLabel" runat="server" Text="Rrit me:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvhapi_TextBox">--%>
                                    <dx:ASPxTextBox ID="hapi_TextBox" runat="server" Width="100%" ClientInstanceName="hapi_TextBox">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="Hapi duhet te jete numer!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox> 
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtIntervali" ID="lblIntervali" ClientInstanceName="lblIntervali" runat="server" Text="Intervali:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvhapi_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtIntervali" runat="server" Width="100%" ClientInstanceName="txtIntervali">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="Intervali duhet te jete numer!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvdrejtimiLabel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="drejtimi_combobox" ID="drejtimiLabel" ClientInstanceName="drejtimiLabel" runat="server"
                                        Text="Drejtimi:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdrejtimi_combobox">--%>
                                    <dx:ASPxComboBox ID="drejtimi_combobox" runat="server" ClientInstanceName="drejtimi_combobox"
                                        Width="100%" ValueType="System.String" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvmajtasLabel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="majtas_TextBox" ID="majtasLabel" ClientInstanceName="majtasLabel" runat="server" Text="Karaktere majtas:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvmajtas_TextBox">--%>
                                    <dx:ASPxTextBox ID="majtas_TextBox" runat="server" ClientInstanceName="majtas_TextBox"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvdjthtasLabel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="djathtas_TextBox" ID="djthtasLabel" ClientInstanceName="djthtasLabel" runat="server"
                                        Text="Karaktere djathtas:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdjathtas_TextBox">--%>
                                    <dx:ASPxTextBox ID="djathtas_TextBox" runat="server" ClientInstanceName="djathtas_TextBox"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvgjatesiaLabel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="gjatesia_TextBox" ID="gjatesiaLabel" ClientInstanceName="gjatesiaLabel" runat="server"
                                        Text="Gjatesia:">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvgjatesia_TextBox">--%>
                                    <dx:ASPxTextBox ID="gjatesia_TextBox" runat="server" ClientInstanceName="gjatesia_TextBox"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="Gjatesia duhet te jete numer!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Numri Fundit" Text="Numri Fundit">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl4" runat="server">
                                    <dx:ASPxGridView ID="grid_NrFunditAutomatik" runat="server" ClientInstanceName="grid_NrFunditAutomatik"
                                        Width="58%" OnProcessColumnAutoFilter="grid_NrFunditAutomatik_ProcessColumnAutoFilter"
                                        OnDataBound="grid_NrFunditAutomatik_DataBound" OnCustomCallback="grid_NrFunditAutomatik_CustomCallback"
                                        SettingsBehavior-ColumnResizeMode="Control" OnHtmlRowCreated="grid_NrFunditAutomatik_HtmlRowCreated"
                                        OnCustomJSProperties="grid_NrFunditAutomatik_CustomJSProperties">
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
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
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="Active_TabChanged" />
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                </dxtc:ASPxPageControl >
                <dx:ASPxLabel ID="pergjigja" runat="server" Text="">
                </dx:ASPxLabel>
            </div>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfVlerat" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
