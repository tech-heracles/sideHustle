<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ABRaporti.aspx.cs" Inherits="PlatinumWeb.ABRaporti" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
        <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/ABRaporti.aspx-IMB.5.4.js&v76""
        type="text/javascript"></script>
    <style>
        .style9
        {
            width: 66%;
        }

        .style13
        {
            font-weight: 700;
            color: #0072c6;
        }

        td.tdStyle6
        {
            width: 16%;
        }

        td.tdStyle7
        {
            width: 11%;
        }

        td.tdStyle8
        {
            width: 30%;
        }

        td.tdStyle9
        {
            width: 5%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>

        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
          
        </asp:ScriptManager>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server"
                                ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                SeparatorWidth="1px">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                <ClientSideEvents ItemClick="ItemClickMenu" Init="function(s) {s.SetClientVisible(true);}" />
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
                            <div id="dvMenu" style="display: none">
                                <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                            BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                            <ClientSideEvents Init="MenuInfoInit" />
                                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <SubMenuStyle GutterWidth="17px" />
                                        </dx:ASPxMenu>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
       
         <dx:ASPxNavBar ID="navBarFiltrat" runat="server" ClientInstanceName="navBarFiltrat"
            Width="100%" Font-Bold="True" CssPostfix="DevEx" ClientIDMode="AutoID">
            <LoadingPanelImage />
            <Groups>
                <dx:NavBarGroup Name="filtraAvancuar" Text="Filtra" HeaderStyle-Font-Bold="false" 
                    HeaderStyle-ForeColor="Gray" HeaderStyle-Font-Size="14px" Expanded="true" HeaderStyleCollapsed-VerticalAlign="Top">
                    <HeaderStyle Font-Bold="True" Font-Size="14px" ForeColor="Gray"></HeaderStyle>
                    <ContentTemplate>
                        <div id="filtraAvanc">
                             <div id="dvAktiviteti" style="width: 100%">
                         <table id="tblPasqyra" class="renditKontrolle">
                            <tbody>
                                <tr>
                         
                                    <td class="tdStyle6">
                                 <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbPeriudha" ID="lblPeriudha" runat="server" CssClass="style13"
                                    Text="Periudha" ClientInstanceName="lblPeriudha">
                                </dx:ASPxLabel>
                                                         </td>
                                    <td class="tdStyle6">   
                                <dx:ASPxComboBox ID="cmbPeriudha" runat="server"  EnableSynchronization="True" Width="100%" AutoPostBack="false" ClientInstanceName="cmbPeriudha">
                                    <ClientSideEvents TextChanged="function(s, e){FiltroRaport();}"/>
                                    <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true"
                                        Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                  
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                    </DisabledStyle>
                                </dx:ASPxComboBox>
                                        </td>
                                     <td class="tdStyle6"></td>
                                     <td class="tdStyle6"></td>
                                     <td class="tdStyle6"></td>
                                     <td class="tdStyle6"></td>
                                </tr>
                           </tbody>
                        </table>
                    </div>
                    <div id="dvAktiviteti2" style="width: 100%">
                        <table id="tblPasqyra2" class="renditKontrolle">
                            <tbody>
                                 <tr>
                                     <td class="tdStyle6">
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbNdermarrje" ID="lblNdermarrja" runat="server" CssClass="style13"
                                            Text="Ndermarrja" ClientInstanceName="lblNdermarrja">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="tdStyle6">   
                                        <dx:ASPxComboBox ID="cmbNdermarrje" runat="server"  EnableSynchronization="True" Width="100%" AutoPostBack="false" ClientInstanceName="cmbNdermarrje">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickNdermarrje(s);}" TextChanged="function(s, e){FiltroRaport();}"/>
                                            <ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true"
                                                Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                  
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="tdStyle6"></td>
                                    <td class="tdStyle6"></td>
                                    <td class="tdStyle6"></td>
                                    <td class="tdStyle6"></td>

                                 </tr>          
                                </tbody>
                            </table>
                            </div>
                        </div>
                    </ContentTemplate>
                 </dx:NavBarGroup>
            </Groups>
            <LoadingPanelStyle ImageSpacing="5px">
            </LoadingPanelStyle>
        </dx:ASPxNavBar>

            <dx:ASPxGridView ID="gvRaporti"  ClientInstanceName="gvRaporti" width="2500px"
                 ToolTip="Raporti" runat="server" style="overflow-x:auto;"
      OnHtmlFooterCellPrepared="gvRaporti_HtmlFooterCellPrepared"
                OnCustomCallback="gvRaporti_CustomCallback"  OnDataBound="gvRaporti_DataBound" >
                <ClientSideEvents EndCallback="gvEndCallback" />

                <Styles>
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                </Styles>
                <SettingsPager PageSize="15">
                </SettingsPager>
                <ClientSideEvents />
                <StylesEditors>
                    <CalendarHeader Spacing="1px">
                    </CalendarHeader>
                    <ProgressBar Height="25px">
                    </ProgressBar>
                </StylesEditors>

                 <Templates>
                    <StatusBar>
                    </StatusBar>
                    <FooterCell></FooterCell>
                      
                                         <GroupRowContent>
                                                 <%# Container.Column.FieldName  %> : <%# Container.GroupText %>
                
                </GroupRowContent>
                                       
                </Templates>
                 <Settings  ShowGroupFooter="VisibleIfExpanded" />
                                        <SettingsBehavior AutoExpandAllGroups="true" />
            </dx:ASPxGridView>
          <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
            CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
            <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');
}" />
               <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
              </dx:ASPxPopupControl>       
        <asp:HiddenField ID="HfKonfAmb" runat="server" />
        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
        <asp:HiddenField ID="hfKolonaGride" runat="server" />
        <asp:HiddenField ID="hfKonffillestar" runat="server" />
        <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
        <asp:HiddenField ID="hfGridaKodi" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
    </form>
</body>
</html>

