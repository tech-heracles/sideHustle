<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NdryshoFjalekalim.aspx.cs" Inherits="PlatinumWeb.E_PaySlip.NdryshoFjalekalim" %>
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

     <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js
         ;~/js/myCookies-IMB.2.1.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/srcCRM/js/jquery.mmenu.min.all.js;~/js/myNrAuto-IMB.2.1.js;~/E-PaySlip/js/CoreEpaySlip.js&v49"
        type="text/javascript"></script>
      <script type="text/javascript">

          $(function () {
              $('nav#menu').mmenu({
                  classes: "mm-light",
              });


          });
           $(document).ready(function (e) {

              var menu = new EPaySlip();
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
                    <td style="width:1%;">
                  
                    </td>
                    <td style="width:94%;vertical-align: top;"><dx:ASPxLabel runat="server" ID="lblNdryshoFjalekalim" Font-Size="16px" ></dx:ASPxLabel>
</td>
                    <td style="width:5%;">
                        <div id="emriLogout" class="emriLogout">
                            <div id="userInfo">
                                <div id="emri" >
                        
                                </div>
                              <div id="logout">
                          
                                    <a style="position:relative; color:white;background-image: none;"  id="dalje" runat="server">Dalje</a>
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
            <form id="form1" runat="server" style="width: 100%">
        <div id="linkuDalje" visible="false" runat="server">
             <table border="0" style="padding: 0px; border-collapse: collapse; border-spacing: 0; width: 100%">
            <tr style="padding-right: 5px; padding-top: 0px">
                <td style="width: 25%"></td>
                <td style="width: 50%"></td>
                <td style="text-align: right; width: 25%; align-content: flex-end; vertical-align: top; padding-top: 0px">
                    <dx:ASPxLabel ID="lblUserEmri" ClientInstanceName="lblUserEmri" runat="server" Text=""
                        Font-Size="11" ForeColor="Black" Font-Names="Calibri">
                    </dx:ASPxLabel>
                    <dx:ASPxHyperLink ID="ASPxHyperLink2" runat="server" EnableViewState="false" ViewStateMode="Disabled"
                     ForeColor="Black" Font-Underline="false" Text="Dalje" Font-Size="11"
                        Font-Names="Calibri">
                    </dx:ASPxHyperLink>
                </td>
            </tr>
                 </table>
        </div>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfPassPerkohshem" runat="server" ClientInstanceName="hfPassPerkohshem">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfGjatesiMinPassword" runat="server" ClientInstanceName="hfGjatesiMinPassword">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfResetimPass" runat="server" ClientInstanceName="hfResetimPass">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfSkaduarPassIPerdoruesit" runat="server" ClientInstanceName="hfSkaduarPass"></dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState"></dx:ASPxHiddenField>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                OnItemClick="ASPxMenu1_ItemClick">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                <ClientSideEvents ItemClick="function(s, e) {
	                      
                            }" Init="function(s) {s.SetClientVisible(true);}" />
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
                            <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                        BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                        <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <SubMenuStyle GutterWidth="17px" />
                                    </dx:ASPxMenu>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlPaswordi" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <br />
                <table>
                    <tr>
                        <td>
                            <div id="dvlblUsername">
                                <dx:ASPxLabel ID="lblUsername" runat="server" ClientInstanceName="lblUsername" Text="Perdoruesi:">
                                </dx:ASPxLabel>
                            </div>
                        </td>
                        <td>
                            <div id="dvusername_TextBox" style="width: 167px">
                                <dx:ASPxTextBox ID="btnPerdorues" HorizontalAlign="Center" runat="server" ClientEnabled="True" ClientInstanceName="btnPerdorues"
                                    Cursor="default" Font-Bold="True" Font-Italic="False" Font-Names="Times New Roman"
                                    Font-Size="12pt" VerticalAlign="Top" Width="170px" Enabled="False">
                                    <DisabledStyle BackColor="#EEEEEE" BackgroundImage-HorizontalPosition="center">
                                        <BackgroundImage HorizontalPosition="center" />
                                    </DisabledStyle>
                                </dx:ASPxTextBox>
                            </div>
                        </td>
                        <tr style="height: 30px; padding-left: 10px;">
                            <td>
                                <div id="dvlblPasswordieksistues">
                                    <dx:ASPxLabel ID="lblPasswordieksistues" runat="server" ClientInstanceName="lblPasswordieksistues"
                                        Text="Fjalekalimi ekzistues:">
                                    </dx:ASPxLabel>
                                </div>
                            </td>
                            <td>
                                <div id="dvtxtPasswordieksistues">
                                    <dx:ASPxTextBox ID="txtPasswordieksistues" runat="server" ClientInstanceName="txtPasswordieksistues"
                                        Password="True" Width="170px">
                                        <ValidationSettings CausesValidation="true" SetFocusOnError="true" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                      
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </div>
                            </td>
                        </tr>
                        <tr style="height: 30px; padding-left: 10px;">
                            <td>
                                <div id="dvlblPassword">
                                    <dx:ASPxLabel ID="lblPassword" runat="server" ClientInstanceName="lblPassword" Text="Fjalekalimi:">
                                    </dx:ASPxLabel>
                                </div>
                            </td>
                            <td>
                                <div id="dvpassword_TextBox">
                                    <dx:ASPxTextBox ID="password_TextBox" runat="server" ClientInstanceName="password_TextBox"
                                        Password="True" Width="170px">
                                        <ClientSideEvents TextChanged="function(s, e) { kontrolloPassword(s, e); }" />
                                        <ValidationSettings CausesValidation="True" SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                 
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </div>
                            </td>
                        </tr>
                        <tr style="height: 30px; padding-left: 15px;">
                            <td>
                                <div id="dvlblKonfirmoPassword">
                                    <dx:ASPxLabel ID="lblKonfirmoPassword" runat="server" ClientInstanceName="lblKonfirmoPassword"
                                        Text="Konfirmo fjalekalimin:">
                                    </dx:ASPxLabel>
                                </div>
                            </td>
                            <td>
                                <div id="dvkonfirmo_Textbox">
                                    <dx:ASPxTextBox ID="konfirmo_Textbox" runat="server" ClientInstanceName="konfirmo_Textbox"
                                        Password="True" Width="170px">
                                        <ValidationSettings CausesValidation="True" SetFocusOnError="True" ValidationGroup="entries">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </div>
                            </td>
                            <td>
                              
                            </td>
                        </tr>
                 <tr style="height: 30px; padding-left: 15px;">
                     <td></td>
                     <td>
                                     <div id="dvkonfirmo_Textbox">
                                         <dx:ASPxButton runat="server" id="btnruaj" Text="Ruaj"  OnClick="btnRuaj_Click" Theme="Metropolis" ></dx:ASPxButton>
                                         </div>
                     </td>

                 </tr>
              <tr style="height: 30px; padding-left: 15px;">
                     <td></td>
                     <td>
                                     <div id="dvkonfirmo_Textbox">
                                      <dx:ASPxLabel runat="server" ID="lblmsg" visible="false" Theme="RedWine" ></dx:ASPxLabel>
                                         </div>
                     </td>

                 </tr>
                                         
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popNdryshoPass" runat="server" AllowDragging="True" ClientIDMode="AutoID"
            ClientInstanceName="popNdryshoPass" CloseAction="CloseButton" CssPostfix="Glass" EnableAnimation="False"
            EnableViewState="False" Font-Bold="true" HeaderText="Skadim Fjalëkalimi" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" Width="300px">
            <HeaderStyle>
                <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
            </HeaderStyle>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                    <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel ID="lblSkaduarPass" runat="server" ClientIDMode="AutoID" Text="Fjalëkalimi juaj ka skaduar , ju duhet t'a ndryshoni atë!">
                                </dx:ASPxLabel>
                                <br />
                                <br />
                                <div style="text-align: right;">
                                    <table>
                                        <tr>
                                            <td style="width: 57%"></td>
                                            <td style="align-content: center">
                                                <dx:ASPxButton ID="ButtonOk" Width="70px" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                    Text="Ok" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {popNdryshoPass.Hide();}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="visibility: hidden">
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel >
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl >
    </form>

        </div>
        <nav id="menu" data-role="panel" data-display="overlay">
    
            <ul id="ulMenu" runat="server" data-role="panel" data-display="overlay">

           <li>Ndrysho fjalekalimin</li>
          
              
            </ul>
        </nav>
        </div>
    
</body>
</html>
