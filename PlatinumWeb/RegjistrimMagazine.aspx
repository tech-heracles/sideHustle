<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegjistrimMagazine.aspx.cs"
    Inherits="PlatinumWeb.RegjistrimMagazine" %>

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
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>



<%@ Register Src="~/ucPopUpUniversal.ascx" TagPrefix="ucPopUp" TagName="popUpUniversal" %>
<%@ Register Src="~/ucPopUpKonvertimi.ascx" TagPrefix="ucPopUpKonvertimi" TagName="popUpKonvertimi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>
<%@ Register Src="~/ucPopUpKlonimi.ascx" TagPrefix="ucPopUpKlonimi" TagName="ucPopUpKlonimi" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <title>Alpha Web</title>

    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/RegjistrimMagazine.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">          
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600"></asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server"></dx:ASPxGlobalEvents>
        <div style="width: 100%">              
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaGjitheDok" runat="server" />   
                    <asp:HiddenField ID="hfStatusi" ClientIDMode="Static" runat="server" />           
                    <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"></dx:ASPxHiddenField>
                    <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva"></dx:ASPxHiddenField>              
                    <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server"  Visible="true" OnMenuClick="Menu_ItemClick" OnMenuTemplate="PercaktoTemplateMenu" OnFilterSave="Ruaj_ASPxButton_Click" OnFilterDelete="FshiFilter_ASPxButton_Click"/>
                    <ucPopUpKonvertimi:popUpKonvertimi ID="popUpKonvertimi" runat="server"/> 
                    <ucPopUpKlonimi:ucPopUpKlonimi runat="server" ID="ucPopUpKlonimi"/>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
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
        <asp:UpdatePanel ID="pnlKryesor" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfLloji" runat="server" />
                <dx:ASPxLabel ID="pergjigja" runat="server" Text="" EncodeHtml="False" ForeColor="Green"
                    ClientVisible="false">
                </dx:ASPxLabel>
                <br />
                <dx:ASPxGridView ID="grid_RegMag" ClientInstanceName="grid_RegMag" runat="server" ToolTip="Dokumenta Magazine"
                    Width="100%" OnDataBound="grid_RegMag_DataBound"
                    OnCustomCallback="grid_RegMag_CustomCallback" OnCustomJSProperties="grid_RegMag_CustomJSProperties"
                    OnProcessColumnAutoFilter="grid_RegMag_ProcessColumnAutoFilter" OnHeaderFilterFillItems="grid_RegMag_HeaderFilterFillItems">
                  
                    <Styles>
                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                        </Header>
                    </Styles>
                    <SettingsPager PageSize="15">
                    </SettingsPager>
                    <ClientSideEvents RowDblClick="function(s, e) {
                OnGridDoubleClick(e,e.visibleIndex); }"
                        BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                    <StylesEditors>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                </dx:ASPxGridView>
              
                <iframe id="Container" runat="server" frameborder="0" height="0" name="Container"
                    width="0"></iframe>
            </ContentTemplate>
        </asp:UpdatePanel>     
         
        <ucPopUp:popUpUniversal runat="server" ID="ucPopUpUniversal"></ucPopUp:popUpUniversal>
    </form>
</body>
</html>
