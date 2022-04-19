<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AktivizoAlphaWeb.aspx.cs" Inherits="PlatinumWeb.AktivizoAlphaWeb" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autentifiko Alpha Web</title>
	    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <style type="text/css">
        .style24 {
            width: 43px;
            height: 28px;
        }

        .style25 {
            width: 12px;
            height: 28px;
        }

        .style26 {
            height: 28px;
            width: 334px;
        }

        .style30 {
            width: 43px;
            height: 35px;
        }

        .style31 {
            width: 12px;
            height: 35px;
        }

        .style32 {
            height: 35px;
            width: 334px;
        }

        .style34 {
            width: 43px;
            height: 27px;
        }

        .style35 {
            width: 12px;
            height: 27px;
        }

        .style36 {
            height: 27px;
            width: 334px;
        }

        .style37 {
            width: 43px;
            height: 32px;
        }

        .style40 {
            width: 43px;
            height: 33px;
        }

        .style41 {
            width: 12px;
            height: 33px;
        }

        .style42 {
            height: 33px;
            width: 334px;
        }

        .styleLoginButton {
            height: 28px;
            float: right;
            vertical-align: middle;
            margin-top: 0px;
        }

        .keniHarruar {
            float: left;
            vertical-align: middle;
            margin-top: 17px;
        }

        .auto-style1 {
            width: 7px;
        }

        .divQender {
            width: 400px;
            height: 200px;
            position: absolute;
            left: 50%;
            top: 50%;
            margin: -100px 0 0 -150px;
        }
    </style>
     <script src="DX.ashx?jsfileset=~/js/aspx.js/AktivizoAlphaWeb.aspx.js"
        type="text/javascript"></script>
