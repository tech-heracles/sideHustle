<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Raportet.aspx.cs" Inherits="PlatinumWeb.E_PaySlip.Raportet" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
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

        a.MyLinks:active
        {
            text-decoration: none;
            color: #FF9900;
        }
        .auto-style1
        {
            width: 21%;
        }
    </style>

    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Raportet.aspx-IMB.2.1.js&v51"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <div id="div2" style="width: 100%; display: block; float: left;">
            <div style="width: 100%">
                <table style="border: 1px solid #999999; background-image: url('images/grad_bg_image.gif'); background-repeat: repeat-x; height: 85px; width: 100%;">
                    <tr>
                        <td colspan="7"></td>
                    </tr>
                    <tr valign="middle">
                        <td align="right">
                            <dx:ASPxLabel ID="lbld" runat="server" Text="Datë dokumenti" Style="color: #003366; font-size: 10pt; font-family: Tahoma; color: #666666;">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server" RepeatColumns="4"
                                ClientIDMode="AutoID" ClientInstanceName="btnradPeriudha" SelectedIndex="0" Width="90%"
                                TextSpacing="2px" Border-BorderWidth="0px" style="margin-left: 5px">
                                <ClientSideEvents ValueChanged="function(s,e){klickselectedvaluedok(s,e);}" Init="function(s,e){klickselectedvaluedok(s,e);}" />
                                <Items>
                                    <dx:ListEditItem Text="Aktuale" Value="0" Selected="True" />
                                    <dx:ListEditItem Text="Periudha" Value="1" />
                                    <dx:ListEditItem Text="Viti ushtrimor" Value="2" />
                                    <dx:ListEditItem Text="Gjithe Vitet" Value="3" />
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
                                ClientInstanceName="dtdoknga" Width="100px">
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
                                ClientInstanceName="dtdokderi" Width="100px">
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
                        <td valign="baseline" class="auto-style1">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Filtër i personalizuar" Width="100%"
                                Style="color: #666666; font-size: 10pt; font-family: Tahoma;">
                            </dx:ASPxLabel>
                            <br />
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ClientInstanceName="ASPxComboBox1"
                                SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False" ValueType="System.String"
                                Width="93%">
                                <LoadingPanelImage>
                                </LoadingPanelImage>
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
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                            <table align="right" width="15%">
                                <tr style="width: 15%">
                                    <td align="right" style="padding-bottom: 2px;">
                                        <dx:ASPxButton ID="btnHelp" runat="server" Text="" ClientInstanceName="btnHelp" Width="40"
                                            Height="24" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                                            ImagePosition="Bottom" HorizontalAlign="Center" ToolTip="Ndihme" OnInit="btnHelp_Init">
                                            <Image Url="~/images/new/help_14.png">
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="right" style="padding-bottom: 2px;">
                                        <dx:ASPxButton ID="btnCollapseAll" runat="server" Text="-" ClientInstanceName="btnCollapseAll"
                                            Width="40" Height="24" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                                            Font-Bold="True">
                                            <ClientSideEvents Click="function(s,e){window.location='ListaRaporte.aspx';}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="right" style="padding-bottom: 2px;">
                                        <dx:ASPxButton ID="btnExpandAll" runat="server" Text="+" ClientInstanceName="btnExpandAll"
                                            Width="40" Height="24" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                                            Font-Bold="True">
                                            <ClientSideEvents Click="function(s,e){expandAll(s,e);}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 100%">
                <%--<dx:ASPxNewsControl ID="newsControl" EnableTheming="True" runat="server" Width="100%" DataSourceID="sqldatasourcereports" NameField="IDRAPORTI" NavigateUrlField="NAVIGATEURL" TextField="RAPEMRI"></dx:ASPxNewsControl>--%>
      <%--          <asp:UpdatePanel ID="updreportsmodules" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>--%>
                        <table style="border-style: none solid solid solid; border-width: 1px; border-color: #999999; background-color: #FCFCFC; width: 100%; height: 510px;">
                            <tr>
                                <td></td>
                                <td align="center" style="width: 21px; padding-top: 20px;">
                                    <asp:Image ID="Image1" runat="server" Height="21" ImageUrl="~/images/t7.gif" Width="21"
                                        ImageAlign="Bottom" />
                                </td>
                                <td style="padding-left: 10px; padding-top: 20px;">
                                    <asp:Label ID="moduliLabel" runat="server" ForeColor="#FF9900" Font-Names="Tahoma"
                                        Font-Size="Medium"></asp:Label>
                                </td>
                                <td width="65%"></td>
                            </tr>
                            <tr style="height: 10px; width: 100%;">
                                <td colspan="4"></td>
                            </tr>
                            <asp:Repeater ID="Repeater2" runat="server" DataSourceID="sqldatasourcereports" OnItemDataBound="Repeater2_ItemDataBound">
                                <ItemTemplate>
                                    <tr style="width: 100%;">
                                        <td colspan="2"></td>
                                        <td style="border-bottom: 1px solid #EBEBEB; padding-bottom: 4px; padding-top: 3px; padding-left: 7px; width: 200px;">                                            
                                            <dx:ASPxHyperLink ID="hapRaport" runat="server" ToolTip="Filtro Raportin" NavigateUrl="javascript:void('0')"
                                                ImageUrl="~/images/filter2.png" Style="padding-right: 7px" ImageHeight="20" ImageWidth="20" />
                                            <dx:ASPxHyperLink runat="server" NavigateUrl="javascript:void('0')" ID="filtroRaport"
                                                Text=""  CssClass="MyLinks">
                                            </dx:ASPxHyperLink>
                                        </td>
                                        <td width="65%"></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="4" style="width: 100%;">
                                    <asp:HiddenField ID="hdn1" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr style="height: 65%; width: 100%;">
                                <td colspan="4"></td>
                            </tr>
                        </table>
                        <asp:SqlDataSource runat="server" ID="sqldatasourcereports"></asp:SqlDataSource>
              
            </div>
        </div>
   
        <dx:ASPxLabel ID="lblerror" runat="server" Visible="false" Text="ASPxLabel">
        </dx:ASPxLabel>
    </form>
</body>
</html>
