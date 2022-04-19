<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaHarta.aspx.cs" Inherits="PlatinumWeb.LupaHarta" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
      <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	  <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="//maps.googleapis.com/maps/api/js?key=AIzaSyACrrRhtdTbsjW6rvdFyl-7mtFWeJL60R0" type="text/javascript"></script>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/OpenLayers-2.13.1/OpenLayers.js;~/js/myHarta.js;~/js/aspx.js/LupaHarta.aspx-IMB.4.3.js&v76""
        type="text/javascript">
    </script>
    <link href="styleOL.css" rel="stylesheet" type="text/css" />
    <link href="stileShtoPike.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; left: 0px; right: 0px; margin-left: 0px; margin-right: 0px;">
          
        <asp:ScriptManager ID="ScriptManager1" runat="server">
          
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
        </dx:ASPxHiddenField>
        <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server" OnMenuTemplate="PercaktoTemplateMenu"/>
        <div>
            <div>
                <br/>
                <table style="width: 100%; top: 5px;"  id="">
                    <tr style="width: 100%;">
                        <td style="width: 40%">
                            <dx:ASPxLabel ID="ASPxLabel1" ClientInstanceName="lblKordinate" Width="100%" runat="server" Text="Cakto Koordinatat (x, y): "></dx:ASPxLabel>
                        </td>                       
                        <td style="width: 60%">
                            <dx:ASPxTextBox ID="koordinate" ClientInstanceName="koordinate" runat="server" Width="100%">
                                <ClientSideEvents TextChanged="function (s, e){ harta.ndryshoPikeNeHarte(); }" />
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="map" class="mapDivCss1"></div>
        </div>
    </form>
</body>
</html>
