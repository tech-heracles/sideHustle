<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_RecetaOptike.aspx.cs"
    Inherits="PlatinumWeb.Shto_RecetaOptike" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="js/css/le-frog/jquery-ui.css" media="screen" rel="stylesheet" type="text/css"
        runat="server" id="themeJQuery" />

<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/Shto_RecetaOptike.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
    <style type="text/css">
        .ui-jqgrid .ui-jqgrid-bdiv {
            position: relative;
            margin: 0em;
            padding: 0;
            overflow-x: hidden;
            overflow-y: auto;
            text-align: left;
        }

        .not_visible {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <asp:HiddenField ID="hfLupaKlientFurnitor" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                    OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                    <ClientSideEvents ItemClick="function(s, e) {
	                            menu_click(s,e);
                                }" />
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
                      
                    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                        Font-Size="9pt" Modal="True" ImagePosition="Top">
                        <LoadingDivStyle Opacity="30">
                        </LoadingDivStyle>

                    </dx:ASPxLoadingPanel>
                    <div id="bootPopUp" class="bootstrap-iso"></div>
                    <div id="accordition" class="not_visible">
                        <div>
                            <h3 id="kokeKonfigurimi"><span id="kokeKonfigurimidiv" class="ui-not-accordion-header-text">Koke Dokumenti: Receta Optike</span></h3>
                            <div>
                                <table id="tblKonfigurimi" runat="server">
                                </table>
                                <table id="tblFillim" class="renditKontrolle">
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="trup">
                            <h3 id="trupKonfigurimi"><span id="trupKonfigurimidiv" class="ui-not-accordion-header-text">Trup Dokumenti</span></h3>
                            <div>
                                <div>
                                    <dx:ASPxGridView ID="gvFushatRO" runat="server" ClientInstanceName="gvFushatRO" OnHtmlRowCreated="gvFushatRO_HtmlRowCreated"
                                        Width="100%" OnDataBound="gvFushatRO_DataBound" OnCustomJSProperties="gvFushatRO_CustomJSProperties">
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                    <br />
                                    <br />
                                    <dx:ASPxGridView ID="gvRecetaTrupi" runat="server" ClientInstanceName="gvRecetaTrupi"
                                        OnDataBound="gvRecetaTrupi_DataBound" OnBatchUpdate="gvRecetaTrupi_BatchUpdate" OnCustomCallback="gvRecetaTrupi_CustomCallback"
                                        Width="100%">
                                        <ClientSideEvents CustomButtonClick="CustomButtonsClick" BeginCallback="BeginCallback" BatchEditStartEditing="StartEditing" BatchEditEndEditing="EndEditing" EndCallback="EndCallback" />
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <Templates>
                                            <StatusBar></StatusBar>
                                        </Templates>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                        <SettingsLoadingPanel Mode="Disabled" />
                                    </dx:ASPxGridView>
                                    <br />
                                </div>
                            </div>
                        </div>

                    </div>
                    <div id="dvFillim" class="atributeDiveFshehur">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNumri" ID="lblNumri" runat="server"
                            Text="Numri:" ClientInstanceName="lblNumri">
                        </dx:ASPxLabel>


                        <dx:ASPxTextBox ID="txtNumri" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtNumri">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip"
                                ValidationGroup="entries" SetFocusOnError="true">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>

                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAdresa" ID="lblAdresa" runat="server"
                            Text="Adresa:" ClientInstanceName="lblAdresa">
                        </dx:ASPxLabel>

                        <dx:ASPxMemo ID="txtAdresa" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtAdresa"
                            Rows="3">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip"
                                ValidationGroup="entries" SetFocusOnError="true">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxMemo>

                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKlienti" ID="lblKlienti"
                            runat="server" Text="Klienti:" ClientInstanceName="lblKlienti">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="cmbKlienti" runat="server" ClientInstanceName="cmbKlienti"
                            EnableCallbackMode="True" ReadOnly="false" IncrementalFilteringMode="Contains"
                            EnableSynchronization="True"
                            SettingsLoadingPanel-ImagePosition="Top" OnItemRequestedByValue="cmbKlienti_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbKlienti_ItemsRequestedByFilterCondition"
                            ShowShadow="False" Width="100%">
                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickKlienti();}" SelectedIndexChanged="function (s,e){KlientFurnitoriChanged()}"
                                GotFocus="function(s, e){s.SelectAll();}" />
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
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>


                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbFormatPrintimi" ID="lblFormatPrintimi"
                            runat="server" Text="Formati i Printimit:" ClientInstanceName="lblFormatPrintimi">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="cmbFormatPrintimi" runat="server" ClientInstanceName="cmbFormatPrintimi"
                            ShowShadow="False" Width="100%">

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
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>


                        <dx:ASPxLabel ID="lblDIAfer" runat="server" AssociatedControlID="txtDIAfer"
                            ClientInstanceName="lblDIAfer" Text="D.I Afer:"
                            Wrap="False">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtDIAfer" runat="server" AutoPostBack="false" ClientInstanceName="txtDIAfer"
                            Width="100%">

                            <ValidationSettings CausesValidation="true" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />

                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>

                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDateDokumenti" ID="lblDateDokumenti" runat="server"
                            Text="Data:" ClientInstanceName="lblDateDokumenti">
                        </dx:ASPxLabel>

                        <dx:ASPxDateEdit ID="dteDateDokumenti" runat="server" ClientInstanceName="dteDateDokumenti"
                            ShowShadow="False" Width="100%">
                            <ClientSideEvents DateChanged="DateChanged" />
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <CalendarProperties>
                                <HeaderStyle Spacing="1px" />
                                <FooterStyle Spacing="17px" />
                            </CalendarProperties>
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxDateEdit>

                        <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDateRegj" ID="lblDateRegj" runat="server"
                            Text="Data:" ClientInstanceName="lblDateRegj">
                        </dx:ASPxLabel>

                        <dx:ASPxDateEdit ID="dteDateRegj" runat="server" ClientInstanceName="dteDateRegj"
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
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxDateEdit>

                        <dx:ASPxLabel ID="lblDILarg" runat="server" AssociatedControlID="txtDILarg"
                            ClientInstanceName="lblDILarg" Text="D.I Larg:"
                            Wrap="False">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtDILarg" runat="server" AutoPostBack="false" ClientInstanceName="txtDILarg"
                            Width="100%">

                            <ValidationSettings CausesValidation="true" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>

                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="lblShenime" runat="server"
                            Text="Shenime:" ClientInstanceName="lblShenime">
                        </dx:ASPxLabel>
                        <dx:ASPxMemo ID="txtShenime" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtShenime"
                            Rows="3">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                ValidationGroup="entries" SetFocusOnError="true">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxMemo>

                        <dx:ASPxLabel Wrap="False" ID="lblNrSerial" AssociatedControlID="txtNrSerial" runat="server"
                            Text="Numer serial:" ClientInstanceName="lblNrSerial">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtNrSerial" runat="server" AutoPostBack="false" ClientInstanceName="txtNrSerial"
                            Width="100%">

                            <ValidationSettings CausesValidation="true" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>

                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxLabel Wrap="False" ID="lblLartesia" AssociatedControlID="txtLartesia" runat="server"
                            Text="Lartesia:" ClientInstanceName="lblLartesia">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtLartesia" runat="server" AutoPostBack="false" ClientInstanceName="txtLartesia"
                            Width="100%">

                            <ValidationSettings CausesValidation="true" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>

                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxLabel Wrap="False" ID="lblReferimi" AssociatedControlID="txtReferimi" runat="server"
                            Text="Referimi:" ClientInstanceName="lblReferimi">
                        </dx:ASPxLabel>
                        <dx:ASPxTextBox ID="txtReferimi" runat="server" AutoPostBack="false" ClientInstanceName="txtReferimi"
                            Width="100%">

                            <ValidationSettings CausesValidation="true" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="entries">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>

                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="checkPrint" ID="lblPrintoCheck" runat="server"
                            Text="Printo:" ClientInstanceName="lblPrintoCheck">
                        </dx:ASPxLabel>

                        <dx:ASPxCheckBox ID="checkPrint" runat="server" ClientInstanceName="checkPrint" Width="100%">
                            <ClientSideEvents CheckedChanged="function(s, e) { 
                        
                                 PrintoChecked(s, e); 
                            }" />
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
                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" AssociatedControlID="cmbKonfigurimi"
                            runat="server" Text="Lloji:" ClientInstanceName="lblKonfigurimi" ClientVisible="false">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox Width="100%" ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" AnimationType="None" ClientVisible="false">
                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                            <LoadingPanelImage>
                            </LoadingPanelImage>
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
                                ValidationGroup="entries" SetFocusOnError="true">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                        <dx:ASPxLabel Wrap="False" ID="lblNiveli" AccessKey="N" AssociatedControlID="cmbNiveli" ClientVisible="false"
                            runat="server" Text="<u>N</u>iveli" EncodeHtml="false" ClientInstanceName="lblNiveli">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="cmbNiveli" Width="100%" runat="server" ClientInstanceName="cmbNiveli" ClientVisible="false"
                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxditors_edtDropDownHover_Aqua" PressedCssClass="dxditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                    </div>


                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
            </dx:ASPxHiddenField>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                <ClientSideEvents Closing="function(s, e) { popupUniversal.SetContentUrl('');}" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>

        <asp:UpdatePanel ID="hfs" runat="server">
            <ContentTemplate>
                <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
                <asp:HiddenField ID="hfGridaKodi" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:HiddenField ID="hfStatusDokumenti" runat="server" />
                <asp:HiddenField ID="hfRecetaOptikeSyri" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
                <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
                <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
                </dx:ASPxHiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>

                    <iframe id="Container" runat="server" frameborder="0" height="0" name="Container"
                        width="0"></iframe>
                    
                    <iframe id="Container2" runat="server" frameborder="0" height="0" name="Container2"
                        width="0"></iframe>
                </ContentTemplate>
            </asp:UpdatePanel>


        </div>
    </form>
</body>
</html>
