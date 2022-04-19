<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaArtikullShpejte.aspx.cs"
    Inherits="PlatinumWeb.LupaArtikullShpejte" EnableEventValidation="false" ValidateRequest="false"
    ViewStateEncryptionMode="Never" %>

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
    <link href="js/css/le-frog/jquery-ui.css" media="screen" rel="stylesheet" type="text/css"
        runat="server" id="themeJQuery" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/jquery.ui.datepicker-sq.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/LupaArtikullShpejte.aspx-IMB.2.2.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <div>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            </dx:ASPxGlobalEvents>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <dx:ASPxHiddenField ID="hfKushtet" runat="server">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfState" runat="server">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfVeprimi" runat="server">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfTeDrejtaCmimi" runat="server" ClientInstanceName="hfTeDrejtaCmimi">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
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

                    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel"
                        Font-Size="9pt" Modal="True" ImagePosition="Top">
                        <LoadingDivStyle Opacity="30">
                        </LoadingDivStyle>
                    </dx:ASPxLoadingPanel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="dvArtikulli" style="display: none;">
                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server">
                    <PanelCollection>
                        <dx:PanelContent>
                            <table class="renditKontrolleTre">
                                <tbody>
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                runat="server" ClientIDMode="AutoID" Text="Modeli">
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
                                        <td class="renditKontrolleCellMeWidth33"></td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <table id="tblInformacion" class="renditKontrolle">
                                <tbody>
                                </tbody>
                            </table>
                            <%--<div id="dvlblKodi">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                Text="Kodi:" ClientInstanceName="lblKodi">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtKodi">--%>
                            <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi">
                                <ClientSideEvents TextChanged="function(s, e) {}"
                                    Init="function(s, e) { s.Focus(); }" />
                                <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="True"
                                    Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 50 karaktere" ValidationExpression="^[\s\S]{0,50}$"></RegularExpression>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                            <%--</div>--%>
                            <%-- <div id="dvlblMagazina">--%>
                            <dx:ASPxLabel Wrap="False" ID="lblMagazina" AssociatedControlID="btneMagazina" runat="server"
                                Text="Magazina" ClientInstanceName="lblMagazina">
                            </dx:ASPxLabel>
                            <%--  </div>--%>
                            <%--    <div id="dvbtnMagazina">--%>
                            <dx:ASPxComboBox ID="btnMagazina" Width="100%" runat="server" ClientInstanceName="btnMagazina"
                                ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                <ClientSideEvents ButtonClick="function(s,e){ButtonClickMagazina();}" />
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidateOnLeave="false"
                                    ValidationGroup="entries1">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                    <RequiredField IsRequired="True"></RequiredField>
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                            <%--   </div> --%>
                            <%--<div id="dvlblPershkrimi">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi" ID="lblPershkrimi"
                                runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtPershkrimi">--%>
                            <dx:ASPxMemo ID="txtPershkrimi" runat="server" Width="100%" AutoPostBack="false"
                                ClientInstanceName="txtPershkrimi" Rows="3">
                                <ClientSideEvents TextChanged="function(s, e) {
	                                            
                                                    }       " />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                    ValidationGroup="entries" SetFocusOnError="True">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>

                            <%--<div id="dvlblPershkrimi2">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimiAng" ID="lblPershkrimiAng"
                                runat="server" Text="Pershkrimi 2:" ClientInstanceName="lblPershkrimiAng">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtPershkrimi">--%>
                            <dx:ASPxMemo ID="txtPershkrimiAng" runat="server" Width="100%" AutoPostBack="false"
                                ClientInstanceName="txtPershkrimiAng" Rows="3">
                                <ClientSideEvents TextChanged="function(s, e) {
	                                            
                                                    }       " />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                    ValidationGroup="entries" SetFocusOnError="True">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cbRezervueshem" ID="lblRezervueshem"
                                runat="server" Text="I rezervueshem:" ClientInstanceName="lblRezervueshem">
                            </dx:ASPxLabel>

                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodiIBarit" ID="lblKodiIBarit" runat="server" Text="Kodi i barit:" ClientInstanceName="lblKodiIBarit">
                            </dx:ASPxLabel>
                            <dx:ASPxTextBox ID="txtKodiIBarit" runat="server" Width="100%" ClientInstanceName="txtKodiIBarit">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>

                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cbIRimbursueshem" ID="lblIRimbursueshem" runat="server" Text="I rimbursueshem:" ClientInstanceName="lblIRimbursueshem">
                            </dx:ASPxLabel>
                            <dx:ASPxCheckBox ID="cbIRimbursueshem" runat="server" Width="100%" ClientInstanceName="cbIRimbursueshem">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                    ValidationGroup="entries" SetFocusOnError="true">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxCheckBox>

                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cbDetajim" ID="lblDetajim" runat="server"
                                Text="Detajim:" ClientInstanceName="lblDetajim">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvcbDetajim">--%>
                            <dx:ASPxCheckBox ID="cbDetajim" runat="server" ClientInstanceName="cbDetajim">
                                <ClientSideEvents CheckedChanged="function(s, e) { kontrolloDetajimLidhur(); }" />
                            </dx:ASPxCheckBox>
                            <%--</div>--%>
                            <%--<div id="dvlblKategoriDetajimi">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategoriDetajimi1" ID="lblKategoriDetajimi1"
                                runat="server" ClientInstanceName="lblKategoriDetajimi1" Text="Kategoria e detajimit">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvcmbKategoriDetajimi">--%>
                            <dx:ASPxComboBox ID="cmbKategoriDetajimi1" runat="server" ClientInstanceName="cmbKategoriDetajimi1"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" ValueType="System.String"
                                Width="100%">
                                <ClientSideEvents SelectedIndexChanged="function (s,e){ KategoriaChanged(1);}" />
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                            <%--</div>--%>
                            <%--<div id="dvlblKategoriDetajimi">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategoriDetajimi2" ID="lblKategoriDetajimi2"
                                runat="server" ClientInstanceName="lblKategoriDetajimi2" Text="Kategoria e detajimit 2: ">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvcmbKategoriDetajimi">--%>
                            <dx:ASPxComboBox ID="cmbKategoriDetajimi2" runat="server" ClientInstanceName="cmbKategoriDetajimi2"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" ValueType="System.String"
                                Width="100%">
                                <ClientSideEvents SelectedIndexChanged="function (s,e){ KategoriaChanged(2);}" />
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                            <%--</div>--%>
                            <%--<div id="dvlblDetajimi">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btnDetajimi1" ID="lblDetajimi1"
                                runat="server" Text="Detajimi 1:" ClientInstanceName="lblDetajimi1">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvbtnDetajim1Nga">--%>
                            <dx:ASPxButtonEdit ID="btnDetajimi1" runat="server" ClientInstanceName="btnDetajimi1"
                                ReadOnly="True" Width="100%">
                                <ClientSideEvents ButtonClick="function(s, e) { DetajimArtikulli_Click(s); }" />
                                <Buttons>
                                    <dx:EditButton>
                                    </dx:EditButton>
                                </Buttons>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxButtonEdit>
                            <%--</div>--%>
                            <%--<div id="dvlblDetajimi2">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btnDetajimi2" ID="lblDetajimi2"
                                runat="server" Text="Detajimi 2:" ClientInstanceName="lblDetajimi2">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvbtnDetajim2Nga">--%>
                            <dx:ASPxButtonEdit ID="btnDetajimi2" runat="server" ClientInstanceName="btnDetajimi2"
                                ReadOnly="True" Width="100%">
                                <ClientSideEvents ButtonClick="function(s, e) { DetajimArtikulli_Click(s); }" />
                                <Buttons>
                                    <dx:EditButton>
                                    </dx:EditButton>
                                </Buttons>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxButtonEdit>
                            <%--</div>--%>
                            <%--<div id="dvlbtxtPershkrimiFurnitori">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimiFurnitori" ID="lblPershkrimiFurnitori"
                                runat="server" Text="Pershkrimi te furnitori:" ClientInstanceName="lblPershkrimiFurnitori">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtPershkrimiFurnitori">--%>
                            <dx:ASPxMemo ID="txtPershkrimiFurnitori" runat="server" Width="100%" Rows="3" AutoPostBack="false"
                                ClientInstanceName="txtPershkrimiFurnitori">
                                <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                            <%--</div>--%>

                            <%--<div id="dvlbtxtSiperfaqjaM2">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtSiperfaqjaM2" ID="lblSiperfaqjaM2"
                                runat="server" Text="Sipërfaqja në m2:" ClientInstanceName="lblSiperfaqjaM2">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtSiperfaqjaM2">--%>
                            <dx:ASPxMemo ID="txtSiperfaqjaM2" runat="server" Width="100%" Rows="3" AutoPostBack="false"
                                ClientInstanceName="txtSiperfaqjaM2">
                                <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                            <%--</div>--%>
                            <%--<div id="dvlbtxtNrKontrate">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrKontrate" ID="lblNrKontrate"
                                runat="server" Text="Nr. Kontrate:" ClientInstanceName="lblNrKontrate">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtNrKontrate">--%>
                            <dx:ASPxMemo ID="txtNrKontrate" runat="server" Width="100%" Rows="3" AutoPostBack="false"
                                ClientInstanceName="txtNrKontrate">
                                <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                            <%--</div>--%>

                            <%--<div id="dvlbtxtNrPasurie">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrPasurie" ID="lblNrPasurie"
                                runat="server" Text="Nr. Pasurie:" ClientInstanceName="lblNrPasurie">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtNrPasurie">--%>
                            <dx:ASPxMemo ID="txtNrPasurie" runat="server" Width="100%" Rows="3" AutoPostBack="false"
                                ClientInstanceName="txtNrPasurie">
                                <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                            <%--</div>--%>
                            <%--<div id="dvlbtxtZonaKadastrale">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtZonaKadastrale" ID="lblZonaKadastrale"
                                runat="server" Text="Zona Kadastrale:" ClientInstanceName="lblZonaKadastrale">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtZonaKadastrale">--%>
                            <dx:ASPxMemo ID="txtZonaKadastrale" runat="server" Width="100%" Rows="3" AutoPostBack="false"
                                ClientInstanceName="txtZonaKadastrale">
                                <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                            <%--</div>--%>

                            <%--<div id="dvlbtxtShasia">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShasia" ID="lblShasia"
                                runat="server" Text="Shasia:" ClientInstanceName="lblShasia">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtShasia">--%>
                            <dx:ASPxMemo ID="txtShasia" runat="server" Width="100%" Rows="3" AutoPostBack="false"
                                ClientInstanceName="txtShasia">
                                <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                            <%--</div>--%>

                            <%--<div id="dvlbtxtMarka">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMarka" ID="lblMarka"
                                runat="server" Text="Marka:" ClientInstanceName="lblMarka">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtMarka">--%>
                            <dx:ASPxMemo ID="txtMarka" runat="server" Width="100%" Rows="3" AutoPostBack="false"
                                ClientInstanceName="txtMarka">
                                <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                            <%--</div>--%>
                            <%--<div id="dvlbtxtModeli">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtModeli" ID="lblModeli"
                                runat="server" Text="Modeli:" ClientInstanceName="lblModeli">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtModeli">--%>
                            <dx:ASPxMemo ID="txtModeli" runat="server" Width="100%" Rows="3" AutoPostBack="false"
                                ClientInstanceName="txtModeli">
                                <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                            <%--</div>--%>
                            <%--<div id="dvlbtxtVitProdhimi">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVitProdhimi" ID="lblVitProdhimi"
                                runat="server" Text="Vit prodhimi:" ClientInstanceName="lblVitProdhimi">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtVitProdhimi">--%>
                            <dx:ASPxMemo ID="txtVitProdhimi" runat="server" Width="100%" Rows="3" AutoPostBack="false"
                                ClientInstanceName="txtVitProdhimi">
                                <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                            <%--</div>--%>
                            <%--<div id="dvlbtxtTeDhenaTeknika">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTeDhenaTeknika" ID="lblTeDhenaTeknika"
                                runat="server" Text="Të dhena teknika:" ClientInstanceName="lblTeDhenaTeknika">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtTeDhenaTeknika">--%>
                            <dx:ASPxMemo ID="txtTeDhenaTeknika" runat="server" Width="100%" Rows="3" AutoPostBack="false"
                                ClientInstanceName="txtTeDhenaTeknika">
                                <ClientSideEvents TextChanged="function(s, e) {
													}       " />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                            <%--</div>--%>
                            <%--<div id="dvlblPerPershore">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cbPerPershore" ID="lblPerPershore" runat="server"
                                Text="Per Peshore:" ClientInstanceName="lblPerPershore">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvcbPerPershore">--%>
                            <dx:ASPxCheckBox ID="cbPerPershore" runat="server" ClientInstanceName="cbPerPershore" Width="100%">
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxCheckBox>
                            <%--</div>--%>
                            <%--<div id="dvcbProdhimMePorosi">--%>
                            <dx:ASPxCheckBox ID="cbRezervueshem" runat="server" ClientInstanceName="cbRezervueshem">
                            </dx:ASPxCheckBox>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cbPerTransferim" ID="lblPerTransferim"
                                runat="server" Text="Artikull per transferim:" ClientInstanceName="lblPerTransferim">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvcbProdhimMePorosi">--%>
                            <dx:ASPxCheckBox ID="cbPerTransferim" runat="server" ClientInstanceName="cbPerTransferim">
                            </dx:ASPxCheckBox>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cbLoan" ID="lblLoan"
                                runat="server" Text="Loan:" ClientInstanceName="lblLoan">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvcbProdhimMePorosi">--%>
                            <dx:ASPxCheckBox ID="cbLoan" runat="server" ClientInstanceName="cbLoan">
                            </dx:ASPxCheckBox>
                            <%--</div>--%>
                            <%--<div id="dvlblKodbari">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKodbari" ID="lblKodbari" runat="server"
                                Text="Kodbari:" ClientInstanceName="lblKodbari">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvbtneKodbari">--%>
                            <dx:ASPxButtonEdit ID="btneKodbari" runat="server" ClientInstanceName="btneKodbari"
                                Width="100%">
                                <ClientSideEvents ButtonClick="function(s, e) {
