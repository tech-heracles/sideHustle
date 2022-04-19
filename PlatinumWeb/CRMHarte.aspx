<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMHarte.aspx.cs" Inherits="PlatinumWeb.CRMHarte" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>








<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha CRM</title>
      <link type="text/css" rel="stylesheet" href="AlphaCRM.css" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="//maps.googleapis.com/maps/api/js?key=AIzaSyACrrRhtdTbsjW6rvdFyl-7mtFWeJL60R0&sensor=false" type="text/javascript"></script>
     <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/srcCRM/js/jquery.mmenu.min.all.js;~/js/srcGIS/jsGoogle/markerwithlabel.js;~/js/aspx.js/CRMHarte-IMB.5.3.3.js&v49"
        type="text/javascript"></script>
    <%--    <script type="text/javascript"  src='https://www.google.com/jsapi'></script>--%>
    <link href="stileShtoPike.css" rel="stylesheet" type="text/css" />
</head>
<body>


    <%--  <div class="content"  >--%>
    <form id="form1" runat="server">
         
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000" EnablePartialRendering="true">
        </asp:ScriptManager>
          <asp:UpdatePanel ID="panelMenu" runat="server">
            <ContentTemplate>
                <dx:ASPxMenu ID="ASPxMenuToolBar" ClientInstanceName="ASPxMenuToolBar" runat="server" 
                    ItemImagePosition="Top" Width="100%" Height="100%"
                    ShowPopOutImages="True" 
                    EnableCallBacks="True" EnableClientSideAPI="True" SyncSelectionMode="None" ClientIDMode="AutoID">
                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                    <ClientSideEvents ItemClick="function(s, e) { menu_click(s,e); }" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="dvMenu" style="display: none">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                <clientsideevents init="init_MenuInfo" />
                                <itemsubmenuoffset firstitemx="2" lastitemx="2" x="2" />
                                <submenustyle gutterwidth="17px" />
                            </dx:ASPxMenu>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxCallbackPanel EnableHierarchyRecreation="false" ID="callbackPanel" ClientInstanceName="callbackPanel" runat="server" Width="100%">
            <ClientSideEvents EndCallback="initialize" />
            <PanelCollection>
                <dx:PanelContent>
                     <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
            ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
             
        <dx:ASPxNavBar ID="navBarFiltrat" runat="server" ClientInstanceName="navBarFiltrat"
            Width="100%" Font-Bold="True" CssPostfix="DevEx" ClientIDMode="AutoID"  >
            <LoadingPanelImage />
            <Groups>
                <dx:NavBarGroup Name="filtraKryesor" Text="Filtra kryesorë" HeaderStyle-Font-Bold="true"
                    HeaderStyle-Font-Size="14px" HeaderStyle-ForeColor="Gray">
                    <HeaderStyle Font-Bold="True" Font-Size="14px" ForeColor="Gray"></HeaderStyle>
                    <ContentTemplate>

                        <div id="dateDokumenti">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="tdStyle6">
                                        <dx:ASPxLabel ID="lblDtDok" ClientInstanceName="lblDtDok" runat="server" Text="Datë dokumenti"
                                            Style="font-weight: 700; color: #0072c6">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="tdStyle8">
                                        <dx:ASPxRadioButtonList ID="radDtDok" ClientInstanceName="radDtDok" Font-Size="12px"
                                            Font-Bold="true" ForeColor="#0072c6" runat="server" RepeatColumns="4" CssClass="Glass"
                                            CssPostfix="Glass" Height="16px" EnableClientSideAPI="true" Border-BorderStyle="None">
                                            <ClientSideEvents ValueChanged="ValueChanged_radDtDok"
                                                Init="Init_radDtDok" />
                                            <Items>
                                                <dx:ListEditItem Text="Aktuale" Value="Aktuale" Selected="true" />
                                                <dx:ListEditItem Text="Periudha" Value="Periudha" />
                                                <dx:ListEditItem Text="Viti Ushtrimor" Value="VitiUshtrimor" />
                                                <dx:ListEditItem Text="Gjithe Vitet" Value="GjitheVitet" />
                                            </Items>
                                        </dx:ASPxRadioButtonList>
                                    </td>
                                    <td class="tdStyle9">
                                        <dx:ASPxLabel ID="lblNgaDok" runat="server" Text="Nga" Style="font-weight: 700; color: #0072c6">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="tdStyle7">
                                        <dx:ASPxDateEdit ID="txtNgaDok" ClientEnabled="false" runat="server" TabIndex="10"
                                            AutoPostBack="false" AllowNull="false" DateOnError="Today" EditFormat="Custom"
                                            EditFormatString="dd/MM/yyyy" Date="2009-05-06" ValidationSettings-CausesValidation="True"
                                            ClientInstanceName="txtNgaDok" CssPostfix="Glass" Height="16px">
                                            <ClientSideEvents Init="function (s,e){initNgaDok(s,e);}" DateChanged="function (s,e){ngaDokDateChanged(s,e);}" />
                                            <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="4px" />
                                            </CalendarProperties>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings CausesValidation="True">
                                                <ErrorImage Height="14px" Width="14px" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td class="tdStyle9">
                                        <dx:ASPxLabel ID="lblDeriDok" runat="server" Text="Deri" Style="font-weight: 700; color: #0072c6">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="tdStyle7">
                                        <dx:ASPxDateEdit ID="txtDeriDok" ClientEnabled="false" runat="server" TabIndex="11"
                                            AutoPostBack="false" AllowNull="false" DateOnError="Today" EditFormat="Custom"
                                            EditFormatString="dd/MM/yyyy" Date="2009-04-29" ClientInstanceName="txtDeriDok"
                                            CssPostfix="Glass" Height="16px">
                                            <ClientSideEvents DateChanged="function (s,e){deriDokDateChanged(s,e);}" />
                                            <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="4px" />
                                            </CalendarProperties>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings CausesValidation="True">
                                                <ErrorImage Height="14px" Width="14px" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </dx:NavBarGroup>
                <dx:NavBarGroup Name="filtraAvancuar" Text="Filtra të avancuar" HeaderStyle-Font-Bold="false"
                    HeaderStyle-ForeColor="Gray" HeaderStyle-Font-Size="14px" Expanded="false" HeaderStyleCollapsed-VerticalAlign="Top">
                    <HeaderStyle Font-Bold="True" Font-Size="14px" ForeColor="Gray"></HeaderStyle>

