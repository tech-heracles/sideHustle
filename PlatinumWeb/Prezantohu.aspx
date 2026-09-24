<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Prezantohu.aspx.cs" Inherits="PlatinumWeb.login" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>AVEC Accounting</title>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="AVEC Accounting - hyrje" />
    <meta name="identifikuesLogin" content="LoginPage" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <meta name="theme-color" content="#121212" />
    <link rel="icon" type="image/x-icon" href="favicon.ico" />
    <link rel="apple-touch-icon" href="images/brand/avec-icon-192.png" />
    <link href="css/avec-login.css" rel="stylesheet" />
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
                LabelInfo.SetText('Ju lutem vendosni perdoruesin!');
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
            if (s.GetText() == "") {
                s.SetFocus();
            }
            else {
                loginButton.SetFocus();
            }
        }

        function IsNotNullOrEmpty(stringu) {
            return stringu && stringu != undefined && stringu != "undefined" && stringu != "";
        }

        function checkExistUrl() {
            if ((getUrlVar('organizata') && getUrlVar('organizata') != "undefined" && getUrlVar('organizata') != '' &&
                getUrlVar('gjuha') && getUrlVar('gjuha') != "undefined" && getUrlVar('gjuha') != '')
                || (getUrlVar('arsye') && getUrlVar('arsye') != "undefined"))
                return true;
            return false;
        }

        function cmbServerInit(s, e) {

            var input = s.GetInputElement();

            input.placeholder = loginHiddenField.Get("srvlbl");
            var lastSelectedItem = localStorage.getItem("server_key");
            if (IsNotNullOrEmpty(getUrlVar('organizata')) && IsNotNullOrEmpty(getUrlVar('gjuha'))) {
                document.getElementById("Login1_OrganisationSwitch").style.display = "block";
                //document.getElementById("comboBox").style.display = "none";
                if (cmbServerat.FindItemByText(getUrlVar('organizata')) === null)
                    cmbServerat.SetSelectedItem(null);
                else {
                    cmbServerat.SetSelectedItem(cmbServerat.FindItemByText(getUrlVar('organizata')));
                    localStorage.setItem("server_key", JSON.stringify({ value: cmbServerat.GetValue(), text: cmbServerat.GetText() }));
                }
            }
            else {
                if (IsNotNullOrEmpty(getUrlVar('arsye')) || IsNotNullOrEmpty(getUrlVar('ReturnUrl'))) {
                    document.getElementById("Login1_OrganisationSwitch").style.display = "block";
                    //    document.getElementById("comboBox").style.display = "none";
                }
                if (lastSelectedItem != null) {
                    try {
                        lastSelectedItem = JSON.parse(lastSelectedItem);
                    }
                    catch (e) {
                        lastSelectedItem = { text: 'Kryesor', value: 0 };
                        localStorage.setItem("server_key", lastSelectedItem);
                    }
                    var item = lastSelectedItem.value != undefined && cmbServerat.FindItemByValue(lastSelectedItem.value);
                    if (!lastSelectedItem.value) {
                        if (cmbServerat.FindItemByText('Kryesor'))
                            cmbServerat.SetSelectedItem(cmbServerat.FindItemByText('Kryesor')); //kryesori eshte default nga sp T_LICENCA_merrLicencatMeDbMeFilter
                        else
                            cmbServerat.SetSelectedIndex(cmbServerat.AddItem('Kryesor', '0'));

                        return;
                    }
                    else {
                        if (!cmbServerat.FindItemByValue(lastSelectedItem.value))//kur ka last selected item por nuk eshte ne dt filestar, e shtojme 
                            cmbServerat.SetSelectedIndex(cmbServerat.AddItem(lastSelectedItem.text, lastSelectedItem.value));
                        else
                            cmbServerat.SetSelectedItem(item);
                        return;
                    }
                }
            }
        }

        function cmbServerSelectedChanged(s, e) {
            var item = cmbServerat.GetSelectedItem();
            if (item == undefined) return;
            localStorage.setItem("server_key", JSON.stringify({ value: item.value, text: item.text }));
        }

        function initLabelInfo(s, e) {
            //if (s.GetText().length > 0)
            //    s.SetVisible(true);
        }
    </script>
