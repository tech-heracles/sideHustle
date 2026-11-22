<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShperndaDhurate.aspx.cs" Inherits="PlatinumWeb.ShperndaDhurate" %>
<%@ Register TagPrefix="avec" Namespace="PlatinumWeb" Assembly="PlatinumWeb" %>

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


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AVEC Accounting</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/ShperndaDhurate.aspx-IMB.5.4.js&v76""
        type="text/javascript"></script>
    <style type="text/css">
        .auto-style3 {
            width: 146px;
        }
        .auto-style5 {
            height: 21px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <avec:AvecToolbar ID="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound" OnItemClick="ASPxMenu1_ItemClick" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvDhurata" >
            <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server"   TabSpacing="3px"
                ClientInstanceName="PageControl" Width="100%" ActiveTabIndex="1">
                <ContentStyle>
                    <Border BorderColor="#F9E7E1" BorderStyle="Solid" BorderWidth="1px" />
                </ContentStyle>
                <TabPages>
                                
                    <dxtc:TabPage Name="Dhurata" Text="Dhurata Karta Klienti">

                        <ContentCollection>
                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                <div id="konfigurimi"  class="atributeDiveFshehur">
                                    <table class="renditKontrolle">
                                        <tbody>
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server" Style="font-size: large" Text="Modeli:">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33">
                                                    <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" Width="100%">
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td class="renditKontrolleLabelMeWidth33">
                                                    <dx:ASPxLabel ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33"></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <table id="tblDhurata" class="renditKontrolle">
                                    <tbody>
                                  <tr>  <td class="auto-style3"> 
                               <dx:ASPxLabel Wrap="False" ID="lblKarta" AssociatedControlID="cmbKarta"
                                            runat="server" Text="Karta:" ClientInstanceName="lblKarta">
                                            </dx:ASPxLabel> </td>
                                      <td>
                                            <dx:ASPxComboBox ID="cmbKarta" Width="100%" runat="server" ClientInstanceName="cmbKarta"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top"  OnItemRequestedByValue="cmbKarta_ItemRequestedByValue" DropDownStyle="DropDown"  >
                                            <ClientSideEvents TextChanged="function(s,e) {TextChangedKarta();}"/>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                                 </td><td>
                                          &nbsp;</td></tr>  
                                        <tr> <td class="auto-style5">
                                              <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKlienti" ID="lblKlienti" runat="server" Text="Klienti:" ClientInstanceName="lblKlienti">
                                </dx:ASPxLabel>
                                             </td>
                                            <td class="auto-style5">

                                         <dx:ASPxTextBox ID="txtKlienti" ClientInstanceName="txtKlienti" runat="server" Width="100%" style="margin-left: 0px" Enabled="True" >      
                                                   <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                               </DisabledStyle>
                                             </dx:ASPxTextBox>
                                            </td><td>
                                          &nbsp;</td></tr> 
                                         
                                      <tr> <td class="auto-style5">
                                              <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPike" ID="lblPike" runat="server" Text="Gjendje Pikesh:" ClientInstanceName="lblPike">
                                </dx:ASPxLabel>
                                             </td>
                                            <td class="auto-style5">

                                         <dx:ASPxTextBox ID="txtPike" ClientInstanceName="txtPike" runat="server" Width="100%" style="margin-left: 0px" Enabled="True" >    
                                           <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                         </dx:ASPxTextBox>         
                                            </td><td>
                                          &nbsp;</td></tr>                                                        
                                  <tr><td class="auto-style3">
                                 <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbDhurata" ID="lblDhurata" runat="server" Text="Zbrit Pike:" ClientInstanceName="lblDhurata">
                                </dx:ASPxLabel>
                                 </td><td >
                              
                                           <dx:ASPxComboBox ID="cmbDhurata" Width="100%" runat="server" ClientInstanceName="cmbDhurata" 
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top"  OnItemRequestedByValue="cmbDhurata_ItemRequestedByValue" >
                                            
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                   </td>
                                   <td>
                              
                                 </td>
                              </tr>
                              </tbody></table>
                                   

                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
       
            </dxtc:ASPxPageControl >
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                        
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:HiddenField ID="hfRuaj" runat="server" />  
                <asp:HiddenField ID="hfKarta" runat="server" />       
                
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
      
    </form>
</body>
</html>



