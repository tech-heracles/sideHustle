<%@ Page Language="C#" Title="" AutoEventWireup="true" CodeBehind="Shto_KPF.aspx.cs"
    Inherits="PlatinumWeb.Shto_KPF" %>

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
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/aspx.js/Shto_KPF.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <div style="width: 100%">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
            </dx:ASPxGlobalEvents>
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="pnlMenu" runat="server">
                <ContentTemplate>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientInstanceName="popFshi"
                        CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes"
                        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        ShowHeader="true" Width="300px" Enabled="True" >
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                                <dxp:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" Width="200px">
                                    <PanelCollection>
                                        <dxp:PanelContent>
                                            <dx:ASPxLabel ID="lblMsgbox" runat="server" Text="Jeni i sigurt?">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOk" runat="server" Text="Ok" CausesValidation="False" OnClick="ButtonOk_Click2">
                                                                <ClientSideEvents Click="function(s, e) {   
	popFshi.Hide();
    Utils.shfaqLoadingGif();;
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancel" runat="server" Text="Anullo">
                                                                <ClientSideEvents Click="function(s, e) {
		popFshi.Hide();
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxp:ASPxPanel >
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl >
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="dvKPF" style="display: none">
                <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
                      TabSpacing="3px" Width="100%" ActiveTabIndex="3" Height="520px">
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </ContentStyle>
                    <TabPages>
                        <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl1" runat="server">
                                    <table class="renditKontrolle">
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                    runat="server" Text="Modeli:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33">
                                                <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                                                    ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top">
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
                                                <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi"
                                                    ClientInstanceName="lblKonfigurimi">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33"></td>
                                        </tr>
                                    </table>
                                    <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl2" runat="server"   TabSpacing="3px"
                                        Width="100%" ActiveTabIndex="2" ClientInstanceName="ASPxPageControl2">
                                        <ContentStyle>
                                            <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                                        </ContentStyle>
                                        <TabPages>
                                            <dxtc:TabPage Name="Struktura 1" Text="Struktura 1">
                                                <ContentCollection>
                                                    <dxw:ContentControl ID="ContentControl2" runat="server">
                                                        <asp:HiddenField ID="hfTab" runat="server" />
                                                        <dx:ASPxGridView ID="grid_ListKPFsh1" ClientInstanceName="grid1" runat="server"
                                                            Width="100%" OnDataBound="grid_ListKPFsh1_DataBound" OnAfterPerformCallback="grid_ListKPFsh1_AfterPerformCallback"
                                                            OnHeaderFilterFillItems="grid_ListKPFsh1_HeaderFilterFillItems" OnAutoFilterCellEditorInitialize="grid_ListKPFsh1_AutoFilterCellEditorInitialize1"
                                                            OnProcessColumnAutoFilter="grid_ListKPFsh1_ProcessColumnAutoFilter" OnCustomCallback="grid_ListKPFsh1_CustomCallback"
                                                            OnCustomJSProperties="grid_ListKPFsh1_CustomJSProperties">
                                                            <Templates>
                                                                <TitlePanel>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Zgjidh kolonat" AutoPostBack="false"
                                                                                    ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                                                    <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,grid1)}"
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
                                                            <ClientSideEvents RowDblClick="function(s, e) {
            OnGridDoubleClick(e.visibleIndex);    kaloTab=true; 	
}"
                                                                FocusedRowChanged="function(s, e) {
            mbush=true;	
}"
                                                                SelectionChanged="function(s, e){OnGridSelectionChanged(e);}" BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" EndCallback="function (s,e){PageControl.AdjustSize();}" />
                                                            <StylesEditors>
                                                                <ProgressBar Height="25px">
                                                                </ProgressBar>
                                                            </StylesEditors>
                                                        </dx:ASPxGridView>
                                                    </dxw:ContentControl>
                                                </ContentCollection>
                                            </dxtc:TabPage>
                                            <dxtc:TabPage Name="Struktura 2" Text="Struktura 2">
                                                <ContentCollection>
                                                    <dxw:ContentControl ID="ContentControl3" runat="server">
                                                        <dx:ASPxGridView ID="grid_ListKPFsh2" ClientInstanceName="grid2" runat="server"
                                                            Width="100%" OnDataBound="grid_ListKPFsh2_DataBound" OnAfterPerformCallback="grid_ListKPFsh1_AfterPerformCallback"
                                                            OnHeaderFilterFillItems="grid_ListKPFsh1_HeaderFilterFillItems" OnAutoFilterCellEditorInitialize="grid_ListKPFsh1_AutoFilterCellEditorInitialize1"
                                                            OnProcessColumnAutoFilter="grid_ListKPFsh2_ProcessColumnAutoFilter" OnCustomCallback="grid_ListKPFsh2_CustomCallback"
                                                            OnCustomJSProperties="grid_ListKPFsh2_CustomJSProperties">
                                                            <Styles>
                                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                                </Header>
                                                            </Styles>
                                                            <ClientSideEvents RowDblClick="function(s, e) {
            OnGridDoubleClick2(e.visibleIndex);    	kaloTab=true; 
}"
                                                                FocusedRowChanged="function(s, e) {
            mbush=true;	
}"
                                                                SelectionChanged="function(s, e){OnGridSelectionChanged2(e);}" BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" EndCallback="function (s,e){PageControl.AdjustSize();}" />
                                                            <StylesEditors>
                                                                <ProgressBar Height="25px">
                                                                </ProgressBar>
                                                            </StylesEditors>
                                                        </dx:ASPxGridView>
                                                    </dxw:ContentControl>
                                                </ContentCollection>
                                            </dxtc:TabPage>
                                            <dxtc:TabPage Name="Struktura 3" Text="Struktura 3">
                                                <ContentCollection>
                                                    <dxw:ContentControl ID="ContentControl4" runat="server">
                                                        <dx:ASPxGridView ID="grid_ListKPFsh3" ClientInstanceName="grid3" runat="server"
                                                            Width="100%" OnDataBound="grid_ListKPFsh3_DataBound" OnAfterPerformCallback="grid_ListKPFsh1_AfterPerformCallback"
                                                            OnHeaderFilterFillItems="grid_ListKPFsh1_HeaderFilterFillItems" OnAutoFilterCellEditorInitialize="grid_ListKPFsh1_AutoFilterCellEditorInitialize1"
                                                            OnProcessColumnAutoFilter="grid_ListKPFsh3_ProcessColumnAutoFilter" OnCustomCallback="grid_ListKPFsh3_CustomCallback"
                                                            OnCustomJSProperties="grid_ListKPFsh3_CustomJSProperties">
                                                            <Styles>
                                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                                </Header>
                                                            </Styles>
                                                            <SettingsPager PageSize="15">
                                                            </SettingsPager>
                                                            <ClientSideEvents RowDblClick="function(s, e) {
            OnGridDoubleClick3(e.visibleIndex);    	kaloTab=true; 
}"
                                                                FocusedRowChanged="function(s, e) {
            mbush=true;	
}"
                                                                SelectionChanged="function(s, e){OnGridSelectionChanged3(e);}" BeginCallback="function(s, e) {
	BeginCallback(s,e);
}"  EndCallback="function (s,e){PageControl.AdjustSize();}"/>
                                                            <StylesEditors>
                                                                <ProgressBar Height="25px">
                                                                </ProgressBar>
                                                            </StylesEditors>
                                                        </dx:ASPxGridView>
                                                    </dxw:ContentControl>
                                                </ContentCollection>
                                            </dxtc:TabPage>
                                        </TabPages>
                                        <ClientSideEvents ActiveTabChanged="function(s, e) {                   
                    $('#HiddenField4')[0].value=e.tab.index;
                    pastrofusha();
                     }" />
                                    </dxtc:ASPxPageControl >
                                    <br />
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Llogari Standarte" Text="Llogari Standarte" >
                         
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl5" runat="server">
                                    <table id="tblLlogaria" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblPrindi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPrindi" ID="lblPrindi" runat="server"
                                        Text="Prindi:" ClientInstanceName="lblPrindi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbPrindi">--%>
                                    <dx:ASPxComboBox ID="cmbPrindi" runat="server" ClientInstanceName="cmbPrindi" ShowShadow="False"
                                        EnableCallbackMode="True" OnItemRequestedByValue="cmbPrindi_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbPrindi_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="Selected_IndexChanged"
                                            ButtonClick="function(s, e) {KPF1_Click(); }" BeginCallback="function(s,e){ tabi();}" />
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
                                    <%--<div id="dvlblKodi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtKodi">--%>
                                    <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodi" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true" ErrorTextPosition="Right" RegularExpression-ValidationExpression="^[\s\S]{0,20}$"
                                            RegularExpression-ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere" ValidationExpression="^[\s\S]{0,20}$"></RegularExpression>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblEmertimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi" ID="lblEmertimi" runat="server"
                                        Text="Emertimi:" ClientInstanceName="lblEmertimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtEmertimi">--%>
                                    <dx:ASPxMemo ID="txtEmertimi" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtEmertimi" Rows="3">
                                        <ClientSideEvents TextChanged="TextChanged_txtEmertimi" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                    <%--<div id="dvlblNiveli">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNiveleKPF" ID="lblNiveli" runat="server"
                                        Text="Niveli:" ClientInstanceName="lblNiveli">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbNiveleKPF">--%>
                                    <dx:ASPxComboBox ID="cmbNiveleKPF" runat="server" ClientInstanceName="cmbNiveleKPF"
                                        ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="true">
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
                                        runat="server" Text="Autorizimi:" ClientInstanceName="lblAutorizimi">
                                    </dx:ASPxLabel>
                                    <div>
                                        <select id="cmbAutorizimi">
                                        </select>
                                        <asp:HiddenField ID="cmbAutorizimiHf" ClientIDMode="Static" runat="server" />
                                               
                                    </div>
                                    <%--</div>--%>
                                    <%--<div id="dvlblInaktiv">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbInaktiv" ID="lblInaktiv" runat="server"
                                        Text="Inaktiv:" ClientInstanceName="lblInaktiv">
                                    </dx:ASPxLabel>


                                    <%--</div>--%>
                                    <%--<div id="dvcbInaktiv">--%>
                                    <dx:ASPxCheckBox ID="cbInaktiv" runat="server" ClientInstanceName="cbInaktiv" Width="100%">
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

                                    <%--</div>--%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Shenime" Text="Shenime" >
                        
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl6" runat="server">
                                    <table id="tblShenime" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblKodi2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="kodi_TextBox" ID="lblKodi2" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvkodi_TextBox">--%>
                                    <dx:ASPxTextBox ID="kodi_TextBox" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="kodi_TextBox">
                                        <ClientSideEvents TextChanged="TextChanged_kodi_TextBox" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblEmertimi2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="emertimi_TextBox" ID="lblEmertimi2"
                                        runat="server" Text="Emertimi:" ClientInstanceName="lblEmertimi2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemertimi_TextBox">--%>
                                    <dx:ASPxMemo ID="emertimi_TextBox" runat="server" Width="100%" AutoPostBack="false" Rows="3"
                                        ClientInstanceName="emertimi_TextBox">
                                        <ClientSideEvents TextChanged="TextChanged_emertimi_TextBox" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                    <%--<div id="dvlblShenime">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="lblShenime" runat="server"
                                        Text="Shenime:" ClientInstanceName="lblShenime">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtShenime">--%>
                                    <dx:ASPxMemo ID="txtShenime" runat="server" Rows="3" Width="100%" ClientInstanceName="txtShenime">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                  
                                    <%--</div>--%>
								   <%--<div id="dvlblEmerFr">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerLlogarieFr" ID="lblEmerFr" runat="server"
                                        Text="Emertimi Frengjisht:" ClientInstanceName="lblEmerFr">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtShenime">--%>
                                    <dx:ASPxMemo ID="txtEmerLlogarieFr" runat="server" Rows="3" Width="100%" ClientInstanceName="txtEmerLlogarieFr">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Llogarite" Text="Llogarite" >
                          
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl7" runat="server">
                                    <table class="renditKontrolle">
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodiLlog" ID="lblKodiLlog" runat="server"
                                                    Text="Kodi:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth25">
                                                <dx:ASPxTextBox ID="txtKodiLlog" runat="server" Width="100%" AutoPostBack="false"
                                                    ClientInstanceName="txtKodiLlog">
                                                    <ClientSideEvents TextChanged="TextChanged_txtKodiLlog" />
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dx:ASPxTextBox>
                                            </td>

                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimiLlog" ID="lblEmertimiLlog"
                                                    runat="server" Text="Emertimi:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth25">
                                                <dx:ASPxMemo ID="txtEmertimiLlog" runat="server" Width="100%" AutoPostBack="false"
                                                    ClientInstanceName="txtEmertimiLlog" Rows="3">
                                                    <ClientSideEvents TextChanged="TextChanged_txtEmertimiLlog" />
                                                    <ValidationSettings>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dx:ASPxMemo>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth25"></td>
                                            <td class="renditKontrolleCellMeWidth25"></td>
                                        </tr>
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="listBoxLlogarite" ID="ASPxLabel13"
                                                    runat="server" Text="Llogarite:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth50" colspan="3">
                                                <dx:ASPxListBox ID="listBoxLlogarite" runat="server" Height="323px" OnDataBound="listBoxLlogarite_DataBound"
                                                    Width="100%" ClientInstanceName="listBoxLlogarite" OnCallback="listBoxLlogarite_Callback">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ErrorText="Error has occurred">
                                                        <ErrorImage />
                                                    </ValidationSettings>
                                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dx:ASPxListBox>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth50"></td>

                                        </tr>
                                    </table>
                                    <br />
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <%-- <dxtc:TabPage Text="Buxheti" TabStyle-Height="150px">
                            <TabStyle Height="150px">
                            </TabStyle>
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl7" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Nr:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txtNrKPFBuxheti" runat="server" Width="170px" AutoPostBack="false"
                                                    ClientInstanceName="txtNrKPFBuxheti"  
                                                     >
                                                    <ClientSideEvents TextChanged="function(s, e) {
	txtKodi.SetText(txtNrKPFBuxheti.GetText());
	kodi_TextBox.SetText(txtNrKPFBuxheti.GetText());
	 if(document.getElementById('HiddenField4').value==0)	                    
	                   { KodiKPF1.SetText(txtKodi.GetText());
  ProcessTextCahnged('KodiKPF', txtKodi.GetText()) ;
	                  }
	                     else if (document.getElementById('HiddenField4').value==1)
	                    { KodiKPF2.SetText(txtKodi.GetText());
  ProcessTextCahnged2('KodiKPF', txtKodi.GetText()) ;
	                    }
	                      else if (document.getElementById('HiddenField4').value==2)
	{  KodiKPF3.SetText(txtKodi.GetText());
  ProcessTextCahnged3('KodiKPF', txtKodi.GetText()) ;}
}" />
                                                    <ValidationSettings>
                                                        
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td nowrap="nowrap">
                                                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="Emer Llogarie:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txtEmerKPFBuxheti" runat="server" Width="170px" AutoPostBack="false"
                                                    ClientInstanceName="txtEmerKPFBuxheti"  
                                                     >
                                                    <ClientSideEvents TextChanged="function(s, e) {
	                                    txtEmertimi.SetText(txtEmerKPFBuxheti.GetText());
	                                    emertimi_TextBox.SetText(txtEmerKPFBuxheti.GetText());
                                       if(document.getElementById('HiddenField4').value==0)	                    
	                   { EmertimiKPF1.SetText(txtEmertimi.GetText());
  ProcessTextCahnged('EmertimiKPF', txtEmertimi.GetText()) ;
	                  }
	                     else if (document.getElementById('HiddenField4').value==1)
	                    { EmertimiKPF2.SetText(txtEmertimi.GetText());
  ProcessTextCahnged2('EmertimiKPF', txtEmertimi.GetText()) ;
	                    }
	                      else if (document.getElementById('HiddenField4').value==2)
	{  EmertimiKPF3.SetText(txtEmertimi.GetText());
  ProcessTextCahnged3('EmertimiKPF', txtEmertimi.GetText()) ;}      }" />
                                                    <ValidationSettings>
                                                        
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="gvBuxheti" runat="server" ClientInstanceName="gvBuxheti"
                                                    OnAfterPerformCallback="gvBuxheti_AfterPerformCallback"  
                                                      OnHtmlRowCreated="gvBuxheti_HtmlRowCreated">
                                                    <Images ImageFolder="~/App_Themes/BlackGlass/{0}/">
                                                        <FilterRowButton Height="13px" Width="13px" />
                                                    </Images>
                                                    <Styles    >
                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                        </Header>
                                                    </Styles>
                                                    <StylesEditors>
                                                        <ProgressBar Height="25px">
                                                        </ProgressBar>
                                                    </StylesEditors>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <dx:ASPxTextBox ID="txtLlog" runat="server" Text="" Visible="true" ClientInstanceName="txtLlog"
                                        Width="0%" EnableTheming="False" BackColor="White" Border-BorderColor="White"
                                        ForeColor="White">
                                        <Border BorderColor="White" />
                                    </dx:ASPxTextBox>
                                    <br />
                                        <table class="butonat">
                                            <tr>
                                                <td style="width: 70%">
                                                </td>
                                                <td align="right" style="width: 10%">
                                                    <dx:ASPxButton ID="btnRuajBuxhete" runat="server" Text="Ruaj" OnClick="ruaj_Button_Click"
                                                            ValidationGroup="entries"
                                                        Width="100%">
                                                        <ClientSideEvents Click="function(s, e) {
	valido(s,e); 
	pastro();
}" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="right" style="width: 10%">
                                                    <dx:ASPxButton ID="btnPatroBuxhete" runat="server" Text="Pastro" OnClick="pastro_Button_Click"
                                                            Width="100%"
                                                        CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) {
	pastro();
}" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="right" style="width: 10%">
                                                    <dx:ASPxButton ID="btnAnulloBuxhete" runat="server" Text="Anullo"  
                                                        PostBackUrl="~/KPF.aspx"   Width="100%" CausesValidation="False">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>--%>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanging="function(s, e) { tabsActiveTabChanging(s, e); }" />
                    <%--  if ( e.tab.index==3)// kur te behet qe te ndodh eventi vetem kur shkon ne tabin e trete
                         {  if(lista)
                          { listBoxLlogarite.PerformCallback(indexModifiko);
                         lista=false;
                             }
                         }--%>
                </dxtc:ASPxPageControl >
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="HiddenField3" runat="server" />
                    <asp:HiddenField ID="HiddenField4" runat="server" />
                    <asp:HiddenField ID="hfBuxheti1" runat="server" />
                    <asp:HiddenField ID="hfBuxheti2" runat="server" />
                    <asp:HiddenField ID="hfAutorizime1" runat="server" />
                    <asp:HiddenField ID="hfAutorizime2" runat="server" />
                    <asp:HiddenField ID="hfAutorizime3" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="hfLupaKpf1" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                    <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
                    </dx:ASPxHiddenField>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                                CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
