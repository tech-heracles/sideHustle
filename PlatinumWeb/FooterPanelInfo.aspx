<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FooterPanelInfo.aspx.cs"
    Inherits="PlatinumWeb.FooterPanelInfo" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    <style type="text/css">
        body {
            padding: 3px 12px 2px 12px;
            margin: 0;
        }

        table, td, tr {
            padding: 0;
            margin: 0;
            border-spacing: 0;
        }
        
        .style2 {
            width: 5%;
            height: 28px;
        }

        .style3 {
            width: 8%;
            height: 28px;
        }

        .style4 {
            width: 57%;
            height: 28px;
        }

        .kesh .style4 {
            width: 77%;
            height: 28px;
			background:url('images/emri_kesh_white.png') no-repeat;
			
        }

        .style5 {
            width: 40%;
            height: 28px;
        }
		
		.kesh .style5 {
            width: 30%;
            height: 28px;
        }
		
		body.kesh{
			background-color: #1478B4;
		}
		
		body form div.style1{
			background-color: transparent;
		}
		
		div table td #lblNdermarrje, div table td #lblEmriNdermarrje, div table td #lblPeriudha{
			color: black;
		}
		
		.kesh div table td #lblNdermarrje, .kesh div table td #lblEmriNdermarrje, .kesh div table td #lblPeriudha{
			color: white;
		}
		
    </style>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/FooterPanelInfo.aspx-IMB.2.1.js&v76"" type="text/javascript"></script>
</head>
<body onload="load()" class="">
    <form id="form1" runat="server">
          
        <div class="style1" >
            <table style="height: 20px; width: 100%;">
                <tr>
                    <td style="text-align: left; vertical-align: top" class="style2">
                        <dx:ASPxLabel ID="lblNdermarrje" runat="server" Text="Ndermarrja:"
                            Font-Bold="False" Font-Size="11">
                        </dx:ASPxLabel>
                    </td>
                    <td class="style3" style="vertical-align: top">
                        <dx:ASPxButtonEdit ID="txtNdermarrja" ClientInstanceName="txtNdermarrja" ReadOnly="true" runat="server" ClientIDMode="AutoID"
                            Width="100%" NullText="             " Font-Bold="True" Font-Size="11" ForeColor="Black">
                            <Buttons>
                                <dx:EditButton>
                                </dx:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {NdryshoNdermarrje();}" />
                            <ValidationSettings>
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                            </ValidationSettings>
                        </dx:ASPxButtonEdit>
                    </td>
                    <td class="style5" style="vertical-align: top">
                        <dx:ASPxLabel ID="lblEmriNdermarrje" runat="server" Text=""
                            Font-Bold="true" Font-Size="12">
                        </dx:ASPxLabel>
                    </td>
                    <td class="style4" style="vertical-align: top">
                        <div class="copyright">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="copyright@IMB" Style="font-size: small">
                        </dx:ASPxLabel>
                        &nbsp;<dx:ASPxHyperLink ID="ASPxHyperLink2" runat="server" Text="www.imb.al" Target="_blank"
                            NavigateUrl="http://www.imb.al" Style="font-size: small">
                        </dx:ASPxHyperLink>
                            </div>
                         <div style="display: none; background:url('images/emri_kesh_white.png')" class="kesh_slogan"></div> 
                    </td>
                    <td style="text-align: right; width: 8%; vertical-align: top; padding-right: 14px;">
                        <dx:ASPxLabel ID="lblPeriudha" runat="server" Text="Periudha"
                            Font-Bold="False" Font-Size="11">
                        </dx:ASPxLabel>
                    </td>
                    <td style="text-align: left; width: 100%; vertical-align: top;">
                        <dx:ASPxButtonEdit ID="btnPeriudha" ReadOnly="true" runat="server" Width="160px"
                            ClientInstanceName="btnPeriudha" ClientIDMode="AutoID"
                            Style="text-align: center"
                            NullText="                     " Font-Bold="True" Font-Size="11" ForeColor="Black">
                            <Buttons>
                                <dx:EditButton>
                                </dx:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {ShfaqPeriudhen();}" />
                            <ValidationSettings>
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                            </ValidationSettings>
                        </dx:ASPxButtonEdit>
                    </td>
            </table>
        </div>
    </form>
    <script>
        function NdryshoNdermarrje() {
            var parentWindow = window.parent;
            //var paneContent = parentWindow.splitter.GetPaneByName('paneKryesor');
            //contentUrl = "Login_Ndermarrje.aspx";
            window.parent.location.href = "Login_Ndermarrje.aspx?redirect=false";

            //paneContent.SetContentUrl(contentUrl);
            //paneContent.RefreshContentUrl();
        }
        function load() {

            if (document.getElementsByTagName('body')[0].className == 'kesh') {
                document.querySelector('.kesh_slogan').style.display = 'block';
                document.querySelector('.copyright').style.display = 'none';
            }
        };
    </script>
</body>
</html>
