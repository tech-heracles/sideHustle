<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_ShperndarjeShpenzimesh.aspx.cs"
    Inherits="PlatinumWeb.Shto_ShperndarjeShpenzimesh" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>
<%@ Register Src="~/ucPopUpUniversal.ascx" TagPrefix="ucPopUp" TagName="popUpUniversal" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="stylesheet" type="text/css" media="screen" href="js/jqGrid445/css/ui.jqgrid.css" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server" id="themeJQuery" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" media="screen" rel="stylesheet" type="text/css" />
    
    <!-- DevExtreme themes -->
    <link rel="stylesheet" type="text/css" href="Content/dx.common.css" />
    <link rel="stylesheet" type="text/css" href="Content/dx.generic.alphaweb-compact.css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />

    <!-- A DevExtreme library -->
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/myMesazh-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/json2.js;~/JsGlobal.js;~/js/myCookies-IMB.2.1.js;~/Scripts/dx.viz-web.js;~/js/localization/DevExtreme.Perkthime.js;~/js/myDxDataGrid.js;~/js/aspx.js/Shto_ShperndarjeShpenzimesh.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
    <style>
        .dx-datagrid-rowsview .dx-row .dx-master-detail-cell {
            padding: 0px 5px 5px 5px;
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
        <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server" OnMenuTemplate="PercaktoTemplateMenu"/>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Red" ClientInstanceName="pergjigja"/>
                <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo" />
                <asp:HiddenField ID="hfUrl" runat="server" />
                <asp:HiddenField ID="status1" runat="server" Value="false" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfLupaFatura" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" ClientInstanceName="splitter"
            PaneMinSize="700px">
            <Panes>
                <%-- Header pane--%>
                <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ScrollBars="Vertical">
                    <Separators Size="10px">
                    </Separators>
                    <PaneStyle>
                    </PaneStyle>
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
                                <br />
                                <div id="dvgrid_faturat">
                                    <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" ClientIDMode="AutoID" Width="100%"
                                        ClientInstanceName="nvFatura" ClientSideEvents-ExpandedChanged="nvFaturaExpanded">
                                        <Groups>
                                            <dx:NavBarGroup Text="Faturat" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px"
                                                HeaderStyle-ForeColor="Gray" Expanded="False">
                                                <HeaderStyle Font-Bold="True" Font-Size="14px" ForeColor="Gray"></HeaderStyle>
                                                <ContentTemplate>
                                                    <table style="table-layout: fixed; width:100%">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <!------Grida e faturave duke perdorur DevExtreme---------->
                                                                    <div id="dxDataGrid_faturat" class ="noUndoGrida"></div>
                                                                    <!------------------------------------------------------->
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </ContentTemplate>
                                            </dx:NavBarGroup>
                                        </Groups>
                                    </dx:ASPxNavBar>
                                </div>
                                <div id="dvFillim" class="atributeDiveFshehur">
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji" runat="server"
                                        ClientInstanceName="lblLloji">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji" ShowShadow="False"
                                        SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){TextChangedLloji();}" />
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                        runat="server" Text="Lloji:" ClientInstanceName="konfigurimi_Label">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%" AnimationType="None">
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrDok" ID="lblNrDok" runat="server"
                                        Text="Nr dokumenti:" ClientInstanceName="lblNrDok">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtNrDok" runat="server" ClientInstanceName="txtNrDok" Width="100%">
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dateDtDok" ID="lblDtDok" runat="server"
                                        Text="Date:" ClientInstanceName="lblDtDok">
                                    </dx:ASPxLabel>
                                    <dx:ASPxDateEdit ID="dateDtDok" runat="server" ClientInstanceName="dateDtDok" ShowShadow="False"
                                        Width="100%">
                                        <ClientSideEvents GotFocus="function(s, e){ dateGotFocus( s, e); }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                        <ClientSideEvents DateChanged="function(s,e){ }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="lblShenime" runat="server"
                                        Text="Shenime" ClientInstanceName="lblShenime">
                                    </dx:ASPxLabel>
                                    <dx:ASPxMemo ID="txtShenime" runat="server" ClientInstanceName="txtShenime" Rows="5"
                                        Columns="22" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ClientSideEvents LostFocus="function(s,e){}" />
                                    </dx:ASPxMemo>
                                    <dx:ASPxRadioButtonList ID="radioShperndaSipas" runat="server" ClientInstanceName="radioShperndaSipas"
                                        TextSpacing="2px">
                                        <Items>
                                            <dx:ListEditItem Text="Shpernda sipas sasise" Value="0" />
                                            <dx:ListEditItem Text="Shpernda sipas vleres" Value="1" Selected="true" />
                                        </Items>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxRadioButtonList>
                                </div>
                                <br />
                                <div id="dvbutonShpernda" style="display: none">
                                    <table width="100%">
                                        <tr align="left">
                                            <td style="width: 40%">
                                                <table width="80%">
                                                    <tr>
                                                        <td class="renditKontrolleCaption">
                                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVlera" ID="lblVlera" runat="server"
                                                                Text="Vlera" ClientInstanceName="lblVlera">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtVlera" runat="server" ClientInstanceName="txtVlera" ValidationSettings-Display="Dynamic"
                                                                Width="100%">
                                                                <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e)}" />
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
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="butonKerko" runat="server" Text="Kerko" AutoPostBack="false"
                                                                CausesValidation="False" ClientInstanceName="butonKerko" Width="68px">
                                                                <ClientSideEvents Click="function(s,e){ButtonClickKerkoFatura();}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 60%">
                                                <table width="100%">
                                                    <tr align="left">
                                                        <td>
                                                            <dx:ASPxButton ID="butonShpernda" runat="server" Text="Shpernda" AutoPostBack="false"
                                                                CausesValidation="False" ClientInstanceName="butonShpernda" Width="170px">
                                                                <ClientSideEvents Click="function(s,e){ButtonClickShpernda();}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <div id="dvgvLlogarite" class="atributeDiveFshehur">
                                    <!------Grida e llogarive duke perdorur DevExtreme---------->
                                    <div class="demo-container">
                                        <div id="data-grid-llogaria">
                                            <div id="dxDataGrid_llogaria" class ="noUndoGrida"></div>
                                        </div>
                                    </div>
                                    <!------------------------------------------------------->
                                </div>
                                <br />
                                <!------Grida e shperndarjes duke perdorur DevExtreme---------->
                                <div class="demo-container">
                                    <div id="data-grid-trupiShpenzimi">
                                        <div id="dxDataGrid_trupiShpenzimi" class ="noUndoGrida"></div>
                                    </div>
                                </div>
                                <!------------------------------------------------------->
                                <br />
                                <table id="tblFund" class="renditKontrolle" align="right">
                                    <tbody>
                                    </tbody>
                                </table>
                                
                                <div id="dvFundi" class="atributeDiveFshehur">
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dateDtRegjistrimi" ID="lblDtRegjistrimi"
                                        runat="server" Text="Dt Regjistrimi:" ClientInstanceName="lblDtRegjistrimi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxDateEdit ID="dateDtRegjistrimi" runat="server" ClientInstanceName="dateDtRegjistrimi"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTotaliSasia" ID="lblTotaliSasia"
                                        runat="server" Text="Sasia" ClientInstanceName="lblTotaliSasia">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtTotaliSasia" runat="server" ClientInstanceName="txtTotaliSasia"
                                        DisplayFormatString="0.00" Width="100%">
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTotaliVleftaPaTVSH" ID="lblTotaliVleftaPaTVSH"
                                        runat="server" Text="Vlefta pa tvsh" ClientInstanceName="lblTotaliVleftaPaTVSH">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtTotaliVleftaPaTVSH" runat="server" ClientInstanceName="txtTotaliVleftaPaTVSH"
                                        Width="100%">
                                        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e)}" />
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
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTotaliVlefteShpenzimi" ID="lblTotaliVlefteShpenzimi"
                                        runat="server" Text="Vlefta e shpenzimit" ClientInstanceName="lblTotaliVlefteShpenzimi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtTotaliVlefteShpenzimi" runat="server" ClientInstanceName="txtTotaliVlefteShpenzimi"
                                        Width="100%">
                                        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e)}" />
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
                                </div>
                                <dx:ASPxHiddenField ID="hfNrAutoShpernd" runat="server" ClientInstanceName="hfNrAutoShpernd">
                                </dx:ASPxHiddenField>
                                <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
                                </dx:ASPxHiddenField>
                                
                            </asp:Panel>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
            </Panes>
        </dx:ASPxSplitter >
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta"/>
        <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri"/>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"/>

        <ucPopUp:popUpUniversal runat="server" ID="ucPopUpUniversal"></ucPopUp:popUpUniversal>
    </form>
</body>
</html>
