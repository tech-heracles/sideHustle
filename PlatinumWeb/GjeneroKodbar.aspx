<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GjeneroKodbar.aspx.cs" Inherits="PlatinumWeb.GjeneroKodbar" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>




<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css"
        runat="server" id="themeJQuery" />
    <link rel="stylesheet" type="text/css" media="screen" href="js/jqGrid445/css/ui.jqgrid.css" />
     <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/aspx.js/GjeneroKodbar.aspx-IMB.2.3.js&v31" type="text/javascript"></script>
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
                <%--<ClientSideEvents EndCallback="function(s,e){ try{ window.parent.SessionTimeout.sendKeepAlive(); } catch(e){}}" />--%>
            </dx:ASPxGlobalEvents>
                <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
    </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                    CssPostfix="Aqua" OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
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
                                                            <%--<dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                OnClick="ButtonOk_Click2" Text="Ok" CssPostfix="Aqua">
                                                                <ClientSideEvents Click="function(s, e) {
	popFshi.Hide();
    Utils.shfaqLoadingGif();;
}" />
                                                            </dx:ASPxButton>--%>
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
                                </dx:ASPxPanel >
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl >
                        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popKodbare" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popKodbare" CloseAction="CloseButton" EnableAnimation="False"
                    EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" Width="300px">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl43" runat="server">
                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel13" runat="server" ClientIDMode="AutoID" Width="271px">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent13" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel Wrap="true" ID="lblMsgbox3" runat="server" ClientIDMode="AutoID" Text="Ka artikuj pa kodbare, doni te vazhdoni?">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonOk3" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk" AutoPostBack="false"
                                                            Text="Po">
                                                            <ClientSideEvents Click="Click_ButtonOk3" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonJo" runat="server" ClientIDMode="AutoID" Text="Jo">
                                                            <ClientSideEvents Click="function(s, e) {
		popKodbare.Hide();
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
                    <div style="visibility: hidden; display: none">
                       <%-- <dx:ASPxButton ID="btnPastro" runat="server" Text="ASPxButton" ClientInstanceName="btnPastro"
                            OnClick="btnPastro_Click">
                        </dx:ASPxButton>--%>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                    <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
                     <asp:HiddenField ID="hfSasia" runat="server" />
                     <asp:HiddenField ID="hfSasia1" runat="server" />
                </ContentTemplate>
                
            </asp:UpdatePanel>
            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Width="100%" Height="670px" ClientInstanceName="splitter"
                CssPostfix="Aqua" PaneMinSize="670px">
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
                                    <table id="tblFillim">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <div id="dvFillim" class="atributeDiveFshehur">
                                        <table class="renditKontrolle">
                                              
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
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtSimboliNdares" ID="lblSimboliNdares" runat="server" ClientInstanceName="lblSimboliNdares"
                                                        Text="Simboli Ndares">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50">
                                                    <dx:ASPxTextBox ID="txtSimboliNdares" runat="server" Width="100%" ClientInstanceName="txtSimboliNdares">
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxCheckBox ID="cbSimboliNdares" runat="server" Text="Tab" ClientInstanceName="cbSimboliNdares"
                                                        TextAlign="Left">
                                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dx:ASPxCheckBox>
                                                </td>
                                                  
                                               

                                              
                                            </tr>
                                           <%-- <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategoria" ID="lblKategoria" runat="server" Text="Kategoria" ClientInstanceName="lblKategoria">
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
                                            </tr>--%>
                                          <%--  <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbFormati" ID="lblFormati" runat="server" Text="Formati" ClientInstanceName="lblFormati">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50">
                                                    <dx:ASPxComboBox ID="cmbFormati" ClientInstanceName="cmbFormati" runat="server" Width="100%"
                                                        CssPostfix="Aqua" ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top">
                                                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickedFormati(s,e)}" LostFocus="function (s,e){MerrFiltraFormati(s,e)}" />
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
                                            </tr>--%>
                                     
                                          
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

                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtGjatesiKodbar" ID="lblGjatesiKodbar" runat="server" ClientInstanceName="lblGjatesiKodbar"
                                                        Text="Gjatesia e kodbarit">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth50">
                                                  <%--  <dx:ASPxTextBox ID="txtGjatesiKodbar" runat="server" Width="100%" ClientInstanceName="txtGjatesiKodbar">
                                                    </dx:ASPxTextBox>--%>


                                                     <dx:ASPxComboBox ID="cmbGjatesiKodbar" runat="server" ClientInstanceName="cmbGjatesiKodbar"
                                    Width="100%" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
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
                                                </td>
                                                  <td class="renditKontrolleCaption">
                                                   <%-- <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbFiltri" ID="lblFiltri" runat="server" Text="Filtri" ClientInstanceName="lblFiltri">
                                                    </dx:ASPxLabel>--%>
                                                </td>
                                                  <td class="renditKontrolleCellMeWidth50">
                                                    <asp:UpdatePanel ID="pnlfiltri" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                     <%--   <dx:ASPxComboBox ID="cmbFiltri" ClientInstanceName="cmbFiltri" runat="server" CssPostfix="Aqua"
                                                                            ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ 
                                                                         if (s.FindItemByText(s.GetInputElement().value) != null) {
                                                                            btnRuaj.SetEnabled(false); btnFshi.SetEnabled(true);
                                                                        gvExport.PerformCallback('Filter;'+s.GetValue());
                                                                        } else if (s.GetText() == '') {
                                                                            btnRuaj.SetEnabled(false);
                                                                            btnFshi.SetEnabled(false);
                                                                        }
                                                                       }"
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
                                                                        </dx:ASPxComboBox>--%>
                                                                    </td>
                                                                    <td>
                                                                 <%--       <dx:ASPxButton ID="btnRuaj" runat="server" Text="" ClientInstanceName="btnRuaj"
                                                                            Image-Url="~/images/new/disk_blue (3).png" OnClick="btnRuaj_Click" Width="10px">
                                                                        </dx:ASPxButton>--%>
                                                                    </td>
                                                                    <td>
                                                                      <%--  <dx:ASPxButton ID="btnFshi" runat="server" Text="" ClientInstanceName="btnFshi"
                                                                            Image-Url="~/images/new/button_cancel-32.png" OnClick="btnFshi_Click" Width="10px">
                                                                        </dx:ASPxButton>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divgride1" style="display: none; overflow: auto;">
                                        <br />
                                        <table cellpadding="0" cellspacing="0" style="margin: 16px 0">
                                            <tr>
                                                <td style="padding-right: 4px">
                                                    <dx:ASPxButton ID="btnSelectAll"  runat="server" Text="Zgjidh te gjitha" UseSubmitBehavior="False"
                                                        AutoPostBack="false"   >
                                                        <ClientSideEvents Click="function(s, e) { gvExport.SelectRows();}  " />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td style="padding-right: 4px">
                                                    <dx:ASPxButton ID="btnUnselectAll" runat="server" Text="Hiq zgjedhjen e te gjithave"
                                                        UseSubmitBehavior="False" AutoPostBack="false" >
                                                        <ClientSideEvents Click="function(s, e) { gvExport.UnselectRows();}" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td style="padding-right: 4px">
                                                    <dx:ASPxButton ID="btnSelectAllOnPage" runat="server" Text="Zgjidh te gjitha ne kete faqe"
                                                        UseSubmitBehavior="False" AutoPostBack="false"     >
                                                        <ClientSideEvents Click="function(s, e) { gvExport.SelectAllRowsOnPage();}" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="btnUnselectAllOnPage" runat="server" Text="Hiq zgjedhjen ne kete faqe"
                                                        UseSubmitBehavior="False" AutoPostBack="false"   >
                                                        <ClientSideEvents Click="function(s, e) { gvExport.UnselectAllRowsOnPage();}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <dx:ASPxGridView ID="gvExport" runat="server" ClientInstanceName="gvExport" Settings-ShowGroupPanel="false"
                                                        Width="100%" CssPostfix="BlackGlass" OnCustomCallback="gvExport_CustomCallback" OnHtmlRowCreated="gvExport_HtmlRowCreated"
                                                        OnDataBound="gvExport_DataBound"    OnCustomJSProperties="gvExport_CustomJSProperties"
                                                        OnAfterPerformCallback="gvExport_AfterPerformCallback" OnSelectionChanged="gvExport_SelectedIndexChanged" SettingsBehavior-ProcessSelectionChangedOnServer="false">
                                                      <ClientSideEvents     SelectionChanged="function (s,e){ Selection();}"      BeginCallback="function(s, e) {  }"
                                                                    EndCallback="function (s,e){ShfaqTeDhenat(); }"  />
                                                         <SettingsPager PageSize="15" >
                            </SettingsPager>
                                                        <Styles CssPostfix="BlackGlass">
                                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                            </Header>
                                                        </Styles>
                                                        <StylesEditors>
                                                            <CalendarHeader Spacing="1px">
                                                            </CalendarHeader>
                                                            <ProgressBar Height="25px">
                                                            </ProgressBar>
                                                        </StylesEditors>
                                                    </dx:ASPxGridView>
                                                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1"    runat="server" GridViewID="gvExport"
                                                        ExportedRowType="Selected">
                                                    </dx:ASPxGridViewExporter>
                                                  
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
                <Styles CssPostfix="Aqua">
                </Styles>
                <Images>
                </Images>
            </dx:ASPxSplitter >
             
            <asp:HiddenField ID="HfKonfAmb" runat="server" />
            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HFStatusiDokumentit" runat="server" />
            <asp:HiddenField ID="hfKolonaGride" runat="server" />
            <asp:HiddenField ID="HfGridCol" runat="server" />
            <asp:HiddenField ID="hfKonffillestar" runat="server" />
            <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
            <asp:HiddenField ID="hfGridaKodi" runat="server" />
            <asp:HiddenField ID="hfGridaDetajimi" runat="server" />
        </div>
        <br />
        <br />
        <div style="visibility: hidden; display: none">
            <dx:ASPxButton ID="btnExporto" runat="server" Text="ASPxButton" ClientInstanceName="btnExporto"
                OnClick="btnExporto_Click">
                <%--  <ClientSideEvents Click="function(s, e) { merrTeDhena(s, e); }" />--%>
            </dx:ASPxButton>
             <dx:ASPxButton ID="btnGjenero" runat="server" Text="ASPxButton" ClientInstanceName="btnGjenero"
                OnClick="gjeneroKodbar_Click">
                
            </dx:ASPxButton>
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
            </dx:ASPxPopupControl >
        </div>
    </form>
</body>
</html>
