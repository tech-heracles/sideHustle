<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FaqeKryesore.aspx.cs" Inherits="PlatinumWeb.FaqeKryesore" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxnb" %>



<%@ Register Src="TimeoutControl.ascx" TagName="TimeoutControl" TagPrefix="uc1" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link rel="icon" type="image/ico" href="/favicon.ico" />

  <meta name="mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-capable" content="yes"/>
    <meta name="apple-mobile-web-app-status-bar-style" content="black"/>
    <link rel="icon" sizes="256x256" href="/favicon.ico">
    <link rel="apple-touch-icon-precomposed" sizes="192x192" href="/favicon.ico">
 



    <link href="bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" />
    <link href="js/themes/black-tie/jquery-ui.css" runat="server" id="themeJQuery" rel="stylesheet" />
    <link href="js/css/ui.multiselect.css" rel="stylesheet" />
    <link href="DataTables-1.10.12/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
    <link href="css/animate.min.css" rel="stylesheet" />
    <link href="css/bootstrap-notify.css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <!-- DevExtreme themes -->
    <link rel="stylesheet" type="text/css" href="Content/dx.common.css" />
    <link rel="dx-theme" data-theme="generic.alphaweb" href="Content/dx.generic.alphaweb.css" data-active="true" />
    <link rel="dx-theme" data-theme="generic.alphaweb.compact" href="Content/dx.generic.alphaweb-compact.css" data-active="false" />
    
    <script type="text/javascript" src="DX.ashx?jsfileset=Scripts/jquery-3.4.1.min.js;Scripts/jquery.signalR-2.2.2.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/ui.multiselect.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/Utils-IMB.2.1.js;~/js/Menu_IMB.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myAbonim-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/customCombobox.js;~/DataTables-1.10.12/media/js/jquery.dataTables.min.js;~/DataTables-1.10.12/media/js/dataTables.bootstrap.min.js;~/js/jquery.blockUI.js;~/js/bootstrap-notify.js;~/js/menu.js;~/Scripts/jszip.min.js;~/Scripts/dx.all.js;~/js/localization/DevExtreme.Perkthime.js;~/js/components/Popup.js;~/js/TransferimSerialeUnike.js;~/js/aspx.js/FaqeKryesore.aspx-IMB.2.1.js&v76"> 
    </script>
    <script src="/signalr/hubs"></script>

    <link href="FaqeKryesore.css" rel="stylesheet" />
    <style>
        html {
            min-height: 100%; /* make sure it is at least as tall as the viewport */
            position: relative;
        }

        body {
            height: 100%; /* force the BODY element to match the height of the HTML element */
        }
        /* Kjo duhet se prishet nga boostrapi */
        input[type="search"]::-webkit-search-cancel-button {
            -webkit-appearance: searchfield-cancel-button;
        }

        /*#backDiv {
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            overflow: auto;
           
        }*/
        /*z-index:-1; /* Remove this line if it's not going to be a background! */

        .multiselect {
            /*width: 100% !important;*/
            height: 300px !important;
        }

        .ui-multiselect {
            margin: 0;
        }

        #btnHome a {
            width: 32px;
        }

        .kesh #btnHome a {
            width: 100%;
        }

        #faqja .style15 {
            font-weight: bold;
        }

        .kesh #faqja .style15 {
            font-weight: normal;
        }
		@media screen and (max-width: 900px) {
		  #backDiv{
			overflow: scroll;
		  }
		  #menu{
			overflow: scroll;
		  }
		  
		  .dxm-gutter{
			max-height: calc(100vh - 50px);
			overflow-y: scroll;
		  }
		}
        .einvoice-notice p{
            font-size: 17px;
        }
        .einvoice-notice img{
            height: 31px;
            margin-right: 5px;
        }
        .x-image{
            position: absolute;
            height: 30px;
            width: 30px;
            cursor: pointer;
            margin-top: 10px;
        }
        @media only screen and (max-width: 892px) {
          .einvoice-notice p{
              font-size: 10px;
          }
        }
    </style>
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-121798081-2"></script>
</head>

