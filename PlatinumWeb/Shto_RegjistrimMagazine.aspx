<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_RegjistrimMagazine.aspx.cs" Inherits="PlatinumWeb.Shto_RegjistrimMagazine" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxnb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ucPopUpKlonimi.ascx" TagPrefix="ucPopUpKlonimi" TagName="ucPopUpKlonimi" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="js/css/le-frog/jquery-ui.css" media="screen" rel="stylesheet" type="text/css"
        runat="server" id="themeJQuery" />
    <%--     <link href="js/jquery-ui-1.11.1.min.css" media="screen" rel="stylesheet" type="text/css"
        runat="server" id="Link1" />--%>
    
    <link href="fine-uploader/fine-uploader-new.css" rel="stylesheet"/>
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/jquery.ui.datepicker-sq.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/myMesazh-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/memoryObject.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/async.min.js;~/js/myNrAuto-IMB.2.1.js;~/js/json2.js;~/JsGlobal.js;~/js/myCookies-IMB.2.1.js;~/fine-uploader/jquery.fine-uploader.js;~/js/multiOpenAccordion-IMB.2.1.js;
~/js/toolbar.js;~/js/aspx.js/Shto_RegjistrimMagazine.aspx-IMB.2.1.js&v76"" type="text/javascript"></script>

</head>
<body>
    <!-- #include file="~/fine-uploader/templates/imb-default.html" -->
    <form id="form1" runat="server">
          
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <div style="width: 100%; height: 100%">
            <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
           </asp:ScriptManager>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
