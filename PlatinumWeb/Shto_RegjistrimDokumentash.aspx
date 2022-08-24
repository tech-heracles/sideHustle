<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_RegjistrimDokumentash.aspx.cs" Inherits="PlatinumWeb.Shto_RegjistrimDokumentash" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxnb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ucPopupValidimMarreveshje.ascx" TagPrefix="ucPopup" TagName="ucPopupValidimMarreveshje" %>
<%@ Register Src="~/ucPopUpKlonimi.ascx" TagPrefix="ucPopUpKlonimi" TagName="ucPopUpKlonimi" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Alpha Web</title>

    <link href="js/css/le-frog/jquery-ui.css" media="screen" rel="stylesheet" type="text/css" runat="server" id="themeJQuery" />
    <link href="fine-uploader/fine-uploader-new.css" rel="stylesheet" />
    <link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="js/jqGrid445/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="AlphaWeb.css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/jquery.ui.datepicker-sq.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/memoryObject.js;~/js/async.min.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/arkiva.js;~/js/myNrAuto-IMB.2.1.js;~/JsGlobal.js;~/js/myCookies-IMB.2.1.js;~/fine-uploader/jquery.fine-uploader.js;~/js/multiOpenAccordion-IMB.2.1.js;~/js/toolbar.js;~/js/aspx.js/Shto_RegjistrimDokumentash.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>

    <style type="text/css">
        .ui-jqgrid .ui-jqgrid-bdiv {
            position: relative;
            margin: 0em;
            padding: 0;
            /*padding-right: 19px;*/
            /*overflow: auto;*/
            overflow-x: hidden;
            overflow-y: auto;
            text-align: left;
        }
        .selectize-control.single .selectize-input:after{
	        display: none;
        }
    </style>

