<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OneTimePinForm.aspx.cs" Inherits="PlatinumWeb.E_PaySlip.OneTimePinForm" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxoc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Alpha Web</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <%--    <meta http-equiv="X-UA-Compatible" content="IE=8" >--%>
    <meta name="description" content="Login-i AlphaWeb" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
      <link rel="icon" type="image/ico" href="CRM/faviconCRM.ico"/>
    <%--  <script src="js/jquery-1.10.2.min.js"></script>        --%>
    <script src="public/modernizr.min.js"></script>
    <script src="public/placeholder.js"></script>
    <link rel="stylesheet" href="~/public/login.css" />
    <script>

        function getClientDate() {
            var date = new Date();
            var dt = date.getDate();
            var m = date.getMonth();
            var year = date.getFullYear();
            var hours = date.getHours();
            var min = date.getMinutes();
            var sec = date.getSeconds();
            var dateStr = (m + 1) + "/" + dt + "/" + year + " " + hours + ":" + min + ":" + sec;
            clientDate.Set('clientDate', dateStr);
        }

     

    

    </script>
	<style>
		.logoutIMG{
			float: right;
		}
	</style>
</head>
<body>

    <form id="authForm" runat="server" style="width: 100%">
        <dx:ASPxHiddenField ID="clientDate" ClientInstanceName="clientDate" runat="server">
        </dx:ASPxHiddenField>
        <div class="main">
            <section class="submain">
                <header class="header">
                    <div id ="logoHeade" style=" display: inline;">
					<dx:ASPxImage ID="ASPxImage6" Height="60px" Width="231px" CssClass="logo" runat="server" ImageUrl="~/images/FaqjaPare/vodLogo.png">
                    </dx:ASPxImage>
                    <section class="gjuhet">
                    </section>
                    </div>
					<div  class="emriLogout" style=" display: inline;">
                            <div>
                                <div>
										<a style="position:relative; color:black; "  runat="server" href="~/E-PaySlip/Login.aspx?arsye=logout">
										<img border="0" style="float: right;" alt="vodLogout" src="images/FaqjaPare/vodLogout.png" width="52px" height="52px">
										</a>
                                </div>
                            </div>
                        </div>
						<div id="emriLogout" class="emriLogout" style=" display: none;">
							<div id="userInfo" >
                                <div id="emri" >
										<a style="position:relative; color:black; "  runat="server" ID="dalje"   href="~/E-PaySlip/Login.aspx?arsye=logout">
										<dx:ASPxImage Height="52px" Width="52px" runat="server"  CssClass="logoutIMG" ImageUrl="~/images/FaqjaPare/vodLogout.png"></dx:ASPxImage>
										</a>
                                </div>
                            </div>
                        </div>
                </header>
                <section class="content">
                      <header>
                            <dx:ASPxLabel ID="LabelInfo" ClientInstanceName="LabelInfo" CssClass="labelInfo"  runat="server" Style="color: #FF0000; font-size: small; font-weight: bold; font-family: Calibri">
                           
                            </dx:ASPxLabel>
                    </header>
                    <section class="mainContent">
                        <section class="left">
                            <section class="info">
                                <table>
                                    <tr>
                                        <td>
                         <%--                   <dx:ASPxImage ID="OnlineImg" runat="server" CssClass="infoIcon" ImageUrl="~/images/FaqjaPare/Online.png">
                                            </dx:ASPxImage>--%>
                                        </td>
                                        
                                    <td>
                                <%--        <dx:ASPxLabel ID="OnlineLbl" runat="server" CssClass="infoLbl" Text="Administrim tërësisht">
                                        </dx:ASPxLabel>--%>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td>
                                       <%--     <dx:ASPxImage ID="KostoImg" runat="server" CssClass="infoIcon" ImageUrl="/images/FaqjaPare/Kosto.png">
                                            </dx:ASPxImage>--%>
                                        </td>

                                        <td>
                                          <%--  <dx:ASPxLabel ID="KostoLbl" runat="server" CssClass="infoLbl" Width="300" Text="Pa kosto instalimi dhe mirëmbajtje">
                                            </dx:ASPxLabel>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                           <%-- <dx:ASPxImage ID="SiguriImg" runat="server" CssClass="infoIcon" ImageUrl="~/images/FaqjaPare/Siguri.png">
                                            </dx:ASPxImage>--%>
                                        </td>

                                        <td>
                                          <%--  <dx:ASPxLabel ID="SiguriLbl" runat="server" CssClass="infoLbl" Text="Siguria më e lartë e të dhënave">
                                            </dx:ASPxLabel>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <%--    <dx:ASPxImage ID="PagesaImg" runat="server" CssClass="infoIcon" ImageUrl="~/images/FaqjaPare/Paguaj.png">
                                            </dx:ASPxImage>--%>
                                        </td>

                                        <td>
                                       <%--     <dx:ASPxLabel ID="PagesaLbl" runat="server" CssClass="infoLbl" Text="Paguaj sipas përdorimit">
                                            </dx:ASPxLabel>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                     <%--       <dx:ASPxImage ID="NewVersionImg" runat="server" CssClass="infoIcon" ImageUrl="~/images/FaqjaPare/new.png">
                                            </dx:ASPxImage>--%>
                                        </td>

                                        <td>
                        <%--           <dx:ASPxLabel ID="lblmsg" Visible="false"   runat="server" Cursor="pointer" Text=" ">
                                            </dx:ASPxLabel>--%>
                                        </td>
                                    </tr>
                                </table>
                        </section>
                        </section>

                        <section style="margin: auto; display: table;">
                            <section class="loginForm">
                                <%--           <table  BorderStyle="Solid" BorderWidth="1px"  Font-Size="0.8em"
                                ForeColor="#333333">
                                     
                                     <tr><td>  <dx:ASPxLabel runat="server" ID="lblPin" Text="Pini:"  CssClass="login"></dx:ASPxLabel>

                                         </td>
                                     
                                     <td>    
                           <dx:ASPxTextBox runat="server" ID="txtPin"  CssClass="login"></dx:ASPxTextBox></td></tr></table>--%>
                                <asp:ScriptManager ID="Scriptmanager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="Label1" runat="server" Style="color: #ed1b24; text-align:left; font-size: xx-large; font-weight: bold; font-family: Calibri"></asp:Label>
                                       <asp:Label ID="Label2" runat="server" Style="color: #ed1b24; text-align:left; font-size: small; font-weight: bold; font-family: Calibri" Visible-="false"></asp:Label>
                                        
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tm1" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                       <asp:Timer ID="tm1"  runat="server"  Interval="1000"  OnInit  ="tm1_Init"   OnTick="tm1_Tick" />
                                <asp:Login ID="KodHyresForm" CssClass="login" runat="server" LoginButtonType="Image"
                                    BorderStyle="Solid" BorderWidth="1px" Font-Size="0.8em"
                                    ForeColor="#333333" OnAuthenticate="Login2_Authenticate" FailureText="Pini eshte i pasakte, ju lutem provojeni perseri."
                                    LoginButtonText="Hyrje" PasswordLabelText="Pini:" PasswordRequiredErrorMessage="Duhet te jepet pini."
                                    TitleText="Hyrje ne Sistem"
                                    UserNameLabelText=" " UserNameRequiredErrorMessage=" "
                                    DestinationPageUrl="FaqeKryesore.aspx" EnableTheming="True" TextLayout="TextOnLeft"
                                    PasswordRecoveryText="Harruar Fjalekalimin?"
                                    InstructionText="   Nuk eshte aktivizuar" LoginButtonImageUrl="~/images/FaqjaPare/loginbutton.png">
                                    <LayoutTemplate>
                                        <%--     <div class="input" style="height:auto;max-height:40px;padding-top:0px; top: 0px; left: 0px;">
                                            <dx:ASPxComboBox ID="cmbServerat" ButtonStyle-HoverStyle-BackColor="#00A74F" IncrementalFilteringMode="Contains" DropDownStyle="DropDownList" ItemStyle-SelectedStyle-BackColor="#00A74F" Border-BorderColor="#999999" Paddings-Padding="0" Width="100%" Height="40px"  Font-Size="16px" Theme="Metropolis"  ClientInstanceName="cmbServerat" runat="server">
                                                <ClientSideEvents Init="cmbServerInit" SelectedIndexChanged="cmbServerSelectedChanged" />     
                                                <ValidationSettings ValidationGroup="Login1" RequiredField-IsRequired="true" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom"></ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </div>--%>
                                        <div class="input" style="display: none;" >
                                            <dx:ASPxTextBox ID="UserName" Visible="false" ClientInstanceName="txtUserName" Width="100%" Height="40px" Border-BorderColor="#999999" Border-BorderStyle="Solid" Font-Size="16px" runat="server" Paddings-Padding="0" Theme="Metropolis">
                                                <ClientSideEvents Init="txtUsername_Init" />
                                                <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="Login1" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom">
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div class="input" style="display:none;">
                                            <dx:ASPxTextBox ID="Password" Visible="false" ClientInstanceName="txtPassword" Password="true" runat="server" Font-Size="16px" Border-BorderStyle="Solid" Theme="Metropolis"
                                                Paddings-Padding="0" Height="40" Width="100%" Border-BorderColor="#999999">
                                                <ClientSideEvents Init="txtPassword_Init" />
                                                <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="Login1" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom"></ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div class="input" id="divPin" style="width:98%; margin-left:1px;">
												<dx:ASPxTextBox ID="fusheKodi" ClientInstanceName="fusheKodi" Width="100%" Height="40px" Border-BorderColor="#999999" Border-BorderStyle="Solid" Font-Size="16px" runat="server" Paddings-Padding="0" Theme="Metropolis" Password="true" autocomplete="new-password">
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div class="input LoginError ">
                                            <dx:ASPxLabel ID="FailureText" ForeColor="Red" runat="server"></dx:ASPxLabel>
                                        </div>
                                        <div class="input" style="padding-top: 10px; max-width: 246px; padding-left: 1px;">
                                            <dx:ASPxButton Text="Hyrje" runat="server" Height="40px" Width="100%" ClientVisible="true" ClientInstanceName="butonPerTeHyre"
                                                CommandName="Login"
                                                ValidationGroup="Login1"
                                                ID="butonPerTeHyre" BackColor="#ed1b24"
                                                Border-BorderStyle="None"
                                                HorizontalAlign="Center"
                                                Theme="Metropolis"
                                                AllowFocus="False"
                                                Font-Size="23px" Font-Bold="true" Font-Names="Open Sans" ForeColor="White">
                                                <ClientSideEvents Click="function(s,e){getClientDate();}"
                                                    Init="function(s,e){makeVisibleLogin(s,e);}" />
                                                <Border BorderColor="White" />
                                            </dx:ASPxButton>
                                        </div>
                                        <div class="input" style="height: auto">
                                            <dx:ASPxLabel ForeColor="#ed1b24" ID="PasswordRecoveryLink" Visible="false" CssClass="keniHarruar" runat="server" Cursor="pointer" Text="Keni harruar fjalëkalimin?" AssociatedControlID="UserName" ClientSideEvents-Click="function(s,e){merrUsername(s,e)}">
                                            </dx:ASPxLabel>
                                        </div>
                               
                                        <dx:ASPxHiddenField runat="server" ID="loginHiddenField" ClientInstanceName="loginHiddenField"></dx:ASPxHiddenField>
                                        <%-- <dx:ASPxLabel runat="server" CssClass="input" ID="lblHyrje" Text="Hyrje në Sistem" Font-Names="Arial" Font-Size="Large"></dx:ASPxLabel>

                                        --%>
                                    </LayoutTemplate>

                                    <ValidatorTextStyle Font-Size="Medium" />
                                    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                                    <LabelStyle Font-Bold="False" Font-Size="Medium" ForeColor="#666666" Height="40px" />
                                    <TitleTextStyle BackColor="#8FC74A" Font-Bold="True" Font-Size="Large" ForeColor="White"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top"
                                        Wrap="False" Height="30" />
                                    <HyperLinkStyle Font-Size="Small" Font-Underline="False" ForeColor="#666666" CssClass="styleFloatLeft" />
                                </asp:Login>

            <div class="input" style="height: auto">
                                       <dx:ASPxLabel runat="server" ID="lblmsg" Visible="false"></dx:ASPxLabel>   
                                            </div>

                            </section>
                        </section>
                    </section>
                </section>
            </section>
        </div>
    </form>
    <script language="javascript">
        (function () {

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");
			
			
            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {// If Internet Explorer, return version number
                var pass = txtPassword.GetInputElement();
                var inputcmb = cmbServerat.GetInputElement();

                pass.setAttribute('placeholder', loginHiddenField.Get("passlbl"));
                inputcmb.setAttribute('placeholder', loginHiddenField.Get("srvlbl"));

            } else                 // If another browser, return 0
            {
                if (typeof cmbServerat !== "undefined")
                    cmbServerat.Focus();
            }

        })();
		
		

    </script>
	<script language="javascript">
		document.addEventListener('DOMContentLoaded', function() {
				document.getElementById('divPin').getElementsByTagName('input')[0].setAttribute("placeholder","PIN");
		
		});
	</script>
	
</body>
</html>
