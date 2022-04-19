<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Eksport.aspx.cs" Inherits="PlatinumWeb.Eksport" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css"
        runat="server" id="themeJQuery" />
    <link rel="stylesheet" type="text/css" media="screen" href="js/jqGrid445/css/ui.jqgrid.css" />


    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/aspx.js/Eksport.aspx-IMB.2.3.js&v76" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" CssPostfix="Aqua" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <div style="width: 100%; height: 100%">
            <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
            </asp:ScriptManager>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            </dx:ASPxGlobalEvents>
            <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                    CssPostfix="Aqua" OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
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
                                <div id="dvMenu" style="display: none">
                                    <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                                BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                                <ClientSideEvents Init="Init_MenuInfo" />
                                                <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <SubMenuStyle GutterWidth="17px" />
                                            </dx:ASPxMenu>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
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
                                                                OnClick="ButtonOk_Click2" Text="Ok" CssPostfix="Aqua">
                                                                <ClientSideEvents Click="Click_ButtonOk" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo"
                                                                CssPostfix="Aqua">
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
                    <div style="visibility: hidden; display: none">
                        <dx:ASPxButton ID="btnPastro" runat="server" Text="ASPxButton" ClientInstanceName="btnPastro"
                            OnClick="btnPastro_Click">
                        </dx:ASPxButton>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <%--            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="670px" ClientInstanceName="splitter"
                CssPostfix="Aqua" PaneMinSize="670px">
                <Panes>
                    
                    <dx:SplitterPane PaneStyle-BackColor="Transparent" Separators-Size="10px" ScrollBars="Vertical">
                        <Separators Size="10px">
                        </Separators>
                        <PaneStyle>
                        </PaneStyle>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                <asp:Panel ID="ContentPanel" runat="server">--%>
            <%-- <asp:UpdatePanel ID="pnlLidhur" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table id='hl' runat="server">
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>--%>
            <%-- <table id="tblFillim">
                <tbody>
                </tbody>
            </table>--%>
            <div id="dvFillim" class="atributeDiveFshehur">
                <table class="renditKontrolle">
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojEksporti" ID="lblLlojEksporti" runat="server"
                                Text="Lloji" ClientInstanceName="lblLlojEksporti">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <dx:ASPxComboBox ID="cmbLlojEksporti" ClientInstanceName="cmbLlojEksporti" runat="server" Width="100%" AutoPostBack="false"
                                Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top">
                                <ClientSideEvents TextChanged="function(s,e){TextChangedLlojEksporti(true);}" />
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
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="rbTipi" ID="lblTipi" ClientInstanceName="lblTipi" runat="server" Text="Tipi">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50" rowspan="3">
                            <dx:ASPxRadioButtonList ID="rbTipi" runat="server" ValueType="System.String" ClientInstanceName="rbTipi"
                                Width="100%">
                                <Items>
                                    <dx:ListEditItem Text="XLS" Value="XLS" Selected="true" />
                                    <dx:ListEditItem Text="XLSX" Value="XLSX" Selected="false" />
                                    <dx:ListEditItem Text="CSV" Value="CSV" Selected="false" />
                                </Items>
                                <ClientSideEvents SelectedIndexChanged="function (s,e){ enabled()}" />
                            </dx:ASPxRadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbEmer" ID="lblEmer" runat="server" Text="Emri" ClientInstanceName="lblEmer">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <asp:UpdatePanel ID="cmbEmer_pnlEmer" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <dx:ASPxComboBox ID="cmbEmer" runat="server" ClientInstanceName="cmbEmer" CssPostfix="Aqua"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%" AutoPostBack="false">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){aplikoFiltra(s,e) ;}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua"
                                                    PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategoria" ID="lblKategoria" runat="server"
                                Text="Kategoria" ClientInstanceName="lblKategoria">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <dx:ASPxComboBox ID="cmbKategoria" ClientInstanceName="cmbKategoria" runat="server" Width="100%"
                                CssPostfix="Aqua" ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top">
                                <ClientSideEvents TextChanged="function(s,e){TextChangedKategoria();}" />
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
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbFormati" ID="lblFormati" runat="server"
                                Text="Formati" ClientInstanceName="lblFormati">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <dx:ASPxComboBox ID="cmbFormati" ClientInstanceName="cmbFormati" runat="server" Width="100%"
                            OnItemRequestedByValue="cmbFormati_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbFormati_ItemsRequestedByFilterCondition" 
                                AutoPostBack="false" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top">
                                <ClientSideEvents ButtonClick="function(s,e){ButtonClickedFormati(s,e)}"
                                    SelectedIndexChanged="function (s,e){MerrFiltraFormati(s,e)}" />
                                <ClientSideEvents TextChanged="function(s,e){TextChangedFormati();}" />
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries" ValidateOnLeave="false">
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
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerSheet" ID="lblEmerSheet" runat="server" ClientInstanceName="lblEmerSheet"
                                Text="Emri i Sheet-it te Excelit">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <dx:ASPxTextBox ID="txtEmerSheet" runat="server" Width="100%" Theme="Aqua" ClientInstanceName="txtEmerSheet">
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerSkedari" ID="lblEmerSkedari" runat="server" ClientInstanceName="lblEmerSkedari"
                                Text="Emri i Skedarit">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <dx:ASPxTextBox ID="txtEmerSkedari" runat="server" Width="100%" ClientInstanceName="txtEmerSkedari">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtFiltriData" ID="lblFiltriData" runat="server" Text="Data" ClientVisible="false" ClientInstanceName="lblFiltriData">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxDateEdit Width="100%" ID="dtFiltriDates" ClientVisible="false" runat="server" ClientInstanceName="dtFiltriDates"
                                            ShowShadow="False">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="None">
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
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtFormatDestinacion" ID="lblFormatDestinacion"
                                runat="server" ClientInstanceName="lblFormatDestinacion" Text="Format destinacion">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <dx:ASPxTextBox ID="txtFormatDestinacion" runat="server" Width="100%" ClientInstanceName="txtFormatDestinacion">
                                <DisabledStyle Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtUrlDestinacion" ID="lblUrlDestinacion"
                                runat="server" ClientInstanceName="lblUrlDestinacion" Text="Url destinacion">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <dx:ASPxTextBox ID="txtUrlDestinacion" runat="server" Width="100%" ClientInstanceName="txtUrlDestinacion">
                                <DisabledStyle Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNdermDestinacion" ID="lblNdermDestinacion"
                                runat="server" ClientInstanceName="lblNdermDestinacion" Text="Kodi i ndermarrjes destinacion">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <dx:ASPxTextBox ID="txtNdermDestinacion" runat="server" Width="100%" ClientInstanceName="txtNdermDestinacion">
                                <DisabledStyle Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerTabKoka" ID="lblEmerTabKoka"
                                runat="server" ClientInstanceName="lblEmerTabKoka" Text="Emri i tabeles se kokes">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <dx:ASPxTextBox ID="txtEmerTabKoka" runat="server" Width="100%" ClientInstanceName="txtEmerTabKoka">
                                <DisabledStyle Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerTabTrupi" ID="lblEmerTabTrupi"
                                runat="server" ClientInstanceName="lblEmerTabTrupi" Text="Emri i tabeles se trupit">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <dx:ASPxTextBox ID="txtEmerTabTrupi" runat="server" Width="100%" ClientInstanceName="txtEmerTabTrupi">
                                <DisabledStyle Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmerTabRec" ID="lblEmerTabRec"
                                runat="server" ClientInstanceName="lblEmerTabRec" Text="Emri i tabeles se recepturave">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <dx:ASPxTextBox ID="txtEmerTabRec" runat="server" Width="100%" ClientInstanceName="txtEmerTabRec">
                                <DisabledStyle Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <dx:ASPxCheckBox ID="cbMerrDokTeMod" runat="server" Text="Shfaq dokumenta te modifikuar"
                                ClientInstanceName="cbMerrDokTeMod"
                                TextAlign="Right" Layout="Flow">
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxCheckBox>


                            <dx:ASPxCheckBox ID="cbMerrDokTeFshire" runat="server" Text="Shfaq dokumenta te fshire"
                                ClientInstanceName="cbMerrDokTeFshire"
                                TextAlign="Right" Layout="Flow">
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxCheckBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divgride1" style="display: none; overflow: auto;">
                <br />
                <table style="margin: 16px 0">
                    <tr>
                        <td style="padding-right: 4px">
                            <dx:ASPxButton ID="btnSelectAll" runat="server" Text="Zgjidh te gjitha" UseSubmitBehavior="False"
                                AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { gvExport.SelectRows(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="padding-right: 4px">
                            <dx:ASPxButton ID="btnUnselectAll" runat="server" Text="Hiq zgjedhjen e te gjithave"
                                UseSubmitBehavior="False" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { gvExport.UnselectRows(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="padding-right: 4px">
                            <dx:ASPxButton ID="btnSelectAllOnPage" runat="server" Text="Zgjidh te gjitha ne kete faqe"
                                UseSubmitBehavior="False" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { gvExport.SelectAllRowsOnPage(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnUnselectAllOnPage" runat="server" Text="Hiq zgjedhjen ne kete faqe"
                                UseSubmitBehavior="False" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { gvExport.UnselectAllRowsOnPage(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td class="renditKontrolleCaption">
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbFiltri" ID="lblFiltri" runat="server" Text="Filtri" ClientInstanceName="lblFiltri">
                            </dx:ASPxLabel>
                        </td>
                        <td class="renditKontrolleCellMeWidth50">
                            <asp:UpdatePanel ID="pnlfiltri" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxComboBox ID="cmbFiltri" ClientInstanceName="cmbFiltri" runat="server" CssPostfix="Aqua"
                                                    ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                                    <ClientSideEvents SelectedIndexChanged="Selected_IndexChanged"
                                                        KeyUp="myMenu.checkText" Init="myMenu.textChanged" />
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
                                                <dx:ASPxFilterControl ID="filterControlGvExport" runat="server" ClientInstanceName="filterControlGvExport" ClientVisible="false">
                                                </dx:ASPxFilterControl>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnRuaj" runat="server" Text="" ClientInstanceName="btnRuaj"
                                                    Image-Url="~/images/new/disk_blue (3).png" OnClick="btnRuaj_Click" Width="10px">
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnFshi" runat="server" Text="" ClientInstanceName="btnFshi"
                                                    Image-Url="~/images/new/button_cancel-32.png" OnClick="btnFshi_Click" Width="10px">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>

                <%-- <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                     <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                    <ContentTemplate>--%>

                <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: top">
                            <dx:ASPxGridView ID="gvExport" runat="server" ClientInstanceName="gvExport" Settings-ShowGroupPanel="false"
                                Width="100%" OnCustomCallback="gvExport_CustomCallback"
                                OnDataBound="gvExport_DataBound" OnCustomJSProperties="gvExport_CustomJSProperties"
                                OnAfterPerformCallback="gvExport_AfterPerformCallback">
                                <ClientSideEvents BeginCallback ="function(s,e){ BeginCallBackGrida(s, e);}" EndCallback="function(s,e){ EndCallbackGrida(s, e);}"
                                    CallbackError="Callback_Error" />
                                <StylesEditors>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1"
                                OnRenderBrick="ASPxGridViewExporter1_RenderBrick" runat="server" GridViewID="gvExport"
                                ExportSelectedRowsOnly="true">
                            </dx:ASPxGridViewExporter>
                        </td>
                    </tr>
                </table>
            </div>
            <%-- </asp:Panel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
                <Styles CssPostfix="Aqua">
                </Styles>
                <Images>
                </Images>
            </dx:ASPxSplitter>--%>
            <%--<asp:HiddenField ID="HfKonfAmb" runat="server" />--%>
            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
            <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
            <%--<asp:HiddenField ID="HFStatusiDokumentit" runat="server" />--%>
            <%--<asp:HiddenField ID="hfKolonaGride" runat="server" />--%>
            <%--<asp:HiddenField ID="HfGridCol" runat="server" />--%>
            <%--<asp:HiddenField ID="hfKonffillestar" runat="server" />--%>
            <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
            <%--<asp:HiddenField ID="hfGridaKodi" runat="server" />--%>
            <%--<asp:HiddenField ID="hfGridaDetajimi" runat="server" />--%>
        </div>
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
        </div>
        <div style="visibility: hidden; display: none">
            <dx:ASPxButton ID="btnExporto" runat="server" Text="ASPxButton" ClientInstanceName="btnExporto"
                OnClick="btnExporto_Click">
            </dx:ASPxButton>
        </div>
    </form>
</body>
</html>