<%--                <ClientSideEvents EndCallback="function(s, e) { EndCallbackGlobalEvents(s, e); }" />--%>
            </dx:ASPxGlobalEvents>
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"
                ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
            <asp:HiddenField ID="hfTmpColMag" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                    OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                    <ClientSideEvents ItemClick="function(s, e) { menu_click(s, e); }" Init="function(s) {s.SetClientVisible(true);}" />
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
                                                <ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}" />
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
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popMesazhQK" runat="server" AllowDragging="true" ClientIDMode="AutoID"
                        ClientInstanceName="popMesazhQK" CloseAction="CloseButton" EnableAnimation="false"
                        ShowCloseButton="false" EnableViewState="false" Font-Bold="true" HeaderText="Kujdes"
                        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        Width="300px">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl7" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel17" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent17" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="False" ID="lblMsgbox4" runat="server" ClientIDMode="AutoID"
                                                Text="Deshironi te beni shperndarjen ne qendrat e kostos?">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOkQK" runat="server" CausesValidation="false" ClientInstanceName="ButtonOkQK"
                                                                AutoPostBack="false" Text="Po">
                                                                <ClientSideEvents Click="function(s, e){ ButtonOkQKClick(s, e); } " />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancelQK" runat="server" ClientIDMode="AutoID" Text="Jo"
                                                                AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s, e) { popMesazhQK.Hide(); }" />
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
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popKonvertim" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                        ClientInstanceName="popKonvertim" CloseAction="CloseButton" CssPostfix="Glass"
                        EnableAnimation="False" EnableViewState="False" Font-Bold="true" HeaderText="Kujdes"
                        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        Width="400px">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl14" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel11" runat="server" ClientIDMode="AutoID" Width="600px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent11" runat="server" SupportsDisabledAttribute="True">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="lblKonvertoNe" runat="server" ClientIDMode="AutoID" Text="Konverto Ne:"
                                                            Wrap="False">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="cmbKonverto" runat="server" ClientInstanceName="cmbKonverto">
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ndryshoNiveli(s,e);
}" />
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel ID="lblKonfig" runat="server" ClientIDMode="AutoID" Text="Lloji:" Wrap="False">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="cmbKonf" runat="server" ClientInstanceName="cmbKonf">
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonOk2" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk2"
                                                            Text="Konverto">
                                                            <ClientSideEvents Click="function(s, e) {   konverto();
	popKonvertim.Hide();

}" />
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
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popKonvertuar" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                        ClientInstanceName="popKonvertuar" CloseAction="CloseButton" EnableAnimation="False"
                        EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" Width="300px">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl433" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel133" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent133" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="True" ID="lblMsgboxKonv" runat="server" ClientInstanceName="lblMsgboxKonv" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOk5" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                OnClick="ButtonOk5_Click" Text="Po">
                                                                <ClientSideEvents Click="function(s, e) {
	popKonvertuar.Hide();
     Utils.shfaqLoadingGif();
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancel5" runat="server" ClientIDMode="AutoID" Text="Jo" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s, e) {
		popKonvertuar.Hide();
                                                                    click = false;
}" />
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
                    <ucPopUpKlonimi:ucPopUpKlonimi runat="server" ID="ucPopUpKlonimi"/>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Red" ClientInstanceName="pergjigja"
                        ClientVisible="false">
                    </dx:ASPxLabel>
                    <asp:HiddenField ID="status1" runat="server" Value="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
                    <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo" />
                    <asp:HiddenField ID="hfUrl" runat="server" />
                    <asp:HiddenField ID="hfKategoria" runat="server" />
                    <asp:HiddenField ID="hfKodi" runat="server" />
                    <asp:HiddenField ID="hfEmertimi" runat="server" />
                    <asp:HiddenField ID="hfDetajimet" runat="server" />
                    <asp:HiddenField ID="hfNjesia" runat="server" />
                    <asp:HiddenField ID="hfSasia" runat="server" />
                    <asp:HiddenField ID="hfCmimi" runat="server" />
                    <asp:HiddenField ID="hfMagazina" runat="server" />
                    <asp:HiddenField ID="hfVlefta" runat="server" />
                    <asp:HiddenField ID="hfMagazina2" runat="server" />
                    <asp:HiddenField ID="hfSkemaKontabel" runat="server" />
                    <asp:HiddenField ID="hfDetajimetSelektuara" runat="server" />
                    <asp:HiddenField ID="hfLupaKlientFurnitor" runat="server" />
                    <asp:HiddenField ID="hfLupaMagazina" runat="server" />
                    <asp:HiddenField ID="hfLupaNjesiVartese" runat="server" />
                    <asp:HiddenField ID="hfRuajDraft" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfAutorizimi" runat="server" />
                    <asp:HiddenField ID="gridDataObject" runat="server" />
                    <asp:HiddenField ID="proveObjekt2" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hfTeDrejtaInfoArt" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaImportoSeriale" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaArtRi" runat="server" />
            <asp:HiddenField ID="HfKonfAmb" runat="server" />
            <asp:HiddenField ID="HfColNjesiArt" runat="server" />
            <asp:HiddenField ID="HfColNjesAdminis" runat="server" />
            <asp:HiddenField ID="HfColNjesAdminisDest" runat="server" />
            <asp:HiddenField ID="HfColTrupMag" runat="server" />
            <asp:HiddenField ID="HfColDetArt" runat="server" />
            <asp:HiddenField ID="HfColKodbare" runat="server" />
            <asp:HiddenField ID="HfKodbari" runat="server" />
            <asp:HiddenField ID="HfColDetArt2" runat="server" />
            <asp:HiddenField ID="HfColArt" runat="server" />  <asp:HiddenField ID="HfColArtSet" runat="server" />
            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HFStatusiDokumentit" runat="server" />
            <asp:HiddenField ID="hfKolonaGride" runat="server" />
            <asp:HiddenField ID="HfGridCol" runat="server" />
            <asp:HiddenField ID="hfKontabilizimi" runat="server" />
            <asp:HiddenField ID="hfKontrollRivleresim" runat="server" />
            <asp:HiddenField ID="hfKonffillestar" runat="server" />
            <asp:HiddenField ID="hfLlogaria" runat="server" />
            <dx:ASPxHiddenField ID="hfArt" runat="server" ClientInstanceName="HfArt">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfSeriale" runat="server" ClientInstanceName="hfSeriale">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfSasiSeriale" runat="server" ClientInstanceName="hfSasiSeriale">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfIdGride" runat="server" ClientInstanceName="hfIdGride">
            </dx:ASPxHiddenField>

            <asp:HiddenField ID="hfArkivaDokId" runat="server" />
            <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
            </dx:ASPxHiddenField>

            <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
            <asp:HiddenField ID="hfGridaKodi" runat="server" />
            <asp:HiddenField ID="hfGridaDetajimi" runat="server" />
            <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
            <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
            <asp:HiddenField ID="hfHapurMbyllur" runat="server" />
            <asp:HiddenField ID="hfLupaAutomjet" runat="server" />
            <asp:HiddenField ID="hfId" runat="server" />
            <asp:HiddenField ID="hfLupaTransportues" runat="server" />


            <dxcb:ASPxCallbackPanel ID="serialetCallbackPanel" runat="server"  OnCallback="serialetCallbackPanel_Callback" SettingsLoadingPanel-Enabled="false">
                <ClientSideEvents BeginCallback="function(s,e){BeginCallback(s,e);}"  EndCallback="function(s,e){SkedariUploaded(s,e);}" />
            </dxcb:ASPxCallbackPanel>

            <div class="container bootstrap-iso">
              <!-- Modal -->
              <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">
                  <!-- Modal content-->
                  <div class="modal-content">
                    <div class="modal-header">
                      <button type="button" class="close" data-dismiss="modal">&times;</button>
                      <h4 class="modal-title">Ngarko skedaret e serialeve</h4>
                    </div>
                    <div class="modal-body">
                           <div class="form-group">    
                       <label>Kategori Seriali</label>
                               <select id="selKategoriSeriali" class="form-control">
                                    <option></option>
                               </select>
               
                     <div id="fine-uploader-validation"></div>
                              </div>
                    </div>
                    <div class="modal-footer">
                <div class="form-group">
                       <button type="button" id="buttonNgarko" class="btn btn-default" data-dismiss="modal">Ngarko</button>
                      <button type="button" class="btn btn-default" data-dismiss="modal">Mbyll</button>
                        </div>
                    </div>
                  </div>
      
                </div>
              </div>
            </div>
            <dx:ASPxSplitter EnableHierarchyRecreation="false"  ID="ASPxSplitter1" runat="server" Width="100%" ClientInstanceName="splitter">
                <Panes>
                    <%-- Header pane--%>
                    <dx:SplitterPane PaneStyle-BackColor="Transparent" Name="mainMagazina" Separators-Size="10px" ScrollBars="Auto">
                        <Separators Size="10px">
                        </Separators>