Kodbare_Click();	
}"
                                    TextChanged="function(s, e) {textChangedKodbari(s,e);}" />
                                <Buttons>
                                    <dx:EditButton>
                                    </dx:EditButton>
                                </Buttons>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="false"
                                    ValidationGroup="entries1">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxButtonEdit>
                            <%--</div>--%>
                            <%--<div id="dvlblMetode">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMetode" ID="lblMetode" runat="server"
                                Text="Metode Kostoje:" ClientInstanceName="lblMetode">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvcmbMetode">--%>
                            <dx:ASPxComboBox ID="cmbMetode" runat="server" ClientInstanceName="cmbMetode" ShowShadow="False"
                                ReadOnly="false" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                <ClientSideEvents TextChanged="function(s, e) {
	var text=s.GetText();
    s.SetText(text.split(';')[0]);
   
}"
                                    EndCallback="function(s, e) {
	if(cmbMetode.GetEnabled()==false)
  { cmbMetode .HideDropDown()
  }
  }" />
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
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
                            <%--<div id="dvlblKlasa">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKlasa" ID="lblKlasa" runat="server"
                                Text="Klasa:" ClientInstanceName="lblKlasa">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvcmbKlasa">--%>
                            <dx:ASPxComboBox ID="cmbKlasa" runat="server" ClientInstanceName="cmbKlasa" ShowShadow="False"
                                SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {cmbKlasaIndexChanged(s,e);}" />
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                            <%--</div>--%>
                            <%--<div id="dvlblSkema">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btneSkema" ID="lblSkema" runat="server"
                                Text="Skema:" ClientInstanceName="lblSkema">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvbtneSkema">--%>
                            <dx:ASPxComboBox ID="btneSkema" runat="server" ClientInstanceName="btneSkema" EnableCallbackMode="True"
                                EnableSynchronization="True" OnItemRequestedByValue="btneSkema_ItemRequestedByValue" 
                                OnItemsRequestedByFilterCondition="btneSkema_ItemsRequestedByFilterCondition"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                <ClientSideEvents ButtonClick="function(s, e) {Skema_Click();}"
                                    TextChanged="function(s, e) {btneSkemaTextChanged(s,e); }"
                                    LostFocus="function(s, e) {kontrolloSkema(); }"
                                    EndCallback="function(s, e) {if(btneSkema.GetEnabled()==false) { btneSkema .HideDropDown()}}"
                                    BeginCallback="function(s, e) {if(btneSkema.GetEnabled()==false){ btneSkema .HideDropDown()}}" />
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries"
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
                            <%--<div id="dvlblProdhimMePorosi">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cbProdhimMePorosi" ID="lblProdhimMePorosi"
                                runat="server" Text="Prodhim me porosi:" ClientInstanceName="lblProdhimMePorosi">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvcbProdhimMePorosi">--%>
                            <dx:ASPxCheckBox ID="cbProdhimMePorosi" runat="server" ClientInstanceName="cbProdhimMePorosi"
                                Width="100%">
                            </dx:ASPxCheckBox>
                            <%--</div>--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogInv" ID="lblLlogInv" runat="server"
                                Text="Llogari Inventar:" ClientInstanceName="lblLlogInv">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="btneLlogInv" runat="server" ClientInstanceName="btneLlogInv"
                                EnableCallbackMode="True" OnItemRequestedByValue="btneLlogInv_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogInv_ItemsRequestedByFilterCondition"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                <ClientSideEvents ButtonClick="function(s, e) {
 txtLlog.SetText('inv');
 LlogariI_Click();
}"
                                    Init="function(s, e) {
	
}"
                                    EndCallback="function(s, e) {
	if(btneLlogInv.GetEnabled()==false)
  { btneLlogInv .HideDropDown()
  }
}" />
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


                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btnLlogPakesim" ID="lblLlogPakesim" runat="server"
                                Text="Llogari pakesim vlere dalje:" ClientInstanceName="lblLlogPakesim">
                            </dx:ASPxLabel>



                            <dx:ASPxComboBox ID="btnLlogPakesim" runat="server" ClientInstanceName="btnLlogPakesim"
                                EnableCallbackMode="True" OnItemRequestedByValue="btnLlogPakesim_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btnLlogPakesim_ItemsRequestedByFilterCondition"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                <ClientSideEvents ButtonClick="function(s, e) {

 txtLlog.SetText('pakesim');
