<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaValidimKlientiArketime.aspx.cs" Inherits="PlatinumWeb.LupaValidimKlientiArketime" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaValidimKlientiArketime.aspx-IMB.4.0.24.js&v25""
        type="text/javascript"></script>
    </head>
<body>
    <form id="form1" runat="server">
        <script  type="text/javascript">       
        </script>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
    
        </asp:ScriptManager>
           
                <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <dx:ASPxHiddenField ID="hfState" runat="server">
        </dx:ASPxHiddenField>
        <table width="100%">
            <tr>
                <td>
                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                        ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                       >
                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                        <ClientSideEvents ItemClick="function(s, e) {
	                                    menu_click(s,e);
                                        }" Init="function(s) {s.SetClientVisible(true);}" />
                        <ItemImage Height="32px" Width="32px">
                        </ItemImage>
                        <SubMenuItemImage Height="16px" Width="16px">
                        </SubMenuItemImage>

                        <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
                            <Paddings PaddingBottom="1px" PaddingTop="9px" />
                            <Paddings PaddingTop="9px" PaddingBottom="1px"></Paddings>
                        </ItemStyle>
                        <SubMenuItemStyle Width="32px">
                        </SubMenuItemStyle>
                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify"></SubMenuStyle>
                    </dx:ASPxMenu>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                <ItemStyle HorizontalAlign="Left" />
                            </dx:ASPxMenu>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="pnlKryesor" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr height="25px"  id="kontakti">
                        <td >

                            <dx:ASPxLabel ID="lblNrKontakti" runat="server" AssociatedControlID="txtNrKontakti" ClientInstanceName="lblNrKontakti"
                                Wrap="False" Text="Nr. Tel">
                            </dx:ASPxLabel>
                        </td>
                        <td align="right">

                            <dx:ASPxLabel ID="lbl355" runat="server" AssociatedControlID="txtNrKontakti" ClientInstanceName="lbl355"
                                Wrap="False" Text="3556">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtNrKontakti" Width="100%" runat="server" ClientInstanceName="txtNrKontakti">
                                <ValidationSettings Display="Dynamic" ValidationGroup="entries" ErrorDisplayMode="ImageWithTooltip"
                                    ValidateOnLeave="true">
                               
                                    <RegularExpression ValidationExpression="[0-9.,]*" ErrorText="Lejohen vetem numra"></RegularExpression>
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr height="25px" id="pin">
                        <td>

                            <dx:ASPxLabel ID="lblEmriKlientit" runat="server" AssociatedControlID="txtEmriKlientit" ClientInstanceName="lblEmriKlientit"
                                Wrap="False" Text="Emri klientit">
                            </dx:ASPxLabel>
                        </td>
                        <td></td>
                        <td>
                            <dx:ASPxTextBox ID="txtEmriKlientit" Width="100%" runat="server" ClientInstanceName="txtEmriKlientit">
                                <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                    ValidateOnLeave="False">
                                   
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                      <tr height="25px" id="discount">
                        <td>

                            <dx:ASPxLabel ID="lblNrLlogarie" runat="server" AssociatedControlID="txtNrLlogarie" ClientInstanceName="lblNrLlogarie"
                                Wrap="False" Text="Nr.Llogarise">
                            </dx:ASPxLabel>
                        </td>
                        <td></td>
                        <td>
                            <dx:ASPxTextBox ID="txtNrLlogarie" Width="100%" runat="server" ClientInstanceName="txtNrLlogarie">
                                <ValidationSettings Display="Dynamic" ValidationGroup="entries1" ErrorDisplayMode="ImageWithTooltip"
                                    ValidateOnLeave="False">
                             
                                </ValidationSettings>
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr height="25px" id="bundle">
                        <td>

                            &nbsp;</td>
                        <td></td>
                        <td>
                            <dx:ASPxButton ID="btnValido" runat="server" ClientInstanceName="btnValido" Height="25px" OnClick="btnValido_Click" Text="Valido" ValidationGroup="entries1">
                                <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                </DisabledStyle>
                                <ClientSideEvents Click="btnValidoClick" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>


                </table>


                <asp:HiddenField ID="hfStatus" runat="server"  Value="false" />
          <asp:HiddenField ID="hfTeDhenaKlienti" runat="server" Value="" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
