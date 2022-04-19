<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Raportet.aspx.cs" Inherits="PlatinumWeb.Raportet" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title>Alpha Web</title>
    
<%--    <link href="bootstrap-3.3.6-dist/css/bootstrap.min.css" rel="stylesheet" />--%>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="js/css/le-frog/jquery-ui.css" media="screen" rel="stylesheet" type="text/css"
        runat="server" id="themeJQuery" />
		<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <style type="text/css">
        /* Kjo duhet se prishet nga boostrapi */
        input[type="search"]::-webkit-search-cancel-button {
            -webkit-appearance: searchfield-cancel-button;
        }
        @font-face {
            font-family: OpenSans;
            src: url(css/fonts/OpenSans-Regular.ttf);
        }

        @font-face {
            font-family: OpenSansLight;
            src: url(css/fonts/OpenSans-Light.ttf);
        }

        @font-face {
            font-family: OpenSansBold;
            src: url(css/fonts/OpenSans-Bold.ttf);
        }

        @font-face {
            font-family: OpenSansLightItalic;
            src: url(css/fonts/OpenSans-LightItalic.ttf);
        }
         @font-face {
            font-family: Arial Narrow;
            src: url(css/fonts/arial-narrow.ttf);
        }

        .MyLinks {
            text-decoration: none;
            color: Gray;
            font-family: Tahoma;
            font-size: 9pt;
        }

        a.MyLinks:visited {
            text-decoration: none;
            color: Gray;
        }

        a.MyLinks:hover {
            text-decoration: none;
            color: #FF9900;
        }

        a.MyLinks:active {
            text-decoration: none;
            color: #FF9900;
        }

        .auto-style1 {
            width: 21%;
        }

        .box:hover {
            /*background-color: #f0f0f0;*/
            border: 1px solid #0072c6;
            cursor: pointer;
            background-color: #0072c6;
            color: #FFFFFF;
        }

        .box {
            /*display: inline-table;*/
            height: inherit;
            width: inherit;
            /*background-color: #f7f7f7;*/
            border: 1px solid #ddd; /*#e7e7e7;*/
            text-align: center;
            /*padding: 30px 30px 0 30px;
            margin: 10px 0;*/
            position: relative;
            color: #959595;
            text-decoration: none;
        }

            .box:hover a {
                display: inline;
                color: #FFFFFF;
            }

            .box a {
                text-decoration: none;
                text-align: center;
                color: #0072c6;
            }

        .boxHeader {
            display: inline-block;
            padding-top: 5px;
            position: relative;
            vertical-align: bottom;
            font-size: 12px !important;
        }

            .boxHeader label {
                font-size: 12px !important;
            }
      
        .text {
            font-size: 14px;
            font-family: OpenSans;
            text-decoration: none;
            padding: 5px 0;
        }

            .text:hover {
                color: #858585;
            }

        .titull {
            display: block;
            width: auto;
            padding-top: 5px;
        }

        .textTitull {
            font-size: 33px;
            border-bottom: 1px solid #eeeeee;
            padding-bottom: 0px;
            display: inline-block;
            font-family: OpenSansLight;
            Color: #0072c6;
            font-weight: bold;
            margin-right: 10px;
            display: none;
        }

        .kerkoRaport {
            display: inline-block;
        }
  

        .reportFiltraHeader {
            display: inline-block;
            position: relative;
            width: 100%;
        }

        .sortable-placeholder {
            background-color: #0072c6;
        }

        #ruajKonfig {
            font-size: 10px !important;
            margin: 5px 5px 18px 5px;
        }

        .imb-sortable {
            list-style-type: none;
            margin: 0;
            padding: 0;
        }

            .imb-sortable li {
                margin: 0 10px 10px 0;
                padding: 1px;
                display: inline-table;
                width: 138px;
                height: 116px;
            }
    </style>

    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/imbList.js;~/js/aspx.js/Raportet.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script> 
</head>
<body class="bootstrap-iso">
    <form id="form1" runat="server">
        
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ if (window.parent) window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="ASPxHiddenField1" runat="server"></dx:ASPxHiddenField>
        <div id="div2" style="width: 100%; display: inline-block; position: relative;">
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server"></dx:ASPxHiddenField>
            <div class="reportFiltraHeader">
                <div class="boxHeader" data-role="none" style="float: right;">
                    <dx:ASPxButton ID="btnHelp" runat="server" Text="" ClientInstanceName="btnHelp" Width="40"
                        Height="24" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                        ImagePosition="Bottom" HorizontalAlign="Center" ToolTip="Ndihme" OnInit="btnHelp_Init">
                        <Image Url="images/new/help_14.png" UrlHottracked="images/new/help_14_W.png">
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnCollapseAll" runat="server" Text="" ClientInstanceName="btnCollapseAll"
                        Width="40" Height="24" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                        Font-Bold="True">
                        <Image Url="images/new/reg_screen.png" UrlHottracked="images/new/reg_screen_W.png">
                        </Image>
                        <ClientSideEvents Click="function(s,e){collapseAll(s,e);}" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnExpandAll" runat="server" Text="" ClientInstanceName="btnExpandAll"
                        Width="40" Height="24" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                        Font-Bold="True">
                        <Image Url="images/new/full_screen.png" UrlHottracked="images/new/full_screen_W.png">
                        </Image>
                        <ClientSideEvents Click="function(s,e){expandAll(s,e);}" />
                    </dx:ASPxButton>
                </div>
                 <div class="titull">
            </div>
           </div>
               
                <div id="menuHomePage" runat="server">
                </div>
        </div>
        <dx:ASPxLabel ID="lblerror" runat="server" Visible="false" Text="ASPxLabel">
        </dx:ASPxLabel>
    </form>
</body>
</html>
