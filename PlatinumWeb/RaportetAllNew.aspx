<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RaportetAllNew.aspx.cs" Inherits="PlatinumWeb.RaportetAllNew" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>





<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AVEC Accounting</title>
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/RaportetAll.aspx-IMB.2.1.js;&v49"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         
    
    <div>
        <table style="border: 1px solid #999999; background-image: url('images/grad_bg_image.gif');
                background-repeat: repeat-x; height: 85px; width: 100%;">
                <tr>
                    <td style="width: 15%; padding-left: 20px;">
                        <dx:ASPxLabel ID="listaRaporteve" runat="server" Text="Lista e raporteve" Font-Bold="false"
                            Font-Italic="False" Font-Size="16pt" ForeColor="Gray">
                        </dx:ASPxLabel>
                    </td>
                    <td align="right">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <dx:ASPxLabel ID="lbld" runat="server" Text="Datë dokumenti" Style="color: #003366;
                                        font-size: 10pt; font-family: Tahoma; color: #666666;" Width="100px">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server" RepeatColumns="3"
                                        ClientIDMode="AutoID" ClientInstanceName="btnradPeriudha" SelectedIndex="0" Width="100%"
                                        TextSpacing="2px" Border-BorderWidth="0px">
                                        <ClientSideEvents ValueChanged="RaportetAll.klickselectedvaluedok" Init="RaportetAll.klickselectedvaluedok" />
                                        <Items>
                                            <dx:ListEditItem Text="Aktuale" Value="0" Selected="True" />
                                            <dx:ListEditItem Text="Periudha" Value="1" />
                                            <dx:ListEditItem Text="Viti ushtrimor" Value="2" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </td>
                                <td align="center">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Nga" Width="30px" Style="color: #666666">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dtdoknga" runat="server" ClientIDMode="AutoID" AutoPostBack="false"
                                        AllowNull="false" DateOnError="Today" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                        Date="2009-05-06" ValidationSettings-CausesValidation="True" ShowShadow="False"
                                        ClientInstanceName="dtdoknga" Width="200px">
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td align="center">
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Deri" Width="30px" Style="color: #666666">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dtdokderi" runat="server" ClientIDMode="AutoID" AutoPostBack="false"
                                        AllowNull="false" DateOnError="Today" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                        Date="2009-05-06" ValidationSettings-CausesValidation="True" ShowShadow="False"
                                        ClientInstanceName="dtdokderi" Width="200px">
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td style="width: 2px">
                                </td>
                                <td valign="top" align="center" style="width: 18%; padding-bottom: 15px;">
                                    <dx:ASPxLabel ID="lblpersonalizuara" runat="server" Text="Raportet e personalizuara"
                                        Style="color: #003366; font-size: 10pt; font-family: Tahoma; color: #666666;">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <table align="right" width="15%">
                                        <tr style="width: 15%">
                                            <td align="right" style="padding-bottom: 2px;">
                                                <dx:ASPxButton ID="btnCollapseAll" runat="server" Text="-" ClientInstanceName="btnCollapseAll"
                                                    Width="40" Height="25" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                                                    Font-Bold="True">
                                                    <ClientSideEvents Click="RaportetAll.collapseAll" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td align="right" style="padding-bottom: 2px;">
                                                <dx:ASPxButton ID="btnExpandAll" runat="server" Text="+" ClientInstanceName="btnExpandAll"
                                                    Width="40" Height="25" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                                                    Font-Bold="True">
                                                    <ClientSideEvents Click="RaportetAll.expandAll" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        <dx:ASPxSiteMapControl ID="sitemap" runat="server"  Width="100%" DefaultLevelProperties-Font-Size="18px" OnDataBound="sitemap_DataBound" FlowLayoutTextLineHeight="24px" Font-Size="18px"   >
        </dx:ASPxSiteMapControl>
    </div>
    </form>

<body>
    

</html>
