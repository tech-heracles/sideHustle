function createChannelTabs(channels) {
    $('#chatTabs').html('');
    $('#chatTabs').append('<ul></ul>');
    for (var i = 0; i < channels.length; i++) {
        var channel = channels[i];
        $('#chatTabs > ul').append('<li><a href=' + channel.tabSelector + '>' + channel.text + '</a></li>');
        $('#chatTabs').append('<div id="' + channel.tabSelector.replace('#', '') + '"><div><ul></ul></div><div class="footer"><div class="text"><input type="text" placeholder="Shkruaj tekstin ketu" class="sendText" /></div><div class="send"><input type="button" value="Send" /></div></div></div>');
    }
    $('#chatTabs').tabs('refresh');
}

function succeded(result) {
    $('#mail').button({
        text: false,
        icons: {
            primary: 'ui-icon-mail-closed'
        }
    });
    channels = result.channels;
    fayeClient = new Faye.Client(result.url); //result.url
    if (fayeClient) {
        $("#chatDiv").dialog({
            position: ['center', 'center'],
            width: 500,
            maxHeight: 400,
            resizable: false,
            modal: true,
            autoOpen: false,
            close: function (event, ui) { $('#backDiv').show(); }, //$('#mail').removeClass('ui-icon-mail-open').addClass('ui-icon-mail-closed'); nuk duhet me 
            open: function (event, ui) {
                $('.ui-widget-overlay').bind('click', function () { $("#chatDiv").dialog('close'); });
                $("#mail").button("option", {
                    icons: { primary: "ui-icon-mail-closed" }
                });
            }
        }).dialog("close");
        $('#chatTabs').tabs({
            active: 0,
            collapsible: false,
            //heightStyle: "auto",
            activate: function (event, ui) {
                selectedTabSelector = ui.newPanel.selector;
                currentChannel = $.grep(channels, function (e) { return e.tabSelector == selectedTabSelector; })[0];
                $(selectedTabSelector).animate({ scrollTop: $(selectedTabSelector + " .sendText").offset().top }, 200); //kjo gjeja duhet per te bere scroll automatikisht poshte :P
                $(selectedTabSelector + '.sendText').focus();
                $('.ui-tabs-nav .ui-corner-top[aria-controls=' + selectedTabSelector.replace('#', '') + ']').removeClass('ui-state-highlight', 600);
            },
            create: function (event, ui) {

            }
        });
        initFaye(lblUserEmri.GetText(), result.ndermKod, result.token, channels);
        $('.send input[type=button]').button();
        $('.sendText').button();
        $('.send input[type=button]').on('click', publikoMsg);
        $('#mail').on('click', function () {
            $("#chatDiv").dialog("open");
        });
        $(document).on('keydown', '.sendText', function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') {
                publikoMsg();
            }
        });
    }
}

function initFaye(username, ndermKod, user_token, channels) {
    createChannelTabs(channels);
    fayeClient.addExtension({
        outgoing: function (message, callback) {
            if (message.channel !== '/meta/subscribe')
                return callback(message);
            message.ext = message.ext || {};
            message.ext.username = username;
            message.ext.token = user_token;
            message.ext.ndermKod = ndermKod;
            callback(message);
        }
    });
    for (var i = 0; i < channels.length; i++) {
        subcribe(channels[i]);
    }
}

function subcribe(channel) {
    fayeClient.subscribe(channel.channel, function (mesazh) {
        mesazh.tabSelector = channel.tabSelector;
        sendMsgToList(mesazh, channel.tabSelector);
    }).then(function () { addDefaultMsgToList(channel.tabSelector); }, errorFunc);
}

function initChat() {
    if (typeof Faye == "undefined") {
        $('#mail').hide();
        return;
    }
    $.ajax({
        type: 'POST',
        url: 'wsfunc.asmx/getChatData',
        cache: false,
        success: function (data, textStatus, jqXHR) {
            if (data.d.Error) {
                alert(data.d.Error);
                return;
            }
            succeded(data.d);
        },
        error: function (jqXHR, textStatus, errorThrown) {console.log(errorThrown) },
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
}

function sendMsgToList(mesazh, channelTabSelector) {
    if (mesazh.text.indexOf('/pop') == 0) {
        mesazh.text = mesazh.text.substr('/pop'.length, mesazh.text.length - 1);
        $("#chatDiv").dialog("open");
        if (channelTabSelector !== selectedTabSelector) {
            $("chatTabs").tabs('option', 'active', $.grep(channels, function (e) { return e.tabSelector == channelTabSelector; })[0].indeks);
        }
    }
    var tmpLi = createMsgLi(mesazh);
    $(channelTabSelector + " ul").append(tmpLi);
    if (channelTabSelector === selectedTabSelector) {
        $(selectedTabSelector).animate({ scrollTop: $(selectedTabSelector + " .sendText").offset().top }, 200); //kjo gjeja duhet per te bere scroll automatikisht poshte :P
        $(selectedTabSelector + '.sendText').focus();
    }
    else {
        $("#mail").button("option", {
            icons: { primary: "ui-icon-mail-open" }
        });
        //$('#mail').removeClass('ui-icon-mail-closed').addClass('ui-icon-mail-open');
        $('.ui-tabs-nav .ui-corner-top[aria-controls=' + channelTabSelector.replace('#', '') + ']').addClass('ui-state-highlight', 600);
    }
}

function errorFunc(error) {
    alert('There was a problem: ' + error.message);
}

function addSystemMsgToList(text) {
    //var tmpLi;
    //tmpLi = createMsgLi({ username: "System", text: text, timeString: getTimeString() });
    //$(selectedTabSelector + " ul").append(tmpLi);
    sendMsgToList({ username: "System", text: text, timeString: getTimeString() }, selectedTabSelector);
}

function addDefaultMsgToList(tabSelector) {
    var tmpLi;
    if ($(tabSelector + " ul li").length == 0) {
        sendMsgToList({ username: "System", text: text, timeString: getTimeString() }, tabSelector);
        //tmpLi = createMsgLi({ username: "System", text: "Mireserdhet ne Kanalin " + $.grep(channels, function (e) { return e.tabSelector == tabSelector; })[0].text + ".", timeString: getTimeString() });
        //$(tabSelector + " ul").append(tmpLi);
    }
}

function createMsgLi(mesazh) {
    var tmpLi = $('<li><span class="mesazh">' + mesazh.text + "</span><span>" + mesazh.timeString + "</span></li>").attr('data-username', mesazh.username + ":");
    if (lblUserEmri.GetText() == mesazh.username || mesazh.username == "System") {
        tmpLi.addClass('myUser');
        return tmpLi;
    }
    tmpLi.addClass('notMyUser');
    return tmpLi;
}

function publikoMsg() {
    var st = $(selectedTabSelector + ' .sendText');
    var stMesazh = st.val();
    if (stMesazh === "")
        return;
    if (!$.grep(channels, function (e) { return e.tabSelector == selectedTabSelector; })[0].canPublish) {
        addSystemMsgToList("Nuk keni te drejta te shkruani ne kete chat!!!");
        st.val('');
        return;
    }
    var publication = fayeClient.publish(currentChannel.publish, { username: lblUserEmri.GetText(), text: stMesazh, timeString: getTimeString() });
    publication.then(function () {
        //alert('Message received by server!');
    }, function (error) {
        alert('There was a problem: ' + error.message);
    });
    st.val('');
}

function getTimeString() {
    var time = new Date();
    return time.getHours() + ":" + (time.getMinutes() < 10 ? '0' : '') + time.getMinutes();
}
//END CHAT FUNCTIONS