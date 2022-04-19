<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KrahasimInventarizimi.aspx.cs" Inherits="PlatinumWeb.KrahasimInventarizimi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/KrahasimInventarzimi.aspx-IMB.5.4.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         
        <asp:ScriptManager ID="ScriptManager1" runat="server">
          
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"
                                ItemImagePosition="Top" OnDataBound="ASPxMenu1_DataBound" SeparatorWidth="1px"  OnItemClick="ASPxMenu1_ItemClick"
                                ShowPopOutImages="True" Width="100%">
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
                 <asp:HiddenField ID="hfRaport" runat="server" />

            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfNrAutoKF" runat="server" ClientInstanceName="hfNrAutoKF">
        </dx:ASPxHiddenField>
        <div style="width: 100%">
            <br />
            <table>
                <tr>
                    <td>
                        <dx:ASPxLabel Wrap="False" ID="lblMagazina" AssociatedControlID="btneMagazina" runat="server"
                            Text="Magazina" ClientInstanceName="lblMagazina">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox Width="100%" ID="btneMagazina" ClientInstanceName="btneMagazina"
                            runat="server" ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top">
                            <LoadingPanelImage>
                            </LoadingPanelImage>
                            <DropDownButton>
                                <Image>
                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                </Image>
                            </DropDownButton>
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
                                ValidationGroup="entries1" ValidateOnLeave="false">
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>
                    <td>
                        <dx:ASPxLabel Wrap="False" ID="lblDtDok" AssociatedControlID="dteDtDok" runat="server"
                            Text="Dt Dokumenti:" ClientInstanceName="lblDtDok">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit Width="100%" ID="dteDtDok" runat="server" ClientInstanceName="dteDtDok"
                            ShowShadow="False">

                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
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
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
                                ValidationGroup="entries" SetFocusOnError="true">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <DisabledStyle Font-Bold="False">
                            </DisabledStyle>
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
            </table>
            <br />
               <table class="renditKontrolle">
            <tbody>
                <tr>
                    <td class="renditKontrolleCaption">
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                            runat="server" ClientIDMode="AutoID" Text="Modeli:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth33">
                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                            ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                            Width="100%" AnimationType="None">
                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
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
                        <dx:ASPxLabel ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                        </dx:ASPxLabel>
                    </td>
                    <td class="renditKontrolleCellMeWidth33"></td>
                </tr>
            </tbody>
        </table>
            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
                  TabSpacing="3px" Width="100%" ActiveTabIndex="0" Height="600px">
                <ClientSideEvents ActiveTabChanging="function(s, e) {ndryshimTabi(e.tab);}" />
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>
                    <dxtc:TabPage Name="ArtProgram" Text="Art. Program">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                <dx:ASPxLabel Wrap="False" ID="lblArtProgram" runat="server"
                                    Text="Artikujt qe jane ne program dhe jo ne inventar" ClientInstanceName="lblArtProgram" Font-Bold="true">
                                </dx:ASPxLabel>
                                <br />
                                <br />

                                <dx:ASPxGridView ID="gvEkzistuese" runat="server" Width="100%" OnAfterPerformCallback="gridat_AfterPerformCallback"
                                    OnHeaderFilterFillItems="gridat_HeaderFilterFillItems"
                                    ClientInstanceName="gvEkzistuese" OnProcessColumnAutoFilter="gridat_ProcessColumnAutoFilter"
                                    OnCustomCallback="gridat_CustomCallback" OnCustomJSProperties="gridat_CustomJSProperties">

                                    
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <SettingsEditing Mode="EditFormAndDisplayRow" />
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="gridExport1" runat="server" GridViewID="gvEkzistuese" ExportedRowType="All">
                                </dx:ASPxGridViewExporter>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="ArtPerbashket" Text="Art. e perbashket">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl2" runat="server">
                                <dx:ASPxLabel Wrap="False" ID="lblArtPerbashket" runat="server"
                                    Text="Artikujt e perbashket qe ekzistojne" ClientInstanceName="lblArtPerbashket" Font-Bold="true">
                                </dx:ASPxLabel>
                                <br />
                                <br />

                                <dx:ASPxGridView ID="gvPerbashket" runat="server" Width="100%" OnAfterPerformCallback="gridat_AfterPerformCallback"
                                    OnHeaderFilterFillItems="gridat_HeaderFilterFillItems"
                                    ClientInstanceName="gvPerbashket" OnProcessColumnAutoFilter="gridat_ProcessColumnAutoFilter"
                                    OnCustomCallback="gridat_CustomCallback" OnCustomJSProperties="gridat_CustomJSProperties">
                                    
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <SettingsEditing Mode="EditFormAndDisplayRow" />
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="gridExport2" runat="server" GridViewID="gvPerbashket" ExportedRowType="All" MaxColumnWidth="250">
                                </dx:ASPxGridViewExporter>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="ArtMagTjeter" Text="Art. magazine tjeter">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl4" runat="server">
                                <dx:ASPxLabel Wrap="False" ID="lblArtMagTjeter" runat="server"
                                    Text="Artikujt qe jane ne inventar dhe ne program jane ne magazine tjeter" ClientInstanceName="lblArtMagTjeter" Font-Bold="true">
                                </dx:ASPxLabel>
                                <br />
                                <br />

                                <dx:ASPxGridView ID="gvMagTjeter" runat="server" Width="100%" OnAfterPerformCallback="gridat_AfterPerformCallback"
                                    OnHeaderFilterFillItems="gridat_HeaderFilterFillItems"
                                    ClientInstanceName="gvMagTjeter" OnProcessColumnAutoFilter="gridat_ProcessColumnAutoFilter"
                                    OnCustomCallback="gridat_CustomCallback" OnCustomJSProperties="gridat_CustomJSProperties">
                                   
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <SettingsEditing Mode="EditFormAndDisplayRow" />
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="gridExport4" runat="server" GridViewID="gvMagTjeter" ExportedRowType="All" MaxColumnWidth="250">
                                </dx:ASPxGridViewExporter>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="ArtJoEkzistues" Text="Art. jo ne program">
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                <dx:ASPxLabel Wrap="False" ID="lblArtJoEkzistues" runat="server"
                                    Text="Artikujt qe nuk jane ne program" ClientInstanceName="lblArtJoEkzistues" Font-Bold="true">
                                </dx:ASPxLabel>
                                <br />
                                <br />

                                <dx:ASPxGridView ID="gvJoEkzistues" runat="server" Width="100%" OnAfterPerformCallback="gridat_AfterPerformCallback"
                                    OnHeaderFilterFillItems="gridat_HeaderFilterFillItems"
                                    ClientInstanceName="gvJoEkzistues" OnProcessColumnAutoFilter="gridat_ProcessColumnAutoFilter"
                                    OnCustomCallback="gridat_CustomCallback" OnCustomJSProperties="gridat_CustomJSProperties">
                                   
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <SettingsEditing Mode="EditFormAndDisplayRow" />
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="gridExport3" runat="server" GridViewID="gvJoEkzistues" ExportedRowType="All" MaxColumnWidth="250">
                                </dx:ASPxGridViewExporter>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
            </dxtc:ASPxPageControl >
            <asp:HiddenField ID="hfRuaj" runat="server" />
            <asp:HiddenField ID="hfKonffillestar" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            <asp:HiddenField ID="hfKonfigurimi" runat="server" />
            <asp:HiddenField ID="hfNiveli" runat="server" />
            <asp:HiddenField ID="hfKonfigurimi2" runat="server" />
            <asp:HiddenField ID="hfNiveli2" runat="server" />  
            <dx:ASPxLabel ID="pergjigja" runat="server" ForeColor="Green">
            </dx:ASPxLabel>

            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
        </div>
        <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" AllowResize="True"
                    AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popupUniversal" CloseAction="CloseButton"
                    EnableAnimation="False" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter">
                    <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');
}" />
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
