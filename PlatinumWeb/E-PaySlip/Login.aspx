<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PlatinumWeb.E_PaySlip.Login" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxoc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html> 
<head runat="server">
    <title>Alpha Web</title>
    <link rel="icon" type="image/ico" href="CRM/faviconCRM.ico"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="Login-i AlphaWeb" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
<link rel="stylesheet" href="~/public/login.css" />




<style>

    @font-face {
            font-family: 'Open Sans';
            font-style: normal;
            font-weight: 400;
            src: local('Open Sans'), local('OpenSans'), url(css/fonts/OpenSans-Regular.ttf) format('truetype');
        }


        html > /**/ body body * {
            font-family: 'Open Sans', serif;
            font-size: 16px;
        }


        .cpyright, .ndihmeKontakt, article, .kosove, .vizitori, .labelInfo, .infoLbl, .keniHarruar {
            font-family: 'Open Sans';
        }


        .styleLoginButton {
            height: 28px;
            float: right;
            vertical-align: top;
            margin-top: 0;
        }


        .labelInfo {
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
            display: block;
            height: auto;
        }

        .login {
            display: block;
            padding: 40px;
            margin-top: 40px;
            background-color: #f7f7f7;
            border-radius: 2px;
            border: #f7f7f7;
            box-shadow: 0 2px 2px rgba(0, 0, 0, 0.3);
            border-collapse: separate !important;
        }

        .input {
            display: block;
            width: 250px;
            padding-top: 10px;
            height: 40px;
            position: relative;
            border: 0.5px #999999;
        }

        .submain {
            width: 338px;
            margin-top: 75px;
            margin-bottom: 60px;
        }
        
        .content {
            border-top: 1px #ed1b24 solid;
            border-bottom: none;
            padding: 0;
            margin: 0;
            clear: both;
        }

        footer {
            display: none;
        }

        .cpyright {
            /* clear: both; */
            float: left;
            font-size: 16px;
            text-align: left;
            max-width: 100px;
            margin-bottom: 10px;
        }

        .contact {
            float: right;
            line-height: 24px;
        }

        article {
            display: block;
        }

        .ndihmeKontakt, .kosove, .vizitori {
            font-size: 14px;
            text-align: right;
        }

        .LoginError {
            height: auto;
            color: red;
            text-align: left;
            padding-top: 0;
        }

        .main {
            width: 100%;
            margin: 0 auto;
            text-align: center;
        }


        .mainContent {
            padding-top: 60px;
            padding-bottom: 60px;
            display: table-cell;
            vertical-align: middle;
            padding: 0;
            position: relative;
            height: auto;
            width: 900px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            top: 0;
            left: 0;
        }

        .info {
            max-width: 300px;
            display: table-cell;
            text-align: left;

        }

        .loginForm {
            max-width: 332px;
            display: table-cell;
            text-align: right;
        }

        .infoLbl {
            font-size: 16px;
            color: #333333;
            width: auto;
        }

        .logo {
            background-image: url(images/FaqjaPare/vodSlogan.png);
            background-repeat: no-repeat;
            float: left;
            width: 285px;
            height: 68px;
            margin-bottom: 0;
            border: none;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }


        .infoIcon {
            width: 32px;
            height: 32px;
        }

        input[type="text"].dxeEditAreaSys, input[type="password"].dxeEditAreaSys {
            padding-left: 7px;
        }

        .left, .right {
            display: table;
            height: inherit;
        }

        .left {
            margin-top: 20px;
        }

        .mainContent .left {
            float: left;
        }

        .left {
            display: none;
        }

        .right {
            float: none;
            display: inline-block;
            margin: auto;
        }

        .mylink {
            color: #ed1b24;
        }

        .hyrje {
            color: white;
            background-color: #ed1b24;
            height: 40px;
            width: 100%;
            max-width: 246px;
            padding-left: 1px;
        }

        .hyrje:hover {
            background-color: #f03d45;
        }

        .keniHarruar {
            text-align: left;
            display: block;
            font: medium;
            font-weight: bold;
            color: #ed1b24;
            height: auto;
            font-size: 13px;
        }

        .keniHarruar:hover {
            color: #f03d45;
        }

        .klient {
            background-image: url(images/keshBCG.jpg);
            background-position: center
        }

        body {
            background-image: url(images/vodBCG.png);
            background-position: top left;
            background-repeat: no-repeat;
        }





        @media all and (max-width:760px) {
            .login {
                padding: 20px;
            }

            .infoLbl {
                max-width: 250px;
            }

            .ndihmeKontakt, .kosove, .vizitori {
                font-size: 11px;
            }
        }



        @media all and (max-width:450px) {
            .main {
                width: 100%;
                padding: 0;
                margin: 0;
                -moz-box-sizing: border-box;
                -webkit-box-sizing: border-box;
                box-sizing: border-box;
            }

            .gjuhet {
                height: 42.13px;
            }

            .submain {
                width: 100%;
                padding: 0;
                margin: 0;
                margin-top: 40px;
                -moz-box-sizing: border-box;
                -webkit-box-sizing: border-box;
                box-sizing: border-box;
            }

            .mainContent {
                width: 100%;
                -moz-box-sizing: border-box;
                -webkit-box-sizing: border-box;
                box-sizing: border-box;
            }
        }





        @media all and (min-width:690px) {
            .info, .loginForm {
                vertical-align: middle;
            }
        }

        @media all and (max-width:690px) {

            .left, .right, .loginForm, .info {
                float: none;
                display: inline-block;
            }

            .left, .right {
                height: auto;
            }

            .loginForm, .info {
                margin-bottom: 20px;
            }

            .input {
                padding-top: 0;
                padding-bottom: 0;
            }

            .mainContent {
                margin-top: 20px;
            }

            .info {
                padding-top: 0;
                margin-top: 20px;
            }
        }


