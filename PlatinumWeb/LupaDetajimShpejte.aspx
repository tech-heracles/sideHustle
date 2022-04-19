<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaDetajimShpejte.aspx.cs"
    Inherits="PlatinumWeb.LupaDetajimShpejte" EnableEventValidation="false" ValidateRequest="false"
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
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <%--    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/myMesazh-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myButtonClickLupa-IMB.2.1.js?versioni22" type="text/javascript"></script>
     
    <script src="js/Utils-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myJQGrid-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myBuxhet-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myFushaShtese-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myNrAuto-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/aspx.js/LupaArtikullShpejte.aspx-IMB.2.2.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/LupaDetajimShpejte-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body onkeydown="enter()" onload="Init()">
    <form id="form1" runat="server" style="width: 100%">
        <div>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.window.parent.SessionTimeout.sendKeepAlive();}" />--%>
            </dx:ASPxGlobalEvents>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
             
            </asp:ScriptManager>
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                    OnItemClick="ASPxMenu1_ItemClick">
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
                     
                    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel"
                        Font-Size="9pt" Modal="True" ImagePosition="Top">
                        <LoadingDivStyle Opacity="30">
                        </LoadingDivStyle>
                    </dx:ASPxLoadingPanel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="dvDetajime" style="display: none;">
                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server">
                    <PanelCollection>
                        <dx:PanelContent>
                            <table class="renditKontrolleTre">
                                <tbody>
                                    <tr>
                                        <td class="renditKontrolleCaption">
                                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                runat="server" ClientIDMode="AutoID" Text="Modeli: ">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td class="renditKontrolleCellMeWidth33">
                                            <dx:ASPxComboBox ID="cmbKonfigurimi" AnimationType="None" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                                Width="100%">
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
                            <%--<div id="dvlblPershkrimi">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi" ID="lblPershkrimi"
                                runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvtxtPershkrimi">--%>
                            <dx:ASPxMemo ID="txtPershkrimi" runat="server" Width="100%" AutoPostBack="false"
                                ClientInstanceName="txtPershkrimi" Rows="3">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                    ValidationGroup="entries" SetFocusOnError="True">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxMemo>
                            <%--</div>--%>
                            <%--<div id="dvlblKlasa">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLloji" ID="lblLloji" runat="server"
                                Text="Klasa:" ClientInstanceName="lblLloji">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvcmbKlasa">--%>
                            <dx:ASPxComboBox ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji" ShowShadow="False"
                                SettingsLoadingPanel-ImagePosition="Top" Width="100%">
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
                            <%--<div id="dvlblKlasa">--%>
                            <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKategoria" ID="lblKategoria"
                                runat="server" Text="Klasa:" ClientInstanceName="lblKategoria">
                            </dx:ASPxLabel>
                            <%--</div>--%>
                            <%--<div id="dvcmbKlasa">--%>
                            <dx:ASPxComboBox ID="cmbKategoria" runat="server" ClientInstanceName="cmbKategoria"
                                ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                <ClientSideEvents TextChanged="function(s, e){ TextChangedKategoria(); }" />
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
                            <br />
                            <dx:ASPxGridView ID="gvLupaDetajime" runat="server" ClientInstanceName="gvLupaDetajime"
                                OnDataBound="gvDetajimArtikulli_DataBound" OnAfterPerformCallback="gvDetajimArtikulli_AfterPerformCallback"
                                Width="100%" OnAutoFilterCellEditorInitialize="gvDetajimArtikulli_AutoFilterCellEditorInitialize"
                                OnCustomCallback="gvLupaDetajime_CustomCallback">
                                <ClientSideEvents RowDblClick="function(s, e) { OnGridSelectionChanged(); }" />
                                <Styles>
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                </Styles>
                                <StylesEditors>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                                <SettingsPager></SettingsPager>
                            </dx:ASPxGridView>
                            <%--</div>--%>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel >
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hfAutorizime" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <%-- per konfigurimin e lupave--%>
                    <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
