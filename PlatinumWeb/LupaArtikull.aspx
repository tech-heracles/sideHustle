<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaArtikull.aspx.cs" Inherits="PlatinumWeb.LupaArtikull"
    EnableEventValidation="false" ValidateRequest="false" ViewStateEncryptionMode="Never" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaArtikull.aspx-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js&v76""
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaArtikull.aspx-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js&v76""
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
     <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaArtikull.aspx-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js&v76""
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/aspx.js/LupaArtikull.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
           
        <script language="javascript" type="text/javascript">       
        </script>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
           
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){
          if (window.parent.identikuesPerPopupArtikulli == 'ArtikullPerberes')   
              window.parent.window.parent.window.parent.SessionTimeout.sendKeepAlive();
              else
                window.parent.window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" runat="server">
        </dx:ASPxHiddenField>
        <table width="100%">
            <tr>
                <td>
                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                        ItemImagePosition="Top" Width="100%" AutoPostBack="True" ShowPopOutImages="True" OnItemClick="ASPxMenu1_ItemClick">
                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                        <ClientSideEvents ItemClick="function(s, e) {
	                                    menu_click(s,e);
                                        }" Init="function(s) {s.SetClientVisible(true);}" />
                        <ItemImage Height="32px" Width="32px">
                        </ItemImage>
                        <SubMenuItemImage Height="16px" Width="16px">
                        </SubMenuItemImage>
                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1"></RootItemSubMenuOffset>
                        <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
                            <Paddings PaddingBottom="1px" PaddingTop="9px" />
                            <Paddings PaddingTop="9px" PaddingBottom="1px"></Paddings>
                        </ItemStyle>
                        <SubMenuItemStyle Width="32px">
                        </SubMenuItemStyle>
                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify"></SubMenuStyle>
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
                            </dx:ASPxMenu>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <div id="dvArtikulli" style="display: none">
            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server">
                <PanelCollection>
                    <dx:PanelContent>
                        <table id="tblInformacion" class="CustomRenditKontrolleDy">
                            <tbody>
                            </tbody>
                        </table>
                        <dx:ASPxLabel ID="lblGjendje" runat="server" AssociatedControlID="cbGjendje" ClientInstanceName="lblGjendje"
                            Wrap="False" Text="Shfaq gjendjen e artikujve">
                        </dx:ASPxLabel>
                        <dx:ASPxCheckBox ID="cbGjendje" runat="server" ClientInstanceName="cbGjendje" Width="100%"
                            RightToLeft="False" Layout="Flow">
                        </dx:ASPxCheckBox>
                        <dx:ASPxDateEdit ID="dtDataGjendje" runat="server" ClientInstanceName="dtDataGjendje" DisplayFormatString="dd/MM/yyyy">
                        </dx:ASPxDateEdit>
                        <dx:ASPxLabel ID="lblKosto" runat="server" AssociatedControlID="cbKosto" ClientInstanceName="lblKosto"
                            Wrap="False" Text="Shfaq koston e artikullit">
                        </dx:ASPxLabel>
                        <dx:ASPxCheckBox ID="cbKosto" runat="server" ClientInstanceName="cbKosto" Width="100%"
                            RightToLeft="False" Layout="Flow">
                        </dx:ASPxCheckBox>
                        <dx:ASPxLabel ID="lblCmime" runat="server" AssociatedControlID="cbCmime" ClientInstanceName="lblCmime"
                            Wrap="False" Text="Shfaq cmimet">
                        </dx:ASPxLabel>
                        <dx:ASPxCheckBox ID="cbCmime" runat="server" ClientInstanceName="cbCmime" Width="100%"
                            RightToLeft="False" Layout="Flow">
                        </dx:ASPxCheckBox>
                        <dx:ASPxLabel ID="lblCmimeMeTvsh" runat="server" AssociatedControlID="cbCmimeMeTvsh" ClientInstanceName="lblCmimeMeTvsh"
                            Wrap="False" Text="Shfaq cmime me TVSH">
                        </dx:ASPxLabel>
                        <dx:ASPxCheckBox ID="cbCmimeMeTvsh" runat="server" ClientInstanceName="cbCmimeMeTvsh" Width="100%"
                            RightToLeft="False" Layout="Flow">
                        </dx:ASPxCheckBox>
                        <dx:ASPxLabel ID="lblRuajFilter" runat="server" AssociatedControlID="cbRuajFilter" ClientInstanceName="lblRuajFilter"
                            Wrap="False" Text="Ruaj filter">
                        </dx:ASPxLabel>
                        <dx:ASPxCheckBox ID="cbRuajFilter" runat="server" ClientInstanceName="cbRuajFilter" Width="100%"
                            RightToLeft="False" Layout="Flow">
                            <ClientSideEvents CheckedChanged="function (s,e){ RuajFilterGrida(); }" />
                        </dx:ASPxCheckBox>
                        <dx:ASPxLabel Wrap="False" ID="lblMagazina" AssociatedControlID="btneMagazina" runat="server"
                            Text="Magazina" ClientInstanceName="lblMagazina">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="btneMagazina" Width="100%" runat="server" ClientInstanceName="btneMagazina"
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
                        <dx:ASPxLabel Wrap="False" ID="lblKodifikimi1" AssociatedControlID="btneKodifikimi1" 
                            runat="server" Text="Grupimi 1:" ClientInstanceName="lblKodifikimi1">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="btneKodifikimi1" runat="server" ClientInstanceName="btneKodifikimi1"
                            EnableCallbackMode="False"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                            <ClientSideEvents ButtonClick="function(s, e) { KodifikimArtikulli_Click(1); }"
                                LostFocus="function(s, e) { merrSkeme(s); }"
                                SelectedIndexChanged="function(s, e) { Updatenormat(s); }" TextChanged="function(s, e) { Updatenormat(s); }" />
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
                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneKodifikimi2" ID="lblKodifikimi2"
                            runat="server" Text="Grupimi 2:" ClientInstanceName="lblKodifikimi2">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="btneKodifikimi2" runat="server" ClientInstanceName="btneKodifikimi2"
                            EnableCallbackMode="False"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                            <ClientSideEvents ButtonClick="function(s, e) { KodifikimArtikulli_Click(2); }" />
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
                            runat="server" Text="Grupimi 3:" ClientInstanceName="lblKodifikimi3">
                        </dx:ASPxLabel>
                        <dx:ASPxComboBox ID="btneKodifikimi3" runat="server" ClientInstanceName="btneKodifikimi3"
                            EnableCallbackMode="False"
                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" Width="100%">
                            <ClientSideEvents ButtonClick="function(s, e) { KodifikimArtikulli_Click(3); }" />
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
                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                            ShowShadow="False" Width="100%" ValueType="System.String" ClientVisible="false" SettingsLoadingPanel-ImagePosition="Top">
                        </dx:ASPxComboBox>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel >
        </div>
        <asp:UpdatePanel ID="pnlKryesor" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                                    <templates>
                                         <TitlePanel>
                                                    <div runat="server" id ="gridaSelectButtons" visible ="false">
                                                        <table>
                                                            <tr>
                                                                <td>
					                                                <dx:ASPxButton ID="gridaSelectFaqe" runat="server" ToolTip="Zgjidh te gjithe faqen"
						                                                AutoPostBack="false" Image-Url="images/check2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
						                                                <ClientSideEvents Click="function(s, e) { gvLupaArtikull.SelectAllRowsOnPage(); }" />
					                                                </dx:ASPxButton>

				                                                </td>
				                                                <td>
					                                                <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
						                                                AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
						                                                <ClientSideEvents Click="function(s, e) { gvLupaArtikull.SelectRows(); }" />
					                                                </dx:ASPxButton>
				                                                </td>
				                                                <td>
					                                                <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen"
						                                                AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
						                                                <ClientSideEvents Click="function(s, e) { gvLupaArtikull.UnselectRows(); }" />
					                                                </dx:ASPxButton>
				                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                   
                                              </TitlePanel>
                                        </templates>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxGridView ID="gvLupaArtikull" runat="server" ClientInstanceName="gvLupaArtikull"
                                OnDataBound="gvLupaArtikull_DataBound" OnAutoFilterCellEditorInitialize="gvLupaArtikull_AutoFilterCellEditorInitialize"
                                OnAfterPerformCallback="gvLupaArtikull_AfterPerformCallback" EnableCallbackCompression="True" EnableCallBacks="True"
                                Width="100%" OnCustomCallback="gvLupaArtikull_CustomCallback" OnProcessColumnAutoFilter="gvLupaArtikull_ProcessColumnAutoFilter"
                                OnCustomJSProperties="gvLupaArtikull_CustomJSProperties">

                                <StylesPager Summary-Width="100%" PageNumber-Width="100%">
                                </StylesPager>
                                <ClientSideEvents Init="function(s, e){s.SetHeight(435); }" RowDblClick="function(s, e) { OnGridSelectionChanged(); }"
                                    BeginCallback="function(s, e) { BeginCallback(s,e); }" />
                                <Styles>
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                </Styles>
                                <StylesEditors>
                                    <ProgressBar Height="100%">
                                    </ProgressBar>
                                </StylesEditors>
                                <SettingsPager></SettingsPager>
                            </dx:ASPxGridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hfLidhur" runat="server" />
        <asp:HiddenField ID="hfKontrollet" runat="server" />
        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
        <asp:HiddenField ID="hfLupaMagazina" runat="server" />
        <asp:HiddenField ID="hfLupaKodifikim1" runat="server" />
        <asp:HiddenField ID="hfLupaKodifikim2" runat="server" />
        <asp:HiddenField ID="hfLupaKodifikim3" runat="server" />
        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
            CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID"
            AutoUpdatePosition="True" Font-Bold="False">
            <ClientSideEvents Closing="function(s, e) { popupUniversal.SetContentUrl(''); }" />
            <ContentStyle>
                <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                    PaddingTop="1px" />
            </ContentStyle>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl >
    </form>
</body>
</html>
