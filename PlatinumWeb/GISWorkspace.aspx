<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GISWorkspace.aspx.cs" Inherits="PlatinumWeb.GISWorkspace" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/jquery.ui.datepicker-sq.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/GISWorkspace.aspx-IMB.6.3.0.0.js&v76""
        type="text/javascript">
    </script>
</head>

<body>
    <form id="form1" runat="server" style="width: 100%">
    <div>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server"></dx:ASPxGlobalEvents>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000"></asp:ScriptManager>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"></dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta"></dx:ASPxHiddenField>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"  
                                OnDataBound="ASPxMenu1_DataBound"  OnItemClick="ASPxMenu1_ItemClick">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                <ClientSideEvents ItemClick="function(s, e) { menu_click(s,e); }" Init="function(s) {s.SetClientVisible(true);}" />
                                <ItemImage Height="32px" Width="32px"></ItemImage>
                                <SubMenuItemImage Height="16px" Width="16px"></SubMenuItemImage>
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
                                    <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%" BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <SubMenuStyle GutterWidth="17px" />
                                    </dx:ASPxMenu>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                 
                <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top">
                    <LoadingDivStyle Opacity="30"></LoadingDivStyle>
                </dx:ASPxLoadingPanel>
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
                                        </dx:ASPxLabel><br /><br />
                                        <div style="text-align: right;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"  OnClick="ButtonOk_Click2" Text="Ok">
                                                        <ClientSideEvents Click="function(s, e) { popFshi.Hide(); Utils.shfaqLoadingGif();; }" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                        <ClientSideEvents Click="function(s, e) { popFshi.Hide(); }" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <div id="dvWorkspace" style=" display:none;">
            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"   TabSpacing="3px"   Width="100%" ActiveTabIndex="0" Height="600px" >
                <ContentStyle>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />                       
                </ContentStyle>
                <TabPages >
                    <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme" >
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                <table class="renditKontrolle">
                                    <tbody>
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" ClientIDMode="AutoID" Text="Modeli:"></dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33">
                                                <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None" ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top">
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
                                                <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi"></dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dx:ASPxGridView ID="ASPxGridView_GISWorkspace" ClientInstanceName="ASPxGridView_GISWorkspace"
                                    runat="server" Width="100%" OnCustomJSProperties="ASPxGridView_GISWorkspace_CustomJSProperties"
                                    OnDataBound="ASPxGridView_GISWorkspace_DataBound" OnAfterPerformCallback="ASPxGridView_GISWorkspace_AfterPerformCallback"
                                    OnCustomCallback="ASPxGridView_GISWorkspace_CustomCallback" OnHeaderFilterFillItems="ASPxGridView_GISWorkspace_HeaderFilterFillItems">
                                    <Styles>
                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                        </Header>
                                    </Styles>
                                    <Templates>
                                        <TitlePanel>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" ToolTip="Zgjidh kolonat" 
                                                            AutoPostBack="false" ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                            <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s, e, ASPxGridView_GISWorkspace)}"
                                                                Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="pnlruaj" runat="server">
                                                            <ContentTemplate>
                                                                <dx:ASPxButton ID="ASPxButton3" runat="server" ToolTip="Ruaj kolonat" 
                                                                    AutoPostBack="true" ClientVisible="false" Image-Url="images/new/disk_blue (3).png" Font-Size="8"
                                                                    OnClick="RuajKolona_Click">
                                                                    <ClientSideEvents Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                                </dx:ASPxButton>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="gridaSelectFaqe" runat="server" ToolTip="Zgjidh te gjithe faqen" 
                                                            AutoPostBack="false" Image-Url="images/check2.png" Image-Height="16px" Font-Size="8"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) { ASPxGridView_GISWorkspace.SelectAllRowsOnPage(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe" 
                                                            AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) { ASPxGridView_GISWorkspace.SelectRows(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen"
                                                            AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) { ASPxGridView_GISWorkspace.UnselectRows(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnXlsxExport" runat="server" ToolTip="Export to Xlsx" Image-Height="16px"
                                                            Image-Url="images/xlsx24.png" Font-Size="8" UseSubmitBehavior="false" OnClick="btnXlsxExport_Click">
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnPdfExport" runat="server" ToolTip="Export to Pdf" Image-Height="16px"
                                                            Image-Url="images/pdf_icon.png" Font-Size="8" UseSubmitBehavior="false" OnClick="btnPdfExport_Click">
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                    </Templates>
                                    <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                        FocusedRowChanged="function(s, e) { onNdryshimFokusi(); }" SelectionChanged="function(s, e) { }"
                                        BeginCallback="function(s, e) {	BeginCallback(s,e); }" />
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView_GISWorkspace"
                                    ExportedRowType="Selected" />
                                <br />
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="Informacion" Text="Informacion" >                            
                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                <table id="tblInformacion" class="renditKontrolle"><tbody></tbody></table>
                                               
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtWorkspace" ID="lblWorkspace" runat="server" ClientInstanceName="lblWorkspace">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtWorkspace" runat="server" Width="100%" ClientInstanceName="txtWorkspace">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true" ValidationGroup="entries1" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtWSFunksionesh" ID="lblWSFunksionesh" runat="server" ClientInstanceName="lblWSFunksionesh">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtWSFunksionesh" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtWSFunksionesh">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtWSDefault" ID="lblWSDefault" runat="server" ClientInstanceName="lblWSDefault">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtWSDefault" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtWSDefault">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtWSOrientues" ID="lblWSOrientues" runat="server" ClientInstanceName="lblWSOrientues">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtWSOrientues" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtWSOrientues">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtWSPublik" ID="lblWSPublik" runat="server" ClientInstanceName="lblWSPublik">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtWSPublik" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtWSPublik">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNdermarrje" ID="lblNdermarrje" runat="server" Text="Ndermarrje:" ClientInstanceName="lblNdermarrje" ></dx:ASPxLabel>
                                    <dx:ASPxComboBox Width="100%" ID="cmbNdermarrje" runat="server" ClientInstanceName="cmbNdermarrje" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <LoadingPanelImage></LoadingPanelImage>
                                        <DropDownButton>
                                            <Image><SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" /></Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" ValidationGroup="entries1" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField ErrorText="*" IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxComboBox>
                                                                                        
                                    <dx:ASPxLabel Wrap="False" ID="lblNeGeoserver" runat="server" ClientInstanceName="lblNeGeoserver" Font-Underline="true"></dx:ASPxLabel>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtUrl" ID="lblUrl" runat="server" ClientInstanceName="lblUrl">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtUrl" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtUrl">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"  ValidationGroup="entries1" SetFocusOnError="True" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtUsername" ID="lblUsername" runat="server" ClientInstanceName="lblUsername">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtUsername" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtUsername">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPassword" ID="lblPassword" runat="server" ClientInstanceName="lblPassword">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtPassword" runat="server" Width="100%" ClientInstanceName="txtPassword">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" ID="lblNeSqlServer" runat="server" ClientInstanceName="lblNeSqlServer" Font-Underline="true"></dx:ASPxLabel>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtDatabase" ID="lblDatabase" runat="server" ClientInstanceName="lblDatabase">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtDatabase" runat="server" Width="100%" ClientInstanceName="txtDatabase">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPortDB" ID="lblPortDB" runat="server" ClientInstanceName="lblPortDB">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtPortDB" runat="server" Width="100%" ClientInstanceName="txtPortDB">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtUserDB" ID="lblUserDB" runat="server" ClientInstanceName="lblUserDB">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtUserDB" runat="server" Width="100%" ClientInstanceName="txtUserDB">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPasswordDB" ID="lblPasswordDB" runat="server" ClientInstanceName="lblPasswordDB">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtPasswordDB" runat="server" Width="100%" ClientInstanceName="txtPasswordDB" Password="true">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>
                                                          
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKonfirmoPasswordDB" ID="lblKonfirmoPasswordDB" runat="server" ClientInstanceName="lblKonfirmoPasswordDB">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtKonfirmoPasswordDB" runat="server" Width="100%" ClientInstanceName="txtKonfirmoPasswordDB" Password="true">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false" ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
                                    </dx:ASPxTextBox>

                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="function(s, e) { tabsActiveTabChanged(s,e); }" />
                <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
            </dxtc:ASPxPageControl >
        </div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:HiddenField ID="hfVeprimeObj" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
          
    </div>

    <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                EnableAnimation="False" PopupVerticalAlign="WindowCenter" AllowResize="True"
                AppearAfter="10" ClientIDMode="AutoID" Height="400px">
                <ClientSideEvents Closing="function(s, e) {	popupUniversal.SetContentUrl(''); }" />
                <ContentStyle>
                    <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                        PaddingTop="1px" />
                </ContentStyle>
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
