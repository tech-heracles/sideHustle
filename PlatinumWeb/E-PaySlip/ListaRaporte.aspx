<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaRaporte.aspx.cs" Inherits="PlatinumWeb.E_PaySlip.ListaRaporte" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v18.2.Core, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E PaySlip</title>
       <link rel="icon" type="image/ico" href="CRM/faviconCRM.ico"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>   
    <link rel="stylesheet" href="~/public/jquery.mmenu.all.css" />    
    <link rel="stylesheet" href="~/public/AlphaCRM.css" />
    <link rel="stylesheet" media="screen (min-width: 768px)" href="~/public/AlphaCRM.css"/>
    <link href="~/public/font-awesome.min.css" rel="stylesheet" />

     <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/srcCRM/js/jquery.mmenu.min.all.js;~/js/myNrAuto-IMB.2.1.js;~/E-PaySlip/js/CoreEpaySlip.js&v49"
        type="text/javascript"></script>
      <script type="text/javascript">

          $(function () {
              $('nav#menu').mmenu({
                  classes: "mm-light",
              });


          });
          $(document).ready(function (e) {
              var NdermarrjeKodi = hfState.Get("NdermarrjeKodi");
              var menu = new EPaySlip();
              menu.krijoMenu(true, NdermarrjeKodi != "ESC");
              $("#dalje").attr("href", menu.logoutPath());
          });
    </script>


    <style>
        .header, .footer {
            background: #ed1b24;
        }

        .fa {
            color: #ed1b24;
        }
    </style>



</head>

<body>
    <div id="page">
        <div class="header">    
            <table style="width:100%;">
                <tr>
                    <%--<td style="width:1%;">
                        <a href="#menu"></a>
                    </td>--%>
                    <td style="width:94%;vertical-align: top;">
                        <dx:ASPxLabel runat="server" ID="lblLista" Font-Size="16px" ></dx:ASPxLabel>
                   

                    </td>
                    <td style="width:5%;">
                          <div id="emriLogout" class="emriLogout">
                            <div id="userInfo">
                                <div id="emri" >
                                    <dx:ASPxLabel  ID="lblUserEmri" ClientInstanceName="lblUserEmri" runat="server" Text=""
                                        Font-Size="14" ForeColor="White" Font-Names="Arial">
                                    </dx:ASPxLabel>
                                </div>
                                <div id="logout">
                                    <a style="position:relative; color:white;background-image: none;" runat="server" id="dalje">Dalje</a>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="content" class="content">     
            <div data-role="content" class="my-home-page"  data-theme="a">
                <ul id="myHomePage" data-theme="a" data-role="listview" class="ui-listview"></ul>
        
            </div>
            <form runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
                </asp:ScriptManager>
                <dx:ASPxHiddenField ID="dtdoknga" runat="server" ></dx:ASPxHiddenField>    
                <dx:ASPxHiddenField ID="dtdokderi" runat="server" ></dx:ASPxHiddenField>    
                  <dx:ASPxHiddenField ID="hfState" runat="server" ></dx:ASPxHiddenField>                
     
            </form>
            
        </div>
        <nav id="menu" runat="server" data-role="panel" data-display="overlay">
    
            <ul id="ulMenu" runat="server" data-role="panel" data-display="overlay">

           
          
              
            </ul>
        </nav>
        </div>
</body>

</html>