LlogariPakesim_Click();
}"
                                    Init="function(s, e) {
	
}"
                                    EndCallback="function(s, e) {
	if(btnLlogPakesim.GetEnabled()==false)
  { btnLlogPakesim .HideDropDown()
  }
}" />
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

                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogBle" ID="lblLlogBle" runat="server"
                                Text="Llogari blerje:" ClientInstanceName="lblLlogBle">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="btneLlogBle" runat="server" ClientInstanceName="btneLlogBle"
                                EnableCallbackMode="True" OnItemRequestedByValue="btneLlogBle_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogBle_ItemsRequestedByFilterCondition"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                <ClientSideEvents ButtonClick="function(s, e) {

 txtLlog.SetText('ble');
 LlogariB_Click();
}"
                                    Init="function(s, e) {
	
}"
                                    EndCallback="function(s, e) {
	if(btneLlogBle.GetEnabled()==false)
  { btneLlogBle .HideDropDown()
  }
}" />
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
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogShit" ID="lblLlogShit" runat="server"
                                Text="Llogari Shitje:" ClientInstanceName="lblLlogShit">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="btneLlogShit" runat="server" ClientInstanceName="btneLlogShit"
                                EnableCallbackMode="True" OnItemRequestedByValue="btneLlogShit_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogShit_ItemsRequestedByFilterCondition"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                <ClientSideEvents ButtonClick="function(s, e) {

 txtLlog.SetText('shit');
 LlogariS_Click();
}"
                                    Init="function(s, e) {

}"
                                    EndCallback="function(s, e) {
	if(btneLlogShit.GetEnabled()==false)
  { btneLlogShit.HideDropDown()
  }
}" />
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
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btneLlogTretet" ID="lblLlogTretet"
                                runat="server" Text="Llogari tek te tretet:" ClientInstanceName="lblLlogTretet">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="btneLlogTretet" runat="server" ClientInstanceName="btneLlogTretet"
                                EnableCallbackMode="True" OnItemRequestedByValue="btneLlogTretet_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogTretet_ItemsRequestedByFilterCondition"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                <ClientSideEvents ButtonClick="function(s, e) {

 txtLlog.SetText('tretet');
