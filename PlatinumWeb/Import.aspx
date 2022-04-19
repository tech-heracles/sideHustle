<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="PlatinumWeb.Import" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>
<%@ Register Src="~/ucPopUpUniversal.ascx" TagPrefix="ucPopUpUniversal" TagName="popUpUniversal" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>

    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server" id="themeJQuery" />
    <link rel="stylesheet" type="text/css" media="screen" href="js/jqGrid445/css/ui.jqgrid.css" /> 
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <!-- DevExtreme themes -->
    <link rel="stylesheet" type="text/css" href="Content/dx.common.css" />
    <link rel="stylesheet" type="text/css" href="Content/dx.generic.alphaweb-compact.css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />

    <!-- A DevExtreme library -->
    <script src="DX.ashx?jsfileset=~/Scripts/jquery-3.4.1.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/myNrAuto-IMB.2.1.js;~/Scripts/dx.viz-web.js;~/js/localization/DevExtreme.Perkthime.js;~/js/myDxDataGrid.js;~/js/aspx.js/Import.aspx-IMB.2.3.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">         
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <div style="width: 100%; height: 100%">
            <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000"></asp:ScriptManager>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server"></dx:ASPxGlobalEvents>
            <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState"></dx:ASPxHiddenField>
            <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server" OnMenuClick="Menu_ItemClick" OnMenuTemplate="PercaktoTemplateMenu"/>   
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
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
                                                                <ClientSideEvents Click="Click_ButtonOk" />
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                    <asp:HiddenField ID="indexRreshti" runat="server" Value="false" />
                    <asp:HiddenField ID="hfStatusMesazh" runat="server" />
                    <asp:HiddenField ID="tabHistoriku" runat="server" Value="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="670px" ClientInstanceName="splitter"
                PaneMinSize="670px">
                <Panes>
                    <%-- Header pane--%>
                    <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ScrollBars="Vertical">
                        <Separators Size="10px">
                        </Separators>

                        <PaneStyle BackColor="Transparent"></PaneStyle>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                <asp:Panel ID="ContentPanel" runat="server">
                                    <asp:UpdatePanel ID="pnlLidhur" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table id='hl' runat="server">
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <table id="tblFillim">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <div id="dvFillim" class="atributeDiveFshehur">
                                        <table class="renditKontrolle">
                                            <tr>
                                                <td class="renditKontrolleCaption" colspan="1" style="width:12%">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbEmer" ID="lblEmer" runat="server" Text="Emri" ClientInstanceName="lblEmer">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50" colspan="1" style="width:38%;">
                                                    <asp:UpdatePanel ID="cmbEmer_pnlEmer" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <dx:ASPxComboBox ID="cmbEmer" runat="server" ClientInstanceName="cmbEmer" ShowShadow="False"
                                                                SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                                                <ClientSideEvents SelectedIndexChanged="function(s,e){aplikoFiltra(s,e) ;}" />
                                                                <DropDownButton>
                                                                    <Image>
                                                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                    </Image>
                                                                </DropDownButton>
                                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries"
                                                                    SetFocusOnError="true">
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                                    </ErrorFrameStyle>
                                                                    <RequiredField IsRequired="true" />
                                                                </ValidationSettings>
                                                                <DisabledStyle Font-Bold="False">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="renditKontrolleCaption" colspan="1" style="width:12%">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="rbTipi" ID="lblTipi" runat="server" Text="Tipi">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50" colspan="1" style="width:38%;">
                                                    <dx:ASPxRadioButtonList ID="rbTipi" runat="server" ValueType="System.String" ClientInstanceName="rbTipi"
                                                        Width="100%" RepeatDirection="Horizontal"  TextAlign="Left" Paddings-Padding="0">
                                                        <Items>
                                                            <dx:ListEditItem Text="XLS" Value="XLS" Selected="true" />
                                                            <dx:ListEditItem Text="XLSX" Value="XLSX" Selected="false" />
                                                            <dx:ListEditItem Text="CSV" Value="CSV" Selected="false" />
                                                            <dx:ListEditItem Text="SQL" Value="SQL" Selected="false" />
                                                        </Items>
                                                        <ClientSideEvents SelectedIndexChanged="function (s,e){ rbTipiIndexChanged(s, e);}" />
                                                    </dx:ASPxRadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="renditKontrolleCaption" colspan="1" style="width:12%">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategoria" ID="lblKategoria" runat="server" Text="Kategoria" ClientInstanceName="lblKategoria">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50" colspan="1" style="width:38%;">
                                                    <dx:ASPxComboBox ID="cmbKategoria" ClientInstanceName="cmbKategoria" runat="server"
                                                        ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                                        <ClientSideEvents TextChanged="function(s,e){TextChangedKategoria();}" />

                                                        <SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries"
                                                            ValidateOnLeave="false">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle Font-Bold="False">
                                                        </DisabledStyle>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td class="renditKontrolleCaption" colspan="1" style="width:12%"></td>
                                                <td class="renditKontrolleCellMeWidth50" colspan="1" rowspan="3" style="vertical-align:top; width:38%;">
                                                    <dx:ASPxCheckBox ID="cbTePaImportuara" runat="server" Text="Te paimportuara"
                                                        ClientInstanceName="cbTePaImportuara"
                                                        TextAlign="Left">
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxCheckBox>
                                                    <span style="display:inline-flex; width:100%">
                                                        <dx:ASPxLabel runat="server" ID="lblNrDokumentash" ClientInstanceName="lblNrDokumentash" Text="Numer Dokumentash"            
                                                                        AssociatedControlID="txtNrDokumentash"/>&nbsp;&nbsp;
                                                        <dx:ASPxTextBox ID="txtNrDokumentash" runat="server"
                                                            ClientInstanceName="txtNrDokumentash"
                                                            TextAlign="Left" ClientSideEvents-TextChanged="function(s,e){KontrolloNrDokumentash(s,e);}">
                                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dx:ASPxTextBox>
                                                    </span>
                                                    <dx:ASPxCheckBox ID="cbPermbledhese" runat="server" Text="Gjenero Fature Permbledhese" ClientInstanceName="cbPermbledhese"
                                                        TextAlign="Left">
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxCheckBox>
                                                    <dx:ASPxButton Wrap="False" ID="btnPastroTabelat" ClientInstanceName="btnPastroTabelat" runat="server" 
                                                            Text="Pastro tabelat">
                                                            <ClientSideEvents Click="PastroTabelatTemporare" />
                                                        </dx:ASPxButton>
                                                    <dx:ASPxCheckBox ID="cbTransferoFatura" runat="server" Text="Transfero Fatura" ClientInstanceName="cbTransferoFatura"
                                                        TextAlign="Left">
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxCheckBox>
                                                     <dx:ASPxCheckBox ID="cbRimerrTeImportuara" runat="server" Text="Rimerr fatura te importuara me pare"
                                                        ClientInstanceName="cbRimerrTeImportuara"
                                                        TextAlign="Left">
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxCheckBox>
                                                    <span style="display:inline-flex">
                                                        <dx:ASPxCheckBox ID="cbDergoMeEmail" runat="server" Text=" Dergo me email" ClientInstanceName="cbDergoMeEmail"
                                                            TextAlign="Left">
                                                            <ClientSideEvents CheckedChanged="cbDergoMeEmail_CheckedChanged" />
                                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dx:ASPxCheckBox>
                                                        <dx:ASPxButton Wrap="False" ID="btnKonfiguroEmail" ClientInstanceName="btnKonfiguroEmail" runat="server" 
                                                            Text="Konfiguro e-mail">
                                                            <ClientSideEvents Click="HapLupaKonfigurimEmail" />
                                                        </dx:ASPxButton>
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="renditKontrolleCaption" colspan="1" style="width:12%">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbFormati" ID="lblFormati" runat="server"
                                                        Text="Formati" ClientInstanceName="lblFormati">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50" colspan="1" style="width:38%;">
                                                    <dx:ASPxComboBox ID="cmbFormati" ClientInstanceName="cmbFormati" runat="server"
                                                                     OnItemRequestedByValue="cmbFormati_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbFormati_ItemsRequestedByFilterCondition"  EnableCallbackMode="True"
                                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                                        <ClientSideEvents TextChanged="function(s,e){TextChangedFormati();}" ButtonClick="function(s,e){ButtonClickedFormati(s,e)}" />

                                                        <SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                            ValidationGroup="entries" ValidateOnLeave="false">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                        <DisabledStyle Font-Bold="False">
                                                        </DisabledStyle>
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="renditKontrolleCaption" colspan="1" style="width:12%">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerSheet" ID="lblEmerSheet"
                                                        runat="server" Text="Emri i Sheet-it te Excelit">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50" colspan="1" style="width:38%;">
                                                    <dx:ASPxTextBox ID="txtEmerSheet" runat="server" Width="100%" Theme="Aqua" ClientInstanceName="txtEmerSheet">
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="renditKontrolleCaption" colspan="1" style="width:12%">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="ucEmerSkedari" ID="lblEmerSkedari" ClientInstanceName="lblEmerSkedari"
                                                        runat="server" Text="Emri i Skedarit">
                                                    </dx:ASPxLabel>
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerTabKoka" ID="lblEmerTabKoka"
                                                        runat="server" ClientInstanceName="lblEmerTabKoka" Text="Emri i tabeles se kokes">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50" id="externalDropZone" colspan="1" style="width:38%;">
                                                    <dx:ASPxUploadControl ID="ucEmerSkedari" runat="server" Width="100%" ClientInstanceName="ucEmerSkedari"
                                                        OnFileUploadComplete="ucEmerSkedari_FileUploadComplete" UploadMode="Auto">
                                                        <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="False" EnableMultiSelect="False" 
                                                            ExternalDropZoneID="externalDropZone" DropZoneText=""/>
                                                        <ClientSideEvents FileUploadComplete="function(s, e) { ucEmerSkedariFileUploadComplete(s, e); }" />
                                                        <ValidationSettings AllowedFileExtensions=".xls,.xlsx,.txt,.csv"
                                                            MaxFileSizeErrorText="Skedari ka kaluar madhesine maximale 10MB">
                                                        </ValidationSettings>
                                                    </dx:ASPxUploadControl>
                                                    <dx:ASPxTextBox ID="txtEmerTabKoka" runat="server" Width="100%" ClientInstanceName="txtEmerTabKoka" MaxLength="50">
                                                        <DisabledStyle Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td class="renditKontrolleCaption" colspan="1" style="width:12%">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerTabKokaHistorik" ID="lblEmerTabKokaHistorik"
                                                        runat="server" ClientInstanceName="lblEmerTabKokaHistorik" Text="Emri i tabeles se historikut te kokes">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50" colspan="1" style="width:38%;">
                                                    <dx:ASPxTextBox ID="txtEmerTabKokaHistorik" runat="server" Width="100%" ClientInstanceName="txtEmerTabKokaHistorik" 
                                                                    MaxLength="50" ClientEnabled="false">
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr> 
                                            <tr>
                                                <td class="renditKontrolleCaption" colspan="1" style="width:12%">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerTabTrupi" ID="lblEmerTabTrupi"
                                                        runat="server" ClientInstanceName="lblEmerTabTrupi" Text="Emri i tabeles se trupit">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50" colspan="1" style="width:38%;">
                                                    <dx:ASPxTextBox ID="txtEmerTabTrupi" runat="server" Width="100%" ClientInstanceName="txtEmerTabTrupi" MaxLength="50">
                                                        <DisabledStyle Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td class="renditKontrolleCaption" colspan="1">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerTabTrupiHistorik" ID="lblEmerTabTrupiHistorik"
                                                        runat="server" ClientInstanceName="lblEmerTabTrupiHistorik" Text="Emri i tabeles se historikut te trupit">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50" colspan="1" style="width:38%;">
                                                    <dx:ASPxTextBox ID="txtEmerTabTrupiHistorik" runat="server" Width="100%" ClientInstanceName="txtEmerTabTrupiHistorik" 
                                                                    MaxLength="50" ClientEnabled="false">
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="renditKontrolleCaption" colspan="1">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerTabRec" ID="lblEmerTabRec"
                                                        runat="server" ClientInstanceName="lblEmerTabRec" Text="Emri i tabeles se recepturave">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50" colspan="1" style="width:38%;">
                                                    <dx:ASPxTextBox ID="txtEmerTabRec" runat="server" Width="100%" ClientInstanceName="txtEmerTabRec" MaxLength="50">
                                                        <DisabledStyle Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td class="renditKontrolleCaption" colspan="1" style="width:12%">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerTabRecHistorik" ID="lblEmerTabRecHistorik"
                                                        runat="server" ClientInstanceName="lblEmerTabRecHistorik" Text="Emri i tabeles se historikut te recepturave">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50" colspan="1" style="width:38%;">
                                                    <dx:ASPxTextBox ID="txtEmerTabRecHistorik" runat="server" Width="100%" ClientInstanceName="txtEmerTabRecHistorik" 
                                                                    MaxLength="50" ClientEnabled="false">
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divgride1" style="display: none;">
                                        <br />
                                        <!------Grida e importit duke perdorur DevExtreme---------->
                                        <div class="demo-container">
                                            <div id="data-grid-importi">
                                                <div id="dxDataGrid_Importi" class ="noUndoGrida"></div>
                                            </div>
                                        </div>
                                        <!------------------------------------------------------->
                                    </div>
                                </asp:Panel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
            </dx:ASPxSplitter>
            <asp:HiddenField ID="HfKonfAmb" runat="server" />
            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
            <asp:HiddenField ID="hfIdKonfigImporti" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HFStatusiDokumentit" runat="server" />
            <asp:HiddenField ID="hfKolonaGride" runat="server" />
            <asp:HiddenField ID="HfGridCol" runat="server" />
            <asp:HiddenField ID="hfKonffillestar" runat="server" />
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hfKaVleraTeImportuara" runat="server" />
                    <asp:HiddenField ID="hfMbishkruajVleraImporti" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Header pane--%>
            <asp:HiddenField ID="hfGridaKodi" runat="server" />
            <asp:HiddenField ID="hfGridaDetajimi" runat="server" />
        </div>
        <br />
        <br />
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <div>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                <ClientSideEvents Closing="closing" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
            <iframe id="Container" runat="server" frameborder="0" name="Container" height="0"
                width="0"></iframe>
        </div>
    </form>
</body>
</html>
