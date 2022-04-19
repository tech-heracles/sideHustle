<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PlatinumWeb._Default" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Alpha Web</title>
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" />
    <link href="js/themes/black-tie/jquery-ui.css" runat="server" id="themeJQuery" rel="stylesheet" />
    <link href="js/css/ui.multiselect.css" rel="stylesheet" />
    <link href="DataTables-1.10.12/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="css/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/animate.min.css" rel="stylesheet" />
    <link href="css/bootstrap-notify.css" rel="stylesheet" />
    <style type="text/css">
        .MyLinks
        {
            text-decoration: none;
            color: Gray;
            font-family: Tahoma;
            font-size: 9pt;
        }

        a.MyLinks:visited
        {
            text-decoration: none;
            color: Gray;
        }

        a.MyLinks:hover
        {
            text-decoration: none;
            color: #FF9900;
        }
		.kesh a.MyLinks:hover
        {
            text-decoration: none;
            color: #1478b4;
        }

        a.MyLinks:active
        {
            text-decoration: none;
            color: #FF9900;
        }
		
		.kesh a.MyLinks:active
        {
            text-decoration: none;
            color: #1478b4;
        }

        .divLink
        {
            border-bottom: 1px solid #D9DED8;
            padding-top: 4px;
            padding-bottom: 3px;
        }

        .divBorder
        {
            border-top: 1px solid #FBFFFC;
        }

        .style1
        {
            width: 244px;
        }

        .style2
        {
            width: 213px;
        }

        .style3
        {
            width: 80px;
        }
        .auto-style1
        {
            width: 7px;
        }
        .auto-style2
        {
            width: 160px;
        }
        .auto-style3
        {
            width: 36px;
        }
        .auto-style4
        {
            width: 266px;
        }
        .auto-style5
        {
            width: 58px;
        }
        .auto-style6
        {
            width: 469px;
        }
		
		span#lblEmerNderm, span#lblEmerNIpti, span#lblEmerViti, span#lblEmerQyteti, span#lblNrTel, span#lblEmerWebPage{
			color: #999999; 
			font-size: 10pt; 
			font-weight: bold;
		}
		.kesh span#lblEmerNderm, .kesh span#lblEmerNIpti, .kesh span#lblEmerViti, .kesh span#lblEmerQyteti, .kesh span#lblNrTel, .kesh span#lblEmerWebPage{		
			color: #CD379B;		
			font-size: 10pt;		
			font-weight: bold;		
		}	
			
		a#hlAdministrimi, a#hlKonfigurimet , a#hlInventare , a#hlShitjeBlerje , a#hlArkaBanka , a#hlKontabilitet, a#hlRaporte {		
			color: #FF9900;		
			font-size: Medium;		
			font-weight: normal;		
			text-decoration: none;		
		}
		
		.kesh a#hlAdministrimi, .kesh a#hlKonfigurimet , .kesh a#hlInventare , .kesh a#hlShitjeBlerje , .kesh a#hlArkaBanka , .kesh a#hlKontabilitet, .kesh a#hlRaporte {		
			color: #1478B4;		
			font-size: Medium;		
			font-weight: bold;		
			text-decoration: none;		
		}
		
		img#imgAdministrimi, img#imgKonfigurimet, img#imgInventare, img#imgShitjeBlerje, img#imgKontabilitet, img#imgRaporte{
			height: 24px;
			width: 24px;
			vertical-align: middle;
			background-image: url(/images/t7.gif);
		}
		img#imgArkaBanka{
			height: 24px;
			width: 24px;
			vertical-align: middle;
			margin-top: 0px;
			background-image: url(/images/t7.gif);
		}
		
		.kesh img#imgAdministrimi, .kesh img#imgKonfigurimet, .kesh img#imgInventare, .kesh img#imgShitjeBlerje, .kesh img#imgArkaBanka, .kesh img#imgKontabilitet, .kesh img#imgRaporte{
			height: 20px;
			width: 20px;
			vertical-align: middle;
			background-image: url(/images/t7.png);
		}
		
		.kesh img#imgArkaBanka{
			height: 20px;
			width: 20px;
			vertical-align: middle;
			margin-top: 0px;
			background-image: url(/images/t7.png);
		}
		
		.post-bgtop .post-bgbtm table.table1{
			border: 1px solid #999999;  
			width: 100%; 
			height:85px;
		}
		
		.kesh .post-bgtop .post-bgbtm table.table1{
			border: 1px solid #1478B4;  
			width: 100%; 
			height:85px;
		}
		
		.post-bgtop .post-bgbtm table.table2{
		border-style: none solid solid solid; 
		border-width: 1px;
		border-color: #999999; 
		background-color: #FCFCFC; 
		width:100%;
		}		
		
		.mzhu .post-bgtop .post-bgbtm table.table2{
		display:none;
		}

		.kesh .post-bgtop .post-bgbtm table.table2{
		border-style: none solid solid solid; 
		border-width: 1px;
		border-color: #1478b4; 
		background-color: #FCFCFC; 
		width:100%;
		}
		
    </style>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myCookies-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/Utils-IMB.2.1.js;~/js/Menu_IMB.js;~/js/aspx.js/Default.aspx-IMB.2.1.js&v76;" type="text/javascript"></script>