</style>





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

    function makeVisibleLogin(s, e) {
        s.SetVisible(true);
    }

    function merrUsername(s, e) {
        var username = txtUserName.GetText();
        if (username == null || username == "") {
            txtUserName.SetIsValid(false);
            return;
        }
        var url = window.location.href;
        url = replaceQueryString(url, "harroPw", 1);
        url = replaceQueryString(url, "user", username);
        window.location.href = url;
    }
    function replaceQueryString(url, param, value) {
        var re = new RegExp("([?|&])" + param + "=.*?(&|$)", "i");
        if (url.match(re))
            return url.replace(re, '$1' + param + "=" + value + '$2');
        else {
            var isQuestionMarkPresent = url && url.indexOf('?') !== -1,
           separator = '';
            separator = isQuestionMarkPresent ? '&' : '?';
            return url + separator + param + "=" + value;
        }
    }
    function IsOldIE() {
        return ASPxClientUtils.ie && (ASPxClientUtils.browserMajorVersion < 9);
    }



    function txtPassword_Init(s, e) {
        var elem = s.GetInputElement();
        elem.placeholder = loginHiddenField.Get("passlbl");
    }

    function txtUsername_Init(s, e) {
        var elem = s.GetInputElement();
        elem.placeholder = loginHiddenField.Get("userlbl");

    }

    function cmbServerInit(s, e) {

        var input = s.GetInputElement();

        input.placeholder = loginHiddenField.Get("srvlbl");
        var value = localStorage.getItem("server_key");
        if (value != null) {
            var item = cmbServerat.FindItemByValue(value);
            cmbServerat.SetSelectedItem(item);
        }
    }
    function cmbServerSelectedChanged(s, e) {
        var item = cmbServerat.GetValue();
        localStorage.setItem("server_key", item);
    }
    function initLabelInfo(s, e) {
        if (s.GetText().length > 0)
            s.SetVisible(true);
    }



</script>

