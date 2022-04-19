<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMDefault.aspx.cs" Inherits="PlatinumWeb.CRMDefault" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha CRM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/images/CRM/faviconCRM.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/images/CRM/faviconCRM.ico" type="image/ico" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <%--<script type="text/javascript" src="~/js/srcCRM/js/jquery.mmenu.min.all.js"></script>--%>
    <link type="text/css" rel="stylesheet" href="~/js/srcCRM/css/jquery.mmenu.all.css" />
    <link type="text/css" rel="stylesheet" href="AlphaCRM.css" />
    <%--<link rel="stylesheet" media="screen (min-width: 768px)" href="AlphaCRM.css" />--%>
    <link href="css/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/srcCRM/js/jquery.mmenu.min.all.js;~/js/myNrAuto-IMB.2.1.js&v49"
        type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () { 
            Utils.PushToGoogleAnalytics(hfState.Get("googleAnalytics"), hfState.Get("googleAnalyticsTrackingId"))
            $('nav#menu').mmenu({ classes: "mm-light" });
            var idNdermarrje = hfState.Get("idNdermarrje");
            var idPerdoruesi = hfState.Get("idPerdoruesi");
            var idVitNdermarrje = hfState.Get("idVitNdermarrje");
            myFaqeCelje.krijoMenuPerCRM(idPerdoruesi, idNdermarrje, idVitNdermarrje, true);
        });
    </script>
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-121798081-2"></script>
</head>
<body>
    <div id="page">
        <div class="header">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 1%;">
                        <a href="#menu"></a>
                    </td>
                    <td style="width: 94%; vertical-align: top;">Alpha CRM</td>
                    <td style="width: 5%;">
                        <div id="emriLogout" class="emriLogout">
                            <div id="userInfo">
                                <div id="emri">
                                    <dx:ASPxLabel ID="lblUserEmri" ClientInstanceName="lblUserEmri" runat="server" Text=""
                                        Font-Size="14" ForeColor="White" Font-Names="Arial">
                                    </dx:ASPxLabel>
                                </div>
                                <div id="logout">
                                    <a style="position: relative; color: white; background-image: none;" class="fa fa-sign-out fa-2x" ><i class="fa fa-sign-out  fa-lg"></i>&nbsp;</a>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="content" class="content">
            <div data-role="content" class="my-home-page" data-theme="a">
                <ul id="myHomePage" data-theme="a" data-role="listview" class="ui-listview"></ul>
            </div>
            <form runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
                </asp:ScriptManager>
                    <dx:ASPxHiddenField ID="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
            </form>
        </div>
        <nav id="menu" data-role="panel" data-position-fixed="false">
            <ul id="ulMenu" data-role="panel">
            </ul>
        </nav>
    </div>
</body>
</html>