<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_AzhornimKlientFurnitor.aspx.cs"
    Inherits="PlatinumWeb.Shto_AzhornimKlientFurnitor" ValidateRequest="false" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxnb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server"
        id="themeJQuery" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" media="screen" rel="stylesheet" type="text/css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/myMesazh-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/aspx.js/Shto_AzhornimKlientFurnitor.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
    <style type="text/css">
        input.btnStyle {
            font-size: 11px;
            width: 15px;
            height: 10px;
            background-image: url('.../images/Ball-stop-32.png');
            border-style: solid;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                            <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                        BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                        <ClientSideEvents Init="function(s, e) {
	
}" />
                                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <SubMenuStyle GutterWidth="17px" />
                                    </dx:ASPxMenu>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
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
	Utils.shfaqLoadingGif();;
    popFshi.Hide();
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
            </Triggers>
            <ContentTemplate>
                <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Red" ClientInstanceName="pergjigja">
                </dx:ASPxLabel>
                <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
            </Triggers>
            <ContentTemplate>
                <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo" />
                <asp:HiddenField ID="hfUrl" runat="server" />
                <asp:HiddenField ID="hfLloji" runat="server" />
                <asp:HiddenField ID="hfKolonaGride" runat="server" />
                <asp:HiddenField ID="hfKod" runat="server" />
                <asp:HiddenField ID="hfEmer" runat="server" />
                <asp:HiddenField ID="hfPershkrimi" runat="server" />
                <asp:HiddenField ID="hfMonedha" runat="server" />
                <asp:HiddenField ID="hfDebiKredi" runat="server" />
                <asp:HiddenField ID="hfGjendja" runat="server" />
                <asp:HiddenField ID="hfGjendjaMon" runat="server" />
                <asp:HiddenField ID="hfKursi" runat="server" />
                <asp:HiddenField ID="hfGjendjaAktuale" runat="server" />
                <asp:HiddenField ID="hfDiferenca" runat="server" />
                <asp:HiddenField ID="hfMonedhaNder" runat="server" />
                <asp:HiddenField ID="hfLupaKlientFurnitor" runat="server" />
                <asp:HiddenField ID="hfLlogaria" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfAutorizimi" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="HfGridCol" runat="server" />
                <asp:HiddenField ID="hfKurset" runat="server" />
                <asp:HiddenField ID="gridDataObject" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaInfoKF" runat="server" />
                <asp:HiddenField ID="hfHapurMbyllur" runat="server" />
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
                                <table id="tblFillim" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                                <div id="dvFillim" class="atributeDiveFshehur">
                                    <%--<div id="dvkonfigurimi_Label">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                        runat="server" Text="Lloji:" ClientInstanceName="konfigurimi_Label">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvcmbKonfigurimi">--%>
                                    <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                        ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                                        <LoadingPanelImage>
                                        </LoadingPanelImage>
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
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <div id="dvlblKonfigurimi">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi"
                                                        ClientInstanceName="lblKonfigurimi" Text="">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <%--<div id="dvlblNrDokumenti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrDokumenti" ID="lblNrDokumenti"
                                        runat="server" Text="Nr.Dokumenti" ClientInstanceName="lblNrDokumenti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtNrDokumenti">--%>
                                    <dx:ASPxTextBox ID="txtNrDokumenti" runat="server" ClientInstanceName="txtNrDokumenti"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
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
                                    <%--<div id="dvlblData">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="Data_DateEdit" ID="lblData" runat="server"
                                        Text="Data" ClientInstanceName="lblData">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvData_DateEdit">--%>
                                    <dx:ASPxDateEdit ID="Data_DateEdit" runat="server" ClientInstanceName="Data_DateEdit"
                                        ShowShadow="False" Width="100%">
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <ClientSideEvents DateChanged="function (s,e){dateChanged()}" GotFocus="function(s, e){ dateGotFocus( s, e); }" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogariDebi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="llogariDebi_ButtonEdit" ID="lblLlogariDebi"
                                        runat="server" Text="Llogari Debi" ClientInstanceName="lblLlogariDebi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvllogariDebi_ButtonEdit">--%>
                                    <dx:ASPxComboBox ID="llogariDebi_ButtonEdit" ClientInstanceName="llogariDebi_ButtonEdit"
                                        runat="server" Width="100%" ShowShadow="False" CallbackPageSize="10" EnableCallbackMode="True"
                                        SettingsLoadingPanel-ImagePosition="Top" OnItemRequestedByValue="llogariDebi_ButtonEdit_ItemRequestedByValue" OnItemsRequestedByFilterCondition="llogariDebi_ButtonEdit_ItemsRequestedByFilterCondition">
                                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickLlogariDebi();}"
                                            TextChanged="function(s,e){var s=llogariDebi_ButtonEdit.GetText().split(';'); llogariDebi_ButtonEdit.SetText(s[0]); vendosDebi();}" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblLlogariKredi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="llogariKredi_ButtonEdit" ID="lblLlogariKredi"
                                        runat="server" Text="Llogari Kredi" ClientInstanceName="lblLlogariKredi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvllogariKredi_ButtonEdit">--%>
                                    <dx:ASPxComboBox ID="llogariKredi_ButtonEdit" ClientInstanceName="llogariKredi_ButtonEdit"
                                        runat="server" Width="100%" ShowShadow="False" CallbackPageSize="10" EnableCallbackMode="True"
                                        SettingsLoadingPanel-ImagePosition="Top" OnItemsRequestedByFilterCondition="llogariKredi_ButtonEdit_ItemsRequestedByFilterCondition" OnItemRequestedByValue="llogariKredi_ButtonEdit_ItemRequestedByValue">
                                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickLlogariKredite();}"
                                            TextChanged="function(s,e){var s=llogariKredi_ButtonEdit.GetText().split(';'); llogariKredi_ButtonEdit.SetText(s[0]); VendosKredi();}" />
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
                                    <%--</div>--%>
                                    <%--<div id="dvlblPershkrimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi" ID="lblPershkrimi"
                                        runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtPershkrimi">--%>
                                    <dx:ASPxMemo ID="txtPershkrimi" runat="server" ClientInstanceName="txtPershkrimi"
                                        Rows="3" Columns="35" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <%--<ErrorImage Url="~/App_Themes/Aqua/Editors/edtError.png" />--%>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <ClientSideEvents TextChanged="function(s,e){ValueChangedPershkrimi();}" />
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                    <%--<div id="dvlblPeriudha">--%>
                                    <dx:ASPxLabel ID="lblPeriudha" runat="server" Text="Periudha kontabel" ClientInstanceName="lblPeriudha"
                                        ClientVisible="false">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvbtnPeriudha">--%>
                                    <dx:ASPxComboBox ID="btnPeriudha" runat="server" ClientInstanceName="btnPeriudha"
                                        ClientVisible="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                        <ClientSideEvents ButtonClick="function(s, e) {ButtonClickPeriudha();}" LostFocus="function(s, e) {                            
                            valueChangedPeriudha();}" />
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
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblPeriudhaAktuale">--%>
                                    <dx:ASPxLabel ID="lblPeriudhaAktuale" runat="server" Text="Kursi:" ClientInstanceName="lblPeriudhaAktuale"
                                        ClientVisible="false">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <dx:ASPxButton ID="btnLlogarit" Width="100%" runat="server" Text="Llogarit Azhornim"
                                        ClientInstanceName="btnLlogarit" AutoPostBack="False" CausesValidation="False">
                                        <ClientSideEvents Click="function(s, e) { LlogaritAzhornim(); 
}" />
                                    </dx:ASPxButton>
                                    <br />
                                    <div id="dvbtnLlogarit">
                                        <table width="100%">
                                            <tr align="center">
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table>
                                        <tr>

                                            <td>
                                                <dx:ASPxButton ID="btnZgjidhGjitha" runat="server" ToolTip="Zgjidh të gjitha në këtë faqe"
                                                    ClientInstanceName="btnZgjidhGjitha" Image-Url="images/check2.png" Image-Height="16px" AutoPostBack="False" CausesValidation="False">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
          }"
                                                        Click="function(s, e) {  KlikoTeGjitha();
}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                                    AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s, e) { gvKF.SelectRows(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnHiqZgjedhjen" runat="server" ToolTip="Hiq zgjedhjen në këtë faqe"
                                                    ClientInstanceName="btnHiqZgjedhjen" Image-Url="images/uncheck2.png" Image-Height="16px" AutoPostBack="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {  HiqTeGjitha();
}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <%-- <td>
                                                <dx:ASPxButton ID="btnZgjidhKunder" runat="server" Text="Zgjidh të kundërtën në këtë faqe"
                                                    ClientInstanceName="btnZgjidhKunder" AutoPostBack="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	  kundert();
}" />
                                                </dx:ASPxButton>
                                            </td>--%>
                                        </tr>
                                    </table>
                                    <dx:ASPxGridView ID="gvKF" runat="server" ClientInstanceName="gvKF" OnDataBound="gvKF_DataBound"
                                        OnAfterPerformCallback="gvKF_AfterPerformCallback" Width="100%" OnCustomCallback="gvKF_CustomCallback"
                                        OnCustomJSProperties="gvKF_CustomJSProperties" OnHtmlRowCreated="gvKF_HtmlRowCreated">
                                        <SettingsBehavior AllowSelectByRowClick="True" />
                                        <SettingsLoadingPanel ImagePosition="Top" />
                                        <ClientSideEvents SelectionChanged="function(s, e) { SelectionChange(s,e); }"
                                            EndCallback="gvKfEndCallback" />
                                        <ImagesEditors>
                                            <DropDownEditDropDown>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </DropDownEditDropDown>
                                            <SpinEditIncrement>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditIncrementImageHover_Aqua"
                                                    PressedCssClass="dxEditors_edtSpinEditIncrementImagePressed_Aqua" />
                                            </SpinEditIncrement>
                                            <SpinEditDecrement>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditDecrementImageHover_Aqua"
                                                    PressedCssClass="dxEditors_edtSpinEditDecrementImagePressed_Aqua" />
                                            </SpinEditDecrement>
                                            <SpinEditLargeIncrement>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeIncImageHover_Aqua"
                                                    PressedCssClass="dxEditors_edtSpinEditLargeIncImagePressed_Aqua" />
                                            </SpinEditLargeIncrement>
                                            <SpinEditLargeDecrement>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeDecImageHover_Aqua"
                                                    PressedCssClass="dxEditors_edtSpinEditLargeDecImagePressed_Aqua" />
                                            </SpinEditLargeDecrement>
                                        </ImagesEditors>
                                        <Styles>
                                            <LoadingPanel ImageSpacing="8px">
                                            </LoadingPanel>
                                        </Styles>
                                        <StylesEditors>
                                            <CalendarHeader Spacing="1px">
                                            </CalendarHeader>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                    <br />
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 30%"></td>
                                            <td>
                                                <dx:ASPxButton ID="btnDjathtas1" runat="server" Text="v" AutoPostBack="false" Width="50px" ClientInstanceName="btnDjathtas1">
                                                    <ClientSideEvents Click="function(s, e) { SelectionChangedGrid(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnDjathtasGjitha" runat="server" Text="vv" AutoPostBack="false"
                                                    Width="50px" ClientInstanceName="btnDjathtasGjitha">
                                                    <ClientSideEvents Click="function(s, e) { GetAllGrid(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnMajtas1" runat="server" Text="^" AutoPostBack="false" Width="50px">
                                                    <ClientSideEvents Click="function(s, e) { Majtas1(s,e); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnMajtaGjitha" runat="server" Text="^^" AutoPostBack="false"
                                                    Width="50px">
                                                    <ClientSideEvents Click="function(s, e) { MajtasGjithe(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td style="width: 30%"></td>
                                        </tr>
                                    </table>
                                    <br />
                                </div>
                                <div id="divgride1" style="display: none">
                                    <div id="divgride2">
                                        <table id="rowed5">
                                        </table>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <table id="tblFund" class="renditKontrolle">
                                    <tbody>
                                        <%--<tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="width: 50%">
                                        </td>
                                    </tr>--%>
                                    </tbody>
                                </table>
                                <div id="dvFundi" class="atributeDiveFshehur">
                                    <%--<div id="dvlblDtRegjistrimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtRegjistrimi" ID="lblDtRegjistrimi"
                                        runat="server" Text="Dt Regjistrimi:" ClientInstanceName="lblDtRegjistrimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvdteDtRegjistrimi">--%>
                                    <dx:ASPxDateEdit ID="dteDtRegjistrimi" runat="server" ClientInstanceName="dteDtRegjistrimi"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
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
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                    <%--</div>--%>
                                </div>
                            </asp:Panel>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
                <dx:SplitterPane MaxSize="300px" ShowCollapseBackwardButton="True" Separators-Size="10px"
                    PaneStyle-BackColor="Transparent" Collapsed="True" ShowCollapseForwardButton="True"
                    ScrollBars="Auto" AllowResize="True" MinSize="80px" AutoWidth="false" AutoHeight="false">
                    <Separators Size="10px">
                    </Separators>
                    <PaneStyle BackColor="Transparent"></PaneStyle>
                    <ContentCollection>
                        <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 100%; vertical-align: top;">
                                <tr>
                                    <td align="center" valign="top">
                                        <asp:UpdatePanel ID="pnl2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="center" style="width: 50%">
                                                            <dx:ASPxButton ID="btnMbyllur" runat="server" Text="-" Width="100%" AutoPostBack="false"
                                                                Height="25px" Font-Size="9" Font-Bold="true" ToolTip="Mos shfaq info">
                                                                <ClientSideEvents Click="function (s,e){RuajHapurMbyllurminus(false)}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td align="center" style="width: 50%">
                                                            <dx:ASPxButton ID="btnHapur" runat="server" Text="+" Width="100%" AutoPostBack="false"
                                                                Height="25px" Font-Size="9" ToolTip="Shfaq info">
                                                                <ClientSideEvents Click="function (s,e){RuajHapurMbyllurplus(true)}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>

                                                </table>

                                                <dxnb:ASPxNavBar ID="ASPxNavBar2" runat="server" ClientInstanceName="navbar" Width="100%"
                                                    EnableAnimation="True" SyncSelectionMode="CurrentPath"
                                                    EnableClientSideAPI="True" AllowSelectItem="True" Font-Size="8pt">
                                                    <ClientSideEvents HeaderClick="function (s,e) { HeaderClick (s,e); }" ItemClick="function(s, e) {}"
                                                        ExpandedChanging="function (s,e) {Expanded();  }" />
                                                    <GroupHeaderTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td style="width: 50%; font-weight: bold; height: 12px;">
                                                                    <dx:ASPxLabel Wrap="False" ID="Label1" runat="server" Font-Size="8" Text='<%# Eval("Text") %>' />
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <dx:ASPxHyperLink ID="HyperLink2" runat="server" Text='<%# Eval("Name") %>' NavigateUrl="javascript:void(0)"
                                                                        ImageWidth="12px" EnableClientSideAPI="true" ImageHeight="12px" ImageUrl="~/images/new/flash.png" ClientSideEvents-Click="function (s,e){ ButtonClickNavBar(s);}"
                                                                        ClientSideEvents-Init="function (s,e){ KontrolloTeDrejta(s);}" DisabledStyle-BackColor="#CCCCCC" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </GroupHeaderTemplate>
                                                    <Groups>
                                                        <dxnb:NavBarGroup Text="Info Klient/Furnitori" Expanded="false" Name="KF">
                                                            <ContentTemplate>
                                                                <dx:ASPxListBox ID="lbxKF" Height="100%" runat="server"
                                                                    Width="100%" ClientInstanceName="lbxKF" Font-Size="8">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn FieldName="Emri" Name="Emri" />
                                                                        <dx:ListBoxColumn FieldName="Vlera" Name="Vlera" />
                                                                    </Columns>
                                                                </dx:ASPxListBox>
                                                            </ContentTemplate>
                                                        </dxnb:NavBarGroup>
                                                    </Groups>
                                                </dxnb:ASPxNavBar>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
            </Panes>
             <ClientSideEvents PaneCollapsed="function(s, e) { spliterPaneResized(s,e);}" PaneExpanded="function(s, e) { spliterPaneResized(s,e);}" PaneCollapsing="function(s, e) { spliterPaneCollapsing(s,e);}" PaneExpanding="function(s, e) { spliterPaneExpanding(s,e);}" PaneResized="function(s, e) { spliterPaneResized(s,e);}" />
        </dx:ASPxSplitter>
        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HFStatusiDokumentit" runat="server" />
        <asp:HiddenField ID="hfKontabilizimi" runat="server" />
        <asp:HiddenField ID="hfKonffillestar" runat="server" />
        <asp:HiddenField ID="HfColTrup" runat="server" />
        <asp:HiddenField ID="HfColLlog" runat="server" />
        <asp:HiddenField ID="HfColLlogKunder" runat="server" />
        <asp:HiddenField ID="HfColKF" runat="server" />
        <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
        <asp:HiddenField ID="hfGridaKodi" runat="server" />
        <asp:HiddenField ID="hfGridaDetajimi" runat="server" />
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="pnlLupa" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                    <ClientSideEvents CloseUp="function(s, e) {
	popupUniversal.SetContentUrl('');closePopup(s,e);
}" />
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
