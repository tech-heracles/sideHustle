<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimeoutControl.ascx.cs" Inherits="PlatinumWeb.TimeoutControl" %>
<%@ Register Assembly="DevExpress.Web.v18.2" Namespace="DevExpress.Web" TagPrefix="dx" %>




<script type="text/javascript">
    window.SessionTimeout = (function () {
        var _timeLeft, _popupTimer, _countDownTimer, _coolDownTimer;

        var stopTimers = function () {
            window.clearTimeout(_popupTimer);
            window.clearTimeout(_countDownTimer);
            window.clearTimeout(_coolDownTimer);
        };

        var updateCountDown = function () {
            var min = Math.floor(_timeLeft / 60);
            var sec = _timeLeft % 60;
            if (sec < 10)
                sec = "0" + sec;

            document.getElementById("CountDownHolder").innerHTML = min + ":" + sec;

            if (_timeLeft > 0) {
                _timeLeft--;
                _countDownTimer = window.setTimeout(updateCountDown, 1000);
            } else {
                document.location.href = Paths.defaultLoginPath+'?arsye=MbarimSessioni';
            }
        };

        var showPopup = function () {
            _timeLeft = 120;
            updateCountDown();
            ClientTimeoutPopup.Show();
        };

        var schedulePopup = function (result) {
            stopTimers();
            _popupTimer = window.setTimeout(showPopup, result);
        };

        var sendKeepAlive = function () {
            stopTimers();
            ClientTimeoutPopup.Hide();
            //ClientKeepAliveHelper.PerformCallback();
            _coolDownTimer = window.setTimeout(sendKeepAliveOnServer, 60000);

        };
        var sendKeepAliveImmediately = function () {
            stopTimers();
            ClientTimeoutPopup.Hide();
            sendKeepAliveOnServer();

        };
        var sendKeepAliveOnServer = function () {
            $.ajax({
                method: "GET",
                url: Utils.getServerApiUrl("Konfigurime", "popupShowDelay"), sendKeepAlive: false
            }).done(schedulePopup)
        };
        return {
            schedulePopup: schedulePopup,
            sendKeepAlive: sendKeepAlive,
            sendKeepAliveImmediately: sendKeepAliveImmediately
        };

    })();
    $(document).ready(function () {
       
        window.SessionTimeout.sendKeepAlive();
    });
    
</script>

<dx:ASPxPopupControl EnableHierarchyRecreation="false" runat="server" ID="TimeoutPopup" ClientInstanceName="ClientTimeoutPopup"
    CloseAction="None" HeaderText="   Sesioni juaj po mbaron!" Modal="True" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="250px" 
    ShowFooter="True" AllowDragging="True" Theme="MetropolisBlue">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <br/>
            <dx:ASPxLabel runat="server" ID="KlikoniOkLabel"></dx:ASPxLabel>
            <br /><br />
            <span id="CountDownHolder"></span>
            <br />
        </dx:PopupControlContentControl>
    </ContentCollection>
    <HeaderStyle>
    <Paddings PaddingLeft="10px" PaddingBottom="3px" PaddingTop="3px" />
    </HeaderStyle>
    <FooterTemplate>
        <dx:ASPxButton runat="server" ID="OkButton" Text="OK" AutoPostBack="false">
            <ClientSideEvents Click="SessionTimeout.sendKeepAlive" />
        </dx:ASPxButton>
    </FooterTemplate>
    <HeaderImage Url="~/images/sessionExpire.png">
    </HeaderImage>
    <FooterStyle>
        <Paddings Padding="5" PaddingLeft="140px" />
    </FooterStyle>
</dx:ASPxPopupControl >