</head>
<body>
    <!-- #include file="~/fine-uploader/templates/imb-default.html" -->
    <form id="form1" runat="server">

        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30"></LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600"></asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <ClientSideEvents ControlsInitialized="function(s,e){ DevExControlsInitialized(s, e);}" />
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true"></dx:ASPxHiddenField>
        <asp:HiddenField ID="hfTmpColMag" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="divLart">
                    <div id="dvMenuButona">
                        <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"
                            ItemImagePosition="Top" OnDataBound="ASPxMenu1_DataBound" OnItemClick="ASPxMenu1_ItemClick"
                            SeparatorWidth="1px" ShowPopOutImages="True" Width="100%">
                            <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                            <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                            <ClientSideEvents ItemClick="menu_click" Init="function(s) {s.SetClientVisible(true);}" />
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
                    </div>
                    <div id="dvMenu" style="display: none">
                        <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <dx:ASPxMenu ID="MenuInfo" runat="server" BorderBetweenItemAndSubMenu="HideRootOnly" BackColor="Transparent"
                                    ClientIDMode="AutoID" ClientInstanceName="MenuInfo" ShowPopOutImages="True" Width="100%"
                                    AllowSelectItem="True">
                                    <ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}" />
                                    <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <SubMenuStyle GutterWidth="17px" />
                                </dx:ASPxMenu>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <dx:ASPxPopupControl ID="popIMEI" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popIMEI" CloseAction="CloseButton" EnableAnimation="False"
                    EnableViewState="False" Font-Bold="true" HeaderText="IMEI gabuar" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" Width="500px">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl114" runat="server">
                            <dx:ASPxPanel ID="ASPxPanel111" runat="server" ClientIDMode="AutoID" Width="500px">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent111" runat="server" SupportsDisabledAttribute="True">
                                        <br />

                                        <dx:ASPxLabel Wrap="True" ID="lblMsgbox11" runat="server" ForeColor="Red" ClientInstanceName="lblMsgbox11" ClientIDMode="AutoID" Text="Kujdes: ky IMEI nuk mund te shitet. Ju keni IMEI te tjere me te vjeter per kete aparat. Vendosni nje IMEI tjeter">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <br />
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
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
                                                            <ClientSideEvents Click="function(s, e) { popKonvertuar.Hide(); Utils.shfaqLoadingGif(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonCancel5" runat="server" ClientIDMode="AutoID" Text="Jo" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s, e) { popKonvertuar.Hide(); click = false; }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popMesazhQK" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popMesazhQK" CloseAction="None" EnableAnimation="False" EnableViewState="False"
                    Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" Width="300px" ShowCloseButton="False">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl7" runat="server">
                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel17" runat="server" ClientIDMode="AutoID" Width="271px">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent17" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel Wrap="true" ID="lblMsgbox4" runat="server" ClientIDMode="AutoID" Text="Deshironi te beni shperndarjen ne qendrat e kostos?"
                                            ClientInstanceName="lblmesazhqendra">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonOkQK" runat="server" CausesValidation="False" ClientInstanceName="ButtonOkQK"
                                                            AutoPostBack="false" Text="Po">
                                                            <ClientSideEvents Click="function(s, e) { popMesazhQK.Hide(); hapPopUp(s,e); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButtonCancelQK" runat="server" ClientIDMode="AutoID" Text="Jo"
                                                            AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s, e) { popMesazhQK.Hide(); JopopupClick(s,e); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
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
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { ndryshoNiveli(s,e); }" />
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
                                                        <ClientSideEvents Click="function(s, e) { konverto(); popKonvertim.Hide(); }" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <ucPopUpKlonimi:ucPopUpKlonimi runat="server" ID="ucPopUpKlonimi"/>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
            </Triggers>
            <ContentTemplate>
                <input type="hidden" runat="server" id="mesazhFromServer" value="">
                <asp:HiddenField ID="status1" runat="server" Value="false" />
                <asp:HiddenField ID="hfqkmesazhi" runat="server" Value="jo" />
                <asp:HiddenField ID="hfqkmesazhimag" runat="server" Value="jo" />
                <asp:HiddenField ID="hfqkmesazhibanka" runat="server" Value="jo" />
                <asp:HiddenField ID="hfqkmesazhiVDK" runat="server" Value="jo" />
                <asp:HiddenField ID="hfUrlVDK" runat="server" />
                <asp:HiddenField ID="hfUrl" runat="server" />
                <asp:HiddenField ID="hfUrlmag" runat="server" />
                <asp:HiddenField ID="hfUrlbanka" runat="server" />
                <asp:HiddenField ID="hfStatusRuajtje" runat="server" />
                <asp:HiddenField ID="hfLloji" runat="server" />
                <asp:HiddenField ID="hfVodOne" runat="server" />
                <asp:HiddenField ID="hfPiket" runat="server" />
                <asp:HiddenField ID="hfKodVFOne" runat="server" />
                <asp:HiddenField ID="hfKodBundle" runat="server" />
                <asp:HiddenField ID="hfMsisdnBazaari" runat="server" />
                <asp:HiddenField ID="hfVlera" runat="server" />
                <asp:HiddenField ID="hfIdKodi" runat="server" />
                <asp:HiddenField ID="hfZbritja" runat="server" />
                <asp:HiddenField ID="hfKodKuponiDD" runat="server" />
                <asp:HiddenField ID="hfKodi" runat="server" />
                <asp:HiddenField ID="hfPershkrimi" runat="server" />
                <asp:HiddenField ID="hfDetajimi" runat="server" />
                <asp:HiddenField ID="hfNjesia" runat="server" />
                <asp:HiddenField ID="hfSasia" runat="server" />
                <asp:HiddenField ID="hfCmimi" runat="server" />
                <asp:HiddenField ID="hfZbritje" runat="server" />
                <asp:HiddenField ID="hfVlefteTVSH" runat="server" />
                <asp:HiddenField ID="hfTVSH" runat="server" />
                <asp:HiddenField ID="hfVlefte" runat="server" />
                <asp:HiddenField ID="hfSkemaKontabel" runat="server" />
                <asp:HiddenField ID="hfDetajimetSelektuara" runat="server" />
                <asp:HiddenField ID="hfMagazina" runat="server" />
                <asp:HiddenField ID="hfLidhur" runat="server" />
                <asp:HiddenField ID="hfAutorizimi" runat="server" />
                <asp:HiddenField ID="hfRuajDraft" runat="server" />
                <asp:HiddenField ID="hfKasa" runat="server" />
                <asp:HiddenField ID="hfKasaNew" runat="server" />
                <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
                <asp:HiddenField ID="hfAtributeNrAutom" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfIdMag" runat="server" />
                <asp:HiddenField ID="hfKonv" runat="server" />
                <asp:HiddenField ID="hfkf" runat="server" />
                <asp:HiddenField ID="hfPolitike" runat="server" />
                <asp:HiddenField ID="hfKarta" runat="server" />
                <asp:HiddenField ID="hfIdKontrata" runat="server" />
                <asp:HiddenField ID="hfMerrFazaNgaSesioni" runat="server" />
                <asp:HiddenField ID="hfDokKontrate" runat="server" />
                <asp:HiddenField ID="hfFazat" runat="server" />
                <asp:HiddenField ID="hfLKVK" runat="server" />
                <asp:HiddenField ID="hfTotalPikesh" runat="server" />
                <asp:HiddenField ID="hfObjektRuajtur" runat="server" />
                <asp:HiddenField ID="blob" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--<dx:ASPxHiddenField ID="hfPeriudhaKontabel" ClientInstanceName="hfPeriudhaKontabel" runat="server"></dx:ASPxHiddenField>--%>
        <asp:HiddenField ID="gridDataObject" runat="server" />
        <asp:HiddenField ID="gridObjectKomision" runat="server" />
        <asp:HiddenField ID="hfKonverto" runat="server" />
        <asp:HiddenField ID="hfSkema" runat="server" />
        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
        <asp:HiddenField ID="hfKolonaGride" runat="server" />
        <asp:HiddenField ID="HfGridCol" runat="server" />
        <asp:HiddenField ID="HfColTrup" runat="server" />
        <asp:HiddenField ID="HfColArt" runat="server" />
        <asp:HiddenField ID="HfColArtPerb" runat="server" />
        <asp:HiddenField ID="HfColKategoriShpenzimi" runat="server" />
        <asp:HiddenField ID="HfColKodbare" runat="server" />
        <asp:HiddenField ID="HfColTvshArt" runat="server" />
        <asp:HiddenField ID="HfColTvshLlog" runat="server" />
        <asp:HiddenField ID="HfColMakro" runat="server" />
        <asp:HiddenField ID="HfColllogarite" runat="server" />
        <asp:HiddenField ID="hfPerdoruesAktual" runat="server" />
        <asp:HiddenField ID="HfColDetArt" runat="server" />
        <asp:HiddenField ID="HfColDetArt2" runat="server" />
        <asp:HiddenField ID="HfColNjesAdminis" runat="server" />
        <asp:HiddenField ID="HfColNjesiArt" runat="server" />
        <asp:HiddenField ID="HfColTaksa" runat="server" />
        <asp:HiddenField ID="hfVeprimi" runat="server" />
        <asp:HiddenField ID="hfPerdoruesi" runat="server" />
        <asp:HiddenField ID="hfKontabilizimi" runat="server" />
        <asp:HiddenField ID="hfKontrollRivleresim" runat="server" />
        <%--hidden fields per konfigurimet e lupave--%>
        <asp:HiddenField ID="hfLupaKlientFurnitor" runat="server" />
        <asp:HiddenField ID="hfLupaKlientFurnitorvartes" runat="server" />
        <asp:HiddenField ID="hfLupaAutomjet" runat="server" />
        <asp:HiddenField ID="hfLupaMenyreTransporti" runat="server" />
        <asp:HiddenField ID="hfLupaKushtDergimi" runat="server" />
        <asp:HiddenField ID="hfLupaAgjentShitje" runat="server" />
        <asp:HiddenField ID="hfLupaTransportues" runat="server" />
        <asp:HiddenField ID="hfLupaAfatMaturimi" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaInfoArt" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaInfoKF" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaInfoLlog" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaImportoSeriale" runat="server" />
        <asp:HiddenField ID="hfLupaMagazina" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaArtRi" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaArtMod" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaArtPerberes" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaGrupimPerberes" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaArtImazhe" runat="server" />
        <asp:HiddenField ID="hfLupaKushtPagese" runat="server" />
        <asp:HiddenField ID="hfHapurMbyllur" runat="server" />
        <asp:HiddenField ID="hfLupa" runat="server" />
        <asp:HiddenField ID="hfTeDrejtaModSkema" runat="server" />

        <%-- per te ruajtur konfigurimin  lupes per lupat qe ndodhen ne gride--%>
        <asp:HiddenField ID="hfGridaKodi" runat="server" />
        <asp:HiddenField ID="hfGridaDetajimi" runat="server" />
        <asp:HiddenField ID="hfLupaLlogShpenz" runat="server" />
        <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfFormatNumri" runat="server" ClientInstanceName="hfFormatNumri">
        </dx:ASPxHiddenField>
        <%--        <dx:ASPxHiddenField ID="memoryArt" runat="server" ClientInstanceName="memoryArt">
        </dx:ASPxHiddenField>--%>
        <dx:ASPxHiddenField ID="hfSeriale" runat="server" ClientInstanceName="hfSeriale">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfSasiSeriale" runat="server" ClientInstanceName="hfSasiSeriale">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfIdGride" runat="server" ClientInstanceName="hfIdGride">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="HfLlog" runat="server" ClientInstanceName="HfLlog">
        </dx:ASPxHiddenField>
        <%-- Hidden fields per Arkiven--%>
        <asp:HiddenField ID="hfArkivaDokId" runat="server" />
        <asp:HiddenField ID="hfKlient" runat="server" />
        <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
        </dx:ASPxHiddenField>

        <dxcb:ASPxCallbackPanel SettingsLoadingPanel-Enabled="false" ID="serialetCallbackPanel" runat="server" OnCallback="serialetCallbackPanel_Callback">
            <ClientSideEvents BeginCallback="function(s,e){BeginCallback(s,e)}" EndCallback="function(s,e){SkedariUploaded(s,e);}" />
        </dxcb:ASPxCallbackPanel>
        <div class="container bootstrap-iso">
              
              <ucPopup:ucPopupValidimMarreveshje runat="server" ID="ucPopUpValidimMarreveshje"></ucPopup:ucPopupValidimMarreveshje>

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

        <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Height="100%" Width="100%" ClientInstanceName="splitter">
            <Panes>
                <%-- Header pane--%>
                <dx:SplitterPane PaneStyle-BackColor="Transparent" ScrollBars="auto" Name="mainShitje" Separators-Size="10px">
                    <%--ScrollBars="None"--%>
                    <Separators Size="10px">
                    </Separators>
                    <PaneStyle BackColor="Transparent"></PaneStyle>
                    <ContentCollection>
                        <dx:SplitterContentControl ID="SplitterContentControl1" Height="100%" runat="server">
                            <asp:Panel ID="ContentPanel" runat="server">
                                <div style="width: 100%; height: 100%">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ASPxMenu1" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div style="display: none">
                                                <dx:ASPxLabel Wrap="False" ID="pergjigja" runat="server" Text="" ForeColor="Red"
                                                    ClientInstanceName="pergjigja" ClientVisible="false">
                                                </dx:ASPxLabel>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="pnlLidhur" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table id='hl' runat="server">
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div id="toolbar" class="bootstrap-iso ui-widget-header ui-corner-all">
                                        <button id="mbrapa">mbrapa</button>
                                        <button id="para">para</button>
                                    </div>
                                    <div id="bootPopUp" class="bootstrap-iso">
                                    </div>
                                    <div id="accordition">
                                        <div>
                                            <h3 id="kokeKonfigurimi"><span id="kokeKonfigurimidiv" class="ui-not-accordion-header-text">Koke Dokumenti:</span></h3>
                                            <div id="kokeKonfigurimiPermbajtje">
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
                                                <div id="divgride1" style="display: none">
                                                    <div id="divgride2">
                                                        <table id="rowed5">
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="trup">
                                            <h3 id="trupKonfigurimiKomision"><span id="trupKonfigurimidiv1" class="ui-not-accordion-header-text">Trup Dokumenti Komision</span></h3>
                                            <div>
                                                <div id="divgrideKomision" style="display: none">
                                                    <div id="divgride2Komision">
                                                        <table id="rowed6">
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <h3 id="fundKonfigurimi"><span id="fundKonfigurimidiv" class="ui-not-accordion-header-text">Fund Dokumenti</span></h3>
                                            <div>
                                                <table id="tblFund" class="renditKontrolle" align="right">
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="dvFillim" class="atributeDiveFshehur">
                                        <%--<div id="dvlblNiveli">--%>
                                        <%--</div>
                                    <div id="dvcmbNiveli" style="display: inline">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" BackColor="white"
                                            ClientInstanceName="lblKonfigurimi" Text="" AssociatedControlID="gauge" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="lblStatusApr" runat="server" AssociatedControlID="lblStatusAprovimi"
                                            ClientInstanceName="lblStatusApr" Text="" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="lblStatusAprovimi" runat="server" class="klasePerLblKonfigurimi"
                                            ClientInstanceName="lblStatusAprovimi" Text="" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="lblKrij" runat="server" AssociatedControlID="lblKrijuesi"
                                            ClientInstanceName="lblKrij" Text="Krijuesi" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="lblKrijuesi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKrijuesi"
                                            Text="" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="lblNiveli" AccessKey="N" AssociatedControlID="cmbNiveli"
                                            runat="server" Text="<u>N</u>iveli" EncodeHtml="false" ClientInstanceName="lblNiveli" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbNiveli" Width="100%" runat="server" ValueType="System.Int32" ClientInstanceName="cmbNiveli"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ TextChangedNiveli();}" />
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
                                        <dx:ASPxLabel Wrap="False" ID="lblModeli" AccessKey="M" AssociatedControlID="cmbModeli"
                                            runat="server" ClientInstanceName="lblModeli" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbModeli" Width="100%" runat="server" ClientInstanceName="cmbModeli" AnimationType="None"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" DropDownHeight="100" ClientVisible="false">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin(true);}" />
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
                                        <dx:ASPxLabel Wrap="False" ID="lblKlienti" AssociatedControlID="btnKlienti" runat="server"
                                            Text="Klient ID" ClientInstanceName="lblKlienti" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btnKlienti" Width="100%" runat="server" ClientInstanceName="btnKlienti"
                                            ShowShadow="False" ValueType="System.Int64" EnableClientSideAPI="True" IncrementalFilteringMode="Contains"
                                            EnableCallbackMode="True" CallbackPageSize="10" EnableSynchronization="True"
                                            OnItemRequestedByValue="btnKlienti_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btnKlienti_ItemsRequestedByFilterCondition"
                                            SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickKlienti();}" SelectedIndexChanged="function(s,e){IndexChangedKlienti(s,e);}" TextChanged="function(s,e){TextChangedKlienti(s,e); }" />
                                            <%-- LostFocus="function(s,e){TextChangedKlienti(s,e); }" />--%>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorText="Ploteso Klientin" ErrorDisplayMode="ImageWithTooltip"
                                                Display="Dynamic" ValidateOnLeave="false" ValidationGroup="entries1">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblEmri" AssociatedControlID="txtEmri" runat="server"
                                            EncodeHtml="False" Text="<u>E</u>mer" ClientInstanceName="lblEmri" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvtxtEmri">--%>
                                        <dx:ASPxTextBox ID="txtEmri" Width="100%" runat="server" ClientInstanceName="txtEmri"
                                            BackColor="AliceBlue" ClientVisible="false">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                                ValidateOnLeave="False">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblEmriKlienti" AssociatedControlID="txtEmriKlienti" runat="server"
                                            EncodeHtml="False" Text="<u>E</u>mer Klienti" ClientInstanceName="lblEmriKlienti" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvtxtEmri">--%>
                                        <dx:ASPxTextBox ID="txtEmriKlienti" Width="100%" runat="server" ClientInstanceName="txtEmriKlienti" ClientVisible="false">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                                ValidateOnLeave="False">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblKontakti" AssociatedControlID="txtKontakti" runat="server"
                                            EncodeHtml="False" Text="Kontakti" ClientInstanceName="lblKontakti" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvtxtEmri">--%>
                                        <dx:ASPxTextBox ID="txtKontakti" Width="100%" runat="server" ClientInstanceName="txtKontakti" ClientVisible="false">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                                ValidateOnLeave="False">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblNipt" AssociatedControlID="txtNipt" runat="server"
                                            EncodeHtml="False" Text="NIPT:" ClientInstanceName="lblNipt" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvtxtEmri">--%>
                                        <dx:ASPxTextBox ID="txtNipt" Width="100%" runat="server" ClientInstanceName="txtNipt" ClientVisible="false">
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--
                                </div>--%>

                                        <dx:ASPxLabel Wrap="False" ID="lblLlojMarreveshje" AssociatedControlID="cmbLlojMarreveshje"
                                            runat="server" Text="Lloji i marreveshjes" ClientInstanceName="lblLlojMarreveshje" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbLlojMarreveshje" Width="100%" runat="server" ClientInstanceName="cmbLlojMarreveshje"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                                ValidateOnLeave="False">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblIdMarreveshje" AssociatedControlID="txtIdMarreveshje"
                                            runat="server" Text="ID e marreveshjes" ClientInstanceName="lblIdMarreveshje" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtIdMarreveshje" Width="100%" ClientInstanceName="txtIdMarreveshje" runat="server" ClientVisible="false">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                                ValidateOnLeave="False">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>

                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbStatusMarreveshje" ID="lblStatusMarreveshje" runat="server"
                                            Text="Aktive" ClientInstanceName="lblStatusMarreveshje" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbStatusMarreveshje"
                                            Width="100%"
                                            runat="server"
                                            ClientInstanceName="cmbStatusMarreveshje"
                                            ShowShadow="False"
                                            SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic"
                                                ValidationGroup="entries1"
                                                ErrorDisplayMode="ImageWithTooltip"
                                                ValidateOnLeave="False">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblQytetiK" AssociatedControlID="txtQytetiK" runat="server"
                                            EncodeHtml="False" Text="<u>Q</u>ytet Klienti" ClientInstanceName="lblQytetiK" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvtxtEmri">--%>
                                        <dx:ASPxTextBox ID="txtQytetiK" runat="server" ClientInstanceName="txtQytetiK" Width="100%" ClientVisible="false">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                                ValidateOnLeave="False">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--<div id="dvlblData">--%>

                                        <dx:ASPxLabel Wrap="False" ID="lblData" AssociatedControlID="data_DateEdit" runat="server"
                                            Text="Data" ClientInstanceName="lblData" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="lblKategoriSeriali" AssociatedControlID="cmbKategoriSeriali" runat="server"
                                            Text="Kategori Seriali" ClientInstanceName="lblKategoriSeriali" ClientVisible="false">
                                        </dx:ASPxLabel>

                                        <dx:ASPxComboBox ID="cmbKategoriSeriali" Width="100%" runat="server" CssClass="form-control" ClientInstanceName="cmbKategoriSeriali"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" OnCallback="cmbKategoriSeriali_Callback" ClientVisible="false">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickKategoriSeriali();}" EndCallback="function(s,e){SkedariUploaded(s,e);}" SelectedIndexChanged="function(s,e){onSelectedIndexChangeserial()}"
                                                LostFocus="function (s,e){LostFocusSerial();}" />
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
                                            runat="server" Text="Emri i Skedarit" ClientVisible="false">
                                        </dx:ASPxLabel>


                                        <dx:ASPxUploadControl ID="ucEmerSkedari" runat="server" Width="100%" ClientInstanceName="ucEmerSkedari"
                                            OnFileUploadComplete="ucEmerSkedari_FileUploadComplete" SettingsLoadingPanel-ImagePosition="Top"  ClientVisible="false">
                                            <ClientSideEvents TextChanged="function (s,e){TextChangeFile();}"
                                                FilesUploadComplete="function(s,e){SkedariUploaded(s,e); }" />
                                            <ValidationSettings AllowedFileExtensions=".xls,.xlsx,.txt,.csv"
                                                MaxFileSizeErrorText="Skedari ka kaluar madhesine maximale 10MB">
                                            </ValidationSettings>
                                        </dx:ASPxUploadControl>

                                        <dx:ASPxButton ID="btnNgarko" Width="100%" runat="server" Text="Ngarko"
                                            ClientInstanceName="btnNgarko" AutoPostBack="False" CausesValidation="False" ClientVisible="false">
                                            <ClientSideEvents Click="function(s, e) { Ngarko(); 
}" />
                                        </dx:ASPxButton>

                                        <%-- </div>
                                    <div id="dvdata_DateEdit">--%>
                                        <dx:ASPxDateEdit ID="data_DateEdit" Width="100%" runat="server" ClientInstanceName="data_DateEdit"
                                            ShowShadow="False" ClientVisible="false">
                                            <ClientSideEvents DateChanged="function(s, e){ dtDok_changed(s, e, true); }" GotFocus="function(s, e){ dateGotFocus( s, e); }" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries">
                                                <RequiredField IsRequired="True" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
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
                                        <%--   <div id="dvlblAgjenti">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblAgjenti" AssociatedControlID="btnAgjenti" runat="server"
                                            Text="Agjenti" ClientInstanceName="lblAgjenti" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvbtnAgjenti">--%>
                                        <dx:ASPxComboBox ID="btnAgjenti" Width="100%" runat="server" ClientInstanceName="btnAgjenti"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">
                                            <ClientSideEvents LostFocus="function(s,e){changeRadio('1')}" TextChanged="function(s,e){changeRadio('1')}" ValueChanged="function(s,e){kontrollagjent('1')}" ButtonClick="function(s,e){ButtonClickAgjenti('1');}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <RequiredField IsRequired="True" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--  </div>--%>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneCaktoNeHarte" ID="lblCaktoNeHarte"
                                            runat="server" Text="Cakto ne harte:" ClientInstanceName="lblCaktoNeHarte" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btneCaktoNeHarte" runat="server" ClientInstanceName="btneCaktoNeHarte"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" Width="100%" ClientVisible="false">
                                            <ClientSideEvents ButtonClick="function(s, e){ hapLupeHarte(s, e); }" TextChanged="function(s, e){ vendosGeomNeHfState(s, e); }" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblKlientFurnitorVartes" AssociatedControlID="btneKlientfurnitorVartes" runat="server"
                                            Text="Klient/Furnitori vartes" ClientInstanceName="lblKlientFurnitorVartes" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <div style="max-width:400px;">
                                            <select id="btneKlientfurnitorVartes">
                                            </select>
                                        </div>
                                         <dx:ASPxLabel Wrap="False" ID="lblExpired" AssociatedControlID="lblExpired"
                                            runat="server" Text="Expired" ClientInstanceName="lblExpired" CssClass="Important" ClientVisible="false">
                                        </dx:ASPxLabel>

                                        <dx:ASPxLabel Wrap="False" ID="lblPerqindjeAgjent" AssociatedControlID="txtPerqindjeAgjent"
                                            runat="server" Text="Perqindje Agjent:" ClientInstanceName="lblPerqindjeAgjent" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvtxtPerqindjeAgjent">--%>
                                        <dx:ASPxTextBox ID="txtPerqindjeAgjent" runat="server" Width="100%" ClientInstanceName="txtPerqindjeAgjent" ClientVisible="false">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" KeyUp="function(s,e){changedPerqindjeAgjent('1');}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                <RegularExpression ValidationExpression="^[-+]?[0-9,.]*"></RegularExpression>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblAgjenti2" AssociatedControlID="btnAgjenti2" runat="server"
                                            Text="Agjenti 2: " ClientInstanceName="lblAgjenti2" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btnAgjenti2" Width="100%" runat="server" ClientInstanceName="btnAgjenti2"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">
                                            <ClientSideEvents LostFocus="function(s,e){changeRadio('2')}" TextChanged="function(s,e){changeRadio('2')}" ValueChanged="function(s,e){kontrollagjent('2')}" ButtonClick="function(s,e){ButtonClickAgjenti('2');}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblPerqindjeAgjent2" AssociatedControlID="txtPerqindjeAgjent2"
                                            runat="server" Text="Perqindje Agjenti 2: " ClientInstanceName="lblPerqindjeAgjent2" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtPerqindjeAgjent2" runat="server" Width="100%" ClientInstanceName="txtPerqindjeAgjent2" ClientVisible="false">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" KeyUp="function(s,e){changedPerqindjeAgjent('2');}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                <RegularExpression ValidationExpression="^[-+]?[0-9,.]*"></RegularExpression>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblAgjenti3" AssociatedControlID="btnAgjenti3" runat="server"
                                            Text="Agjenti 3: " ClientInstanceName="lblAgjenti3" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btnAgjenti3" Width="100%" runat="server" ClientInstanceName="btnAgjenti3"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">
                                            <ClientSideEvents LostFocus="function(s,e){changeRadio('3')}" TextChanged="function(s,e){changeRadio('3')}" ValueChanged="function(s,e){kontrollagjent('3')}" ButtonClick="function(s,e){ButtonClickAgjenti('3');}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblPerqindjeAgjent3" AssociatedControlID="txtPerqindjeAgjent3"
                                            runat="server" Text="Perqindje Agjenti 3: " ClientInstanceName="lblPerqindjeAgjent3" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtPerqindjeAgjent3" runat="server" Width="100%" ClientInstanceName="txtPerqindjeAgjent3" ClientVisible="false">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" KeyUp="function(s,e){changedPerqindjeAgjent('3');}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                <RegularExpression ValidationExpression="^[-+]?[0-9,.]*"></RegularExpression>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--   </div>--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblMuajRaportimi" AssociatedControlID="cmbMuajRaportimi"
                                            runat="server" Text="Muaj raportimi:" ClientInstanceName="lblMuajRaportimi" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbMuajRaportimi" ValueType="System.Int32" Width="100%" runat="server" ClientInstanceName="cmbMuajRaportimi"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">
                                            <ClientSideEvents SelectedIndexChanged="cmbMuajiSelectedChanged" />
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
                                        <dx:ASPxLabel Wrap="False" ID="lblVitRaportimi" AssociatedControlID="cmbVitRaportimi"
                                            runat="server" Text="Vit raportimi:" ClientInstanceName="lblVitRaportimi" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbVitRaportimi" Width="100%" runat="server" ClientInstanceName="cmbVitRaportimi"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" ClientVisible="false">
                                            <ClientSideEvents SelectedIndexChanged="cmbVitiSelectedChanged" />
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
                                        <dx:ASPxLabel Wrap="False" ID="lblAfatiKohor" AssociatedControlID="dteAfatiKohor"
                                            runat="server" Text="Data" ClientInstanceName="lblAfatiKohor" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvdata_DateEdit">--%>


                                        <%--     </div> --%>

                                        <dx:ASPxDateEdit ID="dteAfatiKohor" Width="100%" runat="server" ClientInstanceName="dteAfatiKohor"
                                            ShowShadow="False" ClientVisible="false">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <dx:ASPxLabel Wrap="False" ID="lblDtFature" AssociatedControlID="DtFature_DateEdit"
                                            runat="server" Text="Dt Fature" ClientInstanceName="lblDtFature" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvdata_DateEdit">--%>


                                        <dx:ASPxDateEdit ID="DtFature_DateEdit" Width="100%" runat="server" ClientInstanceName="DtFature_DateEdit"
                                            ShowShadow="False" ClientVisible="false">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <%--     </div> --%>
                                        <dx:ASPxLabel Wrap="False" ID="lblDtFillimi" AssociatedControlID="DtFillimi_DateEdit"
                                            runat="server" Text="Dt Fillimi" ClientInstanceName="lblDtFillimi" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvdata_DateEdit">--%>


                                        <dx:ASPxDateEdit ID="DtFillimi_DateEdit" Width="100%" runat="server" ClientInstanceName="DtFillimi_DateEdit"
                                            ShowShadow="False" ClientVisible="false">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <%--     </div> --%>


                                        <dx:ASPxLabel Wrap="False" ID="lblDtMbarimi" AssociatedControlID="DtMbarimi_DateEdit"
                                            runat="server" Text="Dt Mbarimi" ClientInstanceName="lblDtMbarimi" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvdata_DateEdit">--%>


                                        <dx:ASPxDateEdit ID="DtMbarimi_DateEdit" Width="100%" runat="server" ClientInstanceName="DtMbarimi_DateEdit"
                                            ShowShadow="False" ClientVisible="false">
                                            <ClientSideEvents DateChanged="function(s, e){ DtMbarimi_changed(s, e); }" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>




                                        <dx:ASPxLabel Wrap="False" ID="lblAdresaFaturimit" AssociatedControlID="txtAdresaFaturimit"
                                            runat="server" Text="Adresa faturimit" ClientInstanceName="lblAdresaFaturimit" ClientVisible="false">
                                        </dx:ASPxLabel>


                                        <dx:ASPxMemo ID="txtAdresaFaturimit" Width="100%" ClientInstanceName="txtAdresaFaturimit"
                                         MaxLength="5000"   Rows="5" runat="server" ClientVisible="false">
                             <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
											ValidationGroup="entries" SetFocusOnError="True" RegularExpression-ValidationExpression="^[\s\S]{0,5000}$" RegularExpression-ErrorText="Fusha Adresa e Faturimit lejon deri ne 1000 karaktere.">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                        </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxMemo>
                                        <dx:ASPxLabel Wrap="False" ID="lblAdresaDergimit" AssociatedControlID="txtAdresaDergimit"
                                            runat="server" Text="Adresa dergimit" ClientInstanceName="lblAdresaDergimit" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxMemo ID="txtAdresaDergimit" Width="100%" ClientInstanceName="txtAdresaDergimit"
                                            Rows="5" runat="server" ClientVisible="false">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <RequiredField IsRequired="true" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxMemo>
                                        <dx:ASPxLabel Wrap="False" ID="lblMarresi" AssociatedControlID="txtMarresi"
                                            runat="server" Text="Marresi" ClientInstanceName="lblMarresi" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtMarresi" Width="100%" ClientInstanceName="txtMarresi" runat="server"  ClientVisible="false">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <RequiredField IsRequired="true" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblTransportues" AssociatedControlID="btnTransportues" runat="server"  ClientVisible="false"
                                            Text="Transportues" ClientInstanceName="lblTransportues">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btnTransportues" Width="100%" runat="server"  ClientVisible="false"
                                            ClientInstanceName="btnTransportues" ShowShadow="False"
                                            SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickTransportues( );}"
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
                                        <%--  </div>--%>
                                        <%--   <div id="dvlblMonedhat">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblMonedhat" AssociatedControlID="cmbMonedha" runat="server"  ClientVisible="false"
                                            Text="Monedha" ClientInstanceName="lblMonedhat">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvcmbMonedha">--%>
                                        <dx:ASPxComboBox ID="cmbMonedha" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbMonedha"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){SelectedIndexChangedMonedha();}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%-- </div>
                                    <div id="dvlblMenyreTransporti">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblMenyreTransporti" AssociatedControlID="btnMenyreTransporti"
                                            runat="server"  ClientVisible="false" Text="Menyre transporti" ClientInstanceName="lblMenyreTransporti">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvbtnMenyreTransporti">--%>
                                        <dx:ASPxComboBox ID="btnMenyreTransporti" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="btnMenyreTransporti"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickMenyreTransporti();}" />
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickMenyreTransporti();}"></ClientSideEvents>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
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
                                        <%-- </div>
                                    <div id="dvlblNumer">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblNumer" AssociatedControlID="txtNumer" runat="server"  ClientVisible="false"
                                            Text="Numer" ClientInstanceName="lblNumer">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvtxtNumer">--%>
                                        <dx:ASPxTextBox ID="txtNumer" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtNumer">
                                            <ClientSideEvents TextChanged="function (s,e){TextChangedNrDok(s,e);}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                RequiredField-ErrorText="Numri i dokumentit është i domosdoshëm">
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--  </div>
                                    <div id="dvlblDateTransportimi">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblDateTransportimi" AssociatedControlID="dateTransportimi_DateEdit"
                                            runat="server"  ClientVisible="false" Text="Date transportimi" ClientInstanceName="lblDateTransportimi">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvdateTransportimi_DateEdit">--%>
                                        <dx:ASPxDateEdit ID="dateTransportimi_DateEdit" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="dateTransportimi_DateEdit"
                                            ShowShadow="False">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
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
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <%--  </div>
                                    <div id="dvlblNumerSerial">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblNumerSerial" AssociatedControlID="txtNumerSerial"
                                            runat="server"  ClientVisible="false" Text="Numer serial" ClientInstanceName="lblNumerSerial">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvtxtNumerSerial">--%>
                                        <dx:ASPxTextBox ID="txtNumerSerial" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtNumerSerial">
                                            <ClientSideEvents TextChanged="function (s,e){NdryshoiSeriali(s,e);}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="False" />
                                                <RequiredField IsRequired="False"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--  </div>
                                    <div id="dvlblKushtDergimi">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblKushtDergimi" AssociatedControlID="btnKushtDergimi"
                                            runat="server"  ClientVisible="false" Text="Kushtet e dergimit" ClientInstanceName="lblKushtDergimi">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvbtnKushtDergimi">--%>
                                        <dx:ASPxComboBox ID="btnKushtDergimi" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="btnKushtDergimi"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickKushtDergimi();}" />
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickKushtDergimi();}"></ClientSideEvents>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
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
                                        <%-- </div>
                                    <div id="dvlblNumerProjekti">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblNumerProjekti" AssociatedControlID="txtNumerProjekti"
                                            runat="server"  ClientVisible="false" Text="Numer projekti" ClientInstanceName="lblNumerProjekti">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvtxtNumerProjekti">--%>
                                        <dx:ASPxTextBox ID="txtNumerProjekti" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtNumerProjekti">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--    </div>--%>
                                        <%--   <div id="dvlblKursi">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblKursi" AssociatedControlID="txtKursi" runat="server"  ClientVisible="false"
                                            Text="Kursi" ClientInstanceName="lblKursi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="txtKursi" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtKursi"
                                            ShowShadow="False" EnableClientSideAPI="True" IncrementalFilteringMode="Contains"
                                            EnableCallbackMode="False">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                LostFocus="function(s,e){ Utils.lostFocusTxtNumer(s, e); LostFocusKursi(); kontrolloKurs(s,e);}"
                                                ButtonClick="function(s, e){ ButtonClickKursi(s, e); }" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                RegularExpression-ValidationExpression="[0-9.,]*">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RegularExpression ValidationExpression="[0-9.,]*"></RegularExpression>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>

                                        <%--                                        <dx:ASPxTextBox ID="txtKursi" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtKursi">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                KeyUp="function(s,e){}" LostFocus="function(s,e){ Utils.lostFocusTxtNumer(s, e);LostFocusKursi();kontrolloKurs(s,e);}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                RegularExpression-ValidationExpression="[0-9.,]*">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ValidationExpression="[0-9.,]*"></RegularExpression>
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>--%>

                                        <%--    </dx:PanelContent>
                                        </PanelCollection>

                                    </dx:ASPxCallbackPanel >--%>

                                        <dx:ASPxLabel Wrap="False" ID="lblPershkrimi" AssociatedControlID="txtPershkrimi"
                                            runat="server"  ClientVisible="false" Text="Pershkrimi" ClientInstanceName="lblPershkrimi">
                                        </dx:ASPxLabel>

                                        <dx:ASPxMemo ID="txtPershkrimi" Width="100%" ClientInstanceName="txtPershkrimi" Rows="5"
                                            runat="server"  ClientVisible="false">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                                ValidateOnLeave="false">
                                                <%--<ErrorImage />--%>
                                                <%--<ErrorImage>
                                                </ErrorImage>--%>
                                                <RequiredField IsRequired="false"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="False"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxMemo>

                                        <dx:ASPxLabel Wrap="False" ID="lblShenime2" AssociatedControlID="txtShenime2"
                                            runat="server"  ClientVisible="false" Text="Shenime 2" ClientInstanceName="lblShenime2">
                                        </dx:ASPxLabel>
                                        <dx:ASPxMemo ID="txtShenime2" Width="100%" ClientInstanceName="txtShenime2" Rows="5"
                                            runat="server"  ClientVisible="false">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <RequiredField IsRequired="true" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxMemo>

                                        <dx:ASPxLabel Wrap="False" ID="lblKartaPaPagese" AssociatedControlID="cbKartaPaPagese"
                                            runat="server"  ClientVisible="false" Text="Karta pa pagese" ClientInstanceName="lblKartaPaPagese">
                                        </dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbKartaPaPagese" runat="server"  ClientVisible="false" ClientInstanceName="cbKartaPaPagese" Width="100%">
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <%--                                            <ClientSideEvents CheckedChanged="function(s, e) {
    
	kuponclick(s,e);
}" />--%>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxCheckBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblDogana" AssociatedControlID="cmbDogana" runat="server"  ClientVisible="false"
                                            Text="Dogana" ClientInstanceName="lblDogana">
                                        </dx:ASPxLabel>

                                        <dx:ASPxComboBox ID="cmbDogana" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbDogana"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){SelectedIndexChangedDogana();}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
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
                                        <%-- </div>
                                    <div id="dvlblFormatiPrintimit">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblFormatiPrintimit" AssociatedControlID="cmbFormatiPrintimit"
                                            runat="server"  ClientVisible="false" Text="Formati i printimit" ClientInstanceName="lblFormatiPrintimit">
                                        </dx:ASPxLabel>
                                        <%--   </div>
                                    <div id="dvcmbFormatiPrintimit">--%>
                                        <dx:ASPxComboBox ID="cmbFormatiPrintimit" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbFormatiPrintimit"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){}" />
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
                                        <%--   </div>
                                    <div id="dvlblDetyrimi">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblDetyrimi" AssociatedControlID="txtDetyrimi" runat="server"  ClientVisible="false"
                                            Text="Detyrimi" ClientInstanceName="lblDetyrimi">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvtxtDetyrimi">--%>
                                        <dx:ASPxTextBox ID="txtDetyrimi" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtDetyrimi"
                                            DisplayFormatString="0.00" BackColor="AliceBlue">
                                            <ClientSideEvents Init="function(s,e){}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%-- </div>
                                    <div id="dvlblMaturimi">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblMaturimi" AssociatedControlID="btnMaturimi" runat="server"  ClientVisible="false"
                                            Text="Maturimi" ClientInstanceName="lblMaturimi">
                                        </dx:ASPxLabel>
                                        <%--</div>
                                    <div id="dvbtnMaturimi">--%>
                                        <dx:ASPxComboBox ID="btnMaturimi" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="btnMaturimi"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickAfateMaturimi();}" LostFocus="function(s,e){LostFocusMaturimi();}" />
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickAfateMaturimi();}" LostFocus="function(s,e){LostFocusMaturimi();}"
                                                SelectedIndexChanged="function(s,e){Utils.SelektimiBosh(s,e);}"></ClientSideEvents>
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
                                        <%--  </div>
                                    <div id="dvlblDateMaturimi">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblDateMaturimi" AssociatedControlID="dateMaturimi_DateEdit"
                                            runat="server"  ClientVisible="false" Text="Dt maturimi" ClientInstanceName="lblDateMaturimi">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvdateMaturimi_DateEdit">--%>
                                        <dx:ASPxDateEdit ID="dateMaturimi_DateEdit" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="dateMaturimi_DateEdit"
                                            ShowShadow="False">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                <%--<ErrorImage Height="14px" Width="14px"  >
                                                </ErrorImage>--%>
                                                <RequiredField IsRequired="True" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <%--<ErrorImage Height="14px"   Width="14px" />--%>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <%-- </div>
                                    <div id="dvlblMagazina">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblMagazina" AssociatedControlID="btnMagazina" runat="server"  ClientVisible="false"
                                            Text="Magazina" ClientInstanceName="lblMagazina">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvbtnMagazina">--%>
                                        <dx:ASPxComboBox ID="btnMagazina" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="btnMagazina"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top"
                                            EnableSynchronization="True" OnItemRequestedByValue="btnMagazina_ItemRequestedByValue"
                                            OnItemsRequestedByFilterCondition="btnMagazina_ItemsRequestedByFilterCondition" EnableCallbackMode="True"
                                            EnableClientSideAPI="True" CallbackPageSize="10" IncrementalFilteringMode="Contains" ValueType="System.Int64">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickMagazina(s,e);}"
                                                TextChanged="function(s,e) {MagazinaChanged();}" />

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
                                                <RequiredField IsRequired="False"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--<div id="dvlblDateKerkese">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblDateKerkese" AssociatedControlID="DtKerkese_DateEdit"
                                            runat="server"  ClientVisible="false" Text="Dt Fillimi" ClientInstanceName="lblDateKerkese">
                                        </dx:ASPxLabel>
                                        <%-- <div id="dvdateKerkese_DateEdit">--%>
                                        <dx:ASPxDateEdit ID="DtKerkese_DateEdit" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="DtKerkese_DateEdit"
                                            ShowShadow="False">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxDateEdit>
                                        <%--   </div>
                                    <div id="dvlblPikeShitjeFurnizimi">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblPikeShitjeFurnizimi" AssociatedControlID="cmbPikeShitjeFurnizimi"
                                            runat="server"  ClientVisible="false" Text="Pike Shitje:" ClientInstanceName="lblPikeShitjeFurnizimi">
                                        </dx:ASPxLabel>
                                        <%--    </div>
                                    <div id="dvcmbPikeShitjeFurnizimi">--%>
                                        <dx:ASPxComboBox ID="cmbPikeShitjeFurnizimi" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbPikeShitjeFurnizimi"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents TextChanged="function(s,e) {TextChangedPika();}" />

                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--</div>
                                    <div id="dvlblDegeAdministrative">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblDegeAdministrative" AssociatedControlID="cmbDegeAdministrative"
                                            runat="server"  ClientVisible="false" Text="Inventarizimi:" ClientInstanceName="lblDegeAdministrative">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvcmbDegeAdministrative">--%>
                                        <dx:ASPxComboBox ID="cmbDegeAdministrative" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbDegeAdministrative"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents TextChanged="function(s,e) {TextChangedDega();}" />

                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" CausesValidation="True"
                                                ValidationGroup="entries" SetFocusOnError="true" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                                <RequiredField IsRequired="False"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbKupon" ID="lblKupon" runat="server"  ClientVisible="false"
                                            Text="Kupon per fature tatimore:" ClientInstanceName="lblKupon">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvcbAktiv">--%>
                                        <dx:ASPxCheckBox ID="cbKupon" runat="server"  ClientVisible="false" ClientInstanceName="cbKupon" Width="100%">
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <ClientSideEvents CheckedChanged="function(s, e) {
                                                kuponKerkonKlienti = false;
	                                            kuponclick(s,e);
}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxCheckBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbPrinto" ID="lblPrinto" runat="server"  ClientVisible="false"
                                            Text="Printo:" ClientInstanceName="lblPrinto">
                                        </dx:ASPxLabel>

                                        <dx:ASPxCheckBox ID="cbPrinto" runat="server"  ClientVisible="false" ClientInstanceName="cbPrinto" Width="100%">
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

                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbShpenzimeJoTeZbritshme" ID="lblShpenzimeJoTeZbritshme" runat="server"  ClientVisible="false"
                                            Text="Shpenzime jo te zbritshme:" ClientInstanceName="lblShpenzimeJoTeZbritshme">
                                        </dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbShpenzimeJoTeZbritshme" runat="server"  ClientVisible="false" ClientInstanceName="cbShpenzimeJoTeZbritshme" Width="100%">
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

                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbKasa" ID="lblKase" runat="server"  ClientVisible="false"
                                            Text="Printo ne kase:" ClientInstanceName="lblKase">
                                        </dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbKasa" runat="server"  ClientVisible="false" ClientInstanceName="cbKasa" Width="100%">
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <ClientSideEvents CheckedChanged="function(s, e) { }" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxCheckBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbRenditje" ID="lblRenditje" runat="server"  ClientVisible="false"
                                            Text="Ruaj renditje:" ClientInstanceName="lblRenditje">
                                        </dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbRenditje" runat="server"  ClientVisible="false" ClientInstanceName="cbRenditje" Width="100%">
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbGaranci" ID="lblGaranci" runat="server"  ClientVisible="false"
                                            Text="Printo garanci:" ClientInstanceName="lblGaranci">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvcbAktiv">--%>
                                        <dx:ASPxCheckBox ID="cbGaranci" runat="server"  ClientVisible="false" ClientInstanceName="cbGaranci" Width="100%">
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <ClientSideEvents CheckedChanged="function(s, e) {

}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxCheckBox>
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cbGaranci" ID="lblDergoMeEmail" runat="server"  ClientVisible="false"
                                            Text="Dergo me Email" ClientInstanceName="lblDergoMeEmail">
                                        </dx:ASPxLabel>
                                        <%--</div>--%>
                                        <%--<div id="dvcbAktiv">--%>
                                        <dx:ASPxCheckBox ID="cbDergoMeEmail" runat="server"  ClientVisible="false" ClientInstanceName="cbDergoMeEmail" Width="100%">
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="btneAutomjeti" ID="lblAutomjeti" runat="server"  ClientVisible="false" ClientInstanceName="lblAutomjeti">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="btneAutomjeti" runat="server"  ClientVisible="false" ClientInstanceName="btneAutomjeti"
                                            Width="100%" ShowShadow="False" ValueType="System.Int32" EnableClientSideAPI="True"
                                            IncrementalFilteringMode="Contains" EnableSynchronization="True" EnableCallbackMode="True"
                                            DropDownRows="3" CallbackPageSize="10" OnItemRequestedByValue="btneAutomjeti_ItemRequestedByValue"
                                            OnItemsRequestedByFilterCondition="btneAutomjeti_ItemsRequestedByFilterCondition"
                                            SettingsLoadingPanel-ImagePosition="Top" AllowNull="true">
                                            <ClientSideEvents ButtonClick="function(s, e) { Auto_Click(); }" TextChanged="function(s, e) {textChangedAuto(s,e);}" />
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtTarga" ID="lblTarga" Text="Targa" runat="server"  ClientVisible="false" ClientInstanceName="lblTarga">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTarga" runat="server"  ClientVisible="false" Width="100%" AutoPostBack="false" ClientInstanceName="txtTarga">
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

                                        <dx:ASPxLabel Wrap="False" ID="lblPershkrimMagazine" AssociatedControlID="lblPershkrimMagazine" runat="server"  ClientVisible="false"
                                            Text="Pershkrim mag" ClientInstanceName="lblPershkrimMagazine">
                                        </dx:ASPxLabel>

                                        <dx:ASPxTextBox ID="txtPershkrimMagazine" runat="server"  ClientVisible="false" Width="100%" AutoPostBack="false" ClientInstanceName="txtPershkrimMagazine">
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
                                        <dx:ASPxLabel Wrap="False" ID="lblPershkrimDege" AssociatedControlID="lblPershkrimDege" runat="server"  ClientVisible="false"
                                            Text="Pershkrim dege" ClientInstanceName="lblPershkrimDege">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtPershkrimDege" runat="server"  ClientVisible="false" Width="100%" AutoPostBack="false" ClientInstanceName="txtPershkrimDege">
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

                                        <dx:ASPxLabel Wrap="False" ID="ASPxLabel1" AssociatedControlID="lblPershkrimPike" runat="server"  ClientVisible="false"
                                            Text="Pershkrim pike" ClientInstanceName="lblPershkrimPike">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtPershkrimPike" runat="server"  ClientVisible="false" Width="100%" AutoPostBack="false" ClientInstanceName="txtPershkrimPike">
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

                                        <dx:ASPxLabel Wrap="False" ID="lblKarta" AssociatedControlID="cmbKarta"
                                            runat="server"  ClientVisible="false" Text="Karta:" ClientInstanceName="lblKarta">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbKarta" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbKarta" EnableSynchronization="True"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" OnItemRequestedByValue="cmbKarta_ItemRequestedByValue"
                                            OnItemsRequestedByFilterCondition="cmbKarta_ItemsRequestedByFilterCondition" EnableCallbackMode="True"
                                            EnableClientSideAPI="True" CallbackPageSize="10" IncrementalFilteringMode="Contains" ValueType="System.Int64">
                                            <ClientSideEvents TextChanged="TextChangedKarta" SelectedIndexChanged="TextChangedKarta" ButtonClick="ButtonClickKarta" />
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
                                        <dx:ASPxLabel Wrap="False" ID="lblPike" AssociatedControlID="txtPike"
                                            runat="server"  ClientVisible="false" Text="Pike:" ClientInstanceName="lblPike">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Wrap="False" ID="txtPike" Width="100%"
                                            runat="server"  ClientVisible="false" Text="0" ClientInstanceName="txtPike">
                                        </dx:ASPxTextBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblTotalPike" AssociatedControlID="txtTotalPike"
                                            runat="server"  ClientVisible="false" Text="Pike:" ClientInstanceName="lblTotalPike">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Wrap="False" ID="txtTotalPike" Width="100%"
                                            runat="server"  ClientVisible="false" Text="0" ClientInstanceName="txtTotalPike">
                                        </dx:ASPxTextBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblTarga2" AssociatedControlID="txtTarga2"
                                            runat="server"  ClientVisible="false" Text="Targa Shoferit:" ClientInstanceName="lblTarga2">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Wrap="False" ID="txtTarga2" Width="100%"
                                            runat="server"  ClientVisible="false" Text="0" ClientInstanceName="txtTarga2">
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

                                        <dx:ASPxLabel Wrap="False" ID="lblShoferi" AssociatedControlID="txtShoferi"
                                            runat="server"  ClientVisible="false" Text="Shoferi:" ClientInstanceName="lblShoferi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox Wrap="False" ID="txtShoferi" Width="100%"
                                            runat="server"  ClientVisible="false" Text="0" ClientInstanceName="txtShoferi">
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

                                        <dx:ASPxButton ID="btnFazat" runat="server"  ClientVisible="false" Text="Fazat" ClientEnabled="true" ClientInstanceName="btnFazat" CausesValidation="false">
                                            <ClientSideEvents Click="function(s,e){ButtonClickFazat(); e.processOnServer=false;}" />
                                        </dx:ASPxButton>

                                        <dx:ASPxLabel Wrap="False" ID="lblFaza" AssociatedControlID="cmbFaza"
                                            runat="server"  ClientVisible="false" Text="Faza Kontrates:" ClientInstanceName="lblFaza">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbFaza" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbFaza"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" OnItemRequestedByValue="cmbFaza_ItemRequestedByValue" DropDownStyle="DropDown" EnableCallbackMode="True" EnableClientSideAPI="True" CallbackPageSize="10" IncrementalFilteringMode="Contains" ValueType="System.Int64">
                                            <%--<ClientSideEvents TextChanged="function(s,e) {TextChangedKarta();}"/>--%>
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


                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimKase" AssociatedControlID="cmbKonfigurimKase"
                                            runat="server"  ClientVisible="false" Text="Konfigurimi kase:" ClientInstanceName="lblKonfigurimKase">
                                        </dx:ASPxLabel>
                                        <%--   </div>
                                    <div id="dvcmbFormatiPrintimit">--%>
                                        <dx:ASPxComboBox ID="cmbKonfigurimKase" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbKonfigurimKase"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents Init="cmbKonfigurimKaseInit" SelectedIndexChanged="cmbKonfigurimKaseSelectedChanged" />
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKilometra" ID="lblKilometra" runat="server"  ClientVisible="false" ClientInstanceName="lblKilometra">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtKilometra" runat="server"  ClientVisible="false" Width="100%" AutoPostBack="false" ClientInstanceName="txtKilometra">
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
                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodMotorri" ID="lblKodMotorri" runat="server"  ClientVisible="false" ClientInstanceName="lblKodMotorri">
                                        </dx:ASPxLabel>
                                        <%--<div id="dvlblKerkuarNga">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblKerkuarNga" AssociatedControlID="txtKerkuarNga" runat="server"  ClientVisible="false"
                                            Text="Kerkuar nga:" ClientInstanceName="lblKerkuarNga">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtKerkuarNga" runat="server"  ClientVisible="false" Width="100%" AutoPostBack="false" ClientInstanceName="txtKerkuarNga">
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

                                        <dx:ASPxLabel Wrap="False" ID="lblNrDokMagazine" AssociatedControlID="txtNrDokMagazine"
                                            runat="server"  ClientVisible="false" Text="Numer dokumenti magazine" ClientInstanceName="lblNrDokMagazine">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtNrDokMagazine" Width="100%" ClientInstanceName="txtNrDokMagazine" runat="server"  ClientVisible="false">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                                ValidateOnLeave="False">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblIIC" AssociatedControlID="txtIIC"
                                            runat="server"  ClientVisible="false" Text="Kodi i faturës së lëshuesve" ClientInstanceName="lblIIC">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtIIC" Width="100%" ClientInstanceName="txtIIC" runat="server"  ClientVisible="false">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                                ValidateOnLeave="False">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>

                                         <dx:ASPxLabel Wrap="False" ID="lblNIVF" AssociatedControlID="txtIIC"
                                            runat="server"  ClientVisible="false" Text="Kodi i identifikimit fiskal te fatures" ClientInstanceName="lblNIVF">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtNIVF" Width="100%" ClientInstanceName="txtNIVF" runat="server"  ClientVisible="false">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                                ValidateOnLeave="False">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
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
                                <dx:ASPxLabel Wrap="False" AssociatedControlID="DocumentsUc" ID="lblDocumentEinvoice" 
                                    ClientInstanceName="lblDocumentEinvoice" runat="server" Text="Ngarko Dokumentin:" ClientVisible="False">
                                    </dx:ASPxLabel>
                                  <ContentTemplate>
                                    <dx:ASPxUploadControl ID="UcDocumentEinvoice" runat="server" ClientInstanceName="UcDocumentEinvoice"  Width="100%" ClientEnabled="False" ClientVisible="False" 
                                        NullText="Zgjidhni Dokumentat Einvoice..." OnFileUploadComplete="UcDocumentEinvoice_FileUploadComplete" ShowProgressPanel="False">
                                        <AdvancedModeSettings EnableMultiSelect="True" EnableFileList="False"/>
                                         <ValidationSettings MaxFileSizeErrorText="File qe keni zgjedhur eshte shume i madh!"
                                          AllowedFileExtensions=".csv, .pdf, .jpeg, .png"
                                           MaxFileSize="4194304">
                                         </ValidationSettings>
                                    </dx:ASPxUploadControl>
                                      <dx:ASPxButton ID="upload" runat="server" AutoPostBack="False" Text="Ngarko"
                                      ClientInstanceName="upload" ClientEnabled="True" OnClick="upload_Clilck" ClientVisible="False">
                                       <ClientSideEvents Click="function(s, e) { UcDocumentEinvoice.Upload() ; }" />
                                     </dx:ASPxButton>
                                     </ContentTemplate>
                                        <dx:ASPxLabel Wrap="False" ID="lblNivfKthim" runat="server" ClientInstanceName="lblNivfKthim"
                                            AssociatedControlID="txtNivfKthim">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtNivfKthim" Width="100%" ClientVisible="false" runat="server" ClientInstanceName="txtNivfKthim"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                           
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <RequiredField IsRequired="False" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblEinvoice" AssociatedControlID="cbEinvoice"
                                            runat="server"  ClientVisible="false" ClientInstanceName="lblEinvoice">
                                        </dx:ASPxLabel>
                                        <dx:ASPxCheckBox ID="cbEinvoice" runat="server" ClientVisible="false" ClientInstanceName="cbEinvoice" Width="100%">
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                            <ClientSideEvents CheckedChanged="function (s,e){DokumentEincoiceUpload(s,e);}" />
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                                                ValidationGroup="entries1" ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="false" />
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
                                                <RequiredField IsRequired="False" />
                                            </ValidationSettings>
                                        </dx:ASPxCheckBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblEIC" runat="server" ClientInstanceName="lblEIC"
                                            AssociatedControlID="txtEIC">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtEIC" Width="100%" runat="server" ClientVisible="false" ClientInstanceName="txtEIC"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                           
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip"  Display="Dynamic" ValidationGroup="entries"
                                                ValidateOnLeave="false">
                                                <RequiredField IsRequired="False" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblEinStatus" AssociatedControlID="cmbEinStatus"
                                            runat="server"  ClientVisible="false" ClientInstanceName="lblEinStatus">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbEinStatus" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbEinStatus"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