<HeaderStyleCollapsed VerticalAlign="Top"></HeaderStyleCollapsed>
                    <ContentTemplate>
                    
                       <div id="AgjentiShitjes">

                                <table style="width: 40%;">
                                    <tr>
                                        <td style="width:50%">
                                            <dx:ASPxLabel ID="lblAgjentShitje" Width="100%" runat="server"  Text="Agjenti i Shitjes:">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td style="width:50%" >
                                           <dx:ASPxComboBox runat="server" ID="cmbAgjentShitjesh"  Width="100%" ClientInstanceName="cmbAgjentShitjesh" OnItemRequestedByValue="cmbAgjentShitjesh_ItemRequestedByValue" ValueType="System.String">
                                                  <ClientSideEvents ButtonClick=" function(s,e) {ButtonClickedbtnAgjentShitje(s);
                                                }" />
                                            </dx:ASPxComboBox>
                                
                                        </td>
                                <%--        <td class="tdStyle6">
                                            <dx:ASPxComboBox ID="cmbVeprimiAgjShitje1" Width="80%" ClientInstanceName="cmbVeprimiAgjShitje1"
                                                runat="server" SelectedIndex="0" HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" CssPostfix="Glass" ValueType="System.String"
                                                Height="25px" DropDownRows="8">
                                                <LoadingPanelImage>
                                                </LoadingPanelImage>
                                                <Items>
                                                    <dx:ListEditItem Text="=" Value="0" />
                                                    <dx:ListEditItem Text="&lt;" Value="1" />
                                                    <dx:ListEditItem Text="&gt;" Value="2" />
                                                    <dx:ListEditItem Text="i ndryshem" Value="3" />
                                                    <dx:ListEditItem Text="fillon me" Value="4" />
                                                    <dx:ListEditItem Text="mbaron me" Value="5" />
                                                    <dx:ListEditItem Text="permban" Value="6" />
                                                    <dx:ListEditItem Text="ben pjese ne" Value="7" />
                                                </Items>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Width="14px" />
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                             
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td class="tdStyle6">
                                            <dx:ASPxButtonEdit ID="txtbtnAgjentShitje1" Width="80%" runat="server" CssPostfix="Glass"
                                                ClientInstanceName="txtbtnAgjentShitje1" ValueType="System.String" Height="23px">
                                                <ClientSideEvents ButtonClick=" function(s,e) {ButtonClickedbtnAgjentShitje(s);}"
                                                    GotFocus="function(s,e) {selectBenPjeseNe(s,e, cmbVeprimiAgjShitje1);}" KeyDown="function(s,e) {selectBenPjeseNe(s,e, cmbVeprimiAgjShitje1);}" />
                                                <Buttons>
                                                    <dx:EditButton>
                                                    </dx:EditButton>
                                                </Buttons>
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Width="14px" />
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxButtonEdit>
                                        </td>
                                        <td class="tdStyle6">
                                            <dx:ASPxComboBox ID="cmbLidhesaAgjShitje" Width="80%" ClientInstanceName="cmbLidhesaAgjShitje"
                                                runat="server" SelectedIndex="0" CssPostfix="Glass" ValueType="System.String"
                                                Height="21px" DropDownRows="3">
                                                <LoadingPanelImage>
                                                </LoadingPanelImage>
                                                <ClientSideEvents ValueChanged="function(s, e) {lidhes_valueChanged1(s,e,cmbVeprimiAgjShitje2,txtbtnAgjentShitje2);}" />
                                                <Items>
                                                    <dx:ListEditItem Text=" " Value="0" />
                                                    <dx:ListEditItem Text="dhe" Value="and" />
                                                    <dx:ListEditItem Text="ose" Value="or" />
                                                </Items>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Width="14px" />
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td class="tdStyle6">

                                            <dx:ASPxComboBox ID="cmbVeprimiAgjShitje2" Width="80%" ClientInstanceName="cmbVeprimiAgjShitje2"
                                                runat="server" SelectedIndex="0" HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" ValueType="System.String" CssPostfix="Glass"
                                                Height="24px" ClientSideEvents-Init="function(s,e){filter2_Init(s,e,cmbLidhesaAgjShitje);}">
                                                <LoadingPanelImage>
                                                </LoadingPanelImage>
                                                <Items>
                                                    <dx:ListEditItem Text="=" Value="0" />
                                                    <dx:ListEditItem Text="&lt;" Value="1" />
                                                    <dx:ListEditItem Text="&gt;" Value="2" />
                                                    <dx:ListEditItem Text="i ndryshem" Value="3" />
                                                    <dx:ListEditItem Text="fillon me" Value="4" />
                                                    <dx:ListEditItem Text="mbaron me" Value="5" />
                                                    <dx:ListEditItem Text="permban" Value="6" />
                                                </Items>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Width="14px" />
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td class="tdStyle6">
                                            <dx:ASPxButtonEdit ID="txtbtnAgjentShitje2" Width="80%" runat="server" CssPostfix="Glass"
                                                ClientInstanceName="txtbtnAgjentShitje2" ValueType="System.String" Height="23px">
                                                <ClientSideEvents ButtonClick=" function(s,e) {ButtonClickedbtnAgjentShitje(s);
                                                }" />
                                                <Buttons>
                                                    <dx:EditButton>
                                                    </dx:EditButton>
                                                </Buttons>
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Width="14px" />
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxButtonEdit>
                                        </td>--%>
                                    </tr>
                                </table>
                            </div>
                    </ContentTemplate>
                </dx:NavBarGroup>
            </Groups>
        </dx:ASPxNavBar>
        <table style="width: 100%">
   
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <%-- harta --%>
                                <div id="map" style="width: 100%; height: 800px"></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView_Detyrat"
                    ExportedRowType="Selected" />
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfKontrollet" runat="server" />
                <asp:HiddenField ID="hfStatusi" runat="server" />
                <asp:HiddenField ID="hfKodi" runat="server" />
                <asp:HiddenField ID="hfEmertimi" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <asp:HiddenField ID="hfLupaAutorizimi" runat="server" />
                <asp:HiddenField ID="hfDataVlefshmerie" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfNrAutoDet" runat="server" ClientInstanceName="hfNrAutoDet">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
        </dx:ASPxHiddenField>
                         </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel >
        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
            CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
            <ClientSideEvents Closing="closing" />
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl >

    </form>
    <%-- </div>--%>
    <%--     <nav id="menu">
            <ul id="ulMenu">
            </ul>
        </nav>--%>
</body>
</html>