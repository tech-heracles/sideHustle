<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaEksportim.aspx.cs"
    Inherits="PlatinumWeb.LupaEksportim" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>





<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <style type="text/css">
        .style1
        {
            height: 54px;
        }
    </style>
  <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
  <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaEksportim.aspx-IMB.2.3.js&v76""
        type="text/javascript"></script>
</head>
<body onload="Init()">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
           
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">

        </dx:ASPxGlobalEvents>
         
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <div id='div' style="display: none">
            <asp:UpdatePanel EnableHierarchyRecreation="false" ID="ASPxUpdatePanel1" runat="server" ClientInstanceName="panel"
                Width="100%" HeaderText="Zgjidh Konfigurimin" ShowHeader="False">
                <ContentTemplate>                  
                        <table width="100%">
                            <tr>
                                <td>
                                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" ItemImagePosition="Top"
                                        Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                        SeparatorWidth="1px">
                                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                        <ClientSideEvents ItemClick="function(s, e) { menu_click(s,e); }" Init="function(s) {s.SetClientVisible(true);}" />
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
                        <table width="100%">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lblTipi" runat="server" Text="Tipi">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxRadioButtonList ID="rbTipi" runat="server" ValueType="System.String" Theme="Aqua"
                                        ClientInstanceName="rbTipi" Width="168px">
                                        <Items>
                                            <dx:ListEditItem Text="XLS" Value="XLS" Selected="true" />
                                            <dx:ListEditItem Text="XLSX" Value="XLSX" Selected="false" />
                                            <dx:ListEditItem Text="CSV" Value="CSV" Selected="false" />
                                        </Items>
                                        <ClientSideEvents SelectedIndexChanged="function (s,e){ enabled()}" />
                                    </dx:ASPxRadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lblSimboliNdares" runat="server" Text="Simboli Ndares">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtSimboliNdares" runat="server" Width="170px" Theme="Aqua"
                                        ClientInstanceName="txtSimboliNdares">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxCheckBox ID="cbSimboliNdares" runat="server" Text="Tab" Theme="Aqua" ClientInstanceName="cbSimboliNdares">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lblEmerSheet" runat="server" Text="Emri i Sheet-it te Excelit">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtEmerSheet" runat="server" Width="170px" Theme="Aqua" ClientInstanceName="txtEmerSheet">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lblEmerSkedari" runat="server" Text="Emri i Skedarit">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtEmerSkedari" runat="server" ClientInstanceName="txtEmerSkedari"
                                        Width="170px" Theme="Aqua">
                                        <ValidationSettings CausesValidation="True" ValidationGroup="entries">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    <div style="visibility: hidden; height: 10px; overflow: scroll">
                        <dx:ASPxGridView ID="ASPxGridView1" runat="server">
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
                        </dx:ASPxGridViewExporter>
                    </div>
                    <dx:ASPxButton runat ="server" ID="Export" OnClick="ExportoGride" ClientInstanceName="Exporto" ClientVisible ="false"></dx:ASPxButton>
                </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Export" />
            </Triggers>
            </asp:UpdatePanel>
        </div>


    </form>
</body>
</html>
