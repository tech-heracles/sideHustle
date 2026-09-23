<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_KlientFurnitor.aspx.cs" Inherits="PlatinumWeb.Shto_KlientFurnitor" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Src="~/ucFushatShtese.ascx" TagPrefix="uc1" TagName="ucFushatShtese" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <!-- DevExtreme themes -->
    <link rel="stylesheet" type="text/css" href="Content/dx.common.css" />
    <link rel="stylesheet" type="text/css" href="Content/dx.generic.alphaweb-compact.css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />

    <!-- A DevExtreme library -->
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/Scripts/dx.viz-web.js;~/js/localization/DevExtreme.Perkthime.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/myDxDataGrid.js;~/js/aspx.js/Shto_KlientFurnitor.aspx-IMB.2.1.js&v76"
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
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                    OnItemClick="ASPxMenu1_ItemClick">
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                    <ClientSideEvents ItemClick="function(s, e) { menu_click(s,e); }"
                                        Init="function(s) {s.SetClientVisible(true);}" />
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
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
                                                                <ClientSideEvents Click="function(s, e) { popFshi.Hide(); Utils.shfaqLoadingGif(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                                <ClientSideEvents Click="function(s, e) { popFshi.Hide(); e.processOnServer=false; }" />
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
            <div id="dvKlientFurnitor" style="display: none">
                <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
                    TabSpacing="3px" Width="100%" ActiveTabIndex="7" ClientIDMode="AutoID"
                    Height="600px">
                    <ContentStyle>
                        <border bordercolor="#AECAF0" borderstyle="Solid" borderwidth="1px" />
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
                                                        runat="server" ClientIDMode="AutoID" Text="Modeli:">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33">
                                                    <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                        ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                                        Width="100%" AnimationType="None">
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
                                                <td class="renditKontrolleLabelMeWidth33"></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dx:ASPxGridView ID="ASPxGridView_KF" runat="server" ClientInstanceName="ASPxGridView_KF"
                                        Width="98%" OnAfterPerformCallback="ASPxGridView_KF_AfterPerformCallback" OnCustomCallback="ASPxGridView_KF_CustomCallback"
                                        OnCustomJSProperties="ASPxGridView_KF_CustomJSProperties" OnDataBound="ASPxGridView_KF_DataBound"
                                        OnAutoFilterCellEditorInitialize="ASPxGridView_KF_AutoFilterCellEditorInitialize"
                                        OnProcessColumnAutoFilter="ASPxGridView_KF_ProcessColumnAutoFilter" OnHeaderFilterFillItems="ASPxGridView_KF_HeaderFilterFillItems">
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {onNdryshimFokusi();}"
                                            RowDblClick="function(s, e) {kaloTab=true; OnGridDoubleClick(e.visibleIndex);}"
                                            BeginCallback="function(s, e) {BeginCallback(s,e);}"
                                            EndCallback="function (s,e){PageControl.AdjustSize();}" ColumnSorting="ASPxGridView_KF_OnColumnSorting" />
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
                                    <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView_KF"
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
                                    <br />
                                    <%--<div id="dvlblPergjithshem">--%>
                                    <dx:ASPxLabel Wrap="False" ID="lblPergjithshem" runat="server" Text="Pergjithshem"
                                        ClientInstanceName="lblPergjithshem" Font-Underline="True" Height="30px">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvlblKodi"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%-- <div id="dvtxtKodi">--%>
                                    <dx:ASPxTextBox ID="txtKodi" runat="server" AutoPostBack="false" ClientInstanceName="txtKodi"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodi"
                                            Init="function(s, e) { s.Focus(); }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="True" RequiredField-IsRequired="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 100 karaktere" ValidationExpression="^[\s\S]{0,100}$"></RegularExpression>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%-- </div>--%>
                                    <%--<div id="dvlblEmertimi"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi" ID="lblEmertimi" runat="server"
                                        Text="Emertimi:" ClientInstanceName="lblEmertimi">
                                    </dx:ASPxLabel>
                                    <%--  </div>--%>
                                    <%--<div id="dvtxtEmertimi"> --%>
                                    <dx:ASPxMemo ID="txtEmertimi" runat="server" AutoPostBack="false" ClientInstanceName="txtEmertimi"
                                        Width="100%" Rows="3">
                                        <ClientSideEvents TextChanged="function(s, e) {txtEmertimiTextChanged(s,e);}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="True" RequiredField-IsRequired="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div> --%>
                                    <%--<div id="dvlblNrLlog"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNr" ID="lblNrLlog" runat="server"
                                        Text="Nr. Llogari:" ClientInstanceName="lblNrLlog">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtNr"> --%>
                                    <dx:ASPxComboBox ID="txtNr" runat="server" AutoPostBack="false" ClientInstanceName="txtNr"
                                        OnItemRequestedByValue="txtNr_ItemRequestedByValue" OnItemsRequestedByFilterCondition="txtNr_ItemsRequestedByFilterCondition" SettingsLoadingPanel-ImagePosition="Top"
                                        ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s,e) {ButtonClickedLlogaria(s); }"
                                            LostFocus="LostFocus_txtNr"
                                            Init="function(s, e) { s.Focus(); }" />
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
                                    <%--</div> --%>
                                    <%--<div id="dvlblLloji"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji" runat="server"
                                        Text="Lloji:" ClientInstanceName="lblLloji">
                                    </dx:ASPxLabel>
                                    <%-- </div>--%>
                                    <%--<div id="dvcmbLloji"> --%>
                                    <dx:ASPxComboBox ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji" ShowShadow="False"
                                        OnSelectedIndexChanged="cmbLloji_SelectedIndexChanged" SettingsLoadingPanel-ImagePosition="Top"
                                        Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblAgjentShitje"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAgjentShitjesh" ID="lblAgjentShitje"
                                        runat="server" Text="Agjent Shitjesh:" ClientInstanceName="lblAgjentShitje">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcmbAgjentShitjesh"> --%>
                                    <dx:ASPxComboBox ID="cmbAgjentShitjesh" runat="server" ClientInstanceName="cmbAgjentShitjesh"
                                        ShowShadow="False" EnableCallbackMode="false"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents LostFocus="function(s,e){ZbrazPerqindjeAgjenti('txtPerqindjeAgjent')}" ValueChanged="function(s,e){kontrollagjent('1')}" SelectedIndexChanged="function(s,e){kontrollagjent('1')}" ButtonClick="function(s,e){AgjentShitjesh_Click('1')}" TextChanged="function(s,e){ZbrazPerqindjeAgjenti('txtPerqindjeAgjent')}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbTipiId" ID="lblTipiId"
                                        runat="server" ClientInstanceName="lblTipiId" ClientVisible="false">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcmbAgjentShitjesh"> --%>
                                    <dx:ASPxComboBox ID="cmbTipiId" runat="server" ClientInstanceName="cmbTipiId"
                                        ShowShadow="False" EnableCallbackMode="false"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%" ClientVisible="false">