</head>
<body class="avec-login" onload="load()">
    <form id="authForm" runat="server">
        <dx:ASPxHiddenField ID="clientDate" ClientInstanceName="clientDate" runat="server">
        </dx:ASPxHiddenField>
        <asp:HiddenField ID="step1Complete" Value="false" runat="server"></asp:HiddenField>
        <main class="avec-login-page">
            <section class="avec-card">
                <header class="avec-card-header">
                    <img class="avec-logo" src="images/brand/avec-logo.png" alt="AVEC" />
                    <p class="avec-product">Accounting</p>
                </header>
                <dx:ASPxLabel ID="LabelInfo" ClientInstanceName="LabelInfo" CssClass="labelInfo" runat="server">
                    <ClientSideEvents Init="initLabelInfo" />
                </dx:ASPxLabel>
                <section class="loginForm">
                        <asp:Login ID="Login1" CssClass="login" runat="server" LoginButtonType="Image"
                            BorderStyle="None" OnAuthenticate="Login1_Authenticate" FailureText="Perdoruesi ose fjalekalimi eshte i pasakte, ju lutem provojeni perseri."
                            FailureText2="Otp nuk eshte e sakte."
                            LoginButtonText="Hyrje" PasswordLabelText="Fjalekalimi:" PasswordRequiredErrorMessage="Duhet te jepet fjalekalimi."
                            Login2ButtonText="Hyrje"
                            TitleText="Hyrje ne Sistem"
                            UserNameLabelText="Perdoruesi:" UserNameRequiredErrorMessage="Duhet te jepet perdoruesi."
                            DestinationPageUrl="FaqeKryesore.aspx" EnableTheming="True" TextLayout="TextOnLeft"
                            PasswordRecoveryText="Harruar Fjalekalimin?" PasswordRecoveryUrl="LoginFail.aspx?harroPw=1&user='<%# Eval(Login1.UserName) %>'"
                            InstructionText="Nuk eshte aktivizuar" LoginButtonImageUrl="~/images/FaqjaPare/loginbutton.png">
                            <LayoutTemplate>
                                <section runat="server" id="login_div">

                                    <div id="comboBox" class="input">
									<dx:ASPxComboBox ID="cmbServerat"
                                            IncrementalFilteringMode="Contains" DropDownStyle="DropDownList"
                                            Paddings-Padding="0" Width="100%" Height="40px" Font-Size="16px"
                                            ClientInstanceName="cmbServerat" runat="server" EnableSynchronization="True" >
                                            <ClientSideEvents Init="cmbServerInit" SelectedIndexChanged="cmbServerSelectedChanged" />
                                            <ValidationSettings ValidationGroup="Login1" RequiredField-IsRequired="true" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom"></ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </div>
                                    <div class="input inputUsername">
                                        <dx:ASPxTextBox ID="UserName" ClientInstanceName="txtUserName" Width="100%" Height="40px" Font-Size="16px" runat="server" Paddings-Padding="0">
                                            <ClientSideEvents Init="txtUsername_Init" />
                                            <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="Login1" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom">
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </div>
                                    <div class="input inputPassword">
                                        <dx:ASPxTextBox ID="Password" ClientInstanceName="txtPassword" Password="true" runat="server" Font-Size="16px"
                                            Paddings-Padding="0" Height="40" Width="100%">
                                            <ClientSideEvents Init="txtPassword_Init" />
                                            <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="Login1" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom"></ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </div>
                                    <div class="input LoginError ">
                                        <dx:ASPxLabel ID="FailureText" ForeColor="Red" runat="server"></dx:ASPxLabel>
                                    </div>
                                    <div class="input" style="display: flex; justify-content: center;">
                                        <dx:ASPxButton runat="server" Height="40px" Width="100%" ClientVisible="false" ClientInstanceName="loginButton" CssClass="hyrje"
                                            CommandName="Login"
                                            ValidationGroup="Login1"
                                            ID="loginButton"
                                            Border-BorderStyle="None"
                                            HorizontalAlign="Center"
                                            AllowFocus="False"
                                            Font-Size="17px" Font-Bold="true">
                                            <ClientSideEvents Click="function(s,e){getClientDate();}"
                                                Init="function(s,e){makeVisibleLogin(s,e);}" />

                                        </dx:ASPxButton>
                                        
                                        

                                    </div>
                                    <dx:ASPxHiddenField runat="server" ID="loginHiddenField" ClientInstanceName="loginHiddenField"></dx:ASPxHiddenField>
                                </section>
                                    <div id="OrganisationSwitch" class="avec-org-switch" style="display: none;" runat="server">
                                        <dx:ASPxHyperLink ID="NdryshoOrganizate" CssClass="mylink" ClientVisible="false" Font-Underline="false" runat="server" Text="Nderroni organizate" Font-Size="11pt" Cursor="pointer" name="NdryshoOrganizate"
                                            NavigateUrl="~/Prezantohu.aspx?clearOrg=true" onClick="clickSwitchOrganisation()">
                                        </dx:ASPxHyperLink>
                                    </div>
                                    <div class="input" style="height: auto; display: none;">
                                        <dx:ASPxLabel ID="PasswordRecoveryLabel" CssClass="keniHarruarLabel" runat="server" Text="Keni harruar fjalekalimin?" AssociatedControlID="UserName">
                                        </dx:ASPxLabel>
                                    </div>
                                    <div class="input" style="height: auto">
                                        <dx:ASPxLabel ID="PasswordRecoveryLink" CssClass="keniHarruar" runat="server" Cursor="pointer" Text="Keni harruar fjalëkalimin?" AssociatedControlID="UserName" ClientSideEvents-Click="function(s,e){merrUsername(s,e)}">
                                        </dx:ASPxLabel>
                                    </div>
                                    <section class="gjuhet">
                                        <div id="Div3" class="gjuha" runat="server">
                                            <dx:ASPxHyperLink ID="lblGjuhaEN" CssClass="mylink" Font-Underline="false" runat="server" Text="EN" Font-Size="11pt">
                                            </dx:ASPxHyperLink>

                                        </div>
                                        <div class="gjuha" runat="server" id="Div4">
                                            <dx:ASPxHyperLink ID="lblGjuhaAL" CssClass="mylink" Font-Underline="false" runat="server" Text="AL" Cursor="pointer"
                                                Font-Size="11pt">
                                            </dx:ASPxHyperLink>
                                        </div>
                                        <div class="gjuha" runat="server" id="Div1">
                                            <dx:ASPxHyperLink ID="lblGjuhaFR" CssClass="mylink" Font-Underline="false" runat="server" Text="FR" Cursor="pointer"
                                                Font-Size="11pt">
                                            </dx:ASPxHyperLink>
                                        </div>
                                    </section>
                                    
                                

                                <section runat="server" id="otp_div"  visible="false">

                                    <div class="avec-otp-qr">
                                        <asp:Literal ID="Otp_qr" runat="server"></asp:Literal>
                                    </div>

                                    <div class="input inputUsername">
                                        <dx:ASPxTextBox ID="Kodi" ClientInstanceName="txtKodi" Width="100%" Height="40px" Font-Size="16px" runat="server" Paddings-Padding="0">
                                            <ValidationSettings RequiredField-IsRequired="true" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom">
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </div>
                                    <div class="input LoginError ">
                                        <asp:label ID="otpError" ForeColor="Red" runat="server"></asp:label>
                                    </div>
                                    <p class="avec-otp-hint">Shkruani kodin tuaj OTP.</p>
                                <div class="input">
                                        <dx:ASPxButton runat="server" Height="40px" Width="100%" ClientVisible="false" ClientInstanceName="loginButton2" CssClass="hyrje"
                                            CommandName="Login"
                                            ValidationGroup="Login1"
                                            ID="loginButton2"
                                            Border-BorderStyle="None"
                                            HorizontalAlign="Center"
                                            AllowFocus="False"
                                            Font-Size="17px" Font-Bold="true">
                                            <ClientSideEvents Click="function(s,e){getClientDate();}"
                                                Init="function(s,e){makeVisibleLogin(s,e);}" />

                                        </dx:ASPxButton>

                                    </div>
                                </section>
                                
                            </LayoutTemplate>

                            <ValidatorTextStyle Font-Size="Medium" />
                            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                            <LabelStyle Font-Bold="False" Font-Size="Medium" ForeColor="#666666" Height="40px" />
                            <TitleTextStyle BackColor="#8FC74A" Font-Bold="True" Font-Size="Large" ForeColor="White"
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top"
                                Wrap="False" Height="30" />
                            <HyperLinkStyle Font-Size="Small" Font-Underline="False" ForeColor="#666666" CssClass="styleFloatLeft" />
                        </asp:Login>
                    <dx:ASPxHiddenField ID="hfPeriudhKontabel" runat="server" ClientInstanceName="hfPeriudhKontabel">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState">
            </dx:ASPxHiddenField>
                </section>
            </section>
            <footer class="avec-footer">
                <dx:ASPxLabel ID="ASPxLabel8" CssClass="cpyrightText" runat="server" Text="© AVEC" />
            </footer>
        </main>
    </form>
    <script>
        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        function getUrlVar(name) {
            return decodeURIComponent(getUrlVars()[name]);
        }

        function clickSwitchOrganisation() {
            localStorage.removeItem("organisation");
        }

        function load() {
            if (sessionStorage)
                sessionStorage.clear();

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
                //duhet hequr 
                if (typeof cmbServerat !== "undefined" && cmbServerat.GetVisible())
                    cmbServerat.Focus();
            }
        };
    </script>
</body>
</html>