<%--                                            <ClientSideEvents SelectedIndexChanged="function (s,e){MenyrePageseChanged(s,e);}" />--%>
                                            <DropDownButton>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ValidateOnLeave="False"
                                                ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblTipiIVetefaturimit" AssociatedControlID="cmbTipiIVetefaturimit"
                                            runat="server"  ClientVisible="false" ClientInstanceName="lblTipiIVetefaturimit">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbTipiIVetefaturimit" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbTipiIVetefaturimit"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
<%--                                            <ClientSideEvents SelectedIndexChanged="function (s,e){MenyrePageseChanged(s,e);}" />--%>
                                            <DropDownButton>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ValidateOnLeave="False"
                                                                ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="False" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lbleInvoiceType" AssociatedControlID="cmbeInvoiceType"
                                            runat="server"  ClientVisible="false" ClientInstanceName="lbleInvoiceType">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbeInvoiceType" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbeInvoiceType"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
<%--                                            <ClientSideEvents SelectedIndexChanged="function (s,e){MenyrePageseChanged(s,e);}" />--%>
                                            <DropDownButton>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ValidateOnLeave="False"
                                                ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblProcesi" AssociatedControlID="cmbProcesi"
                                            runat="server"  ClientVisible="false" Text="Menyre pagese" ClientInstanceName="lblProcesi">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbProcesi" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbProcesi"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