LlogariT_Click();
}"
                                    Init="function(s, e) {
	
}"
                                    EndCallback="function(s, e) {
	if(btneLlogTretet.GetEnabled()==false)
  { btneLlogTretet .HideDropDown()
  }
}" />
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
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btnLlogShpe" ID="lblLlogShpe" runat="server"
                                Text="Llogari Shpenzim:" ClientInstanceName="lblLlogShpe">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="btnLlogShpe" runat="server" ClientInstanceName="btnLlogShpe"
                                EnableCallbackMode="True" OnItemRequestedByValue="btnLlogShpe_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btnLlogShpe_ItemsRequestedByFilterCondition"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                <ClientSideEvents ButtonClick="function(s, e) {

 txtLlog.SetText('shpe');
LlogariShp_Click();
}"
                                    Init="function(s, e) {
	
}"
                                    EndCallback="function(s, e) {
	if(btnLlogShpe.GetEnabled()==false)
  { btnLlogShpe .HideDropDown()
  }
}" />
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
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlogAmortizimi" ID="lblLlogAmortizimi"
                                runat="server" Text="Llogari Amortizimi:" ClientInstanceName="lblLlogAmortizimi">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="cmbLlogAmortizimi" runat="server" ClientInstanceName="cmbLlogAmortizimi"
                                EnableCallbackMode="True" OnItemRequestedByValue="cmbLlogAmortizimi_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbLlogAmortizimi_ItemsRequestedByFilterCondition"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                <ClientSideEvents ButtonClick="function(s, e) {txtLlog.SetText('amor');LlogariAmor_Click();}"
                                    Init="function(s, e) {}"
                                    EndCallback="function(s, e) {if(cmbLlogAmortizimi.GetEnabled()==false){ cmbLlogAmortizimi.HideDropDown()}}" />
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
                            <br />
                            <dx:ASPxHiddenField runat="server" ID="HfArtPerbLloji" ClientInstanceName="HfArtPerbLloji">
                            </dx:ASPxHiddenField>
                            <div id="divgride1" style="display: none">
                                <div id="divgride2">
                                    <table id="rowed5">
                                    </table>
                                </div>
                            </div>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNivelTvsh" ID="lblNivelTvsh"
                                runat="server" Text="Nivel TVSH-je:" ClientInstanceName="lblNivelTvsh">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="cmbNivelTvsh" runat="server" ClientInstanceName="cmbNivelTvsh"
                                Width="100%" OnPreRender="cmbNivelTvsh_PreRender" ShowShadow="False" ValueType="System.Int32"
                                OnItemRequestedByValue="cmbNivelTvsh_ItemRequestedByValue" SettingsLoadingPanel-ImagePosition="Top">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { ndryshoTvsh();}" />
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
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNjesia1" ID="lblNjesia1" runat="server"
                                Text="Njesia 1:" ClientInstanceName="lblNjesia1">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="cmbNjesia1" runat="server" ClientInstanceName="cmbNjesia1"
                                ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                Width="100%">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {cmbNjesia2.SetText(cmbNjesia1.GetText());}"
                                    EndCallback="function(s, e) {if(cmbNjesia1.GetEnabled()==false){ cmbNjesia1 .HideDropDown()}}"
                                    BeginCallback="function(s, e) {if(cmbNjesia1.GetEnabled()==false){ cmbNjesia1 .HideDropDown()}}" />
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                    SetFocusOnError="True" ValidationGroup="entries">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVendodhja" ID="lblVendodhja"
                                runat="server" Text="Vendodhja:" ClientInstanceName="lblVendodhja">
                            </dx:ASPxLabel>
                            <dx:ASPxTextBox ID="txtVendodhja" runat="server" Width="100%" ClientInstanceName="txtVendodhja">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                    ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="false" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNjesia2" ID="lblNjesia2" runat="server"
                                Text="Njesia 2:" ClientInstanceName="lblNjesia2">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="cmbNjesia2" runat="server" ClientInstanceName="cmbNjesia2"
                                ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                Width="100%">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
}"
                                    EndCallback="function(s, e) {
	if(cmbNjesia2.GetEnabled()==false)
  { cmbNjesia2 .HideDropDown()
  }
}"
                                    BeginCallback="function(s, e) {
	if(cmbNjesia2.GetEnabled()==false)
  { cmbNjesia2 .HideDropDown()
  }
}" />
                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                    SetFocusOnError="True" ValidationGroup="entries">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKoeficienti" ID="lblKoeficienti"
                                runat="server" Text="Koeficienti:" ClientInstanceName="lblKoeficienti">
                            </dx:ASPxLabel>
                            <dx:ASPxTextBox ID="txtKoeficienti" runat="server" ClientInstanceName="txtKoeficienti"
                                Text="1" Width="100%">
                                <ClientSideEvents LostFocus="function(s, e) {
	kontrolloNjesi();
}" />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                    SetFocusOnError="True" ValidationGroup="entries">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ErrorText="Lejohen vetem numra!" ValidationExpression="[0-9,.]*" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPeshaBruto" ID="lblPeshaBruto"
                                runat="server" Text="Pesha Bruto:" ClientInstanceName="lblPeshaBruto">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtPeshaBruto">--%>
                            <dx:ASPxTextBox ID="txtPeshaBruto" runat="server" ClientInstanceName="txtPeshaBruto"
                                Width="100%">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                    SetFocusOnError="True" ValidationGroup="entries">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ErrorText="Lejohen vetem numra!" ValidationExpression="[0-9,.]*" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                            <%--</div>--%>
                            <%--<div id="dvlblPeshaNeto">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPeshaNeto" ID="lblPeshaNeto"
                                runat="server" Text="Pesha Neto:" ClientInstanceName="lblPeshaNeto">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtPeshaNeto">--%>
                            <dx:ASPxTextBox ID="txtPeshaNeto" runat="server" ClientInstanceName="txtPeshaNeto"
                                Width="100%">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                    SetFocusOnError="True" ValidationGroup="entries">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ErrorText="Lejohen vetem numra!" ValidationExpression="[0-9,.]*" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMinimumi" ID="lblMinimum" runat="server"
                                Text="Minimumi:" ClientInstanceName="lblMinimum">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtMinimumi">--%>
                            <dx:ASPxTextBox ID="txtMinimumi" runat="server" Width="100%" ClientInstanceName="txtMinimumi">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                    ValidationGroup="entries" SetFocusOnError="true">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                            <%--</div>--%>
                            <%--<div id="dvlblMaksimum">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMaximumi" ID="lblMaksimum" runat="server"
                                Text="Maksimumi:" ClientInstanceName="lblMaksimum">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtMaximumi">--%>
                            <dx:ASPxTextBox ID="txtMaximumi" runat="server" Width="100%" ClientInstanceName="txtMaximumi">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                    ValidationGroup="entries" SetFocusOnError="true">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cbMeSerial" ID="lblMeSerial" runat="server"
                                Text="Me serial:" ClientInstanceName="lblMeSerial">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvcbAktiv">--%>
                            <dx:ASPxCheckBox ID="cbMeSerial" runat="server" ClientInstanceName="cbMeSerial" Width="100%">
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                    ValidationGroup="entries1" ValidateOnLeave="false">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxCheckBox>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKodifikimi1" ID="lblKodifikimi1"
                                runat="server" Text="Kodifikimi 1:" ClientInstanceName="lblKodifikimi1">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="btneKodifikimi1" runat="server" ClientInstanceName="btneKodifikimi1"
                                EnableCallbackMode="False"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                <ClientSideEvents ButtonClick="function(s, e) { KodifikimArtikulli_Click(1); grida='1';
}"
                                    TextChanged="function(s, e) {                                     
}"
                                    LostFocus="function(s, e) { merrSkeme(s); }" />
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
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="checkKontrollGjendjeArtikulli" ID="lblKontrollGjendjeArtikulli"
                                runat="server" Text="Kontroll gjendje" ClientInstanceName="lblKontrollGjendjeArtikulli">
                            </dx:ASPxLabel>
                            <dx:ASPxCheckBox ID="checkKontrollGjendjeArtikulli" runat="server" ClientInstanceName="checkKontrollGjendjeArtikulli">
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxCheckBox>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="checkKontrollCmimi" ID="lblKontrollCmimi"
                                runat="server" Text="Kontroll cmimi per detajimin" ClientInstanceName="lblKontrollCmimi">
                            </dx:ASPxLabel>
                            <dx:ASPxCheckBox ID="checkKontrollCmimi" runat="server"
                                ClientInstanceName="checkKontrollCmimi">
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxCheckBox>
                            <%--</div>--%>
                            <%--<div id="dvcheckKontrollGjendje">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="checkKontrollGjendje" ID="lblKontrollDet1"
                                runat="server" Text="Kontroll gjendje per detajimin 1" ClientInstanceName="lblKontrollDet1">
                            </dx:ASPxLabel>
                            <dx:ASPxCheckBox ID="checkKontrollGjendje" runat="server"
                                ClientInstanceName="checkKontrollGjendje">
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxCheckBox>
                            <%--</div>--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="checkKontrollGjendjeDetajim2" ID="lblKontrollDet2"
                                runat="server" Text="Kontroll gjendje per detajimin 2" ClientInstanceName="lblKontrollDet2">
                            </dx:ASPxLabel>
                            <dx:ASPxCheckBox ID="checkKontrollGjendjeDetajim2" runat="server"
                                ClientInstanceName="checkKontrollGjendjeDetajim2">
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxCheckBox>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKodifikimi2" ID="lblKodifikimi2"
                                runat="server" Text="Kodifikimi 2:" ClientInstanceName="lblKodifikimi2">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="btneKodifikimi2" runat="server" ClientInstanceName="btneKodifikimi2"
                                EnableCallbackMode="False"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                <ClientSideEvents LostFocus="function(s, e) { LostFocusKodifikim(s); }"  ButtonClick="function(s, e) {