<body class="">
   
    <form id="form1" class="main" runat="server">  
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <div id="popup"></div>
        <div class="certificate-notice" id="certificate-notice" style="height: 20px; display:none; text-align: center;">
            <p></p>
        </div>
        <img src="images/FaqjaPare/x.png" onclick="hideNotice()" class="x-image"/>
        <div class="einvoice-notice" id="04" style="height: 60px;text-align: center;">
            <p><img src="images/FaqjaPare/bell.png"/>Bëj backup & restore të programit tënd Alpha. <a href="https://www.notion.so/alphawiki/Rivler-simi-i-magazin-s-ccd8291318d044389b0764668bc47003" target="_blank">Lexo më shumë..</a><br />
               <img src="images/FaqjaPare/bell.png"/>Bëj rivlerësimin në rivlersimi.alpha.al me quota falas dhe paguaj sipas perdorimit.<a href="https://www.notion.so/alphawiki/Backup-dhe-Restore-i-Alpha-04404a76f769475b884860625127972f" target="_blank">Lexo më shumë..</a>
            </p>
        </div>
        <div id="backDiv" runat="server">
            <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" Height="100%" runat="server" Orientation="Vertical" SeparatorVisible="false"
                ClientInstanceName="splitter" BackColor="Transparent" ClientIDMode="AutoID">
                <Panes>
                    <%-- Header pane--%>

                    <dx:SplitterPane Size="40px" Name="Top" MinSize="40px" MaxSize="40px" ShowCollapseBackwardButton="True"
                        PaneStyle-BackColor="Transparent" Separators-Size="2px">
                        <PaneStyle BackColor="Transparent"></PaneStyle>
                        <ContentCollection>
                            <dx:SplitterContentControl EnableViewState="false" CssClass="topSpliter" Height="32px" ID="SplitterContentControl1" runat="server">

                                <div id="top" style="vertical-align: top;">
                                    <div id="btnHome" class="logoHome">
                                        <%-- ImageUrl="images/home.png"--%>
                                        <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Height="32px"
                                            NavigateUrl="javascript:splitter.GetPaneByName('paneKryesor').SetContentUrl('Default.aspx?kontrollodefault=true');">
                                            <ClientSideEvents Click="Click_ASPxHyperLink1" />
                                        </dx:ASPxHyperLink>
                                    </div>
                                    <div id="faqja" class="faqja">
                                        <dx:ASPxLabel runat="server" ClientInstanceName="lblFaqja" CssClass="style15"
                                            Font-Size="Large" ForeColor="#ffffff" ClientIDMode="AutoID" ID="ASPxLabel1">
                                        </dx:ASPxLabel>
                                    </div>


                                    <div id="menu" class="menu">
                                        <dx:ASPxMenu ID="ASPxMenu1" Width="100%" Height="40px" runat="server" EnableClientSideAPI="true" ClientInstanceName="menu">
                                            <%--                                            <RootItemSubMenuOffset FirstItemX="-1" LastItemX="-1" X="-1" />
                                            <RootItemSubMenuOffset FirstItemX="-1" LastItemX="-1" X="-1" />--%>
                                            <ClientSideEvents ItemClick="function(s, e) {kontrolloTeDrejta(s,e,e.item.name);}" />
                                            <Items>
                                                <dx:MenuItem Text="Administrimi" Name="administrimi">
                                                    <Items>
                                                        <dx:MenuItem Text="Asistenti" Name="Asistenti.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Backup/Restore" Name="backup-restore" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Backup" Name="Backup.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Restore" Name="Restore.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Restore Database" Name="RestoreDatabase.aspx" Visible="true">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Dergo mesazh" Name="MessageToAll.html" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Motivet" Name="UserThemes.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Fjalekalimi" Name="fjalekalimi" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Politika Fjalekalimi" Name="PolitikaFjalekalimi.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Ndryshim Fjalëkalimi" Name="NdryshimFjalekalimi.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Historiku i emaileve" Name="HistorikuEmail.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Hyrjet/daljet në program" Name="AuditimUser.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Konfigurime Email" Name="KonfigurimeEmail.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Konfigurime Ftp" Name="KonfigurimeFtp.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Loge Sistemi" Name="LogeSistemi.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Mbyllje Periudhe" Name="MbylljePeriudhe.html" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Skema Pune" Name="Shto_SkemaWorkFlow.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Struktura e organizimit" Name="struktura-organizimit" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Grupim ndërmarrjesh" Name="LupaGrupNdermarrje.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Ndërmarrjet" Name="Shto_Ndermarrje.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Degët Administrative" Name="Shto_DegeAdministrative.aspx">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Departamentet" Name="StrukturaAdministrative.aspx">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Te drejtat" Name="te-drejtat" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Rolet" Name="ShtoModifiko_Grup_Perdoruesish.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Përdoruesit" Name="Shto_Perdorues.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Autorizimet" Name="Shto_Autorizimet.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Dashboard" Name="Dashboard.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        
                                                        <dx:MenuItem Text="Webhooks" Name="Webhooks.aspx" Visible="true">
                                                        </dx:MenuItem>
                                                    </Items>
                                                </dx:MenuItem>
                                                <dx:MenuItem Text="Konfigurime" Name="konfigurime">
                                                    <Items>
                                                        <dx:MenuItem Text="Amortizimi" Name="amortizimi" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Standarte Amortizimi" Name="StandarteAmortizimi.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Rregullat e Amortizimit" Name="Shto_RregullaAmortizimi.aspx"
                                                                    Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Çmimet" Name="cmimet" Image-Height="10px" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Nivel Çmimi" Name="Shto_NivelCmimi.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Përcaktim Çmimi" Name="percaktim-cmimi" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Text="Çmimet e shitjeve" Name="CmimeArtikulli.aspx?lloji=shitje" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                    <Items>
                                                                        <dx:MenuItem Text="Çmimet e blerjeve" Name="CmimeArtikulli.aspx?lloji=blerje" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                            </Items>
                                                            <Image Height="10px">
                                                            </Image>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Dokumenta" Name="" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Kategori dokumenti" Name="KonfigurimRegjistrimi.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Grupet e dokumentave" Name="GrupimDokumentash.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Eksport/import të dhënash" Name="import-eksport" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Konfigurim formati" Name="konfig-formati" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Text="Lista" Name="KonfigurimFormatImporti.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="E Re" Name="Shto_KonfigurimFormatImporti.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Eksport" Name="Eksport.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Import" Name="Import.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Import shitje Winline karta" Name="ImportWK.aspx?lloji=importwk" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Import shitje Tollona" Name="ImportWK.aspx?lloji=importtollona" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Import shitje Tollona Leter" Name="ImportWK.aspx?lloji=importtollonaleter" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Import shitje Tollona Elektronik" Name="ImportWK.aspx?lloji=importtollonaelektronik" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Import shitje Tollona Elektronik Specifik" Name="ImportWK.aspx?lloji=importtollonaelektronikspecifik" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Import Flete Kontabel" Name="ImportWK.aspx?lloji=importfk" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Transferim Dalje" Name="transferimDaljePopup">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Trasfero ne ISKSH" Name="TransferoNeISKSH.aspx" Visible ="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Elemente per integrim" Name="ElementePerIntegrim.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Instrumenta" Name="instrumenta" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Formati i numrave" Name="FormatNumrash.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Formate printimi" Name="FormatePrintimi.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Fushat Shtesë" Name="Shto_FushatShtese.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Gjenero Kodbar" Name="GjeneroKodbar.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Info" Name="Shto_ModelInfoArtikulli.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Konfigurim Kasash" Name="KonfigurimKasash.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Monedhat" Name="Shto_Monedhe.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Numrat automatikë" Name="Shto_NrAutom.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Qytete" Name="Qytetet.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Taksat" Name="Shto_NivelTvsh.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Vitet" Name="Shto_Vit.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Kategori Arkive" Name="KategoriArkive.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Posto ne EPaySlip" Name="PostoEPaySlip.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Pajisje" Name="Pajisje.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Kategori Shpenzimi" Name="Shto_KategoriShpenzimi.aspx" Visible="false">
                                                        </dx:MenuItem>

                                                        <dx:MenuItem Text="Konfigurim Dokumenti" Name="konfig-dokumenti" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Konfigurime Çelje" Name="konfig-celje" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Text="Lista" Name="KonfigDokumentash.aspx?idsuperkat=1" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="E Re" Name="KonfigurimDokumentash.aspx?idsuperkat=1&shtim_modifikim=shtim"
                                                                            Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Konfigurime Regjistrime" Name="konfig-regjistrime" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Text="Lista" Name="KonfigDokumentash.aspx?idsuperkat=2" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="E Re" Name="KonfigurimDokumentash.aspx?idsuperkat=2&shtim_modifikim=shtim"
                                                                            Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Konfigurime Lupa" Name="KonfigDokumentash.aspx?idsuperkat=3"
                                                                    Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Text="Lista" Name="KonfigDokumentash.aspx?idsuperkat=3" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="E Re" Name="KonfigurimDokumentash.aspx?idsuperkat=3&shtim_modifikim=shtim"
                                                                            Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Qendra Kostoje" Name="qk" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Llogari për shpërndarje në QK" Name="LlogariShperndarjeQK.aspx"
                                                                    Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Konfigurime për Qendrat e Kostos" Name="KonfigurimeQK.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Raporte financiare" Name="rap-financiare" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Format bilanci" Name="KonfigPASH.aspx?lloji=Bilanc" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Format P.A.SH" Name="KonfigPASH.aspx?lloji=Pash" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Format Cash Flow" Name="KonfigPASH.aspx?lloji=Cashflow" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Format Buxhetore" Name="KonfigPASH.aspx?lloji=Buxhetor" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Format OJF" Name="format-OJF" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Text="Format bilanci" Name="KonfigPASH.aspx?lloji=BilancOJF" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Format P.A.SH" Name="KonfigPASH.aspx?lloji=PashOJF" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Format Cash Flow" Name="KonfigPASH.aspx?lloji=CashflowOJF" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Skedulimi i punonjesve" Name="skedulimi-punonjesve" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Konfigurim liste orare" Name="KonfigurimListOrari.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Kalendari i festave" Name="KalendarFestash.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Legjenda e list orareve" Name="LegjendaListOrareve.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Profesionet dhe Pozicione" Name="ProfesioneTitujPune.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Vendndodhjet" Name="Vendndodhjet.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Grupimet Lokale dhe Globale" Name="GrupimeLocaleGlobale.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Kode Profesione" Name="KodeProfesione.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Strukturat e llogarive" Name="Shto_KPF.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Konfigurim Urdhër Pagesa" Name="konfig-urdher-pagese" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Grupe" Name="KonfigUrdherPagese.aspx?lloji=Grup" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Kod Programi" Name="KonfigUrdherPagese.aspx?lloji=Titull" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Kapituj" Name="KonfigUrdherPagese.aspx?lloji=Kapitull" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>

                                                        <dx:MenuItem Text="Zbritje analitike" Name="zbritje-analitike" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Nivel Zbritje" Name="Shto_NivelZbritje.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Përcaktim Zbritje" Name="ZbritjeAnalitike.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>

                                                        <dx:MenuItem Text="Konfigurime GIS" Name="konfig-gis" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Workspace" Name="GISWorkspace.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                    </Items>
                                                </dx:MenuItem>
                                                <dx:MenuItem Text="Çelje" Name="celje" ItemStyle-Paddings-PaddingLeft="8px">
                                                    <Items>
                                                        <dx:MenuItem Text="Agjentët e shitjes" Name="Shto_AgjentShitje.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Arka/Banka" Name="arka-banka" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Çelja e arkave" Name="Shto_Banka.aspx?ab=arka" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Çelja e bankave" Name="Shto_Banka.aspx?ab=banka" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Artikujt" Name="artikujt" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Artikujt" Name="Shto_Artikull.aspx?llojiart=afatshkurter" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Artikujt afatgjatë" Name="Shto_Artikull.aspx?llojiart=aqt" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Atribute të artikujve" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Text="Njësitë matëse" Name="NjesiArtikulli.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Grupet e artikujve" Name="KodifikimArtikulli.aspx?llojiart=afatshkurter" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Grupet e artikujve afatgjate" Name="KodifikimArtikulli.aspx?llojiart=aqt" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Detajime" Name="DetajimeArtikulli.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Seriale unike" Name="Shto_SerialeUnike.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Kategori seriali" Name="Shto_KategoriSeriali.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Formate seriali" Name="Shto_FormatSeriali.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Automjetet" Name="Shto_Automjete.aspx" Visible="false">
                                                        </dx:MenuItem>                               
                                                        <dx:MenuItem Text="Buxheti" Name="komponente-buxheti"  Visible="false" >
                                                            <Items>
                                                                <dx:MenuItem Text="Artikuj Buxhetimi" Name="B_KategoriBuxhetimi.aspx?lupe=false"  Visible="false"> </dx:MenuItem>
                                                                <dx:MenuItem Text="Komponente Kalkuluese" Name="B_KomponenteBuxheti.aspx"  Visible="false"> </dx:MenuItem>
                                                                <dx:MenuItem Text="Hedhja e të dhënave" Name="B_KomponenteBuxhetiVlere.aspx"  Visible="false"> </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Elemente Page" Name="elemnte-page" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Struktura page" Name="struktura-page" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Text="Kategori Page" Name="KategoriPage.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Shtesa Page" Name="ShtesaPage.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Sigurime Suplementare" Name="SigurimeSuplementare.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Komponente Listëpagese" Name="Shto_KomponentePage.aspx?lloji=true"
                                                                    Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Komponente Page" Name="Shto_KomponentePage.aspx?lloji=false"
                                                                    Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Sigurimet" Name="Shto_Sigurimet.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Tatime mbi pagën" Name="Shto_Tatime.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Elemente prodhimi" Name="" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Aktivitetet" Name="Shto_Aktivitete.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Burimet" Name="Shto_Burime.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Karta Klienti" Name="" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Karta Klienti" Name="Shto_KartaKlienti.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Politike Karta Klienti" Name="Shto_PolitikeKartaKlienti.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Shpernda Dhurata" Name="ShperndaDhurate.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Klient/Furnitor" Name="kf" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Furnitorët" Name="Shto_KlientFurnitor.aspx?kf=furnitor" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Klientët" Name="Shto_KlientFurnitor.aspx?kf=klient" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Atribute për klient/furnitor" Name="atribute-kf" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Text="Afatet e maturimit" Name="Shto_AfateMaturimi.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Kategoritë e zbritjeve" Name="Shto_KategoriZbritje.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Llojet e transportit" Name="MenyraTransporti.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Kusht  dërgimi" Name="KushteDergimi.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Kusht  pagese" Name="KushtePagese.aspx" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Grupet e klientëve" Name="GrupimeKlientFurnitor.aspx?kf=klient"
                                                                            Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Grupet e furnitorëve" Name="GrupimeKlientFurnitor.aspx?kf=furnitor"
                                                                            Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Llogaritë" Name="Shto_Llogari.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Lloj difekti" Name="LlojDifekti.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Makro" Name="Makro.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Njesi administrative" Name="njesi-administrative" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Pikat e shitjeve" Name="Shto_PikeShitjeFurnizimi.aspx?sf=shitje"
                                                                    Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Pikat e furnizimit" Name="Shto_PikeShitjeFurnizimi.aspx?sf=furnizim"
                                                                    Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Magazinat" Name="Shto_NjesiAdministrative.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Njësi vartëse" Name="Shto_NjesiVartese.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Njësi prodhimi" Name="Shto_NjesiProdhimi.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Punonjës" Name="Shto_Punonjes.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Komponente Qendra kosto" Name="komponente-qk" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Qendra Kosto" Name="Shto_QendraKosto.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Objektiva Kosto" Name="Shto_ObjektivaKosto.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Skema me Qendra Kosto" Name="Shto_SkemaQendraKosto.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Status riparimi" Name="StatusRiparimi.aspx" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Transportues/Operatore" Name="Shto_Transportues.aspx" Visible="false">
                                                        </dx:MenuItem>        
                                                    </Items>
                                                    <Image Url="~/images/spacer.gif" />
                                                    <Image Url="~/images/spacer.gif" />
                                                </dx:MenuItem>
                                                <dx:MenuItem Text="Regjistrime" Name="regjistrime">
                                                    <Items>
                                                        <dx:MenuItem Text="Amortizimi" Name="amortizimi" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Regjistrim Amortizimi" Name="regjistrim-amortizimi" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="RegjistrimAmortizimi.aspx" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_RegjistrimAmortizimi.aspx" Text="E Re" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Rivleresim Amortizimi" Name="rivleresim-amortizimi" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Text="Amortizimi fillestar" Name="amortizim-fillestar" Visible="false">
                                                                            <Items>
                                                                                <dx:MenuItem Name="RivleresimeAmortizimi.aspx?lloj=amortizim" Text="Lista" Visible="false">
                                                                                </dx:MenuItem>
                                                                                <dx:MenuItem Name="Shto_RivleresimeAmortizimi.aspx?lloj=amortizim&shtim_modifikim=shtim"
                                                                                    Text="E Re" Visible="false">
                                                                                </dx:MenuItem>
                                                                            </Items>
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Rivleresim" Name="rilveresim" Visible="false">
                                                                            <Items>
                                                                                <dx:MenuItem Name="RivleresimeAmortizimi.aspx?lloj=rivleresim" Text="Lista" Visible="false">
                                                                                </dx:MenuItem>
                                                                                <dx:MenuItem Name="Shto_RivleresimeAmortizimi.aspx?lloj=rivleresim&shtim_modifikim=shtim"
                                                                                    Text="E Re" Visible="false">
                                                                                </dx:MenuItem>
                                                                            </Items>
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Name="RillogaritjeAmortizimi.aspx" Text="Rillogaritje Amortizimi" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Aprovime" Name="aprovime" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Name="ListeAprovimi.aspx?status=aprovim" Text="Aprovimet" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Name="ListeAprovimi.aspx?status=kerkese" Text="Kerkese per aprovim"
                                                                    Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Faturat e blerjeve" Name="fatura-blerje" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text=" Lista" Name="RegjistrimDokumentash.aspx?shitje_blerje=blerje"
                                                                    Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text=" E Re" Name="Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&shtim_modifikim=shtim"
                                                                    Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text=" Fatura Blerje Einvoice" Name="FaturaBlerjeEinvoice.aspx"
                                                                    Visible="true">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>

                                                        <dx:MenuItem Text="Ekzekutim Buxheti" Name="EkzekutimBuxheti" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Planifikim Ekzekutim Buxheti" Name="PlanifikimEkzekutimBuxheti" Visible="false">
                                                                    <Items>
                                                                    <dx:MenuItem Name="B_RegjistrimBuxheti.aspx?lloji=planifikimEkzekutimi" Text="Lista" Visible="false"></dx:MenuItem>
                                                                    <dx:MenuItem Name="B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=planifikimEkzekutimi&shtim_modifikim=shtim" Text="E Re" Visible="false"></dx:MenuItem>
                                                                    </Items>
                                                                 </dx:MenuItem>
                                                                <dx:MenuItem Text="Ekzekutim Buxheti" Name="EkzekutimBuxheti" Visible="false">
                                                                    <Items>
                                                                    <dx:MenuItem Name="B_RegjistrimBuxheti.aspx?lloji=ekzekutim" Text="Lista" Visible="false"></dx:MenuItem>
                                                                    <dx:MenuItem Name="B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=ekzekutim&shtim_modifikim=shtim" Text="E Re" Visible="false"></dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        
                                                        <dx:MenuItem Name="flete-doganore" Text="Fletë doganore" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Import" Name="import" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="FleteDoganore.aspx?lloji=import" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_FleteDoganore.aspx?lloji=import&shtim_modifikim=shtim" Text="E Re"
                                                                            Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Export" Name="export" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="FleteDoganore.aspx?lloji=export" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_FleteDoganore.aspx?lloji=export&shtim_modifikim=shtim" Text="E Re"
                                                                            Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Fletë Kontabël" Name="fk" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Lista" Name="FleteKontabel.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="E Re" Name="Shto_FleteKontabel.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                                 <dx:MenuItem Text="Kontabilizim dokumenti" Name="KontabilizimDokumenti.aspx" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>                                                         
                                                        <dx:MenuItem Text="Lidhja e dokumentave" Name="lidhja-dokumentave" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Name="ListaLidhjaDokumentave.aspx" Text="Lista" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Name="LidhjaDokumentave.aspx" Text="E Re" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="List pagesa" Name="listpagesa" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Name="ListPagesa.aspx" Text="Lista" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Name="Shto_ListPagesa.aspx" Text="E Re" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Magazina" Name="magazina" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Regjistrimet e hyrjeve" Name="regjistrime-hyrje" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="RegjistrimMagazine.aspx?lloj=hyrje" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_RegjistrimMagazine.aspx?lloj=hyrje&shtim_modifikim=shtim"
                                                                            Text="E Re" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Regjistrimet e daljeve" Name="regjistrime-dalje" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="RegjistrimMagazine.aspx?lloj=dalje" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_RegjistrimMagazine.aspx?lloj=dalje&shtim_modifikim=shtim"
                                                                            Text="E Re" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Name="NdryshimCmimi.aspx" Text="Ndryshim Cmimi" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Inventarizim per artikujt afatshkurter" Name="inventarizim-artikulli" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="RegjistrimInventarizimi.aspx?lloj=ash" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_RegjistrimInventarizimi.aspx?lloj=ash&shtim_modifikim=shtim"
                                                                            Text="E Re" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Inventarizim per artikujt afatgjate" Name="inventarizim-aqt" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="RegjistrimInventarizimi.aspx?lloj=agj" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_RegjistrimInventarizimi.aspx?lloj=agj&shtim_modifikim=shtim"
                                                                            Text="E Re" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Ndryshim Sasi/Cmimi" Name="ndryshim-sasi-cmim" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="RegjistrimNdryshimCmimSasi.aspx" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_RegjistrimNdryshimCmimSasi.aspx"
                                                                            Text="E Re" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Name="RivleresimMagazine.aspx" Text="Rivlerësimi i inventarit" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>                                                        
                                                        <dx:MenuItem Text="Prodhimi" Name="prodhim" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Planifikimi i Prodhimit" Name="planifikim-prodhimi" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="Planifikimi.aspx" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_Planifikim.aspx" Text="E Re" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Skedulimi i Prodhimit" Name="skedulim-prodhimi" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="SkedulimProdhimi.aspx" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_SkedulimProdhimi.aspx" Text="E Re" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Ekzekutimi i Prodhimit" Name="ekzekutim-prodhimi" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="EkzekutimProdhimi.aspx" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_Ekzekutim.aspx" Text="E Re" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Name="GjeneroProjektProdhimi.aspx" Text="Gjenero Projektin e Prodhimit"
                                                                    Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Regjistrim Qendra Kosto" Name="regjistrim-qk" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Name="RegjistrimQendraKosto.aspx" Text="Lista" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Name="Shto_RegjistrimQendraKosto.aspx" Text="E Re" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Receta Optike" Name="receta-optike" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Name="RecetaOptike.aspx" Text="Lista" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Name="Shto_RecetaOptike.aspx" Text="E Re" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Regjistrim Riparimi" Name="regjistrim-riparimi" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Name="RegjistrimRiparimi.aspx" Text="Statusi i celulareve me probleme" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Name="Shto_RegjistrimRiparimi.aspx" Text="Riparimi i aparateve te prishura" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Rezervime" Name="rezervime" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Regjistrimet e hyrjeve" Name="regjistrime-hyrje" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="RegjistrimRezervimi.aspx?lloj=hyrje" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_RegjistrimRezervimi.aspx?lloj=hyrje&shtim_modifikim=shtim"
                                                                            Text="E Re" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Regjistrimet e daljeve" Name="regjistrime-dalje" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="RegjistrimRezervimi.aspx?lloj=dalje" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_RegjistrimRezervimi.aspx?lloj=dalje&shtim_modifikim=shtim"
                                                                            Text="E Re" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>

                                                        <dx:MenuItem Text="Sigurim Buxheti" Name="SigurimBuxheti" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Planifikim Buxheti" Name="PlanifikimBuxheti" Visible="false">
                                                                    <Items>
                                                                    <dx:MenuItem Name="B_RegjistrimBuxheti.aspx?lloji=planifikim" Text="Lista" Visible="false"></dx:MenuItem>
                                                                    <dx:MenuItem Name="B_Shto_RegjistrimBuxheti.aspx?planifikim_miratim=planifikim&shtim_modifikim=shtim" Text="E Re" Visible="false"></dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Miratim Buxheti" Name="MiratimBuxheti" Visible="false">
                                                                    <Items>
                                                                    <dx:MenuItem Name="B_RegjistrimBuxheti.aspx?lloji=miratim" Text="Lista" Visible="false"></dx:MenuItem>
                                                                    <dx:MenuItem Name="B_Shto_RegjistrimBuxheti.aspx?planifikim_miratim=miratim&shtim_modifikim=shtim" Text="E Re" Visible="false"></dx:MenuItem>
                                                                    </Items>
                                                                 </dx:MenuItem>
                                                                <dx:MenuItem Text="Alokim Buxheti" Name="AlokimBuxheti" Visible="false">
                                                                    <Items>
                                                                    <dx:MenuItem Name="B_RegjistrimBuxheti.aspx?lloji=alokim" Text="Lista" Visible="false"></dx:MenuItem>
                                                                    <dx:MenuItem Name="B_Shto_RegjistrimAlokimBuxheti.aspx" Text="E Re" Visible="false"></dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Rialokim Buxheti" Name="RialokimBuxheti" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="B_RegjistrimBuxheti.aspx?lloji=rialokim" Text="Lista" Visible="false"></dx:MenuItem>
                                                                        <dx:MenuItem Name="B_Shto_RegjistrimRialokimBuxheti.aspx" Text="E Re" Visible="false"></dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Perfitim Buxheti" Name="PerfitimBuxheti" Visible="false">
                                                                    <Items>
                                                                    <dx:MenuItem Name="B_RegjistrimBuxheti.aspx?lloji=perfitim" Text="Lista" Visible="false"></dx:MenuItem>
                                                                    <dx:MenuItem Name="B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=perfitim&shtim_modifikim=shtim" Text="E Re" Visible="false"></dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>

                                                        <dx:MenuItem Text="Faturat e shitjeve" Name="fatura-shitjesh" Visible="false">
                                                            <Items>
                                                                        <dx:MenuItem Text=" Lista" Name="RegjistrimDokumentash.aspx?shitje_blerje=shitje"  Visible="false">  </dx:MenuItem>
                                                                        <dx:MenuItem Text=" E Re" Name="Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&shtim_modifikim=shtim"  Visible="false">  </dx:MenuItem>
                                                                        <dx:MenuItem Text="Fatura Shitje Einvoice" Name="FaturaShitjeEinvoice.aspx"
                                                                        Visible="true">
                                                                        </dx:MenuItem>        
                                                                        <dx:MenuItem Text="Gjenero fature permbledhese" Name="GjeneroFaturePermbledhese.aspx"  Visible="false"> </dx:MenuItem>
                                                                        <dx:MenuItem Text="Ruajtja automatike e dokumentave" Name="GjenerimAutomatik.aspx" Visible="false">  </dx:MenuItem>
                                                                        <dx:MenuItem Text="Discounted device" Name="discounted-device" Visible="false">
                                                                                <Items>
                                                                                      <dx:MenuItem Text=" Lista" Name="RegjistrimDokumentash.aspx?shitje_blerje=shitjediscount" Visible="false">  </dx:MenuItem>
                                                                                      <dx:MenuItem Text=" E Re" Name="Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitjediscount&shtim_modifikim=shtim" Visible="false"> </dx:MenuItem>

                                                                                </Items>
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Bazaar" Name="bazar" Visible="false">
                                                                              <Items>
                                                                                     <dx:MenuItem Text=" Lista" Name="RegjistrimDokumentash.aspx?shitje_blerje=bazaar" Visible="false"> </dx:MenuItem>
                                                                                     <dx:MenuItem Text=" E Re" Name="Shto_RegjistrimDokumentash.aspx?shitje_blerje=bazaar&shtim_modifikim=shtim"  Visible="false"> </dx:MenuItem>

                                                                              </Items>
                                                                      </dx:MenuItem>
                                                                     <dx:MenuItem Text="Promocioni Plus" Name="Shto_AlphawebEnhancments.aspx" Visible="false"></dx:MenuItem>
                                                           </Items>
                                                        </dx:MenuItem>
                                                      
                                                    
                                                        <dx:MenuItem Text="Shpërndarja e shpenzimeve" Name="shperndarja-shpenzimeve" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Name="ShperndarjeShpenzimesh.aspx" Text="Lista" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Name="Shto_ShperndarjeShpenzimesh.aspx" Text="E Re" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Urdhër pagesa" Name="urdher-pagesa" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Name="UrdherPagesa.aspx" Text="Lista" Visible="false">
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Name="Shto_UrdherPagesa.aspx" Text="E Re" Visible="false">
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Veprime Arka/Banka" Name="veprime-arka-banka" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Veprime Arka" Name="veprime-arka" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Text="Arkëtimet" Name="arketimet" Visible="false">
                                                                            <Items>
                                                                                <dx:MenuItem Name="VeprimeBanka.aspx?lloji=arketim" Text="Lista" Visible="false">
                                                                                </dx:MenuItem>
                                                                                <dx:MenuItem Name="ShtoVeprimBanka.aspx?lloji=arketim&shtim_modifikim=shtim" Text="E Re"
                                                                                    Visible="false">
                                                                                </dx:MenuItem>
                                                                                <dx:MenuItem Name="ShtoVeprimBanka.aspx?lloji=arketimLlogariKlienti&shtim_modifikim=shtim" Text="Arketim per llogari klienti"
                                                                                    Visible="false">
                                                                                </dx:MenuItem>

                                                                                <dx:MenuItem Name="ShtoVeprimBanka.aspx?lloji=arketimAbonent&shtim_modifikim=shtim" Text="Arketim per abonent"
                                                                                    Visible="false">
                                                                                </dx:MenuItem>
                                                                            </Items>
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Pagesat" Name="pagesat" Visible="false">
                                                                            <Items>
                                                                                <dx:MenuItem Name="VeprimeBanka.aspx?lloji=pagese" Text="Lista" Visible="false">
                                                                                </dx:MenuItem>
                                                                                <dx:MenuItem Name="ShtoVeprimBanka.aspx?lloji=pagese&shtim_modifikim=shtim" Text="E Re"
                                                                                    Visible="false">
                                                                                </dx:MenuItem>
                                                                            </Items>
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="GjendjeArkeDitore.aspx" Text="Gjendje arke ditore" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Veprime Banka" Name="veprime-banka" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Text="Derdhjet bankare" Name="derdhjet-bankare" Visible="false">
                                                                            <Items>
                                                                                <dx:MenuItem Name="VeprimeBanka.aspx?lloji=derdhje" Text="Lista" Visible="false">
                                                                                </dx:MenuItem>
                                                                                <dx:MenuItem Name="ShtoVeprimBanka.aspx?lloji=derdhje&shtim_modifikim=shtim" Text="E Re"
                                                                                    Visible="false">
                                                                                </dx:MenuItem>
                                                                            </Items>
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Text="Tërheqjet bankare" Name="terheqjet-bankare" Visible="false">
                                                                            <Items>
                                                                                <dx:MenuItem Name="VeprimeBanka.aspx?lloji=terheqje" Text="Lista" Visible="false">
                                                                                </dx:MenuItem>
                                                                                <dx:MenuItem Name="ShtoVeprimBanka.aspx?lloji=terheqje&shtim_modifikim=shtim" Text="E Re"
                                                                                    Visible="false">
                                                                                </dx:MenuItem>
                                                                            </Items>
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Veprime me klient/furnitor" Name="veprime-kf" Visible="false">
                                                            <Items>
                                                                <dx:MenuItem Text="Veprime klient\furnitor" Name="veprime-kf" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="VeprimeKF.aspx" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_VeprimeKF.aspx" Text="Veprim i Ri" Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Azhornime klient\furnitor" Name="azhronim-kf" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="AzhornimKlientFurnitor.aspx?vep=azhornim" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_AzhornimKlientFurnitor.aspx?vep=azhornim" Text="Azhornim i Ri"
                                                                            Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                                <dx:MenuItem Text="Mbyllje klient\furnitor" Name="mbyllje-kf" Visible="false">
                                                                    <Items>
                                                                        <dx:MenuItem Name="AzhornimKlientFurnitor.aspx?vep=mbyllje" Text="Lista" Visible="false">
                                                                        </dx:MenuItem>
                                                                        <dx:MenuItem Name="Shto_AzhornimKlientFurnitor.aspx?vep=mbyllje" Text="Mbyllje e Re"
                                                                            Visible="false">
                                                                        </dx:MenuItem>
                                                                    </Items>
                                                                </dx:MenuItem>
                                                            </Items>
                                                        </dx:MenuItem>
                                                        
                                                      
                                                    </Items>                                                
                                                        
                                                </dx:MenuItem>
                                                <dx:MenuItem Text="Raportet" Name="raportet">
                                                    <Items>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=21" Text="Amortizimi" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=2" Text="Arka" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=6" Text="Banka" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=19" Text="Business Intelligence" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=13" Text="Blerjet" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text=" Fatura Blerje Einvoice" Name="FaturaBlerjeEinvoice.aspx"
                                                           Visible="true">
                                                                </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=17" Text="Burimet Njerëzore" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=57" Text="Buxheti" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=16" Text="Inventari" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=9" Text="Klientët dhe furnitorët" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Kontabiliteti" Name="Raportet.aspx?idmod=7" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=22" Text="Raporte menaxheriale" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=18" Text="Prodhimi" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=24" Text="Raportet CRM" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=20" Text="Qendrat e Kostos" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Raportet.aspx?idmod=12" Text="Shitjet" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Fatura Shitje Einvoice" Name="FaturaShitjeEinvoice.aspx" Visible="true">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="RaporteGrida.aspx?lloji=tollon" Text="Raport tollonash" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="RaporteGrida.aspx?lloji=kastrat" Text="Raport tollonash kastrati" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="RaporteGrida.aspx?lloji=GjendjaEMagazines" Text="Raport Gjendja e Magazines" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="RaporteGrida.aspx?lloji=GjendjaEArtikujveMeSeriale" Text="Raport Gjendja e Artikujve me Seriale" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="RaporteGrida.aspx?lloji=gjendjaArtikujveIMEI" Text="Gjendja e Artikujve me IMEI" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="RaporteGrida.aspx?lloji=gjendjaArtikujveIMEIEkspozitor" Text="Gjendja e artikujve me IMEI ne ekspozitor" Visible="false">
                                                        </dx:MenuItem>
                                                    </Items>
                                                </dx:MenuItem>
                                                <dx:MenuItem Text="Help" Name="help">
                                                    <Items>
                                                        <dx:MenuItem Text="Manuali Perdoruesit" Name="manuali" Visible="false" Target="_blank">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="ProgramKase" Text="Program Kase" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="ProgramKaseNew" Text="Program Kase (E Re) " Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="RemoteSupport" Text="Remote Support" Visible="false">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Name="Versioni" Text="Versioni" Visible="false" Target="_blank">
                                                        </dx:MenuItem>
                                                    </Items>
                                                </dx:MenuItem>
                                                <dx:MenuItem Text="" ItemStyle-Paddings-PaddingTop="6px" Name="ikonaImazhPerdorues"
                                                    ItemStyle-DropDownButtonStyle-Paddings-PaddingRight="5px" ItemStyle-Paddings-PaddingLeft="14px" ItemStyle-Width="64px">
                                                    <Items>
                                                        <dx:MenuItem Text="Perdorues" Name="LupaPersonalizoPerdorues.aspx">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Fjalekalimi" Name="NdryshimFjalekalimi.aspx">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Mesazhe" Name="mesazhe">
                                                        </dx:MenuItem>
                                                        <%--                                                        <dx:MenuItem Text="personalizo" Name="settings">
                                                        </dx:MenuItem>--%>
                                                        <dx:MenuItem Text="Abonimi im" Name="abonimi">
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Dalje" Name="dalje">
                                                        </dx:MenuItem>
                                                    </Items>
                                                </dx:MenuItem>
                                           
                                            </Items>
                                            <%--  <ItemStyle DropDownButtonSpacing="11px" ToolbarDropDownButtonSpacing="8px" ToolbarPopOutImageSpacing="8px" />
                                            <SubMenuStyle GutterWidth="0px" />--%>
                                        </dx:ASPxMenu>
                                    </div>

                                </div>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <%-- Middle pane--%>
                    <dx:SplitterPane ScrollBars="Auto">
                        <Separators Size="10px" Visible="true">
                        </Separators>
                        <Panes>
                            <%-- Navigation pane --%>
                            <dx:SplitterPane Size="170px" MinSize="150px" MaxSize="360px" ScrollBars="Auto" ShowCollapseBackwardButton="True"
                                Separators-Size="10px" PaneStyle-Paddings-Padding="0">
                                <PaneStyle>
                                </PaneStyle>
                                <Separators Size="10px">
                                </Separators>
                                <ContentCollection>
                                    <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                                        <dxnb:ASPxNavBar ID="ASPxNavBar1" runat="server" ClientInstanceName="navbar" Height="100%" Width="100%"
                                            AutoCollapse="True" EnableAnimation="True" SyncSelectionMode="CurrentPath"
                                            ClientSideEvents-ItemClick="function(s, e) {kontrolloTeDrejta(s,e,e.item.name);}"
                                            ClientSideEvents-HeaderClick="function (s,e) {grupKlick(s,e,e.group.name);}"
                                            EnableClientSideAPI="True" AllowSelectItem="true" Visible="true">
                                            <CollapseImage Height="0px" Width="0px">
                                            </CollapseImage>
                                            <ExpandImage Height="0px" Width="0px">
                                            </ExpandImage>
                                            <ClientSideEvents HeaderClick="function (s,e) {grupKlick(s,e,e.group.name);}"
                                                ItemClick="function(s, e) {kontrolloTeDrejta(s,e,e.item.name);}" />
                                            <Groups>
                                                <dxnb:NavBarGroup Text="Administrimi" Name="Administrimi" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Rolet" Name="ShtoModifiko_Grup_Perdoruesish.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Perdoruesit" Name="Shto_Perdorues.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Ndermarrjet" Name="Shto_Ndermarrje.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Kontabiliteti" Name="kontabiliteti" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Strukturat e llogarive" Name="Shto_KPF.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Llogaritë" Name="Shto_Llogari.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Ambjenti i kontabilizimit" Name="FleteKontabel.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Inventari" Name="inventari" Expanded="False">
                                                    <Items>

                                                        <dxnb:NavBarItem Text="Artikujt" Name="Shto_Artikull.aspx?llojiart=afatshkurter"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>

                                                        <dxnb:NavBarItem Name="RegjistrimMagazine.aspx?lloj=hyrje" Text="Dokumentat e hyrjeve"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Shto_RegjistrimMagazine.aspx?lloj=hyrje&shtim_modifikim=shtim"
                                                            Text="Regjistrimet e hyrjeve" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="RegjistrimMagazine.aspx?lloj=dalje" Text="Dokumentat e daljeve"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Shto_RegjistrimMagazine.aspx?lloj=dalje&shtim_modifikim=shtim"
                                                            Text="Regjistrimet e daljeve" Visible="false">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Blerjet dhe shitjet" Name="blerjeShitje" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Çmimet e Shitjeve" Name="CmimeArtikulli.aspx?lloji=shitje" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Çmimet e Blerjeve" Name="CmimeArtikulli.aspx?lloji=blerje" Visible="false">
                                                        </dxnb:NavBarItem>

                                                        <dxnb:NavBarItem Text="Klientët" Name="Shto_KlientFurnitor.aspx?kf=klient" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Furnitorët" Name="Shto_KlientFurnitor.aspx?kf=furnitor" Visible="false">
                                                        </dxnb:NavBarItem>

                                                        <dxnb:NavBarItem Name="RegjistrimDokumentash.aspx?shitje_blerje=blerje" Text="Faturat e blerjeve"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&shtim_modifikim=shtim"
                                                            Text="Regjistrimet e blerjeve" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="RegjistrimDokumentash.aspx?shitje_blerje=shitje" Text="Faturat e shitjeve"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&shtim_modifikim=shtim"
                                                            Text="Regjistrimet e shitjeve" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="RegjistrimDokumentash.aspx?shitje_blerje=shitjediscount" Text="Discounted device"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitjediscount&shtim_modifikim=shtim"
                                                            Text="Regjistrimet e discounted devices" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dx:NavBarItem Name="RegjistrimDokumentash.aspx?shitje_blerje=bazaar" Text="Shitje bazaar"
                                                            Visible="false">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Name="Shto_RegjistrimDokumentash.aspx?shitje_blerje=bazaar&shtim_modifikim=shtim"
                                                            Text="Regjistrimet e shitjeve bazaar" Visible="false">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Name="FaturaBlerjeEinvoice.aspx?"
                                                            Text="Fatura Blerje Einvoice" Visible="true">
                                                        </dx:NavBarItem>

                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Arka dhe banka" Name="arkaBanka" Expanded="False">
                                                    <Items>

                                                        <dxnb:NavBarItem Name="VeprimeBanka.aspx?lloji=derdhje" Text="Derdhjet bankare" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ShtoVeprimBanka.aspx?lloji=derdhje&shtim_modifikim=shtim"
                                                            Text="Regjistrimi i derdhjeve" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="VeprimeBanka.aspx?lloji=terheqje" Text="Tërheqjet bankare"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ShtoVeprimBanka.aspx?lloji=terheqje&shtim_modifikim=shtim"
                                                            Text="Regjistrimi i tërheqjeve" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="VeprimeBanka.aspx?lloji=arketim" Text="Arkëtimet" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ShtoVeprimBanka.aspx?lloji=arketim&shtim_modifikim=shtim"
                                                            Text="Regjistrimi i arkëtimeve" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="VeprimeBanka.aspx?lloji=pagese" Text="Pagesat" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ShtoVeprimBanka.aspx?lloji=pagese&shtim_modifikim=shtim" Text="Regjistrimi i pagesave"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                         <dxnb:NavBarItem Name="GjendjeArkeDitore.aspx" Text="Gjendje arke ditore"  Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dx:NavBarItem Name="ShtoVeprimBanka.aspx?lloji=arketimLlogariKlienti&shtim_modifikim=shtim"
                                                            Text="Regjistrimi i arkëtimeve per llogari klienti" Visible="false">
                                                        </dx:NavBarItem>

                                                        <dx:NavBarItem Name="ShtoVeprimBanka.aspx?lloji=arketimAbonent&shtim_modifikim=shtim"
                                                            Text="Regjistrimi i arkëtimeve per abonent" Visible="false">
                                                        </dx:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Burimet Njerëzore" Name="hr" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Departamentet" Name="StrukturaAdministrative.aspx"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>

                                                        <dxnb:NavBarItem Text="Komponente listëpagese" Name="Shto_KomponentePage.aspx?lloji=true"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>

                                                        <dxnb:NavBarItem Text="Punonjës" Name="Shto_Punonjes.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="List pagesa" Name="ListPagesa.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Urdher Pagesa" Name="pagesa" Expanded="false">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Grupe" Name="KonfigUrdherPagese.aspx?lloji=Grup" />
                                                        <dxnb:NavBarItem Text="Titull" Name="KonfigUrdherPagese.aspx?lloji=Titull" />
                                                        <dxnb:NavBarItem Text="Kapitull" Name="KonfigUrdherPagese.aspx?lloji=Kapitull" />
                                                        <dxnb:NavBarItem Text="Urdher pagesat" Name="Shto_UrdherPagesa.aspx" />
                                                        <dxnb:NavBarItem Text="Regjistrimi i urdher pagesave" Name="UrdherPagesa.aspx" />
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Prodhimi" Name="prodhimi" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Planifikimi i Prodhimit" Name="Planifikimi.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Planifikimi i ri" Name="Shto_Planifikim.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Ekzekutimi i Prodhimit" Name="EkzekutimProdhimi.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Ekzekutimi i ri" Name="Shto_Ekzekutim.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Gjenero Projektin e Prodhimit" Name="GjeneroProjektProdhimi.aspx"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Qendra Kosto" Name="qk" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Konfigurime për Qendrat e Kostos" Name="KonfigurimeQK.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Qendra Kosto" Name="Shto_QendraKosto.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Skema me Qendra Kosto" Name="Shto_SkemaQendraKosto.aspx" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <%--  <dxnb:NavBarItem Text="Objektiva Kosto" Name="Shto_ObjektivaKosto.aspx" Visible="false">
                                                                    </dxnb:NavBarItem>--%>
                                                        <dxnb:NavBarItem Text="Regjistrim Qendra Kosto" Name="RegjistrimQendraKosto.aspx"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Regjistrim Qendra Kosto e re" Name="Shto_RegjistrimQendraKosto.aspx"

                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Amortizimi" Name="amortizimi" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Artikujt afatgjatë " Name="Shto_Artikull.aspx?llojiart=aqt"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Amortizimi fillestar" Name="RivleresimeAmortizimi.aspx?lloj=amortizim"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Amortizimi fillestar i ri" Name="Shto_RivleresimeAmortizimi.aspx?lloj=amortizim&shtim_modifikim=shtim"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Regjistrim Amortizimi" Name="RegjistrimAmortizimi.aspx"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Regjistrim Amortizimi i ri" Name="Shto_RegjistrimAmortizimi.aspx"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <%--   <dxnb:NavBarItem Text="Rivleresim amortizimi" Name="RivleresimeAmortizimi.aspx?lloj=rivleresim"
                                                                        Visible="false">
                                                                    </dxnb:NavBarItem>
                                                                    <dxnb:NavBarItem Text="Rivleresim amortizimi i ri" Name="Shto_RivleresimeAmortizimi.aspx?lloj=rivleresim&shtim_modifikim=shtim"
                                                                        Visible="false">
                                                                    </dxnb:NavBarItem>
                                                                    <dxnb:NavBarItem Text="Rillogaritje amortizimi" Name="RillogaritjeAmortizimi.aspx"
                                                                        Visible="false">
                                                                    </dxnb:NavBarItem>--%>
                                                    </Items>
                                                </dxnb:NavBarGroup>

                                                <dxnb:NavBarGroup Text="Aprovime dokumentash" Name="aprovime-dokumentash" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Aprovimet" Name="ListeAprovimi.aspx?status=aprovim" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Text="Kerkese per aprovim" Name="ListeAprovimi.aspx?status=kerkese"
                                                            Visible="false">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>

                                                <dx:NavBarGroup Text="Promocioni Plus" Name="Promocioni_Plus" Expanded="False">
                                                    <Items>
                                                        <dx:NavBarItem Text="Promocioni Plus" Name="Shto_AlphawebEnhancments.aspx" Visible="false">
                                                        </dx:NavBarItem>
                                                    </Items>
                                                </dx:NavBarGroup>

                                                <dxnb:NavBarGroup Text="Raportet" Name="rap" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Kontabiliteti" Name="Raportet.aspx?idmod=7" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=13" Text="Blerjet" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="FaturaBlerjeEinvoice.aspx?" Text="FaturaBlerjeEinvoice" Visible="true">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=12" Text="Shitjet" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=16" Text="Inventar" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=9" Text="Klientët dhe furnitorët" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=2" Text="Arka" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=6" Text="Banka" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=17" Text="Burimet Njerëzore" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=18" Text="Prodhimi" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=20" Text="Qendrat e Kostos" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=21" Text="Amortizimi" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=19" Text="Business Intelligence" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="RaporteGrida.aspx?lloji=tollon" Text="Raport tollonash" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="RaporteGrida.aspx?lloji=kastrat" Text="Raport tollonash kastrati" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="RaporteGrida.aspx?lloji=GjendjaEMagazines" Text="Raport Gjendja e Magazines" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="RaporteGrida.aspx?lloji=GjendjaEArtikujveMeSeriale" Text="Raport Gjendja e Artikujve me Seriale" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="RaporteGrida.aspx?lloji=gjendjaArtikujveIMEI" Text="Gjendja e Artikujve me IMEI" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="RaporteGrida.aspx?lloji=gjendjaArtikujveIMEIEkspozitor" Text="Gjendja e artikujve me IMEI ne ekspozitor" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=22" Text="Raporte menaxheriale" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=24" Text="Raportet CRM" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=57" Text="Buxheti" Visible="false">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Business Intelligence" Name="bi" Expanded="false">
                                                    <Items>
                                                        <dxnb:NavBarItem Name="Raport_PivotGrid.aspx?idModuli=12" Text="Shitje" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raport_PivotGrid.aspx?idModuli=16" Text="Magazina" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raport_PivotGrid.aspx?idModuli=13" Text="Blerje" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="Raport_PivotGrid.aspx?idModuli=7" Text="Kontabiliteti" Visible="false"></dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Harta" Name="map" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Name="GoogleHarte.aspx?lloji=mag" Text="Hartat e Njesive Administrative" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="GoogleHarte.aspx?lloji=magshitje" Text="Hartat e shitjeve sipas magazinave" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="GoogleHarte.aspx?lloji=klientshitje" Text="Harta e shitjeve sipas klienteve" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="GoogleHarte.aspx?lloji=funritorblerje" Text="Harta e blerjeve sipas furnitoreve" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="GoogleHarte.aspx?lloji=marzhishitje" Text="Marzhi i shitjes sipas klienteve" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="GoogleHarte.aspx?lloji=pikashitje" Text="Harta e shitjeve sipas pikave te shitjes" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="GoogleHarte.aspx?lloji=maggjendje" Text="Harta e gjendjes se magazinave" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="GoogleHarte.aspx?lloji=amortizimShqiptar" Text="Harta e amortizimit te aseteve ne perqindje" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="GoogleHarte.aspx?lloji=klientKoordinata" Text="Harta e klienteve" Visible="false">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="CRM" Name="crm" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Alpha CRM" Name="CRMDefault.aspx" Visible="false" Target="_blank">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="CRMRouteAgjenti.aspx" Text="Route i Agjenteve" Target="_blank" Visible="false" />
                                                        <dxnb:NavBarItem Name="CRMFushaAnkete.aspx" Text="Fusha Ankete" Target="_blank" Visible="false" />

                                                        <dxnb:NavBarItem Name="CRMAnketa.aspx" Text="Konfiguro Ankete" Target="_blank" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="CRMListaAnketa.aspx" Text="Lista E Anketave" Target="_blank" Visible="false" />
                                                        <dxnb:NavBarItem Name="CRMLidhAnkete.aspx" Text="Percakto Veprimtarine" Target="_blank" Visible="false" />

                                                        <dxnb:NavBarItem Name="CRMHistoriku.aspx" Text="Historiku" Target="_blank" Visible="false" />
                                                        <dxnb:NavBarItem Name="CRMDetyra.aspx" Text="Detyrat" Target="_blank" Visible="false" />

                                                        <dxnb:NavBarItem Name="Raportet.aspx?idmod=24" Target="_blank" Text="Raportet CRM" Visible="false">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="GIS" Name="gis" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Alpha GIS" Name="GISDefault.aspx" Visible="false" Target="_blank">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Analize Buxheti" Name="analizeBuxheti" Expanded="False">
                                                    <Items>

                                                        <dxnb:NavBarItem Name="ABKonfiguroFusha.aspx" Text="Konfigurimi i zerave per ambjentet e analizes se buxhetit" Visible="false"></dxnb:NavBarItem>
                                                        <%-- <dxnb:NavBarItem Name="ABParashikimShpenzimeKonfig.aspx" Text="Konfigurimi i parashikimit te shpenzimeve " Visible="false"></dxnb:NavBarItem> 
                                                                  <dxnb:NavBarItem Name="ABKonfiguroShpenzimeOperative.aspx"  Text="Konfigurimi i shpenzimeve operative" Visible="false" ></dxnb:NavBarItem>--%><%-- perfshire tek ABKonfiguroFusha--%>
                                                        <dxnb:NavBarItem Name="ABBuxhetPermbledhes.aspx" Text="Regjistrimi i buxhetit permbledhes" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABParashikimShpenzPersoneli.aspx" Text="Regjistrimi i parashikimit te shpenzimeve per personelin" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABParashikimTeArdhura.aspx" Text="Regjistrimi i parashikimit te te ardhurave" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABShpenzimeKapitale.aspx" Text="Regjistrimi i shpenzimeve kapitale" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABPBuxheti3Vjecar.aspx" Text="Regjistrimi i projektbuxhetit per tre vite" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABPlanifikimiIProdukteve.aspx" Text="Regjistrimi i planifikimit te produkteve" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABShpenzimeOperative.aspx" Text="Regjistrimi i shpenzimeve operative" Visible="false"></dxnb:NavBarItem>
                                                        <%--<dxnb:NavBarItem Name="ABInventariPerdorues.aspx" Text="Regjistrimi i inventarit sipas perdoruesve" Visible="false"></dxnb:NavBarItem>
                                                                  <dxnb:NavBarItem Name="ABInventariVite.aspx" Text="Regjistrimi i inventarit sipas viteve" Visible="false"></dxnb:NavBarItem> --%>
                                                        <dxnb:NavBarItem Name="ABPasqyraOrganike.aspx" Text="Regjistrimi i pasqyres organike" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABEvidencaStatistikore.aspx" Text="Regjistrimi i evidences statistikore" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABPlanifikimRealizim.aspx" Text="Planifikim dhe realizim" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABProkurimePublike.aspx" Text="Regjistrim i realizimit te prokurimeve publike" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABProkurimePublikeParashikim.aspx" Text="Regjistrim i realizimit te prokurimeve publike" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABRaporti.aspx?raporti=BuxhetPermbledhes" Text="Raporti per projekt buxhetin permbledhes" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABRaporti.aspx?raporti=ParashikimiTeArdhura" Text="Raporti per parashikimin e te ardhurave" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABRaporti.aspx?raporti=ParashikimShpenzPersoneli" Text="Raporti per parashikimin e shpenzimeve per personelin" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABRaporti.aspx?raporti=ShpenzimeOperative" Text="Raporti per projekt buxhetin ne zerin e shpenzimeve operative ne vitet pasardhes" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABRaporti.aspx?raporti=ShpenzimeOperative3Vjecar" Text="Raporti per projekt buxhetin ne zerin e shpenzimeve operative ne 3 vitet pasardhese" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABRaporti.aspx?raporti=ShpenzimeKapitale" Text="Raporti per parashikimin e shpenzimeve kapitale" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABRaporti.aspx?raporti=PBuxheti3Vjecar" Text="Raporti per projekt buxhetin 3 vjecar" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABRaporti.aspx?raporti=ShpenzimeOperativeMujore" Text="Raporti per shpenzimet operative ne baze mujore" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABRaporti.aspx?raporti=PlanifikimiIProdukteve" Text="Raporti per planifikimin e produkteve te programit" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABRaporti.aspx?raporti=ParashikimShpenzimeshRaportuese" Text="Raporti per parashikimin e shpenzimeve per vitin pasardhes(Raportuese)" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABRaporti.aspx?raporti=ShpenzimeOperativePermbledhese" Text="Raporti permbledhes per shpenzimet operative" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABPivotGrid.aspx?raporti=EvidencaStatistikore" Text="Raporti i evidences statistikore(Raportuese)" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABPivotGrid.aspx?raporti=InventariSipasViteve" Text="Raporti i inventarit sipas viteve (Raportuese)" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABPivotGrid.aspx?raporti=InventariSipasPerdoruesve" Text="Raporti i inventarit sipas perdoruesve(Raportuese)" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABRaporti.aspx?raporti=RegjistriIRealizimitTeProkurimevePublike" Text="Regjistri i realizimit te prokurimeve publike" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ABPivotGrid.aspx?raporti=PlanifikimRealizim" Text="Tabela permbledhese e planifikimeve dhe realizimeve" Visible="false"></dxnb:NavBarItem>

                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Buxheti" Name="buxheti" Expanded="False" Visible="true">
                                                    <Items>
                                                        <dxnb:NavBarItem Name="B_KategoriBuxhetimi.aspx?lupe=false" Text="Artikuj Buxhetimi" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="B_KomponenteBuxheti.aspx" Text="Komponente Kalkuluese" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="B_KomponenteBuxhetiVlere.aspx" Text="Hedhja e të dhënave" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="B_RegjistrimBuxheti.aspx?lloji=planifikim" Text="Planifikim Buxheti" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="B_RegjistrimBuxheti.aspx?lloji=miratim" Text="Miratim Buxheti" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="B_RegjistrimBuxheti.aspx?lloji=alokim" Text="Alokim Buxheti" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="B_RegjistrimBuxheti.aspx?lloji=rialokim" Text="Rialokim Buxheti" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="B_RegjistrimBuxheti.aspx?lloji=perfitim" Text="Perfitim Buxheti" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="B_RegjistrimBuxheti.aspx?lloji=planifikimEkzekutimi" Text="Planifikim Ekzekutim Buxheti" Visible="false"></dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="B_RegjistrimBuxheti.aspx?lloji=ekzekutim" Text="Ekzekutim Buxheti" Visible="false"></dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Dashboard" Name="dashboard" Expanded="False" Visible="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Dashboard" Name="Dashboard.aspx" Visible="false" Target="_blank">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Help" Name="help" Expanded="False">
                                                    <Items>
                                                        <dxnb:NavBarItem Text="Manuali Perdoruesit" Name="manuali" Visible="false" Target="_blank">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="RemoteSupport" Text="Remote Support" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ProgramKase" Text="Program Kase" Visible="false">
                                                        </dxnb:NavBarItem>
                                                        <dxnb:NavBarItem Name="ProgramKaseNew" Text="Program Kase (E Re)" Visible="false">
                                                        </dxnb:NavBarItem>
                                                    </Items>
                                                    <ItemStyle CssClass="navItem"></ItemStyle>
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Mobile" Name="Mobile" Expanded="False" Visible="False">
                                                </dxnb:NavBarGroup>
                                                <dxnb:NavBarGroup Text="Personalizo" Name="settings" Expanded="False" Visible="true">
                                                </dxnb:NavBarGroup>
                                            </Groups>
                                            <LoadingPanelImage>
                                            </LoadingPanelImage>
                                        </dxnb:ASPxNavBar>
                                        <div id="newMenu" class="menu" style="display: none"></div>
                                    </dx:SplitterContentControl>
                                </ContentCollection>
                            </dx:SplitterPane>
                            <%-- Main pane (iOS problem pane) --%>
                            <dx:SplitterPane Name="paneKryesor" ContentUrl="javascript:false" ContentUrlIFrameName="frameKryesor"
                                ScrollBars="Auto" PaneStyle-BackColor="White" AutoWidth="true">
                                <PaneStyle BackColor="White">
                                </PaneStyle>
                                <ContentCollection>
                                    <dx:SplitterContentControl ID="SplitterContentControl3" Height="100%" runat="server">
                                        <div id='diviKryesor'>
                                        </div>
                                    </dx:SplitterContentControl>
                                </ContentCollection>
                            </dx:SplitterPane>

                        </Panes>
                    </dx:SplitterPane>
                    <%-- Footer pane --%>
                    <dx:SplitterPane Size="30px" ScrollBars="None" AutoWidth="true" MinSize="30px" MaxSize="30px" ShowCollapseForwardButton="True" PaneStyle-Paddings-Padding="0"
                        Name="Footer" ContentUrl="FooterPanelInfo.aspx" PaneStyle-BackColor="#d5d5d5" PaneStyle-CssClass="footer1"
                        ContentUrlIFrameName="frameFooter">
                    </dx:SplitterPane>
                </Panes>
                <ClientSideEvents Init="function(s,e){pageRefresh(s,e);}"></ClientSideEvents>
            </dx:ASPxSplitter>

            <dx:ASPxHiddenField ID="hfPeriudhKontabel" runat="server" ClientInstanceName="hfPeriudhKontabel">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState">
            </dx:ASPxHiddenField>
            <asp:HiddenField ID="hfPeriudha" runat="server" />
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Zgjidh periudhen kontabel"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                Width="600px" Height="600px" ClientIDMode="AutoID" CssPostfix="Glass" Style="background-color: #EDF3F4">
                <HeaderStyle>
                    <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                </HeaderStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                        <iframe id="Container" clientidmode="Static" scrolling="no" frameborder="0" runat="server"
                            width="600" height="600" style="background-color: #EDF3F4"></iframe>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupSettings" runat="server" AllowDragging="True" ClientInstanceName="popupSettings"
                CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Zgjidh elementet e menuve"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                Width="600px" Height="600px" ClientIDMode="AutoID" CssPostfix="Glass" Style="background-color: #EDF3F4">
                <HeaderStyle>
                    <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                </HeaderStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                        <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel4" runat="server" ClientIDMode="AutoID" Width="680px">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent4" runat="server" SupportsDisabledAttribute="True">
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupRemoteSupport" runat="server" AllowDragging="True"
                ClientIDMode="AutoID" ClientInstanceName="popupRemoteSupport" CloseAction="CloseButton"
                CssPostfix="Glass" EnableAnimation="False" EnableViewState="False" Font-Bold="true"
                HeaderText="Kerko Support" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" Width="680px">
                <HeaderStyle>
                    <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                </HeaderStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="680px">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel runat="server" Text="Merrni tani online kursin për programin Alpha:"> </dx:ASPxLabel> <a href='http://www.udemy.com/course/kurs-per-programin-alpha-certifikohu-online' target="_blank">Kliko</a> <br> 
                                        <dx:ASPxLabel runat="server" Text="Për të kërkuar support na shkruani në adresën e email:"> </dx:ASPxLabel> <a href='mailto:ndihma@imb.al'>ndihma@imb.al</a> <dx:ASPxLabel runat="server" Text="ose klikoni"> </dx:ASPxLabel> <a href='https://helpdesk.alpha.al' target="_blank">këtu.</a>
                                       <dx:ASPxLabel ForeColor="#595959" ClientIDMode="AutoID" ID="lblMsgbox">
                                            </dx:ASPxLabel>
                                    <br />
                                    <br />
                                    <div>
                                        <table style="margin: 0 auto;">
                                            <tr>
                                                <td colspan="2">
                                                    </dx:ASPxLabel>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
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
            <dx:ASPxButton ID="btnDownloadProgramKase" runat="server" ClientSideEvents-Click="function(s,e){btnDownloadProgramKaseClick(s,e);}" CausesValidation="False" ClientInstanceName="btnDownloadProgramKase" ClientVisible="false"
                Text="Ok" OnClick="btnDownloadProgramKase_Click">
            </dx:ASPxButton>
            <uc1:TimeoutControl ID="SessionTimeout" runat="server" />
            <dx:ASPxHiddenField ID="hfUrlHelp" runat="server"></dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfUsername" runat="server"></dx:ASPxHiddenField>
        </div>

        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popUpDiteTeMbeturaLicenca" runat="server" AllowDragging="True" ClientIDMode="AutoID"
            ClientInstanceName="popUpDiteTeMbeturaLicenca" CloseAction="CloseButton" CssPostfix="Glass" EnableAnimation="False" HeaderStyle-HorizontalAlign="Center"
            EnableViewState="False" Font-Bold="true" HeaderText="Skadim liçence!" HeaderStyle-Font-Size="Medium" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" Width="300px">
            <HeaderStyle>
                <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
            </HeaderStyle>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel2" runat="server" ClientIDMode="AutoID" Width="271px">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">

                                <div style="text-align: center;">

                                    <dx:ASPxLabel Width="100%" runat="server" ID="lblDiteTeMbeturaTeLicenca" ClientIDMode="AutoID"
                                        ClientInstanceName="lblDiteTeMbeturaTeLicenca" Font-Bold="True"
                                        Text="" Font-Size="Small" Wrap="True">
                                    </dx:ASPxLabel>
                                </div>
                                <br />
                                <div style="text-align: center">
                                    <dx:ASPxButton ID="ASPxButton1" Width="70px" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                        Text="Ok" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {popUpDiteTeMbeturaLicenca.Hide();}" />
                                    </dx:ASPxButton>

                                </div>
                                <div style="visibility: hidden">
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popUpAzhornim" ContentStyle-HorizontalAlign="Center" runat="server" AllowDragging="True" ClientIDMode="AutoID" ShowOnPageLoad="false"
            ClientInstanceName="popUpAzhornim" CloseAction="CloseButton" CssPostfix="Glass" EnableAnimation="False" HeaderStyle-HorizontalAlign="Center"
            EnableViewState="False" Font-Bold="true" HeaderText="Azhurnim versioni!" HeaderStyle-Font-Size="Medium" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" Width="300px">
            <HeaderStyle>
                <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
            </HeaderStyle>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel3" runat="server" ClientIDMode="AutoID" Width="500px">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">

                                <div style="text-align: center;">

                                    <dx:ASPxLabel Width="100%" runat="server" ID="lblMesazhiPer" ClientIDMode="AutoID"
                                        ClientInstanceName="lblMesazhiPer" Font-Bold="True"
                                        Text="" Font-Size="Small" Wrap="True">
                                    </dx:ASPxLabel>
                                </div>
                                <br />
                                <div style="text-align: center">
                                    <dx:ASPxButton ID="ASPxButton2" Width="70px" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                        Text="Ok" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {popUpAzhornim.Hide();}" />
                                    </dx:ASPxButton>

                                </div>
                                <div style="visibility: hidden">
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
         <!-- Place this tag where you want the Live Helper Plugin to render. -->
        <div id="lhc_status_container_page" ></div>

        
    </form>

    
    <script type="text/javascript">      

        //Popup Window
        window.showPopup = function (template, title) {
            new Popup().init("#popup", template, { width: "40%", height: "30%", title: title});
        };
  
        // The hub
        pageState.signalR = { hubStart: null, imbChatConn: null };
        pageState.signalR.isActiv = (hfState.Get("activateSignalR") == 'true') ? true : false;

        window.startHub = function () {            
            if (pageState.signalR.isActiv && pageState.signalR.hubStart == null)
                pageState.signalR.hubStart = $.connection.hub.start();
            return pageState.signalR.hubStart;
        };
        window.doneHub = function (from, template, title) {
            if (pageState.signalR.isActiv) {
                switch (from) {
                    default:
                        window.startHub().done(function () {
                            window.imbChatConn().server.send(template, title);
                        });
                }
            }
        };

        // SignalR
        window.imbChatConn = function () {
            if (pageState.signalR.isActiv && pageState.signalR.imbChatConn == null)
                pageState.signalR.imbChatConn = $.connection.messageToAllHub;
            return pageState.signalR.imbChatConn;
        };

        window.imbChatConn().client.broadcastMessage = function (template, title) {
            window.showPopup(template, title);
        };

        window.startHub();
    </script>

</body>
</html>