<%--                                        <ClientSideEvents LostFocus="function(s,e){ZbrazPerqindjeAgjenti('txtPerqindjeAgjent')}" ValueChanged="function(s,e){kontrollagjent('1')}" SelectedIndexChanged="function(s,e){kontrollagjent('1')}" ButtonClick="function(s,e){AgjentShitjesh_Click('1')}" TextChanged="function(s,e){ZbrazPerqindjeAgjenti('txtPerqindjeAgjent')}" />--%>
                                        <DropDownButton>
                                            <%--<Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>--%>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDatelindjaKF" ID="lblDatelindjaKF"
                                        runat="server" Text="Datelindja:" ClientInstanceName="lblDatelindjaKF">
                                    </dx:ASPxLabel>
                                    <dx:ASPxDateEdit ID="dteDatelindjaKF" runat="server" ClientInstanceName="dteDatelindjaKF"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKlientiKryesor" ID="lblKlientiKryesor"
                                        runat="server" Text="Klienti Kryesor:" ClientInstanceName="lblKlientiKryesor">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcmbAgjentShitjesh"> --%>
                                    <dx:ASPxComboBox ID="btneKlientiKryesor" runat="server" ClientInstanceName="btneKlientiKryesor"
                                        ShowShadow="False" OnItemRequestedByValue="btneKlientiKryesor_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneKlientiKryesor_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s,e){kfKryesor_Click('1')}" />
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

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="lblShenime" runat="server" Text="Shenime:" ClientInstanceName="lblShenime">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPershkrimi">--%>
                                    <dx:ASPxMemo ID="txtShenime" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtShenime" Rows="3">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </dx:ASPxMemo>
                                   
                                    <%--</div> --%>
                                    <%--  <div id="dvlblPerqindjeAgjent">--%>
                                    <dx:ASPxLabel Wrap="False" ID="lblPerqindjeAgjent" AssociatedControlID="txtPerqindjeAgjent"
                                        runat="server" Text="Perqindje Agjent:" ClientInstanceName="lblPerqindjeAgjent">
                                    </dx:ASPxLabel>
                                    <%-- </div>
									<div id="dvtxtPerqindjeAgjent">--%>
                                    <dx:ASPxTextBox ID="txtPerqindjeAgjent" runat="server" Width="100%" ClientInstanceName="txtPerqindjeAgjent">
                                        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                            LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" TextChanged="function(s,e){changedPerqindjeAgjent('1');}" />
                                        <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <RegularExpression ValidationExpression="^[-+]?[0-9,.]*"></RegularExpression>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" ID="lblAgjenti2" AssociatedControlID="btnAgjenti2" runat="server"
                                        Text="Agjenti 2: " ClientInstanceName="lblAgjenti2">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="btnAgjenti2" Width="100%" runat="server" ClientInstanceName="btnAgjenti2"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" EnableCallbackMode="false">
                                        <ClientSideEvents LostFocus="function(s,e){ZbrazPerqindjeAgjenti('txtPerqindjeAgjent2')}" SelectedIndexChanged="function(s,e){kontrollagjent('2')}" ValueChanged="function(s,e){kontrollagjent('2')}" ButtonClick="function(s,e){AgjentShitjesh_Click('2');}" TextChanged="function(s,e){ZbrazPerqindjeAgjenti('txtPerqindjeAgjent2')}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblPerqindjeAgjent2" AssociatedControlID="txtPerqindjeAgjent2"
                                        runat="server" Text="Perqindje Agjenti 2: " ClientInstanceName="lblPerqindjeAgjent2">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtPerqindjeAgjent2" runat="server" Width="100%" ClientInstanceName="txtPerqindjeAgjent2">
                                        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" KeyUp="function(s,e){changedPerqindjeAgjent('2');}" />
                                        <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <RegularExpression ValidationExpression="^[-+]?[0-9,.]*"></RegularExpression>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblAgjenti3" AssociatedControlID="btnAgjenti3" runat="server"
                                        Text="Agjenti 3: " ClientInstanceName="lblAgjenti3">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="btnAgjenti3" Width="100%" runat="server" ClientInstanceName="btnAgjenti3"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" EnableCallbackMode="false">
                                        <ClientSideEvents LostFocus="function(s,e){ZbrazPerqindjeAgjenti('txtPerqindjeAgjent3')}" SelectedIndexChanged="function(s,e){kontrollagjent('3')}"
                                            ValueChanged="function(s,e){kontrollagjent('3')}" ButtonClick="function(s,e){AgjentShitjesh_Click('3');}" 
                                            TextChanged="function(s,e){ZbrazPerqindjeAgjenti('txtPerqindjeAgjent3')}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblPerqindjeAgjent3" AssociatedControlID="txtPerqindjeAgjent3"
                                        runat="server" Text="Perqindje Agjenti 3: " ClientInstanceName="lblPerqindjeAgjent3">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtPerqindjeAgjent3" runat="server" Width="100%" ClientInstanceName="txtPerqindjeAgjent3">
                                        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" 
                                            LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" KeyUp="function(s,e){changedPerqindjeAgjent('3');}" />
                                        <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <RegularExpression ValidationExpression="^[-+]?[0-9,.]*"></RegularExpression>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAutorizimi" ID="lblAutorizimi"
                                        runat="server" Text="Nivel Autorizimi:" ClientInstanceName="lblAutorizimi">
                                    </dx:ASPxLabel>

                                    <div>
                                        <select id="cmbAutorizimi">
                                        </select>
                                        <asp:HiddenField ID="cmbAutorizimiHf" ClientIDMode="Static" runat="server" />

                                    </div>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNipt" ID="lblNipt" runat="server"
                                        Text="Nipt:" ClientInstanceName="lblNipt">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtNipt"> --%>
                                    <dx:ASPxTextBox ID="txtNipt" runat="server" ClientInstanceName="txtNipt" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <br />
                                    <%-- </div>--%>
                                    <%--<div id="dvlblTitulli"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbTitulli" ID="lblTitulli" runat="server"
                                        Text="Titulli:" ClientInstanceName="lblTitulli">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcmbTitulli"> --%>
                                    <dx:ASPxComboBox ID="cmbTitulli" runat="server" ClientInstanceName="cmbTitulli"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblLicenca"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLicenca" ID="lblLicenca" runat="server"
                                        Text="Licenca:" ClientInstanceName="lblLicenca">
                                    </dx:ASPxLabel>
                                    <%-- </div>--%>
                                    <%--<div id="dvtxtLicenca"> --%>
                                    <dx:ASPxTextBox ID="txtLicenca" runat="server" ClientInstanceName="txtLicenca" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <br />
                                    <%--</div> --%>
                                    <%--<div id="dvlblAktiv"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAktiv" ID="lblAktiv" runat="server"
                                        Text="Aktiv:" ClientInstanceName="lblAktiv">
                                    </dx:ASPxLabel>
                                    <%-- </div> --%>
                                    <%--<div id="dvcbAktiv"> --%>
                                    <dx:ASPxCheckBox ID="cbAktiv" runat="server" ClientInstanceName="cbAktiv" Checked="true"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbSpecifik" ID="lblSpecifik" runat="server"
                                        Text="Aktiv:" ClientInstanceName="lblSpecifik">
                                    </dx:ASPxLabel>
                                    <%-- </div> --%>
                                    <%--<div id="dvcbAktiv"> --%>
                                    <dx:ASPxCheckBox ID="cbSpecifik" runat="server" ClientInstanceName="cbSpecifik" Checked="true"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbFermer" ID="lblFermer" runat="server"
                                        Text="fermer:" ClientInstanceName="lblFermer">
                                    </dx:ASPxLabel>
                                    <%-- </div> --%>
                                    <%--<div id="dvcbAktiv"> --%>
                                    <dx:ASPxCheckBox ID="cbFermer" runat="server" ClientInstanceName="cbFermer" Checked="true"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbAutongarkese" ID="lblAutongarkese" runat="server"
                                        Text="Klient/Furnitor per autongarkese:" ClientInstanceName="lblAutongarkese">
                                    </dx:ASPxLabel>
                                    <%-- </div> --%>
                                    <%--<div id="dvcbAktiv"> --%>
                                    <dx:ASPxCheckBox ID="cbAutongarkese" runat="server" ClientInstanceName="cbAutongarkese" Checked="true"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbShitjePaTvsh" ID="lblShitjePaTvsh" runat="server"
                                        Text="Shitje pa tvsh:" ClientInstanceName="lblShitjePaTvsh">
                                    </dx:ASPxLabel>
                                    <%-- </div> --%>
                                    <%--<div id="dvcbAktiv"> --%>
                                    <dx:ASPxCheckBox ID="cbShitjePaTvsh" runat="server" ClientInstanceName="cbShitjePaTvsh" Checked="true"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <%--</div> --%>
                                    <%-- <div id="dvlblAktiviteti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAktiviteti" ID="lblAktiviteti"
                                        runat="server" Text="Aktiviteti:" ClientInstanceName="lblAktiviteti">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtAktiviteti"> --%>
                                    <dx:ASPxTextBox ID="txtAktiviteti" runat="server" ClientInstanceName="txtAktiviteti"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%-- </div>--%>
                                    <%--<div id="dvlblEmerKerkimi"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerKerkimi" ID="lblEmerKerkimi"
                                        runat="server" Text="Emer Kerkimi:" ClientInstanceName="lblEmerKerkimi">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%-- <div id="dvtxtEmerKerkimi">--%>
                                    <dx:ASPxTextBox ID="txtEmerKerkimi" runat="server" ClientInstanceName="txtEmerKerkimi" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <br />

                                    <%--</div> --%>
                                    <%--<div id="dvlblKontakti"> --%>
                                    <dx:ASPxLabel ID="lblKontakti" runat="server" Text="Kontakti" ClientInstanceName="lblKontakti" Font-Underline="True" Height="30px">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvlblLlojAdrese"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojAdrese" ID="lblLlojAdrese"
                                        runat="server" Text="Lloj adrese:" ClientInstanceName="lblLlojAdrese">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcmbLlojAdrese"> --%>
                                    <dx:ASPxComboBox ID="cmbLlojAdrese" runat="server" ClientInstanceName="cmbLlojAdrese"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { adresat(); }" />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblTel"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTel" ID="lblTel" runat="server"
                                        Text="Tel:" ClientInstanceName="lblTel">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtTel"> --%>
                                    <dx:ASPxTextBox ID="txtTel" runat="server" ClientInstanceName="txtTel" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="false">
                                            <RegularExpression ValidationExpression="^\s*\+?\s*([0-9 \(\)][\s-]*){9,}$" ErrorText="Formati nuk eshte i sakte!" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblAdresa"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAdresa" ID="lblAdresa" runat="server"
                                        Text="Adresa:" ClientInstanceName="lblAdresa">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtAdresa"> --%>
                                    <dx:ASPxMemo ID="txtAdresa" runat="server" ClientInstanceName="txtAdresa" Width="100%" Rows="3">
                                        <ClientSideEvents TextChanged="function(s, e) {	ndryshoVlereAdreseKodiPostar(); }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div> --%>
                                    <%--<div id="dvlblFax"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtFax" ID="lblFax" runat="server"
                                        Text="Fax:" ClientInstanceName="lblFax">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtFax"> --%>
                                    <dx:ASPxTextBox ID="txtFax" runat="server" ClientInstanceName="txtFax" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblKodiPostar"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodiPostar" ID="lblKodiPostar"
                                        runat="server" Text="Kodi Postar:" ClientInstanceName="lblKodiPostar">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtKodiPostar"> --%>
                                    <dx:ASPxTextBox ID="txtKodiPostar" runat="server" ClientInstanceName="txtKodiPostar" Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	ndryshoVlereAdreseKodiPostar(); }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblCel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtCel" ID="lblCel" runat="server"
                                        Text="Cel:" ClientInstanceName="lblCel">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtCel"> --%>
                                    <dx:ASPxTextBox ID="txtCel" runat="server" ClientInstanceName="txtCel" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="false">
                                            <RegularExpression ValidationExpression="^\s*\+?\s*([0-9 \(\)][\s-]*){9,}$" ErrorText="Formati nuk eshte i sakte!" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblQyteti"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtQyteti" ID="lblQyteti" runat="server"
                                        Text="Qyteti:" ClientInstanceName="lblQyteti">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtQyteti"> --%>
                                    <dx:ASPxComboBox ID="txtQyteti" runat="server" ClientInstanceName="txtQyteti" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { adresat(); }" />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblEmail"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmail" ID="lblEmail" runat="server"
                                        Text="Email:" ClientInstanceName="lblEmail">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtEmail"> --%>
                                    <dx:ASPxTextBox ID="txtEmail" runat="server" ClientInstanceName="txtEmail" Width="100%">
                                       <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorText="Format i gabuar e-mail!" />
                                           <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblShteti"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShteti" ID="lblShteti" runat="server"
                                        Text="Shteti:" ClientInstanceName="lblShteti">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtShteti"> --%>
                                    <dx:ASPxTextBox ID="txtShteti" runat="server" ClientInstanceName="txtShteti" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%-- </div>--%>
                                    <%--<div id="dvlblWebPage"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtWebpage" ID="lblWebPage" runat="server"
                                        Text="Web Page:" ClientInstanceName="lblWebPage">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtWebpage"> --%>
                                    <dx:ASPxTextBox ID="txtWebpage" runat="server" ClientInstanceName="txtWebpage" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%-- </div>--%>
                                    <%--<div id="dvlblEmertimFature"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimFature" ID="lblEmertimFature" runat="server"
                                        Text="Emertimi ne fature:" ClientInstanceName="lblEmertimFature">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtEmertimFature"> --%>
                                    <dx:ASPxTextBox ID="txtEmertimFature" runat="server" ClientInstanceName="txtEmertimFature" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodIntegrimi" ID="lblKodIntegrimi" runat="server"
                                        Text="Kod Integrimi:" ClientInstanceName="lblKodIntegrimi">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtEmertimFature"> --%>
                                    <dx:ASPxTextBox ID="txtKodIntegrimi" runat="server" ClientInstanceName="txtKodIntegrimi" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <%--<div id="dvlblNrTvsh"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrTvsh" ID="lblNrTvsh" runat="server"
                                        Text="Nr TVSH:" ClientInstanceName="lblNrTvsh">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtNrTvsh"> --%>
                                    <dx:ASPxTextBox ID="txtNrTvsh" runat="server" ClientInstanceName="txtNrTvsh" Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {
										}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbBij" ID="lblBij" runat="server"
                                        Text="Ndermarrje bij:" ClientInstanceName="lblBij">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbBij" runat="server" Width="100%" ClientInstanceName="cmbBij"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" OnItemRequestedByValue="cmbBij_ItemRequestedByValue">
                                        <ClientSideEvents ButtonClick="function(s, e) {Ndermarje_Click();}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries2" SetFocusOnError="true" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojPorosie" ID="lblLlojPorosie" runat="server"
                                        Text="Lloj Porosie" ClientInstanceName="lblLlojPorosie">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbLlojPorosie" runat="server" Width="100%" ClientInstanceName="cmbLlojPorosie"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries2" SetFocusOnError="true" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneCaktoNeHarte" ID="lblCaktoNeHarte"
                                        runat="server" Text="Cakto ne harte:" ClientInstanceName="lblCaktoNeHarte">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="btneCaktoNeHarte" runat="server" ClientInstanceName="btneCaktoNeHarte"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e){ hapLupeHarte(s, e); }" TextChanged="function(s, e){ vendosGeomNeHfState(s, e); }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField ErrorText="*" IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmailPerPajisje" ID="lblEmailPerPajisje" runat="server"
                                        Text="Email per pajisje:" ClientInstanceName="lblEmailPerPajisje">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtEmailPerPajisje" runat="server" ClientInstanceName="txtEmailPerPajisje" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorText="Format i gabuar e-mail!" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cblistLlojMarreveshje" ID="lblLlojMarreveshje" runat="server"
                                        Text="Lloj marreveshjeje:" ClientInstanceName="lblLlojMarreveshje">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBoxList ID="cblistLlojMarreveshje" ClientInstanceName="cblistLlojMarreveshje" runat="server" RepeatColumns="1" RepeatLayout="Table"
                                        Caption="">
                                        <CaptionSettings Position="Top" />
                                    </dx:ASPxCheckBoxList>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodiISKSH" ID="lblKodiISKSH" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodiISKSH">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtKodiISKSH" runat="server" AutoPostBack="false" ClientInstanceName="txtKodiISKSH"
                                        Width="100%">
                                        <ClientSideEvents Init="function(s, e) { s.Focus(); }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="True" RequiredField-IsRequired="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Kodi ISKSH nuk duhet te jete me shume se 100 karaktere" ValidationExpression="^[\s\S]{0,100}$"></RegularExpression>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>                                    
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbMeDogane" ID="lblMeDogane" runat="server" Text="Me Dogane" ClientInstanceName="lblMeDogane">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbMeDogane" runat="server" ClientInstanceName="cbMeDogane" Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>

                                    <br />
                                    <%--</div> --%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Kontakti" Text="Kontakti">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl4" runat="server">
                                    <table id="tblKontakti" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblKodi3">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi3" ID="lblKodi3" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi3">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtKodi3"> --%>
                                    <dx:ASPxTextBox ID="txtKodi3" runat="server" AutoPostBack="false" ClientInstanceName="txtKodi3"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodi3" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblEmertimi3"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi3" ID="lblEmertimi3"
                                        runat="server" Text="Emertimi:" ClientInstanceName="lblEmertimi3">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtEmertimi3"> --%>
                                    <dx:ASPxTextBox ID="txtEmertimi3" runat="server" AutoPostBack="false" ClientInstanceName="txtEmertimi3"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="TextChanged_txtEmertimi3" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--div< id="dvlblNrLlog3"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNr3" ID="lblNrLlog3" runat="server"
                                        Text="Nr. Llogari:" ClientInstanceName="lblNrLlog3">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtNr3"> --%>
                                    <dx:ASPxComboBox ID="txtNr3" runat="server" AutoPostBack="false" ClientInstanceName="txtNr3"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s,e) { ButtonClickedLlogaria(s); }" LostFocus="LostFocus_txtNr3" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <dx:ASPxTextBox ID="idLlog3" ClientInstanceName="idLlog3" runat="server" Visible="false"
                                        Width="100%">
                                    </dx:ASPxTextBox>
                                    <dx:ASPxGridView ID="gvKontakti" runat="server" ClientInstanceName="gvKontakti"
                                        OnAfterPerformCallback="gvKontakti_AfterPerformCallback" OnHtmlRowCreated="gvKontakti_HtmlRowCreated"
                                        OnCustomCallback="gvKontakti_CustomCallback" OnCustomJSProperties="gvKontakti_CustomJSProperties"
                                        OnDataBound="gvKontakti_DataBound">
                                        <ClientSideEvents BeginCallback="function(s, e) {
													merrTeDhena();
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
                                    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Text="" Visible="true" ClientInstanceName="txtLlog"
                                        Width="0%" EnableTheming="False" BackColor="White" Border-BorderColor="White"
                                        ForeColor="White">
                                        <Border BorderColor="White" />
                                    </dx:ASPxTextBox>
                                    <%--<div id="dvlblMenyraTransporti"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMenyraTransporti" ID="lblMenyraTransporti"
                                        runat="server" Text="Menyrat e transportit:" ClientInstanceName="lblMenyraTransporti">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcmbMenyraTransporti"> --%>
                                    <dx:ASPxComboBox ID="cmbMenyraTransporti" runat="server" ClientInstanceName="cmbMenyraTransporti"
                                        ShowShadow="False" OnItemRequestedByValue="cmbMenyraTransporti_ItemRequestedByValue"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s,e){MenyraTransporti_Click()}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblCmimUlet"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtCmimUlet" ID="lblCmimUlet" runat="server"
                                        Text="Me cmim te ulet:" ClientInstanceName="lblCmimUlet">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtCmimUlet"> --%>
                                    <dx:ASPxTextBox ID="txtCmimUlet" ClientInstanceName="txtCmimUlet" runat="server"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblEmertimi4"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi4" ID="lblEmertimi4"
                                        runat="server" Text="Emertimi:" ClientInstanceName="lblEmertimi4">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtEmertimi4"> --%>
                                    <dx:ASPxTextBox ID="txtEmertimi4" runat="server" AutoPostBack="false" ClientInstanceName="txtEmertimi4"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="TextChanged_txtEmertimi4" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Kontabiliteti" Text="Kontabiliteti">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl2" runat="server">
                                    <table id="tblKontabiliteti" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblKodi2"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi2" ID="lblKodi2" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi2">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtKodi2"> --%>
                                    <dx:ASPxTextBox ID="txtKodi2" runat="server" AutoPostBack="false" ClientInstanceName="txtKodi2"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodi2" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblEmertimi2"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi2" ID="lblEmertimi2"
                                        runat="server" Text="Emertimi:" ClientInstanceName="lblEmertimi2">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtEmertimi2"> --%>
                                    <dx:ASPxTextBox ID="txtEmertimi2" runat="server" AutoPostBack="false" ClientInstanceName="txtEmertimi2"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="TextChanged_txtEmertimi2" />
                                        <ValidationSettings>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblNrLlog2"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNr2" ID="lblNrLlog2" runat="server"
                                        Text="Nr. Llogari:" ClientInstanceName="lblNrLlog2">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtNr2"> --%>
                                    <dx:ASPxComboBox ID="txtNr2" runat="server" AutoPostBack="false" ClientInstanceName="txtNr2"
                                        CallbackPageSize="10" EnableCallbackMode="True" OnItemRequestedByValue="txtNr2_ItemRequestedByValue" OnItemsRequestedByFilterCondition="txtNr2_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick=" function(s,e) {ButtonClickedLlogaria(s); }" LostFocus="function(s, e) {LostFocusNrLlogarieTxtNr2(s);}"
                                            TextChanged="function(s, e) {
	nrLlogariChange();
}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true" RequiredField-IsRequired="True"
                                            EnableCustomValidation="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <asp:CustomValidator ID="nrLLogariseCostumValidator" runat="server" ValidationGroup="entries"
                                        ValidateEmptyText="False" ControlToValidate="txtNr2" ClientValidationFunction="validateLLogariKod"
                                        ErrorMessage="Nr. i llogarise nuk ekziston!" SetFocusOnError="True"></asp:CustomValidator>
                                    <%--</div> --%>
                                    <%--<div id="dvlblMonedha">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="lblPershkrimMonedha" ID="lblMonedha"
                                        runat="server" Text="Monedha:" ClientInstanceName="lblMonedha">
                                    </dx:ASPxLabel>
                                 
                                    <dx:ASPxTextBox ID="lblPershkrimMonedha" runat="server" Text="" ClientInstanceName="lblPershkrimMonedha"
                                        class="klasePerLblKonfigurimi" Height="25px" Width="100%">
                                        <Border BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" />
                                    </dx:ASPxTextBox>
                                    <%--<div id="dvlblLlogZbritje"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLlogKons" ID="lblLlogZbritje"
                                        runat="server" Text="Llogari Zbritje:" ClientInstanceName="lblLlogZbritje">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtLlogKons"> --%>
                                    <dx:ASPxComboBox ID="txtLlogKons" ClientInstanceName="txtLlogKons" runat="server"
                                        CallbackPageSize="10" EnableCallbackMode="True" OnItemRequestedByValue="txtLlogKons_ItemRequestedByValue" OnItemsRequestedByFilterCondition="txtLlogKons_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) {ButtonClickedLlogariZbritje(s);}"
                                            TextChanged="function(s, e) {
	nrLlogariZbritjeChange();
}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="true" ErrorTextPosition="Right"
                                            EnableCustomValidation="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblMonedhaZ"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="lblPershkrimMonedhaZ" ID="lblMonedhaZ"
                                        runat="server" Text="Monedha:" ClientInstanceName="lblMonedhaZ">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvlblPershkrimMonedhaZ" style="width: 100px;
									vertical-align: bottom;" align="center"> --%>
                                    <dx:ASPxLabel ID="lblPershkrimMonedhaZ" runat="server" Text="" ClientInstanceName="lblPershkrimMonedhaZ"
                                        class="klasePerLblKonfigurimi" Height="25px" Font-Size="10pt">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvlblLlogDytesore"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLlogDytesor" ID="lblLlogDytesore"
                                        runat="server" Text="Llogari dytesore:" ClientInstanceName="lblLlogDytesore">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtLlogDytesor"> --%>
                                    <dx:ASPxComboBox ID="txtLlogDytesor" runat="server" ClientInstanceName="txtLlogDytesor"
                                        OnItemRequestedByValue="txtLlogDytesor_ItemRequestedByValue" OnItemsRequestedByFilterCondition="txtLlogDytesor_ItemsRequestedByFilterCondition" SettingsLoadingPanel-ImagePosition="Top"
                                        ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) {ButtonClickedParaLlogaria(s);}" TextChanged="function(s, e) {
	nrLlogariParaChange();
}"
                                            LostFocus="function(s, e) {
	nrLlogariParaChange();
}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <asp:CustomValidator ID="CustomValidator2" ValidationGroup="entries" runat="server"
                                        ErrorMessage="Kjo llogari nuk ekziston" ControlToValidate="txtLlogDytesor" Display="None">*</asp:CustomValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" Enabled="True" TargetControlID="CustomValidator2"
                                        HighlightCssClass="validorCalloutHighlight" runat="server">
                                    </cc1:ValidatorCalloutExtender>
                                    <%--<div id="dvlblFushata"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbFushata" ID="lblFushata" runat="server"
                                        Text="Fushata:" ClientInstanceName="lblFushata">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcmbFushata"> --%>
                                    <dx:ASPxComboBox ID="cmbFushata" runat="server" Enabled="true" ClientInstanceName="cmbFushata"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblKategoriKlienti"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategoriKlienti" ID="lblKategoriKlienti"
                                        runat="server" Text="Kategori Klienti:" ClientInstanceName="lblKategoriKlienti">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcmbKategoriKlienti"> --%>
                                    <dx:ASPxComboBox ID="cmbKategoriKlienti" runat="server" ClientInstanceName="cmbKategoriKlienti"
                                        Enabled="true" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblQenderKosto"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtQenderKosto" ID="lblQenderKosto"
                                        runat="server" Text="Qendra e Kostos:" ClientInstanceName="lblQenderKosto">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtQenderKosto"> --%>
                                    <dx:ASPxComboBox ID="txtQenderKosto" Enabled="true" ClientInstanceName="txtQenderKosto"
                                        runat="server" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblKushteDergimi"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKushteDergimi" ID="lblKushteDergimi"
                                        runat="server" Text="Kushtet e dergimit:" ClientInstanceName="lblKushteDergimi">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcmbKushteDergimi"> --%>
                                    <dx:ASPxComboBox ID="cmbKushteDergimi" runat="server" ClientInstanceName="cmbKushteDergimi"
                                        ShowShadow="False" OnItemRequestedByValue="cmbKushteDergimi_ItemRequestedByValue"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s,e){KushteDergimi_Click()}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Regjistrime" Text="Regjistrime">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl6" runat="server">
                                    <table id="tblRegjistrime" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblKodi4"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi4" ID="lblKodi4" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi4">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <dx:ASPxLabel Wrap="False" ID="lblKrij" runat="server" AssociatedControlID="lblKrijuesi"
                                        ClientInstanceName="lblKrij" Text="Krijuesi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxLabel Wrap="False" ID="lblKrijuesi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKrijuesi"
                                        Text="">
                                    </dx:ASPxLabel>
                                    <%--<div id="dvtxtKodi4"> --%>
                                    <dx:ASPxTextBox ID="txtKodi4" runat="server" AutoPostBack="false" ClientInstanceName="txtKodi4"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodi4" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <br />
                                    <%--</div> --%>
                                    <%--<div id="dvlblLlojZbritjeCmime"> --%>

                                    <dx:ASPxLabel Wrap="False" ID="lblLlojZbritjeCmime" runat="server" Text="Lloj zbritje dhe cmime"
                                        ClientInstanceName="lblLlojZbritjeCmime" Font-Underline="True" Height="30px">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvlblKategoriZbritje"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKategoriZbritje" ID="lblKategoriZbritje"
                                        runat="server" Text="Kategori Zbritje(totale):" ClientInstanceName="lblKategoriZbritje">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvbtneKategoriZbritje"> --%>
                                    <dx:ASPxComboBox ID="btneKategoriZbritje" Enabled="true" runat="server" ClientInstanceName="btneKategoriZbritje"
                                        EnableCallbackMode="False"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { KategoriZbritje_Click();}" TextChanged="TextChanged_btneKategoriZbritje" />
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
                                    <%--</div> --%>

                                    <%--<div id="dvlblKategoriPerqindje"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKategoriPerqindja" ID="lblKategoriPerqindje"
                                        runat="server" Text="Perqindja:" ClientInstanceName="lblKategoriPerqindje">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtKategoriPerqindja"> --%>
                                    <dx:ASPxTextBox ID="txtKategoriPerqindja" ClientInstanceName="txtKategoriPerqindja"
                                        runat="server" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblZbritjeTotale"> --%>
                                    <dx:ASPxLabel ID="lblZbritjeTotale" runat="server" Text="Zbritje Totale:" ClientInstanceName="lblZbritjeTotale">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtZbritjeTotale"> --%>
                                    <dx:ASPxTextBox ID="txtZbritjeTotale" ClientInstanceName="txtZbritjeTotale" runat="server"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%-- </div> --%>
                                    <%--<div id="dvlblZbritjeAnalitike"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneNivelZbritje" ID="lblZbritjeAnalitike"
                                        runat="server" Text="Zbritje Analitike:" ClientInstanceName="lblZbritjeAnalitike">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvbtneNivelZbritje"> --%>
                                    <dx:ASPxComboBox ID="btneNivelZbritje" Enabled="true" runat="server" ClientInstanceName="btneNivelZbritje"
                                        EnableCallbackMode="False"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { NivelZbritje_Click();}" 
                                            TextChanged="function(s, e) { NivelZbritje_Changed();}" />
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
                                    <%--</div> --%>
                                    <%--<div id="dvlblNivelCmimi"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneNivelCmimi" ID="lblNivelCmimi"
                                        runat="server" Text="Nivel Cmimi:" ClientInstanceName="lblNivelCmimi">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvbtneNivelCmimi"> --%>
                                    <dx:ASPxComboBox ID="btneNivelCmimi" Enabled="true" runat="server" ClientInstanceName="btneNivelCmimi"
                                        EnableCallbackMode="True" OnItemRequestedByValue="btneNivelCmimi_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneNivelCmimi_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { NivelCmimi_Click();}" TextChanged="function(s, e) {niveliChange();
									   }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNivelTvsh" ID="lblNivelTVSH"
                                        runat="server" Text="Nivel TVSH-je:" ClientInstanceName="lblNivelTVSH">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbNivelTvsh" runat="server" ClientInstanceName="cmbNivelTvsh"
                                        Width="100%" ShowShadow="False" ValueType="System.String"
                                        OnItemRequestedByValue="cmbNivelTvsh_ItemRequestedByValue" SettingsLoadingPanel-ImagePosition="Top">
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
                                    <%--</div> --%>
                                    <%--<div id="dvlblOferta"> --%>
                                    <dx:ASPxLabel Wrap="False" ID="lblOferta" runat="server" Text="Oferta" ClientInstanceName="lblOferta">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvlblOfertaAutomatike"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbOfertaAutomatike" ID="lblOfertaAutomatike"
                                        runat="server" Text="Oferta Automatike:" ClientInstanceName="lblOfertaAutomatike">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcbOfertaAutomatike"> --%>
                                    <dx:ASPxCheckBox ID="cbOfertaAutomatike" runat="server" ClientInstanceName="cbOfertaAutomatike">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblVleraMin"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVleraMin" ID="lblVleraMin" runat="server"
                                        Text="Vlera minimum e porositur:" ClientInstanceName="lblVleraMin">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtVleraMin" style="visibility: hidden"> --%>
                                    <dx:ASPxTextBox ID="txtVleraMin" ClientInstanceName="txtVleraMin" runat="server"
                                        ClientVisible="false" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblPrioriteti"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPrioriteti" ID="lblPrioriteti"
                                        runat="server" Text="Prioriteti:" ClientInstanceName="lblPrioriteti">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcmbPrioriteti"> --%>
                                    <dx:ASPxComboBox ID="cmbPrioriteti" runat="server" ClientInstanceName="cmbPrioriteti"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <br />
                                    <%--</div> --%>
                                    <%--<div id="dvlblKushteVleraDefault"> --%>
                                    <dx:ASPxLabel Wrap="False" ID="lblKushteVleraDefault" runat="server" Text="Kushte dhe vlera default per shitjen"
                                        ClientInstanceName="lblKushteVleraDefault" Font-Underline="True" Height="30px">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvlblLimitParalajmerues"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLimitiParalajmerues" ID="lblLimitParalajmerues"
                                        runat="server" Text="Limiti Paralajmerues:" ClientInstanceName="lblLimitParalajmerues">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtLimitiParalajmerues"> --%>
                                    <dx:ASPxTextBox ID="txtLimitiParalajmerues" runat="server" ClientInstanceName="txtLimitiParalajmerues"
                                        ValidationSettings-CausesValidation="true" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblLimitiBllokues"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLimitiBllokues" ID="lblLimitiBllokues"
                                        runat="server" Text="Limiti Bllokues:" ClientInstanceName="lblLimitiBllokues">
                                    </dx:ASPxLabel>
                                    <%-- </div>--%>
                                    <%--<div id="dvtxtLimitiBllokues"> --%>
                                    <dx:ASPxTextBox ID="txtLimitiBllokues" runat="server" ClientInstanceName="txtLimitiBllokues"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblGrupimi1"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneGrupimi1" ID="lblGrupimi1" runat="server"
                                        Text="Grupimi 1:" ClientInstanceName="lblGrupimi1">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvbtneGrupimi1"> --%>
                                    <dx:ASPxComboBox ID="btneGrupimi1" runat="server" ClientInstanceName="btneGrupimi1"
                                        EnableCallbackMode="False"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) {
											grida='1';
											GrupiKF_Click(1);
											}"
                                            TextChanged="function(s, e) {
											}"
                                            SelectedIndexChanged="function(s,e){Utils.SelektimiBosh(s,e);}" />
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
                                    <%--</div> --%>
                                    <%--<div id="dvlblGrupimi2"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneGrupimi2" ID="lblGrupimi2" runat="server"
                                        Text="Grupimi 2:" ClientInstanceName="lblGrupimi2">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvbtneGrupimi2"> --%>
                                    <dx:ASPxComboBox ID="btneGrupimi2" runat="server" ClientInstanceName="btneGrupimi2"
                                        EnableCallbackMode="False"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) {
										grida ='2';
										GrupiKF_Click(2);
										}"
                                            TextChanged="function(s, e) {
										}"
                                            SelectedIndexChanged="function(s,e){Utils.SelektimiBosh(s,e);}" />
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
                                    <%--</div> --%>
                                    <%--<div id="dvlblGrupimi3"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneGrupimi3" ID="lblGrupimi3" runat="server"
                                        Text="Grupimi 3:" ClientInstanceName="lblGrupimi3">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvbtneGrupimi3"> --%>
                                    <dx:ASPxComboBox ID="btneGrupimi3" runat="server" ClientInstanceName="btneGrupimi3"
                                        EnableCallbackMode="False"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) {
											grida='3';
											GrupiKF_Click(3);}"
                                            TextChanged="function(s, e) { }"
                                            SelectedIndexChanged="function(s,e){Utils.SelektimiBosh(s,e);}" />
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbKupon" ID="lblKupon" runat="server"
                                        Text="Kupon:" ClientInstanceName="lblKupon">
                                    </dx:ASPxLabel>
                                    <%-- </div> --%>
                                    <%--<div id="dvcbAktiv"> --%>
                                    <dx:ASPxCheckBox ID="cbKupon" runat="server" ClientInstanceName="cbKupon" Checked="true"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <%--</div> --%>
                                    <%-- </div>--%>
                                    <%--<div id="dvlblLlogaritKomision"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbLlogaritKomision" ID="ASPxLabel1" runat="server"
                                        Text="Llogarit Komision:" ClientInstanceName="lblLlogaritKomision">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcbLlogaritKomision"> --%>
                                    <dx:ASPxCheckBox ID="cbLlogaritKomision" runat="server" ClientInstanceName="cbLlogaritKomision" Checked="False" Width="100%">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                    <%--</div> --%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Arka/Banka" Text="Arka/Banka">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl8" runat="server">
                                    <table id="tblBanka" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblNrLlog4"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNr4" ID="lblNrLlog4" runat="server"
                                        Text="Nr. Llogari:" ClientInstanceName="lblNrLlog4">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtNr4"> --%>
                                    <dx:ASPxComboBox ID="txtNr4" runat="server" AutoPostBack="false" ClientInstanceName="txtNr4"
                                        OnItemRequestedByValue="txtNr4_ItemRequestedByValue" SettingsLoadingPanel-ImagePosition="Top" OnItemsRequestedByFilterCondition="txtNr4_ItemsRequestedByFilterCondition"
                                        ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="
													function(s,e) {ButtonClickedLlogaria(s); }"
                                            LostFocus="LostFocus_txtNr4" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvidLlog4"> --%>
                                    <dx:ASPxTextBox ID="idLlog4" ClientInstanceName="idLlog4" runat="server" Visible="false">
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblKodi7"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi7" ID="lblKodi7" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi7">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtKodi7"> --%>
                                    <dx:ASPxTextBox ID="txtKodi7" runat="server" AutoPostBack="false" ClientInstanceName="txtKodi7"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodi7" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblEmertimi7"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi7" ID="lblEmertimi7"
                                        runat="server" Text="Emertimi:" ClientInstanceName="lblEmertimi7">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtEmertimi7"> --%>
                                    <dx:ASPxTextBox ID="txtEmertimi7" runat="server" AutoPostBack="false" ClientInstanceName="txtEmertimi7"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="TextChanged_txtEmertimi7" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblNrLlog7"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNr7" ID="lblNrLlog7" runat="server"
                                        Text="Nr. Llogari:" ClientInstanceName="lblNrLlog7">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtNr7"> --%>
                                    <dx:ASPxComboBox ID="txtNr7" runat="server" ClientInstanceName="txtNr7" CallbackPageSize="10"
                                        EnableCallbackMode="True" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False"
                                        Width="100%">
                                        <ClientSideEvents ButtonClick="
													function(s,e) {ButtonClickedLlogaria(s); }"
                                            LostFocus="LostFocus_txtNr7" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvASPxTextBox4"> --%>
                                    <dx:ASPxTextBox Wrap="False" ID="ASPxTextBox4" ClientInstanceName="idLlog5" runat="server"
                                        Visible="false" Width="100%">
                                    </dx:ASPxTextBox>
                                    <br />
                                    <%--</div>--%>
                                    <%--<div id="dvlblPergjithshemBanka"> --%>
                                    <dx:ASPxLabel Wrap="False" ID="lblPergjithshemBanka" runat="server" Text="Pergjithshem"
                                        ClientInstanceName="lblPergjithshemBanka" Font-Underline="True" Height="30px">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvlblIban"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtIban" ID="lblIban" runat="server"
                                        Text="IBAN:" ClientInstanceName="lblIban">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtIban"> --%>
                                    <dx:ASPxTextBox ID="txtIban" runat="server" ClientInstanceName="txtIban" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%-- </div>--%>
                                    <%--<div id="dvlblSwift"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtSwift" ID="lblSwift" runat="server"
                                        Text="SWIFT:" ClientInstanceName="lblSwift">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtSwift"> --%>
                                    <dx:ASPxTextBox ID="txtSwift" runat="server" ClientInstanceName="txtSwift" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblEmriBanka"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmriBanka" ID="lblEmriBanka"
                                        runat="server" Text="Banka:" ClientInstanceName="lblEmriBanka">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtEmriBanka"> --%>
                                    <dx:ASPxComboBox ID="txtEmriBanka" ClientInstanceName="txtEmriBanka" runat="server"
                                        EnableCallbackMode="True" OnItemRequestedByValue="txtEmriBanka_ItemRequestedByValue"
                                        OnItemsRequestedByFilterCondition="txtEmriBanka_ItemsRequestedByFilterCondition"
                                        ShowShadow="False" ValueType="System.String" Style="margin-bottom: 0px"
                                        TabIndex="3" AutoPostBack="false"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickBanka();}" TextChanged="function(s,e){TextChangedBanka()}" />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblPershkrimiEmriBanka" style=" width: 100px;
									vertical-align: bottom;" align="center"> --%>
                                    <dx:ASPxTextBox ID="lblPershkrimiEmriBanka" runat="server" Text="" ClientInstanceName="lblPershkrimiEmriBanka"
                                        class="klasePerLblKonfigurimi" Height="25px" Width="100%">
                                        <Border BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" />
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblAdresaBanka"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAdresaBanka" ID="lblAdresaBanka"
                                        runat="server" Text="Adresa e Bankes:" ClientInstanceName="lblAdresaBanka">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtAdresaBanka"> --%>
                                    <dx:ASPxMemo ID="txtAdresaBanka" runat="server" ClientInstanceName="txtAdresaBanka"
                                        Width="100%" Rows="3">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div> --%>
                                    <%--<div id="dvlblLlogBankare"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLlogariBankare" ID="lblLlogBankare"
                                        runat="server" Text="Llogari Bankare:" ClientInstanceName="lblLlogBankare">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtLlogariBankare"> --%>
                                    <dx:ASPxTextBox ID="txtLlogariBankare" runat="server" ClientInstanceName="txtLlogariBankare"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <br />
                                    <%--</div> --%>
                                    <%--<div id="dvlblLidhjaRegj"> --%>
                                    <dx:ASPxLabel Wrap="False" ID="lblLidhjaRegj" runat="server" Text="Lidhja me regjistrimet (likuidimi)"
                                        ClientInstanceName="lblLidhjaRegj" Font-Underline="True" Height="30px">
                                    </dx:ASPxLabel>
                                    <%-- </div>--%>
                                    <%--<div id="dvlblMaturimi"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btneMaturimi" ID="lblMaturimi" runat="server"
                                        Text="Maturimi:" ClientInstanceName="lblMaturimi">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvbtneMaturimi"> --%>
                                    <dx:ASPxComboBox ID="btneMaturimi" Enabled="true" runat="server" ClientInstanceName="btneMaturimi"
                                        EnableCallbackMode="True" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False"
                                        Width="100%">
                                        <ClientSideEvents ButtonClick="function(s, e) { AfateMaturimi_Click();}" SelectedIndexChanged="function(s,e){Utils.SelektimiBosh(s,e);}" />
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
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblKushtePagese"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKushtetPageses" ID="lblKushtePagese"
                                        runat="server" Text="Kushtet e Pageses:" ClientInstanceName="lblKushtePagese">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcmbKushtetPageses"> --%>
                                    <dx:ASPxComboBox ID="cmbKushtetPageses" runat="server" ClientInstanceName="cmbKushtetPageses" EnableCallbackMode="false"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents ButtonClick="function(s,e){KushtetPageses_Click()}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%-- </div>--%>
                                    <%--<div id="dvlblMetoda"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMetoda" ID="lblMetoda" runat="server"
                                        Text="Metoda e Pageses:" ClientInstanceName="lblMetoda">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvcmbMetoda"> --%>
                                    <dx:ASPxComboBox ID="cmbMetoda" runat="server" ClientInstanceName="cmbMetoda" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>


                                    <%--</div> --%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>

                        <dxtc:TabPage Name="Buxheti" Text="Buxheti">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl5" runat="server">
                                    <table id="tblBuxheti" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblKodi5"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi5" ID="lblKodi5" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi5">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtKodi5"> --%>
                                    <dx:ASPxTextBox ID="txtKodi5" runat="server" AutoPostBack="false" ClientInstanceName="txtKodi5"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="TextChanged_txtKodi5" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div> --%>
                                    <%--<div id="dvlblEmertimi5"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi5" ID="lblEmertimi5"
                                        runat="server" Text="Emertimi:" ClientInstanceName="lblEmertimi5">
                                    </dx:ASPxLabel>
                                    <%-- </div>--%>
                                    <%--<div id="dvtxtEmertimi5"> --%>
                                    <dx:ASPxTextBox ID="txtEmertimi5" runat="server" AutoPostBack="false" ClientInstanceName="txtEmertimi5"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="TextChanged_txtEmertimi5" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%-- </div>--%>
                                    <%--<div id="dvlblNrLlog5"> --%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNr5" ID="lblNrLlog5" runat="server"
                                        Text="Nr. Llogari:" ClientInstanceName="lblNrLlog5">
                                    </dx:ASPxLabel>
                                    <%--</div> --%>
                                    <%--<div id="dvtxtNr5"> --%>
                                    <dx:ASPxComboBox ID="txtNr5" runat="server" ClientInstanceName="txtNr5" SettingsLoadingPanel-ImagePosition="Top"
                                        ShowShadow="False" Width="100%">
                                        <ClientSideEvents ButtonClick="
													function(s,e) {ButtonClickedLlogaria(s); }"
                                            LostFocus="LostFocus_txtNr5" />
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
                                    <%-- </div>--%>
                                    <dx:ASPxTextBox ID="idLlog5" ClientInstanceName="idLlog5" runat="server" Visible="false">
                                    </dx:ASPxTextBox>
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

                        <%-- FUSHAT SHTESE--%>
                        <dxtc:TabPage Name="Fushat Shtese" Visible="true" Text="Fushat Shtese">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl7" runat="server">
                                    <uc1:ucFushatShtese runat="server" ID="ucFushatShtese" />
                                    <dx:ASPxLabel ID="lblNrLlog6" ClientInstanceName="lblNrLlog6" runat="server"></dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtNr6" ClientVisible="false" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtNr6"></dx:ASPxTextBox>
                                    
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="function(s, e) {
					   onActiveTabChanged(s,e);
					}" />
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                </dxtc:ASPxPageControl>
            </div>
            <dx:ASPxTextBox ID="txtLlog" runat="server" Text="" Visible="true" ClientInstanceName="txtLlog"
                Width="0%" EnableTheming="False" BackColor="White" Border-BorderColor="White"
                ForeColor="White">
                <Border BorderColor="White" />
            </dx:ASPxTextBox>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <%-- per te ruajtur vlerat e konfigurimit te lupave si konfig ambjenti--%>
                    <asp:HiddenField ID="hfLupaLlogaria" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogKons" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogDytesor" runat="server" />
                    <asp:HiddenField ID="hfLupaNivelZbritje" runat="server" />
                    <asp:HiddenField ID="hfLupaKushtePagese" runat="server" />
                    <asp:HiddenField ID="hfPerdoruesAktual" runat="server" />
                    <asp:HiddenField ID="hfLupaKushteDergimi" runat="server" />
                    <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
                    <asp:HiddenField ID="hfLupaAfateMaturimi" runat="server" />
                    <asp:HiddenField ID="hfLupaKategoriZbritje" runat="server" />
                    <asp:HiddenField ID="hfLupaNivelCmimi" runat="server" />
                    <asp:HiddenField ID="hfLupaMenyraTransp" runat="server" />
                    <asp:HiddenField ID="hfLupaAgjShitje" runat="server" />
                    <asp:HiddenField ID="hfLupaKfkryesor" runat="server" />
                    <asp:HiddenField ID="hfEmer" runat="server" />
                    <asp:HiddenField ID="hfMbiemer" runat="server" />
                    <asp:HiddenField ID="hfTel" runat="server" />
                    <asp:HiddenField ID="hfFax" runat="server" />
                    <asp:HiddenField ID="hfCel" runat="server" />
                    <asp:HiddenField ID="hfEmail" runat="server" />
                    <asp:HiddenField ID="hfAdresa" runat="server" />
                    <asp:HiddenField ID="hfKodiPostar" runat="server" />
                    <asp:HiddenField ID="hfBuxheti1" runat="server" />
                    <asp:HiddenField ID="hfBuxheti2" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="hfFushatShtese" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                    <asp:HiddenField ID="hfDraft" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <%-- Hidden fields per Arkiven--%>
                    <asp:HiddenField ID="hfArkivaDokId" runat="server" />
                    <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
                    </dx:ASPxHiddenField>
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfNrAutoKF" runat="server" ClientInstanceName="hfNrAutoKF">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
            </dx:ASPxHiddenField>
            <asp:HiddenField ID="hfMeme" runat="server" />
            <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
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
                    </dx:ASPxPopupControl>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