<%--                                            <ClientSideEvents SelectedIndexChanged="function (s,e){MenyrePageseChanged(s,e);}" />--%>
                                            <DropDownButton>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ValidateOnLeave="False"
                                                ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                    </div>

                                    <dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server"  ClientVisible="false" ClientInstanceName="hfNrAutoShitje">
                                    </dx:ASPxHiddenField>
                                    <dx:ASPxHiddenField ID="hfNrAuto" runat="server"  ClientVisible="false" ClientInstanceName="hfNrAuto">
                                    </dx:ASPxHiddenField>
                                    <div id="dvFundi" style="display: none;">
                                        <%--<div id="dvlblMonedheFature">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblMonedheFature" runat="server"  ClientVisible="false" Text="Monedhe fature"
                                            ClientInstanceName="lblMonedheFature">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvlblMonedhaBaze">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblMonedhaBaze" runat="server"  ClientVisible="false" Text="Monedhe baze"
                                            ClientInstanceName="lblMonedhaBaze">
                                        </dx:ASPxLabel>
                                        <dx:ASPxLabel Wrap="False" ID="lblMenyrePagese" AssociatedControlID="cmbMenyrePagese"
                                            runat="server"  ClientVisible="false" Text="Menyre pagese" ClientInstanceName="lblMenyrePagese">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbMenyrePagese" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbMenyrePagese"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents SelectedIndexChanged="function (s,e){MenyrePageseChanged(s,e);}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ValidateOnLeave="False"
                                                ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>

                                        <%--   <div id="dvlblArka">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblArka" EnableCallbackMode="True" ShowShadow="False" ValueType="System.String" AssociatedControlID="btneArka" runat="server"  ClientVisible="false"
                                            Text="Arka" ClientInstanceName="lblArka">
                                        </dx:ASPxLabel>

                                        <dx:ASPxComboBox ID="btneArka" runat="server"  ClientVisible="false" ClientInstanceName="btneArka"
                                            ShowShadow="False" ValueType="System.String" EnableClientSideAPI="True" IncrementalFilteringMode="Contains"
                                            EnableCallbackMode="True" CallbackPageSize="10"
                                            OnItemRequestedByValue="btneArka_ItemRequestedByValue" Width="100%" OnItemsRequestedByFilterCondition="btneArka_ItemsRequestedByFilterCondition">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickArka();}" TextChanged="function(s,e){TextChangedArka()}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>

                                        <%-- </div>
                                    <div id="dvpagesa_HyperLink">--%>
                                        <dx:ASPxHyperLink ID="pagesa_HyperLink" runat="server" Text="Pagese qe ne shitje"
                                            ClientInstanceName="pagesa_HyperLink" ClientVisible="false"/>
                                        <%-- </div>
                                    <div id="dvlblZbritjeTotal" align="right">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblZbritjeTotal" runat="server" AssociatedControlID="txtTotalMeZbritje1"
                                            Text="Zbritje ne total" ClientInstanceName="lblZbritjeTotal" RightToLeft="True" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%--   </div>
                                    <div id="dvlblTotal">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblTotal" AssociatedControlID="txtTotal1" runat="server"
                                            Text="Total" ClientInstanceName="lblTotal" Font-Bold="True" ClientVisible="false">
                                        </dx:ASPxLabel>
                                        <%--     </div>
                                    <div id="dvtxtTotal1">--%>

                                        <dx:ASPxTextBox ID="txtTotal1" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtTotal1"
                                            Font-Bold="True">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                                Display="Dynamic" ErrorText="" ErrorTextPosition="Left">
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--  </div>
                                    <div id="dvtxtTotal2">--%>
                                        <dx:ASPxTextBox ID="txtTotal2" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtTotal2"
                                            Font-Bold="True">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                                Display="Dynamic" ErrorText="">
                                                <%--<ErrorImage   />--%>
                                                <%--<ErrorImage  >
                                                </ErrorImage>--%>
                                                <RequiredField IsRequired="true" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--    </div>
                                    <div id="dvlblKushtPagese">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblKushtPagese" AssociatedControlID="btnKushtPagese"
                                            runat="server"  ClientVisible="false" Text="Kusht pagese" ClientInstanceName="lblKushtPagese">
                                        </dx:ASPxLabel>
                                        <%--    </div>
                                    <div id="dvbtnKushtPagese">--%>
                                        <dx:ASPxComboBox ID="btnKushtPagese" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="btnKushtPagese"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickKushtPagese();}" />
                                            <ClientSideEvents ButtonClick="function(s,e){ButtonClickKushtPagese();}"></ClientSideEvents>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
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
                                        <%--  </div>
                                    <div id="dvlblShitesi">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblShitesi" AssociatedControlID="btnShitesi" runat="server"  ClientVisible="false"
                                            Text="Shitesi" ClientInstanceName="lblShitesi">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvbtnShitesi">--%>
                                        <dx:ASPxComboBox ID="btnShitesi" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="btnShitesi"
                                            ClientEnabled="False" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
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
                                        <%--  </div>
                                    <div id="dvlblPerqindje">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblPerqindje" AssociatedControlID="txtPerqindje" runat="server"  ClientVisible="false"
                                            Text="Perqindje" ClientInstanceName="lblPerqindje">
                                        </dx:ASPxLabel>

                                        <dx:ASPxLabel Wrap="False" ID="lblTotalLitra" AssociatedControlID="txtTotalLitra" runat="server"  ClientVisible="false"
                                            Text="Totali i litrave:" ClientInstanceName="lblTotalLitra">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvlblVlefte">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblVlefte" AssociatedControlID="txtVlefte" runat="server"  ClientVisible="false"
                                            Text="Vlefte" ClientInstanceName="lblVlefte">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvlblTotalMeZbritje">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblTotalMeZbritje" AssociatedControlID="txtTotalMeZbritje2"
                                            runat="server"  ClientVisible="false" Text="Total me zbritje" ClientInstanceName="lblTotalMeZbritje">
                                        </dx:ASPxLabel>
                                        <%--    </div>
                                    <div id="dvtxtTotalMeZbritje1">--%>
                                        <dx:ASPxTextBox ID="txtTotalMeZbritje1" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtTotalMeZbritje1">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                                Display="Dynamic" ErrorText="">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--    </div>
                                    <div id="dvtxtTotalMeZbritje2">--%>
                                        <dx:ASPxTextBox ID="txtTotalMeZbritje2" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtTotalMeZbritje2">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorText="">
                                                <%--<ErrorImage   />--%>
                                                <%--<ErrorImage  >
                                                </ErrorImage>--%>
                                                <RequiredField IsRequired="true" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%-- </div>
                                    <div id="dvtxtPerqindje">--%>
                                        <dx:ASPxTextBox ID="txtPerqindje" Width="100%" runat="server"  ClientVisible="false" ClientEnabled="false" ClientInstanceName="txtPerqindje">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e); }" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                TextChanged="function(s,e){setTimeout(changedPerqindje,0);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries">
                                                <RegularExpression ValidationExpression="^[-+]?[0-9,.]*"></RegularExpression>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--       </div>
                                    <div id="dvtxtVlefte">--%>
                                        <dx:ASPxTextBox ID="txtVlefte" Width="100%" runat="server"  ClientVisible="false" ClientEnabled="false" ClientInstanceName="txtVlefte">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e);}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                TextChanged="function(s,e){setTimeout(changedVlefteZbritje,0);}"
                                                LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                RegularExpression-ValidationExpression="^[-+]?[0-9,.]*">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ValidationExpression="^[-+]?[0-9,.]*"></RegularExpression>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>

                                        <dx:ASPxTextBox ID="txtTotalLitra" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtTotalLitra">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                TextChanged="function(s,e){changedVlefteZbritje();}"
                                                LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);LostFocusVlefteZbritje();}" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                RegularExpression-ValidationExpression="^[-+]?[0-9,.]*">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ValidationExpression="^[-+]?[0-9,.]*"></RegularExpression>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--     </div>
                                    <div id="dvlblTVSH">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblTVSH" runat="server"  ClientVisible="false" Text="TVSH" AssociatedControlID="txtTVSH1" ClientInstanceName="lblTVSH">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvtxtTVSH1">--%>
                                        <dx:ASPxTextBox ID="txtTVSH1" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtTVSH1">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="None"
                                                ErrorText="">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="false" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--    </div>
                                    <div id="dvtxtTVSH2">--%>
                                        <dx:ASPxTextBox ID="txtTVSH2" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtTVSH2">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="None"
                                                ErrorText="">
                                                <%--<ErrorImage   />--%>
                                                <%--<ErrorImage  >
                                                </ErrorImage>--%>
                                                <RequiredField IsRequired="true" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--   </div>
                                    <div id="dvlblTotaliPaTVSH">--%>
                                        <dx:ASPxLabel ID="lblTotaliPaTVSH" runat="server"  ClientVisible="false" Text="Totali pa TVSH" AssociatedControlID="txtTotaliPaTVSH1" ClientInstanceName="lblTotaliPaTVSH"
                                            Wrap="False">
                                        </dx:ASPxLabel>
                                        <%--   </div>
                                    <div id="dvtxtTotaliPaTVSH1">--%>
                                        <dx:ASPxTextBox ID="txtTotaliPaTVSH1" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtTotaliPaTVSH1">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="None"
                                                ErrorText="">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%-- </div>
                                    <div id="dvtxtTotaliPaTVSH2">--%>
                                        <dx:ASPxTextBox ID="txtTotaliPaTVSH2" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtTotaliPaTVSH2">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="None"
                                                ErrorText="">
                                                <%--<ErrorImage   />--%>
                                                <%--<ErrorImage  >
                                                </ErrorImage>--%>
                                                <RequiredField IsRequired="true" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel ID="lblTotaliMeZbritjePaTVSH" runat="server"  ClientVisible="false" Text="Totali me zbritje pa TVSH" AssociatedControlID="txtTotaliMeZbritjePaTVSH1" ClientInstanceName="lblTotaliMeZbritjePaTVSH"
                                            Wrap="False">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtTotaliMeZbritjePaTVSH1" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtTotaliMeZbritjePaTVSH1">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="None"
                                                ErrorText="">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%-- </div>
                                    <div id="dvtxtTotaliPaTVSH2">--%>
                                        <dx:ASPxTextBox ID="txtTotaliMeZbritjePaTVSH2" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtTotaliMeZbritjePaTVSH2">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="None"
                                                ErrorText="">
                                                <%--<ErrorImage   />--%>
                                                <%--<ErrorImage  >
                                                </ErrorImage>--%>
                                                <RequiredField IsRequired="true" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--   </div>
                                    <div id="dvlblKrediti">--%>
                                        <dx:ASPxLabel ID="lblKrediti" AssociatedControlID="txtKrediti" runat="server"  ClientVisible="false"
                                            Text="Krediti" ClientInstanceName="lblKrediti" Wrap="False">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvtxtKrediti">--%>
                                        <dx:ASPxTextBox ID="txtKrediti" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtKrediti"
                                            BackColor="AliceBlue" DisplayFormatString="0.00">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%-- </div>
                                    <div id="dvlblLimit">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblLimit" AssociatedControlID="txtLimit" runat="server"  ClientVisible="false"
                                            Text="Limiti" ClientInstanceName="lblLimit">
                                        </dx:ASPxLabel>
                                        <%--   </div>
                                    <div id="dvtxtLimit">--%>
                                        <dx:ASPxTextBox ID="txtLimit" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtLimit"
                                            BackColor="AliceBlue" DisplayFormatString="0.00">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%-- </div>
                                    <div id="dvlblDateRegjistrimi">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblDateRegjistrimi" AssociatedControlID="dateRegjistrimi_DateEdit"
                                            runat="server"  ClientVisible="false" Text="Date regjistrimi" ClientInstanceName="lblDateRegjistrimi">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvdateRegjistrimi_DateEdit">--%>
                                        <dx:ASPxDateEdit ID="dateRegjistrimi_DateEdit" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="dateRegjistrimi_DateEdit"
                                            ShowShadow="False">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <CalendarProperties>
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="17px" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                        <%--   </div>
                                    <div id="dvlblMonedhaPagese">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblMonedhaPagese" AssociatedControlID="cmbMonedhaPagese"
                                            runat="server"  ClientVisible="false" Text="Monedhe Pagese" ClientInstanceName="lblMonedhaPagese">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvcmbMonedhaPagese">--%>
                                        <dx:ASPxComboBox ID="cmbMonedhaPagese" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbMonedhaPagese"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){SelectedIndexChangedMonedhaPagese();}" />
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){SelectedIndexChangedMonedhaPagese();}"></ClientSideEvents>
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false">
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

                                        <dx:ASPxLabel Wrap="False" ID="lblKursiPagese" AssociatedControlID="txtKursiPagese"
                                            runat="server"  ClientVisible="false" Text="Kursi" ClientInstanceName="lblKursiPagese">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="txtKursiPagese" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtKursiPagese"
                                            ShowShadow="False" EnableClientSideAPI="True" IncrementalFilteringMode="Contains"
                                            EnableCallbackMode="True" CallbackPageSize="10" SettingsLoadingPanel-ImagePosition="Top">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" KeyUp="function(s,e){LostFocusKursiPagese();}" />
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                RegularExpression-ValidationExpression="[0-9.,]*">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RegularExpression ValidationExpression="[0-9.,]*"></RegularExpression>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblVleftePagese" AssociatedControlID="txtVleftePagese"
                                            runat="server"  ClientVisible="false" Text="Vlefta(Monedhe)" ClientInstanceName="lblVleftePagese" Font-Bold="False"
                                            Font-Size="14" ForeColor="Black">
                                        </dx:ASPxLabel>
                                        <dx:ASPxTextBox ID="txtVleftePagese" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="txtVleftePagese"
                                            Font-Bold="True" Font-Size="14">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                                                LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="None"
                                                ErrorText="">
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="Black" ForeColor="#00FF00">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--     </div>
                                    <div id="dvlblPaguar">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblPaguar" AssociatedControlID="txtPaguar" runat="server"  ClientVisible="false"
                                            Text="Paguar" ClientInstanceName="lblPaguar" Font-Bold="False" Font-Size="14">
                                        </dx:ASPxLabel>
                                        <%--   </div>
                                    <div id="dvtxtPaguar" align="right">--%>
                                        <dx:ASPxTextBox ID="txtPaguar" runat="server"  ClientVisible="false" Width="100%" ClientInstanceName="txtPaguar"
                                            Font-Bold="True" Font-Size="13">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e); lostFocusTxtNumer()}" KeyUp="function (s,e){LlogaritResto()}" />

                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="None"
                                                RegularExpression-ValidationExpression="[0-9,.]*" ErrorText="">
                                                <RegularExpression ValidationExpression="[0-9,.]*"></RegularExpression>
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--  </div>
                                    <div id="dvlblResto">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblResto" AssociatedControlID="txtResto" runat="server"  ClientVisible="false"
                                            Text="Resto" ClientInstanceName="lblResto" Font-Bold="False" Font-Size="14">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvtxtResto" align="right">--%>
                                        <dx:ASPxTextBox ID="txtResto" runat="server"  ClientVisible="false" Width="100%" ClientInstanceName="txtResto"
                                            Font-Bold="True" Font-Size="13">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="None"
                                                ErrorText="">
                                                <RequiredField IsRequired="true" />
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel Wrap="False" ID="lblCash" AssociatedControlID="txtCash" runat="server"  ClientVisible="false"
                                            Text="Cash" ClientInstanceName="lblCash">
                                        </dx:ASPxLabel>
                                        <%--  </div>
                                    <div id="dvtxtResto" align="right">--%>
                                        <dx:ASPxTextBox ID="txtCash" runat="server"  ClientVisible="false" Width="100%" ClientInstanceName="txtCash"
                                            DisplayFormatString="0.00">
                                            <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}" LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                                            <ValidationSettings ValidationGroup="entries1" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip"
                                                ErrorText="">
                                                <RequiredField IsRequired="true" />
                                                <RegularExpression ValidationExpression="^[-+]?[0-9,.]*"></RegularExpression>
                                            </ValidationSettings>
                                            <DisabledStyle BackColor="#EEEEEE" ForeColor="Black">
                                            </DisabledStyle>
                                        </dx:ASPxTextBox>
                                        <%--    </div>
                                    <div id="dvlblGrup1">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblGrup1" AssociatedControlID="cmbGrup1" runat="server"  ClientVisible="false"
                                            Text="Grupim 1:" ClientInstanceName="lblGrup1">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvcmbGrup1">--%>
                                        <dx:ASPxComboBox ID="cmbGrup1" runat="server"  ClientVisible="false" Width="100%" ClientInstanceName="cmbGrup1"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>

                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries1"
                                                ValidateOnLeave="false"
                                                CausesValidation="True">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--   </div>--%>

                                        <%-- <div id="dvlblGrup2">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblGrup2" AssociatedControlID="cmbGrup2" runat="server"  ClientVisible="false"
                                            Text="Grupim 2:" ClientInstanceName="lblGrup2">
                                        </dx:ASPxLabel>
                                        <%--    </div>
                                    <div id="dvcmbGrup2">--%>
                                        <dx:ASPxComboBox ID="cmbGrup2" runat="server"  ClientVisible="false" Width="100%" ClientInstanceName="cmbGrup2"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidateOnLeave="false"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--      </div>
                                    <div id="dvlblGrup3">--%>
                                        <dx:ASPxLabel Wrap="False" ID="lblGrup3" AssociatedControlID="cmbGrup3" runat="server"  ClientVisible="false"
                                            Text="Grupim 3:" ClientInstanceName="lblGrup3">
                                        </dx:ASPxLabel>
                                        <%-- </div>
                                    <div id="dvcmbGrup3">--%>
                                        <dx:ASPxComboBox ID="cmbGrup3" runat="server"  ClientVisible="false" Width="100%" ClientInstanceName="cmbGrup3"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidateOnLeave="false"
                                                ValidationGroup="entries" SetFocusOnError="true">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="*" IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>

                                        <dx:ASPxLabel Wrap="False" ID="lblLlojZbritje" AssociatedControlID="cmbLlojZbritje"
                                            runat="server"  ClientVisible="false" Text="Lloji" ClientInstanceName="lblLlojZbritje">
                                        </dx:ASPxLabel>
                                        <dx:ASPxComboBox ID="cmbLlojZbritje" Width="100%" runat="server"  ClientVisible="false" ClientInstanceName="cmbLlojZbritje"
                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">

                                            <DropDownButton>
                                                <Image>
                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                </Image>
                                            </DropDownButton>
                                            <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ValidateOnLeave="False"
                                                ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--<br />--%>
                                    </div>
                                </div>
                            </asp:Panel>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
                <%-- Navigation pane --%>
                <dx:SplitterPane MaxSize="700px" ShowCollapseBackwardButton="True" Separators-Size="10px"
                    PaneStyle-BackColor="Transparent" Collapsed="True" ShowCollapseForwardButton="True"
                    ScrollBars="Auto" AllowResize="True" MinSize="80px" AutoWidth="false" AutoHeight="false" Name="pnlInfo">
                    <Separators Size="10px">
                    </Separators>

                    <PaneStyle BackColor="Transparent"></PaneStyle>
                    <ContentCollection>
                        <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 100%; vertical-align: top;">
                                <tr>
                                    <td align="center" valign="top">
                                        <asp:UpdatePanel ID="pnl2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="center" style="width: 50%">
                                                            <dx:ASPxButton ID="btnMbyllur" runat="server" Text="-" Width="100%" AutoPostBack="false"
                                                                Height="25px" Font-Size="9" Font-Bold="true" ToolTip="Mos shfaq info">
                                                                <ClientSideEvents Click="function (s,e){RuajHapurMbyllurminus(false)}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td align="center" style="width: 50%">
                                                            <dx:ASPxButton ID="btnHapur" runat="server" Text="+" Width="100%" AutoPostBack="false"
                                                                Height="25px" Font-Size="9" ToolTip="Shfaq info">
                                                                <ClientSideEvents Click="function (s,e){RuajHapurMbyllurplus(true)}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <dxnb:ASPxNavBar ID="ASPxNavBar1" runat="server" ClientInstanceName="navbar" Width="100%"
                                                    EnableAnimation="True" SyncSelectionMode="CurrentPath"
                                                    EnableClientSideAPI="True" AllowSelectItem="True" Font-Size="8pt">
                                                    <ClientSideEvents HeaderClick="function (s,e) { HeaderClick (s,e); }" ItemClick="function(s, e) {}"
                                                        ExpandedChanging="function (s,e) {Expanded();  }" />
                                                    <GroupHeaderTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td style="width: 50%; font-weight: bold; height: 12px;">
                                                                    <dx:ASPxLabel Wrap="False" ID="Label1" runat="server" Font-Size="8" Text='<%# Eval("Text") %>' />
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <dx:ASPxHyperLink ID="HyperLink2" runat="server" Text='<%# Eval("Name") %>' NavigateUrl="javascript:void(0)"
                                                                        ImageWidth="12px" EnableClientSideAPI="true" ImageHeight="12px" ImageUrl="~/images/new/flash.png" ClientSideEvents-Click="function (s,e){ ButtonClickNavBar(s);}"
                                                                        ClientSideEvents-Init="function (s,e){ KontrolloTeDrejta(s);}" DisabledStyle-BackColor="#CCCCCC" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </GroupHeaderTemplate>
                                                    <Groups>
                                                        <dxnb:NavBarGroup Text="Info Klient/Furnitori" Expanded="false" Name="KF">
                                                            <ContentTemplate>
                                                                <dx:ASPxListBox ID="lbxKF" Height="100%" runat="server" Width="100%" ClientInstanceName="lbxKF"
                                                                    Font-Size="8">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn FieldName="Emri" Name="Emri" />
                                                                        <dx:ListBoxColumn FieldName="Vlera" Name="Vlera" />
                                                                    </Columns>
                                                                </dx:ASPxListBox>
                                                            </ContentTemplate>
                                                        </dxnb:NavBarGroup>
                                                        <dxnb:NavBarGroup Text="Info Artikulli" Expanded="false" Name="Artikulli">
                                                            <ContentTemplate>
                                                                <dx:ASPxListBox ID="lbxZgjedhur" Height="100%" runat="server" Width="100%" ClientInstanceName="lbxZgjedhur"
                                                                    Font-Size="8">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn FieldName="Emri" Name="Emri" />
                                                                        <dx:ListBoxColumn FieldName="Vlera" Name="Vlera" />
                                                                    </Columns>
                                                                </dx:ASPxListBox>
                                                            </ContentTemplate>
                                                        </dxnb:NavBarGroup>
                                                        <dxnb:NavBarGroup Text="Info Llogari" Expanded="false" Name="Llogari">
                                                            <ContentTemplate>
                                                                <dx:ASPxListBox ID="lbxLlogari" Height="100%" runat="server" Width="100%" ClientInstanceName="lbxLlogari"
                                                                    SettingsLoadingPanel-ImagePosition="Top" Font-Size="8">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn FieldName="Emri" Name="Emri" />
                                                                        <dx:ListBoxColumn FieldName="Vlera" Name="Vlera" />
                                                                    </Columns>
                                                                </dx:ASPxListBox>
                                                            </ContentTemplate>
                                                        </dxnb:NavBarGroup>
                                                    </Groups>
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
            <ClientSideEvents PaneCollapsed="function(s, e) { spliterPaneResized(s,e);}" 
                PaneExpanded="function(s, e) { spliterPaneResized(s,e);}" 
                PaneCollapsing="function(s, e) { spliterPaneCollapsing(s,e);}" 
                PaneExpanding="function(s, e) { spliterPaneExpanding(s,e);}" 
                PaneResized="function(s, e) { spliterPaneResized(s,e);}" />
            <Styles>
            </Styles>
            <Images>
            </Images>
        </dx:ASPxSplitter>

        <div id="divfund1" style="display: none;">
        </div>
        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <ContentTemplate>
                <div style="display: none">
                    <dx:ASPxButton ID="btnruaj" runat="server" Text="Ruaj" ClientInstanceName="btn" Width="0%"
                        ValidationGroup="entries" OnClick="btnruaj_Click">
                    </dx:ASPxButton>
                </div>
                <iframe id="Container55" runat="server" frameborder="0" height="0" name="Container55"
                    width="0"></iframe>
                <iframe id="Container1" runat="server" frameborder="0" height="0" name="Container1"
                    width="0"></iframe>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div>

            <asp:UpdatePanel ID="UpdatePanel9" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal" ShowCollapseButton="true"
                        CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID" ShowCloseButton="true"
                        AutoUpdatePosition="True" Font-Bold="False">
                        <ClientSideEvents CloseUp="function(s,e) { closePopup(s,e);}" />
                        <ContentStyle>
                            <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                                PaddingTop="1px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupSerialet" runat="server" AllowDragging="True" ClientInstanceName="popupSerialet" ShowCollapseButton="true"
                        CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID" ShowCloseButton="false"
                        AutoUpdatePosition="True" Font-Bold="False">
                        <ClientSideEvents CloseUp="function(s,e) { closePopup(s,e);}" />
                        <ContentStyle>
                            <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                                PaddingTop="1px" />
                        </ContentStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popUpNrAutomatikDrejtFundit" runat="server" AllowDragging="True" AllowResize="True"
                AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popUpNrAutomatikDrejtFundit" CloseAction="CloseButton"
                EnableAnimation="False" HeaderText="Kujdes! Numrat automatik drejt fundit..." Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter">

                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                        <dx:ASPxLabel runat="server" ClientInstanceName="lblNrAutoDrejtFundit" Text="Kujdes, po perfundojne numrat seriale/dokumentit!"
                            ForeColor="#595959" ClientIDMode="AutoID" ID="lblNrAutoDrejtFundit">
                        </dx:ASPxLabel>
                        <br />
                        <br />
                        <dx:ASPxButton ID="bntOKNrAuto" runat="server" CausesValidation="False" AutoPostBack="false" ClientInstanceName="bntOKNrAuto"
                            Text="Ok">
                            <ClientSideEvents Click="function(s, e) { popUpNrAutomatikDrejtFundit.Hide(); }" />
                        </dx:ASPxButton>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>
    </form>
</body>
</html>