<%@ Page Language="C#" Title="" AutoEventWireup="true" CodeBehind="Shto_Llogari.aspx.cs"
    Inherits="PlatinumWeb.Shto_Llogari" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>


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
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
    
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Shto_Llogari.aspx-IMB.2.1.js&v76""
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
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
                        Width="300px" ClientIDMode="AutoID">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
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
            <div id="dvLlogaria" style="display: none">
                <%-- style="visibility: hidden"--%>
                <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
                      TabSpacing="3px" Width="100%" ActiveTabIndex="4" OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged"
                    Height="520px">
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                    <TabPages>
                        <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl1" runat="server">
                                    <table class="renditKontrolle">
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                    runat="server" Style="font-size: large" Text="Modeli:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33">
                                                <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                    Height="23px" Width="100%" ShowShadow="False" Style="font-size: medium" ValueType="System.String"
                                                    SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
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
                                                <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi"
                                                    ClientInstanceName="lblKonfigurimi">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33"></td>
                                        </tr>
                                    </table>
                                    <dx:ASPxGridView ID="grid_ListLlogarish" ClientInstanceName="grid_ListLlogarish"
                                        runat="server" Width="100%" OnDataBound="grid_ListLlogarish_DataBound" OnAfterPerformCallback="grid_ListLlogarish_AfterPerformCallback"
                                        OnHeaderFilterFillItems="grid_ListLlogarish_HeaderFilterFillItems" OnProcessColumnAutoFilter="grid_ListLlogarish_ProcessColumnAutoFilter"
                                        OnCustomCallback="grid_ListLlogarish_CustomCallback" OnCustomJSProperties="gridllog_CustomJSProperties">
                                        <ClientSideEvents RowDblClick="function(s, e) { kaloTab=true; OnGridDoubleClick(e.visibleIndex); }"
                                            FocusedRowChanged="function(s, e) { onNdryshimFokusi(); }"
                                            BeginCallback="function(s, e) { BeginCallback(s,e); }"  
                                            EndCallback="function (s, e){ PageControl.AdjustSize(); }"/>
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
                                    <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_ListLlogarish"
                                        ExportedRowType="Selected" />
                                    <br />
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Llogaria" Text="Llogaria">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl3" runat="server">
                                    <table id="tblLlogaria" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblNr">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNr" ID="lblNr" runat="server"
                                        Text="Nr:" ClientInstanceName="lblNr">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtNr">--%>
                                    <dx:ASPxTextBox ID="txtNr" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtNr">
                                        <ClientSideEvents TextChanged="TextChanged_txtNr" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true" RegularExpression-ValidationExpression="^[\s\S]{0,20}$"
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
                                    <%--<div id="dvlblEmer1">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerLlogarie1" ID="lblEmer1"
                                        runat="server" Text="Emer Llogarie 1:" ClientInstanceName="lblEmer1">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtEmerLlogarie1">--%>
                                    <dx:ASPxMemo ID="txtEmerLlogarie1" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtEmerLlogarie1" Rows="3">
                                        <ClientSideEvents TextChanged="TextChanged_txtEmerLlogarie1" />
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
                                    <%--<div id="dvlblEmer2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerLlogarie2" ID="lblEmer2"
                                        runat="server" Text="Emer Llogarie 2:" ClientInstanceName="lblEmer2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtEmerLlogarie2">--%>
                                    <dx:ASPxMemo ID="txtEmerLlogarie2" runat="server" Width="100%" ClientInstanceName="txtEmerLlogarie2"
                                        Rows="3">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                    <%--<div id="dvlblEmerFr">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerLlogarieFr" ID="lblEmerFr"
                                        runat="server" Text="Emer Llogarie Frengjisht:" ClientInstanceName="lblEmerFr">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtEmerLlogarieFr">--%>
                                    <dx:ASPxMemo ID="txtEmerLlogarieFr" runat="server" Width="100%" ClientInstanceName="txtEmerLlogarieFr"
                                        Rows="3">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                    <%--<div id="dvlblMonedha">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMonedha" ID="lblMonedha" runat="server"
                                        Text="Monedha:" ClientInstanceName="lblMonedha">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbMonedha">--%>
                                    <dx:ASPxComboBox ID="cmbMonedha" runat="server" ClientInstanceName="cmbMonedha"
                                        ShowShadow="False" Width="100%" OnItemRequestedByValue="cmbMonedha_ItemRequestedByValue"
                                        SettingsLoadingPanel-ImagePosition="Top">
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
                                            <RequiredField ErrorText="*" IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblGrupi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGrupi" ID="lblGrupi" runat="server"
                                        Text="Grupi:" ClientInstanceName="lblGrupi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbGrupi">--%>
                                    <dx:ASPxComboBox ID="cmbGrupi" runat="server" AutoPostBack="false" ClientInstanceName="cmbGrupi"
                                        EnableCallbackMode="True" Width="100%" ShowShadow="False" ValueType="System.String"
                                         SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents ButtonClick=" function(s,e) 
                                                                                            {grida=false;
                                                                                            Grupi_Click(); }"
                                            LostFocus="function(s, e) {
                                                                                                               
	                                                        }" />
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
                                            <RequiredField ErrorText="*" IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblNengrupi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNengrupi" ID="lblNengrupi" runat="server"
                                        Text="Nengrupi:" ClientInstanceName="lblNengrupi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbNengrupi">--%>
                                    <dx:ASPxComboBox ID="cmbNengrupi" runat="server" ClientInstanceName="cmbNengrupi"
                                        EnableCallbackMode="True" Width="100%" ShowShadow="False" ValueType="System.String"
                                      OnItemRequestedByValue="cmbNengrupi_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbNengrupi_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents ButtonClick="function(s, e) { kontrolloGrupin();}"
                                            LostFocus="function(s, e) { }" />
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
                                            <RequiredField ErrorText="*" IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji" runat="server"
                                        Text="Lloj qendre:" ClientInstanceName="lblLloji">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbMonedha">--%>
                                    <dx:ASPxComboBox ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji" ShowShadow="False"
                                        Width="100%" SettingsLoadingPanel-ImagePosition="Top">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {qendraKostos_TextBox.SetText('');}" />
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="qendraKostos_TextBox" ID="lblQenderKosto"
                                        runat="server" Text="Qendra e Kostos:" ClientInstanceName="lblQenderKosto">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvqendraKostos_TextBox">--%>
                                    <dx:ASPxComboBox ID="qendraKostos_TextBox" Enabled="true" runat="server" ClientInstanceName="qendraKostos_TextBox"
                                         EnableCallbackMode="True" OnItemRequestedByValue="qendraKostos_TextBox_ItemRequestedByValue" OnItemsRequestedByFilterCondition="qendraKostos_TextBox_ItemsRequestedByFilterCondition"
                                       SettingsLoadingPanel-ImagePosition="Top"
                                        ShowShadow="False" Width="100%">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ClientSideEvents ButtonClick="function(s, e) {Qendra_Click();}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave='false'>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbObjektiva" ID="lblObjektiva"
                                        runat="server" Text="Objektiva:" ClientInstanceName="lblObjektiva">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbObjektiva" ClientInstanceName="cmbObjektiva" Width="100%"
                                        runat="server"  EnableCallbackMode="True"
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="checkAktivLlogaria" ID="lblAktivLlogaria"
                                        runat="server" Text="Aktiv:" ClientInstanceName="lblAktivLlogaria">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="checkAktivLlogaria" runat="server" ClientInstanceName="checkAktivLlogaria" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1">
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Grupim" Text="Grupim">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl2" runat="server">
                                    <table id="tblGrupimi" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblNr2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="numri_TextBox" ID="lblNr2" runat="server"
                                        Text="Nr:" ClientInstanceName="lblNr2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvnumri_TextBox">--%>
                                    <dx:ASPxTextBox ID="numri_TextBox" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="numri_TextBox">
                                        <ClientSideEvents TextChanged="TextChanged_numri_TextBox" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblEmri2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="emer_TextBox" ID="lblEmri2" runat="server"
                                        Text="Emer Llogarie:" ClientInstanceName="lblEmri2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemer_TextBox">--%>
                                    <dx:ASPxMemo Rows="3" ID="emer_TextBox" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="emer_TextBox">
                                        <ClientSideEvents TextChanged="TextChanged_emer_TextBox" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogStandarte">--%>
                                    <dx:ASPxLabel Wrap="False" ID="lblLlogStandarte" runat="server" Text="Llogari standarte:"
                                        ClientInstanceName="lblLlogStandarte">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvlblGrupi1">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="FSC_1_Debi_TextBox" ID="lblGrupi1"
                                        runat="server" Text="Grupi 1:" ClientInstanceName="lblGrupi1">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvFSC_1_Debi_TextBox">--%>
                                    <dx:ASPxComboBox ID="FSC_1_Debi_TextBox" Enabled="true" runat="server" ClientInstanceName="FSC_1_Debi_TextBox"
                                        EnableCallbackMode="True" Width="100%" 
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                        <DisabledStyle Font-Bold="False" BackColor="#EEEEEE" ForeColor="Black">
                                        </DisabledStyle>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <RequiredField IsRequired="true" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <ClientSideEvents ButtonClick="function(s, e) {grida=false;KPF1_Click(); }" />
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblGrupi2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="FSC_2_Debi_TextBox" ID="lblGrupi2"
                                        runat="server" Text="Grupi 2:" ClientInstanceName="lblGrupi2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvFSC_2_Debi_TextBox">--%>
                                    <dx:ASPxComboBox ID="FSC_2_Debi_TextBox" Enabled="true" runat="server" ClientInstanceName="FSC_2_Debi_TextBox"
                                        EnableCallbackMode="True" Width="100%"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                        <ClientSideEvents ButtonClick="function(s, e) {grida=false;  KPF2_Click();}" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblGrupi3">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="FSC_3_Debi_TextBox" ID="lblGrupi3"
                                        runat="server" Text="Grupi 3:" ClientInstanceName="lblGrupi3">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvFSC_3_Debi_TextBox">--%>
                                    <dx:ASPxComboBox ID="FSC_3_Debi_TextBox" Enabled="true" runat="server" ClientInstanceName="FSC_3_Debi_TextBox"
                                        EnableCallbackMode="True" Width="100%"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                        <ClientSideEvents ButtonClick="function(s, e) {grida=false; KPF3_Click();}" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblQenderKosto">--%>
                                    <%--</div>--%>
                                    <%--<div id="dvlblNivelTakse">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="nivelTakse_ComboBox" ID="lblNivelTakse"
                                        runat="server" Text="Nivel Takse:" ClientInstanceName="lblNivelTakse">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvnivelTakse_ComboBox">--%>
                                    <dx:ASPxComboBox ID="nivelTakse_ComboBox" Enabled="true" runat="server" ClientInstanceName="nivelTakse_ComboBox"
                                        ShowShadow="False" OnItemRequestedByValue="nivelTakse_ComboBox_ItemRequestedByValue"
                                         SettingsLoadingPanel-ImagePosition="Top" Width="100%">
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
                                    <%--<div id="dvlblAutorizimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAutorizimi" ID="lblAutorizimi"
                                        runat="server" Text="Nivel Autorizimi:" ClientInstanceName="lblAutorizimi">
                                    </dx:ASPxLabel>
                                   <div>
                                        <select id="cmbAutorizimi">
                                        </select>
                                        <asp:HiddenField ID="cmbAutorizimiHf" ClientIDMode="Static" runat="server" />
                                               
                                    </div>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategori" ID="lblKategori"
                                        runat="server" Text="Kategori:" ClientInstanceName="lblKategori">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbLlog">--%>
                                    <dx:ASPxComboBox ID="cmbKategori" ClientInstanceName="cmbKategori" Width="100%"
                                        runat="server" OnItemRequestedByValue="cmbKategori_ItemRequestedByValue" EnableCallbackMode="True"
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
                                        <ClientSideEvents ButtonClick="function(s, e) {Kategori_Click();}" />
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogKons">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="llogKonsoliduese_TextBox" ID="lblLlogKons"
                                        runat="server" Text="Llogari Konsoliduese:" ClientInstanceName="lblLlogKons">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvllogKonsoliduese_TextBox">--%>
                                    <dx:ASPxComboBox ID="llogKonsoliduese_TextBox" 
                                        ClientInstanceName="llogKonsoliduese_TextBox"
                                        runat="server" 
                                        Width="100%" 
                                        OnItemRequestedByValue="llogKonsoliduese_TextBox_ItemRequestedByValue"
                                        OnItemsRequestedByFilterCondition="llogKonsoliduese_TextBox_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                        <ClientSideEvents ButtonClick="function(s, e) {LlogKons_Click();}" />
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
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogKorr">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="llogKorresponduese_TextBox" ID="lblLlogKorr"
                                        runat="server" Text="Llogari Korresponduese:" ClientInstanceName="lblLlogKorr">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvllogKorresponduese_TextBox">--%>
                                    <dx:ASPxComboBox ID="llogKorresponduese_TextBox" ClientInstanceName="llogKorresponduese_TextBox"
                                        runat="server" Width="100%" ShowShadow="False" OnItemRequestedByValue="llogKorresponduese_TextBox_ItemRequestedByValue"
                                        OnItemsRequestedByFilterCondition="llogKorresponduese_TextBox_ItemsRequestedByFilterCondition"
                                        SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents ButtonClick="function(s, e) {LlogKorr_Click();}" />
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
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>

                                      <%--<div id="dvlblShenime1">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime1" ID="lblShenime1" runat="server"
                                        Text="Shenime 1:" ClientInstanceName="lblShenime1">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtShenime1">--%>
                                    <dx:ASPxTextBox ID="txtShenime1" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtShenime1">
                                       
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
                                    
                                    <%--<div id="dvlblShenime2">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime2" ID="lblShenime2" runat="server"
                                        Text="Shenime 2:" ClientInstanceName="lblShenime2">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtShenime2">--%>
                                    <dx:ASPxTextBox ID="txtShenime2" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtShenime2">
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

                                    <%--<div id="dvlblShenime3">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime3" ID="lblShenime3" runat="server"
                                        Text="Shenime 3:" ClientInstanceName="lblShenime3">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtShenime3">--%>
                                    <dx:ASPxTextBox ID="txtShenime3" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtShenime3">
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

                                    <%--<div id="dvlblShenime4">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime4" ID="lblShenime4" runat="server"
                                        Text="Shenime 4:" ClientInstanceName="lblShenime4">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtShenime4">--%>
                                    <dx:ASPxTextBox ID="txtShenime4" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtShenime4">
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

                                     <%--<div id="dvlblShenime5">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime5" ID="lblShenime5" runat="server"
                                        Text="Shenime 5:" ClientInstanceName="lblShenime5">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtShenime5">--%>
                                    <dx:ASPxTextBox ID="txtShenime5" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtShenime5">
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


                                    <%--</div>--%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>

                        <dxtc:TabPage Name="Fushat Shtese" Text="Fushat Shtese">

                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl7" runat="server">
                                    <%--<br />
                                <br />--%>
                                    <br />
                                    <table id="tblFushaShtese" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <dx:ASPxTextBox ID="txtLlog" runat="server" Text="" Visible="true" ClientInstanceName="txtLlog"
                                        Width="0%" EnableTheming="False" BackColor="White" Border-BorderColor="White"
                                        ForeColor="White">
                                        <Border BorderColor="White" />
                                    </dx:ASPxTextBox>
                                    <%--<div id="dvlblNr4">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNr6" ID="lblNr4" runat="server"
                                        Text="Nr:" ClientInstanceName="lblNr4">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtNr6">--%>
                                    <dx:ASPxTextBox ID="txtNr6" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtNr6">
                                        <ClientSideEvents TextChanged="TextChanged_txtNr6" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="false"
                                            ValidationGroup="entries1">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblEmer4">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmertimi6" ID="lblEmer4" runat="server"
                                        Text="Emer Llogarie:" ClientInstanceName="lblEmer4">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtEmertimi6">--%>
                                    <dx:ASPxTextBox ID="txtEmertimi6" runat="server" Width="100%" AutoPostBack="false"
                                        ClientInstanceName="txtEmertimi6">
                                        <ClientSideEvents TextChanged="TextChanged_txtEmertimi6" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="false"
                                            ValidationGroup="entries1">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblModeli">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbModeli" ID="lblModeli" runat="server"
                                        Text="Modeli:" ClientInstanceName="lblModeli">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbModeli">--%>
                                    <dx:ASPxComboBox ID="cmbModeli" runat="server" SelectedIndex="0" AutoPostBack="true"
                                        ClientInstanceName="cmbModeli" Width="100%" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="false"
                                            ValidationGroup="entries1">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="function(s, e) { activeTabChanged(s, e); }" />
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                </dxtc:ASPxPageControl >
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />

                    <asp:HiddenField ID="hfFushatShtese" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfGrupNenGrup" runat="server" />
                    <asp:HiddenField ID="hfLupaGrupi" runat="server" />
                    <asp:HiddenField ID="hfLupaNengrupi" runat="server" />
                    <asp:HiddenField ID="hfLupaKpf1" runat="server" />
                    <asp:HiddenField ID="hfLupaKpf2" runat="server" />
                    <asp:HiddenField ID="hfLupaKpf3" runat="server" />
                    <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogKons" runat="server" />
                    <asp:HiddenField ID="hfLupaLloKorr" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfMonedhaNder" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
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
                    </dx:ASPxPopupControl >
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