</head>
<body class="">

    <form id="form1" runat="server" class="style27">
<%--    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <div class="post" style="height: 537px">
        <div class="post-bgtop">
            <div class="post-bgbtm">
                <table class="table1">
                    <tr>
                        <td class="auto-style1">
                            <br />
                        </td>
                        <td class="style11" colspan="8">
                        </td>
                        <td class="style12">
                        </td>
                    </tr>
                    <tr style="height:30px;">
                        <td class="auto-style1">
                        </td>
                        <td class="style2">
                            <dx:ASPxLabel ID="lblNdermarje" runat="server" Text="Miresevini ne Ndermarrjen:"
                         
                                ForeColor="#999999" Font-Size="10pt" Font-Bold="True">
                            </dx:ASPxLabel>
                        </td>
                        <td class="auto-style6">
                            <dx:ASPxLabel ID="lblEmerNderm" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                        <%--<td class="style18">
                        </td>--%>
                        <td class="auto-style5">
                            <dx:ASPxLabel ID="lblNIPT" runat="server" Text="NIPT:" 
                                ForeColor="#999999" Font-Size="10pt" Font-Bold="True">
                            </dx:ASPxLabel>
                        </td>
                        <td class="auto-style4">
                            <dx:ASPxLabel ID="lblEmerNIpti" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                        <td class="auto-style3">
                        </td>
                        <td class="auto-style2">
                            <dx:ASPxLabel ID="lblViti" runat="server" Text="Viti:" 
                               ForeColor="#999999" Font-Size="10pt" Font-Bold="True">
                            </dx:ASPxLabel>
                        </td>
                        <td class="style4">
                            <dx:ASPxLabel ID="lblEmerViti" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr style="height:30px">
                        <td class="auto-style1">
                            &nbsp;
                        </td>
                        <td class="style2">
                            <dx:ASPxLabel ID="lblQyteti" runat="server" Text="Qyteti/Vendi:" 
                               ForeColor="#999999" Font-Size="10pt" Font-Bold="True">
                            </dx:ASPxLabel>
                        </td>
                        <td class="auto-style6">
                            <dx:ASPxLabel ID="lblEmerQyteti" width="100%" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                       <%-- <td class="style18">
                            &nbsp;
                        </td>--%>
                        <td class="auto-style5">
                            <dx:ASPxLabel ID="lblTel" runat="server" Text="Tel:" 
                             ForeColor="#999999" Font-Size="10pt" Font-Bold="True">
                            </dx:ASPxLabel>
                        </td>
                        <td class="auto-style4">
                            <dx:ASPxLabel ID="lblNrTel" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                        <td class="auto-style3">
                        </td>
                        <td class="auto-style2">
                            <dx:ASPxLabel ID="lblWebPage" runat="server" Text="Web Page:" 
                             ForeColor="#999999" Font-Size="10pt" Font-Bold="True">
                            </dx:ASPxLabel>
                        </td>
                        <td class="style4">
                            <dx:ASPxLabel ID="lblEmerWebPage" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            &nbsp;
                        </td>
                        <td class="style1" colspan="8">
                            <br />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table class="table2">
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                            &nbsp;
                        </td>
                        <td class="style8">
                            &nbsp;
                        </td>
                        <td class="style11">
                            &nbsp;
                        </td>
                        <td class="style13">
                            &nbsp;
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                            &nbsp;
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                            <dx:ASPxImage ID="imgAdministrimi" runat="server">
                            </dx:ASPxImage>
                        </td>
                        <td class="style8">
                            <dx:ASPxHyperLink ID="hlAdministrimi" runat="server" Text="Si te fillosh punen ne program?"
                                ClientIDMode="AutoID"  
                                NavigateUrl="Default.aspx" Font-Underline="False">
                            </dx:ASPxHyperLink>
                        </td>
                        <td class="style11">
                            <dx:ASPxImage ID="imgKonfigurimet" runat="server">
                            </dx:ASPxImage>
                        </td>
                        <td class="style13">
                            <dx:ASPxHyperLink ID="hlKonfigurimet" runat="server" Text="Konfigurime" ClientIDMode="AutoID"
                            
                                NavigateUrl="Default.aspx" Font-Underline="False">
                            </dx:ASPxHyperLink>
                        </td>
                        <td class="style12">
                            <dx:ASPxImage ID="imgInventare" runat="server">
                            </dx:ASPxImage>
                        </td>
                        <td class="style25">
                            <dx:ASPxHyperLink ID="hlInventare" runat="server" Text="Inventari" ClientIDMode="AutoID"
                           
                                NavigateUrl="Default.aspx" Font-Underline="False">
                            </dx:ASPxHyperLink>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style8">
                            <dx:ASPxLabel ID="lblAdministrimi" runat="server" Text="Do të njiheni me celjet fillestare që kryhen në program përpara se të fillohen konfigurimet.">
                            </dx:ASPxLabel>
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                            <dx:ASPxLabel ID="lblKonfigurimi" runat="server" Text="Do të njiheni me konfigurimet kryesore të të dhënave si psh çelje llogarish, artikujsh, çmimet etj.">
                            </dx:ASPxLabel>
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                            <dx:ASPxLabel ID="lblInventare" runat="server" Text="Do të njiheni me regjistrimet që kanë të bëjnë me lëvizjet e magazinës dhe mbajtjen e kostos.">
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style8">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlCeljaNdermarje" runat="server" Text="Ndërmarrjet" ClientIDMode="AutoID"
                                    CssClass="MyLinks" NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_Ndermarrje.aspx')"
                                     rende>
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlKPF" runat="server" Text="Strukturat e llogarive" ClientIDMode="AutoID"
                                    CssClass="MyLinks" NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_KPF.aspx')"
                                    >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlMagazina" runat="server" Text="Magazinat" ClientIDMode="AutoID"
                                    CssClass="MyLinks" NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_NjesiAdministrative.aspx')"
                                    >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style8">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlCeljaRole" runat="server" Text="Rolet" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('ShtoModifiko_Grup_Perdoruesish.aspx')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hfLlogari" runat="server" Text="Llogaritë" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_Llogari.aspx')" CssClass="MyLinks"
                                    >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlArtikuj" runat="server" Text="Artikujt" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_Artikull.aspx?llojiart=afatshkurter')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style8">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlCeljaPerdoruesve" runat="server" Text="Përdoruesit" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_Perdorues.aspx')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlTaksa" runat="server" Text="Taksat" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_NivelTvsh.aspx')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlAQT" runat="server" Text="Artikujt afatgjatë" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_Artikull.aspx?llojiart=aqt')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style8">
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlVitet" runat="server" Text="Vitet" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_Vit.aspx')" CssClass="MyLinks"
                                    >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlRivleresim" runat="server" Text="Rivlerësimi i inventarit"
                                    ClientIDMode="AutoID" NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('RivleresimMagazine.aspx')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style8">
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlRegjMag" runat="server" Text="Dokumentet e hyrjeve" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('RegjistrimMagazine.aspx?lloj=hyrje')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style22" colspan="5">
                            &nbsp;
                        </td>
                        <td class="style10" colspan="2">
                            <br />
                            <br />
                        </td>
                        <td class="style10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxImage ID="imgShitjeBlerje" runat="server">
                            </dx:ASPxImage>
                        </td>
                        <td class="style8">
                            <dx:ASPxHyperLink ID="hlShitjeBlerje" runat="server" Text="Blerjet dhe Shitjet"
                                ClientIDMode="AutoID" NavigateUrl="Default.aspx" 
                                Font-Underline="False">
                            </dx:ASPxHyperLink>
                        </td>
                        <td class="style19">
                            <dx:ASPxImage ID="imgArkaBanka" runat="server" >
                            </dx:ASPxImage>
                        </td>
                        <td class="style13">
                            <dx:ASPxHyperLink ID="hlArkaBanka" runat="server" Text="Arka dhe Banka" ClientIDMode="AutoID"
                                NavigateUrl="Default.aspx" Font-Underline="False">
                            </dx:ASPxHyperLink>
                        </td>
                        <td>
                            <dx:ASPxImage ID="imgKontabilitet" runat="server">
                            </dx:ASPxImage>
                        </td>
                        <td class="style25" >
                            <dx:ASPxHyperLink ID="hlKontabilitet" runat="server" Text="Kontabilitet" ClientIDMode="AutoID"
                                NavigateUrl="Default.aspx" Font-Underline="False">
                            </dx:ASPxHyperLink>
                        </td>
                        <td class="style17" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style13" style="width: 33%">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Do të njiheni me ambjentet e konfigurimit të klientëve e furnitorëve si edhe me rregjistrimet e shitjeve dhe blerjeve.">
                            </dx:ASPxLabel>
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13" style="width: 33%">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Do të njiheni me konfigurimet e njësive të arkave e bankave si edhe me rregjistrimet e arkëtimeve dhe pagesave.">
                            </dx:ASPxLabel>
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25" style="width: 30%">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Do të njiheni me kontabilizimin e regjistrimeve të kryera në program.">
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style8">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlKlient" runat="server" Text="Klientët" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_KlientFurnitor.aspx?kf=klient')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                            <dx:ASPxLabel ID="lblBlerjeShitje" runat="server" Text=" ">
                            </dx:ASPxLabel>
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlArka" runat="server" Text="Çelja e arkave" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_Banka.aspx?ab=arka')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlAmbjentKont" runat="server" Text="Ambjenti I kontabilizimit"
                                    ClientIDMode="AutoID" NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('FleteKontabel.aspx')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style8">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlFurnitor" runat="server" Text="Furnitorët" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_KlientFurnitor.aspx?kf=furnitor')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlBanka" runat="server" Text="Çelja e bankave" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('Shto_Banka.aspx?ab=banka')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                            <div>
                                <dx:ASPxLabel ID="lblKontabilitet" runat="server" Text=" ">
                                </dx:ASPxLabel>
                            </div>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style8">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlShitje" runat="server" Text="Faturat e shitjeve" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('RegjistrimDokumentash.aspx?shitje_blerje=shitje')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlRegjArka" runat="server" Text="Arkëtimet"
                                    ClientIDMode="AutoID" NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('VeprimeBanka.aspx?lloji=arketim')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style12">
                            <dx:ASPxImage ID="imgRaporte" runat="server">
                            </dx:ASPxImage>
                        </td>
                        <td class="style25">
                            <dx:ASPxHyperLink ID="hlRaporte" runat="server" Text="Raportet" ClientIDMode="AutoID"
                             
                                NavigateUrl="Default.aspx" Font-Underline="False">
                            </dx:ASPxHyperLink>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style8">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlBlerje" runat="server" Text="Faturat e blerjeve" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('RegjistrimDokumentash.aspx?shitje_blerje=blerje')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlRegjBanka" runat="server" Text="Pagesat"
                                    ClientIDMode="AutoID" NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('VeprimeBanka.aspx?lloji=pagese')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                            <%--<div class="divLink">
                                <dx:ASPxHyperLink ID="hlRaportimi" runat="server" Text="Raportimi" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('RaportetAll.aspx')" CssClass="MyLinks"
                                    >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>--%>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style8">
                            &nbsp;
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlLidhja" runat="server" Text="Lidhja e dokumentave" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('ListaLidhjaDokumentave.aspx')"
                                    CssClass="MyLinks" >
                                </dx:ASPxHyperLink>
                            </div>
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Do të njiheni me informacione të detajuara për çdo funksion të ndërmarrjes.">
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                        </td>
                        <td class="style8">
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                            <div>
                                <dx:ASPxLabel ID="lblArkaBanka" runat="server" Text=" ">
                                </dx:ASPxLabel>
                            </div>
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style24">
                            &nbsp;
                        </td>
                        <td class="style10">
                            &nbsp;
                        </td>
                        <td class="style8">
                            &nbsp;
                        </td>
                        <td class="style11">
                        </td>
                        <td class="style13">
                            &nbsp;
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td class="style25">
                            <div class="divLink">
                                <dx:ASPxHyperLink ID="hlRaportimi" runat="server" Text="Raportimi" ClientIDMode="AutoID"
                                    NavigateUrl="javascript:myFaqeCelje.kontrolloTeDrejta('RaportetAllNew.aspx')" CssClass="MyLinks"
                                    >
                                </dx:ASPxHyperLink>
                            </div>
                            <div class="divBorder">
                            </div>
                            <div>
                                <dx:ASPxLabel ID="lblRaportet" runat="server" Text=" ">
                                </dx:ASPxLabel>
                            </div>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table style="width:100%;">
                    <tr>
                        <td>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:80%;">
                        </td>
                        <td style="float:left">
                            <%--  <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Copyright © IMB
Instituti i Modelimeve ne Biznes 
www.imb.al" Font-Italic="True" ForeColor="Green" Font-Bold="True">
                            </dx:ASPxLabel>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
