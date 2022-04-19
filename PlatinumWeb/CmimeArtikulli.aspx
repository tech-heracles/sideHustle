<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CmimeArtikulli.aspx.cs"
    Inherits="PlatinumWeb.CmimeArtikulli" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <!-- DevExtreme themes -->
    <link rel="stylesheet" type="text/css" href="Content/dx.common.css" />
    <link rel="stylesheet" type="text/css" href="Content/dx.generic.alphaweb-compact.css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />

    <!-- A DevExtreme library -->
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/Scripts/jszip.min.js;~/Scripts/dx.viz-web.js;~/js/localization/DevExtreme.Perkthime.js;~/js/myDxDataGrid.js;~/js/aspx.js/CmimeArtikulli.aspx-IMB.2.1.js&v76"" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.debug.js" integrity="sha384-NaWTHo/8YCBYJ59830LTz/P4aQZK1sS0SneOgAvhsIl3zBu8r9RevNg5lHCHAuQ/" crossorigin="anonymous"></script>
    <style>
        .dx-datagrid-header-panel {
            border: 1px solid #ddd;
            padding: 5px;
            padding-bottom: unset;
            border-bottom: unset;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">          
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
          
        </asp:ScriptManager>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>

                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"                         
                                   ItemImagePosition="Top" Width="100%" ShowPopOutImages="True">
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                    <ClientSideEvents ItemClick="cmimet.handlers.menu_click" Init="function(s) {s.SetClientVisible(true);}" />
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
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
                                    <dx:ASPxMenu ID="MenuInfo" runat="server" BorderBetweenItemAndSubMenu="HideRootOnly"
                                        ClientIDMode="AutoID" ClientInstanceName="MenuInfo" ShowPopOutImages="True" Width="100%">
                                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <SubMenuStyle GutterWidth="17px" />
                                    </dx:ASPxMenu>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divgride1" style="display: none">
            <table>
                <tr>
                    <td class="renditKontrolleCaption" style="width: 3%">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="lblKonfigurimi"
                            runat="server" Text="Modeli:" ClientInstanceName='lblKonfigurimi' class="klasePerLblKonfigurimi">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 20%; padding: 2px 2px 2px 3px; border-collapse: collapse">
                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                            ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ClientSideEvents SelectedIndexChanged="cmimet.handlers.cmbKonfigurimiChanged" />
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="renditKontrolleLabelMeWidth25">
                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimiP" runat="server" Text="" class="klasePerLblKonfigurimi"
                            ClientInstanceName="lblKonfigurimiP" >
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth50"></td>
                </tr>
            </table>
            <div id="dvVlera">
                <table id="tblVlera" class="renditKontrolle">
                    <tbody>
                    </tbody>
                </table>
                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbRritjeZbritje" ID="lblRritjeZbritje"
                    runat="server" Text="Rritje/Zbritje" ClientInstanceName="lblRritjeZbritje">
                </dx:ASPxLabel>
                <dx:ASPxComboBox ID="cmbRritjeZbritje" runat="server" ClientInstanceName="cmbRritjeZbritje"
                    ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top">
                    <ClientSideEvents ValueChanged="cmimet.handlers.cmbRritjeZbritjeValueChanged" />
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
                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbVlerePerqindje" ID="lblVlerePerqindje"
                    runat="server" Text="Vlere/Perqindje" ClientInstanceName="lblVlerePerqindje">
                </dx:ASPxLabel>
                <dx:ASPxComboBox ID="cmbVlerePerqindje" runat="server" ClientInstanceName="cmbVlerePerqindje"
                    ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top">
                    <ClientSideEvents SelectedIndexChanged="cmimet.handlers.cmbVlerePerqindjeIndexChanged" />
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
                <dx:ASPxLabel Wrap="False" AssociatedControlID="txtVlera" ID="lblVlera" runat="server"
                    Text="Vlera:" ClientInstanceName="lblVlera">
                </dx:ASPxLabel>
                <dx:ASPxTextBox ID="txtVlera" runat="server" ClientInstanceName="txtVlera" Text="0"
                    ClientVisible="False" Width="100%">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                        ValidationGroup="entries" SetFocusOnError="true">
                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                        <RegularExpression ValidationExpression="[0-9,.]*" ErrorText="Lejohen vetem numra!" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
                <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtFillimi2" ID="lblDtFillimi2"
                    runat="server" Text="Dt.Fillimit:" ClientInstanceName="lblDtFillimi2">
                </dx:ASPxLabel>
                <dx:ASPxDateEdit ID="dteDtFillimi2" runat="server" ClientInstanceName="dteDtFillimi2"
                    ValidationSettings-CausesValidation="True" ShowShadow="False" Width="100%">
                    <CalendarProperties>
                        <HeaderStyle Spacing="1px" />
                        <FooterStyle Spacing="17px" />
                    </CalendarProperties>
                    <DropDownButton>
                        <Image>
                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                        </Image>
                    </DropDownButton>
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                        ValidationGroup="entries">
                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                    </ValidationSettings>
                </dx:ASPxDateEdit>
                <dx:ASPxLabel Wrap="False" AssociatedControlID="dteDtMbarimi2" ID="lblDtMbarimi2"
                    runat="server" Text="Dt.Mbarimit:" ClientInstanceName="lblDtMbarimi2">
                </dx:ASPxLabel>
                <dx:ASPxDateEdit ID="dteDtMbarimi2" runat="server" ClientInstanceName="dteDtMbarimi2"
                    ValidationSettings-CausesValidation="True" ShowShadow="False" Width="100%">
                    <CalendarProperties>
                        <HeaderStyle Spacing="1px" />
                        <FooterStyle Spacing="17px" />
                    </CalendarProperties>
                    <DropDownButton>
                        <Image>
                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                        </Image>
                    </DropDownButton>
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                        ValidationGroup="entries">
                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                    </ValidationSettings>
                </dx:ASPxDateEdit> 
                 <dx:ASPxLabel Wrap="False" AssociatedControlID="dteKoheFillimi2" ID="lblKoheFillimi2"
                    runat="server" Text="Kohe Fillimi:" ClientInstanceName="lblKoheFillimi2">
                </dx:ASPxLabel>
                <dx:ASPxTimeEdit ID="dteKoheFillimi2" runat="server" ClientInstanceName="dteKoheFillimi2"
                    ValidationSettings-CausesValidation="True" ShowShadow="False" Width="100%">
                                      <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                        ValidationGroup="entries">
                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                    </ValidationSettings>
                </dx:ASPxTimeEdit>
                <dx:ASPxLabel Wrap="False" AssociatedControlID="dteKoheMbarimi2" ID="lblKoheMbarimi2"
                    runat="server" Text="Kohe Mbarimi:" ClientInstanceName="lblKoheMbarimi2">
                </dx:ASPxLabel>
                <dx:ASPxTimeEdit ID="dteKoheMbarimi2" runat="server" ClientInstanceName="dteKoheMbarimi2"
                    ValidationSettings-CausesValidation="True" ShowShadow="False" Width="100%">
                   
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                        ValidationGroup="entries">
                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                    </ValidationSettings>
                </dx:ASPxTimeEdit>
                <dx:ASPxButton ID="btnNdrysho" runat="server" Text="Ndrysho" ClientInstanceName="btnNdrysho"
                    AutoPostBack="False" CausesValidation="False" Width="100%">
                    <ClientSideEvents Click="cmimet.handlers.NdryshoClicked" />
                </dx:ASPxButton>
                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbGjendje" ID="lblGjendje" runat="server"
                    Text="Shfaq gjendjen e artikujve" ClientInstanceName="lblGjendje">
                </dx:ASPxLabel>
                <dx:ASPxCheckBox ID="cbGjendje" runat="server" ClientInstanceName="cbGjendje" Width="100%">
                    <ClientSideEvents CheckedChanged="cmimet.handlers.cbGjendjeChecked" />
                </dx:ASPxCheckBox>
                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbKosto" ID="lblKosto" runat="server"
                    Text="Shfaq koston e artikullit" ClientInstanceName="lblKosto">
                </dx:ASPxLabel>
                <dx:ASPxCheckBox ID="cbKosto" runat="server" ClientInstanceName="cbKosto" Width="100%">
                    <ClientSideEvents CheckedChanged="cmimet.handlers.cbKostoChecked" />
                </dx:ASPxCheckBox>
            </div>            
        </div>
        <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                    <ClientSideEvents Closing="cmimet.handlers.popupUniversalCloseUp" />
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server"></dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hfKontrollet" runat="server" />
 
        <div id="MainFrameDiv">
            <!------Grida e cmimeve duke perdorur DevExtreme---------->
            <div id="PricesDataGrid" style="min-width: 800px; margin-top:20px;" class =""></div>
        </div>
    </form>
</body>
</html>