</head>
<body>
    <form id="autentifikoAlphaWeb" runat="server">
        <div style="width: 100%; margin-top: 3%;">
            <table style="width: 100%;">
                <tbody>
                    <tr>
                        <td colspan="3">
                            <table style="margin-left: 20%;">
                                <tr style="height: 120px">
                                    <td>
                                        <dx:ASPxImage ID="ASPxImage6" runat="server" Height="80px" ImageUrl="~/images/FaqjaPare/AlphawebIcon.png"
                                            Width="80px">
                                        </dx:ASPxImage>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="AlphaLabel" runat="server" ForeColor="#8fc74a" Font-Names="Calibri"
                                            Font-Size="32pt" Text="ALPHA">
                                        </dx:ASPxLabel>
                                        &nbsp;<dx:ASPxLabel ID="webLabel" runat="server" ForeColor="#8fc74a" Font-Bold="True"
                                            Font-Names="Calibri" Font-Size="32pt" Text="WEB">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 40px;">
                        <td style="width: 17%;"></td>
                        <td style="border-top-style: solid; border-width: 1px; border-color: #8FC74A;">&nbsp;
                        </td>
                        <td style="width: 17%;"></td>
                    </tr>
                </tbody>
            </table>
            <table style="width: 60%; margin-left: 20%; margin-right: 20%">
                <tbody>
                    <tr>
                        <td>
                            <table style="height: 217px;">
                                <tr>
                                    <td class="style24">
                                        <dx:ASPxImage ID="ASPxImage1" runat="server" Height="43px" ImageUrl="~/images/FaqjaPare/network-ring-icon.png"
                                            Width="43px">
                                        </dx:ASPxImage>
                                    </td>
                                    <td class="style25"></td>
                                    <td class="style26">
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="13pt" Text="Administrim tërësisht">
                                        </dx:ASPxLabel>
                                        &nbsp;<dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True" Font-Italic="True"
                                            Font-Names="Arial" Font-Size="13pt" Text="online">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style34">
                                        <dx:ASPxImage ID="ASPxImage2" runat="server" Height="43px" ImageUrl="/images/FaqjaPare/tool-box-preferences-icon.png"
                                            Width="43px">
                                        </dx:ASPxImage>
                                    </td>
                                    <td class="style35"></td>
                                    <td class="style36">
                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Names="Arial" Font-Size="13pt"
                                            Text="Pa kosto instalimi dhe mirëmbajtje">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style37">
                                        <dx:ASPxImage ID="ASPxImage3" runat="server" Height="43px" ImageUrl="~/images/FaqjaPare/windows-7-security-icon.png"
                                            Width="43px">
                                        </dx:ASPxImage>
                                    </td>
                                    <td></td>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Names="Arial" Font-Size="13pt"
                                            Text="Siguria më e lartë e të dhënave">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style40">
                                        <dx:ASPxImage ID="ASPxImage4" runat="server" Height="43px" ImageUrl="~/images/FaqjaPare/credit-card-icon.png"
                                            Width="43px">
                                        </dx:ASPxImage>
                                    </td>
                                    <td class="style41"></td>
                                    <td class="style42">
                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Names="Arial" Font-Size="13pt"
                                            Text="Paguaj sipas përdorimit">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style30">
                                        <dx:ASPxImage ID="ASPxImage5" runat="server" Height="43px" ImageUrl="~/images/FaqjaPare/new.png"
                                            Width="43px">
                                        </dx:ASPxImage>
                                    </td>
                                    <td class="style31"></td>
                                    <td class="style32">&nbsp;<dx:ASPxHyperLink runat="server" ID="HelpLink" Font-Bold="True" Font-Italic="True"
                                        Font-Names="Arial" Font-Size="13pt" Text="Të rejat e versionit 4.0" Target="_blank">
                                    </dx:ASPxHyperLink>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="padding-left: 160px">
                             <table>
                                <tr>
                                    <td colspan="5">
                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Ju lutem kaloni id e gjeneruar më poshtë IMB-së!" Font-Size="X-Small" Font-Italic="True"></dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="ID e gjeneruar: " Font-Underline="True"></dx:ASPxLabel>
                                    </td>
                                    <td colspan="3">
                                        <dx:ASPxLabel ID="lblIdGjeneruar" runat="server" Text="ID e gjeneruar" Font-Bold="True" Font-Size="Larger"></dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="Ju lutem vendosni kodin e dhënë nga IMB-ja në formën e mëposhtme!" Font-Italic="True" Font-Size="X-Small"></dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxTextBox ID="txtKey1" ClientInstanceName="txtKey1" runat="server" Width="63px" MaxLength="5"> 
                                            <ClientSideEvents TextChanged="TextChanged_txtKey1" /> 
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtKey2" ClientInstanceName="txtKey2" runat="server" Width="63px" MaxLength="5">
                                             <ClientSideEvents TextChanged="TextChanged_txtKey2" /> 
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtKey3" ClientInstanceName="txtKey3" runat="server" Width="63px" MaxLength="5">
                                             <ClientSideEvents TextChanged="TextChanged_txtKey3" /> 
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtKey4" ClientInstanceName="txtKey4" runat="server" Width="63px" MaxLength="5">
                                             <ClientSideEvents TextChanged="TextChanged_txtKey4" /> 
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtKey5" ClientInstanceName="txtKey5" runat="server" Width="63px" MaxLength="5">
                                             <ClientSideEvents TextChanged="TextChanged_txtKey5" /> 
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <dx:ASPxButton ID="btnInstalo" ClientInstanceName="btnInstalo" runat="server" Text="Instalo kopjen e regjistruar!"
                                            Wrap="False" HorizontalAlign="Center" OnClick="btnInstalo_Click" VerticalAlign="Middle" ClientEnabled="false"></dx:ASPxButton>                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <dx:ASPxLabel ID="lblInfoInstalimi" runat="server" Text=""></dx:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 70px;">
                        <td colspan="8">&nbsp;
                        </td>
                    </tr>
                </tbody>
            </table>
            <table style="width: 100%;">
                <tbody>
                    <tr style="height: 8px;">
                        <td style="width: 17%;"></td>
                        <td style="border-top-style: solid; border-width: 1px; border-color: #8FC74A;"
                            colspan="2">&nbsp;
                        </td>
                        <td style="width: 17%;"></td>
                    </tr>
                </tbody>
            </table>
            <table style="width: 100%;">
                <tbody>
                    <tr>
                        <td style="width: 20%;">&nbsp;
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Font-Names="Arial" Font-Size="13pt"
                                Text="© 2014 IMB">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Font-Names="Arial" Font-Size="13pt"
                                Text="Kontakto për ndihmë!   |   Tel : +355 4 22 53 466 / 4 22 55 121 / 4 22 55 123" Width="600px"
                                Height="20px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 20%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%;">&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%;">&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Font-Names="Arial" Font-Size="13pt"
                                Text="Kosovë :  +377 44177110" Width="400px" Height="20px" Style="margin-bottom: 0px">
                            </dx:ASPxLabel>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="width: 20%;">&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Font-Names="Calibri" Font-Size="12pt"
                                Text="Vizitorët mund të logohen me përdorues: vizitor dhe fjalëkalim: vizitor"
                                Width="100%" Height="20px" Style="margin-bottom: 0px">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
    </form>
</body>
</html>