KodifikimArtikulli_Click2(2);
grida='2';
}" />
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
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKodifikimi3" ID="lblKodifikimi3"
                                runat="server" Text="Kodifikimi 3:" ClientInstanceName="lblKodifikimi3">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="btneKodifikimi3" runat="server" ClientInstanceName="btneKodifikimi3"
                                EnableCallbackMode="False"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                                <ClientSideEvents LostFocus="function(s, e) { LostFocusKodifikim(s); }" ButtonClick="function(s, e) {
KodifikimArtikulli_Click3(3);
grida='3';
}" />
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
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtFurnitori" ID="lblFurnitori"
                                runat="server" Text="Furnitori Kryesor:" ClientInstanceName="lblFurnitori">
                            </dx:ASPxLabel>
                            <dx:ASPxComboBox ID="txtFurnitori" runat="server" ClientInstanceName="txtFurnitori"
                                Width="100%" ShowShadow="False" ValueType="System.Int32" EnableClientSideAPI="True"
                                IncrementalFilteringMode="Contains" EnableSynchronization="True" EnableCallbackMode="True"
                                DropDownRows="10" CallbackPageSize="10" OnItemRequestedByValue="txtFurnitori_ItemRequestedByValue"
                                OnItemsRequestedByFilterCondition="txtFurnitori_ItemsRequestedByFilterCondition"
                                SettingsLoadingPanel-ImagePosition="Top">
                                <ClientSideEvents ButtonClick="function(s,e){Furnitori_Click();}" SelectedIndexChanged="function (s,e){KlientFurnitoriChanged()}"
                                    GotFocus="function(s, e){s.SelectAll();}" />
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
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAutorizimi" ID="lblAutorizimi"
                                runat="server" Text="Nivel Autorizimi:" ClientInstanceName="lblAutorizimi">
                            </dx:ASPxLabel>
                            <div>
                                <select id="cmbAutorizimi">
                                </select>
                                <asp:HiddenField ID="cmbAutorizimiHf" ClientIDMode="Static" runat="server" />

                            </div>

                            <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDateAk2" ID="lblDateAk2" runat="server"
                                Text="Date Aktivizimi:" ClientInstanceName="lblDateAk2">
                            </dx:ASPxLabel>

                            <dx:ASPxDateEdit ID="dteDateAk2" runat="server" ClientInstanceName="dteDateAk2"
                                ShowShadow="False" Width="100%">

                                <DropDownButton>
                                    <Image>
                                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                    </Image>
                                </DropDownButton>
                                <CalendarProperties>
                                    <HeaderStyle Spacing="1px" />
                                    <FooterStyle Spacing="17px" />
                                </CalendarProperties>
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries2">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DisabledStyle Font-Bold="False">
                                </DisabledStyle>
                            </dx:ASPxDateEdit>

                            <br />
                            <dx:ASPxGridView ID="gvCmimet" runat="server" Width="100%" ClientInstanceName="gvCmimet"
                                OnAfterPerformCallback="gvCmimet_AfterPerformCallback" OnHtmlRowCreated="gvCmimet_HtmlRowCreated"
                                OnCustomCallback="gvCmimet_CustomCallback" OnDataBound="gvCmimet_DataBound" OnCustomJSProperties="gvCmimet_CustomJSProperties">
                                <ClientSideEvents EndCallback="function(s, e){formatoFushaDevi(); EndCallback(s, e);}" BeginCallback="function(s, e){merrTeDhenat(s, e);}" />
                                <Styles>
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                </Styles>
                                <StylesEditors>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                            </dx:ASPxGridView>
                            <dx:ASPxGridView ID="gvAmortizimi" runat="server" Width="100%" ClientVisible="false"
                                ClientInstanceName="gvAmortizimi" OnHtmlRowCreated="gvAmortizimi_HtmlRowCreated"
                                OnCustomCallback="gvAmortizimi_CustomCallback"
                                OnCustomJSProperties="gvAmortizimi_CustomJSProperties" ClientIDMode="AutoID">
                                <ClientSideEvents EndCallback="function(s,e){}" />
                                <Styles>
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                </Styles>
                                <SettingsPager PageSize="15">
                                </SettingsPager>
                                <StylesEditors>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                            </dx:ASPxGridView>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </div>
            <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hfArtikuj" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="gridDataObject" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hfAutorizime" runat="server" />
                    <asp:HiddenField ID="hfSkema" runat="server" />
                    <asp:HiddenField ID="hfKodifikime" runat="server" />
                    <asp:HiddenField ID="hfArtikujtPerberes" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfTemplateArtikujPerberes" runat="server" />
                    <asp:HiddenField ID="hfLupaFurnitori" runat="server" />
                    <asp:HiddenField ID="hfLupaMagazina" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="HfGridCol" runat="server" />
                    <asp:HiddenField ID="HfColTrup" runat="server" />
                    <asp:HiddenField ID="HfColArt" runat="server" />
                    <asp:HiddenField ID="HfColAktivitete" runat="server" />
                    <asp:HiddenField ID="HfColKosto" runat="server" />
                    <%-- per konfigurimin e lupave--%>
                    <asp:HiddenField ID="hfLupaKodbari" runat="server" />
                    <asp:HiddenField ID="hfLupaKodifikim1" runat="server" />
                    <asp:HiddenField ID="hfLupaKodifikim2" runat="server" />
                    <asp:HiddenField ID="hfLupaKodifikim3" runat="server" />
                    <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
                    <asp:HiddenField ID="hfLupaSkema" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogInv" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogBle" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogShit" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogTretet" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogShpe" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogAmortizimi" runat="server" />
                    <asp:HiddenField ID="hfLupaLlogPakesim" runat="server" />
                    <asp:HiddenField ID="hfLupaArtikuj" runat="server" />
                    <asp:HiddenField ID="hfLupaArtikujPerberes" runat="server" />
                    <asp:HiddenField ID="hfLupaDetajimi" runat="server" />
                    <asp:HiddenField ID="hfSkemaKlasa" runat="server" />
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfMetoda" runat="server" />
                    <asp:HiddenField ID="hfArtikujtEkzistues" runat="server" />
                    <asp:HiddenField ID="hfTrupiFillimit" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="hfKodetArtPerberes" runat="server" />
                    <asp:HiddenField ID="hfKoeficenti" runat="server" />
                    <asp:HiddenField ID="hfRuaj" runat="server" />
                    <asp:HiddenField ID="hfKodbaret" runat="server" />
                    <asp:HiddenField ID="hfArkivaDokId" runat="server" />
                    <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
                    </dx:ASPxHiddenField>
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxHiddenField ID="hfNrAutoKF" runat="server" ClientInstanceName="hfNrAutoKF">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
            </dx:ASPxHiddenField>
            <dx:ASPxTextBox ID="txtLlog" runat="server" Text="" Visible="true" ClientInstanceName="txtLlog"
                Width="0px" EnableTheming="False" BackColor="Transparent" Border-BorderColor="Transparent"
                ForeColor="Transparent" Height="0px">
                <Border BorderColor="Transparent" />
            </dx:ASPxTextBox>
        </div>
        <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                    EnableAnimation="False" PopupVerticalAlign="WindowCenter" AllowResize="True"
                    AppearAfter="10" ClientIDMode="AutoID" Height="400px">
                    <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');  
}" />
                    <ContentStyle>
                        <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                            PaddingTop="1px" />
                    </ContentStyle>
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