<PaneStyle BackColor="Transparent"></PaneStyle>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                <asp:Panel ID="ContentPanel" runat="server">
                                    <asp:UpdatePanel ID="pnlLidhur" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table id='hl' runat="server">
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div id="accordition">
                                        <div>
                                            <h3 id="kokeKonfigurimi"> <span class="ui-not-accordion-header-text">Koke Dokumenti: </span></h3>
                                            <div>
                                                <table id="tblFillim" class="renditKontrolle">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div>
                                            <h3 id="trupKonfigurimi"><span class="ui-not-accordion-header-text">Trup Dokumenti</span></h3>
                                            <div>
                                                <div id="divgride1" style="display: none">
                                                    <div id="divgride2">
                                                        <table id="rowed5">
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <h3 id="fundKonfigurimi"><span class="ui-not-accordion-header-text">Fund Dokumenti</span></h3>
                                            <div>
                                                <table id="tblFund" class="renditKontrolle" align="right">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div id="dvFillim" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" ID="lblLloji" Text="boo" AssociatedControlID="cmbLloji"
                                            runat="server" ClientInstanceName="lblLloji">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="cmbLloji" runat="server" ClientInstanceName="cmbLloji"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){TextChangedLloji(true);}" Init="function(s,e){TextChangedLloji(false);}" />

<SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="konfigurimi_Label" AssociatedControlID="cmbKonfigurimi"
                                            runat="server" Text="Lloji:" ClientInstanceName="konfigurimi_Label">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" DropDownHeight="100" AnimationType="None">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin(true)}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>

<SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

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
                                        <div id="dvlblKonfigurimi">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" BackColor="white"
                                                            ClientInstanceName="lblKonfigurimi"  Text="">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <dx:ASPxLabel Wrap="False" ID="lblKlientFurnitori" AssociatedControlID="btneKlientFurnitori"
                                            ClientInstanceName="lblKlientFurnitori" runat="server" Text="Furnitori">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="btneKlientFurnitori" runat="server" ClientInstanceName="btneKlientFurnitori"
                                            ShowShadow="False" ValueType="System.Int32" IncrementalFilteringMode="Contains"
                                            EnableSynchronization="True" EnableCallbackMode="True" DropDownRows="3" CallbackPageSize="3"
                                            OnItemRequestedByValue="btneKlientFurnitori_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneKlientFurnitori_ItemsRequestedByFilterCondition"
                                            SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickFurnitori();}" SelectedIndexChanged="function (s,e){KlientFurnitoriChanged()}"
                                                GotFocus="function(s, e){s.SelectAll();}" />

<SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

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
                                        <dx:ASPxLabel Wrap="False" ID="lblMagazina" AssociatedControlID="btneMagazina" runat="server"
                                            Text="Magazina" ClientInstanceName="lblMagazina">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="btneMagazina" ClientInstanceName="btneMagazina"
                                            runat="server" ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top"
                                             EnableSynchronization="True" OnItemRequestedByValue="btneMagazina_ItemRequestedByValue" 
                                            OnItemsRequestedByFilterCondition="btneMagazina_ItemsRequestedByFilterCondition" EnableCallbackMode="True"
                                            EnableClientSideAPI="True" CallbackPageSize="10" IncrementalFilteringMode="Contains" ValueType="System.Int64">
                                            <ClientSideEvents ButtonClick="function(s,e){identifikuesMagazina='Mag1';ButtonClickMagazina();}"
                                                TextChanged="function(s,e){identifikuesMagazina='Mag1';MagazinaChanged();}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>

<SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

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
                                         <dx:ASPxLabel Wrap="False" ID="lblKategoriSeriali" AssociatedControlID="cmbKategoriSeriali" runat="server"
                                        Text="Kategori Seriali" ClientInstanceName="lblKategoriSeriali">
                                    </dx:ASPxLabel>
                                    <%--  </div>--%>
                                    <%--    <div id="dvbtnMagazina">--%>
                                    <dx:ASPxComboBox ID="cmbKategoriSeriali" Width="100%" runat="server" ClientInstanceName="cmbKategoriSeriali"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickKategoriSeriali();}"  LostFocus="function (s,e){LostFocusSerial();}"/>

<SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />

                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidateOnLeave="false"
                                            ValidationGroup="entries1">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />

                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />

                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                      
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="ucEmerSkedari" ID="lblEmerSkedari" ClientInstanceName="lblEmerSkedari"
                                                        runat="server" Text="Emri i Skedarit">
                                                    </dx:ASPxLabel>
                                               
                                              
                                                    <dx:ASPxUploadControl ID="ucEmerSkedari" runat="server" Width="100%" ClientInstanceName="ucEmerSkedari"
                                                        OnFileUploadComplete="ucEmerSkedari_FileUploadComplete">
                                                 <ClientSideEvents  TextChanged="function (s,e){TextChangeFile();}" />
                                                        <ValidationSettings AllowedFileExtensions=".xls,.xlsx,.txt,.csv"
                                                            MaxFileSizeErrorText="Skedari ka kaluar madhesine maximale 10MB">
                                                        </ValidationSettings>
                                                    </dx:ASPxUploadControl>
                                          <dx:ASPxButton ID="btnNgarko" Width="100%" runat="server" Text="Ngarko"
                                        ClientInstanceName="btnNgarko" AutoPostBack="False" CausesValidation="False">
                                        <ClientSideEvents Click="function(s, e) { Ngarko(); 
}" />
                                    </dx:ASPxButton>
                                        <dx:ASPxLabel Wrap="False" ID="lblFormatiPrintimit" AssociatedControlID="cmbFormatiPrintimit"
                                            runat="server" Text="Formati i printimit" ClientInstanceName="lblFormatiPrintimit">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbFormatiPrintimit" Width="100%" runat="server" ClientInstanceName="cmbFormatiPrintimit"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){}" />

<SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbDergoMeEmail" ID="lblDergoMeEmail" runat="server"
                                            Text="Dergo me email:" ClientInstanceName="lblDergoMeEmail">
                                        </dx:ASPxLabel>

                                        <dx:ASPxCheckBox ID="cbDergoMeEmail" runat="server" ClientInstanceName="cbDergoMeEmail" Width="100%">
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbDergoEmailDokArkives" ID="lblDergoEmailDokArkives" runat="server"
                                            Text="Dergo dok e arkives:" ClientInstanceName="lblDergoEmailDokArkives">
                                        </dx:ASPxLabel>

                                        <dx:ASPxLabel Wrap="False" ID="lblMallraTeDjegshme" AssociatedControlID="cbMallraTeDjegshme"
                                            runat="server"  ClientVisible="false" ClientInstanceName="lblMallraTeDjegshme">
                                        </dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbMallraTeDjegshme" runat="server"  ClientVisible="false" ClientInstanceName="cbMallraTeDjegshme" Width="100%">
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
										<dx:ASPxLabel Wrap="False" ID="lblTipiMag" AssociatedControlID="cmbTipiMag"
                                            runat="server" ClientInstanceName="lblTipiMag" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="cmbTipiMag" runat="server" ClientInstanceName="cmbTipiMag"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">
                                            
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
										
										<dx:ASPxLabel Wrap="False" ID="lblTransaksioni" AssociatedControlID="cmbTransaksioni"
                                            runat="server" ClientInstanceName="lblTransaksioni" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="cmbTransaksioni" runat="server" ClientInstanceName="cmbTransaksioni"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">

                                            <DropDownButton>

                                            </DropDownButton>
                                            
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblShoqerimIKerkuar" AssociatedControlID="cbShoqerimIKerkuar"
                                            runat="server"  ClientVisible="false" ClientInstanceName="lblShoqerimIKerkuar">
                                        </dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbShoqerimIKerkuar" runat="server"  ClientVisible="false" ClientInstanceName="cbShoqerimIKerkuar" Width="100%">
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
                                        <dx:ASPxLabel Wrap="False" ID="lblFiskalizo" AssociatedControlID="cbFiskalizo"
                                            runat="server"  ClientVisible="false" ClientInstanceName="lblFiskalizo">
                                        </dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbFiskalizo" runat="server"  ClientVisible="false" ClientInstanceName="cbFiskalizo" Width="100%">
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btnTransportuesi" ID="lbltransportuesi" runat="server" ClientInstanceName="lbltransportuesi" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btnTransportuesi" Width="100%" runat="server"  ClientVisible="false"
                                            ClientInstanceName="btnTransportuesi" ShowShadow="False"
                                            SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickTransportues();}"
                                                SelectedIndexChanged="function(s,e){IndexChangedTransportues(s, e);}"
                                                TextChanged="IndexChangedTransportues" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua"
                                                        PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua"
                                                        PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxCheckBox ID="cbDergoEmailDokArkives" runat="server" ClientInstanceName="cbDergoEmailDokArkives" Width="100%">
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneAutomjeti" ID="lblAutomjeti" runat="server" ClientInstanceName="lblAutomjeti">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btneAutomjeti" runat="server" ClientInstanceName="btneAutomjeti"
                                            Width="100%" ShowShadow="False" ValueType="System.Int32" EnableClientSideAPI="True"
                                            IncrementalFilteringMode="Contains" EnableSynchronization="True" EnableCallbackMode="True"
                                            DropDownRows="3" CallbackPageSize="10" OnItemRequestedByValue="btneAutomjeti_ItemRequestedByValue"
                                            OnItemsRequestedByFilterCondition="btneAutomjeti_ItemsRequestedByFilterCondition"
                                            SettingsLoadingPanel-ImagePosition="Top" AllowNull="true">
                                            <ClientSideEvents ButtonClick="function(s, e) { Auto_Click(); }" TextChanged="function(s, e) {textChangedAuto(s,e);}" />

<SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                ValidationGroup="entries" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbRenditje" ID="lblRenditje" runat="server"
                                            Text="Ruaj renditje:" ClientInstanceName="lblRenditje">
                                        </dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbRenditje" runat="server" ClientInstanceName="cbRenditje" Width="100%">
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <ClientSideEvents CheckedChanged="function(s, e) { RenditjeCheck(true); }" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxCheckBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTarga" ID="lblTarga" Text="Targa" runat="server" ClientInstanceName="lblTarga">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTarga" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtTarga">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblMagazina2" AssociatedControlID="btneMagazina2"
                                            runat="server" ClientInstanceName="lblMagazina2">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="btneMagazina2" ClientInstanceName="btneMagazina2"
                                            runat="server" ShowShadow="False" Style="margin-bottom: 0px" SettingsLoadingPanel-ImagePosition="Top"
                                             EnableSynchronization="True" OnItemRequestedByValue="btneMagazina2_ItemRequestedByValue" 
                                            OnItemsRequestedByFilterCondition="btneMagazina2_ItemsRequestedByFilterCondition" EnableCallbackMode="True"
                                            EnableClientSideAPI="True" CallbackPageSize="10" IncrementalFilteringMode="Contains" ValueType="System.Int64">
                                            <ClientSideEvents ButtonClick="function(s,e){identifikuesMagazina='Mag2';ButtonClickMagazina2();}"
                                                TextChanged="function(s,e){identifikuesMagazina='Mag2';TextChangedMagazina2();}"  />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>

<SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

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
                                        <dx:ASPxLabel Wrap="False" ID="lblNrDok" AssociatedControlID="txtNrDok" runat="server"
                                            Text="Nr dokumenti:" ClientInstanceName="lblNrDok">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Width="100%" ID="txtNrDok" runat="server" ClientInstanceName="txtNrDok">
                                            <ClientSideEvents Init="function(s, e) {  }" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
                                                ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblMagazinieri" AssociatedControlID="txtMagazinieri"
                                            runat="server" Text="Magazinieri:" ClientInstanceName="lblMagazinieri">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Width="100%" ID="txtMagazinieri" runat="server" ClientInstanceName="txtMagazinieri">
                                            <ClientSideEvents Init="function(s, e) {  
	
}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblPershkrimi" AssociatedControlID="txtPershkrimi"
                                            runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxMemo ID="txtPershkrimi" Width="100%" runat="server" ClientInstanceName="txtPershkrimi"
                                            Rows="3">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <ClientSideEvents LostFocus="function(s,e){myJQGrid.focusGrid({emergride: '#rowed5', isLidhur: lidhur, idKoloneGrideFokus: arrayIdKolonaGrides[0]});}" />
                                        </dx:ASPxMemo>
                                        <dx:ASPxLabel Wrap="False" ID="lbladresa" AssociatedControlID="txtadresa" runat="server"
                                            Text="Adresa:" ClientInstanceName="lbladresa">
                                        </dx:ASPxLabel>
                                        <dx:ASPxMemo Width="100%" ID="txtadresa" runat="server" ClientInstanceName="txtadresa" Rows="3">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxMemo>
                                        <dx:ASPxLabel Wrap="False" ID="lblNrProjekti" AssociatedControlID="txtNrProjekti"
                                            runat="server" Text="Nr Projekti" ClientInstanceName="lblNrProjekti">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Width="100%" ID="txtNrProjekti" runat="server" ClientInstanceName="txtNrProjekti">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblDtDok" AssociatedControlID="dteDtDok" runat="server"
                                            Text="Dt Dokumenti:" ClientInstanceName="lblDtDok">
                                        </dx:ASPxLabel>
                                        <dx:ASPxDateEdit Width="100%" ID="dteDtDok" runat="server" ClientInstanceName="dteDtDok"
                                            ShowShadow="False">
                                            <ClientSideEvents DateChanged="function (s,e){DateChanged(s,e);}" GotFocus="function(s, e){ dateGotFocus( s, e); }" />
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
                                        <dx:ASPxLabel Wrap="False" ID="lblDtTransporti" AssociatedControlID="dteDtTransporti" runat="server"
                                            Text="Dt Dokumenti:" ClientInstanceName="lblDtTransporti">
                                        </dx:ASPxLabel>
                                        <dx:ASPxDateEdit Width="100%" ID="dteDtTransporti" runat="server" ClientInstanceName="dteDtTransporti"
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
                                        <dx:ASPxLabel Wrap="False" ID="lblMeKonfirmim" AssociatedControlID="cbMeKonfirmim"
                                            runat="server" Text="Me konfirmim:" ClientInstanceName="lblMeKonfirmim">
                                        </dx:ASPxLabel>
                                        <dx:ASPxCheckBox Width="100%" ID="cbMeKonfirmim" runat="server" ClientInstanceName="cbMeKonfirmim">
                                        </dx:ASPxCheckBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblDegeAdministrative" AssociatedControlID="cmbDegeAdministrative"
                                            runat="server" Text="Inventarizimi:" ClientInstanceName="lblDegeAdministrative">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="cmbDegeAdministrative" runat="server" ClientInstanceName="cmbDegeAdministrative"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents TextChanged="function(s,e) {TextChangedDega();}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>

<SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblNrSerial" AssociatedControlID="txtNrSerial" runat="server"
                                            Text="Nr Serial:" ClientInstanceName="lblNrSerial">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Width="100%" ID="txtNrSerial" runat="server" ClientInstanceName="txtNrSerial">
                                            <ClientSideEvents Init="function(s, e) {  }" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="true"
                                                ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblShenime" AssociatedControlID="txtShenime" runat="server"
                                            Text="Shenime" ClientInstanceName="lblShenime">
                                        </dx:ASPxLabel>
                                        <dx:ASPxMemo ID="txtShenime" Width="100%" runat="server" ClientInstanceName="txtShenime"
                                            Rows="3">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <ClientSideEvents LostFocus="function(s,e){myJQGrid.focusGrid({emergride: '#rowed5', isLidhur: lidhur, idKoloneGrideFokus: arrayIdKolonaGrides[0]});}" />
                                        </dx:ASPxMemo>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNIVFSH" ID="lblNIVFSH" Text="Targa" runat="server" ClientInstanceName="lblNIVFSH">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtNIVFSH" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtNIVFSH">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                         <dx:ASPxLabel Wrap="False" AssociatedControlID="txtWTNIC" ID="lblWTNIC" Text="Targa" runat="server" ClientInstanceName="lblWTNIC">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtWTNIC" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtWTNIC">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblLlogariKunderParti" AssociatedControlID="cmbLlogariKunderParti"
                                            runat="server" Text="Llogari kunderparti" ClientInstanceName="lblLlogariKunderParti">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="cmbLlogariKunderParti" ClientInstanceName="cmbLlogariKunderParti"
                                            runat="server" ShowShadow="False" Style="margin-bottom: 0px" OnItemRequestedByValue="cmbLlogariKunderParti_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbLlogariKunderParti_ItemsRequestedByFilterCondition"
                                            EnableCallbackMode="True" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickLlogaria();}"
                                                TextChanged="function(s, e){nrLlogariChange(s,e);}" />
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>

                                            <SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

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

                                        <dx:ASPxLabel Wrap="False" ID="lblPershkrimMagazine" AssociatedControlID="lblPershkrimMagazine" runat="server"
                                            Text="Pershkrim mag" ClientInstanceName="lblPershkrimMagazine">
                                        </dx:ASPxLabel>

                                        <dx:ASPxTextBox ID="txtPershkrimMagazine" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtPershkrimMagazine">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblPershkrimDege" AssociatedControlID="lblPershkrimDege" runat="server"
                                            Text="Pershkrim dege" ClientInstanceName="lblPershkrimDege">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtPershkrimDege" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtPershkrimDege">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblTarga2" AssociatedControlID="txtTarga2"
                                            runat="server" Text="Targa Shoferit:" ClientInstanceName="lblTarga2">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Wrap="False" ID="txtTarga2" Width="100%"
                                            runat="server" Text="0" ClientInstanceName="txtTarga2">
                                        </dx:ASPxTextBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblShoferi" AssociatedControlID="txtShoferi"
                                            runat="server" Text="Shoferi:" ClientInstanceName="lblShoferi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Wrap="False" ID="txtShoferi" Width="100%"
                                            runat="server" Text="0" ClientInstanceName="txtShoferi">
                                        </dx:ASPxTextBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblOperatori" runat="server" Text="Operatori:" ClientInstanceName="lblOperatori"
                                            AssociatedControlID="cmbOperatori">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbOperatori" Width="100%" runat="server" ClientInstanceName="cmbOperatori"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){}" />
                                            <SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>
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
                                    </div>

                                    <div id="dvFundi" class="atributeDiveFshehur">
                                        <dx:ASPxLabel Wrap="False" ID="lblDtRegjistrimi" runat="server" Text="Dt Regjistrimi:"
                                            ClientInstanceName="lblDtRegjistrimi" AssociatedControlID="dteDtRegjistrimi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxDateEdit Width="100%" ID="dteDtRegjistrimi" runat="server" ClientInstanceName="dteDtRegjistrimi"
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
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <dx:ASPxLabel Wrap="False" ID="lblTotali" runat="server" Text="Totali" ClientInstanceName="lblTotali"
                                            AssociatedControlID="txtVlefta">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Width="100%" ID="txtVlefta" runat="server" ClientInstanceName="txtVlefta">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblGrup1" runat="server" Text="Grupim 1:" ClientInstanceName="lblGrup1"
                                            AssociatedControlID="cmbGrup1">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="cmbGrup1" runat="server" ClientInstanceName="cmbGrup1"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblGrup2" runat="server" Text="Grupim 2:" ClientInstanceName="lblGrup2"
                                            AssociatedControlID="cmbGrup2">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="cmbGrup2" runat="server" ClientInstanceName="cmbGrup2"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblGrup3" runat="server" Text="Grupim 3:" ClientInstanceName="lblGrup3"
                                            AssociatedControlID="cmbGrup3">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox Width="100%" ID="cmbGrup3" runat="server" ClientInstanceName="cmbGrup3"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                            <SettingsLoadingPanel ImagePosition="Top"></SettingsLoadingPanel>

                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>

                                    </div>
                                    <dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server" ClientInstanceName="hfNrAutoShitje">
                                    </dx:ASPxHiddenField>
                                    <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto">
                                    </dx:ASPxHiddenField>
                                    <br />
                                    <br />
                                    <div id="divfund1" style="visibility: hidden;">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <table width="100%">
                                            
                                                    <tr>
                                                        <td style="width: 50%">
                                                            <table>
                                                                <tr style="visibility: hidden">
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="btnPeriudha" runat="server" ClientInstanceName="btnPeriudha"
                                                                            SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                                                            <ClientSideEvents ButtonClick="function(s,
    e) {ShfaqPeriudhen();}"
                                                                                LostFocus="function(s, e) {lostFocusPeriudha(s.GetText());
    valueChangedPeriudha();}" />
                                                                            <LoadingPanelImage>
                                                                            </LoadingPanelImage>
                                                                            <DropDownButton>
                                                                                <Image>
                                                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                                </Image>
                                                                            </DropDownButton>
                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxLabel Wrap="False" ID="lblPeriudhaAktuale" ClientInstanceName="lblPeriudhaAktuale"
                                                                            runat="server" Text="">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="right" style="width: 10%"></td>
                                                        <td align="right" style="width: 10%"></td>
                                                    </tr>
                                                </table>
                                                <iframe id="Container" runat="server" frameborder="0" height="0" name="Container"
                                                    width="0"></iframe>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </asp:Panel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <%-- Navigation pane --%>
                    <dx:SplitterPane MaxSize="700px" ShowCollapseBackwardButton="True" Separators-Size="10px"
                    PaneStyle-BackColor="Transparent" Collapsed="True" ShowCollapseForwardButton="True" 
                    ScrollBars="Auto" AllowResize="True" MinSize="80px" AutoWidth="false" AutoHeight="false" Name="pnlInfo">
                        <PaneStyle>
                        </PaneStyle>
                        <Separators Size="10px">
                        </Separators>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 100%; vertical-align: top;">
                                    <tr>
                                        <td align="center" valign="top">
                                            <asp:UpdatePanel ID="pnl2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 50%">
                                                                <dx:ASPxButton ID="btnMbyllur" runat="server" Text="-" Width="100%" AutoPostBack="false"
                                                                    Height="25px" Font-Size="9" Font-Bold="true"  ToolTip="Mos shfaq info">
                                                                    <ClientSideEvents Click="function (s,e){RuajHapurMbyllurminus(false)}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td style="width: 50%">
                                                                <dx:ASPxButton ID="btnHapur" runat="server" Text="+" Width="100%" AutoPostBack="false"
                                                                    Height="25px" Font-Size="9" ToolTip="Shfaq info">
                                                                    <ClientSideEvents Click="function (s,e){RuajHapurMbyllurplus(true)}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <dxnb:ASPxNavBar ID="ASPxNavBar1" runat="server" ClientInstanceName="navbar" Width="100%"
                                                        EnableAnimation="True"  SyncSelectionMode="CurrentPath"
                                                        EnableClientSideAPI="True" AllowSelectItem="True" Font-Size="8pt">
                                                        <ClientSideEvents HeaderClick="function (s,e) { HeaderClick (s,e); }" ItemClick="function(s, e) {}"
                                                            ExpandedChanging="function (s,e) {Expanded();  }" />
                                                        <GroupHeaderTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="width: 60%; font-weight: bold; height: 12px;">
                                                                        <dx:ASPxLabel ID="Label1" runat="server" Font-Size="8" Text='<%# Eval("Text") %>' />
                                                                    </td>
                                                                    <td style="width: 10%;">
                                                                        <dx:ASPxHyperLink ID="HyperLink2" runat="server" Text='<%# Eval("Name") %>' NavigateUrl="javascript:void(0)"
                                                                            ImageWidth="12px" ImageHeight="12px" ImageUrl="~/images/new/flash.png" ClientSideEvents-Click="function (s,e){ ButtonClickNavBar(s);}"
                                                                            ClientSideEvents-Init="function (s,e){ KontrolloTeDrejta(s);}" DisabledStyle-BackColor="#CCCCCC" />
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </GroupHeaderTemplate>
                                                        <Groups>
                                                            <dxnb:NavBarGroup Text="Info Artikulli" Expanded="true" Name="Artikulli">
                                                                <ContentTemplate>
                                                                    <dx:ASPxListBox ID="lbxZgjedhur" runat="server" Width="100%" ClientInstanceName="lbxZgjedhur"
                                                                        Font-Size="8" SettingsLoadingPanel-ImagePosition="Top">
                                                                        <Columns>
                                                                            <dx:ListBoxColumn FieldName="Emri" Name="Emri" />
                                                                            <dx:ListBoxColumn FieldName="Vlera" Name="Vlera" />
                                                                        </Columns>
                                                                        <LoadingPanelImage>
                                                                        </LoadingPanelImage>
                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxListBox>
                                                                </ContentTemplate>
                                                            </dxnb:NavBarGroup>
                                                        </Groups>
                                                        <LoadingPanelImage>
                                                        </LoadingPanelImage>
                                                    </dxnb:ASPxNavBar>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
                 <ClientSideEvents PaneCollapsed="function(s, e) { spliterPaneCollapsed(s,e);}" PaneExpanded="function(s, e) { spliterPaneCollapsed(s,e);}" PaneCollapsing="function(s, e) { spliterPaneCollapsing(s,e);}" PaneExpanding="function(s, e) { spliterPaneExpanding(s,e);}" PaneResized="function(s, e) { spliterPaneResized(s,e);}" />
                <Styles>
                </Styles>
                <Images>
                </Images>
            </dx:ASPxSplitter >
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                    <div style="display: none">
                        <dx:ASPxButton ID="btnruaj" runat="server" Text="Ruaj" ClientInstanceName="btn" Width="0%"
                            ValidationGroup="entries">
                        </dx:ASPxButton>
                    </div>
                    <iframe id="Container55" runat="server" frameborder="0" height="0" name="Container55"
                        width="0"></iframe>
                    <iframe id="Container1" runat="server" frameborder="0" height="0" name="Container1"
                        width="0"></iframe>
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
            </dx:ASPxHiddenField>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                <ClientSideEvents Closing="function(s, e) { popUniversalClose(s, e); }" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl >
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupSerialet" runat="server" AllowDragging="True" ClientInstanceName="popupSerialet"
                CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine" ShowCloseButton="false" ShowCollapseButton="true"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                <ClientSideEvents Closing="function(s, e) { popupSerialet.SetContentUrl('');}" />
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl >
        </div>
    </form>
</body>
</html>
