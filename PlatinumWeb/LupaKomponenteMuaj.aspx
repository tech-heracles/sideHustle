<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaKomponenteMuaj.aspx.cs" Inherits="PlatinumWeb.LupaKomponenteMuaj" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />

    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/ListPagesaUtils.js;~/js/aspx.js/LupaKomponenteMuaj.aspx-IMB.5.3.js&v76""></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <div>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <%--<ClientSideEvents EndCallback="function(s,e){window.parent.parent.parent.parent.SessionTimeout;}" />--%>
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
                                    <ClientSideEvents ItemClick="lupa.handlers.menu_click" Init="function(s) {s.SetClientVisible(true);}" />
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
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientInstanceName="popFshi"
                        CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes"
                        Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        Width="300px" ClientIDMode="AutoID" CssPostfix="Glass">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel2" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Deshironi ta mbyllni dritaren pa ruajtur ndryshimet?">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk" AutoPostBack="false"
                                                                Text="Po">
                                                                <ClientSideEvents Click="lupa.handlers.popupFshiOkClick" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Jo" AutoPostBack="false">
                                                                <ClientSideEvents Click="lupa.handlers.popupFshiCancelClick" />
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
                     
                    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel"
                        Font-Size="9pt" Modal="True" ImagePosition="Top">
                        <LoadingDivStyle Opacity="30">
                        </LoadingDivStyle>
                    </dx:ASPxLoadingPanel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="dvDetajime">
                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server">
                    <PanelCollection>
                        <dx:PanelContent>

                            <br />
                            <table id="tblInformacion" class="renditKontrolle">
                                <tbody>
                                </tbody>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                            Text="Kodi:" ClientInstanceName="lblKodi">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi" ClientEnabled="false">
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
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi" ID="lblPershkrimi"
                                            runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtPershkrimi" runat="server" Width="100%" AutoPostBack="false" Enabled="false"
                                            ClientInstanceName="txtPershkrimi">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="True">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtParametri" ID="lblParametri"
                                            runat="server" Text="Parametri:" ClientInstanceName="lblParametri">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtParametri" runat="server" Width="100%" AutoPostBack="false" Enabled="false"
                                            ClientInstanceName="txtParametri">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="True">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMuaji" ID="lblMuaji" runat="server"
                                            Text="Muaji:" ClientInstanceName="lblMuaji">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxComboBox ID="cmbMuaji" runat="server" ClientInstanceName="cmbMuaji" ShowShadow="False"
                                            SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <ClientSideEvents SelectedIndexChanged="lupa.handlers.vitiMuajiIndexChanged"/>
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
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbViti" ID="lblViti"
                                            runat="server" Text="Viti:" ClientInstanceName="lblViti">
                                        </dx:ASPxLabel>

                                    </td>
                                    <td>
                                        <dx:ASPxComboBox ID="cmbViti" runat="server" ClientInstanceName="cmbViti"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%">
                                            <ClientSideEvents SelectedIndexChanged="lupa.handlers.vitiMuajiIndexChanged" />
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
                                    </td>
                                    <td>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVleraParametri" ID="lblVleraParametri"
                                            runat="server" Text="Vlera Parametri:" ClientInstanceName="lblVleraParametri">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtVleraParametri" runat="server" Width="100%" AutoPostBack="false"
                                            ClientInstanceName="txtVleraParametri" Text="0.00" DisplayFormatString="#,#0.00">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" KeyPress="lupa.handlers.KeyPresVleraParam" TextChanged="lupa.handlers.TextChanged"/>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="True">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVlera" ID="lblVlera"
                                            runat="server" Text="Vlera:" ClientInstanceName="lblVlera">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtVlera" runat="server" Width="100%" AutoPostBack="false" DisplayFormatString="#,#0.00"
                                            ClientInstanceName="txtVlera" Text="0.00">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="True">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>

                            <br />
                            <asp:UpdatePanel ID="updPanelGrida" runat="server">
                                <ContentTemplate>
                            <dx:ASPxGridView ID="gvLupaKomp" runat="server" ClientInstanceName="gvLupaKomp"
                                OnDataBound="gvLupaKomp_DataBound" OnAfterPerformCallback="gvLupaKomp_AfterPerformCallback"
                                Width="100%" OnAutoFilterCellEditorInitialize="gvLupaKomp_AutoFilterCellEditorInitialize"
                                OnCustomCallback="gvLupaKomp_CustomCallback">
                             <%--   <ClientSideEvents RowDblClick="function(s, e) { OnGridSelectionChanged(); }" />--%>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            <%--</div>--%>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel >
            </div>
            <asp:HiddenField ID="hfDiteMuaji" runat="server" />
            <asp:HiddenField ID="hfPageMuaji" runat="server" />
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
