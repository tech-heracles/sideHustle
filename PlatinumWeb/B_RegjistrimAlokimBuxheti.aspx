<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="B_RegjistrimAlokimBuxheti.aspx.cs" Inherits="PlatinumWeb.B_RegjistrimAlokimBuxheti" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>
<%@ Register Src="~/ucPopUpUniversal.ascx" TagPrefix="ucPopUp" TagName="popUpUniversal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
	    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/B_RegjistrimAlokimBuxheti.aspx-IMB.7.6.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top" LoadingDivOpacity="30"></dx:ASPxLoadingPanel> 
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600"></asp:ScriptManager>
        <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" Visible="true" runat="server" OnMenuClick="Menu_ItemClick" OnMenuTemplate="PercaktoTemplateMenu" OnFilterSave="Ruaj_ASPxButton_Click" OnFilterDelete="FshiFilter_ASPxButton_Click"/>
        <table class="renditKontrolle">
            <tbody>
                <tr>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" ClientIDMode="AutoID" Text="Modeli:"></dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth33">
                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None" ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top">
                            <ClientSideEvents SelectedIndexChanged="function(s,e){ ndryshoKonfigurimin() }" />
                            <LoadingPanelImage>
                            </LoadingPanelImage>
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
                        <dx:ASPxLabel ID="lblKonfigurimi" runat="server" Text="" BackColor="white" ClientInstanceName="lblKonfigurimi">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth33"></td>
                </tr>
            </tbody>
        </table>        
        <asp:UpdatePanel ID="pnlKryesor" runat="server">
            <ContentTemplate>
                <dx:ASPxGridView ID="gv_dokAlokimBuxheti" ClientInstanceName="gv_dokAlokimBuxheti" runat="server" Width="100%" OnDataBound="gv_dokAlokimBuxheti_DataBound" OnAfterPerformCallback="gv_dokAlokimBuxheti_AfterPerformCallback" OnCustomCallback="gv_dokAlokimBuxheti_CustomCallback"  OnCustomJSProperties="gv_dokAlokimBuxheti_CustomJSProperties" OnHeaderFilterFillItems="gv_dokAlokimBuxheti_HeaderFilterFillItems" SettingsPager-PageSize="20">
                    <Styles>
                        <Header ImageSpacing="5px" SortingImageSpacing="5px"></Header>
                    </Styles>
                    <SettingsPager PageSize="15"></SettingsPager>
                    <ClientSideEvents RowDblClick="function(s, e) { OnGridDoubleClick(s,e,e.visibleIndex); }" BeginCallback="function(s, e) {	BeginCallback(s,e); }"/>
                </dx:ASPxGridView>
                <iframe id="Container" runat="server" frameborder="0" name="Container" height="0" width="0"></iframe>
            </ContentTemplate>
        </asp:UpdatePanel>
        <ucPopUp:popUpUniversal runat="server" ID="ucPopUpUniversal"></ucPopUp:popUpUniversal>
        <asp:UpdatePanel runat="server" id="hiddenFields">
            <ContentTemplate>
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaGjitheDok" runat="server" /> 
                <asp:HiddenField ID="hfStatusi" ClientIDMode="Static" runat="server" />  
                <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"></dx:ASPxHiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