</head>
<body onload="load" >

    <form id="authFormPayslip" 
         runat="server" style="width: 100%">
        <dx:ASPxHiddenField ID="clientDate" ClientInstanceName="clientDate" runat="server">
        </dx:ASPxHiddenField>
        <div class="main">
            <section class="submain">
                <header class="header">
                    <div class="logo"></div>
                    <section class="gjuhet">
                   <div id="Div3" class="gjuha" runat="server">
                            <dx:ASPxHyperLink ID="lblGjuhaEN" CssClass="mylink" Font-Underline="false" runat="server" Text="EN" Font-Bold="False" Font-Size="11pt">
                            </dx:ASPxHyperLink>
                        </div>
                        <div class="gjuha" runat="server" id="Div4">
                            <dx:ASPxHyperLink ID="lblGjuhaAL" CssClass="mylink" Font-Underline="false" runat="server" Text="AL" Cursor="pointer" Font-Bold="False"
                                Font-Size="11pt">
                            </dx:ASPxHyperLink>
                        </div>
                    </section>
                </header>
                <section class="content">
                    <header>
                            <dx:ASPxLabel ID="LabelInfo" ClientInstanceName="LabelInfo" CssClass="labelInfo"  runat="server" Style="color: #FF0000; font-size: medium; font-weight: bold; font-family: Calibri">
                                <ClientSideEvents  Init="initLabelInfo"/>
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
                                       <%--     <dx:ASPxHyperLink runat="server" ID="HelpLink" Font-Underline="false" ForeColor="#00a74f" CssClass="infoLbl" Text="Të rejat e versionit 4.7" Target="_blank">
                                            </dx:ASPxHyperLink>--%>
                                        </td>
                                    </tr>
                                </table>
                        </section>
                        </section>
                        <section class="right">
                            <section class="loginForm">
                            <asp:Login ID="Login1" CssClass="login" runat="server"  LoginButtonType="Image"
                                  BorderStyle="Solid" BorderWidth="1px"  Font-Size="0.8em"
                                ForeColor="#333333" OnAuthenticate="Login1_Authenticate" FailureText="Perdoruesi ose fjalekalimi eshte i pasakte, ju lutem provojeni perseri."
                                LoginButtonText="Hyrje" PasswordLabelText="Fjalekalimi:" PasswordRequiredErrorMessage="Duhet te jepet fjalekalimi."
                                TitleText="Hyrje ne Sistem"
                                UserNameLabelText="Perdoruesi:" UserNameRequiredErrorMessage="Duhet te jepet perdoruesi."
                                DestinationPageUrl="FaqeKryesore.aspx" EnableTheming="True" TextLayout="TextOnLeft"
                                PasswordRecoveryText="Harruar Fjalekalimin?" PasswordRecoveryUrl="LoginFail.aspx?harroPw=1&user='<%# Eval(Login1.UserName) %>'"
                                InstructionText="   Nuk eshte aktivizuar" LoginButtonImageUrl="~/images/FaqjaPare/loginbutton.png">
                                <LayoutTemplate>
                                        <div class="input" style="height:auto;max-height:40px;padding-top:0px">
                                            <dx:ASPxComboBox ID="cmbServerat" ButtonStyle-HoverStyle-BackColor="#00A74F" IncrementalFilteringMode="Contains" DropDownStyle="DropDownList" ItemStyle-SelectedStyle-BackColor="#00A74F" Border-BorderColor="#999999" Paddings-Padding="0" Width="100%" Height="40px"  Font-Size="16px" Theme="Metropolis"  ClientInstanceName="cmbServerat" runat="server">
                                                <ClientSideEvents Init="cmbServerInit" SelectedIndexChanged="cmbServerSelectedChanged" />     
                                                <ValidationSettings ValidationGroup="Login1" RequiredField-IsRequired="true" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom"></ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </div>
                                        <div class="input">
                                            <dx:ASPxTextBox ID="UserName" ClientInstanceName="txtUserName"  Width="100%" Height="40px" Border-BorderColor="#999999" Border-BorderStyle="Solid" Font-Size="16px" runat="server" Paddings-Padding="0" Theme="Metropolis">
                                                <ClientSideEvents Init="txtUsername_Init"  />
                                                <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="Login1" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom">
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div class="input">
                                            <dx:ASPxTextBox ID="Password" ClientInstanceName="txtPassword"  Password="true" runat="server" Font-Size="16px" Border-BorderStyle="Solid" Theme="Metropolis"
                                                Paddings-Padding="0" Height="40" Width="100%" Border-BorderColor="#999999">
                                                <ClientSideEvents Init="txtPassword_Init" />
                                                <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="Login1" ErrorFrameStyle-Paddings-Padding="0"  ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom"></ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div class="input LoginError ">
                                            <dx:ASPxLabel ID="FailureText" ForeColor="Red" runat="server"></dx:ASPxLabel>
                                        </div>
                                        <div class="input">
                                            <dx:ASPxButton runat="server" Height="40px" Width="100%" ClientVisible="false" ClientInstanceName="loginButton" CssClass="hyrje"
                                                CommandName="Login"
                                                ValidationGroup="Login1"
                                                ID="loginButton"
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
                                        <div class="input" style="height:auto">
                                            <dx:ASPxLabel ID="PasswordRecoveryLink" CssClass="keniHarruar" runat="server" Cursor="pointer" Text="Keni harruar fjalëkalimin?" AssociatedControlID="UserName" ClientSideEvents-Click="function(s,e){merrUsername(s,e)}">
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
                        </section>
                        </section>
                        
                        
                    </section>
                </section>
                <footer>
                    <section class="cpyright">
                        <dx:ASPxLabel ID="ASPxLabel8" CssClass="cpyright" runat="server"  Text="© 2015 IMB" />
                    </section>
                    <section class="contact">
                        <article>
                            <dx:ASPxLabel ID="ASPxLabel9" CssClass="ndihmeKontakt" runat="server"  
                                Text="Kontakto për ndihmë!  |  Tel : +355 4 22 53 466 / 4 22 55 121 / 4 22 55 123"
                                Height="20">
                            </dx:ASPxLabel>
                        </article>
                        <article>
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" CssClass="kosove" 
                                Text="Kosovë : +377 44177110" Height="20" Style="margin-bottom: 0">
                            </dx:ASPxLabel>
                        </article>
                        <section>
                        <%--    <dx:ASPxLabel ID="ASPxLabel11" CssClass="vizitori" runat="server"
                                Text="Vizitorët mund të logohen me përdorues: vizitor dhe fjalëkalim: vizitor"
                                Width="100%" Height="20" Style="margin-bottom: 0">
                            </dx:ASPxLabel>--%>
                        </section>
                    </section>
                </footer>
          </section>
        </div>
    </form>
 <script>
        function load() {

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {// If Internet Explorer, return version number
                var pass = txtPassword.GetInputElement();
                var txtUser = txtUserName.GetInputElement();
                var inputcmb = cmbServerat.GetInputElement();

                pass.setAttribute('placeholder', loginHiddenField.Get("passlbl"));
                txtUser.setAttribute('placeholder', loginHiddenField.Get("userlbl"));
                inputcmb.setAttribute('placeholder', loginHiddenField.Get("srvlbl"));

            } else                 // If another browser, return 0
            {
                if (typeof cmbServerat !== "undefined" && cmbServerat.GetVisible())
                    cmbServerat.Focus();
            }

        };
    </script>
</body>
</html>