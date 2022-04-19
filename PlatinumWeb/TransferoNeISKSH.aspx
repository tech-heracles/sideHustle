<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferoNeISKSH.aspx.cs" Inherits="PlatinumWeb.TransferoNeISKSH" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/TransferoNeISKSH.aspx-IMB.7.8.js&v76" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top" loadingdivopacity="30"></dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600"></asp:ScriptManager>
        <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" Visible="true" runat="server" OnMenuClick="Menu_ItemClick" OnMenuTemplate="PercaktoTemplateMenu" OnFilterSave="Ruaj_ASPxButton_Click" OnFilterDelete="FshiFilter_ASPxButton_Click" />
        <div>
            <table class="renditKontrolle" hidden="hidden">
                <tbody>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" ClientIDMode="AutoID" Text="Modeli:"></dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth33">
                            <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None" ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top">
                                <ClientSideEvents Init="ndryshoKonfiguriminInit" />
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
            <br />
            <div id="divKontrollet">
                <table id="tblKontrollet" class="renditKontrolleTre">
                    <tbody></tbody>
                </table>
                <dx:ASPxLabel ID="lblKategoria" AssociatedControlID="cmbKategoria" Text="Kategoria " ClientInstanceName="lblKategoria" runat="server" Width="100%"></dx:ASPxLabel>
                <dx:ASPxComboBox ID="cmbKategoria" ClientInstanceName="cmbKategoria" runat="server" Width="100%"></dx:ASPxComboBox>
                <dx:ASPxLabel ID="lblPeriudha" Text="Periudha " AssociatedControlID="dataNga" ClientInstanceName="lblPeriudha" runat="server" Width="100%"></dx:ASPxLabel>
                <dx:ASPxDateEdit ID="dataNga" ClientInstanceName="dataNga" runat="server" Width="100%"></dx:ASPxDateEdit>
                <dx:ASPxLabel ID="lblDataDeri" AssociatedControlID="dataDeri" Text="-" ClientInstanceName="lblDataDeri" runat="server" Width="100%"></dx:ASPxLabel>
                <dx:ASPxDateEdit ID="dataDeri" runat="server" ClientInstanceName="dataDeri" Width="100%"></dx:ASPxDateEdit>
                <dx:ASPxLabel ID="lblPathDbAksesi" AssociatedControlID="txtPathDbAksesi" Text="Vendndodhja e databazes " ClientInstanceName="lblPathDbAksesi" runat="server" Width="100%"></dx:ASPxLabel>
                <dx:ASPxTextBox ID="txtPathDbAksesi" ClientInstanceName="txtPathDbAksesi" runat="server" Width="100%">
                    <ClientSideEvents Init="txtPathDbAksesi_Init" TextChanged="txtPathDbAksesi_TextChanged" />
                </dx:ASPxTextBox>
            </div>
            <br />
            <asp:UpdatePanel ID="pnlKryesor" runat="server">
                <ContentTemplate>
                    <dx:ASPxGridView ID="gv_TransferoNeISKSH" ClientInstanceName="gv_TransferoNeISKSH" OnDataBound="gv_TransferoNeISKSH_DataBound" runat="server" OnHeaderFilterFillItems="gv_TransferoNeISKSH_HeaderFilterFillItems" OnCustomJSProperties="gv_TransferoNeISKSH_CustomJSProperties" OnAfterPerformCallback="gv_TransferoNeISKSH_AfterPerformCallback" OnCustomCallback="gv_TransferoNeISKSH_CustomCallback" Width="100%" SettingsPager-PageSize="20">
                        <Templates>
                        </Templates>
                        <Styles>
                            <Header ImageSpacing="5px" SortingImageSpacing="5px"></Header>
                        </Styles>
                        <SettingsPager PageSize="15"></SettingsPager>
                        <ClientSideEvents BeginCallback="function(s, e) {	BeginCallback(s,e); }" />
                        <StylesEditors>
                            <ProgressBar Height="25px">
                            </ProgressBar>
                        </StylesEditors>
                    </dx:ASPxGridView>
                    <iframe id="Container" runat="server" frameborder="0" name="Container" height="0" width="0"></iframe>
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gv_TransferoNeISKSH" ExportedRowType="Selected" />
            <dx:ASPxButton ID="btnXlsxExportHidden" ClientVisible="False" ClientInstanceName="btnXlsxExportHidden" runat="server" ToolTip="Export to Xlsx" Text="Export to Xlsx" Font-Size="8" UseSubmitBehavior="false" OnClick="btnXlsxExport_Click_Hidden">
                <ClientSideEvents Click="function(s, e) { clickExport(e) }" />
            </dx:ASPxButton>
        </div>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"></dx:ASPxHiddenField>
        <asp:HiddenField ID="hfStatusi" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hfKaGabime" ClientIDMode="Static" runat="server" />
    </form>
</body>
</html>
